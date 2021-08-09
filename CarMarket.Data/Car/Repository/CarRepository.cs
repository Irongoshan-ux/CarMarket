using AutoMapper;
using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Repository;
using CarMarket.Data.Car.Domain;
using CarMarket.Data.User.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            var carEntity = await _context.Cars
                .Include(x => x.CarImages)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<CarModel>(carEntity);
        }

        public async Task<List<CarModel>> FindAllAsync()
        {
            var carEntities = await _context.Cars
                .Include(x => x.CarImages)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<CarModel>>(carEntities);
        }

        public async Task<CarModel> SaveAsync(CarModel carModel)
        {
            var newCarEntity = _mapper.Map<CarEntity>(carModel);

            if (newCarEntity.Owner != null)
            {
                _context.Entry(newCarEntity.Owner).State = EntityState.Unchanged;
            }

            var added = await _context.Cars.AddAsync(newCarEntity);

            await _context.SaveChangesAsync();

            return _mapper.Map<CarModel>(added.Entity);
        }

        public async Task DeleteAsync(long carId)
        {
            var carEntity = await _context.Cars
                .Include(x => x.CarImages)
                .Where(x => x.Id == carId)
                .FirstOrDefaultAsync();

            _context.Cars.Remove(carEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<CarModel> FindOneByNameAsync(string name)
        {
            var carEntity = await _context.Cars
                .Include(x => x.CarImages)
                .FirstOrDefaultAsync(x => x.Name == name);

            return _mapper.Map<CarModel>(carEntity);
        }

        public async Task<IEnumerable<CarModel>> FindAllByNameAsync(string name)
        {
            var carEntities = await _context.Cars
                .Include(x => x.CarImages)
                .Where(x => x.Name == name)
                .ToListAsync();

            return _mapper.Map<List<CarModel>>(carEntities);
        }
        public async Task UpdateAsync(long carId, CarModel car)
        {
            var carEntity = _mapper.Map<CarEntity>(car);

            _context.Update(carEntity);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarModel>> FindAllUserCarsAsync(long userId)
        {
            var carEntities = await _context.Cars
                 .Include(x => x.CarImages)
                 .Where(x => x.Owner.Id == userId)
                 .ToListAsync();

            var userCars = _mapper.Map<List<CarModel>>(carEntities);

            return userCars;
        }

        public async Task<IEnumerable<CarModel>> SearchAsync(string carName, CarType? carType)
        {
            IQueryable<CarEntity> query = _context.Cars;

            if (!string.IsNullOrEmpty(carName))
            {
                query = query.Where(c => c.Name.Contains(carName));
            }

            if (carType != null)
            {
                query = query.Where(c => c.CarType == carType);
            }

            return _mapper.Map<IEnumerable<CarModel>>(await query.ToListAsync());
        }
    }
}