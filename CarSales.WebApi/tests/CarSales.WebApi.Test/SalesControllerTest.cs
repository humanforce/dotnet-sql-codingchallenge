using CarSales.Common.Database;
using CarSales.WebApi.Controllers;
using CarSales.WebApi.Models;
using CarSales.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CarSales.WebApi.Test
{
    public class SalesControllerTest
    {

        [Fact]
        public void Fetch_ReturnsOkObjectResult()
        {
            var mock = new Mock<ISalesService>();

            mock.Setup(t => t.Find(It.IsAny<DateOnly?>(), It.IsAny<DateOnly?>())).Returns(new List<SaleViewModel>());

            var controller = new SalesController(mock.Object);
            var result = controller.Fetch("20220521","20220710");

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Fetch_GivenInvalidDates_ReturnsObjectResult()
        {
            var mock = new Mock<ISalesService>();

            mock.Setup(t => t.Find(It.IsAny<DateOnly?>(), It.IsAny<DateOnly?>())).Returns(new List<SaleViewModel>());

            var controller = new SalesController(mock.Object);

            //invalid start date
            var result = controller.Fetch("hello", "20220710");
            Assert.IsType<ObjectResult>(result);

            //invalid end date
            result = controller.Fetch("20220521", "world");
            Assert.IsType<ObjectResult>(result);

            //both dates are invalid
            result = controller.Fetch("hello", "world");
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedResult()
        {
            var mock = new Mock<ISalesService>();
            mock.Setup(t => t.Create(It.IsAny<Sale>())).ReturnsAsync(new Sale { });

            var controller = new SalesController(mock.Object);
            var result = await controller.Create(new SaleDto { CarId = new Guid(), Date = new DateOnly(2022,5,5) });

            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Create_GivenInvalidModelState_ReturnsObjectResult()
        {
            var mock = new Mock<ISalesService>();
            var controller = new SalesController(mock.Object);

            controller.ModelState.AddModelError(nameof(SaleDto.CarId), "Not found");
            var result = await controller.Create(new SaleDto { });

            Assert.IsType<ObjectResult>(result);
        }
    }
}
