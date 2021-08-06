﻿using CarMarket.Core.User.Domain;
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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        private static bool IsUserContainsPermission(UserModel user, Permission permission) => false;/*user.Permissions.Contains(permission);*/
    }
}
