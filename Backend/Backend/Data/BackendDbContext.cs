using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class BackendDbContext : DbContext
    {
        public BackendDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TypeDocument> TypesDocument { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Presentation> Presentations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("Roles");

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<TypeDocument>().ToTable("TypesDocument");

            modelBuilder.Entity<Category>().ToTable("Categories");

            modelBuilder.Entity<SubCategory>().ToTable("SubCategories");

            modelBuilder.Entity<Product>().ToTable("Products");

            modelBuilder.Entity<Presentation>().ToTable("Presentations");
        }
    }
}