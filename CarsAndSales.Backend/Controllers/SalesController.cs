using CarsAndSales.Repository.Entity;
using CarsAndSales.Services.Class;
using CarsAndSales.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarsAndSales.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesServices _salesServices;

        public SalesController(ISalesServices salesServices)
        {
            _salesServices = salesServices;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> sales(int month, int year)
        {

            try
            {
                return Ok(await _salesServices.GetSales(month, year));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response { Status = "Error", Message = e.Message });
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> sales([FromBody] Sales sales)
        {
            try
            {
                await _salesServices.Add(sales);
                return StatusCode(StatusCodes.Status200OK, new Models.Response { Status = "Success", Message = "Sales Successfully Added!" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response { Status = "Error", Message = e.Message });
            }
        }
    }
}
