using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using WorkTime.Models;
using WorkTime.Utils.Security;

namespace WorkTime.Utils.MVC.Security
{
    public class Authorication : IAuthorication
    {
        ContributorsEntities dbContext;

        public Authorication(ContributorsEntities context)
        {
            dbContext = context;
        }

        public bool Allowed(RequestContext requestContext, Type controllerType)
        {
            var action = controllerType.GetMethod((string)requestContext.RouteData.Values["action"]);
            var hasattribute = action == null ? null : Attribute.GetCustomAttribute(action, typeof(DtoActionAttribute));

            // в случае если атрибут не указан, то считается что это SystemInternal,
            //  то есть разрешено системой. 
            var optypeVal = hasattribute != null ? (
                (DtoActionAttribute)Attribute.GetCustomAttribute(action, typeof(DtoActionAttribute))
                ).Value : DtoActionOperationType.SystemInternal;

            if (optypeVal == DtoActionOperationType.SystemInternal)
                return true;
            if (optypeVal == DtoActionOperationType.SpecialAccess)
                return false;
            // если пользователь зарегистрирован, то есть доступ
            //  в этой системе больше ничего проверять не нужно
            if (!requestContext.HttpContext.User.Identity.IsAuthenticated)
                return false;

            return true;
        }

        public bool IsAuthenticated()
        {
            return System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public bool Login(string username, string password)
        {
            bool authoricated = dbContext.Users
                    .Where(n => n.Name == username && n.Password == password)
                    .Any();

            if (authoricated)
            {
                FormsAuthentication.SetAuthCookie(username, true);
            }

            return authoricated;
        }

        public void Logoff()
        {
            FormsAuthentication.SignOut();
        }

    }
}