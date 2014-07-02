using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTime.Utils.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
            where T : class
    {
        public abstract T Add(T item);

        public abstract T Update(T item);

        public abstract void Delete(T item);

        public abstract IQueryable<T> Get();
        //public virtual IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        public virtual IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return Get().Where(whereCondition);
        }

        public virtual T GetSingle(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return Get().Where(whereCondition).Single();
        }

        public virtual void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual T Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}