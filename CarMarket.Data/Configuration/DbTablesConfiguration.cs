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
        }
    }

    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("Cars").HasKey(p => p.Id);
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
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "qwe",
                    UserId = "qwe"
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
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "qwe",
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
