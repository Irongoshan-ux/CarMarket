using System.Collections.Generic;
using System.Threading.Tasks;
using CarMarket.Core.User.Domain;

namespace CarMarket.Core.User.Repository
{
    public interface IUserRepository
    {
        Task<UserModel> FindByIdAsync(int id);
        Task<UserModel> FindByEmailAsync(string email);
        Task<long> SaveAsync(UserModel user);
        Task<List<UserModel>> FindAllAsync();
    }
}