using CarMarket.Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket.Data.User
{
    public class UserConverter
    {
        public UserModel ToEntity(UserDto userDto)
        {
            return new UserModel(userDto.FirstName, userDto.LastName, userDto.Permissions, userDto.Email, userDto.Password);
        }

        public UserDto ToDto(UserModel userModel)
        {
            return new UserDto(userModel.FirstName, userModel.LastName, userModel.Permissions, userModel.Email, userModel.Password);
        }
    }
}
