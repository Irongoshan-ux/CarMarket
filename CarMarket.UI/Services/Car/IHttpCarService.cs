using CarMarket.Core.Car.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.UI.Services.Car
{
    public interface IHttpCarService : IHttpService<CarModel, long>
    {
        Task<IEnumerable<CarModel>> GetAllUserCarsAsync(long userId);
        Task<IEnumerable<CarModel>> SearchAsync(string carName, CarType? carType);
    }
}
