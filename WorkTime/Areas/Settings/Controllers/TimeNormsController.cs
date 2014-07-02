using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkTime.Models;
using WorkTime.Utils.ExtJs;

namespace WorkTime.Areas.Settings.Controllers
{
    // контроллер вызывающий ExtJs приложение - настройка норм времени
    public class TimeNormsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
	}
}