
using EMP.Common.Enums;

namespace EMP.Common.Dto.APP_Dto
{
    public class StatisticRequestFilter
    {
        public CurrentDayClosedBillsDto? CurrentDayFilter { get; set; } = null;
        public StatisticRange StatisticRange { get; set; }
        public DateTime Today { get; set; }
        public string Token { get; set; }   = string.Empty;
        public int ExternalId { get; set; }
        public string UserFullName { get; set; } = string.Empty;
    }
}
