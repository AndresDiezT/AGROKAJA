

using Microsoft.EntityFrameworkCore;
using RoleService.Api.Models;

namespace RoleService.Api.Data
{
    public class RolesDbContext : DbContext
    {
        public RolesDbContext(DbContextOptions<RolesDbContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("Roles");
        }
    }
}
