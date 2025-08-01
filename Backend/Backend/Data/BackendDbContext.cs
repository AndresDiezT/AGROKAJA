using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class BackendDbContext : DbContext
    {
        // Constructor, le dice a Entity Framework como debe conectarse(cadena conexion, proveedor SQL, etc)
        public BackendDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
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
            modelBuilder.Entity<Role>().ToTable("Roles");

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<TypeDocument>().ToTable("TypesDocument");

            modelBuilder.Entity<Category>().ToTable("Categories");

            modelBuilder.Entity<SubCategory>().ToTable("SubCategories");

            modelBuilder.Entity<Product>().ToTable("Products");

            modelBuilder.Entity<Presentation>().ToTable("Presentations");
            
            modelBuilder.Entity<Country>().ToTable("Countries");

            modelBuilder.Entity<Department>().ToTable("Departments");

            modelBuilder.Entity<City>().ToTable("Cities");

            modelBuilder.Entity<Address>().ToTable("Addresses");

            modelBuilder.Entity<Comission>().ToTable("Comissions");
            modelBuilder.Entity<Invoice>().ToTable("Invoices");
            modelBuilder.Entity<PaymentStatus>().ToTable("PaymentStatuses");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderStatus>().ToTable("OrderStatus");
        }
    }
}