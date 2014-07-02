using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkTime.Models;
using WorkTime.Utils.ExtJs;
using WorkTime.Utils.Security;
using WorkTime.Utils.Stores;

namespace WorkTime.Areas.Settings.Controllers
{

    public class VacationTypesController : DtoStore<VacationType, ContributorsEntities>
    {

        [DtoAction(DtoActionOperationType.Read)]
        public ActionResult GetVacationTypes(string sort, string filter, int start, int limit)
        {
            var sorters = GetSortInfo(sort);
            var filters = GetFilterInfo(filter);
            var vacationtypes = dbContext
                .VacationTypes
                .Select(n => new
                {
                    Id = n.Id,
                    Name = n.Name,
                    Label = n.Label,
                    FullName = "(" + n.Label + ") " + n.Name
                })
                .ToList()
                .AsQueryable();

            return vacationtypes.ToExtJs(sorters, filters, start, limit);
        }

    }
}