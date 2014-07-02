using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;
using WorkTime.Utils.EF;

namespace WorkTime.Utils.Repositories
{
    /// <summary>
    /// Репозиторий, работающий с БД через Entity Framework
    /// </summary>
    /// <typeparam name="T">Тип модели</typeparam>
    /// <typeparam name="TContext">Тип контекста БД</typeparam>
    public abstract class EntityRepository<T, TContext> : BaseRepository<T>, IDisposable
        where T : class
        where TContext : DbContext
    {
        /// <summary>
        /// Контекст БД
        /// </summary>`
        protected TContext DbContext { get; set; }

        /// <summary>
        /// Набор данных типа Т
        /// </summary>
        protected DbSet<T> DbSet { get; set; }

        public void Dispose()
        {
            if (DbContext != null)
                DbContext.Dispose();
        }

        public EntityRepository(DbContext context)
        {
            DbContext = context as TContext;
            DbSet = DbContext.Set<T>();
        }

        public override T Add(T item)
        {
            DbSet.Add(item);
            DbContext.SaveChanges();
            T result = this.Get().FindSameEntity(DbContext, item);
            return result;
        }

        public override T Update(T item)
        {
            var entry = DbContext.Entry(item);
            if (entry.State == EntityState.Detached)
            {
                var original = DbSet.FindSameEntity(DbContext, item);
                entry = DbContext.Entry(original);
            }
            entry.CurrentValues.SetValues(item);
            DbContext.SaveChanges();
            T result = Get().FindSameEntity(DbContext, item);
            return result;
        }

        public override void Delete(T item)
        {
            DbContext.Attach(item, true);
            DbSet.Remove(item);
            DbContext.SaveChanges();
        }

        public override void Delete(int id)
        {
            var item = Activator.CreateInstance<T>();
            (item as dynamic).Id = id;
            Delete(item);
        }

        public override IQueryable<T> Get()
        {
            return DefaultInclude(DbSet);
        }

        public override T Find(int id)
        {
            return Get().Where("it.Id == @0", new object[] { id }).FirstOrDefault();
        }

        /// <summary>
        /// Добавить включения по умолчанию для конкретного репозитория
        /// </summary>
        /// <param name="dbSet">Набор данных</param>
        /// <returns>Набор данных с включениями по умолчанию</returns>
        protected virtual IQueryable<T> DefaultInclude(DbSet<T> dbSet)
        {
            return dbSet;
        }
    }

}