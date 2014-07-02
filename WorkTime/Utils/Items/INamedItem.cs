using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTime.Utils.Items
{
    public interface INamedItem : IItem
    {
        string Name { get; set; }
    }
}