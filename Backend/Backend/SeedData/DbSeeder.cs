using Backend.Data;
using Backend.Models;
using Backend.Models.Seed;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Backend.SeedData
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(BackendDbContext context)
        {
            if (!context.Countries.Any())
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "colombia.min.json");
                if (!File.Exists(filePath))
                    throw new Exception($"No se encontró el archivo JSON en la ruta: {filePath}");
                var json = await File.ReadAllTextAsync(filePath);
                var departments = JsonSerializer.Deserialize<List<ColombiaJsonModel>>(json);

                if (departments == null || !departments.Any())
                    throw new Exception("El archivo JSON se cargó, pero está vacío o con formato incorrecto");

                var country = new Country
                {
                    NameCountry = "Colombia",
                    CodeCountry = "CO",
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };

                context.Countries.Add(country);
                await context.SaveChangesAsync();

                foreach (var dept in departments)
                {
                    if (string.IsNullOrWhiteSpace(dept.Departamento)) continue;

                    var department = new Department
                    {
                        NameDepartment = dept.Departamento,
                        IdCountry = country.IdCountry,
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    };
                    context.Departments.Add(department);
                    await context.SaveChangesAsync();

                    if (dept.Ciudades != null && dept.Ciudades.Any())
                    {
                        var cities = dept.Ciudades
                            .Where(city => !string.IsNullOrWhiteSpace(city))
                            .Select(city => new City
                            {
                                NameCity = city.Trim(),
                                IdDepartment = department.IdDepartment,
                                CreatedAt = DateTime.Now,
                                IsActive = true
                            });

                        context.Cities.AddRange(cities);
                        await context.SaveChangesAsync();
                    }
                }
            }

            // INSERTA ROLES SI NO HAY ROLES
            if (!context.Roles.Any())
            {
                var adminRole = new Role
                {
                    NameRole = "Administrador",
                    HierarchyRole = 1,
                    GuardNameRole = "web",
                    EditableRole = false,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };
                context.Roles.Add(adminRole);
                await context.SaveChangesAsync();

                // Seed permisos
                var permissions = new[]
                {
                // Permisos de Usuarios
                new Permission { NamePermission = "admin.users.details", DescriptionPermission = "Detalles", GuardNamePermission = "web", ModulePermission = "Usuarios" },
                new Permission { NamePermission = "admin.users.update", DescriptionPermission = "Actualizar", GuardNamePermission = "web", ModulePermission = "Usuarios" },
                new Permission { NamePermission = "admin.users.active", DescriptionPermission = "Activar", GuardNamePermission = "web", ModulePermission = "Usuarios" },
                new Permission { NamePermission = "admin.users.deactive", DescriptionPermission = "Desactivar", GuardNamePermission = "web", ModulePermission = "Usuarios" },

                // Permisos de Empleados y Clientes
                new Permission { NamePermission = "admin.employees.create", DescriptionPermission = "Crear Empleado", GuardNamePermission = "web", ModulePermission = "Usuarios" },
                new Permission { NamePermission = "admin.employees.resendCredentials", DescriptionPermission = "Reenviar Credenciales", GuardNamePermission = "web", ModulePermission = "Usuarios" },
                new Permission { NamePermission = "admin.employees.resetPassword", DescriptionPermission = "Cambiar Contraseña", GuardNamePermission = "web", ModulePermission = "Usuarios" },
                new Permission { NamePermission = "admin.employees.read", DescriptionPermission = "Ver Empleados", GuardNamePermission = "web", ModulePermission = "Usuarios" },
                new Permission { NamePermission = "admin.customers.read", DescriptionPermission = "Ver Clientes", GuardNamePermission = "web", ModulePermission = "Usuarios" },

                // Permisos de Dashboard
                new Permission { NamePermission = "admin.dashboard.access", DescriptionPermission = "Acceso al dashboard general", GuardNamePermission = "web", ModulePermission = "Dashboard" },

                // Permisos de Tipos de Documento
                new Permission { NamePermission = "admin.typesDocument.read", DescriptionPermission = "Ver", GuardNamePermission = "web", ModulePermission = "Tipos de Documento" },
                new Permission { NamePermission = "admin.typesDocument.details", DescriptionPermission = "Detalles", GuardNamePermission = "web", ModulePermission = "Tipos de Documento" },
                new Permission { NamePermission = "admin.typesDocument.create", DescriptionPermission = "Crear", GuardNamePermission = "web", ModulePermission = "Tipos de Documento" },
                new Permission { NamePermission = "admin.typesDocument.update", DescriptionPermission = "Actualizar", GuardNamePermission = "web", ModulePermission = "Tipos de Documento" },
                new Permission { NamePermission = "admin.typesDocument.active", DescriptionPermission = "Activar", GuardNamePermission = "web", ModulePermission = "Tipos de Documento" },
                new Permission { NamePermission = "admin.typesDocument.deactive", DescriptionPermission = "Desactivar", GuardNamePermission = "web", ModulePermission = "Tipos de Documento" },

                // Permisos de Locaciones
                new Permission { NamePermission = "admin.countries.read", DescriptionPermission = "Ver", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.countries.details", DescriptionPermission = "Detalles", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.countries.create", DescriptionPermission = "Crear", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.countries.update", DescriptionPermission = "Actualizar", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.countries.active", DescriptionPermission = "Activar", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.countries.deactive", DescriptionPermission = "Desactivar", GuardNamePermission = "web", ModulePermission = "Localización" },

                new Permission { NamePermission = "admin.departments.read", DescriptionPermission = "Ver", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.departments.details", DescriptionPermission = "Detalles", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.departments.create", DescriptionPermission = "Crear", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.departments.update", DescriptionPermission = "Actualizar", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.departments.active", DescriptionPermission = "Activar", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.departments.deactive", DescriptionPermission = "Desactivar", GuardNamePermission = "web", ModulePermission = "Localización" },

                new Permission { NamePermission = "admin.cities.read", DescriptionPermission = "Ver", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.cities.details", DescriptionPermission = "Detalles", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.cities.create", DescriptionPermission = "Crear", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.cities.update", DescriptionPermission = "Actualizar", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.cities.active", DescriptionPermission = "Activar", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "admin.cities.deactive", DescriptionPermission = "Desactivar", GuardNamePermission = "web", ModulePermission = "Localización" },

                // Permisos de Roles
                new Permission { NamePermission = "admin.roles.read", DescriptionPermission = "Ver", GuardNamePermission = "web", ModulePermission = "Roles" },
                new Permission { NamePermission = "admin.roles.details", DescriptionPermission = "Detalles", GuardNamePermission = "web", ModulePermission = "Roles" },
                new Permission { NamePermission = "admin.roles.create", DescriptionPermission = "Crear", GuardNamePermission = "web", ModulePermission = "Roles" },
                new Permission { NamePermission = "admin.roles.update", DescriptionPermission = "Actualizar", GuardNamePermission = "web", ModulePermission = "Roles" },
                new Permission { NamePermission = "admin.roles.active", DescriptionPermission = "Activar", GuardNamePermission = "web", ModulePermission = "Roles" },
                new Permission { NamePermission = "admin.roles.deactive", DescriptionPermission = "Desactivar", GuardNamePermission = "web", ModulePermission = "Roles" },

                // Permisos Comunes
                new Permission { NamePermission = "common.typesDocument.read", DescriptionPermission = "Ver", GuardNamePermission = "web", ModulePermission = "Tipos de Documento" },
                new Permission { NamePermission = "common.countries.read", DescriptionPermission = "Ver", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "common.departments.read", DescriptionPermission = "Ver", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "common.cities.read", DescriptionPermission = "Ver", GuardNamePermission = "web", ModulePermission = "Localización" },
                new Permission { NamePermission = "common.roles.read", DescriptionPermission = "Ver", GuardNamePermission = "web", ModulePermission = "Roles" },

                new Permission { NamePermission = "common.profile.access", DescriptionPermission = "Ver", GuardNamePermission = "web", ModulePermission = "Usuarios" },
                };

                context.Permissions.AddRange(permissions);
                await context.SaveChangesAsync();

                // Relaciona todos los permisos al rol admin
                context.RolesHasPermissions.AddRange(
                    permissions.Select(p => new RolePermission
                    {
                        IdRole = adminRole.IdRole,
                        IdPermission = p.IdPermission,
                    })
                );
                await context.SaveChangesAsync();
            }

            // INSERTA TIPOS DE DOCUMENTOS SI NO HAY
            if (!context.TypesDocument.Any())
            {
                var typeDocument = new TypeDocument
                {
                    NameTypeDocument = "CC",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                context.TypesDocument.Add(typeDocument);
                await context.SaveChangesAsync();
            }

            // INSERTA EL ADMINISTRADOR SI NO EXISTE
            if (!context.Users.Any())
            {
                var colombia = await context.Countries.FirstAsync(c => c.CodeCountry == "CO");
                var dabeiba = await context.Cities.FirstOrDefaultAsync(c => c.NameCity.Contains("Dabeiba"));
                var cedula = await context.TypesDocument.FirstOrDefaultAsync(c => c.NameTypeDocument.Contains("CC"));

                var user = new User
                {
                    Document = "0000000000",
                    Username = "GOD",
                    Email = "god@agrokaja.com",
                    PasswordHash = "$2a$12$3PnYBDSegIGdKAis95YfCuobDqpXYsY965sjCo7FxVXFYoi/djLcG", // god123
                    FirstName = "Administrador",
                    LastName = "Agrokaja",
                    CountryCode = "+57",
                    PhoneNumber = "3000000000",
                    ProfileImage = "data:image/jpeg;base64,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",
                    EmailIsVerified = true,
                    PhoneIsVerified = true,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    IdTypeDocument = cedula.IdTypeDocument
                };
                context.Users.Add(user);
                await context.SaveChangesAsync();

                context.Employees.Add(new Employee
                {
                    Document = user.Document,
                    HireDate = DateOnly.FromDateTime(DateTime.Now),
                    IdCity = dabeiba.IdCity,
                    Salary = 0
                });

                var adminRole = await context.Roles.FirstAsync(r => r.NameRole == "Administrador");
                context.UserHasRoles.Add(new UserRole
                {
                    IdRole = adminRole.IdRole,
                    UserDocument = user.Document
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
