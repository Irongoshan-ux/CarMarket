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
    public class CarImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;

        public CarImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ImageModel>> FindAllAsync(long carId)
        {
            // TODO: How to realize this business logic?

            var car = await _context.Cars
                .Where(car => car.Id == carId)
                .Include(cars => cars.CarImages)
                .ThenInclude(images => images.ImageData)
                .ToListAsync();

            List<ImageModel> carImages = new(car.Count);

            foreach(var item in car)
            {
                carImages = (List<ImageModel>)item.CarImages;
            }

            return carImages;
        }

        public async Task<long> SaveAsync(ImageModel carImage)
        {
            var added = await _context.Images.AddAsync(carImage);
            await _context.SaveChangesAsync();

            return added.Entity.Id;
        }
    }
}
