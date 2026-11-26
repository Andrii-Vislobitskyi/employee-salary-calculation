using EMP.Common.Dto.APP_Dto;
using EMP.Common.Enums;
using EMP.Services.Services.Statistic_Report.Models;

namespace EMP.Services.Services.Statistic_Report.Actions
{
    public abstract class StatisticReport
    {
        protected readonly StatisticRange _statisticRange;
        protected StatisticReport(StatisticRange statisticRange)
        {
            _statisticRange = statisticRange;
        }

        public StatisticRange StatisticKey { get => _statisticRange; }
        public abstract Task<ReportStatistic> CalculateStatisticReport_Async(StatisticRequestFilter filter);
        public abstract Task<DiscountReportDto> CalculateEmployee_DiscountReport_Async(StatisticRequestFilter filter);  
     
    }
}
