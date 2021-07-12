using CarMarket.Core.User.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.User.Service
{
    public interface IUserService
    {
        Task AddPermissionAsync(long userId, params Permission[] permissions);
        Task AddPermissionAsync(long userId, Permission permission);
        Task ChangePermissionAsync(long userId, Permission replaceablePermission, Permission substitutePermission);
        Task<long> CreateAsync(UserModel userModel);
        Task<UserModel> GetAsync(long userId);
        Task<List<UserModel>> GetAllAsync();
        Task DeleteAsync(long userId);
    }
}
