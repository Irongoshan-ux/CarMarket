using CarMarket.Core.Car.Domain;
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
        public DbSet<CarImage> CarImages { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PermissonConfiguration());

            InitializeUserTable(modelBuilder);
        }

        private void InitializeUserTable(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@gmail.com";
            string adminPassword = "admin";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            UserEntity adminUser = new UserEntity { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<UserEntity>().HasData(new UserEntity[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
