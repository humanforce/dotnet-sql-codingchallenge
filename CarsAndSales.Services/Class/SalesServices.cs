using CarsAndSales.Repository;
using CarsAndSales.Repository.Entity;
using CarsAndSales.Services.Interface;
using CarsAndSales.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace CarsAndSales.Services.Class
{
    public class SalesServices : ISalesServices
    {
        protected readonly AppDbContext _dbContext;
        internal DbSet<Sales> _dbSetSales;
        internal DbSet<Car> _dbSetCar;
        protected readonly ILogger<SalesServices> _logger;
        public SalesServices(AppDbContext dbContext, ILogger<SalesServices> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            this._dbSetSales = dbContext.Set<Sales>();
            this._dbSetCar = dbContext.Set<Car>();
        }

        public Task<IEnumerable<Sales>> All()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Add(Sales entity)
        {
            entity.Id = Guid.NewGuid();
            await _dbSetSales.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CarSalesModel>> GetSales(int month, int year)
        {

            //IEnumerable<CarSalesModel> test = Enumerable.Empty<CarSalesModel>();

            var  data = (from car in _dbSetCar
                       join sale in _dbSetSales on car.Id equals sale.CarId
                       where ((sale.Date.Month) == month && (sale.Date.Year == year))
                       select new { car.Id ,car.Name, car.Colour, sale.Date.Month, sale.Date.Year}).ToList();

            IEnumerable<CarSalesModel> result = (from d in data
                         group d by d.Id into g
                         select new CarSalesModel
                         {
                             carName = g.First().Name,
                             carColour = g.First().Colour,
                             quantity = g.Count(),
                             month = g.First().Month,
                             year = g.First().Year
                         });

            return result;
        }
    }
}
