using CarMarket.Core.Car.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarMarket.UI.Services.CarBrand
{
    public interface IHttpCarBrandService
    {
        public Task<Brand> AddAsync(Brand brand, CancellationToken token);
        public Task<Brand> GetAsync(long brandId, CancellationToken cancellationToken);
        public Task<IEnumerable<Brand>> GetAllAsync(CancellationToken cancellationToken);
        public Task<bool> UpdateAsync(long brandId, Brand brand, CancellationToken cancellationToken);
        public Task<bool> DeleteAsync(long brandId, CancellationToken cancellationToken);
    }
}
