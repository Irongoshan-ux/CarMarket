using CarMarket.Core.Car.Domain;
using CarMarket.Core.Image.Domain;
using CarMarket.Core.Image.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Data.Image.Repository
{
    public class CarImageRepository : ICarImageRepository
    {
        private readonly ApplicationDbContext _context;

        public CarImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarImage>> FindAllAsync(long carId)
        {
            // TODO: How to realize this business logic?

            var car = await _context.Cars
                .Where(car => car.Id == carId)
                .Include(cars => cars.CarImages)
                .ThenInclude(images => images.ImageData)
                .ToListAsync();

            List<CarImage> carImages = new List<CarImage>(car.Count);

            foreach(var item in car)
            {
                carImages = (List<CarImage>)item.CarImages;
            }

            return carImages;
        }

        public async Task<long> SaveAsync(CarImage carImage)
        {
            var added = await _context.CarImages.AddAsync(carImage);
            await _context.SaveChangesAsync();

            return added.Entity.Id;
        }
    }
}
