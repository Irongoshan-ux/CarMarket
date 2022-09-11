using CarMarket.Core.Car.Domain;
using CarMarket.Server.Mediator.Commands;
using CarMarket.Server.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestMediatorController : ControllerBase
    {
        private readonly IMediator _mediator;


        public TestMediatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

       
        [HttpGet]
        public async Task<ActionResult> GetCars()
        {
            var cars = await _mediator.Send(new GetCarsQuery());

            return Ok(cars);
        }

        [HttpPost]
        public async Task<ActionResult> AddCar([FromBody] CarModel car)
        {
            await _mediator.Send(new AddCarCommand(car));

            return StatusCode(201);
        }
    }
}
