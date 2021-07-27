using CarMarket.Core.Car.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.Car.Service
{
    public interface ICarService
    {
        Task<long> CreateAsync(CarModel userModel);
        Task<CarModel> GetAsync(long id);
        Task<List<CarModel>> GetAllAsync();
        Task DeleteAsync(long carId);
        Task<CarModel> GetByName(string carName);
        Task<IEnumerable<CarModel>> GetByNameAsync(string carName);
        Task UpdateCar(long carId, CarModel car);
    }
}
