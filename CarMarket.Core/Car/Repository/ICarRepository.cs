using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.Car.Repository
{
    public interface ICarRepository
    {
        Task<CarModel> FindByIdAsync(int id);
        Task<CarModel> FindByNameAsync(string email);
        Task<long> SaveAsync(CarModel user);
        Task<List<CarModel>> FindAllAsync();
    }
}