using Azure;
using CarsAndSales.Repository.Entity;
using CarsAndSales.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;
using CarsAndSales.Backend.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CarsAndSales.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarServices _carServices;

        public CarsController(ICarServices carServices)
        {
            _carServices = carServices;
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult> cars()
        {
            try
            {
                return Ok(await _carServices.All());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response { Status = "Error", Message = e.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> cars([FromBody] Car car)
        {
            try
            {
                await _carServices.Add(car);
                return StatusCode(StatusCodes.Status200OK, new Models.Response { Status = "Success", Message = "Car Successfully Added!" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response { Status = "Error", Message = e.Message });
            }
        }
    }
}
