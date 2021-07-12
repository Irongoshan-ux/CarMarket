using CarMarket.Core.Car.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.Car.Service
{
    public interface ICarService
    {
        Task<long> CreateAsync(CarModel userModel);
        Task<CarModel> GetAsync(int id);
        Task<List<CarModel>> GetAllAsync();
    }
}
