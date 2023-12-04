using Microsoft.AspNetCore.Mvc;
using CarSales.Common.Database;
using CarSales.WebApi.Exceptions;
using CarSales.WebApi.Models;
using CarSales.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace CarSales.WebApi.Controllers
{
    [ApiController]
    [Route("cars")]
    public class CarsController : ControllerBase
    {
        private readonly ICarsService _carsService;
        public CarsController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        /// <summary>
        /// Fetch car records
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "read:cars")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<Car>), Description = "Success")]
        public async Task<IActionResult> Fetch()
        {
            return Ok(await _carsService.Cars.ToListAsync());
        }

        /// <summary>         
        /// Get car record by <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "read:cars")]
        [SwaggerResponse(200, Type = typeof(Car), Description = "Success")]
        [SwaggerResponse(404, Description = "Not found")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var car = await _carsService.Cars.FirstOrDefaultAsync(t => t.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        /// <summary>
        /// Create car record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "write:cars")]
        [SwaggerResponse(201, Type = typeof(Car), Description = "Successfully created")]
        [SwaggerResponse(400, Description = "Validation errors")]
        public async Task<IActionResult> Create([FromBody] CarCreateDto carDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var car = await _carsService.Create(carDto);

            return Created($"car/{car.Id}", car);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "write:cars")]
        [SwaggerResponse(200, Description = "Successfully updated")]
        [SwaggerResponse(400, Description = "Validation errors")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CarModifyDto carDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var car = await _carsService.Update(id, carDto);

            return Ok(car);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "write:cars")]
        [SwaggerResponse(204, Description = "Successfully deleted")]
        [SwaggerResponse(404, Description = "Not found")]
        [SwaggerResponse(409, Description = "Car record has associated sales")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _carsService.Delete(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DeleteException ex)
            {
                return Conflict(ex.Message);
            }

        }
    }
}
