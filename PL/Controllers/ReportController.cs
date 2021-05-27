using AutoMapper;
using BLL.Managers;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReport _report;
        private readonly IMapper _mapper;
        public ReportController(IReport report, IMapper mapper)
        {
            _report = report;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetOrders(DateTime? date1, DateTime? date2)
        {
            if (date1 == null || date2 == null)
                return View("Error", new ErrorViewModel { Message = "Date not entered", ViewName = "Index", ControllerName = "Report" });
            if (date1.Value > date2.Value)
                return View("Error", new ErrorViewModel { Message = "The end date must be greater than the start date", ViewName = "Index", ControllerName = "Report" });
            DateTime endDate = date2.Value;
            endDate = endDate.AddDays(1);
            var items = _mapper.Map<IEnumerable<OrderViewModel>>(_report.GetOrdersByDate(date1.Value.Date, endDate.Date));
            return View(items);
        }
        [HttpGet]
        public ActionResult GetReportByPizzaCount(DateTime? date1, DateTime? date2)
        {
            if (date1 == null || date2 == null)
                return View("Error", new ErrorViewModel { Message = "Date not entered", ViewName = "Index", ControllerName = "Report" });
            if (date1.Value > date2.Value)
                return View("Error", new ErrorViewModel { Message = "The end date must be greater than the start date", ViewName = "Index", ControllerName = "Report" });
            DateTime endDate = date2.Value;
            endDate = endDate.AddDays(1);
            var items = _mapper.Map<IEnumerable<ReportByPizzaCountViewModel>>(_report.GetReportByPizzaCount(date1.Value.Date, endDate));

            Chart chart = new Chart(width: 700, height: 300)
               .AddTitle($"Quantitative statistics on pizzas from {date1.Value.ToShortDateString()} to {date2.Value.ToShortDateString()}")
               .AddSeries(
                   xValue: items.Select(it => it.PizzaName).ToArray(),
                   yValues: items.Select(it => it.Count).ToArray()
               )
               .Write();

            return null;
        }
        [HttpGet]
        public ActionResult GetReportByPizzaPrice(DateTime? date1, DateTime? date2)
        {
            if(date1 == null || date2 == null)
                return View("Error", new ErrorViewModel { Message = "Date not entered", ViewName = "Index", ControllerName = "Report" });
            if (date1.Value > date2.Value)
                return View("Error", new ErrorViewModel { Message = "The end date must be greater than the start date", ViewName = "Index", ControllerName = "Report" });
            DateTime endDate = date2.Value;
            endDate = endDate.AddDays(1);
            var items = _mapper.Map<IEnumerable<ReportByPizzaPriceViewModel>>(_report.GetReportByPizzaPrice(date1.Value.Date, endDate));

            Chart chart = new Chart(width: 700, height: 300)
               .AddTitle($"Pizza price statistics from {date1.Value.ToShortDateString()} to {date2.Value.ToShortDateString()}")
               .AddSeries(
                   xValue: items.Select(it => it.PizzaName).ToArray(),
                   yValues: items.Select(it => it.TotalPrice).ToArray()
               )
               .Write();

            return null;
        }
        [HttpGet]
        public ActionResult GetReportByEmployeeCount(DateTime? date1, DateTime? date2)
        {
            if (date1 == null || date2 == null)
                return View("Error", new ErrorViewModel { Message = "Date not entered", ViewName = "Index", ControllerName = "Report" });
            if (date1.Value > date2.Value)
                return View("Error", new ErrorViewModel { Message = "The end date must be greater than the start date", ViewName = "Index", ControllerName = "Report" });
            DateTime endDate = date2.Value;
            endDate = endDate.AddDays(1);
            var items = _mapper.Map<IEnumerable<ReportByEmployeeCountViewModel>>(_report.GetReportByEmployeeCount(date1.Value.Date, endDate));

            Chart chart = new Chart(width: 700, height: 300)
               .AddTitle($"Statistics on employees by the number of orders received from {date1.Value.ToShortDateString()} to {date2.Value.ToShortDateString()}")
               .AddSeries(
                   chartType: "Pie",
                   xValue: items.Select(it => it.EmployeeName).ToArray(),
                   yValues: items.Select(it => it.Count).ToArray()
               )
               .Write();

            return null;
        }
        [HttpGet]
        public ActionResult GetReportByEmployeePrice(DateTime? date1, DateTime? date2)
        {
            if (date1 == null || date2 == null)
                return View("Error", new ErrorViewModel { Message = "Date not entered", ViewName = "Index", ControllerName = "Report" });
            if (date1.Value > date2.Value)
                return View("Error", new ErrorViewModel { Message = "The end date must be greater than the start date", ViewName = "Index", ControllerName = "Report" });
            DateTime endDate = date2.Value;
            endDate = endDate.AddDays(1);
            var items = _mapper.Map<IEnumerable<ReportByEmployeePriceViewModel>>(_report.GetReportByEmployeePrice(date1.Value.Date, endDate));

            Chart chart = new Chart(width: 700, height: 300)
               .AddTitle($"Statistics on employees by the price of orders received from {date1.Value.ToShortDateString()} to {date2.Value.ToShortDateString()}")
               .AddSeries(
                   xValue: items.Select(it => it.EmployeeName).ToArray(),
                   yValues: items.Select(it => it.TotalPrice).ToArray()
               )
               .Write();

            return null;
        }
        [HttpGet]
        public ActionResult GetTotalPrice(DateTime? date1, DateTime? date2)
        {
            if (date1 == null || date2 == null)
                return View("Error", new ErrorViewModel { Message = "Date not entered", ViewName = "Index", ControllerName = "Report" });
            if (date1.Value > date2.Value)
                return View("Error", new ErrorViewModel { Message = "The end date must be greater than the start date", ViewName = "Index", ControllerName = "Report" });
            DateTime endDate = date2.Value;
            endDate = endDate.AddDays(1);
            double price = _report.GetTotalPrice(date1.Value.Date, endDate);

            return View(price);
        }
        [HttpGet]
        public ActionResult GetReportByClient(DateTime? date1, DateTime? date2)
        {
            if (date1 == null || date2 == null)
                return View("Error", new ErrorViewModel { Message = "Date not entered", ViewName = "Index", ControllerName = "Report" });
            if (date1.Value > date2.Value)
                return View("Error", new ErrorViewModel { Message = "The end date must be greater than the start date", ViewName = "Index", ControllerName = "Report" });
            DateTime endDate = date2.Value;
            endDate = endDate.AddDays(1);
            var items = _mapper.Map<IEnumerable<ReportByClientViewModel>>(_report.GetReportByClient(date1.Value.Date, endDate));

            Chart chart = new Chart(width: 700, height: 300)
               .AddTitle($"Statistics on client by the number of orders received from {date1.Value.ToShortDateString()} to {date2.Value.ToShortDateString()}")
               .AddSeries(
                   xValue: items.Select(it => it.ClientName).ToArray(),
                   yValues: items.Select(it => it.Count).ToArray()
               )
               .Write();

            return null;
        }
    }
}