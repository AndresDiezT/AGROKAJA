using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class DevelopService
    {
        private readonly BackendDbContext _context;

        public DevelopService(BackendDbContext context)
        {
            _context = context;
        }

        public async Task ResetDataAsync()
        {
            var tables = new[]
            {
                "PasswordResetTokens",
                "EmailVerifications",
                "Customers",
                "Employees",
                "Users",
                "Roles",
                "Permissions",
                "RolePermissions"
                // Agrega aquí todas tus tablas que quieres limpiar
            };

            foreach (var table in tables)
            {
                await _context.Database.ExecuteSqlRawAsync($"DELETE FROM [{table}];");
                await _context.Database.ExecuteSqlRawAsync($"DBCC CHECKIDENT ('[{table}]', RESEED, 0);");
            }

            // 📝 Re-aplicar los datos seed (HasData)
            await _context.Database.MigrateAsync();
        }
    }
}
