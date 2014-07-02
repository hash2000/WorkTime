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
    public class StaffsController : DtoStore<Staff, ContributorsEntities>
    {
        //
        // GET: /Settings/Staffs/
        public ActionResult Index()
        {
            return View();
        }

        [DtoAction(DtoActionOperationType.Read)]
        public ActionResult GetStaffs(string sort, string filter, int start, int limit)
        {
            var sorters = GetSortInfo(sort);
            var filters = GetFilterInfo(filter);

            var staffs = dbContext
                .Staffs
                .Include(n => n.Post)
                .Include(n => n.PostType)
                .Include(n => n.RegNorm)
                .Include(n => n.Gender)
                .Include(n => n.RegNorm)
                .Select(n => new
                {
                    Id = n.Id,
                    Surname = n.Surname,
                    Name = n.Name,
                    PatronymicName = n.PatronymicName,
                    TabelNumber = n.TabelNumber,
                    PostId = n.PostId,
                    PostName = n.Post.Name,
                    PostTypeId = n.PostTypeId,
                    PostTypeName = n.PostType.Name,
                    StartDate = n.StartDate,
                    EndDate = n.EndDate,
                    GenderId = n.GenderId,
                    GenderName = n.Gender.Name,
                    RegNormGraphTypeId = n.RegNormGraphTypeId,
                    RegNormGraphTypeName = n.RegNormGraphType.Name,
                    RegNormId = n.RegNormId,
                    RegNormName = n.RegNorm.Name,
                    FullName = n.Name + " " + n.PatronymicName + " " + n.Surname,
                })
                .ToList()
                .AsQueryable();

            return staffs.ToExtJs(sorters, filters, start, limit);
        }
    }
}