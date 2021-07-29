using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Service;
using CarMarket.Core.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly ICarService _carService;

        public CarController(ICarService carService, ILogger<CarController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        [HttpGet("GetCars")]
        public async Task<IEnumerable<CarModel>> GetAllCars()
        {
            return await _carService.GetAllAsync();
        }

        [HttpGet("GetCarsByParameters")]
        public async Task<IActionResult> GetCarsByParameters([FromQuery] ModelParameters carParameters)
        {
            var cars = await _carService.GetAllByParametersAsync(carParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(cars.MetaData));

            return Ok(cars);
        }

        [HttpGet]
        [Route("GetCar/{carId}")]
        public async Task<CarModel> GetCar(long carId)
        {
            return await _carService.GetAsync(carId);
        }

        [HttpDelete]
        [Route("DeleteCar/{carId}")]
        public async Task<IActionResult> DeleteCar(long carId)
        {
            await _carService.DeleteAsync(carId);

            return NoContent();
        }
        
        [HttpPost]
        [Route("CreateCar")]
        public async Task<IActionResult> CreateCar([FromBody] CarModel carModel)
        {
            var carId = await _carService.CreateAsync(carModel);

            if (carId == default)
                return BadRequest(carModel + " is invalid");

            return Created("", carModel);
            //return CreatedAtAction("GetCar", new { id = carModel.Id }, carModel);
        }

        [HttpGet("GetCarsByPage")]
        public async Task<ActionResult<IEnumerable<CarModel>>> GetCarsByPage(int pageSize, int pageNumber)
        {
            var carModelList = await _carService.GetAllAsync();
            carModelList = carModelList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(carModelList);
        }

        [HttpPut("UpdateCar/{carId}")]
        public async Task<IActionResult> UpdateCar(long carId, [FromBody] CarModel car)
        {
            if (carId != car.Id)
            {
                return BadRequest();
            }

            await _carService.UpdateCar(carId, car);

            return NoContent();
        }
    }
}