using CarMarket.Core.User.Domain;
using CarMarket.Data.User.Domain;
using System;

namespace CarMarket.Data.User.Converter
{
    public class UserConverter
    {
        public UserEntity ToEntity(UserModel userDto)
        {
            return new UserEntity(userDto.FirstName, userDto.LastName, userDto.Permissions, userDto.Email, userDto.Password);
        }

        public UserModel ToModel(UserEntity userModel)
        {
            return new UserModel(userModel.FirstName, userModel.LastName, userModel.Permissions, userModel.Email, userModel.Password);
        }

        internal UserModel ToModel(object model)
        {
            throw new NotImplementedException();
        }
    }
}
