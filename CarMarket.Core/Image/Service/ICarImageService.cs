using System.Threading.Tasks;
using CarMarket.Core.Image.Domain;

namespace CarMarket.Core.Image.Service
{
    public interface ICarImageService
    {
        Task<long> UploadAsync(CarImage carImage);
    }
}
