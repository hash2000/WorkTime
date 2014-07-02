using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using WorkTime.Utils.ExtJs;
using WorkTime.Utils.EF;
using WorkTime.Utils.Dto;

namespace WorkTime.Utils.Stores
{
    public class DtoTreeStore<TEntity, TContext> : BaseStore<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        protected virtual ExtBaseNode<TEntity> NodeIterationHandler(TEntity item, ExtBaseNode<TEntity> node)
        {
            return node;
        }


        public string SortInfo = "[{\"property\":\"Id\",\"direction\":\"ASC\"}]";

        [HttpGet]
        //[Action(OperationType.Read)]
        public ActionResult Get(string node)
        {
            int? parentId = (node == String.Empty) || (node == "0") ? default(int?) : Convert.ToInt32(node);
            var nodes = Repository
                .Get()
                .Where(
                    parentId == null ? "ParentId == NULL" : "ParentId == @0", parentId
                )
                .OrderBy(GetSortInfo(SortInfo))
                .ToList()
                .Select(x => { 
                    return NodeIterationHandler(x, ExtBaseNode<TEntity>.EntityToNode(x)); 
                });
            return
                ExtJs(nodes);
        }

        [HttpPost]
        //[Action(OperationType.Write)]
        public ActionResult Add(TEntity item)
        {
            return InternalCreate(item).Dto().ToExtJs();
        }

        [HttpPut]
        //[Action(OperationType.Write)]
        public ActionResult Update(TEntity item)
        {
            return InternalUpdate(item).Dto().ToExtJs();
        }

        [HttpDelete]
        //[Action(OperationType.Write)]
        public ActionResult Delete(TEntity item)
        {
            InternalDelete(item);
            return true.ToExtJs();
        }
    }
}