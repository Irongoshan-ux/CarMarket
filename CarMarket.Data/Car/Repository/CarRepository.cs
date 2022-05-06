using AutoMapper;
using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Repository;
using CarMarket.Core.DataResult;
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
            var carEntity = await _context.Cars
                .AsNoTracking()
                .Include(x => x.CarImages)
                .Include(x => x.Owner)
                .Include(x => x.Model)
                .Include(x => x.Model.Brand)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<CarModel>(carEntity);
        }

        public async Task<IEnumerable<CarModel>> FindAllAsync()
        {
            var carEntities = await _context.Cars
                .AsNoTracking()
                .Include(x => x.CarImages)
                .Include(x => x.Owner)
                .Include(x => x.Model)
                .Include(x => x.Model.Brand)
                .ToListAsync();

            return _mapper.Map<List<CarModel>>(carEntities);
        }

        public async Task<CarModel> AddAsync(CarModel carModel)
        {
            var newCarEntity = _mapper.Map<CarEntity>(carModel);

            if (newCarEntity.Owner != null)
            {
                _context.Entry(newCarEntity.Owner).State = EntityState.Unchanged;
            }

            if (_context.Models.AsNoTracking().Where(x => x.Id == newCarEntity.Model.Id).Any())
            {
                _context.Entry(newCarEntity.Model).State = EntityState.Unchanged;
            }

            var added = await _context.Cars.AddAsync(newCarEntity);

            await _context.SaveChangesAsync();

            return _mapper.Map<CarModel>(added.Entity);
        }

        public async Task DeleteAsync(long carId)
        {
            var carEntity = await _context.Cars
                .AsNoTracking()
                .Include(x => x.CarImages)
                .Where(x => x.Id == carId)
                .FirstOrDefaultAsync();

            _context.Cars.Remove(carEntity);
            await _context.SaveChangesAsync();
        }
        
        public async Task<CarModel> UpdateAsync(long carId, CarModel car)
        {
            var carEntity = _mapper.Map<CarEntity>(car);

            //var currentCar = await _context.Cars.FindAsync(carId);

            //var existedBrand = await _context.Brands.Where(x => x.Name.Equals(currentCar.Model.Brand.Name)).ToListAsync();

            //if (existedBrand.Count > 0)
            //{
            //    if (existedBrand.First().Name.Equals(car.Model.Brand.Name))
            //    {
            //        _context.Entry(carEntity.Model).State = EntityState.Unchanged;
            //    }
            //    else
            //    {
            //        carEntity.Model.Brand = _context.Brands.Where(x => x.Name.Equals(car.Model.Brand.Name)).First();
            //    }
            //}

            _context.Update(carEntity);

            await _context.SaveChangesAsync();

            return await FindByIdAsync(carId);
        }

        public async Task<IEnumerable<CarModel>> FindAllUserCarsAsync(string userId)
        {
            var carEntities = await _context.Cars
                .AsNoTracking()
                .Where(x => x.Owner.Id == userId)
                .Include(x => x.CarImages)
                .Include(x => x.Model)
                .Include(x => x.Model.Brand)
                .ToListAsync();

            var userCars = _mapper.Map<List<CarModel>>(carEntities);

            return userCars;
        }

        public async Task<IEnumerable<CarModel>> SearchAsync(string carName, CarType? carType)
        {
            IQueryable<CarEntity> query = _context.Cars
                .AsNoTracking()
                .Include(x => x.CarImages)
                .Include(x => x.Owner)
                .Include(x => x.Model)
                .Include(x => x.Model.Brand);

            if (!string.IsNullOrEmpty(carName))
            {
                query = query.Where(c => c.Model.Name.Contains(carName));
            }

            if (carType != null)
            {
                query = query.Where(c => c.Model.Type == carType);
            }

            return _mapper.Map<IEnumerable<CarModel>>(await query.ToListAsync());
        }

        public async Task<DataResult<CarModel>> FindByPageAsync(int skip = 0, int take = 5)
        {
            var carEntities = await _context.Cars
                .AsNoTracking()
                .OrderBy(x => x.Model.Name)
                .Include(x => x.CarImages)
                .Include(x => x.Owner)
                .Include(x => x.Model)
                .Include(x => x.Model.Brand)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var result = new DataResult<CarModel>
            {
                Data = _mapper.Map<IEnumerable<CarModel>>(carEntities),
                Count = await _context.Cars.CountAsync()
            };

            return result;
        }
    }
}