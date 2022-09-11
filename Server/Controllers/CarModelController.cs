using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        private readonly ICarModelRepository _carModelRepository;

        public CarModelController(ICarModelRepository carModelRepository)
        {
            _carModelRepository = carModelRepository;
        }

        [HttpGet("{modelId:long}", Name = nameof(GetCarModelAsync))]
        public async Task<IActionResult> GetCarModelAsync(long modelId)
        {
            var result = await _carModelRepository.FindByIdAsync(modelId);

            if (result is not null) return Ok(result);

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetCarModelsAsync()
        {
            var result = await _carModelRepository.FindAllAsync();

            if (result.Any()) return Ok(result);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarModel([FromBody] Model carModel)
        {
            if (carModel is null || carModel.Brand is null)
            {
                return BadRequest("Check the data you provide");
            }

            try
            {
                var createdModel = await _carModelRepository.AddAsync(carModel);

                return CreatedAtRoute(nameof(GetCarModelAsync), new { modelId = createdModel.Id }, createdModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new car record");
            }
        }
    }
}
