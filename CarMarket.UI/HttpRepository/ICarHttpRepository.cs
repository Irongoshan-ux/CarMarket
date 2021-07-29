using CarMarket.Core.Car.Domain;
using CarMarket.Core.RequestFeatures;
using CarMarket.UI.Features;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarMarket.UI.HttpRepository
{
    public interface ICarHttpRepository
    {
        Task<PagingResponse<CarModel>> GetProducts(ModelParameters carParameters);
        Task CreateCar(CarModel car);
        Task<string> UploadCarImage(MultipartFormDataContent content);
        Task<CarModel> GetCar(long id);
        Task UpdateCar(CarModel car);
        Task DeleteCar(long id);
    }
}
