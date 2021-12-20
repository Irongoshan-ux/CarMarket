using CarMarket.Core.Car.Repository;
using CarMarket.Server.Mediator.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarMarket.Server.Mediator.Handlers
{
    public class AddCarHandler : IRequestHandler<AddCarCommand, Unit>
    {
        private readonly ICarRepository _carRepository;

        public AddCarHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Unit> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            await _carRepository.AddAsync(request.CarModel);

            return Unit.Value;
        }
    }
}
