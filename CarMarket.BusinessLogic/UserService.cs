using System;
using CarMarket.Core;

namespace CarMarket.BusinessLogic
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Core.User Get(int id)
        {
            throw new NotImplementedException();
        }

        public Core.User GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
