
namespace EMP.Services.Services.Statistic_Report.Models
{
    public class StatisticByRangeDto: ReportStatistic
    {
        public string CloseUser { get; set; } = string.Empty;
        public int ExternalId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Total { get; set; }
        public decimal Bonus_Salary { get; set; }
        public int Count_WorkShift { get; set; }
    }
}
