using CarMarket.Core.Car.Domain;
using CarMarket.Core.User.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.User.Service
{
    public interface IUserService
    {
        Task AddPermissionAsync(long userId, params Permission[] permissions);
        Task ChangePermissionAsync(long userId, Permission replaceablePermission, Permission substitutePermission);
        Task<long> CreateAsync(UserModel userModel);
        Task<UserModel> GetAsync(long userId);
        Task<UserModel> GetByEmailAsync(string email);
        Task<List<UserModel>> GetAllAsync();
        Task DeleteAsync(long userId);
        Task<UserModel> AuthenticateAsync(string email, string password);
        Task<Role> GetUserRoleAsync(string roleName);
        Task UpdateUser(long userId, UserModel userModel);
        Task DeletePermissionAsync(long userId, Permission permission);
    }
}
