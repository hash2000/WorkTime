using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WorkTime.Utils.IoC;
using WorkTime.Utils.MVC.Security;

namespace WorkTime.Utils.MVC.Controllers
{
    public class ProtectedControllerFactory : DefaultControllerFactory
    {
        private IAuthorication Authorication;
        public ProtectedControllerFactory()
        {
            Authorication = AutofacConfigurator.Container.Resolve<IAuthorication>();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (Authorication.Allowed(requestContext, controllerType))
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }

            throw new Exception("Нет доступа");
        }
    }
}