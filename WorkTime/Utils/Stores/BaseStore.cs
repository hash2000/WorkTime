using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkTime.Utils.Dto;
using WorkTime.Utils.MVC.Controllers;
using WorkTime.Utils.Repositories;
using WorkTime.Utils.ExtJs;
using System.ComponentModel.DataAnnotations;
using WorkTime.Utils.IoC;

namespace WorkTime.Utils.Stores
{

    [RegisterInIoC]
    public class BaseStore<TEntity, TContext> : BaseController
        where TEntity : class
        where TContext : DbContext
    {
        #region Хелперы

        public string AreaName
        {
            get
            {
                string areaName = this
                    .GetType()
                    .Namespace.Contains(".Areas.") ? this.GetType().Namespace.Replace(typeof(MvcApplication).Namespace + ".Areas.", "") : String.Empty;
                if (areaName.Contains("."))
                {
                    areaName = areaName
                        .Remove(areaName.IndexOf('.'));
                }
                return areaName;
            }
        }

        public string StoreName
        {
            get
            {
                var name = this.GetType().Name;
                return name
                    .EndsWith("Controller") ? name.Remove(name.IndexOf("Controller")) : name;
            }
        }
        #endregion



        public TContext dbContext { get; set; }

        protected AllInclusiveRepository<TEntity, TContext> repository;
        protected AllInclusiveRepository<TEntity, TContext> Repository
        {
            get
            {
                return repository ?? (repository = new AllInclusiveRepository<TEntity, TContext>(dbContext));
            }
            set
            {
                repository = value;
            }
        }
        private Type DToType { get; set; }
        private int? CallerMenuItemId
        {
            get
            {
                string strid = this.Request.Url.Segments.Last();
                int i;
                return int.TryParse(strid, out i) ? i : (int?)null;
            }
        }


        public BaseStore()
        {
            this.DToType = DToConstructor.Dto<TEntity>();

            if (AutoMapper.Mapper.FindTypeMapFor(typeof(TEntity), this.DToType) == null)
            {
                AutoMapper.Mapper.CreateMap(typeof(TEntity), this.DToType);
            }

        }

        protected virtual IQueryable<TEntity> InternalRead(string sort, string filter, int start, int limit)
        {
            var sorters = GetSortInfo(sort);
            var filters = GetFilterInfo(filter);
            return Repository
                .Get()
                .Where(filters)
                .ToList()
                .AsQueryable()
                .OrderBy(sorters)
                .Skip(start)
                .Take(limit);
        }

        protected virtual TEntity InternalCreate(TEntity item)
        {
            return Repository.Add(item);
        }

        protected virtual TEntity InternalUpdate(TEntity item)
        {
            return Repository.Update(item);
        }


        protected void InternalDelete(TEntity item)
        {
            Repository.Delete(item);
        }

        //[Action(OperationType.SystemInternal)]
        /// <summary>
        /// Хелпер генерирования Ext моделей по классу ДТО
        /// </summary>

        static string CRLF = "" + (char)0x0D + (char)0x0A;
        public ActionResult model()
        {

            var define = String.Concat("Ext.define(", String.Format("'{0}.model.{1}'", this.AreaName, typeof(TEntity).Name), ", ", CRLF, " {");
            var extend = "extend: 'Ext.data.Model'," + CRLF;
            var fields = "  fields: [" + CRLF;

            foreach (var prop in this.DToType.GetProperties().Reverse())
            {
                switch (prop.PropertyType.ToString())
                {
                    case "System.Int16":
                    case "System.UInt16":
                    case "System.Nullable`1[System.Int16]":
                    case "System.Nullable`1[System.UInt16]":
                    case "System.Int32":
                    case "System.UInt32":
                    case "System.Nullable`1[System.Int32]":
                    case "System.Nullable`1[System.UInt32]":
                    case "System.Int64":
                    case "System.UInt64":
                    case "System.Nullable`1[System.Int64]":
                    case "System.Nullable`1[System.UInt64]":
                    case "System.Byte":
                    case "System.SByte":
                    case "System.Nullable`1[System.Byte]":
                    case "System.Nullable`1[System.SByte]":
                        fields += "        { name:'" + prop.Name + "', type:'int' }," + CRLF;
                        break;

                    case "System.Char":
                    case "System.String":
                        fields += "        { name:'" + prop.Name + "', type:'string' }," + CRLF;
                        break;

                    case "System.Single":
                    case "System.Nullable`1[System.Single]":
                    case "System.Double":
                    case "System.Nullable`1[System.Double]":
                    case "System.Decimal":
                    case "System.Nullable`1[System.Decimal]":
                        fields += "        { name:'" + prop.Name + "', type:'float' }," + CRLF;
                        break;

                    case "System.DateTime":
                    case "System.Nullable`1[System.DateTime]":
                        fields += "        { name:'" + prop.Name + "', type:'date', dateFormat: 'c' }," + CRLF;
                        break;

                    case "System.Boolean":
                    case "System.Nullable`1[System.Boolean]":
                        fields += "        { name:'" + prop.Name + "', type:'boolean' }," + CRLF;
                        break;

                    default:
                        fields += "        { name:'" + prop.Name + "' }," + CRLF;
                        break;
                }
            }

            fields += "          ]," + CRLF;
            fields = fields.Replace("," + CRLF + "          ]", CRLF + "         ]");
            var k = typeof(TEntity).GetProperties().Where(p => p.IsDefined(typeof(KeyAttribute), true));
            var idproperty = "idProperty: '" + (k.FirstOrDefault() != null ? k.FirstOrDefault().Name : "Id") + "'," + CRLF;
            var proxy = this.getproxy();
            var model = String.Concat(define, extend, fields, idproperty, proxy);

            return Content(model);
        }

        protected virtual string getproxy()
        {
            var p =
              "proxy: { " + CRLF
             + "   type : 'rest'," + CRLF
             + "         api : {" + CRLF
             + "                 read    : '" + (this.AreaName == String.Empty ? this.StoreName + "/Get'," : this.AreaName + "/" + this.StoreName + "/Get',") + CRLF
             + "                 create  : '" + (this.AreaName == String.Empty ? this.StoreName + "/Add'," : this.AreaName + "/" + this.StoreName + "/Add',") + CRLF
             + "                 update  : '" + (this.AreaName == String.Empty ? this.StoreName + "/Update'," : this.AreaName + "/" + this.StoreName + "/Update',") + CRLF
             + "                 destroy : '" + (this.AreaName == String.Empty ? this.StoreName + "/Delete'" : this.AreaName + "/" + this.StoreName + "/Delete'") + CRLF
             + "               }," + CRLF
             + "                 paramsAsHash : true," + CRLF
             + "                 reader : 'DefaultReader'," + CRLF
             + "                 writer : 'DefaultWriter'" + CRLF
             + " }" + CRLF + "});";
            return p;
        }
    }


}