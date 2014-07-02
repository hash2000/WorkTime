using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace WorkTime.Utils.MVC.Security
{
    public interface IAuthorication
    {
        bool Allowed(RequestContext requestContext, Type controllerType);
        bool IsAuthenticated();
        bool Login(string username, string password);
        void Logoff();
    }
}