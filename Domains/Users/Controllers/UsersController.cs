using HomeApi.Domains.Users.DTOs;
using HomeApi.Domains.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Domains.Users.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = await _service.RegisterAsync(request);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _service.LoginAsync(request);

            if (user == null)
                return Unauthorized("Invalid credentials");

            return Ok(user);
        }
    }
}
