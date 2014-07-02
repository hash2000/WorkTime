using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkTime.Models;
using WorkTime.Utils.ExtJs;
using WorkTime.Utils.Security;
using WorkTime.Utils.Stores;
using WorkTime.Utils.Dto;



namespace WorkTime.Areas.Settings.Controllers
{

    public class TimenormDto
    {
        public int Id { get; set; }
        public double Norm { get; set; }
        public int RegNormsId { get; set; }
        public string RegNormsName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
    }

    public class TimeNormsOfMonthController : DtoStore<TimeNormOfMonth, ContributorsEntities>
    {
        private IEnumerable<TimenormDto> GetList(int? id)
        {
            var dtf = CultureInfo.GetCultureInfo(CultureInfo.CurrentCulture.Name).DateTimeFormat;
            var monthnames = dtf.MonthNames.ToList();

            var handler = dbContext
                .TimeNormOfMonths
                .Include(n => n.RegNorm);
            if (id.HasValue)
            {
                handler = handler
                    .Where(n => n.Id == id.Value);
            }

            var list = handler.ToArray();
            var result = list.Select(n => new TimenormDto
            {
                Id = n.Id,
                Norm = n.Norm,
                RegNormsId = n.RegNormsId,
                RegNormsName = n.RegNorm.Name,
                Year = n.Year,
                Month = n.Month,
                MonthName = monthnames[n.Month - 1]
            });

            return result;
        }

        [HttpPost]
        [DtoAction(DtoActionOperationType.Write)]
        public ActionResult AddTimeNormOfMonth(TimeNormOfMonth item)
        {
            var n = InternalCreate(item);
            return GetList(n.Id)
                .ToExtJs();
        }

        [HttpGet]
        [DtoAction(DtoActionOperationType.Read)]
        public ActionResult GetTimeNormOfMonth(string sort, string filter, int start, int limit)
        {
            var sorters = GetSortInfo(sort);
            var filters = GetFilterInfo(filter);
            var timenorms = GetList(null)
                .AsQueryable();
            return timenorms.ToExtJs(sorters, filters, start, limit);
        }

        [DtoAction(DtoActionOperationType.Write)]
        public ActionResult ShiftTimenormsOfNothOnNextYear(int currentYear)
        {
            dbContext.ShiftTimeNormOfMonthsOnNextYear(currentYear);
            return true.ToExtJs();
        }

    }
}

