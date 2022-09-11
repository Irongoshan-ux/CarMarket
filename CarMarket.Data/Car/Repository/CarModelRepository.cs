using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Data.Car.Repository
{
    public class CarModelRepository : ICarModelRepository
    {
        private readonly ApplicationDbContext _context;

        public CarModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Model> AddAsync(Model carModel)
        {
            var brand = carModel.Brand;

            if (_context.Brands.AsNoTracking().Where(x => x.Id == brand.Id).Any())
            {
                _context.Entry(carModel.Brand).State = EntityState.Unchanged;
            }
            else
            {
                brand.Id = default;

                var result = await _context.Brands.AddAsync(carModel.Brand);
                await _context.SaveChangesAsync();
            }

            var added = await _context.Models.AddAsync(carModel);

            await _context.SaveChangesAsync();

            return added.Entity;
        }

        public async Task DeleteAsync(long carModelId)
        {
            var carModel = await _context.Models
                 .AsNoTracking()
                 .Where(x => x.Id == carModelId)
                 .FirstOrDefaultAsync();

            _context.Models.Remove(carModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Model>> FindAllAsync()
        {
            return await _context.Models
                 .AsNoTracking()
                 .Include(x => x.Brand)
                 .ToListAsync();
        }

        public Task<Model> FindByIdAsync(long id)
        {
            return _context.Models
                .AsNoTracking()
                .Include(x => x.Brand)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Model>> SearchByBrandAsync(string brandName)
        {
            return await _context.Models
                .AsNoTracking()
                .Where(x => x.Brand.Name.Equals(brandName))
                .ToListAsync();
        }

        public async Task<Model> UpdateAsync(long carModelId, Model model)
        {
            model.Id = carModelId;

            _context.Update(model);

            await _context.SaveChangesAsync();

            return await FindByIdAsync(carModelId);
        }
    }
}
