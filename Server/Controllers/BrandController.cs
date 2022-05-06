using CarMarket.Core.Car.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ICarModelRepository _carModelRepository;

        public BrandController(ICarModelRepository carModelRepository)
        {
            _carModelRepository = carModelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarBrandsAsync()
        {
            var result = await _carModelRepository.FindAllBrandsAsync();

            if (result.Any()) return Ok(result);

            return NotFound();
        }
    }
}
