using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Repository;
using CarMarket.Data.Car.Converter;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Data.Car.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly CarConverter _carConverter;

        public CarRepository(ApplicationDbContext context, CarConverter carConverter)
        {
            _context = context;
            _carConverter = carConverter;
        }

        public async Task<CarModel> FindByIdAsync(long id)
        {
            var carEntity = await _context.Cars.FindAsync(id);
            return _carConverter.ToModel(carEntity);
        }

        public async Task<List<CarModel>> FindAllAsync()
        {
            var carModels = await _context.Cars
                .AsNoTracking()
                .Select(x => _carConverter.ToModel(x))
                .ToListAsync();

            return carModels;
        }

        public async Task<long> SaveAsync(CarModel carModel)
        {
            var newCarEntity = _carConverter.ToEntity(carModel);

            var added = await _context.Cars.AddAsync(newCarEntity);
            await _context.SaveChangesAsync();

            return added.Entity.Id;
        }

        public async Task DeleteAsync(CarModel carModel)
        {
            var carEntity = _context.Cars.Where(x => x.Id == carModel.Id).FirstOrDefault(); // why not working??? _carConverter.ToEntity(carModel);

            _context.Cars.Remove(carEntity);
            await _context.SaveChangesAsync();
        }

        public CarModel FindByName(string name)
        {
            var carEntity = _context.Cars.FirstOrDefault(x => x.Name == name);

            return _carConverter.ToModel(carEntity);
        }

        public async Task<IEnumerable<CarModel>> FindByNameAsync(string name)
        {
            var carEntity = await _context.Cars.Where(x => x.Name == name).ToListAsync();

            var carModels = new List<CarModel>(carEntity.Count);

            Parallel.ForEach(carEntity, x =>
            {
                carModels.Add(_carConverter.ToModel(x));
            });

            return carModels;
        }
    }
}
