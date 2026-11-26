using EMP.Common.Enums;
using EMP.Common.Helpers;
using EMP.Http.Interfaces;
using EMP.Services.Services.Interfaces;
using EMP.Services.Services.Statistic_Report.Actions;

namespace EMP.Services.Services.Statistic_Report
{
    public class StatistcActionStorage
    {
        private readonly List<StatisticReport> _statisticStorage;

        public StatistcActionStorage(DateRangeCalculator dateRangeCalculator
                                     , IMathStatistic mathStatistic
                                     , IManagerAPIService managerAPIService)
        {
            _statisticStorage = new List<StatisticReport>()
            {
                 new TodayReport(StatisticRange.TODAY,dateRangeCalculator, mathStatistic,managerAPIService),
                 new YesterdayReport(StatisticRange.YESTERDAY, dateRangeCalculator,mathStatistic,managerAPIService),
                 new CurrentWeekReport(StatisticRange.CURRENT_WEEK, dateRangeCalculator, mathStatistic,managerAPIService),
                 new LastWeekReport(StatisticRange.LAST_WEEK, dateRangeCalculator,mathStatistic,managerAPIService),
                 new CurrentMonthReport(StatisticRange.CURRENT_MONTH, dateRangeCalculator, mathStatistic,managerAPIService),
                 new LastMonthReport(StatisticRange.LAST_MONTH , dateRangeCalculator, mathStatistic,managerAPIService),
            };
        }

        public List<StatisticReport> StatisticStorage { get => _statisticStorage; }
    }
}
