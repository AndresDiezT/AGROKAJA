using Backend.DTOs.UserDTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // PUT: api/Auth/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);

            if (!result.Success)
            {
                return BadRequest(new { error = result.Error });
            }

            return Ok(new { message = "Registro exitoso" });
        }

        // PUT: api/Auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            if (!result.Success)
            {
                if (result.Error == "Usuario no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return Ok(result.Data);
        }
    }
}
