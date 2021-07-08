using CarMarket.Core.User;
using System;
using System.Collections.Generic;

namespace CarMarket.Data.User
{
    public class UserRepository : IUserRepository
    {
        public UserDto FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> FindAll()
        {
            throw new NotImplementedException();
        }

        public UserDto FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void Save(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
