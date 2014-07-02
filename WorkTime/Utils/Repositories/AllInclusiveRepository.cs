using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WorkTime.Utils.Repositories
{
    public class AllInclusiveRepository<T, TContext> : GenericRepository<T, TContext>
        where T : class
        where TContext : DbContext
    {

        public AllInclusiveRepository(DbContext context)
            : base(context)
        {

        }
        /// <summary>
        /// включения для конкретного репозитория
        /// </summary>
        /// <param name="dbSet">Набор данных</param>
        /// <returns>Набор данных с включениями</returns>
        protected override IQueryable<T> DefaultInclude(DbSet<T> dbSet)
        {
            var query = dbSet.AsQueryable();
            var includes = typeof(T)
                .GetProperties()
                .Where(p => (
                        (!p.PropertyType.IsSealed) &&
                        (typeof(System.Collections.IEnumerable).IsAssignableFrom(p.PropertyType))
                    )
                )
                .Select(p => p)
                .ToList();

            foreach (var include in includes)
            {
                query = query
                    .Include(include.Name);
            }
            return query;
        }
    }
}