using EMP.Common.Dto.APP_Dto;
using EMP.Services.Services.Statistic_Report.Models;

namespace EMP.Services.Services.Interfaces
{
    public interface IStatisticService
    {
        Task<ReportStatistic?> GetCurrentDayClosedBills_Async(StatisticRequestFilter filter);
        Task<ReportStatistic?> GetStatisticByDateRange_Async(StatisticRequestFilter filter);
        Task<DiscountReportDto> GetCurrentDay_StaffDiscountReport_Async(StatisticRequestFilter filter);
        Task<DiscountReportDto> GetRangeDate_StaffDiscountReport_Async(StatisticRequestFilter filter);
    }
}
