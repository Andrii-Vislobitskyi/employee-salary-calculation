using Newtonsoft.Json.Linq;
using EMP.DataAccess.Models;
using EMP.Services.Services.Statistic_Report.Models;

namespace EMP.Services.Services.Interfaces
{
    public interface IMathStatistic
    {
        /// <summary>
        /// Calculate salary bonus by current day
        /// </summary>
        /// <param name="obj">Json from external API</param>
        /// <param name="fullName">Full Name</param>
        /// <returns>Statistic data</returns>
        Task<StatisticByDayDto> GetStatisticDayDtoAsync(JObject obj, string fullName);
        /// <summary>
        /// Calculate salary bonus by range dates
        /// </summary>
        /// <param name="obj">Json from external API</param>
        /// <param name="externalId">User`s External ID</param>
        /// <returns></returns>
        Task<StatisticByRangeDto> GetStatisticRangeDtoAsync(JObject obj, int externalId);
        /// <summary>
        /// Get External ID
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="obj">Json from external API endpoint /***Waiters***</param>
        /// <returns>External ID</returns>
        Task<int?> GetUserExternalID_Async(User user, JObject obj);
        /// <summary>
        /// Select only biils ID where employees have discount by current day
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>List of bill ID</returns>
        Task<List<int>> GetStaffDiscount_BillId_ByCurrentDay_Async(JObject obj);
        /// <summary>
        /// Select only biils ID where employees have discount by range dates
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>List of bill ID</returns>
        Task<List<int>> GetStaffDiscount_BillId_ByRangeDate_Async(JObject obj);
        /// <summary>
        /// Calculate staff discount data by carrent day
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<DiscountReportDto> GetStaffDiscountReport_ByDay_Async(JObject obj);
        /// <summary>
        /// Calculate staff discount data by range date
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<DiscountReportDto> GetStaffDiscountReport_ByRangeDAte_Async(JObject obj);

    }
}
