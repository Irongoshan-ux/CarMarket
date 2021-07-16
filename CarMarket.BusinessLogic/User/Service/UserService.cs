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

        public async Task<UserModel> AuthenticateAsync(string email, string password)
        {
            return await _userRepository.FindUserModelAsync(email, password);
        }

        public async Task AddPermissionAsync(long userId, params Permission[] permissions)
        {
            var user = await GetAsync(userId);

            foreach (var permission in permissions)
            {
                if (IsUserContainsPermission(user, permission))
                    continue;

                user.Permissions.Add(permission);
            }
        }

        public async Task ChangePermissionAsync(long userId, Permission replaceablePermission, Permission substitutePermission)
        {
            var user = await GetAsync(userId);

            if (IsUserContainsPermission(user, replaceablePermission))
            {
                user.Permissions.Remove(replaceablePermission);
                user.Permissions.Add(substitutePermission);
            }
        }

        public async Task<long> CreateAsync(UserModel userModel)
        {
            if (userModel is null)
            {
                throw new ArgumentNullException(nameof(userModel));
            }

            return await _userRepository.SaveAsync(userModel);
        }

        public async Task<UserModel> GetAsync(long userId)
        {
            return await _userRepository.FindByIdAsync(userId);
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            return await _userRepository.FindAllAsync();
        }

        public async Task DeleteAsync(long userId)
        {
            var userModel = await _userRepository.FindByIdAsync(userId);

            if (userModel is null)
            {
                throw new ArgumentException(nameof(userModel) + " shouldn't be null");
            }

            await _userRepository.DeleteAsync(userModel.Id);
        }

        public async Task<UserModel> GetByEmailAsync(string email)
        {
            UserModel user;
            try
            {
                user = await _userRepository.FindByEmailAsync(email);
            }
            catch(Exception e)
            {
                return null;
            }

            return user;
        }

        public async Task<Role> GetUserRoleAsync(string roleName)
        {
            return await _userRepository.FindUserRoleAsync(roleName);
        }

        private static bool IsUserContainsPermission(UserModel user, Permission permission) => user.Permissions.Contains(permission);
    }
}
