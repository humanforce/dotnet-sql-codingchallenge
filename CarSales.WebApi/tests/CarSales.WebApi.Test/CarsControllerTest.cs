using CarSales.Common.Database;
using CarSales.WebApi.Controllers;
using CarSales.WebApi.Exceptions;
using CarSales.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CarSales.WebApi.Test
{
    public class CarsControllerTest
    {
        [Fact]
        public async Task Fetch_ReturnsOkObjectResult()
        {
            var data = new List<Car>().AsQueryable();
            var mock = new Mock<ICarsService>();
            var mockCarlist = new Mock<IQueryable<Car>>();

            mockCarlist.As<IQueryable<Car>>().Setup(m => m.Provider).Returns(data.Provider);
            mockCarlist.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(data.Expression);
            mockCarlist.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockCarlist.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mock.SetupGet(t => t.Cars).Returns(mockCarlist.Object);

            var controller = new CarsController(mock.Object);
            var result = await controller.Fetch();
            Assert.IsType<OkObjectResult>(result);
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
