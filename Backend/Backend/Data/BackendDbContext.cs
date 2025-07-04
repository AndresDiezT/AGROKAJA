﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("Roles");

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<TypeDocument>().ToTable("TypesDocument");

            modelBuilder.Entity<Country>().ToTable("Countries");

            modelBuilder.Entity<Department>().ToTable("Departments");

            modelBuilder.Entity<City>().ToTable("Cities");

            modelBuilder.Entity<Address>().ToTable("Addresses");
        }
    }
}