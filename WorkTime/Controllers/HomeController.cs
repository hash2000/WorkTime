using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkTime.Utils.Security;

namespace WorkTime.Controllers
{
    public class HomeController : Controller
    {
        [DtoAction(DtoActionOperationType.SystemInternal)]
        public ActionResult Index()
        {
            return View();
        }
    }
}