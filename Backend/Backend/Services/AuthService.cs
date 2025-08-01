using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.AddressDTOs;
using Backend.DTOs.RoleDTOs;
using Backend.DTOs.UserDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly BackendDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(BackendDbContext context, IEmailService emailService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<string>> RegisterAsync(RegisterDto registerDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Validaciones unificadas
                bool emailExists = await _context.Users.AnyAsync(u => u.Email == registerDto.Email);
                bool documentExists = await _context.Users.AnyAsync(u => u.Document == registerDto.Document);
                bool phoneExists = await _context.Users.AnyAsync(u => u.PhoneNumber == registerDto.PhoneNumber);

                if (emailExists) return Result<string>.Fail("El correo ya está registrado");
                if (documentExists) return Result<string>.Fail("El número de documento ya está registrado");
                if (phoneExists) return Result<string>.Fail("El numero de telefono ya está registrado");

                var allowedRoles = new[] { "Agricultor", "Comprador" };
                var role = await _context.Roles
                    .FirstOrDefaultAsync(r => r.IdRole == registerDto.IdRole && allowedRoles.Contains(r.NameRole));

                if (role == null)
                    return Result<string>.Fail("El rol seleccionado no es válido, solo se permite Agricultor o Comprador.");

                // Generar Username
                var firstName = registerDto.FirstName.Split(' ')[0];
                var lastName = registerDto.LastName.Split(' ')[0];

                var baseUsername = $"{firstName}{lastName}".ToLower().Replace(" ", "");
                baseUsername += registerDto.Document[^3..]; // Últimos 3 dígitos del documento

                var username = baseUsername;
                int suffix = 1;

                while (await _context.Users.AnyAsync(u => u.Username == username))
                {
                    username = $"{baseUsername}{suffix}";
                    suffix++;
                }

                // Hash de la contraseña
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

                var newUser = new User
                {
                    Document = registerDto.Document,
                    Username = username,
                    Email = registerDto.Email,
                    PasswordHash = hashedPassword,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    CountryCode = registerDto.CountryCode,
                    PhoneNumber = registerDto.PhoneNumber,
                    BirthDate = registerDto.BirthDate,
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                    IdTypeDocument = registerDto.IdTypeDocument
                };

                var newCustomer = new Customer
                {
                    Document = newUser.Document
                };

                _context.Customers.Add(newCustomer);
                _context.Users.Add(newUser);

                await _context.SaveChangesAsync();

                // Generar y enviar el código de verificación por correo
                var emailResult = await GenerateEmailVerifyAsync(newUser);
                if (!emailResult.Success)
                {
                    await transaction.RollbackAsync();
                    return Result<string>.Fail("El usuario se ha creado exitosamente, pero hubo un problema al enviar el correo de verificación");
                }

                await transaction.CommitAsync();
                return Result<string>.Ok("Usuario registrado exitosamente, se ha enviado un correo de verificación");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result<string>.Fail("Hubo un problema al agregar el usuario, intentelo mas tarde");
            }
        }

        public async Task<Result<string>> AddEmployeeAsync(AddEmployeeDto addEmployeeDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Validaciones unificadas
                bool emailExists = await _context.Users.AnyAsync(u => u.Email == addEmployeeDto.Email);
                bool documentExists = await _context.Users.AnyAsync(u => u.Document == addEmployeeDto.Document);
                bool phoneExists = await _context.Users.AnyAsync(u => u.PhoneNumber == addEmployeeDto.PhoneNumber);

                if (emailExists) return Result<string>.Fail("El correo ya está registrado");
                if (documentExists) return Result<string>.Fail("El número de documento ya está registrado");
                if (phoneExists) return Result<string>.Fail("El numero de telefono ya está registrado");

                // Generar Username
                var firstName = addEmployeeDto.FirstName.Split(' ')[0];
                var lastName = addEmployeeDto.LastName.Split(' ')[0];

                var baseUsername = $"{firstName}{lastName}".ToLower().Replace(" ", "");
                baseUsername += addEmployeeDto.Document[^3..]; // Últimos 3 dígitos del documento

                var username = baseUsername;
                int suffix = 1;

                while (await _context.Users.AnyAsync(u => u.Username == username))
                {
                    username = $"{baseUsername}{suffix}";
                    suffix++;
                }

                //Generar Password
                var birthYear = addEmployeeDto.BirthDate.Year;
                var rawPassword = firstName.Substring(0, Math.Min(3, firstName.Length)).ToLower() + birthYear.ToString() + "@123";

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(rawPassword);

                var roles = await _context.Roles
                    .Where(r => addEmployeeDto.IdRoles.Contains(r.IdRole))
                    .ToListAsync();

                // Validar que todos los roles existen
                if (roles.Count != addEmployeeDto.IdRoles.Count)
                    return Result<string>.Fail("Uno o más roles enviados no existen.");

                var newUser = new User
                {
                    Document = addEmployeeDto.Document,
                    Username = username,
                    Email = addEmployeeDto.Email,
                    PasswordHash = hashedPassword,
                    FirstName = addEmployeeDto.FirstName,
                    LastName = addEmployeeDto.LastName,
                    CountryCode = addEmployeeDto.CountryCode,
                    PhoneNumber = addEmployeeDto.PhoneNumber,
                    BirthDate = addEmployeeDto.BirthDate,
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                    IdTypeDocument = addEmployeeDto.IdTypeDocument,
                    EmailIsVerified = true,
                    PhoneIsVerified = true,
                    // Asociar roles al usuario
                    Roles = roles
                };

                var newEmployee = new Employee
                {
                    Salary = addEmployeeDto.Salary,
                    HireDate = addEmployeeDto.HireDate,
                    Document = newUser.Document,
                    IdCity = addEmployeeDto.IdCity
                };

                _context.Users.Add(newUser);
                _context.Employees.Add(newEmployee);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                try
                {
                    //Enviar correo al empleado con sus credenciales
                    var emailSubject = "Bienvenido a la empresa - Tus credenciales de acceso";
                    var emailBody = $@"
                    <h2>Hola {firstName} {lastName}, bienvenido a la empresa 👋 AGROKAJA S.A.S</h2>
                    <p>Hemos creado una cuenta para ti en nuestro sistema.</p>
                    <p><strong>Utiliza tu correo y la siguiente contraseña:</strong></p>
                    <p><strong>Contraseña temporal:</strong> {rawPassword}</p>
                    <p>Con estos datos podras iniciar sesión con tu cuenta</p>
                    <p>👉 <a href='http://localhost:5173/login'>Haz clic aquí para iniciar sesión</a></p>
                    <br/>
                    <p>Saludos,</p>
                    <p>El equipo de Recursos Humanos</p>";

                    await _emailService.SendEmailAsync(newUser.Email, emailSubject, emailBody);
                }
                catch (Exception ex)
                {
                    // Manejar el error de envío de correo
                    return Result<string>.Fail("El empleado se ha creado exitosamente, pero hubo un problema al enviar el correo con las credenciales");
                }

                return Result<string>.Ok("Empleado subido exitosamente, se ha envíado el correo con las credenciales");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result<string>.Fail("Hubo un problema al agregar el empleado, intentelo mas tarde");
            }
        }

        public async Task<Result<string>> ResendCredentialsAsync(string document)
        {
            var user = await _context.Users.FindAsync(document);

            if (user == null)
                return Result<string>.Fail("Usuario no encontrado.");

            if (!user.IsActive)
                return Result<string>.Fail("No se pueden enviar credenciales a un usuario inactivo.");

            // Generar nueva contraseña temporal
            var firstName = user.FirstName.Split(' ')[0];
            var birthYear = user.BirthDate.Year;
            var newRawPassword = $"{firstName[..Math.Min(3, firstName.Length)].ToLower()}{birthYear}@123";

            var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(newRawPassword);

            // Actualizar contraseña en la base de datos
            user.PasswordHash = newHashedPassword;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // Construir y enviar el correo con las credenciales
            var emailSubject = "Reenvío de credenciales - AGROKAJA S.A.S";
            var emailBody = $@"
                <h2>Hola {user.FirstName} 👋</h2>
                <p>Como solicitaste, aquí están tus nuevas credenciales de acceso:</p>
                <p><strong>Correo:</strong> {user.Email}</p>
                <p><strong>Contraseña temporal:</strong> {newRawPassword}</p>
                <p>👉 <a href='http://localhost:5173/login'>Haz clic aquí para iniciar sesión</a></p>
                <br/>
                <p>Por seguridad, al ingresar deberás cambiar esta contraseña temporal.</p>
                <br/>
                <p>Saludos,<br/>El equipo de Recursos Humanos</p>";

            await _emailService.SendEmailAsync(user.Email, emailSubject, emailBody);

            return Result<string>.Ok("Se ha enviado un correo con nuevas credenciales.");
        }

        private async Task<Result<string>> GenerateEmailVerifyAsync(User user)
        {
            // Verificar si el usuario ya tiene un código de verificación activo
            var existingCode = await _context.EmailVerifications
                .Where(ev => ev.UserDocument == user.Document && !ev.IsUsed && ev.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(ev => ev.CreatedAt)
                .FirstOrDefaultAsync();

            if (existingCode != null)
            {
                // Marcar el código actual como usado para invalidarlo
                existingCode.IsUsed = true;
                _context.EmailVerifications.Update(existingCode);
                await _context.SaveChangesAsync();
            }

            // Generar un código de verificación aleatorio de 6 digitos
            var random = new Random();
            var code = random.Next(100000, 999999);

            var emailVerification = new EmailVerification
            {
                UserDocument = user.Document,
                Code = code,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30), // El token expira en 30 minutos
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };

            await _context.EmailVerifications.AddAsync(emailVerification);
            await _context.SaveChangesAsync();

            // Enviar el código de verificación al correo del usuario
            var message = $"Tu código de verificación es: {code}. Este codigo es válido por 30 minutos.";
            await _emailService.SendEmailAsync(user.Email, "Código de verificación", message);

            return Result<string>.Ok("Código de verificación enviado al correo.");
        }

        public async Task<Result<string>> VerifyEmailAsync(string document, int code)
        {
            var verification = await _context.EmailVerifications
                .Include(ev => ev.User)
                .Where(ev => ev.UserDocument == document && ev.Code == code && !ev.IsUsed)
                .OrderByDescending(ev => ev.CreatedAt)
                .FirstOrDefaultAsync();

            if (verification == null)
            {
                return Result<string>.Fail("Código de verificación inválido o ya utilizado");
            }
            if (verification.ExpiresAt < DateTime.UtcNow)
            {
                return Result<string>.Fail("El código de verificación ha expirado");
            }

            // Marcar el código como utilizado
            verification.IsUsed = true;
            verification.User.EmailIsVerified = true;

            _context.EmailVerifications.Update(verification);
            await _context.SaveChangesAsync();

            return Result<string>.Ok("Correo electrónico verificado exitosamente");
        }

        /// <summary>
        /// Autentica a un usuario y genera un token JWT de acceso y un refresh token
        /// </summary>
        public async Task<Result<LoginResponseDto>> LoginAsync(LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                return Result<LoginResponseDto>.Fail("Todos los campos son obligatorios");

            var user = await _context.Users
                .Include(u => u.Roles)
                    .ThenInclude(r => r.Permissions)
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user is null)
                return Result<LoginResponseDto>.Fail("No hay un usuario registrado con este correo");

            // Verificar si el usuario tiene el correo verificado
            if (!user.EmailIsVerified)
            {
                await GenerateEmailVerifyAsync(user);

                return Result<LoginResponseDto>.FailWithData(
                    "Debes confirmar tu correo electrónico. Se ha enviado un código de verificación.",
                    new { requiresVerification = true, document = user.Document } // Esto ayuda al frontend
                );
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return Result<LoginResponseDto>.Fail("Correo o contraseña incorrecta");

            // Generar el token de acceso y Refresh token
            var accesToken = GenerateAccessToken(user);

            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserDocument = user.Document,
                ExpireAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };

            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync();

            // Establecer la cookie del refresh token
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = refreshTokenEntity.ExpireAt
            });

            var dto = new LoginResponseDto
            {
                AccessToken = accesToken
            };

            return Result<LoginResponseDto>.Ok(dto);
        }

        public async Task<Result<string>> GeneratePasswordResetTokenAsync(string email)
        {
            // Buscar al usuario por email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return Result<string>.Fail("No existe un usuario registrado con este correo.");

            // Invalidar tokens anteriores no usados
            var previousTokens = await _context.PasswordResetTokens
                .Where(t => t.UserDocument == user.Document && !t.IsUsed && t.Expiration > DateTime.UtcNow)
                .ToListAsync();

            // Marcar los tokens anteriores como usados
            foreach (var token in previousTokens)
            {
                token.IsUsed = true;
                _context.PasswordResetTokens.Update(token);
            }

            await _context.SaveChangesAsync();

            // Generar nuevo token (GUID seguro)
            var tokenValue = Guid.NewGuid().ToString();

            var resetToken = new PasswordResetToken
            {
                Token = tokenValue,
                Expiration = DateTime.UtcNow.AddMinutes(30), // Expira en 30 minutos
                IsUsed = false,
                UserDocument = user.Document,
                CreatedAt = DateTime.UtcNow
            };

            await _context.PasswordResetTokens.AddAsync(resetToken);
            await _context.SaveChangesAsync();

            // Enviar email con link para reset
            var resetLink = $"http://localhost:5173/reset-password?token={tokenValue}";
            var subject = "Restablecer tu contraseña";
            var body = $@"
                <h2>Hola {user.FirstName},</h2>
                <p>Haz click en el siguiente enlace para restablecer tu contraseña:</p>
                <p><a href='{resetLink}'>Restablecer contraseña</a></p>
                <p>El enlace expira en 30 minutos.</p>
                <br/>
                <p>Si no solicitaste este cambio, ignora este correo.</p>";

            await _emailService.SendEmailAsync(user.Email, subject, body);

            return Result<string>.Ok("Se ha enviado un enlace para restablecer la contraseña a tu correo.");
        }

        public async Task<Result<string>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            // Buscar token en la base de datos
            var resetToken = await _context.PasswordResetTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Token == resetPasswordDto.Token);

            if (resetToken == null)
                return Result<string>.Fail("Token inválido.");

            if (resetToken.IsUsed)
                return Result<string>.Fail("Este enlace ya ha sido utilizado.");

            if (resetToken.Expiration < DateTime.UtcNow)
                return Result<string>.Fail("El enlace ha expirado.");

            // Hash de la nueva contraseña
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);

            // Actualizar contraseña del usuario
            resetToken.User.PasswordHash = hashedPassword;

            // Marcar token como usado
            resetToken.IsUsed = true;

            _context.Users.Update(resetToken.User);
            _context.PasswordResetTokens.Update(resetToken);

            await _context.SaveChangesAsync();

            return Result<string>.Ok("La contraseña se ha restablecido correctamente.");
        }

        /// <summary>
        /// Genera un nuevo access token usando el refresh token almacenado en cookie
        /// </summary>
        public async Task<Result<ProfileDto>> GetProfileAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext?.User?.Identity?.IsAuthenticated != true)
            {
                return Result<ProfileDto>.Fail("Usuario no autenticado");
            }

            var document = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? httpContext?.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
            ?? httpContext?.User?.FindFirst("document")?.Value;

            if (string.IsNullOrEmpty(document))
            {
                return Result<ProfileDto>.Fail("No se pudo obtener el documento del usuario autenticado");
            }

            var profile = await _context.Users
                .Where(u => u.Document == document)
                .Select(u => new ProfileDto
                {
                    Document = u.Document,
                    Username = u.Username,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    BirthDate = u.BirthDate,
                    Roles = u.Roles.Select(r => new ReadRoleDto
                    {
                        IdRole = r.IdRole,
                        NameRole = r.NameRole
                    }).ToList(),
                    IdTypeDocument = u.IdTypeDocument,
                    NameTypeDocument = u.TypeDocument.NameTypeDocument,
                    EmailIsVerified = u.EmailIsVerified,
                    PhoneIsVerified = u.PhoneIsVerified,
                    ProfileImage = u.ProfileImage,
                    Addresses = u.Addresses
                    .Where(a => a.IsActive)
                    .Select(a => new ReadAddressDto
                    {
                        IdAddress = a.IdAddress,
                        StreetAddress = a.StreetAddress,
                        PostalCodeAddress = a.PostalCodeAddress,
                        IsDefaultAddress = a.IsDefaultAddress,
                        AddressReference = a.AddressReference,
                        PhoneNumber = a.PhoneNumber,
                        IdCity = a.IdCity,
                        NameCity = a.City.NameCity,
                        IdDepartment = a.City.Department.IdDepartment,
                        NameDepartment = a.City.Department.NameDepartment
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (profile is null)
            {
                return Result<ProfileDto>.Fail("Usuario no encontrado");
            }

            return Result<ProfileDto>.Ok(profile);
        }

        /// <summary>
        /// Genera un nuevo access token usando el refresh token almacenado en cookie
        /// </summary>
        public async Task<Result<LoginResponseDto>> RefreshTokenAsync()
        {
            // Obtener el refresh token de la cookie
            var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return Result<LoginResponseDto>.Fail("Refresh token no proporcionado");

            var storedToken = await _context.RefreshTokens
                .Include(rt => rt.User)
                    .ThenInclude(u => u.Roles)
                        .ThenInclude(r => r.Permissions)
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (storedToken == null || storedToken.ExpireAt < DateTime.UtcNow)
                return Result<LoginResponseDto>.Fail("Refresh token inválido o expirado");

            _context.RefreshTokens.Remove(storedToken);

            var newRefreshTokenValue = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var newRefreshToken = new RefreshToken
            {
                Token = newRefreshTokenValue,
                UserDocument = storedToken.UserDocument,
                ExpireAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };

            await _context.RefreshTokens.AddAsync(newRefreshToken);
            await _context.SaveChangesAsync();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = newRefreshToken.ExpireAt
            };
            // Actualizar la cookie del refresh token
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            var newAccessToken = GenerateAccessToken(storedToken.User);

            var dto = new LoginResponseDto
            {
                AccessToken = newAccessToken,
            };

            return Result<LoginResponseDto>.Ok(dto);
        }

        /// <summary>
        /// Elimina el refresh token y borra la cookie
        /// </summary>
        public async Task<Result<string>> LogoutAsync()
        {
            // Obtener el refresh token de la cookie
            var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            if (!string.IsNullOrEmpty(refreshToken))
            {
                var storedToken = await _context.RefreshTokens
                    .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

                if (storedToken != null)
                {
                    _context.RefreshTokens.Remove(storedToken);
                    await _context.SaveChangesAsync();
                }
                // Eliminar la cookie del refresh token
                _httpContextAccessor.HttpContext?.Response.Cookies.Delete("refreshToken");
            }

            return Result<string>.Ok("Sesión cerrada correctamente");
        }

        /// <summary>
        /// Genera un access token JWT para el usuario
        /// </summary>
        private string GenerateAccessToken(User user)
        {
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            // Cargar roles y permisos del usuario
            var roles = user.Roles.Select(r => r.NameRole).ToList();
            var permissions = user.Roles
                .SelectMany(r => r.Permissions)
                .Select(p => p.NamePermission)
                .Distinct()
                .ToList();

            Console.WriteLine("Roles cargados:");
            foreach (var role in roles)
            {
                Console.WriteLine($" - {role}");
            }

            Console.WriteLine("Permisos cargados:");
            foreach (var permission in permissions)
            {
                Console.WriteLine($" - {permission}");
            }
            // Crear los claims del token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Document),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("username", user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Agregar roles y permisos como claims
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            claims.AddRange(permissions.Select(permission => new Claim("permission", permission)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    SecurityAlgorithms.HmacSha512Signature),
            };

            // Crear el token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var securyToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securyToken);
        }
    }
}