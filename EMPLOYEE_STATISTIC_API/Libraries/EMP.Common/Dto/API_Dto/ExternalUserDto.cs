
namespace EMP.Common.Dto.API_Dto
{
    public class ExternalUserDto
    {
        public int ID { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
    }
}
