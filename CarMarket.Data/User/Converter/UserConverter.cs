using CarMarket.Core.User.Domain;
using CarMarket.Data.User.Domain;

namespace CarMarket.Data.User.Converter
{
    public class UserConverter
    {
        public UserEntity ToEntity(UserModel userModel)
        {
            return new UserEntity(userModel.Id, userModel.FirstName, userModel.LastName, userModel.RoleId, userModel.Role,
                userModel.Permissions, userModel.Email, userModel.Password);
        }

        public UserModel ToModel(UserEntity userEntity)
        {
            return new UserModel(userEntity.Id, userEntity.FirstName, userEntity.LastName, userEntity.RoleId, userEntity.Role,
                userEntity.Permissions, userEntity.Email, userEntity.Password);
        }
    }
}
