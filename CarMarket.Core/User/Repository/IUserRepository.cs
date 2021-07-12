using System.Collections.Generic;
using System.Threading.Tasks;
using CarMarket.Core.User.Domain;

namespace CarMarket.Core.User.Repository
{
    public interface IUserRepository
    {
        Task<UserModel> FindByIdAsync(long id);
        Task<long> SaveAsync(UserModel user);
        Task<List<UserModel>> FindAllAsync();
        Task DeleteAsync(UserModel user);
    }
}