using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Exceptions;
using CarMarket.Core.Car.Service;
using CarMarket.Core.User.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly IUserService _userService;

        public CarController(ICarService carService, IUserService userService, ILogger<CarController> logger)
        {
            _carService = carService;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<CarModel>>> Search(string carName, CarType? carType)
        {
            try
            {
                var result = await _carService.SearchAsync(carName, carType);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("GetCars")]
        public async Task<IActionResult> GetAllCars()
        {
            try
            {
                return Ok(await _carService.GetAllAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("GetCarsByPage")]
        public async Task<IActionResult> GetCarsByPage(int skip = 0, int take = 5)
        {
            try
            {
                return Ok(await _carService.GetByPageAsync(skip, take));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("GetCar/{carId:long}")]
        public async Task<ActionResult<CarModel>> GetCar(long carId)
        {
            try
            {
                var result = await _carService.GetAsync(carId);

                if (result is null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpDelete]
        [Route("DeleteCar/{carId:long}")]
        public async Task<IActionResult> DeleteCar(long carId)
        {
            var currentUser = HttpContext.User;

            var user = await _userService.GetByEmailAsync(currentUser.Identity.Name);

            if ((user != null) && ((user.Id == (await _carService.GetAsync(carId)).Owner.Id) ||
                currentUser.IsInRole("Admin")))
            {
                try
                {
                    await _carService.DeleteAsync(carId);
                }
                catch (CarNotFoundException e)
                {
                    return NotFound(e.Message);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error deleting car record");
                }
                return Ok();
            }

            return BadRequest("Access denied.");
        }

        [HttpPost]
        [Route("CreateCar")]
        public async Task<IActionResult> CreateCar([FromBody] CarModel carModel)
        {
            if (carModel is null)
            {
                return BadRequest();
            }

            try
            {
                var createdCar = await _carService.CreateAsync(carModel);

                return CreatedAtAction(nameof(GetCar), new { carId = createdCar.Id }, createdCar);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new car record");
            }
        }

        //[HttpGet("GetAllUserCars/{userId:long}")]
        //public async Task<ActionResult<IEnumerable<CarModel>>> GetAllUserCars(string userId)
        //{
        //    return Ok(await _carService.GetAllUserCarsAsync(userId));
        //}

        [HttpPut("UpdateCar/{carId:long}")]
        public async Task<ActionResult<CarModel>> UpdateCar(long carId, CarModel car)
        {
            if (carId != car.Id)
            {
                return BadRequest("Car ID mismatch");
            }

            try
            {
                return await _carService.UpdateCarAsync(carId, car);
            }
            catch (CarNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating car record");
            }
        }
    }
}