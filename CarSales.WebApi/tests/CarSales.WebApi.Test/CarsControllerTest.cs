using CarSales.Common.Database;
using CarSales.WebApi.Controllers;
using CarSales.WebApi.Exceptions;
using CarSales.WebApi.Services;
using CarSales.WebApi.Test.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CarSales.WebApi.Test
{
    public class CarsControllerTest : IClassFixture<CarSalesDbContextFixture>
    {
        private readonly CarSalesDbContextFixture _fixture;
        public CarsControllerTest(CarSalesDbContextFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task Fetch_ReturnsOkObjectResult()
        {
            using(var context = _fixture.CreateContext())
            {
                var controller = new CarsController(new CarsService(context));
                var result = await controller.Fetch();
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async Task Create_ReturnsCreatedObjectResult()
        {
            var mock = new Mock<ICarsService>();

            mock.Setup(t => t.Create(It.IsAny<Models.CarModifyDto>())).ReturnsAsync(new Car
            {
                Id = new Guid(),
                Name = "Test1",
                Colour = "colour",
                Price = 12345
            });

            var controller = new CarsController(mock.Object);
            var result = await controller.Create(new Models.CarCreateDto { });
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Create_GivenInvalidModelState_ReturnsObjectResult()
        {
            var mock = new Mock<ICarsService>();
            var controller = new CarsController(mock.Object);
            controller.ModelState.AddModelError(nameof(Models.CarCreateDto.Name), "Required");

            var result = await controller.Create(new Models.CarCreateDto { });
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsOkObjectResult()
        {
            var mock = new Mock<ICarsService>();

            mock.Setup(t => t.Update(It.IsAny<Guid>(),It.IsAny<Models.CarModifyDto>())).ReturnsAsync(new Car
            {
                Id = new Guid(),
                Name = "Test1",
                Colour = "colour",
                Price = 12345
            });

            var controller = new CarsController(mock.Object);
            var result = await controller.Update(new Guid(), new Models.CarModifyDto { });
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Update_GivenInvalidModelState_ReturnsObjectResult()
        {
            var mock = new Mock<ICarsService>();
            var controller = new CarsController(mock.Object);
            controller.ModelState.AddModelError(nameof(Models.CarCreateDto.Name), "Too long");

            var result = await controller.Update(new Guid(), new Models.CarModifyDto { });
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult()
        {
            var mock = new Mock<ICarsService>();
            var controller = new CarsController(mock.Object);

            var result = await controller.Delete(new Guid());
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_GivenNotFoundExceptionThrown_ReturnsNotFoundResult()
        {
            var mock = new Mock<ICarsService>();
            mock.Setup(t => t.Delete(It.IsAny<Guid>())).ThrowsAsync(new NotFoundException());

            var controller = new CarsController(mock.Object);

            var result = await controller.Delete(new Guid());
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_GivenDeleteExceptionThrown_ReturnsConflictResult()
        {
            var mock = new Mock<ICarsService>();
            mock.Setup(t => t.Delete(It.IsAny<Guid>())).ThrowsAsync(new DeleteException());

            var controller = new CarsController(mock.Object);

            var result = await controller.Delete(new Guid());
            Assert.IsType<ConflictObjectResult>(result);
        }

    }
}
