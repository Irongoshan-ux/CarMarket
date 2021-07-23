using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Service;
using CarMarket.Core.Image.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpGet]
        [Route("GetCar/{carId}")]
        public async Task<CarModel> GetCar(long carId)
        {
            return await _carService.GetAsync(carId);
        }

        [HttpDelete]
        [Route("DeleteCar/{carId}")]
        public async Task DeleteCar(long carId)
        {
            await _carService.DeleteAsync(carId);
        }
        
        [HttpPost]
        [Route("CreateCar")]
        public async Task<IActionResult> CreateCar([FromBody] CarModel carModel)
        {
            var carId = await _carService.CreateAsync(carModel);

            if (carId == default)
                return BadRequest(carModel + " is invalid");

            return CreatedAtAction("GetCar", new { id = carModel.Id }, carModel);
        }

        [HttpGet("GetCarsByPage")]
        public async Task<ActionResult<IEnumerable<CarModel>>> GetCarsByPage(int pageSize, int pageNumber)
        {
            var carModelList = await _carService.GetAllAsync();
            carModelList = carModelList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(carModelList);
        }

        //private async Task<IDictionary<string, string>> GetCarImages(CarModel car)
        //{
        //    var images = _carService.

        //    Dictionary<string, string> frontImages = new Dictionary<string, string>(images.Count);

        //    Parallel.ForEach(images, image =>
        //    {
        //        string imageBase64Data = Convert.ToBase64String(image.ImageData);

        //        string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

        //        frontImages.Add(imageDataURL, imageBase64Data);
        //    });

        //    return frontImages;
        //}
    }
}