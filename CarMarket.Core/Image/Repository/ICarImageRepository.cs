using CarMarket.Core.Image.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.Image.Repository
{
    public interface ICarImageRepository
    {
        Task<long> SaveAsync(CarImage carImageModel);
        Task<List<CarImage>> FindAllAsync(long carId);
    }
}
