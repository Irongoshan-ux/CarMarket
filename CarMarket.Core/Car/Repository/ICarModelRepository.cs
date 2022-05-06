using CarMarket.Core.Car.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.Car.Repository
{
    public interface ICarModelRepository
    {
        Task<Model> FindByIdAsync(long id);
        Task<Model> AddAsync(Model carModel);
        Task<IEnumerable<Model>> FindAllAsync();
        Task DeleteAsync(long carModelId);
        Task<Model> UpdateAsync(long carModelId, Model model);
        Task<IEnumerable<Model>> SearchByBrandAsync(string brandName);
        Task<IEnumerable<Brand>> FindAllBrandsAsync();
    }
}