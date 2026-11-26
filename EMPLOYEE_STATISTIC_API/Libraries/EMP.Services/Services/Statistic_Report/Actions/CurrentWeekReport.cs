using Newtonsoft.Json.Linq;
using EMP.Common.Dto.APP_Dto;
using EMP.Common.Enums;
using EMP.Common.Helpers;
using EMP.Http.Interfaces;
using EMP.Services.Services.Interfaces;
using EMP.Services.Services.Statistic_Report.Models;

namespace EMP.Services.Services.Statistic_Report.Actions
{
    public class CurrentWeekReport : StatisticReport
    {
        private readonly DateRangeCalculator _dateRangeCalculator;
        private readonly IMathStatistic _mathStatistic;
        private readonly IManagerAPIService _managerAPIService;
        public CurrentWeekReport(StatisticRange statisticRange, DateRangeCalculator dateRangeCalculator
                                 , IMathStatistic mathStatistic, IManagerAPIService managerAPIService)
                                :base(statisticRange) 
        { 
            _dateRangeCalculator = dateRangeCalculator;
            _mathStatistic = mathStatistic;
            _managerAPIService = managerAPIService;
        }
        public async override Task<ReportStatistic> CalculateStatisticReport_Async(StatisticRequestFilter filter)
        {       
            RangeDateReq dateReq = _dateRangeCalculator.GetCurrentWeek(new RangeDateReq(), filter.Today);

            (JObject?, JObject?) multi = await _managerAPIService.GetMultipleData(filter, dateReq);

            StatisticByDayDto todayStatistic = await _mathStatistic.GetStatisticDayDtoAsync(multi.Item1, filter.UserFullName);
            StatisticByRangeDto rangeStatistic = await _mathStatistic.GetStatisticRangeDtoAsync(multi.Item2, filter.ExternalId);


            rangeStatistic.BeginDate = dateReq.BeginDate;
            rangeStatistic.EndDate = new DateTime(dateReq.EndDate.Year, dateReq.EndDate.Month
                                                , dateReq.EndDate.Day+1, dateReq.EndDate.Hour
                                                , dateReq.EndDate.Minute, dateReq.EndDate.Second);

            rangeStatistic.Total += todayStatistic.Total;
            rangeStatistic.Bonus_Salary += todayStatistic.Bonus_Salary;
            rangeStatistic.Count_WorkShift += 1;

            return rangeStatistic;
        }

        public async override Task<DiscountReportDto> CalculateEmployee_DiscountReport_Async(StatisticRequestFilter filter)
        {
            RangeDateReq dateReq = _dateRangeCalculator.GetCurrentWeek(new RangeDateReq(), filter.Today);

            (JObject?, JObject?) multi = await _managerAPIService.GetMultipleData(filter, dateReq);
            throw new NotImplementedException();
        }

    }
}
