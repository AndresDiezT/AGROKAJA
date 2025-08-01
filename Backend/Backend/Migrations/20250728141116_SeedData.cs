using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "IdEmployee",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 14, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 15, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 16, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 17, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 18, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 19, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 20, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 21, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 22, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 23, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 24, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 25, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 26, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 27, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 28, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 29, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 30, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 31, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 32, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 33, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 34, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 35, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 36, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 37, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 38, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 39, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 40, 1 });

            migrationBuilder.DeleteData(
                table: "RolesHasPermissions",
                keyColumns: new[] { "IdPermission", "IdRole" },
                keyValues: new object[] { 41, 1 });

            migrationBuilder.DeleteData(
                table: "UserHasRoles",
                keyColumns: new[] { "IdRole", "UserDocument" },
                keyValues: new object[] { 1, "0000000000" });

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "IdCity",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "IdPermission",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRole",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Document",
                keyValue: "0000000000");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "IdDepartment",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TypesDocument",
                keyColumn: "IdTypeDocument",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "IdCountry",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "IdCountry", "CodeCountry", "CreatedAt", "DeactivatedAt", "IsActive", "NameCountry", "UpdatedAt" },
                values: new object[] { 1, "CO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Colombia", null });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "IdPermission", "CreatedAt", "DescriptionPermission", "GuardNamePermission", "ModulePermission", "NamePermission", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Detalles", "web", "Usuarios", "admin.users.details", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Actualizar", "web", "Usuarios", "admin.users.update", null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Activar", "web", "Usuarios", "admin.users.active", null },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Desactivar", "web", "Usuarios", "admin.users.deactive", null },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Crear Empleado", "web", "Usuarios", "admin.employees.create", null },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reenviar Credenciales", "web", "Usuarios", "admin.employees.resendCredentials", null },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cambiar Contraseña", "web", "Usuarios", "admin.employees.resetPassword", null },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ver Empleados", "web", "Usuarios", "admin.employees.read", null },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ver Clientes", "web", "Usuarios", "admin.customers.read", null },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Acceso al dashboard general", "web", "Dashboard", "admin.dashboard.access", null },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ver", "web", "Tipos de Documento", "admin.typesDocument.read", null },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Detalles", "web", "Tipos de Documento", "admin.typesDocument.details", null },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Actualizar", "web", "Tipos de Documento", "admin.typesDocument.update", null },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Activar", "web", "Tipos de Documento", "admin.typesDocument.active", null },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Desactivar", "web", "Tipos de Documento", "admin.typesDocument.deactive", null },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ver", "web", "Localización", "admin.countries.read", null },
                    { 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Detalles", "web", "Localización", "admin.countries.details", null },
                    { 18, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Actualizar", "web", "Localización", "admin.countries.update", null },
                    { 19, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Activar", "web", "Localización", "admin.countries.active", null },
                    { 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Desactivar", "web", "Localización", "admin.countries.deactive", null },
                    { 21, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ver", "web", "Localización", "admin.departments.read", null },
                    { 22, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Detalles", "web", "Localización", "admin.departments.details", null },
                    { 23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Actualizar", "web", "Localización", "admin.departments.update", null },
                    { 24, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Activar", "web", "Localización", "admin.departments.active", null },
                    { 25, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Desactivar", "web", "Localización", "admin.departments.deactive", null },
                    { 26, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ver", "web", "Localización", "admin.cities.read", null },
                    { 27, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Detalles", "web", "Localización", "admin.cities.details", null },
                    { 28, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Actualizar", "web", "Localización", "admin.cities.update", null },
                    { 29, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Activar", "web", "Localización", "admin.cities.active", null },
                    { 30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Desactivar", "web", "Localización", "admin.cities.deactive", null },
                    { 31, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ver", "web", "Roles", "admin.roles.read", null },
                    { 32, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Detalles", "web", "Roles", "admin.roles.details", null },
                    { 33, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Actualizar", "web", "Roles", "admin.roles.update", null },
                    { 34, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Activar", "web", "Roles", "admin.roles.active", null },
                    { 35, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Desactivar", "web", "Roles", "admin.roles.deactive", null },
                    { 36, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ver", "web", "Tipos de Documento", "common.typesDocument.read", null },
                    { 37, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ver", "web", "Localización", "common.countries.read", null },
                    { 38, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ver", "web", "Localización", "common.departments.read", null },
                    { 39, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ver", "web", "Localización", "common.cities.read", null },
                    { 40, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ver", "web", "Roles", "common.roles.read", null },
                    { 41, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ver", "web", "Usuarios", "common.profile.access", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "IdRole", "CreatedAt", "DeactivatedAt", "EditableRole", "GuardNameRole", "HierarchyRole", "IsActive", "NameRole", "UpdatedAt" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "web", 1, true, "Administrador", null });

            migrationBuilder.InsertData(
                table: "TypesDocument",
                columns: new[] { "IdTypeDocument", "CreatedAt", "DeactivatedAt", "IsActive", "NameTypeDocument", "UpdatedAt" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "CC", null });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "IdDepartment", "CreatedAt", "DeactivatedAt", "IdCountry", "IsActive", "NameDepartment", "UpdatedAt" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, false, "Cundinamarca", null });

            migrationBuilder.InsertData(
                table: "RolesHasPermissions",
                columns: new[] { "IdPermission", "IdRole" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 17, 1 },
                    { 18, 1 },
                    { 19, 1 },
                    { 20, 1 },
                    { 21, 1 },
                    { 22, 1 },
                    { 23, 1 },
                    { 24, 1 },
                    { 25, 1 },
                    { 26, 1 },
                    { 27, 1 },
                    { 28, 1 },
                    { 29, 1 },
                    { 30, 1 },
                    { 31, 1 },
                    { 32, 1 },
                    { 33, 1 },
                    { 34, 1 },
                    { 35, 1 },
                    { 36, 1 },
                    { 37, 1 },
                    { 38, 1 },
                    { 39, 1 },
                    { 40, 1 },
                    { 41, 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Document", "BirthDate", "CountryCode", "CreatedAt", "DeactivatedAt", "Email", "EmailIsVerified", "FirstName", "IdTypeDocument", "IsActive", "LastLogin", "LastLoginIp", "LastName", "PasswordHash", "PhoneIsVerified", "PhoneNumber", "ProfileImage", "UpdatedAt", "Username" },
                values: new object[] { "0000000000", new DateOnly(1, 1, 1), "+57", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "god@agrokaja.com", true, "Administrador", 1, true, null, null, "Agrokaja", "$2a$12$3PnYBDSegIGdKAis95YfCuobDqpXYsY965sjCo7FxVXFYoi/djLcG", true, "3000000000", "https://www.istockphoto.com/photo/resurrected-jesus-christ-ascending-above-the-sky-and-clouds-heaven-concept-gm1464383776-497090261", null, "GOD" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "IdCity", "CreatedAt", "DeactivatedAt", "IdDepartment", "IsActive", "NameCity", "UpdatedAt" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, false, "Bogota", null });

            migrationBuilder.InsertData(
                table: "UserHasRoles",
                columns: new[] { "IdRole", "UserDocument" },
                values: new object[] { 1, "0000000000" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "IdEmployee", "Document", "HireDate", "IdCity", "Salary" },
                values: new object[] { 1, "0000000000", new DateOnly(2024, 7, 18), 1, 0m });
        }
    }
}
