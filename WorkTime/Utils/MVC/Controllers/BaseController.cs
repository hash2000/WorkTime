using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WorkTime.Utils.ExtJs;
using WorkTime.Utils.MVC.Messsages;

namespace WorkTime.Utils.MVC.Controllers
{
    /// <summary>
    /// Базовый контроллер
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// Должен ли вызов метода быть записан в лог
        /// </summary>
        private bool ShouldBeLogged(ActionDescriptor actionDescriptor)
        {
            return true; // actionDescriptor.GetCustomAttributes(typeof(LogAttribute), true).Any();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (ShouldBeLogged(filterContext.ActionDescriptor))
            {
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string actionName = filterContext.ActionDescriptor.ActionName;
                //!!!!!!!!!! найти Area тоже
                //и записать в лог
            }
            base.OnActionExecuting(filterContext);
        }

        // Получить пользовательский текст ошибки, записать в лог и выдать в результат
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = new JsonResult()
            {
                Data = new ExtJsResult<object>()
                {
                    success = false,
                    message = ErrorMessageHelper.GetMessageText(filterContext.Exception),
                    messageText = filterContext.Exception.ToString()
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            filterContext.ExceptionHandled = true;
            base.OnException(filterContext);
        }

        public ExtJsSortInfo[] GetSortInfo(string sort)
        {
            return sort == null ? null : new JavaScriptSerializer().Deserialize<ExtJsSortInfo[]>(sort);
        }

        public ExtJsFilterInfo[] GetFilterInfo(string filter)
        {
            return filter == null ? null : new JavaScriptSerializer().Deserialize<ExtJsFilterInfo[]>(filter);
        }

        public ActionResult ExtJs(bool success)
        {
            return new ExtJsResult<object>() { success = success };
        }

        public ActionResult ExtJs(bool success, JsonRequestBehavior behavior)
        {
            var result = ExtJs(success) as JsonResult;
            result.JsonRequestBehavior = behavior;
            return result;
        }

        public ActionResult ExtJs<T>(T item, bool success)
            where T : class
        {
            var result = new ExtJsResult<T>()
            {
                total = 1,
                items = new[] { item },
                success = success
            };
            return result;
        }

        public ActionResult ExtJs<T>(T item)
            where T : class
        {
            return ExtJs(item, true);
        }

        public ActionResult ExtJs<T>(IQueryable<T> query, ExtJsSortInfo[] sorters, int start, int limit)
            where T : class
        {
            query = query.OrderBy(sorters);

            var result = new ExtJsResult<T>()
            {
                total = query == null ? 0 : query.Count(),
                items = query == null ? new List<T>() : query.Skip(start).Take(limit).ToList(),
                success = true
            };
            return result;
        }



        public ActionResult ExtJs<T>(IQueryable<T> query, ExtJsFilterInfo[] filters, int start, int limit)
            where T : class
        {
            query = query.Where(filters);

            var result = new ExtJsResult<T>()
            {
                total = query == null ? 0 : query.Count(),
                items = query == null ? new List<T>() : query.Skip(start).Take(limit).ToList(),
                success = true
            };
            return result;
        }


        public ActionResult ExtJs<T>(IQueryable<T> query, ExtJsSortInfo[] sorters, ExtJsFilterInfo[] filters, int start, int limit)
            where T : class
        {
            query = query.Where(filters);
            query = query.OrderBy(sorters);

            var result = new ExtJsResult<T>()
            {
                total = query == null ? 0 : query.Count(),
                items = query == null ? new List<T>() : query.Skip(start).Take(limit).ToList(),
                success = true
            };
            return result;
        }

        public ActionResult ExtJs<T>(IQueryable<T> query, ExtJsSortInfo[] sorters, int start, int limit, JsonRequestBehavior behavior)
            where T : class
        {
            var result = ExtJs(query, sorters, start, limit) as JsonResult;
            result.JsonRequestBehavior = behavior;
            return result;
        }

        public ActionResult ExtJs<T>(IQueryable<T> query, ExtJsFilterInfo[] filters, int start, int limit, JsonRequestBehavior behavior)
            where T : class
        {
            var result = ExtJs(query, filters, start, limit) as JsonResult;
            result.JsonRequestBehavior = behavior;
            return result;
        }


        public ActionResult ExtJs<T>(IQueryable<T> query, ExtJsSortInfo[] sorters, ExtJsFilterInfo[] filters, int start, int limit, JsonRequestBehavior behavior)
            where T : class
        {
            var result = ExtJs(query, sorters, filters, start, limit) as JsonResult;
            result.JsonRequestBehavior = behavior;
            return result;
        }

        public ActionResult ExtJs<T>(IQueryable<T> query, string sort, int start, int limit)
            where T : class
        {
            return ExtJs(query, GetSortInfo(sort), start, limit);
        }

        public ActionResult ExtJs<T>(IQueryable<T> query, string sort, int start, int limit, JsonRequestBehavior behavior)
            where T : class
        {
            return ExtJs(query, GetSortInfo(sort), start, limit, behavior);
        }

        public ActionResult ExtJs<T>(IQueryable<T> query)
            where T : class
        {
            var result = new ExtJsResult<T>()
            {
                total = query == null ? 0 : query.Count(),
                items = query == null ? new List<T>() : query.ToList(),
                success = true
            };
            return result;
        }

        public ActionResult ExtJs<T>(IQueryable<T> query, JsonRequestBehavior behavior)
            where T : class
        {
            var result = ExtJs(query) as JsonResult;
            result.JsonRequestBehavior = behavior;
            return result;
        }

        public ActionResult ExtJs<T>(IEnumerable<T> items)
            where T : class
        {
            var result = new ExtJsResult<T>()
            {
                total = items == null ? 0 : items.Count(),
                items = items == null ? new List<T>() : items.ToList(),
                success = true
            };
            return result;
        }

        public ActionResult ExtJs<T>(IEnumerable<T> items, JsonRequestBehavior behavior)
            where T : class
        {
            var result = ExtJs(items) as JsonResult;
            result.JsonRequestBehavior = behavior;
            return result;
        }

        public ActionResult ExtJsNull(JsonRequestBehavior behavior)
        {
            var result = new JsonResult();
            result.Data = null;
            result.JsonRequestBehavior = behavior;
            return result;
        }

        public ActionResult ExtJsNull()
        {
            return ExtJsNull();
        }
    }
}