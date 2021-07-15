﻿using CarMarket.Core.Car.Domain;
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

        public async Task DeleteAsync(long carId)
        {
            var carEntity = await _context.Cars
                .Where(x => x.Id == carId)
                .FirstOrDefaultAsync();

            //var carEntity = _carConverter.ToEntity(carModel);

            _context.Cars.Remove(carEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<CarModel> FindOneByNameAsync(string name)
        {
            var carEntity = await _context.Cars.FirstOrDefaultAsync(x => x.Name == name);

            return _carConverter.ToModel(carEntity);
        }

        public async Task<IEnumerable<CarModel>> FindAllByNameAsync(string name)
        {
            var carEntity = await _context.Cars
                .Where(x => x.Name == name)
                .ToListAsync();

            var carModels = new List<CarModel>(carEntity.Count);

            Parallel.ForEach(carEntity, x =>
            {
                carModels.Add(_carConverter.ToModel(x));
            });

            return carModels;
        }
    }
}
