using Newtonsoft.Json.Linq;
using EMP.Common.Dto.APP_Dto;

namespace EMP.Http.Interfaces
{
    public interface IManagerAPIService : IUser, IAuthenticate
    {
        Task<JObject> GetCurrentDayClosedBills_Async(CurrentDayClosedBillsDto dto, string token);

        Task<JObject> GetRangeDayClosedBills_Async(RangeDateReq range, string token);

        Task<(JObject?, JObject?)> GetMultipleData(StatisticRequestFilter filter, RangeDateReq range);
    }
}
