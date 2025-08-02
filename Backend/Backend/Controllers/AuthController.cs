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

        // POST: api/Auth/register
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

        // POST: api/Auth/employee/register
        [Authorize(Policy = "permission:admin.employees.create")]
        [HttpPost("register/employee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] AddEmployeeDto addEmployeeDto)
        {
            var result = await _authService.AddEmployeeAsync(addEmployeeDto);

            if (!result.Success)
            {
                return BadRequest(result.Data);
            }

            return Ok(result.Data);
        }

        // POST: api/Auth/employee/resend-credentials
        [Authorize(Policy = "permission:admin.employees.resendCredentials")]
        [HttpPost("employee/resend-credentials")]
        public async Task<IActionResult> ResendEmployeeCredentials([FromBody] string document)
        {
            var result = await _authService.ResendCredentialsAsync(document);

            if (!result.Success)
            {
                return BadRequest(new { error = result.Error });
            }

            return Ok(result.Data);
        }

        // GET: api/Auth/confirm-email?document=231231&code=123456
        [HttpGet("email-verification")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string document, int code)
        {
            var result = await _authService.VerifyEmailAsync(document, code);

            if (!result.Success)
            {
                return BadRequest(result.Data);
            }

            return Ok(result.Data);
        }

        // POST: api/Auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            if (result.Success)
                return Ok(result.Data);

            var errors = new Dictionary<string, string[]>();

            switch (result.Error)
            {
                case "Todos los campos son obligatorios":
                    errors["Email"] = new[] { result.Error };
                    errors["Password"] = new[] { result.Error };
                    break;

                case "No hay un usuario registrado con este correo":
                    errors["Email"] = new[] { result.Error };
                    break;

                case "Correo o contraseña incorrecta":
                    errors["Email"] = new[] { result.Error };
                    errors["Password"] = new[] { result.Error };
                    break;

                default:
                    errors["General"] = new[] { result.Error };
                    break;
            }

            return BadRequest(new { errors });
        }

        // POST: api/Auth/reset-password
        [AllowAnonymous]
        [HttpPost("password-reset/request")]
        public async Task<IActionResult> PasswordToken([FromBody] string email)
        {
            var result = await _authService.GeneratePasswordResetTokenAsync(email);
            if (!result.Success)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Data);
        }

        // POST: api/Auth/confirm-reset-password
        [AllowAnonymous]
        [HttpPost("password-reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var result = await _authService.ResetPasswordAsync(resetPasswordDto);

            if (!result.Success)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Data);
        }

        // GET: api/auth/me
        [Authorize(Policy = "permission:common.profile.access")]
        [HttpGet("me")]
        public async Task<ActionResult<ProfileDto>> GetProfile()
        {
            var result = await _authService.GetProfileAsync();

            if (!result.Success)
            {
                if (result.Error == "Usuario no encontrado")
                    return NotFound(result.Error);

                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        // POST: api/Auth/refresh-token
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var result = await _authService.RefreshTokenAsync();
            if (!result.Success)
                return Unauthorized(new { error = result.Error });

            return Ok(result.Data);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.LogoutAsync();
            if (!result.Success)
            {
                return BadRequest(new { error = result.Error });
            }
            return Ok(result.Data);
        }
    }
}
