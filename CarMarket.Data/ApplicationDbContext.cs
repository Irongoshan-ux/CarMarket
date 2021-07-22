using CarMarket.Core.Image.Domain;
using CarMarket.Core.User.Domain;
using CarMarket.Data.Car.Domain;
using CarMarket.Data.Configuration;
using CarMarket.Data.User.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarMarket.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<Role> Roles { get; set; }
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
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PermissonConfiguration());

            //InitializeUserTable(modelBuilder);
        }

        //private void InitializeUserTable(ModelBuilder modelBuilder)
        //{
        //    string adminRoleName = "admin";
        //    string userRoleName = "user";

        //    string adminEmail = "admin@gmail.com";
        //    string adminPassword = "admin";

        //    string userEmail = "user@gmail.com";
        //    string userPassword = "user";

        //    Role adminRole = new() { Id = 1, RoleName = adminRoleName };
        //    Role userRole = new() { Id = 2, RoleName = userRoleName };

        //    UserEntity adminUser = new() { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };
        //    UserEntity defaultUser = new() { Id = 2, Email = userEmail, Password = userPassword, RoleId = userRole.Id };

        //    modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
        //    modelBuilder.Entity<UserEntity>().HasData(new UserEntity[] { adminUser, defaultUser });
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
