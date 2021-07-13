using CarMarket.Core.Car.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.Car.Repository
{
    public interface ICarRepository
    {
        Task<CarModel> FindByIdAsync(long id);
        Task<IEnumerable<CarModel>> FindByNameAsync(string name);
        CarModel FindByName(string name);
        Task<long> SaveAsync(CarModel car);
        Task<List<CarModel>> FindAllAsync();
        Task DeleteAsync(CarModel car);
    }
}