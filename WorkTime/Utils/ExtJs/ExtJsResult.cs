using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkTime.Utils.ExtJs
{
    /// <summary>
    /// Типизированный ответ для ExtJs
    /// </summary>
    /// <remarks>
    /// В ExtJs reader для store должен обязательно быть сконфигурирован с параметром 'root': 'items'.
    /// </remarks>        
    public class ExtJsResult<T> : JsonResult
        where T : class
    {
        public IEnumerable<object> items { get; set; }
        public int total { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public string messageText { get; set; }


        public override void ExecuteResult(ControllerContext context)
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = new[] { new IsoDateTimeConverter() }
            });
            context.HttpContext.Response.Write(json);
        }


        public ExtJsResult()
        {

        }

        public ExtJsResult(T item, bool success)
        {
            this.items = new[] { item };
            this.success = success;
        }

        public ExtJsResult(T item)
            : this(item, true)
        {

        }

        public ExtJsResult(IQueryable<T> query, ExtJsSortInfo[] sorters, int start, int limit)
        {
            query = query.OrderBy(sorters);

            this.total = query == null ? 0 : query.Count();
            this.items = query == null ? new List<T>() : query.Skip(start).Take(limit).ToList();
            this.success = true;
        }

        public ExtJsResult(IQueryable<T> query, ExtJsSortInfo[] sorters, ExtJsFilterInfo[] filters, int start, int limit)
        {
            query = query.Where(filters);
            query = query.OrderBy(sorters);

            this.total = query == null ? 0 : query.Count();
            this.items = query == null ? new List<T>() : query.Skip(start).Take(limit).ToList();
            this.success = true;
        }

        public ExtJsResult(IEnumerable<T> items)
        {
            var result = items == null ? new List<T>() : items.ToList();
            this.total = result.Count();
            this.items = result.ToList();
            this.success = true;
        }

        public ExtJsResult(IQueryable<T> query)
            : this(items: query.AsEnumerable())
        {

        }

    }
    
    /// <summary>
    /// Ответ для ExtJs
    /// </summary>
    /// <remarks>
    /// В ExtJs reader должен обязательно быть сконфигурирован с параметром 'root': 'items'.
    /// </remarks>
    public class ExtJsResult : ActionResult
    {
        public IEnumerable items { get; set; }
        public int total { get; set; }
        public bool success { get; set; }
        public string messages { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var sertializer = Utils.Json.CreateSerializer();
            sertializer.Serialize(context.HttpContext.Response.Output, this);
        }
    }

}