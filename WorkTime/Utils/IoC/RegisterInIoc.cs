using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTime.Utils.IoC
{
    public class RegisterInIoCAttribute : Attribute
    {
        public RegisterInIoCAttribute() 
        {
            _Register = true;
        }

        public RegisterInIoCAttribute(bool register)
        {
            _Register = register;
        }

        private bool _Register;
        public bool Register {
            get { return _Register; }
            set { }
        }

    }
}