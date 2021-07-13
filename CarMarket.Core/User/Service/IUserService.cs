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
        UserModel GetByEmail(string email);
        Task<List<UserModel>> GetAllAsync();
        Task DeleteAsync(long userId);
        UserModel Authenticate(string email, string password);
    }
}
