
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
        private readonly ILogger<AuthController> _logger;
        public AuthController(IUserService userService, ILogger<AuthController> logger)
        { 
            _userService = userService;
            _logger= logger;
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
                _logger.LogError($"{nameof(AuthController)} >> {nameof(LoginAsync)} :: Message: {ex.Message}\n{ex.StackTrace}\n");
                return StatusCode((int)ex.GetStatus(), ex.GetReason());
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(AuthController)} >> {nameof(LoginAsync)} :: Message: {ex.Message}\n{ex.StackTrace}\n");
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
                _logger.LogError($"{nameof(AuthController)} >> {nameof(RegisterAsync)} :: Message: {ex.Message}\n{ex.StackTrace}\n");
                return StatusCode((int)ex.GetStatus(), ex.GetReason());
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(AuthController)} >> {nameof(RegisterAsync)} :: Message: {ex.Message}\n{ex.StackTrace}\n");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
