
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EMP.Common.Dto.APP_Dto;
using EMP.Common.Enums;
using EMP.Services.Auth.Services;
using EMP.Services.Services.Interfaces;
using EMP.Services.Services.Statistic_Report.Models;

namespace Employee_API.Controllers
{
    [Authorize]
    [Route("api/statistic")]
    [ApiController]
    public class StaticticSaleryController : ControllerBase
    {
        private readonly IStatisticService _serviceStatistic;
        private readonly IUserService _userService;
        private readonly JwtProvider _jwtProvider;
        public StaticticSaleryController(IStatisticService serviceStatistic
                                        , IUserService userService
                                        , JwtProvider jwtProvider) 
        {
            _serviceStatistic = serviceStatistic;
            _userService = userService;
            _jwtProvider = jwtProvider;
        }

        #region CURREN DAY STATISTIC - REMOVE??
        [HttpGet("current-day")]
        [ActionName(nameof(GetClosedBills_Async))]
        public async Task<ActionResult<StatisticByDayDto>> GetClosedBills_Async()
        {
            try
            {
                (int, int, string) userData = _jwtProvider.GetClaimsUserData(HttpContext);

                StatisticRequestFilter filter = new StatisticRequestFilter();
                filter.Today = DateTime.Now;
                filter.Token = userData.Item3;
                filter.StatisticRange = StatisticRange.TODAY;
                filter.CurrentDayFilter = new CurrentDayClosedBillsDto
                {
                    Bracelet = null,
                    PlaceCode = null,
                    PlaceSchemaID = 0,
                    row_limit = 10000,
                    row_offset = 0
                };

                ReportStatistic? statistic = await _serviceStatistic.GetCurrentDayClosedBills_Async(filter);

                return Ok(statistic);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        #endregion



        [HttpPost("bonus-data")]
        public async Task<ActionResult<ReportStatistic>> Get_StatisticByRange_Async([FromBody] CurrentDayClosedBillsDto? currentDay, StatisticRange statisticRange)
        {
            try
            {
                if (statisticRange == StatisticRange.TODAY)
                    return RedirectToAction(nameof(GetClosedBills_Async));


                (int, int, string) userData = _jwtProvider.GetClaimsUserData(HttpContext);

                StatisticRequestFilter filter = new StatisticRequestFilter();
                filter.Today = DateTime.Now;
                filter.Token = userData.Item3;
                filter.ExternalId = userData.Item2;
                filter.StatisticRange = statisticRange;
                filter.CurrentDayFilter = currentDay;
                filter.UserFullName = await _userService.GetFullName_Async(userData.Item2);
              
                ReportStatistic? statisticRangeDto = await _serviceStatistic.GetStatisticByDateRange_Async(filter);

                return Ok(statisticRangeDto);
            }        
            catch (Exception ex)
            {             
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }





        [HttpPost("discount-details")]
        public async Task<ActionResult<DiscountReportDto>> Get_DiscountDetails_Async([FromBody] CurrentDayClosedBillsDto? currentDay, StatisticRange statisticRange)
        {
            try
            {

                var claim = HttpContext.User.Claims.FirstOrDefault();


                (int, int, string) userData = _jwtProvider.GetClaimsUserData(HttpContext);

                StatisticRequestFilter filter = new StatisticRequestFilter();
                filter.Today = DateTime.Now;
                filter.Token = userData.Item3;
                filter.ExternalId = userData.Item2;
                filter.StatisticRange = statisticRange;
                filter.CurrentDayFilter = currentDay;
                filter.UserFullName = await _userService.GetFullName_Async(userData.Item2);


                DiscountReportDto discountReport = await _serviceStatistic.GetRangeDate_StaffDiscountReport_Async(filter);

                return Ok(discountReport);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }
    }
}
