using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTime.Utils.Objects
{
    public static class ObjectExtension
    {

        public static bool HasOwnProperty(this object source, string propertyName)
        {
            return source.GetType().GetProperty(propertyName) != null;
        }

        public static bool HasOwnProperty<T>(this object source, string propertyName)
        {
            return (source.GetType().GetProperty(propertyName) != null) && (source.GetType().GetProperty(propertyName).PropertyType.IsAssignableFrom(typeof(T)));
        }

        public static T GetPropertyValue<T>(this object source, string propertyName)
        {
            return (T)source.GetType().GetProperty(propertyName).GetValue(source, null);
        }

        public static T GetPropertyValue2<T>(object source, string propertyName)
        {
            return (T)source.GetType().GetProperty(propertyName).GetValue(source, null);
        }

        public static object GetPropertyValue(this object source, string propertyName)
        {
            return source.GetType().GetProperty(propertyName).GetValue(source, null); ;
        }
    }
}