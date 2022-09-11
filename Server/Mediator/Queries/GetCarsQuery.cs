using CarMarket.Core.Car.Domain;
using MediatR;
using System.Collections.Generic;

namespace CarMarket.Server.Mediator.Queries
{
    public record GetCarsQuery() : IRequest<IEnumerable<CarModel>>;
}
