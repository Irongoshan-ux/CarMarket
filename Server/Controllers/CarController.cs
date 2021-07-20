using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Service;
using CarMarket.Core.Image.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IImageService _carImageService;

        public CarController(ICarService carService, IImageService carImageService, ILogger<CarController> logger)
        {
            _carService = carService;
            _carImageService = carImageService;
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

        /// <summary>
        /// Think through
        /// </summary>
        //[HttpPost]
        //[Route("images/upload")]
        //public async Task<IActionResult> UploadCarImage()
        //{
        //    foreach (var file in Request.Form.Files)
        //    {
        //        var image = new ImageModel();
        //        image.ImageTitle = file.FileName;

        //        MemoryStream ms = new();
        //        file.CopyTo(ms);
        //        image.ImageData = ms.ToArray();

        //        ms.Close();
        //        ms.Dispose();

        //        await _carImageService.UploadAsync(image);
        //    }

        //    return Ok();
        //}

        //[HttpGet]
        //[Route("images/get")]
        //public async Task<IDictionary<string, string>> GetCarImages(long carId)
        //{
        //    var images = await _carImageService.GetAllAsync(carId);

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