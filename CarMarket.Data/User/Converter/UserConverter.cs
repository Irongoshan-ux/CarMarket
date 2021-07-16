using CarMarket.Core.User.Domain;
using CarMarket.Data.User.Domain;

namespace CarMarket.Data.User.Converter
{
    public class UserConverter
    {
        public static UserEntity ToEntity(UserModel userModel)
        {
            return new UserEntity
            {
                Id = userModel.Id,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Permissions = userModel.Permissions,
                RoleId = userModel.RoleId,
                Role = userModel.Role,
                Password = userModel.Password
            };
        }

        public static UserModel ToModel(UserEntity userEntity)
        {
            return new UserModel
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Permissions = userEntity.Permissions,
                RoleId = userEntity.RoleId,
                Role = userEntity.Role,
                Password = userEntity.Password
            };
        }
    }
}
