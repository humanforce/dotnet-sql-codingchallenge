using CarSales.Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CarSales.WebApi.Test.Fixtures
{
    public class CarSalesDbContextFixture : IDisposable
    {
        private readonly DbContextOptions<CarSalesDbContext> _options;
        private readonly CarSalesDbContext _context;

        public CarSalesDbContextFixture()
        {
            _options = new DbContextOptionsBuilder<CarSalesDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                    .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                                    .Options;

            _context = new CarSalesDbContext(_options);

            var carA = new Car { Id = Guid.Parse("1dfb9d63-cba8-4628-af22-1a99d4dd2903"), Name = "CarA", Colour = "Blue", Price = 1220000 };
            var carB = new Car { Id = Guid.Parse("734748bb-960e-480d-b953-409cc292f8a0"), Name = "CarB", Colour = "Green", Price = 1670000 };
            var carC = new Car { Id = Guid.Parse("5cc6e6c4-22d2-4fee-8632-1f0a86e5f162"), Name = "CarC", Colour = "Purple", Price = 785000 };


            _context.Cars.AddRange(carA, carB, carC);
            _context.SaveChanges();

            _context.Sales.AddRange([
                new Sale { CarId = carA.Id, Date = new DateOnly(2022, 5, 5) },
                new Sale { CarId = carA.Id, Date = new DateOnly(2022, 5, 12) },
                new Sale { CarId = carA.Id, Date = new DateOnly(2022, 5, 18) },
                new Sale { CarId = carA.Id, Date = new DateOnly(2023, 3, 18) },
                new Sale { CarId = carA.Id, Date = new DateOnly(2023, 3, 22) },
                new Sale { CarId = carA.Id, Date = new DateOnly(2023, 8, 5) },

                new Sale { CarId = carB.Id, Date = new DateOnly(2023, 7, 10) },
                new Sale { CarId = carB.Id, Date = new DateOnly(2023, 7, 14) },
                new Sale { CarId = carB.Id, Date = new DateOnly(2023, 7, 17) },

                new Sale { CarId = carC.Id, Date = new DateOnly(2021, 5, 9) },
                new Sale { CarId = carC.Id, Date = new DateOnly(2021, 5, 21) },
                new Sale { CarId = carC.Id, Date = new DateOnly(2023, 10, 11) },
                new Sale { CarId = carC.Id, Date = new DateOnly(2023, 10, 17) },
                new Sale { CarId = carC.Id, Date = new DateOnly(2023, 10, 17) },
            ]);
            _context.SaveChanges();
        }

        public CarSalesDbContext CreateContext()
        {
            return new CarSalesDbContext(_options);
        }

        public CarSalesDbContext GetContext()
        {
            return _context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
