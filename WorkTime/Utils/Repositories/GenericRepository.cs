using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WorkTime.Utils.Repositories
{
    public class GenericRepository<T, TContext> : EntityRepository<T, TContext>
        where T : class
        where TContext : DbContext
    {
        public GenericRepository(DbContext context)
            : base(context)
        {
        }
    }

}