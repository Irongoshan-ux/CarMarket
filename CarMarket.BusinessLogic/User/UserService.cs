using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Repository;
using CarMarket.Core.User.Service;
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

        public void Create(Core.User.Domain.UserModel user)
        {
            throw new NotImplementedException();
        }

        public Core.User.Domain.UserModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public Core.User.Domain.UserModel GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
