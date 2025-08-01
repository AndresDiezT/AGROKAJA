using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCountry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodeCountry = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeactivatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.IdCountry);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    IdPermission = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamePermission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescriptionPermission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModulePermission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GuardNamePermission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.IdPermission);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HierarchyRole = table.Column<int>(type: "int", nullable: false),
                    GuardNameRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditableRole = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeactivatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "TypesDocument",
                columns: table => new
                {
                    IdTypeDocument = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameTypeDocument = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeactivatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesDocument", x => x.IdTypeDocument);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    IdDepartment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameDepartment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeactivatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.IdDepartment);
                    table.ForeignKey(
                        name: "FK_Departments_Countries_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Countries",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesHasPermissions",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false),
                    IdPermission = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesHasPermissions", x => new { x.IdRole, x.IdPermission });
                    table.ForeignKey(
                        name: "FK_RolesHasPermissions_Permissions_IdPermission",
                        column: x => x.IdPermission,
                        principalTable: "Permissions",
                        principalColumn: "IdPermission",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesHasPermissions_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Document = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailIsVerified = table.Column<bool>(type: "bit", nullable: false),
                    PhoneIsVerified = table.Column<bool>(type: "bit", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeactivatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTypeDocument = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Document);
                    table.ForeignKey(
                        name: "FK_Users_TypesDocument_IdTypeDocument",
                        column: x => x.IdTypeDocument,
                        principalTable: "TypesDocument",
                        principalColumn: "IdTypeDocument",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    IdCity = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeactivatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdDepartment = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.IdCity);
                    table.ForeignKey(
                        name: "FK_Cities_Departments_IdDepartment",
                        column: x => x.IdDepartment,
                        principalTable: "Departments",
                        principalColumn: "IdDepartment",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    IdCustomer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalSales = table.Column<int>(type: "int", nullable: false),
                    TotalPurchases = table.Column<int>(type: "int", nullable: false),
                    ReputationScore = table.Column<int>(type: "int", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.IdCustomer);
                    table.ForeignKey(
                        name: "FK_Customers_Users_Document",
                        column: x => x.Document,
                        principalTable: "Users",
                        principalColumn: "Document",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailVerifications",
                columns: table => new
                {
                    IdEmailVerification = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserDocument = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailVerifications", x => x.IdEmailVerification);
                    table.ForeignKey(
                        name: "FK_EmailVerifications_Users_UserDocument",
                        column: x => x.UserDocument,
                        principalTable: "Users",
                        principalColumn: "Document",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordResetTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserDocument = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetTokens", x => x.Token);
                    table.ForeignKey(
                        name: "FK_PasswordResetTokens_Users_UserDocument",
                        column: x => x.UserDocument,
                        principalTable: "Users",
                        principalColumn: "Document",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneVerifications",
                columns: table => new
                {
                    IdPhoneVerification = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserDocument = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneVerifications", x => x.IdPhoneVerification);
                    table.ForeignKey(
                        name: "FK_PhoneVerifications_Users_UserDocument",
                        column: x => x.UserDocument,
                        principalTable: "Users",
                        principalColumn: "Document",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    IdRefreshToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserDocument = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.IdRefreshToken);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserDocument",
                        column: x => x.UserDocument,
                        principalTable: "Users",
                        principalColumn: "Document",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserHasRoles",
                columns: table => new
                {
                    UserDocument = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    IdRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHasRoles", x => new { x.UserDocument, x.IdRole });
                    table.ForeignKey(
                        name: "FK_UserHasRoles_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserHasRoles_Users_UserDocument",
                        column: x => x.UserDocument,
                        principalTable: "Users",
                        principalColumn: "Document",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    IdAddress = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PostalCodeAddress = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDefaultAddress = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeactivatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserDocument = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    IdCity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.IdAddress);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_IdCity",
                        column: x => x.IdCity,
                        principalTable: "Cities",
                        principalColumn: "IdCity",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserDocument",
                        column: x => x.UserDocument,
                        principalTable: "Users",
                        principalColumn: "Document",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    IdEmployee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HireDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    IdCity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.IdEmployee);
                    table.ForeignKey(
                        name: "FK_Employees_Cities_IdCity",
                        column: x => x.IdCity,
                        principalTable: "Cities",
                        principalColumn: "IdCity",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Users_Document",
                        column: x => x.Document,
                        principalTable: "Users",
                        principalColumn: "Document",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "Document", "BirthDate", "CreatedAt", "DeactivatedAt", "Email", "EmailIsVerified", "FirstName", "IdTypeDocument", "IsActive", "LastLogin", "LastLoginIp", "LastName", "PasswordHash", "PhoneIsVerified", "PhoneNumber", "ProfileImage", "UpdatedAt", "Username" },
                values: new object[] { "0000000000", new DateOnly(1, 1, 1), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "god@agrokaja.com", true, "Administrador", 1, true, null, null, "Agrokaja", "$2a$12$3PnYBDSegIGdKAis95YfCuobDqpXYsY965sjCo7FxVXFYoi/djLcG", true, "3000000000", "https://www.istockphoto.com/photo/resurrected-jesus-christ-ascending-above-the-sky-and-clouds-heaven-concept-gm1464383776-497090261", null, "GOD" });

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

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_IdCity",
                table: "Addresses",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserDocument",
                table: "Addresses",
                column: "UserDocument");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IdDepartment",
                table: "Cities",
                column: "IdDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Document",
                table: "Customers",
                column: "Document");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_IdCountry",
                table: "Departments",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerifications_UserDocument",
                table: "EmailVerifications",
                column: "UserDocument");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Document",
                table: "Employees",
                column: "Document");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdCity",
                table: "Employees",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_UserDocument",
                table: "PasswordResetTokens",
                column: "UserDocument");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneVerifications_UserDocument",
                table: "PhoneVerifications",
                column: "UserDocument");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserDocument",
                table: "RefreshToken",
                column: "UserDocument");

            migrationBuilder.CreateIndex(
                name: "IX_RolesHasPermissions_IdPermission",
                table: "RolesHasPermissions",
                column: "IdPermission");

            migrationBuilder.CreateIndex(
                name: "IX_UserHasRoles_IdRole",
                table: "UserHasRoles",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdTypeDocument",
                table: "Users",
                column: "IdTypeDocument");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "EmailVerifications");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "PasswordResetTokens");

            migrationBuilder.DropTable(
                name: "PhoneVerifications");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "RolesHasPermissions");

            migrationBuilder.DropTable(
                name: "UserHasRoles");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "TypesDocument");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
