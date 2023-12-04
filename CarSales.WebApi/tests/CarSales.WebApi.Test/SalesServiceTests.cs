using CarSales.Common.Database;
using CarSales.WebApi.Services;
using CarSales.WebApi.Test.Fixtures;

namespace CarSales.WebApi.Test
{
    public class SalesServiceTests : IClassFixture<CarSalesDbContextFixture>
    {
        private readonly CarSalesDbContextFixture _fixture;

        public SalesServiceTests(CarSalesDbContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Create_ShouldCreate()
        {
            using(var context = _fixture.CreateContext())
            {
                var id = Guid.Parse("e4bc1003-3cb5-47f8-9d48-40b3e0930359");
                context.Cars.Add(new Common.Database.Car
                {
                    Id = id,
                    Name = "SalesTestCar1",
                    Colour = "Lavender",
                    Price = 1456900
                });
                context.SaveChanges();

                var service = new SalesService(context);
                var createdResult = await service.Create(new Common.Database.Sale { CarId = id, Date = new DateOnly(2021,7,20)});

                Assert.NotNull(createdResult);
                Assert.True(context.Sales.Any(t => t.Id == createdResult.Id));
            }
        }

        [Fact]
        public async Task Create_GivenNonExistingCarRecord_ShouldThrowArgumentException()
        {
            using (var context = _fixture.CreateContext())
            {
                var id = Guid.Parse("00000000-fa14-442b-8ee4-e9784a60a8a8");

                var service = new SalesService(context);

                var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await service.Create(new Common.Database.Sale { CarId = id, Date = new DateOnly(2021, 7, 20) }));
                
                Assert.Equal(nameof(Sale.CarId), ex.ParamName);
            }
        }

        [Fact]
        public void Find_ShouldReturnCorrectResults()
        {
            var context = _fixture.GetContext();
            var service = new SalesService(context);


            var result = service.Find(new DateOnly(2023, 3, 22), new DateOnly(2023, 10, 11)).ToList();

            //Total count should be 4
            Assert.Equal(4, result.Count);

            //Total count for CarA should be 2
            Assert.Equal(2, result.Where(t => t.CarName == "CarA").Count());

            //Total count for CarB should be 1
            Assert.Single(result.Where(t => t.CarName == "CarB"));

            //Total count for CarC should be 1
            Assert.Single(result.Where(t => t.CarName == "CarC"));

            //CarA 2023-3 should have quantity 1
            var testSubject = result.FirstOrDefault(t => t.CarName == "CarA" && t.Year == 2023 && t.Month == 3);
            Assert.NotNull(testSubject);
            Assert.Equal(1, testSubject.Quantity);

            //CarB 2023-7 should have quantity 3
            testSubject = result.FirstOrDefault(t => t.CarName == "CarB" && t.Year == 2023 && t.Month == 7);
            Assert.NotNull(testSubject);
            Assert.Equal(3, testSubject.Quantity);
        }
    }
}
