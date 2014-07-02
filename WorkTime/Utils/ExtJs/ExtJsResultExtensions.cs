using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTime.Utils.ExtJs
{
    public static class ExtJsResultExtensions
    {
        public static ExtJsResult ToExtJs<T>(this IQueryable<T> query, ExtJsSortInfo[] sort, ExtJsFilterInfo[] filter, int start, int limit)
        {
            query = query.Where(filter);
            query = query.OrderBy(sort);
            return new ExtJsResult()
            {
                total = query == null ? 0 : query.Count(),
                items = query == null ? new List<T>() : query.Skip(start).Take(limit).ToList(),
                success = true
            };
        }

        public static ExtJsResult ToExtJs<T>(this IEnumerable<T> items)
        {
            var result = items == null ? new List<T>() : items.ToList();
            return new ExtJsResult()
            {
                total = result.Count(),
                items = result,
                success = true
            };
        }

        public static ExtJsResult ToExtJs<T>(this IQueryable<T> items)
        {
            return ToExtJs<T>(items: items.AsEnumerable());
        }

        public static ExtJsResult ToExtJs<T>(this T item)
        {
            return new ExtJsResult()
            {
                items = new[] { item },
                total = 1,
                success = true
            };
        }   
    }

}