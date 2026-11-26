using System.ComponentModel.DataAnnotations;

namespace EMP.Common.Dto.Auth_Dto
{
    public class RegisterRequestDTO
    {
        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Patronymic { get; set; } = string.Empty;  

        [Required]
        public string Password { get; set; } = string.Empty;

        public UserCardCodeDTO UserCardCode { get; set; } = new UserCardCodeDTO();

    }
}
