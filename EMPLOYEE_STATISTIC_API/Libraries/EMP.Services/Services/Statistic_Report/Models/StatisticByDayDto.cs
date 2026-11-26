using EMP.Common.Dto.APP_Dto;

namespace EMP.Services.Services.Statistic_Report.Models
{
    public class StatisticByDayDto : ReportStatistic
    {
        public DateTime Date { get; set; }
        public decimal Total { get; set; } = 0;
        public decimal Bonus_Salary { get; set; } = 0;
        public List<StatisticItem> StatisticItems { get; set; } = new List<StatisticItem>();
    }
}
