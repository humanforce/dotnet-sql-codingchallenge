using CarSales.WebApi.Exceptions;
using CarSales.WebApi.Services;
using CarSales.WebApi.Test.Fixtures;

namespace CarSales.WebApi.Test
{
    public class CarServiceTests : IClassFixture<CarSalesDbContextFixture>
    {
        private readonly CarSalesDbContextFixture _fixture;
        public CarServiceTests(CarSalesDbContextFixture fixture) {
            _fixture = fixture;
        }

        [Fact]
        public void GetCars_ShouldNotBeNullOrEmpty()
        {
            var context = _fixture.GetContext();
            var service = new CarsService(context);

            Assert.NotNull(service.Cars);
            Assert.True(service.Cars.Any());
        }

        [Fact]
        public async Task Create_ShouldAddEntityToDatabase()
        {
            using (var context = _fixture.CreateContext())
            {
                var service = new CarsService(context);

                var created = await service.Create(new Models.CarModifyDto
                {
                    Name = "TestCar1",
                    Colour = "Green",
                    Price = 1510000
                });

                Assert.NotNull(created);
                Assert.NotNull(context.Cars.FirstOrDefault(t => t.Id == created.Id));
                Assert.Equal("TestCar1", created.Name);
                Assert.Equal("Green", created.Colour);
                Assert.Equal(1510000, created.Price);
            }
        }

        [Fact]
        public async Task Update_GivenNonExistingRecord_ShouldThrowNotFoundException()
        {
            var context = _fixture.GetContext();
            var service = new CarsService(context);

            await Assert.ThrowsAsync<NotFoundException>(async () => await service.Update(Guid.Parse("00000000-a5a0-441d-ada3-38f398d0e15a"), new Models.CarModifyDto { }));
        }

        [Fact]
        public async Task Update_GivenEmptyDto_ShouldHaveNoChanges()
        {
            using (var context = _fixture.CreateContext())
            {
                context.Cars.Add(new Common.Database.Car
                {
                    Id = Guid.Parse("fd5cfbd9-cecc-460f-a7cd-6d9a079c910d"),
                    Name = "TestCar2",
                    Colour = "Yellow",
                    Price = 121000
                });
                context.SaveChanges();

                var service = new CarsService(context);

                await service.Update(Guid.Parse("fd5cfbd9-cecc-460f-a7cd-6d9a079c910d"), new Models.CarModifyDto { });

                var updated = context.Cars.FirstOrDefault(t => t.Id == Guid.Parse("fd5cfbd9-cecc-460f-a7cd-6d9a079c910d"));

                Assert.NotNull(updated);
                Assert.Equal("TestCar2", updated.Name);
                Assert.Equal("Yellow", updated.Colour);
                Assert.Equal(121000, updated.Price);
            }
        }

        [Fact]
        public async Task Update_ShouldUpdate()
        {
            using (var context = _fixture.CreateContext())
            {
                context.Cars.Add(new Common.Database.Car
                {
                    Id = Guid.Parse("727625ff-ba10-4a61-8e35-45da5e7cb9f9"),
                    Name = "TestCar3",
                    Colour = "White",
                    Price = 125000
                });
                context.SaveChanges();

                var service = new CarsService(context);

                await service.Update(Guid.Parse("727625ff-ba10-4a61-8e35-45da5e7cb9f9"), new Models.CarModifyDto { 
                    Name = "TestUpdated3",
                    Colour = "Black"
                });

                var updated = context.Cars.FirstOrDefault(t => t.Id == Guid.Parse("727625ff-ba10-4a61-8e35-45da5e7cb9f9"));

                Assert.NotNull(updated);
                Assert.Equal("TestUpdated3", updated.Name);
                Assert.Equal("Black", updated.Colour);
                Assert.Equal(125000, updated.Price);
            }
        }

        [Fact]
        public async Task Delete_GivenCarWithSales_ShouldThrowDeleteException()
        {
            var context = _fixture.GetContext();
            var service = new CarsService(context);
            
            var r =  context.Sales.Any(t => t.CarId == Guid.Parse("5cc6e6c4-22d2-4fee-8632-1f0a86e5f162"));
            await Assert.ThrowsAsync<DeleteException>(async () => await service.Delete(Guid.Parse("5cc6e6c4-22d2-4fee-8632-1f0a86e5f162")));
        }

        [Fact]
        public async Task Delete_GivenCarWithoutSales_ShouldDelete()
        {
            using (var context = _fixture.CreateContext())
            {
                context.Cars.Add(new Common.Database.Car
                {
                    Id = Guid.Parse("3918289c-5478-4aa8-8ea0-32a5acf61a99"),
                    Name = "CarNoSales",
                    Colour = "Yellow",
                    Price = 121000
                });
                context.SaveChanges();

                var service = new CarsService(context);

                await service.Delete(Guid.Parse("3918289c-5478-4aa8-8ea0-32a5acf61a99"));

                Assert.False(context.Cars.Any(t => t.Id == Guid.Parse("3918289c-5478-4aa8-8ea0-32a5acf61a99")));
            }
        }

        [Fact]
        public async Task Delete_GivenNonExistingRecord_ShouldThrowNotFoundException()
        {
            var context = _fixture.GetContext();
            var service = new CarsService(context);

            await Assert.ThrowsAsync<NotFoundException>(async () => await service.Delete(Guid.Parse("00000000-a5a0-441d-ada3-38f398d0e15a")));
        }

    }
}
