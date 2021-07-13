using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly ICarService _carService;

        public CarController(ICarService carService, ILogger<CarController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<CarModel>> GetAll()
        {
            return await _carService.GetAllAsync();
        }

        //TODO: Methods below don't work

        [HttpGet]
        [Route("{carId}")]
        public async Task<CarModel> Get(long carId)
        {
            return await _carService.GetAsync(carId);
        }

        [HttpPost]
        [Route("delete/{carId}")]
        public async Task Delete(long carId)
        {
            await _carService.DeleteAsync(carId);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CarModel carModel)
        {
            var carId = await _carService.CreateAsync(carModel);

            if (carId == default)
                return BadRequest(carModel + " is invalid");

            return Ok(carModel);
        }
    }
}
