using CarMarket.Core.Image.Domain;
using CarMarket.Core.User.Domain;
using CarMarket.Data.Car.Domain;
using CarMarket.Data.Configuration;
using CarMarket.Data.User.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //public DbSet<UserEntity> Users { get; set; }
        public DbSet<CarEntity> Cars { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
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
            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            //modelBuilder.ApplyConfiguration(new UserModelConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            //modelBuilder.ApplyConfiguration(new UserClaimsConfiguration());
            //modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
            modelBuilder.ApplyConfiguration(new PermissonConfiguration());

            InitializeUserTable(modelBuilder);
        }

        private void InitializeUserTable(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@gmail.com";
            string adminPassword = "c54cd4083d0e3b7625cd3b8c652a2537"; // qwe

            string userEmail = "user@gmail.com";
            string userPassword = "c54cd4083d0e3b7625cd3b8c652a2537"; // qwe

            Role adminRole = new() { Id = 1, RoleName = adminRoleName };
            Role userRole = new() { Id = 2, RoleName = userRoleName };

            UserEntity admin = new() { Id = "qwe", Email = adminEmail, UserName = "adminUser", PasswordHash = adminPassword/*, RoleId = adminRole?.Id*/ };
            UserEntity user = new() { Id = "qwerty", Email = userEmail, UserName = "userUser", PasswordHash = userPassword/*, RoleId = userRole?.Id*/ };

            //modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<UserEntity>().HasData(new UserEntity[] { admin, user });
            base.OnModelCreating(modelBuilder);
        }
    }
}
