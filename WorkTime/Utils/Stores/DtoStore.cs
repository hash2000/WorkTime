using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkTime.Utils.ExtJs;
using WorkTime.Utils.Dto;
using WorkTime.Utils.Security;

namespace WorkTime.Utils.Stores
{
    public class DtoStore<TEntity, TContext> : BaseStore<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        /// <summary>
        /// вернуть набор 
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="filter"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [DtoAction(DtoActionOperationType.Read)]
        public virtual ActionResult Get(string sort, string filter, int start, int limit)
        {
            var result = InternalRead(sort, filter, start, limit);
            return new ExtJsResult()
            {
                total = result == null ? 0 : result.Count(),
                items = result == null ? new List<object>() : result.Select(x => x.Dto()).ToList(),
                success = true
            };
        }

        /// <summary>
        /// добавить элемент
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [DtoAction(DtoActionOperationType.Write)]
        public virtual ActionResult Add(TEntity item)
        {
            return InternalCreate(item).Dto().ToExtJs();
        }
        
        /// <summary>
        /// изменить элемент набора
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        [DtoAction(DtoActionOperationType.Write)]
        public virtual ActionResult Update(TEntity item)
        {
            return InternalUpdate(item).Dto().ToExtJs();
        }

        /// <summary>
        /// удалить элемент из набора
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpDelete]
        [DtoAction(DtoActionOperationType.Write)]
        public virtual ActionResult Delete(TEntity item)
        {
            InternalDelete(item);
            return true.ToExtJs();
        }
    }
}