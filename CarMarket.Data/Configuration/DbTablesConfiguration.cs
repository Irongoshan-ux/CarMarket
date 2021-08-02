using CarMarket.Core.Image.Domain;
using CarMarket.Core.User.Domain;
using CarMarket.Data.Car.Domain;
using CarMarket.Data.User.Domain;
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

    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users").HasKey(p => p.Id);
        }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.ToTable("AspNetRoles").HasKey(p => p.Id);
        }
    }

    //public class RoleConfiguration : IEntityTypeConfiguration<Role>
    //{
    //    public void Configure(EntityTypeBuilder<Role> builder)
    //    {
    //        builder.ToTable("Roles").HasKey(p => p.Id);
    //    }
    //}

    public class PermissonConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions").HasKey(p => p.Id);
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
            builder.ToTable("AspNetUserRoles").HasKey(keys => new { keys.UserId, keys.RoleId });
        }
    }

    //public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    //{
    //    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    //    {
    //        builder.HasData(
    //            new IdentityRole
    //            {
    //                Name = "Guest",
    //                NormalizedName = "GUEST"
    //            },
    //            new IdentityRole
    //            {
    //                Name = "User",
    //                NormalizedName = "USER"
    //            },
    //            new IdentityRole
    //            {
    //                Name = "Admin",
    //                NormalizedName = "ADMIN"
    //            }
    //        );
    //    }
    //}
}
