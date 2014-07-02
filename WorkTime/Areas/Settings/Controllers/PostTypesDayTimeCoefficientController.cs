using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using WorkTime.Models;
using WorkTime.Utils.ExtJs;
using WorkTime.Utils.Security;
using WorkTime.Utils.Stores;


namespace WorkTime.Areas.Settings.Controllers
{
    public class PostTypesDayTimeCoefficientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double MaleCoefficient { get; set; }
        public double FemaleCoefficient { get; set; }
        public double MaleTimeNormOfDay { get; set; }
        public double FemaleTimeNormOfDay { get; set; }
    }

    public class PostTypesDayTimeCoefficientController : DtoStore<PostTypesDayTimeCoefficient, ContributorsEntities>
    {

        [DtoAction(DtoActionOperationType.Read)]
        public ActionResult GetPostTypesDayTimeCoefficient(string sort, string filter, int start, int limit)
        {
            var sorters = GetSortInfo(sort);
            var filters = GetFilterInfo(filter);

            var handler = dbContext
                .PostTypesDayTimeCoefficients
                .Include(n => n.PostTypesDayTimeCoefficientValues)
                .Select(n => new
                {
                    Id = n.Id,
                    Name = n.Name
                })
                .ToList();

            var values = new List<PostTypesDayTimeCoefficientDto>();
            foreach (var p in handler)
            {
                var Male = dbContext.PostTypesDayTimeCoefficientValues
                        .Where(n => n.GendersId == 2 && n.PostTypesDayTimeCoefficientId == p.Id)
                        .FirstOrDefault();
                var Female = dbContext.PostTypesDayTimeCoefficientValues
                        .Where(n => n.GendersId == 1 && n.PostTypesDayTimeCoefficientId == p.Id)
                        .FirstOrDefault();

                values.Add(new PostTypesDayTimeCoefficientDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    MaleCoefficient = Male == null ? 0 : Male.Coefficient,
                    FemaleCoefficient = Female == null ? 0 : Female.Coefficient,
                    MaleTimeNormOfDay = Male == null ? 0 : Male.TimeNormOfDay,
                    FemaleTimeNormOfDay = Female == null ? 0 : Female.TimeNormOfDay
                });
            }

            var models = values.AsQueryable();
            return models.ToExtJs(sorters, filters, start, limit);
        }

        [DtoAction(DtoActionOperationType.Write)]
        public ActionResult UpdatePostTypesDayTimeCoefficient(PostTypesDayTimeCoefficientDto item)
        {
            var dbitem = dbContext
                .PostTypesDayTimeCoefficients
                .Where(n => n.Id == item.Id)
                .FirstOrDefault();

            var pttc = dbContext
                .PostTypesDayTimeCoefficientValues
                .Where(n => n.PostTypesDayTimeCoefficientId == dbitem.Id);

            var mpttc = pttc
                .Where(n => n.GendersId == 2)
                .FirstOrDefault();
            var fpttc = pttc
                .Where(n => n.GendersId == 1)
                .FirstOrDefault();

            mpttc.Coefficient = item.MaleCoefficient;
            mpttc.TimeNormOfDay = item.MaleTimeNormOfDay;
            fpttc.Coefficient = item.FemaleCoefficient;
            fpttc.TimeNormOfDay = item.FemaleTimeNormOfDay;

            dbContext.SaveChanges();

            return item.ToExtJs();
        }
    }
}