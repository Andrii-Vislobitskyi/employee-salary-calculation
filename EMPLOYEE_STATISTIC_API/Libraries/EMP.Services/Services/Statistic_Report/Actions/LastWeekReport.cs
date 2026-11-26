using Newtonsoft.Json.Linq;
using EMP.Common.Dto.APP_Dto;
using EMP.Common.Enums;
using EMP.Common.Helpers;
using EMP.Http.Interfaces;
using EMP.Services.Services.Interfaces;
using EMP.Services.Services.Statistic_Report.Models;

namespace EMP.Services.Services.Statistic_Report.Actions
{
    public class LastWeekReport : StatisticReport
    {
        private readonly DateRangeCalculator _dateRangeCalculator;
        private readonly IMathStatistic _mathStatistic;
        private readonly IManagerAPIService _managerAPIService;
        public LastWeekReport(StatisticRange statisticRange, DateRangeCalculator dateRangeCalculator
                             , IMathStatistic mathStatistic, IManagerAPIService managerAPIService)
                             :base(statisticRange) 
        { 
            _dateRangeCalculator = dateRangeCalculator;
            _mathStatistic = mathStatistic;
            _managerAPIService = managerAPIService;
        }
        public async override Task<ReportStatistic> CalculateStatisticReport_Async(StatisticRequestFilter filter)
        {
            RangeDateReq dateReq = _dateRangeCalculator.GetLastWeek(new RangeDateReq(), filter.Today);
          
            JObject jobj = new JObject();

            jobj = await _managerAPIService.GetRangeDayClosedBills_Async(dateReq, filter.Token);

            StatisticByRangeDto statistic = await _mathStatistic.GetStatisticRangeDtoAsync(jobj, filter.ExternalId);
            statistic.BeginDate = dateReq.BeginDate;
            statistic.EndDate = dateReq.EndDate;

            return statistic;
        }

        public async override Task<DiscountReportDto> CalculateEmployee_DiscountReport_Async(StatisticRequestFilter filter)
        {
            RangeDateReq dateReq = _dateRangeCalculator.GetLastWeek(new RangeDateReq(), filter.Today);

            JObject jobj = new JObject();

            jobj = await _managerAPIService.GetRangeDayClosedBills_Async(dateReq, filter.Token);
            throw new NotImplementedException();
        }

    }
}
