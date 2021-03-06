﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorkTime.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ContributorsEntities : DbContext
    {
        public ContributorsEntities()
            : base("name=ContributorsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostType> PostTypes { get; set; }
        public virtual DbSet<RegNorm> RegNorms { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<TimeNormOfMonth> TimeNormOfMonths { get; set; }
        public virtual DbSet<Vacation> Vacations { get; set; }
        public virtual DbSet<VacationType> VacationTypes { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<RegNormGraphType> RegNormGraphTypes { get; set; }
        public virtual DbSet<SysParameter> SysParameters { get; set; }
        public virtual DbSet<PostTypesDayTimeCoefficientValue> PostTypesDayTimeCoefficientValues { get; set; }
        public virtual DbSet<PostTypesDayTimeCoefficient> PostTypesDayTimeCoefficients { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual int ShiftHolidayOnNextYear(Nullable<int> currentYear)
        {
            var currentYearParameter = currentYear.HasValue ?
                new ObjectParameter("currentYear", currentYear) :
                new ObjectParameter("currentYear", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ShiftHolidayOnNextYear", currentYearParameter);
        }
    
        public virtual int ShiftTimeNormOfMonthsOnNextYear(Nullable<int> currentYear)
        {
            var currentYearParameter = currentYear.HasValue ?
                new ObjectParameter("currentYear", currentYear) :
                new ObjectParameter("currentYear", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ShiftTimeNormOfMonthsOnNextYear", currentYearParameter);
        }
    
        public virtual ObjectResult<GetStaffsSummary_Result> GetStaffsSummary()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetStaffsSummary_Result>("GetStaffsSummary");
        }
    
        public virtual ObjectResult<GetT13ReportData_Result> GetT13ReportData(Nullable<System.DateTime> periodFrom, Nullable<System.DateTime> periodTo)
        {
            var periodFromParameter = periodFrom.HasValue ?
                new ObjectParameter("PeriodFrom", periodFrom) :
                new ObjectParameter("PeriodFrom", typeof(System.DateTime));
    
            var periodToParameter = periodTo.HasValue ?
                new ObjectParameter("PeriodTo", periodTo) :
                new ObjectParameter("PeriodTo", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetT13ReportData_Result>("GetT13ReportData", periodFromParameter, periodToParameter);
        }
    
        public virtual ObjectResult<AddVacation_Result> AddVacation(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo, Nullable<int> staffId, Nullable<int> vacationTypeId)
        {
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("dateFrom", dateFrom) :
                new ObjectParameter("dateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("dateTo", dateTo) :
                new ObjectParameter("dateTo", typeof(System.DateTime));
    
            var staffIdParameter = staffId.HasValue ?
                new ObjectParameter("staffId", staffId) :
                new ObjectParameter("staffId", typeof(int));
    
            var vacationTypeIdParameter = vacationTypeId.HasValue ?
                new ObjectParameter("vacationTypeId", vacationTypeId) :
                new ObjectParameter("vacationTypeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AddVacation_Result>("AddVacation", dateFromParameter, dateToParameter, staffIdParameter, vacationTypeIdParameter);
        }
    
        public virtual int SetVacation(Nullable<int> vacationId, Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo, Nullable<int> staffId, Nullable<int> vacationTypeId)
        {
            var vacationIdParameter = vacationId.HasValue ?
                new ObjectParameter("VacationId", vacationId) :
                new ObjectParameter("VacationId", typeof(int));
    
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("dateFrom", dateFrom) :
                new ObjectParameter("dateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("dateTo", dateTo) :
                new ObjectParameter("dateTo", typeof(System.DateTime));
    
            var staffIdParameter = staffId.HasValue ?
                new ObjectParameter("staffId", staffId) :
                new ObjectParameter("staffId", typeof(int));
    
            var vacationTypeIdParameter = vacationTypeId.HasValue ?
                new ObjectParameter("vacationTypeId", vacationTypeId) :
                new ObjectParameter("vacationTypeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SetVacation", vacationIdParameter, dateFromParameter, dateToParameter, staffIdParameter, vacationTypeIdParameter);
        }
    
        public virtual ObjectResult<GetVacationsOnRange_Result> GetVacationsOnRange(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo)
        {
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("dateFrom", dateFrom) :
                new ObjectParameter("dateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("dateTo", dateTo) :
                new ObjectParameter("dateTo", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetVacationsOnRange_Result>("GetVacationsOnRange", dateFromParameter, dateToParameter);
        }
    
        public virtual ObjectResult<GetHolidaysInDate_Result> GetHolidaysInDate(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo)
        {
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("dateFrom", dateFrom) :
                new ObjectParameter("dateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("dateTo", dateTo) :
                new ObjectParameter("dateTo", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetHolidaysInDate_Result>("GetHolidaysInDate", dateFromParameter, dateToParameter);
        }
    }
}
