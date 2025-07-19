using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.UserDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly BackendDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(BackendDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Registra un nuevo usuario en la base de datos y retorna sus datos públicos.
        /// </summary>
        /// <param name="registerDto">
        /// Objeto con los datos necesarios para el registro:
        /// <list type="bullet">
        ///   <item><c>Document</c>: Número de documento único del usuario.</item>
        ///   <item><c>Username</c>: Nombre de usuario único.</item>
        ///   <item><c>Email</c>: Correo electrónico único.</item>
        ///   <item><c>Password</c>: Contraseña en texto plano (será encriptada).</item>
        ///   <item><c>FirstName</c>: Nombre(s) del usuario.</item>
        ///   <item><c>LastName</c>: Apellido(s) del usuario.</item>
        ///   <item><c>PhoneNumber</c>: Número de celular (10 dígitos).</item>
        ///   <item><c>BirthDate</c>: Fecha de nacimiento.</item>
        ///   <item><c>RoleId</c>: ID del rol asignado.</item>
        ///   <item><c>IdTypeDocument</c>: ID del tipo de documento.</item>
        /// </list>
        /// </param>
        /// <returns>
        /// Objeto <see cref="ReadUserDto"/> con los datos visibles del usuario recién creado.
        /// </returns>
        /// <exception cref="Exception">
        /// Se lanza si ya existe un usuario con el mismo correo o nombre de usuario.
        /// </exception>
        public async Task<Result<ReadUserDto>> RegisterAsync(RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                return Result<ReadUserDto>.Fail("El correo ya existe");

            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
                return Result<ReadUserDto>.Fail("El nombre de usuario ya existe");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var newUser = new User
            {
                Document = registerDto.Document,
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
                BirthDate = registerDto.BirthDate,
                CreatedAt = DateTime.Now,
                IsActive = true,
                IdRole = registerDto.IdRole,
                IdTypeDocument = registerDto.IdTypeDocument
            };

            _context.Users.Add(newUser);

            await _context.SaveChangesAsync();

            var roleName = await _context.Roles
                .Where(r => r.IdRole == newUser.IdRole)
                .Select(r => r.NameRole)
                .FirstOrDefaultAsync();
            var typeDocumentName = await _context.TypesDocument
                .Where(td => td.IdTypeDocument == newUser.IdTypeDocument)
                .Select(td => td.NameTypeDocument)
                .FirstOrDefaultAsync();

            var dto = new ReadUserDto
            {
                Document = newUser.Document,
                Username = newUser.Username,
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                PhoneNumber = newUser.PhoneNumber,
                BirthDate = newUser.BirthDate,
                CreatedAt = newUser.CreatedAt,
                IsActive = newUser.IsActive,
                IdRole = newUser.IdRole,
                RoleName = roleName,
                IdTypeDocument = newUser.IdTypeDocument,
                TypeDocumentName = typeDocumentName
            };

            return Result<ReadUserDto>.Ok(dto);
        }

        /// <summary>
        /// Autentica a un usuario y genera un token JWT de acceso.
        /// </summary>
        /// <param name="loginDto">
        /// Objeto con los datos de inicio de sesión:
        /// <list type="bullet">
        ///   <item><c>Email</c>: Correo electrónico registrado del usuario.</item>
        ///   <item><c>Password</c>: Contraseña en texto plano para validar.</item>
        /// </list>
        /// </param>
        /// <returns>
        /// Un <see cref="LoginResponseDto"/> que contiene:
        /// <list type="number">
        ///   <item><see cref="LoginResponseDto.AccessToken"/>: Token JWT para autorizar peticiones.</item>
        ///   <item><see cref="LoginResponseDto.Email"/>: Correo del usuario autenticado.</item>
        ///   <item><see cref="LoginResponseDto.ExpiresIn"/>: Tiempo de expiración del token en segundos.</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Se lanza si <paramref name="loginDto"/> es nulo o si <c>Email</c> o <c>Password</c> están vacíos.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se lanza si el correo no existe o la contraseña es incorrecta.
        /// </exception>
        public async Task<Result<LoginResponseDto>> LoginAsync(LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                return Result<LoginResponseDto>.Fail("debes ingresar los datos correctamente");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user is null)
                return Result<LoginResponseDto>.Fail("Usuario no encontrado");

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return Result<LoginResponseDto>.Fail("Contraseña incorrecta");

            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Document),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("username", user.Username),
                new Claim("role", user.IdRole.ToString()),
                new Claim(ClaimTypes.Role, user.IdRole.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    SecurityAlgorithms.HmacSha512Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securyToken = tokenHandler.CreateToken(tokenDescriptor);
            var accesToken = tokenHandler.WriteToken(securyToken);

            var dto = new LoginResponseDto
            {
                AccessToken = accesToken,
                Email = loginDto.Email,
                Role = user.IdRole,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            };

            return Result<LoginResponseDto>.Ok(dto);
        }
    }
}
