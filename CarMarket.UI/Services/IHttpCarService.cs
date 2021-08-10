using CarMarket.Core.Car.Domain;
using CarMarket.Core.DataResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.UI.Services
{
    public interface IHttpCarService
    {
        Task<CarModel> CreateAsync(CarModel userModel);
        Task<CarModel> GetAsync(long id);
        Task<IEnumerable<CarModel>> GetAllAsync();
        Task<DataResult<CarModel>> GetByPageAsync(int skip, int take);
        Task DeleteAsync(long carId);
        Task<CarModel> UpdateCarAsync(long carId, CarModel car);
        Task<IEnumerable<CarModel>> GetAllUserCarsAsync(long userId);
        Task<IEnumerable<CarModel>> SearchAsync(string carName, CarType? carType);
    }
}
