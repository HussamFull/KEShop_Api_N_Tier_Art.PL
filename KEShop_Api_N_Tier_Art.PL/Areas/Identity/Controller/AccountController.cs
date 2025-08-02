using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KEShop_Api_N_Tier_Art.PL.Areas.Identity.Controller
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest registerRequest)
        {
            if (registerRequest == null)
            {
                return BadRequest("Invalid registration request.");
            }
            try
            {
                var result = await _authenticationService.RegisterAsync(registerRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Invalid login request.");
            }
            try
            {
                var userResponse = await _authenticationService.LoginAsync(loginRequest);
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
