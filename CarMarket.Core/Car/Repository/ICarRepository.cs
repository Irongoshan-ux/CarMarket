using CarMarket.Core.Car.Domain;
using CarMarket.Core.Image.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.Car.Repository
{
    public interface ICarRepository
    {
        Task<CarModel> FindByIdAsync(long id);
        Task<IEnumerable<CarModel>> FindAllByNameAsync(string name);
        Task<CarModel> FindOneByNameAsync(string name);
        Task<long> SaveAsync(CarModel car);
        Task<List<CarModel>> FindAllAsync();
        Task DeleteAsync(long carId);
        Task UpdateAsync(long carId, CarModel car);
    }
}