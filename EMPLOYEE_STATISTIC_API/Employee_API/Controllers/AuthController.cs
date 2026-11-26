
using Microsoft.AspNetCore.Mvc;
using EMP.Common.Dto.Auth_Dto;
using EMP.Common.Exceptions;
using EMP.Services.Services.Interfaces;

namespace Employee_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        { 
           _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO requestDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string? token = await  _userService.LoginUser_Async(requestDTO);

                return Ok(token);
            }
            catch(CustomResponseException ex)
            {
                return StatusCode((int)ex.GetStatus(), ex.GetReason());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDTO requestDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string token = await _userService.RegisterUser_Async(requestDTO);

                return Ok(token);
            }
            catch(CustomResponseException ex)
            {
                return StatusCode((int)ex.GetStatus(), ex.GetReason());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
