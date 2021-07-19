using AutoMapper;
using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Repository;
using CarMarket.Core.Image.Domain;
using CarMarket.Data.Car.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Data.Car.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CarRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CarModel> FindByIdAsync(long id)
        {
            var carEntity = await _context.Cars.FindAsync(id);
            return _mapper.Map<CarModel>(carEntity);
        }

        public async Task<List<CarModel>> FindAllAsync()
        {
            var carEntities = await _context.Cars
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<CarModel>>(carEntities);
        }

        public async Task<long> SaveAsync(CarModel carModel)
        {
            var newCarEntity = _mapper.Map<CarEntity>(carModel);

            var added = await _context.Cars.AddAsync(newCarEntity);
            await _context.SaveChangesAsync();

            return added.Entity.Id;
        }

        public async Task DeleteAsync(long carId)
        {
            var carEntity = await _context.Cars
                .Where(x => x.Id == carId)
                .FirstOrDefaultAsync();

            _context.Cars.Remove(carEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<CarModel> FindOneByNameAsync(string name)
        {
            var carEntity = await _context.Cars.FirstOrDefaultAsync(x => x.Name == name);

            return _mapper.Map<CarModel>(carEntity);
        }

        public async Task<IEnumerable<CarModel>> FindAllByNameAsync(string name)
        {
            var carEntities = await _context.Cars
                .Where(x => x.Name == name)
                //.Select(x => _mapper.Map<CarModel>(x))
                .ToListAsync();

            return _mapper.Map<List<CarModel>>(carEntities);
        }

        public async Task<long> SaveCarImageAsync(CarImage carImage)
        {
            var added = await _context.CarImages.AddAsync(carImage);
            await _context.SaveChangesAsync();

            return added.Entity.Id;
        }
    }
}