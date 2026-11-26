
using EMP.Common.Dto.API_Dto;

namespace EMP.Common.Dto.APP_Dto
{
    public class AppUserDto : ExternalUserDto
    {
        public string Token { get; set; } = string.Empty;
    }
}
