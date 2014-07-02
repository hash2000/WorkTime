using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using WorkTime.Models;
using WorkTime.Utils.ExtJs;
using System.Web.Security;
using System.Security.Principal;
using WorkTime.Utils.MVC.Security;


namespace WorkTime.Controllers
{
    public class AccountController : Controller
    {
        private IAuthorication Authorication { get; set; }

        public AccountController(IAuthorication authorication)
        {
            Authorication = authorication;
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            return Authorication.Login(username, password).ToExtJs();
        }

        public ActionResult Logoff()
        {
            Authorication.Logoff();
            return true.ToExtJs();
        }

        public ActionResult IsAuthenticated()
        {
            return Authorication.IsAuthenticated().ToExtJs();
        }

    }
}