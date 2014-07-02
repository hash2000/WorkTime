using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTime.Utils.ExtJs
{
    [Serializable]
    public class ExtJsFilterInfo
    {
        public string property { get; set; }
        public string field
        {
            get
            {
                return property;
            }
            set
            {
                property = value;
            }
        }
        public string value { get; set; }
        public string comparison { get; set; }
    }
}