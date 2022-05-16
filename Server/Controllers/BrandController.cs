using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Exceptions;
using CarMarket.Core.Car.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ICarBrandRepository _carBrandRepository;

        public BrandController(ICarBrandRepository carBrandRepository)
        {
            _carBrandRepository = carBrandRepository;
        }

        [HttpGet("{brandId:long}", Name = nameof(GetCarBrandByIdAsync))]
        public async Task<IActionResult> GetCarBrandByIdAsync(long brandId)
        {
            var result = await _carBrandRepository.FindByIdAsync(brandId);

            if (result is not null) return Ok(result);

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetCarBrandsAsync()
        {
            var result = await _carBrandRepository.FindAllAsync();

            if (result.Any()) return Ok(result);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddCarBrandAsync([FromBody] Brand brand)
        {
            var createdBrand = new Brand();
            try
            {
                createdBrand = await _carBrandRepository.AddAsync(brand);
            }
            catch(Exception e)
            {
                return BadRequest("Brand already exists");
            }

            return CreatedAtRoute(nameof(GetCarBrandByIdAsync), new { brandId = createdBrand.Id }, createdBrand);
        }

        [HttpPut("{brandId:long}")]
        public async Task<IActionResult> UpdateCarBrandAsync(long brandId, [FromBody] Brand brand)
        {
            var result = await _carBrandRepository.UpdateAsync(brandId, brand);

            return Ok(result);
        }

        [HttpDelete("{brandId:long}")]
        public async Task<IActionResult> DeleteCarBrandByIdAsync(long brandId)
        {
            await _carBrandRepository.DeleteAsync(brandId);

            return Ok();
        }
    }
}
