using CarMarket.Core.Car.Domain;
using MediatR;

namespace CarMarket.Server.Mediator.Commands
{
    public record AddCarCommand(CarModel CarModel) : IRequest;
}