using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Web;

namespace WorkTime.Utils.Dto
{
    /// <summary>
    /// DToConstructor
    ///     Статический класс- конструктор типов
    ///     Содержит расширения для object и Type
    /// </summary>
    public static class DToConstructor
    {
        public static Type[] DefaultIncludeTypes = { typeof(int?), typeof(int), typeof(double?), typeof(double), typeof(short?), typeof(short), typeof(float?), typeof(float), typeof(string), typeof(Guid?), typeof(Guid), typeof(DateTime?), typeof(DateTime), typeof(TimeSpan), typeof(TimeSpan?), typeof(DateTimeOffset), typeof(DateTimeOffset?), typeof(long?), typeof(long), typeof(bool), typeof(bool?) };
        private static string GetDynamicDtoTypeName(Type T)
        {
            return String.Format("Dynamic_{0}_DTo", T.Name);
        }

        private static string GetDynamicCustomTypeName(Type T, Type[] mixins, string[] excludeProps, Type[] ExtraPropertyTypes = null)
        {
            string result = String.Format("Dynamic_{0}_CustomImplements", T.Name);

            foreach (Type mixin in mixins)
            {
                result += String.Format("_{0}", mixin.Name);
            }

            result = String.Format("{0}_Exclude", result);

            if ((excludeProps != null) && (excludeProps.Length > 0))
            {
                foreach (string prop in excludeProps)
                {
                    result += String.Format("_{0}", prop);
                }
            }

            result = String.Format("{0}_ExtraProps", result);

            if ((ExtraPropertyTypes != null) && (ExtraPropertyTypes.Length > 0))
            {
                foreach (Type xtype in ExtraPropertyTypes)
                {
                    result += String.Format("_{0}", xtype.Name);
                }
            }

            result = String.Format("{0}_DTo", result);

            return result;
        }

        private static string GetDynamicMixinsTypeName(Type T, Type[] mixins)
        {
            string result = String.Format("Dynamic_{0}_Implements", T.Name);

            foreach (Type mixin in mixins)
            {
                result += String.Format("_{0}", mixin.Name);
            }

            result = String.Format("{0}_Dto", result);

            return result;
        }

        internal class PropDesc
        {
            internal PropertyInfo Property { get; set; }
            internal Type ParentType { get; set; }
        }

        private static Type _Implement(Type source, Type[] mixins, string[] excludeProps = null, bool inheriteFrombase = false, bool cutNonSimpleProperties = true, Type[] extraPropertyTypes = null)
        {
            Dictionary<string, PropertyInfo> properties = new Dictionary<string, PropertyInfo>();
            Dictionary<string, PropDesc> propertiesFromInterfaces = new Dictionary<string, PropDesc>();

            List<Type> _interfaces = new List<Type>();

            if (source.IsInterface)
            {
                _interfaces.Add(source);
                foreach (var prop in source.GetProperties()) { propertiesFromInterfaces[prop.Name] = new PropDesc() { Property = prop, ParentType = source }; }
            };

            if (!inheriteFrombase)
            {
                foreach (var prop in source.GetProperties())
                {
                    if ((cutNonSimpleProperties && DefaultIncludeTypes.Contains(prop.PropertyType)) || (extraPropertyTypes != null && extraPropertyTypes.Contains(prop.PropertyType)))
                    {
                        if ((excludeProps == null) || (!excludeProps.Contains(prop.Name)))
                        {
                            properties[prop.Name] = prop;
                        }
                    }
                }
            }

            if (mixins != null)
            {
                foreach (Type mixin in mixins)
                {
                    if (mixin.IsInterface)
                    {
                        _interfaces.Add(mixin);
                    }
                    foreach (var prop in mixin.GetProperties())
                    {
                        if (!properties.ContainsKey(prop.Name))
                        {
                            if ((cutNonSimpleProperties && DefaultIncludeTypes.Contains(prop.PropertyType)) || (extraPropertyTypes != null && extraPropertyTypes.Contains(prop.PropertyType)))
                            {
                                properties[prop.Name] = prop;

                                if (mixin.IsInterface)
                                { propertiesFromInterfaces[prop.Name] = new PropDesc() { Property = prop, ParentType = mixin }; }
                            }
                        }
                    }
                }
            }

            var DynamicTypeName =


              /*
               Если есть кастомные настройки (свойства, которые нужно исключить, либо типы, кторые нужно оставить) - генеряем специфическое длинное имя.
               */
                (extraPropertyTypes != null) || (excludeProps != null) ? GetDynamicCustomTypeName(source, mixins, excludeProps, extraPropertyTypes)
             : (
                /*
                 Если есть Mixin'ы  - генеряем имя с "Implenments".
                 */
                    (mixins != null) ? GetDynamicMixinsTypeName(source, mixins)
                /*
                  В противном случае - класс является простым DTO, генеряем короткое имя вида "Dybamic_ClassName_DTo" Если есть Mixin'ы  - генеряем имя с "Implenments".
                */

                    : GetDynamicDtoTypeName(source)
              );

            /* Динамический тип уже существует? */
            if (!DtoFactory.Instance.TypeExists(DynamicTypeName))
            {
                /*
                 * нет-создаём
                 */


                // Получаем ассемблибилдер из кэша АппДомена (чтоб не плодить динамические ассембли)
                AssemblyBuilder assemblyBuilder = DtoFactory.Instance.Builder;
                // Получаем модулебилдер
                ModuleBuilder moduleBuilder = assemblyBuilder.GetDynamicModule(DtoFactory.ModuleName) ?? assemblyBuilder.DefineDynamicModule(DtoFactory.ModuleName);
                // Создаем тип
                TypeBuilder typeBuilder = moduleBuilder.DefineType(DynamicTypeName, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable, inheriteFrombase ? source : typeof(System.Object), _interfaces.ToArray());

                foreach (Type I in _interfaces)
                {
                    typeBuilder.AddInterfaceImplementation(I);
                }

                foreach (var property in properties)
                {
                    /* создаём свойство */
                    PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(property.Key, PropertyAttributes.None, property.Value.PropertyType, Type.EmptyTypes);

                    /* создаем приватное поле с именем как у свойства и знаком "_" перед ним */
                    FieldBuilder _field = typeBuilder.DefineField("_" + property.Key, property.Value.PropertyType, FieldAttributes.Private);
                    /* определяем getter */
                    MethodAttributes _attributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.Virtual;
                    MethodBuilder _getter;
                    MethodBuilder _setter;

                    if (propertiesFromInterfaces.Count(p => p.Key == property.Key) > 0)
                    {

                        Type Interface = propertiesFromInterfaces[property.Key].ParentType;
                        _getter = typeBuilder.DefineMethod(property.Value.GetGetMethod().Name, _attributes, property.Value.PropertyType, Type.EmptyTypes);
                        _setter = typeBuilder.DefineMethod(property.Value.GetSetMethod().Name, _attributes, null, new Type[] { property.Value.PropertyType });

                        typeBuilder.DefineMethodOverride(_getter, property.Value.GetGetMethod());
                        typeBuilder.DefineMethodOverride(_setter, property.Value.GetSetMethod());
                    }
                    else
                    {
                        _getter = typeBuilder.DefineMethod("get_" + property.Key, _attributes, property.Value.PropertyType, Type.EmptyTypes);
                        _setter = typeBuilder.DefineMethod("set_" + property.Key, _attributes, null, new Type[] { property.Value.PropertyType });
                    }

                    ILGenerator _getterIL = _getter.GetILGenerator();
                    _getterIL.Emit(OpCodes.Ldarg_0);
                    _getterIL.Emit(OpCodes.Ldfld, _field);
                    _getterIL.Emit(OpCodes.Ret);


                    /* определяем setter */

                    ILGenerator _setterIL = _setter.GetILGenerator();
                    _setterIL.Emit(OpCodes.Ldarg_0);
                    _setterIL.Emit(OpCodes.Ldarg_1);
                    _setterIL.Emit(OpCodes.Stfld, _field);
                    _setterIL.Emit(OpCodes.Ret);

                    /* применяем getter и setter к свойству в новом классе */
                    propertyBuilder.SetGetMethod(_getter);
                    propertyBuilder.SetSetMethod(_setter);
                }

                return typeBuilder.CreateType();
            }
            else
            {
                /*
                 тип уже существует - получаем его из ассембли по имени
                */
                return DtoFactory.Instance.GetDynamicType(DynamicTypeName);
            }
        }

        /****************************************************************
                 Публичные методы
        *****************************************************************/


        public static object MapTo(this object source, object target)
        {
            foreach (var property in source.GetType().GetProperties())
            {
                var sourceProp = source.GetType().GetProperty(property.Name);
                var targetProp = target.GetType().GetProperty(property.Name);
                var val = sourceProp.GetValue(source, null);
                if (targetProp != null) { targetProp.SetValue(target, val); };
            }
            return target;
        }

        public static object MapFrom(this object target, object source)
        {
            foreach (var property in source.GetType().GetProperties())
            {
                var sourceProp = source.GetType().GetProperty(property.Name);
                var targetProp = target.GetType().GetProperty(property.Name);
                var val = sourceProp.GetValue(source, null);
                if (targetProp != null) { targetProp.SetValue(target, val); };
            }
            return target;
        }


        public static Type Implement(Type source, Type[] mixins)
        {
            return _Implement(source, mixins, null);
        }

        public static Type Implement(Type source, Type[] mixins, string[] excludeProperties)
        {
            return _Implement(source, mixins, excludeProperties);
        }


        public static Type Dto<TSource>()
        {
            return _Implement(typeof(TSource), new Type[] { });
        }

        public static object Dto(this object source)
        {
            var target = Activator.CreateInstance(_Implement(source.GetType(), null));
            return target.MapFrom(source);
        }

        public static object Implement<T>(this object source)
        {
            var mixed = Activator.CreateInstance(_Implement(source.GetType(), new Type[] { typeof(T) }, new string[] { }, true));
            /*
             Можно добавить маппирование данных через AutoMapper
             */
            return mixed.MapFrom(source);
        }
    }
}