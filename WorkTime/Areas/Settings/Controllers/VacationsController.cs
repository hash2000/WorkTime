using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkTime.Models;
using WorkTime.Utils.Stores;
using WorkTime.Utils.ExtJs;
using WorkTime.Utils.Security;

namespace WorkTime.Areas.Settings.Controllers
{

    public class VacationsDto
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? EndDateWithHolidays { get; set; }
        public int VacationTypeId { get; set; }
        public string VacationTypeName { get; set; }

    }

    public class VacationsController : DtoStore<Vacation, ContributorsEntities>
    {
        //
        // GET: /Settings/Vacations/
        public ActionResult Index()
        {
            return View();
        }

        private IEnumerable<VacationsDto> GetList(int? id)
        {
            var handler = dbContext
                .Vacations
                .Include(n => n.Staff)
                .Include(n => n.VacationType);
            if (id.HasValue)
            {
                handler = handler
                    .Where(n => n.Id == id.Value);
            }
            var list = handler.ToArray();
            var result = list.Select(n => new VacationsDto
            {
                Id = n.Id,
                StaffId = n.StaffId,
                StaffName = n.Staff.Name + " " + n.Staff.PatronymicName + " " + n.Staff.Surname,
                StartDate = n.StartDate,
                EndDate = n.EndDate,
                EndDateWithHolidays = n.EndDateWithHolidays,
                VacationTypeId = n.VacationTypeId,
                VacationTypeName = "(" + n.VacationType.Label + ") " + n.VacationType.Name
            });

            return result;
        }


        [HttpPost]
        [DtoAction(DtoActionOperationType.Write)]
        public ActionResult AddVacations(Vacation item)
        {
            var result = dbContext
                .AddVacation(item.StartDate, item.EndDate, item.StaffId, item.VacationTypeId)
                .First();
            var vacationId = (int)result.VacationId.Value; //InternalCreate(item);
            return GetList(vacationId)
                .ToExtJs();
        }

        [HttpGet]
        [DtoAction(DtoActionOperationType.Read)]
        public ActionResult GetVacations(string sort, string filter, int start, int limit)
        {
            var sorters = GetSortInfo(sort);
            var filters = GetFilterInfo(filter);
            var vacations = GetList(null)
                .AsQueryable();
            return vacations.ToExtJs(sorters, filters, start, limit);
        }

        [HttpPut]
        [DtoAction(DtoActionOperationType.Write)]
        public ActionResult UpdateVacations(VacationsDto item)
        {
            dbContext
                .SetVacation(item.Id, item.StartDate, item.EndDate, item.StaffId, item.VacationTypeId);
            return GetList(item.Id)
                .ToExtJs();
        }

    }
}