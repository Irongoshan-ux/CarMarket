using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Repository;
using CarMarket.Core.User.Service;
using System;

namespace CarMarket.BusinessLogic.User.Service
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

        public bool Create(UserModel userModel)
        {
            return _userRepository.Save(userModel);
        }

        public UserModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
