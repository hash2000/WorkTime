using Autofac;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WorkTime.Models;
using WorkTime.Utils;
using WorkTime.Utils.Reporting;
using WorkTime.Utils.Security;


namespace WorkTime.Controllers
{

    // данные об отрабонанных днях за месяц
    public class StaffWorkTimeOfMonthSectionDto
    {
        public string c1 { get; set; }
        public string c2 { get; set; }
        public string c3 { get; set; }
        public string c4 { get; set; }
        public string c5 { get; set; }
        public string c6 { get; set; }
        public string c7 { get; set; }
        public string c8 { get; set; }
        public string c9 { get; set; }
        public string c10 { get; set; }
        public string c11 { get; set; }
        public string c12 { get; set; }
        public string c13 { get; set; }
        public string c14 { get; set; }
        public string c15 { get; set; }
        public string c16 { get; set; }
        public string c17 { get; set; }
        public string c18 { get; set; }
        public string c19 { get; set; }
        public string c20 { get; set; }
        public string c21 { get; set; }
        public string c22 { get; set; }
        public string c23 { get; set; }
        public string c24 { get; set; }
        public string c25 { get; set; }
        public string c26 { get; set; }
        public string c27 { get; set; }
        public string c28 { get; set; }
        public string c29 { get; set; }
        public string c30 { get; set; }
        public string c31 { get; set; }

        // количество отработанных часов и количество за первую и вторую половину месяца
        public double dayshours1 { get; set; }
        public double dayshours2 { get; set; }

        public StaffWorkTimeOfMonthSectionDto()
        {
            dayshours1 = dayshours2 = 0;
        }

    }

    public class T13ReportController : Controller
    {
        public ContributorsEntities dbContext { get; set; }

        private DateTime DateFrom, DateTo;
        private IList<GetVacationsOnRange_Result> Vacations;
        private IList<VacationType> VacationTypes;
        private VacationType DesignationOfWorkDayType; // обозначение на рабочий день
        private VacationType HolidayType; // обозначение выходного дня
        private IList<PostTypesDayTimeCoefficientValue> PostTypesDayTimeCoefficientValues;
        private IList<PostTypesDayTimeCoefficient> PostTypesDayTimeCoefficients;
        private IList<GetHolidaysInDate_Result> HolidaysInDateRange;

        public T13ReportController(ContributorsEntities context)
        {
            // получение контекста EF из Autofac
            dbContext = context;
        }

        [DtoAction(DtoActionOperationType.Read)]
        public ReportResult Generate(DateTime dateFrom, DateTime dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;

            if (DateFrom.Month != DateTo.Month && DateFrom.Year != DateTo.Year)
                throw new Exception("Выбранный период должен охватывать только один месяц");

            Vacations = dbContext.GetVacationsOnRange(DateFrom, DateTo).ToList();
            VacationTypes = dbContext.VacationTypes.Select(n => n).ToList();
            // поиск обозначение для рабочего дня
            //  хранится в системных настройках под именем 'DesignationOfWorkDay'
            var DesignationOfWorkDayTypeDb = dbContext.SysParameters.Where(n => n.Name == "DesignationOfWorkDay").FirstOrDefault();
            if (DesignationOfWorkDayTypeDb != null)
            {
                DesignationOfWorkDayType = VacationTypes.Where(n => n.Id == int.Parse(DesignationOfWorkDayTypeDb.Value)).FirstOrDefault();
            }

            var HolidayTypeDb = dbContext.SysParameters.Where(n => n.Name == "Holiday").FirstOrDefault();
            if (HolidayTypeDb != null)
            {
                HolidayType = VacationTypes.Where(n => n.Id == int.Parse(HolidayTypeDb.Value)).FirstOrDefault();
            }

            PostTypesDayTimeCoefficients = dbContext
                .PostTypesDayTimeCoefficients
                .Select(n => n)
                .ToList();

            PostTypesDayTimeCoefficientValues = dbContext
                .PostTypesDayTimeCoefficientValues
                .Select(n => n)
                .ToList();

            // все праздники на выбранный диапазон дат 
            HolidaysInDateRange = dbContext
                .GetHolidaysInDate(DateFrom, DateTo)
                .ToList();


            var reportheadermodel = dbContext.GetT13ReportData(DateFrom, dateTo);
            var staffssummarymodel = dbContext.GetStaffsSummary();
            return new ReportResult(ReportFormat.Excel, "T13Report"
                , Server.MapPath("~/Reports/T13/T13Report.rdlc")
                , new[] {
                    new ReportDataSource ("T13ReportHeader", reportheadermodel ) ,
                    new ReportDataSource ("StaffsSummary", staffssummarymodel )
                }
                , new[] {
                    new SubreportProcessingEventHandler(this.GenerateStaffsSubreport)
                }
            );
        }

        public void GenerateStaffsSubreport(object sender, SubreportProcessingEventArgs e)
        {
            var staffId = int.Parse(e.Parameters["StaffId"].Values[0]);
            // выбор всех отпусков у сотрудника которые попадают в указанный диапазон
            var vacations = Vacations.Where(n => n.StaffId == staffId);

            var model = new List<StaffWorkTimeOfMonthSectionDto>();
            var el1 = new StaffWorkTimeOfMonthSectionDto();
            var el2 = new StaffWorkTimeOfMonthSectionDto();

            var daysinmonth = DateTime.DaysInMonth(DateFrom.Year, DateFrom.Month);

            var staff = dbContext.Staffs.Where(n => n.Id == staffId).First();
            var regNormDayCount = staff.RegNormGraphType.DayCount;
            var posttype = dbContext.PostTypes.Where(n => n.Id == staff.PostTypeId).First();
            var coefficient = PostTypesDayTimeCoefficients.Where(n => n.Id == posttype.PostTypesDayTimeCoefficientId).First();
            var coefficientvalue = PostTypesDayTimeCoefficientValues.Where(n => n.GendersId == staff.GenderId && n.PostTypesDayTimeCoefficientId == coefficient.Id).First();

            for (var i = 1; i <= 31; i++)
            {
                var el1prop = el1.GetType().GetProperty(string.Format("c{0}", i));
                var el2prop = el2.GetType().GetProperty(string.Format("c{0}", i));

                if (i > DateTo.Day)
                {
                    el1prop.SetValue(el1, "X");
                    el2prop.SetValue(el2, "X");
                    continue;
                }

                var date = new DateTime(DateFrom.Year, DateFrom.Month, i);
                var nextdate = date.AddDays(1);
                var vacation = vacations
                    .Where(n => n.StartDate <= date && date <= n.EndDateWithHolidays)
                    .FirstOrDefault();

                int TimeNormOfDayIncr = 0;

                var IsNextDayAreHoliday = HolidaysInDateRange.Where(n =>
                        n.StartDate <= nextdate && nextdate <= (n.EndDate.HasValue ? n.EndDate.Value : n.StartDate) &&
                        n.IsNormsCheck
                        )
                    .Any();
                if (IsNextDayAreHoliday)
                {
                    // если следующий день выходной
                    TimeNormOfDayIncr = -1;
                }


                if (vacation != null)
                {
                    var vacationType = VacationTypes.Where(n => n.Id == vacation.VacationTypeId).First();
                    el1prop.SetValue(el1, vacationType.Label);
                    el2prop.SetValue(el2, String.Empty);
                }
                else
                {

                    // суббота для пятидневки и воскресенье
                    //  обозначение выходного дня
                    if ((date.DayOfWeek == DayOfWeek.Saturday && regNormDayCount == 5) ||
                        (date.DayOfWeek == DayOfWeek.Sunday))
                    {
                        if (HolidayType != null)
                        {
                            el1prop.SetValue(el1, HolidayType.Label);
                            el2prop.SetValue(el2, String.Empty);
                        }
                    }
                    else
                    {
                        // обозначение рабочего дня
                        if (DesignationOfWorkDayType != null)
                        {

                            var coeffWithInbcr = coefficientvalue.TimeNormOfDay + TimeNormOfDayIncr;

                            el1prop.SetValue(el1, DesignationOfWorkDayType.Label);
                            el2prop.SetValue(el2, coeffWithInbcr.ToString());

                            if (i <= 15)
                            {
                                el1.dayshours1++;
                                el2.dayshours1 += coeffWithInbcr;
                            }
                            else
                            {
                                el1.dayshours2++;
                                el2.dayshours2 += coeffWithInbcr;
                            }

                        }
                    }

                }
            }

            model.Add(el1);
            model.Add(el2);

            var ds = new ReportDataSource("StaffCalculation", model);
            e.DataSources.Add(ds);
        }

    }



}




