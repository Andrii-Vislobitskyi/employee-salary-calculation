using Newtonsoft.Json.Linq;
using EMP.Common.Dto.APP_Dto;
using EMP.Common.Enums;
using EMP.Common.Helpers;
using EMP.Http.Interfaces;
using EMP.Services.Services.Interfaces;
using EMP.Services.Services.Statistic_Report.Models;

namespace EMP.Services.Services.Statistic_Report.Actions
{
    public class TodayReport : StatisticReport
    {
        private readonly DateRangeCalculator _dateRangeCalculator;
        private readonly IMathStatistic _mathStatistic;
        private readonly IManagerAPIService _managerAPIService;
        public TodayReport(StatisticRange statisticRange, DateRangeCalculator dateRangeCalculator
                          , IMathStatistic mathStatistic, IManagerAPIService managerAPIService) 
                          :base(statisticRange) 
        {
            _dateRangeCalculator= dateRangeCalculator;
            _mathStatistic = mathStatistic;
            _managerAPIService = managerAPIService;
        }
        public async override Task<ReportStatistic> CalculateStatisticReport_Async(StatisticRequestFilter filter)
        {
            JObject jobj = await _managerAPIService.GetCurrentDayClosedBills_Async(filter.CurrentDayFilter, filter.Token);

            ReportStatistic statistic = await _mathStatistic.GetStatisticDayDtoAsync(jobj, filter.UserFullName);

            return statistic;
        }

        public async override Task<DiscountReportDto> CalculateEmployee_DiscountReport_Async(StatisticRequestFilter filter)
        {
            JObject jobj = await _managerAPIService.GetCurrentDayClosedBills_Async(filter.CurrentDayFilter, filter.Token);
            throw new NotImplementedException();
        }

    }
}
