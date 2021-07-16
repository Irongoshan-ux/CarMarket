using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Service;
using CarMarket.Core.Image.Domain;
using CarMarket.Core.Image.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CarMarket.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly ICarService _carService;
        private readonly ICarImageService _carImageService;

        public CarController(ICarService carService, ICarImageService carImageService, ILogger<CarController> logger)
        {
            _carService = carService;
            _carImageService = carImageService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<CarModel>> GetAll()
        {
            return await _carService.GetAllAsync();
        }

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

        [HttpPost]
        public async Task<IActionResult> UploadCarImage()
        {
            foreach (var file in Request.Form.Files)
            {
                var image = new CarImage();
                image.ImageTitle = file.FileName;

                MemoryStream ms = new();
                file.CopyTo(ms);
                image.ImageData = ms.ToArray();

                ms.Close();
                ms.Dispose();

                await _carImageService.UploadAsync(image);
            }

            return Ok();
        }

        // TODO: Realize method GET

        //[HttpGet]
        //public IActionResult GetCarImages()
        //{
        //    CarImage image = db.Images.OrderByDescending
        //(i => i.Id).SingleOrDefault();
        //    string imageBase64Data =
        //Convert.ToBase64String(img.ImageData);
        //    string imageDataURL =
        //string.Format("data:image/jpg;base64,{0}",
        //imageBase64Data);
        //    ViewBag.ImageTitle = img.ImageTitle;
        //    ViewBag.ImageDataUrl = imageDataURL;
        //    return View("Index");
        //}
    }
}
