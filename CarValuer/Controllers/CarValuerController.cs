using CarValuer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarValuer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarValuerController : ControllerBase
    {
        private readonly ILogger<CarValuerController> _logger;
        private readonly ApplicationDbContext _context;

        public CarValuerController(ApplicationDbContext context,
                                   ILogger<CarValuerController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{carId}", Name = "GetMaintenanceCost")]
        public async Task<IActionResult> GetMaintenanceCost(long carId, CancellationToken token)
        {
            var listResult = await _context.CarsMaintenanceCostsPerYear
                .Where(x => x.CarId == carId)
                .ToListAsync(token);

            if (listResult.Any())
                return Ok(listResult.First());

            return NotFound();
        }

        [HttpPost]
        [ActionName(nameof(CreateBrandModelAndMaintenanceCostPerYear))]
        public async Task<IActionResult> CreateBrandModelAndMaintenanceCostPerYear(Car carViewModel, CancellationToken token)
        {
            var cost = GenerateMaintenanceCostPerYear(carViewModel);

            var result = new CarsMaintenanceCostsPerYear
            {
                CarId = carViewModel.Model.Id,
                Cost = cost
            };

            try
            {
                await _context.CarsMaintenanceCostsPerYear.AddAsync(result, token);

                await _context.SaveChangesAsync(token);
            }
            catch
            {
                return BadRequest("Something went wrong");
            }

            return CreatedAtRoute("GetMaintenanceCost", new { carId = carViewModel.Id }, result);
        }

        private static int GenerateMaintenanceCostPerYear(Car carViewModel)
        {
            var majorityGrade = (int)carViewModel.Model.Type;
            var yearsOfCar = DateTime.Now.Year - carViewModel.Year.Year;

            if (majorityGrade <= 3)
            {
                if (yearsOfCar < 7)
                {
                    return Random.Shared.Next(300, 800);
                }
                else return Random.Shared.Next(300, 1500);
            }

            else if (majorityGrade <= 5)
            {
                if (yearsOfCar < 7)
                {
                    return Random.Shared.Next(1000, 2000);
                }
                else return Random.Shared.Next(1000, 3000);
            }
            else if (majorityGrade <= 6)
            {
                if (yearsOfCar < 7)
                {
                    return Random.Shared.Next(2800, 4000);
                }
                else return Random.Shared.Next(2800, 7000);
            }
            else
            {
                if (yearsOfCar < 7)
                    return Random.Shared.Next(6000, 10000);
                else return Random.Shared.Next(6000, 20000);
            }
        }
    }
}