
namespace EMP.Common.Dto.APP_Dto
{
    public class CurrentDayClosedBillsDto
    {      
        public string? Bracelet { get; set; } = null;
        public string? PlaceCode { get; set; } = null;
        public int? PlaceSchemaID { get; set; } = 0;
        public int row_limit { get; set; } = 10000;
        public int row_offset { get; set; } = 0;
    }
}
