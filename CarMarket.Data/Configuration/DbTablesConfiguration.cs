using CarMarket.Core.Car.Domain;
using CarMarket.Core.Image.Domain;
using CarMarket.Data.Car.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarMarket.Data.Configuration
{
    public class CarConfiguration : IEntityTypeConfiguration<CarEntity>
    {
        public void Configure(EntityTypeBuilder<CarEntity> builder)
        {
            builder.ToTable("Cars").HasKey(p => p.Id);
            //    .HasData(new CarEntity()
            //{
            //    Id = 1,
            //    Description = "The coolest Tesla Model S!",
            //    Price = 36500,
            //    Owner = new User.Domain.UserEntity { Id = "qwe"},
            //    Model = new Model { Id = 1 },
            //    Year = System.DateTime.Parse("2015-01-01")
            //},
            //new CarEntity()
            //{
            //    Id = 2,
            //    Description = "The coolest Tesla Model S!",
            //    Price = 43500,
            //    Owner = new User.Domain.UserEntity { Id = "qwerty" },
            //    Model = new Model { Id = 2 },
            //    Year = System.DateTime.Parse("2020-01-01 00:00:00.0000000")
            //},
            //new CarEntity()
            //{
            //    Id = 3,
            //    Description = "The coolest Tesla Model S!",
            //    Price = 59500,
            //    Owner = new User.Domain.UserEntity { Id = "4a083075-5fe6-4bb3-b9d5-0d3cde2bec30" },
            //    Model = new Model { Id = 3 },
            //    Year = System.DateTime.Parse("2000-01-01 00:00:00.0000000")
            //},
            //new CarEntity()
            //{
            //    Id = 4,
            //    Description = "The coolest Tesla Model S!",
            //    Price = 305739,
            //    Owner = new User.Domain.UserEntity { Id = "4a083075-5fe6-4bb3-b9d5-0d3cde2bec30" },
            //    Model = new Model { Id = 4 },
            //    Year = System.DateTime.Parse("2012-01-01 00:00:00.0000000")
            //},
            //new CarEntity()
            //{
            //    Id = 5,
            //    Description = "The coolest Tesla Model S!",
            //    Price = 17400,
            //    Owner = new User.Domain.UserEntity { Id = "qwe" },
            //    Model = new Model { Id = 5 },
            //    Year = System.DateTime.Parse("2016-01-01 00:00:00.0000000")
            //},
            //new CarEntity()
            //{
            //    Id = 6,
            //    Description = "The coolest Tesla Model S!",
            //    Price = 30000,
            //    Owner = new User.Domain.UserEntity { Id = "qwe" },
            //    Model = new Model { Id = 6 },
            //    Year = System.DateTime.Parse("2019-01-01 00:00:00.0000000")
            //}
            //);
        }
    }

    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("Models").HasKey(p => p.Id);
        }
    }

    public class ImageConfiguration : IEntityTypeConfiguration<ImageModel>
    {
        public void Configure(EntityTypeBuilder<ImageModel> builder)
        {
            builder.ToTable("Images").HasKey(p => p.Id);
        }
    }

    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new IdentityUserRole<string>
                {
                    RoleId = "admin",
                    UserId = "qwe"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "user",
                    UserId = "qwerty"
                });
        }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "Guest",
                    NormalizedName = "GUEST"
                },
                new IdentityRole
                {
                    Id = "user",
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "admin",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );
        }
    }

    public class BrandsConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasData(
                new Brand { Id = 1, Name = "Tesla" },
                new Brand { Id = 2, Name = "BMW" },
                new Brand { Id = 3, Name = "Ferrari" },
                new Brand { Id = 4, Name = "Ford" },
                new Brand { Id = 5, Name = "Porsche" },
                new Brand { Id = 6, Name = "Honda" },
                new Brand { Id = 7, Name = "Lamborghini" },
                new Brand { Id = 8, Name = "Toyota" },
                new Brand { Id = 9, Name = "Bentley" },
                new Brand { Id = 10, Name = "Maserati" },
                new Brand { Id = 11, Name = "Audi" },
                new Brand { Id = 12, Name = "Subaru" },
                new Brand { Id = 13, Name = "Caillac" },
                new Brand { Id = 14, Name = "Jeep" },
                new Brand { Id = 15, Name = "Chrysler" },
                new Brand { Id = 16, Name = "Chevrolet" },
                new Brand { Id = 17, Name = "Dodge" },
                new Brand { Id = 18, Name = "Hyundai" },
                new Brand { Id = 19, Name = "Jaguar" },
                new Brand { Id = 20, Name = "Mazda" },
                new Brand { Id = 21, Name = "Nissan" },
                new Brand { Id = 22, Name = "Alfa Romeo" },
                new Brand { Id = 23, Name = "Bugatti" },
                new Brand { Id = 24, Name = "Lexus" },
                new Brand { Id = 25, Name = "Volkswagen" },
                new Brand { Id = 26, Name = "Mercedes-Benz" },
                new Brand { Id = 27, Name = "Volvo" },
                new Brand { Id = 28, Name = "Kia" },
                new Brand { Id = 29, Name = "Mitsubishi" },
                new Brand { Id = 30, Name = "Peugeot" },
                new Brand { Id = 31, Name = "Renault" },
                new Brand { Id = 32, Name = "Mini" },
                new Brand { Id = 33, Name = "Citroën" }
            );
        }
    }
}
