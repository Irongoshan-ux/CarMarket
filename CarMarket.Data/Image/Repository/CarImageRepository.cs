using CarMarket.Core.Image.Domain;
using CarMarket.Core.Image.Repository;
using System;
using System.Threading.Tasks;

namespace CarMarket.Data.Image.Repository
{
    class CarImageRepository : ICarImageRepository
    {
        private readonly ApplicationDbContext _context;

        public CarImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> SaveAsync(CarImage carImage)
        {
            // TODO Add automapper 

            var added = await _context.CarImages.AddAsync(carImage);
            await _context.SaveChangesAsync();

            return added.Entity.Id;
        }
    }
}
