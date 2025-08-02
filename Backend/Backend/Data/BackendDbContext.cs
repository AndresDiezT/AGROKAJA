using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Newtonsoft.Json;

namespace Backend.Data
{
    public class BackendDbContext : DbContext
    {
        // Constructor, le dice a Entity Framework como debe conectarse(cadena conexion, proveedor SQL, etc)
        public BackendDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserHasRoles { get; set; }
        public DbSet<RolePermission> RolesHasPermissions { get; set; }
        public DbSet<EmailVerification> EmailVerifications { get; set; }
        public DbSet<PhoneVerification> PhoneVerifications { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TypeDocument> TypesDocument { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Comission> Comissions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }

        public DbSet<PaymentStatus> PaymentStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Permission>().ToTable("Permissions");
            modelBuilder.Entity<RolePermission>().ToTable("RolesHasPermissions");
            modelBuilder.Entity<EmailVerification>().ToTable("EmailVerifications");
            modelBuilder.Entity<PhoneVerification>().ToTable("PhoneVerifications");
            modelBuilder.Entity<PasswordResetToken>().ToTable("PasswordResetTokens");
            modelBuilder.Entity<RefreshToken>().ToTable("RefreshToken");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<TypeDocument>().ToTable("TypesDocument");

            modelBuilder.Entity<Category>().ToTable("Categories");

            modelBuilder.Entity<SubCategory>().ToTable("SubCategories");

            modelBuilder.Entity<Product>().ToTable("Products");

            modelBuilder.Entity<Presentation>().ToTable("Presentations");

            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<Address>().ToTable("Addresses");

            // Configuración de claves primarias y relaciones
            modelBuilder.Entity<UserRole>()
                .ToTable("UserHasRoles")
                .HasKey(ur => new { ur.UserDocument, ur.IdRole });
            modelBuilder.Entity<RolePermission>()
                .ToTable("RolesHasPermissions")
                .HasKey(rp => new { rp.IdRole, rp.IdPermission });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRole>(
                join => join.ToTable("UserHasRoles")
            );

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermission>(
                join => join.ToTable("RolesHasPermissions")
            );
            modelBuilder.Entity<Comission>().ToTable("Comissions");
            modelBuilder.Entity<Invoice>().ToTable("Invoices");
            modelBuilder.Entity<PaymentStatus>().ToTable("PaymentStatuses");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderStatus>().ToTable("OrderStatus");

            // Relación User - Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserDocument)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Address)
                .WithMany()
                .HasForeignKey(o => o.IdAddress)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.OrderStatus)
                .WithMany()
                .HasForeignKey(o => o.IdOrderStatus)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Comission)
                .WithMany()
                .HasForeignKey(o => o.IdComission)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.PaymentMethod)
                .WithMany()
                .HasForeignKey(o => o.IdPaymentMethod)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.PaymentStatus)
                .WithMany()
                .HasForeignKey(o => o.IdPaymentStatus)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}