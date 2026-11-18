using College.Core.Entities;
using College.Core.Entities;
using College.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College.collegeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var tokenObj = await _authService.LoginAsync(login);
            if (tokenObj == null)
                return Unauthorized();
            return Ok(tokenObj);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            var result = await _authService.RegisterAsync(register);

            return result switch
            {
                RegisterResult.Success => StatusCode(StatusCodes.Status201Created),
                RegisterResult.PasswordsDoNotMatch => BadRequest("Passwords do not match."),
                RegisterResult.EmailAlreadyExists => Conflict("Email already exists."),
                _ => BadRequest()
            };
        }
    }
}
    

    

