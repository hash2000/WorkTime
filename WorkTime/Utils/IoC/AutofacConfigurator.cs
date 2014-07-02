using Autofac;
using Autofac.Extras.AggregateService;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using WorkTime.Models;
using WorkTime.Utils.MVC.Security;

namespace WorkTime.Utils.IoC
{
    public static class AutofacConfigurator
    {
        public static void ConfigureDbContextEntity<TEntity>(ContainerBuilder builder)
        {
            // ! DbContext общий в пределах области жизненного цикла
            // (один для всех в пределах HTTP-запроса для Web-приложения)
            builder.RegisterType<TEntity>()
                .As<DbContext>()
                .AsSelf()
                .SingleInstance();
        }

        public static void ConfigureEntity<TEntity>(ContainerBuilder builder)
        {
            builder.RegisterType<TEntity>()
                .AsSelf()
                .SingleInstance();
        }

        public static void Configure(System.Reflection.Assembly assembly, ContainerBuilder builder)
        {
            // регистрация всех классов отмеченыз атрибутом RegisterInIoc
            AssemblyBuilder dynamicAssemblyCheck = assembly as AssemblyBuilder;
            if (dynamicAssemblyCheck == null)
            {
                var types = assembly.GetTypes()
                        .Where(type => !type.IsAbstract)
                        .Where(type => !type.IsInterface)
                        .Where((Type type ) => {
                            // поиск всех типов с атрибутом RegisterInIocAttribute
                            //  у которого параметр Register == true
                            if (type.IsDefined(typeof(RegisterInIoCAttribute), true))
                            {
                                return Attribute.GetCustomAttributes(type)
                                    .Where((Attribute attrib) => {
                                        if (attrib.GetType() == typeof(RegisterInIoCAttribute))
                                        {
                                            var attribute = attrib as RegisterInIoCAttribute;
                                            if (attribute.Register)
                                                return true;
                                        }
                                        return false;
                                    })
                                    .Any();
                            }
                            return false;
                        })
                        //.Where(type => type.IsDefined(typeof(RegisterInIocAttribute), true) )
                        .ToArray();

                builder.RegisterTypes(types)
                    .PropertiesAutowired()
                    .AsSelf()
                    .AsImplementedInterfaces();
            }

            // регистрация контейнеров в параметрах контроллеров
            builder.RegisterControllers(assembly)
                .PropertiesAutowired();

            builder.RegisterType<Authorication>()
                .As<IAuthorication>()
                .SingleInstance();

        }

        private static IContainer _Container;
        public static IContainer Container { get { return _Container; } }

        public static IContainer Build(ContainerBuilder builder)
        {
            _Container = builder.Build();
            return _Container;
        }

    }
}