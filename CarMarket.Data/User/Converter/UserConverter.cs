using CarMarket.Core.User.Domain;
using CarMarket.Data.User.Domain;

namespace CarMarket.Data.User.Converter
{
    public class UserConverter
    {
        public UserEntity ToEntity(UserModel userModel)
        {
            return new UserEntity(userModel.FirstName, userModel.LastName, userModel.Permissions, userModel.Email, userModel.Password);
        }

        public UserModel ToModel(UserEntity userEntity)
        {
            return new UserModel(userEntity.FirstName, userEntity.LastName, userEntity.Permissions, userEntity.Email, userEntity.Password);
        }
    }
}
