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
    public class VacationTypeInPostTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public bool IsUsed { get; set; }
        
    }

    public class PostTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CoefficientId { get; set; }
        public string CoefficientName { get; set; }
    }


    public class PostTypesController : DtoStore<PostType, ContributorsEntities>
    {

        private IEnumerable<PostTypeDto> GetPostTypesList(int? id)
        {
            var handler = dbContext
                .PostTypes
                .Include(n => n.PostTypesDayTimeCoefficient);
            if (id.HasValue)
            {
                handler = handler
                    .Where(n => n.Id == id.Value);
            }

            var list = handler.ToArray();
            var result = list.Select(n => new PostTypeDto
            {
                Id = n.Id,
                Name = n.Name,
                CoefficientId = n.PostTypesDayTimeCoefficient.Id,
                CoefficientName = n.PostTypesDayTimeCoefficient.Name
            });

            return result;
        }


        [HttpPost]
        [DtoAction(DtoActionOperationType.Write)]
        public ActionResult AddPostType(PostTypeDto item)
        {
            var n = InternalCreate(new PostType
            {
                Id = item.Id,
                Name = item.Name, 
                PostTypesDayTimeCoefficientId = item.CoefficientId
            });
            return GetPostTypesList(n.Id)
                .ToExtJs();
        }

        [HttpPut]
        [DtoAction(DtoActionOperationType.Write)]
        public ActionResult UpdatePostType(PostTypeDto item)
        {
            var n = InternalUpdate(new PostType
            {
                Id = item.Id,
                Name = item.Name,
                PostTypesDayTimeCoefficientId = item.CoefficientId
            });
            return GetPostTypesList(n.Id)
                .ToExtJs();
        }


        [DtoAction(DtoActionOperationType.Read)]
        public ActionResult GetPostType(string sort, string filter, int start, int limit)
        {
            var sorters = GetSortInfo(sort);
            var filters = GetFilterInfo(filter);
            var posttypes = GetPostTypesList(null)
                .AsQueryable();
            return posttypes.ToExtJs(sorters, filters, start, limit);
        }



        [DtoAction(DtoActionOperationType.Read)]
        public ActionResult GetVacationTypesInPostTypes(string sort, string filter, int start, int limit, int postTypeId)
        {
            var sorters = GetSortInfo(sort);
            var filters = GetFilterInfo(filter);
            var vacations = dbContext
                .VacationTypes
                .Include(n => n.PostTypes)
                .Select(n => new VacationTypeInPostTypeDto
                {
                    Id = n.Id,
                    Name = n.Name,
                    Label = n.Label,
                    IsUsed = n.PostTypes.Where(p => p.Id == postTypeId).Any()
                })
                .ToList()
                .AsQueryable();

            return vacations.ToExtJs(sorters, filters, start, limit);
        }


        [DtoAction(DtoActionOperationType.Write)]
        public ActionResult UpdateVacationTypesInPostTypes(VacationTypeInPostTypeDto item, int postTypeId)
        {
            var vacation = dbContext.VacationTypes.Where(n => n.Id == item.Id).FirstOrDefault();
            var post = dbContext.PostTypes.Where(n => n.Id == postTypeId).FirstOrDefault();
            if (vacation == null || post == null)
                throw new Exception("vacation или post не найдены в базе");

            if (item.IsUsed)
            {
                // если создается новая связь .. то нужно найти экземпляры внутри контекста 
                //  и добавить их друг в друга
                vacation.PostTypes.Add(post);
                post.VacationTypes.Add(vacation);                
            }
            else
            {
                // если связь обозначается как false то ее нужно удалить из таблицы связей
                vacation.PostTypes.Remove(post);
                post.VacationTypes.Remove(vacation);
            }

            dbContext.SaveChanges();

            return true.ToExtJs();
        }

    }
}