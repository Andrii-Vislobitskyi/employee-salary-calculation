
namespace EMP.Services.Services.Statistic_Report.Models
{
    public class DiscountReportDto
    {
        public string CloseUser { get; set; } = string.Empty;
        public int ExternalId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal SubTotal { get => BillItems.Sum(b => b.SubTotal); }
        public decimal DiscountSum { get => BillItems.Sum(b => b.DiscountSum); }
        public decimal Total { get => BillItems.Sum(b => b.Total); }
        public List<DiscountBillItem> BillItems { get; set; } = new List<DiscountBillItem>();
    }
}
