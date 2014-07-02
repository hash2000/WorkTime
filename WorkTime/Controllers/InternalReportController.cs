using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkTime.Models;
using WorkTime.Utils.Reporting;
using WorkTime.Utils.Security;

namespace WorkTime.Controllers
{
    public class StaffCalculationDto
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

        public string StaffInfo { get; set; }
        public int TabelNumber { get; set; }
        public string Post { get; set; }
        public int DaysWorked { get; set; }

        public double OT { get; set; }
        public double OD { get; set; }
        public double OCH { get; set; }
        public double R { get; set; }
        public double OG { get; set; }
        public double B { get; set; }
        public double E { get; set; }
        public double DO { get; set; }
        public double OZ { get; set; }
        public double SK { get; set; }
        public double PK { get; set; }
        public double K { get; set; }

        public int Holidays { get; set; }
        public double HoursPlan { get; set; }

        public double HoursFact { get; set; }
        public int DaysInMonth { get; set; }

    }

    public class InternalPostTypesSummaries
    {
        public double Employees { get; set; }
        public double Pluralists { get; set; }
        public double Experts { get; set; }
    }

    public class InternalReportController : Controller
    {
        public ContributorsEntities dbContext { get; set; }

        public InternalReportController(ContributorsEntities context)
        {
            // получение контекста EF из Autofac
            dbContext = context;
        }

        [DtoAction(DtoActionOperationType.Read)]
        public ReportResult Generate(DateTime dateFrom, DateTime dateTo)
        {

            if (dateFrom.Month != dateTo.Month && dateFrom.Year != dateTo.Year)
                throw new Exception("Выбранный период должен охватывать только один месяц");

            var vacations = dbContext.GetVacationsOnRange(dateFrom, dateTo).ToList();
            var vacationTypes = dbContext.VacationTypes.Select(n => n).ToList();
            // поиск обозначение для рабочего дня
            //  хранится в системных настройках под именем 'DesignationOfWorkDay'
            VacationType designationOfWorkDayType = null;
            var DesignationOfWorkDayTypeDb = dbContext.SysParameters
                .Where(n => n.Name == "DesignationOfWorkDay")
                .FirstOrDefault();
            if (DesignationOfWorkDayTypeDb != null)
            {
                designationOfWorkDayType = vacationTypes.Where(n => n.Id == int.Parse(DesignationOfWorkDayTypeDb.Value))
                    .FirstOrDefault();
            }

            VacationType holidayType = null;
            var HolidayTypeDb = dbContext.SysParameters
                .Where(n => n.Name == "Holiday")
                .FirstOrDefault();
            if (HolidayTypeDb != null)
            {
                holidayType = vacationTypes.Where(n => n.Id == int.Parse(HolidayTypeDb.Value))
                    .FirstOrDefault();
            }

            var postTypesDayTimeCoefficients = dbContext
                .PostTypesDayTimeCoefficients
                .Select(n => n)
                .ToList();

            var postTypesDayTimeCoefficientValues = dbContext
                .PostTypesDayTimeCoefficientValues
                .Select(n => n)
                .ToList();

            // все праздники на выбранный диапазон дат 
            var holidaysInDateRange = dbContext
                .GetHolidaysInDate(dateFrom, dateTo)
                .ToList();

            var posts = dbContext
                .Posts
                .Select(n => n)
                .ToList();

            List<StaffCalculationDto> internalStaffCalculation = new List<StaffCalculationDto>();
            List<InternalPostTypesSummaries> internalPostTypesSummarySet = new List<InternalPostTypesSummaries>();
            var internalPostTypesSummary = new InternalPostTypesSummaries();
            var daysinmonth = DateTime.DaysInMonth(dateFrom.Year, dateFrom.Month);
            var staffs = dbContext.GetStaffsSummary().ToList();

            // цикл по всем сотрудникам
            foreach (var staff in staffs)
            {
                var model = new StaffCalculationDto();
                model.StaffInfo = staff.StaffName;
                model.TabelNumber = staff.TabelNumber;
                model.Post = posts.Where(n => n.Id == staff.PostId).FirstOrDefault().Name;
                model.DaysInMonth = daysinmonth;

                var regNormDayCount = dbContext.RegNormGraphTypes
                    .Where(n => n.Id == staff.RegNormGraphTypeId)
                    .First()
                    .DayCount;

                var posttype = dbContext
                    .PostTypes.Where(n => n.Id == staff.PostTypeId)
                    .First();

                var coefficient = postTypesDayTimeCoefficients
                    .Where(n => n.Id == posttype.PostTypesDayTimeCoefficientId)
                    .First();

                var coefficientvalue = postTypesDayTimeCoefficientValues
                    .Where(n => n.GendersId == staff.GenderId && n.PostTypesDayTimeCoefficientId == coefficient.Id)
                    .First();

                        
                // цикл по дням месяца
                for (int i = 1; i <= 31; i++)
                {
                    var elprop = model.GetType().GetProperty(string.Format("c{0}", i));
                    if (i > dateTo.Day)
                    {
                        elprop.SetValue(model, "X");
                        continue;
                    }

                    var date = new DateTime(dateFrom.Year, dateFrom.Month, i);
                    var nextdate = date.AddDays(1);
                    var vacation = dbContext.Vacations
                        .Where(n => n.StartDate <= date && date <= n.EndDateWithHolidays && n.StaffId == staff.Id)
                        .FirstOrDefault();
                    
                    int TimeNormOfDayIncr = 0;
                    var IsNextDayAreHoliday = dbContext.Holidays.Where(n =>
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
                        var vacationType = dbContext.VacationTypes
                            .Where(n => n.Id == vacation.VacationTypeId)
                            .First();
                        elprop.SetValue(model, vacationType.Label);

                        switch (vacationType.Label)
                        { 
                            case "ОТ": model.OT++; break;
                            case "ОД": model.OD++; break;
                            case "ОЧ": model.OCH ++; break;
                            case "Р": model.R++; break;
                            case "ОЖ": model.OG++; break;
                            case "Б": model.B++; break;
                            case "Е": model.E++; break;
                            case "ДО": model.DO++; break;
                            case "ОЗ": model.OZ++; break;
                            case "СК": model.SK++; break;
                            case "ПК": model.PK++; break;
                            case "К": model.K++; break;
                        }

                    }
                    else
                    {
                        // суббота для пятидневки и воскресенье
                        //  обозначение выходного дня
                        if ((date.DayOfWeek == DayOfWeek.Saturday && regNormDayCount == 5) ||
                            (date.DayOfWeek == DayOfWeek.Sunday))
                        {
                            if (holidayType != null)
                            {
                                elprop.SetValue(model, holidayType.Label);
                                model.Holidays++;
                            }
                        }
                        else
                        {
                            // обозначение рабочего дня
                            if (designationOfWorkDayType != null)
                            {
                                var coeffWithInbcr = coefficientvalue.TimeNormOfDay + TimeNormOfDayIncr;
                                model.HoursFact += coeffWithInbcr;
                                model.DaysWorked++;
                                elprop.SetValue(model, coeffWithInbcr.ToString());
                            }
                        }


                    }

                    //.......................
                }

                internalStaffCalculation.Add(model);
            }

            internalPostTypesSummarySet.Add(internalPostTypesSummary);

            return new ReportResult(ReportFormat.Excel, "InternalReport"
                , Server.MapPath("~/Reports/Internal/InternalReport.rdlc")
                , new[] {
                    new ReportDataSource ("InternalStaffCalculation", internalStaffCalculation ) ,
                    new ReportDataSource ("InternalPostTypesSummaries", internalPostTypesSummarySet ) 
                }
            );

        }
    }
}