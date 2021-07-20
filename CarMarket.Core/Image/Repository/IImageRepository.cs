using CarMarket.Core.Image.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.Image.Repository
{
    public interface IImageRepository
    {
        Task<long> SaveAsync(ImageModel carImageModel);
        Task<List<ImageModel>> FindAllAsync(long carId);
    }
}
