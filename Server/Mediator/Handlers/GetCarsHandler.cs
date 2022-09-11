using CarMarket.Core.Car.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using CarMarket.Core.Car.Domain;
using CarMarket.Server.Mediator.Queries;
using MediatR;

namespace CarMarket.Server.Mediator.Handlers
{
    public class GetCarsHandler : IRequestHandler<GetCarsQuery, IEnumerable<CarModel>>
    {
        private readonly ICarRepository _carRepository;

        public GetCarsHandler(ICarRepository repository)
        {
            _carRepository = repository;
        }

        public async Task<IEnumerable<CarModel>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
        {
            return await _carRepository.FindAllAsync();
        }
    }
}
