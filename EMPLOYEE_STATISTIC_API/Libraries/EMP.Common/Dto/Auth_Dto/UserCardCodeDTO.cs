using System.ComponentModel.DataAnnotations;


namespace EMP.Common.Dto.Auth_Dto
{
    public class UserCardCodeDTO
    {
        [Required]
        public string CardCode { get; set; } = string.Empty;

        [Required]
        public string TermID { get; set; } = string.Empty;
    }
}
