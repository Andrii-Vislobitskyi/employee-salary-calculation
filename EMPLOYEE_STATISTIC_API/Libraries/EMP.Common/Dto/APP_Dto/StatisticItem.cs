
namespace EMP.Common.Dto.APP_Dto
{
    public class StatisticItem
    {
       public int BillId {  get; set; }
       public int BillNumber {  get; set; }
       public decimal Total {  get; set; }
       public DateTime Date { get; set; }
       public string CloseUserName { get; set; } = string.Empty;
    }
}
