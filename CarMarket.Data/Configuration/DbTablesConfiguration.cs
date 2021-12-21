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
}
