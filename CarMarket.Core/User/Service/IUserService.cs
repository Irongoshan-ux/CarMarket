using CarMarket.Core.DataResult;
using CarMarket.Core.User.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.User.Service
{
    public interface IUserService
    {
        Task AddPermissionAsync(string userId, params Permission[] permissions);
        Task ChangePermissionAsync(string userId, Permission replaceablePermission, Permission substitutePermission);
        Task<string> CreateAsync(UserModel userModel);
        Task AddToRoleAsync(UserModel userModel, string role);
        Task<UserModel> GetAsync(string userId);
        Task<UserModel> GetByEmailAsync(string email);
        Task<DataResult<UserModel>> GetByPageAsync(int skip = 0, int take = 5);
        Task<List<UserModel>> GetAllAsync();
        Task DeleteAsync(string userId);
        Task<UserModel> AuthenticateAsync(string email, string password);
        Task<Role> GetUserRoleAsync(string roleName);
        Task UpdateUser(string userId, UserModel userModel);
        Task DeletePermissionAsync(string userId, Permission permission);
    }
}
