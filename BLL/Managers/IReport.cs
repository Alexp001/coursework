using BLL.DTO;
using System;
using System.Collections.Generic;

namespace BLL.Managers
{
    public interface IReport
    {
        IEnumerable<ReportByPizzaCountDto> GetReportByPizzaCount(DateTime date1, DateTime date2);
        IEnumerable<ReportByPizzaPriceDto> GetReportByPizzaPrice(DateTime date1, DateTime date2);
        IEnumerable<ReportByEmployeeCountDto> GetReportByEmployeeCount(DateTime date1, DateTime date2);
        IEnumerable<ReportByEmployeePriceDto> GetReportByEmployeePrice(DateTime date1, DateTime date2);
        double GetTotalPrice(DateTime date1, DateTime date2);
        IEnumerable<OrderDto> GetOrdersByDate(DateTime date1, DateTime date2);
        IEnumerable<ReportByClientDto> GetReportByClient(DateTime date1, DateTime date2);

    }
}
