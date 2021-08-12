﻿using CarMarket.Core.Car.Domain;
using CarMarket.Core.DataResult;
using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Repository;
using CarMarket.Core.User.Service;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.BusinessLogic.User.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<UserModel> _userManager;

        public UserService(IUserRepository userRepository, UserManager<UserModel> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<UserModel> AuthenticateAsync(string email, string password)
        {
            return await _userRepository.FindUserModelAsync(email, password);
        }

        public async Task AddPermissionAsync(string userId, params Permission[] permissions)
        {
            var user = await GetAsync(userId);

            foreach (var permission in permissions)
            {
                if (IsUserContainsPermission(user, permission))
                    continue;

                //user.Permissions.Add(permission);
            }

            await UpdateUser(userId, user);
        }

        public async Task ChangePermissionAsync(string userId, Permission replaceablePermission, Permission substitutePermission)
        {
            var user = await GetAsync(userId);

            if (IsUserContainsPermission(user, replaceablePermission))
            {
                //user.Permissions.Remove(replaceablePermission);
                //user.Permissions.Add(substitutePermission);

                await UpdateUser(userId, user);
            }
        }

        public async Task DeletePermissionAsync(string userId, Permission permission)
        {
            var user = await GetAsync(userId);

            if (IsUserContainsPermission(user, permission))
            {
                //user.Permissions.Remove(permission);

                await UpdateUser(userId, user);
            }
        }

        public async Task<string> CreateAsync(UserModel userModel)
        {
            if (userModel is null)
            {
                throw new ArgumentNullException(nameof(userModel));
            }

            userModel.Id = Guid.NewGuid().ToString();

            return await _userRepository.SaveAsync(userModel);
        }

        public async Task<UserModel> GetAsync(string userId)
        {
            return await _userRepository.FindByIdAsync(userId);
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            return await _userRepository.FindAllAsync();
        }

        public async Task DeleteAsync(string userId)
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
            catch
            {
                return null;
            }

            return user;
        }

        public async Task<Role> GetUserRoleAsync(string roleName)
        {
            return await _userRepository.FindUserRoleAsync(roleName);
        }

        public async Task UpdateUser(string userId, UserModel userModel)
        {
            await _userRepository.UpdateAsync(userId, userModel);
        }

        public async Task AddToRoleAsync(UserModel userModel, string role)
        {
            await _userManager.AddToRoleAsync(userModel, role);
        }

        private static bool IsUserContainsPermission(UserModel user, Permission permission) => false;/*user.Permissions.Contains(permission);*/

        public async Task<DataResult<UserModel>> GetByPageAsync(int skip = 0, int take = 5)
        {
            return await _userRepository.FindByPageAsync(skip, take);
        }
    }
}
