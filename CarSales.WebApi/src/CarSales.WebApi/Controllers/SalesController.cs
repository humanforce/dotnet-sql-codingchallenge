using CarSales.Common.Database;
using CarSales.WebApi.Models;
using CarSales.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Globalization;

namespace CarSales.WebApi.Controllers
{
    [ApiController]
    [Route("sales")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet]
        [SwaggerOperation("Fetch sale records grouped by month")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<SaleViewModel>), Description = "Success")]
        public IActionResult Fetch([FromQuery] string? startDate = null, [FromQuery] string? endDate = null)
        {
            DateOnly? sd = null, ed = null;


            if (!string.IsNullOrWhiteSpace(startDate))
            {

                try
                {
                    sd = DateOnly.ParseExact(startDate.Trim(), "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                catch
                {
                    ModelState.AddModelError(nameof(startDate), "Format should be yyyyMMdd");
                }
            }

            if (!string.IsNullOrWhiteSpace(endDate))
            {

                try
                {
                    ed = DateOnly.ParseExact(endDate.Trim(), "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                catch
                {
                    ModelState.AddModelError(nameof(endDate), "Format should be yyyyMMdd");
                }
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            return Ok(_salesService.Find(sd, ed));
        }

        [HttpPost]
        [Authorize(Policy = "write:sales")]
        [SwaggerOperation("Create sale record")]
        [SwaggerResponse(201, Type = typeof(Sale), Description = "Success")]
        [SwaggerResponse(400, Description = "Validation errors")]
        public async Task<IActionResult> Create([FromBody] SaleDto saleDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var sale = new Sale
            {
                CarId = saleDto.CarId.Value,
                Date = saleDto.Date.Value,
            };

            try
            {
                sale = await _salesService.Create(sale);
            }
            catch (ArgumentException ex)
            {
                if (ex.ParamName != null)
                {
                    ModelState.AddModelError(ex.ParamName, ex.Message);
                    return ValidationProblem(ModelState);
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return Created($"sales/{sale.Id}", sale);
        }
    }
}
