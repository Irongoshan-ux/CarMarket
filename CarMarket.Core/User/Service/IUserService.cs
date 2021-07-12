using CarMarket.Core.User.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.User.Service
{
    public interface IUserService
    {
        Task AddPermissionAsync(params Permission[] permissions);
        Task AddPermissionAsync(Permission permission);
        Task ChangePermissionAsync(Permission repcaceablePermission, Permission substitutePermission);
        Task<long> CreateAsync(UserModel userModel);
        Task<UserModel> GetAsync(int id);
        Task<List<UserModel>> GetAllAsync();
    }
}
