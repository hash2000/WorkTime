using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkTime.Models;
using WorkTime.Utils.ExtJs;
using WorkTime.Utils.Security;
using WorkTime.Utils.Stores;

namespace WorkTime.Areas.Settings.Controllers
{
    public class HolidaysController : DtoStore<Holiday, ContributorsEntities>
    {
        public ActionResult Index()
        {
            return View();
        }

        [DtoAction(DtoActionOperationType.Write)]
        public ActionResult ShiftHolidaysOnNextYear(int currentYear) 
        {
            dbContext.ShiftHolidayOnNextYear(currentYear); 
            return true.ToExtJs();
        }

    }
}