using System;
using CarMarket.Core;

namespace CarMarket.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddPermission(params Permission[] permissions)
        {
            throw new NotImplementedException();
        }

        public void AddPermission(Permission permission)
        {
            throw new NotImplementedException();
        }

        public void ChangePermission(Permission repcaceablePermission, Permission substitutePermission)
        {
            throw new NotImplementedException();
        }

        public void Create(Core.User user)
        {
            throw new NotImplementedException();
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
