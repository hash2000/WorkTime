using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WorkTime.Utils.EF
{
    public static class EfExtensions
    {
        /// <summary>
        /// Получить имя EntitySet
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        /// <param name="entityType">Тип сущности</param>
        /// <returns>Имя EntitySet</returns>
        public static string GetEntitySetName(this DbContext dbContext, Type entityType)
        {
            var context = (dbContext as IObjectContextAdapter).ObjectContext;
            var container = context.MetadataWorkspace.GetEntityContainer(context.DefaultContainerName, DataSpace.CSpace);
            string result = container.BaseEntitySets
                .Where(meta => meta.BuiltInTypeKind == BuiltInTypeKind.EntitySet)
                .Where(entitySet => entitySet.ElementType.Name == entityType.Name)
                .Select(entitySet => entitySet.Name)
                .FirstOrDefault();

            if (result == null)
                throw new InvalidOperationException("Can't get EntitySet name for " + entityType.Name);

            return result;
        }

        /// <summary>
        /// Получить такую же сущность из источника source, сравнивая по ключу
        /// </summary>
        /// <param name="source">Источник для поиска сущности</param>
        /// <param name="entityKey">Ключ EntityKey сущности</param>
        /// <returns></returns>
        public static T FindSameEntity<T>(this IQueryable<T> source, EntityKey entityKey)
            where T : class
        {
            Expression<Func<T, bool>> whereCondition = CreateSameEntitySearchCondition<T>(entityKey);
            return source.FirstOrDefault(whereCondition);
        }

        /// <summary>
        /// Получить такую же сущность из источника source, сравнивая по ключу
        /// </summary>
        /// <param name="source">Источник для поиска сущности</param>
        /// <param name="context">Контекст БД</param>
        /// <param name="entity">Сущность с таким же ключом из источника source</param>
        /// <returns></returns>
        public static T FindSameEntity<T>(this IQueryable<T> source, DbContext dbContext, T entity)
            where T : class
        {
            var entityKey = CreateEntityKey<T>(dbContext, entity);
            return FindSameEntity<T>(source, entityKey);
        }

        /// <summary>
        /// Получить условие выборки такой же сущности по ключу
        /// </summary>
        /// <param name="entityKey">Ключ EntityKey сущности</param>
        /// <returns>Условие выборки</returns>
        private static Expression<Func<T, bool>> CreateSameEntitySearchCondition<T>(EntityKey entityKey)
            where T : class
        {
            var keyMembers = entityKey.EntityKeyValues;

            var x = Expression.Parameter(typeof(T), "x");

            // Условия сравнения (x.Property.Equals(constant))
            var equalityExpressions = new List<Expression>();

            foreach (var km in keyMembers)
            {
                var p = Expression.Parameter(km.Value.GetType(), km.Key);

                var eq = Expression.Equal(
                    Expression.Property(x, km.Key),
                    Expression.Convert(Expression.Property(Expression.Constant(km), "Value"), km.Value.GetType()));

                equalityExpressions.Add(eq);
            }

            Expression<Func<T, bool>> result;
            if (equalityExpressions.Count == 1)
            {
                // Результат для случая с одним ключевым полем
                result = Expression.Lambda<Func<T, bool>>(
                    equalityExpressions.First(),
                    x);
            }
            else if (equalityExpressions.Count > 1)
            {
                // Случай с несколькими ключевыми полями
                // Условие "И" объединяющее все условия сравнения
                Expression andExpression = equalityExpressions.First();
                foreach (var eqExpression in equalityExpressions.Skip(1))
                {
                    andExpression = Expression.And(andExpression, eqExpression);
                }
                result = Expression.Lambda<Func<T, bool>>(andExpression, x);
            }
            else
            {
                throw new InvalidOperationException("Cannot create search condition for entity " + typeof(T).Name);
            }

            return result;
        }

        /// <summary>
        /// Создать ключ EntityKey для сущности entity
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        /// <param name="entity">Сущность</param>
        /// <returns>Ключ</returns>
        public static EntityKey CreateEntityKey<T>(this DbContext dbContext, T entity)
        {
            var context = (dbContext as IObjectContextAdapter).ObjectContext;
            string entitySetName = dbContext.GetEntitySetName(typeof(T));
            return context.CreateEntityKey(entitySetName, entity);
        }

        /// <summary>
        /// Получить подключенную к контексту сущность по ключу
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        /// <param name="entityKey">Ключ сущности</param>
        /// <returns>Подключенную сущность или null, если не найдена</returns>
        public static T GetAttachedEntity<T>(this DbContext dbContext, EntityKey entityKey)
            where T : class
        {
            var context = (dbContext as IObjectContextAdapter).ObjectContext;
            object result;
            if (context.TryGetObjectByKey(entityKey, out result))
            {
                return result as T;
            }
            return null;
        }

        /// <summary>
        /// Подключить сущность к контексту
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        /// <param name="entity">Сущность, которую необходимо подключить</param>
        /// <param name="force">Принудительно заменить, если существует с таким же ключом</param>
        public static void Attach<T>(this DbContext dbContext, T entity, bool force = false)
            where T : class
        {
            var dbSet = dbContext.Set<T>();
            ObjectContext context = (dbContext as IObjectContextAdapter).ObjectContext;
            if (force)
            {
                EntityKey entityKey = dbContext.CreateEntityKey<T>(entity);
                T attached = dbContext.GetAttachedEntity<T>(entityKey);
                if (attached == entity)
                    return;
                if (attached != null)
                    context.Detach(attached);
                dbSet.Attach(entity);
            }
            else
            {
                dbSet.Attach(entity);
            }
        }

    }

}