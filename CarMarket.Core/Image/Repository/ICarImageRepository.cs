using CarMarket.Core.Image.Domain;
using System.Threading.Tasks;

namespace CarMarket.Core.Image.Repository
{
    public interface ICarImageRepository
    {
        Task<long> SaveAsync(CarImage carImageModel);
    }
}
