using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Repository;
using CarMarket.Core.User.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.BusinessLogic.User.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddPermissionAsync(params Permission[] permissions)
        {
            throw new NotImplementedException();
        }

        public async Task AddPermissionAsync(Permission permission)
        {
            throw new NotImplementedException();
        }

        public async Task ChangePermissionAsync(Permission repcaceablePermission, Permission substitutePermission)
        {
            throw new NotImplementedException();
        }

        public async Task<long> CreateAsync(UserModel userModel)
        {
            if (userModel is null)
            {
                throw new ArgumentNullException(nameof(userModel));
            }

            return await _userRepository.SaveAsync(userModel);
        }

        public async Task<UserModel> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            return await _userRepository.FindAllAsync();
        }
    }
}
