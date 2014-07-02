using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTime.Utils.Security
{
    public enum DtoActionOperationType
    {
        Read = 0,
        Write = 1,
        SpecialAccess = 2,
        SystemInternal = 10000
    }

    public class DtoActionAttribute : Attribute
    {
        private DtoActionOperationType _value;
        public DtoActionAttribute(DtoActionOperationType accessType)
        {
            _value = accessType;
        }
        public DtoActionOperationType Value 
        {
            get
            {
                return _value;
            }
        }

    }
}