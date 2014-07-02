using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Linq.Dynamic;

namespace WorkTime.Utils.ExtJs
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> query, IEnumerable<ExtJsFilterInfo> filters)
        {
            if (filters == null)
                return query;

            foreach (var filter in filters)
            {
                if (!String.IsNullOrEmpty(filter.property) && !String.IsNullOrEmpty(filter.value))
                {
                    /*
                     * T = 
                     * {
                     *      Question = 
                     *      {
                     *          Name = "Test"
                     *      }
                     * };
                     * 
                     * filter.property = "Question.Name";
                     */

                    string[] properties = filter.property.Split('.');
                    PropertyInfo property = typeof(T).GetProperty(properties[0]);
                    for (int i = 1; i < properties.Length; i++)
                    {
                        string p = properties[i];
                        property = property.PropertyType.GetProperty(p);
                    }

                    string propertyType = property.PropertyType.ToString();

                    string expression;
                    switch (propertyType)
                    {
                        case "System.Int32":
                        case "System.Nullable`1[System.Int32]":
                        case "System.Int64":
                        case "System.Nullable`1[System.Int64]":
                        case "System.Byte":
                        case "System.Nullable`1[System.Byte]":
                            expression = String.Format("{0} == @0", filter.property);
                            int intValue = Int32.Parse(filter.value);
                            query = query.Where(expression, intValue);
                            break;

                        case "System.String":
                            expression = String.Format("{0}.ToUpper().Contains(@0)", filter.property);
                            query = query.Where(expression, filter.value.ToUpper());
                            break;

                        case "System.DateTime":
                        case "System.Nullable`1[System.DateTime]":
                            expression = String.Format("{0} {1} @0", filter.property, GetDateComparer(filter));
                            DateTime dateValue = DateTime.Parse(filter.value);
                            query = query.Where(expression, dateValue);
                            break;

                        default:
                            expression = String.Format("{0} == @0", filter.property);
                            query = query.Where(expression, filter.value);
                            break;
                    }


                }
            }
            return query;
        }

        private static string GetDateComparer(ExtJsFilterInfo filter)
        {
            switch (filter.comparison)
            {
                case "gt":
                    return ">=";
                case "lt":
                    return "<";
                case "rt":
                    return "<=";

                default:
                    return "==";
            }
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, IEnumerable<ExtJsSortInfo> sorters)
        {
            if (sorters == null)
                return query;

            string ordering = String.Join<ExtJsSortInfo>(", ", sorters);
            query = query.OrderBy(ordering);
            return query;
        }
    }
}