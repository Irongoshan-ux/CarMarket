﻿using CarMarket.Core.Car.Domain;
using CarMarket.Core.Image.Domain;
using CarMarket.Core.User.Domain;
using CarMarket.Data.Car.Domain;
using CarMarket.Data.Configuration;
using CarMarket.Data.User.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ImageModel> Images { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new BrandsConfiguration());

            InitializeUserTable(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void InitializeUserTable(ModelBuilder modelBuilder)
        {
            string adminEmail = "admin@gmail.com";
            string adminPassword = "c54cd4083d0e3b7625cd3b8c652a2537"; // qwe

            string userEmail = "user@gmail.com";
            string userPassword = "c54cd4083d0e3b7625cd3b8c652a2537"; // qwe

            UserEntity admin = new()
            {
                Id = "qwe",
                Email = adminEmail,
                UserName = "adminUser",
                FirstName = "Admin",
                LastName = "User",
                PasswordHash = adminPassword
            };
            UserEntity user = new()
            {
                Id = "qwerty",
                Email = userEmail,
                UserName = "userUser",
                FirstName = "Basic",
                LastName = "User",
                PasswordHash = userPassword
            };

            modelBuilder.Entity<UserEntity>().HasData(new UserEntity[] { admin, user });
        }
    }
}
