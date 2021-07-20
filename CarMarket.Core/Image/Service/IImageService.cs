using System.Collections.Generic;
using System.Threading.Tasks;
using CarMarket.Core.Image.Domain;

namespace CarMarket.Core.Image.Service
{
    public interface IImageService
    {
        Task<long> UploadAsync(ImageModel carImage);
        Task<List<ImageModel>> GetAllAsync(long carId);
    }
}
