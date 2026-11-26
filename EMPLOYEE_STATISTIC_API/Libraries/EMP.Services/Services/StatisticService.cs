
using EMP.Common.Dto.APP_Dto;
using EMP.Http.Interfaces;
using EMP.Services.Services.Interfaces;
using EMP.Services.Services.Statistic_Report;
using EMP.Services.Services.Statistic_Report.Models;

namespace EMP.Services.Services
{
    public class StatisticService : IStatisticService
    {     
        private readonly IMathStatistic _mathStatistic;
        private readonly StatistcActionStorage _actionStorage;
        public StatisticService(IManagerAPIService managerAPIService, IMathStatistic mathStatistic
                                ,StatistcActionStorage actionStorage) 
        {
            _mathStatistic = mathStatistic;
            _actionStorage = actionStorage;   
        }

        #region Methods
        public async Task<ReportStatistic?> GetCurrentDayClosedBills_Async(StatisticRequestFilter filter)
        {         
            var worker = _actionStorage.StatisticStorage.Where(a => a.StatisticKey == filter.StatisticRange).First();

            ReportStatistic? statistic = await worker.CalculateStatisticReport_Async(filter) as StatisticByDayDto;

            return statistic;
        }

        public async Task<ReportStatistic?> GetStatisticByDateRange_Async(StatisticRequestFilter filter)
        {
            var worker = _actionStorage.StatisticStorage.Where(a => a.StatisticKey == filter.StatisticRange).First();

            ReportStatistic? statistic = await worker.CalculateStatisticReport_Async(filter);

            return statistic;
        }

        public async Task<DiscountReportDto> GetCurrentDay_StaffDiscountReport_Async(StatisticRequestFilter filter)
        {
            var worker = _actionStorage.StatisticStorage.Where(a => a.StatisticKey == filter.StatisticRange).First();
            DiscountReportDto report = await worker.CalculateEmployee_DiscountReport_Async(filter);
            return report;
        }
        public async Task<DiscountReportDto> GetRangeDate_StaffDiscountReport_Async(StatisticRequestFilter filter)
        {
            var worker = _actionStorage.StatisticStorage.Where(a => a.StatisticKey == filter.StatisticRange).First();
            DiscountReportDto report = await worker.CalculateEmployee_DiscountReport_Async(filter);
            return report;  
        }

        #endregion
    }
}
