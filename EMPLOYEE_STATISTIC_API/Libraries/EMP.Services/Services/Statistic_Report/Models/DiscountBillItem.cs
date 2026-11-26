
namespace EMP.Services.Services.Statistic_Report.Models
{
    public class DiscountBillItem
    {
        public int Id { get; set; } 
        public int ParentBillId {  get; set; }
        public DateTime BillDate { get; set; }
        public string ArticleName { get; set; } = string.Empty;
        /// <summary>
        /// Full prace
        /// </summary>
        public decimal SubTotal { get; set; }
        /// <summary>
        /// Discount sum
        /// </summary>
        public decimal DiscountSum { get; set; }
        /// <summary>
        /// The difference in payment
        /// </summary>
        public decimal Total { get; set; }
    }
}
