using CarMarket.Core.Car.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.Car.Repository
{
    public interface ICarBrandRepository
    {
        Task<Brand> FindByIdAsync(long id);
        Task<Brand> AddAsync(Brand carBrand);
        Task<IEnumerable<Brand>> FindAllAsync();
        Task DeleteAsync(long carBrand);
        Task<Brand> UpdateAsync(long carBrandId, Brand brand);
    }
}