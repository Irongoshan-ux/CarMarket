using CarMarket.Core.User;
using System;

namespace CarMarket.BusinessLogic.User
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

        public void Create(UserDto user)
        {
            throw new NotImplementedException();
        }

        public UserDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public UserDto GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
