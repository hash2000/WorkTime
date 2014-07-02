using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTime.Utils.ExtJs
{
    [Serializable]
    public class ExtJsSortInfo
    {
        public string property { get; set; }
        public string direction { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}", property, direction);
        }
    }
}