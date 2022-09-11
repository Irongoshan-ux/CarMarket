using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Data.Car.Repository
{
    public class CarBrandRepository : ICarBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public CarBrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Brand> AddAsync(Brand carBrand)
        {
            carBrand.Id = 0;

            if (_context.Brands.AsNoTracking().Where(x => x.Name == carBrand.Name).Any())
                throw new CarBrandExistsException();

            var added = await _context.AddAsync(carBrand);

            await _context.SaveChangesAsync();

            return added.Entity;
        }

        public async Task DeleteAsync(long carBrand)
        {
            var brand = await _context.Brands
                .AsNoTracking()
                .Where(x => x.Id == carBrand)
                .FirstOrDefaultAsync();

            _context.Brands.Remove(brand);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Brand>> FindAllAsync()
        {
            return await _context.Brands.AsNoTracking().ToListAsync();
        }

        public Task<Brand> FindByIdAsync(long id)
        {
            return _context.Brands
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Brand> UpdateAsync(long carBrandId, Brand brand)
        {
            brand.Id = carBrandId;

            _context.Brands.Update(brand);

            await _context.SaveChangesAsync();

            return await FindByIdAsync(carBrandId);
        }
    }
}
