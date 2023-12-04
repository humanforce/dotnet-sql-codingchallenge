using CarSales.Common.Database;
using CarSales.WebApi.Models;

namespace CarSales.WebApi.Services
{
    public class SalesService : ISalesService
    {
        private readonly CarSalesDbContext _dbContext;

        public SalesService(CarSalesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<SaleViewModel> Find(DateOnly? startDate, DateOnly? endDate)
        {
            IQueryable<Sale> salesQuery = _dbContext.Sales;

            if (startDate != null)
            {
                salesQuery = salesQuery.Where(t => t.Date >= startDate);
            }

            if (endDate != null)
            {
                salesQuery = salesQuery.Where(t => t.Date <= endDate);
            }


            var salesGrouped = salesQuery
                                    .GroupBy(t => new { t.CarId, t.Date.Month, t.Date.Year })
                                    .Select(t => new
                                    {
                                        CarId = t.Key.CarId,
                                        Month = t.Key.Month,
                                        Year = t.Key.Year,
                                        Count = t.Count()
                                    });


            var result = from s in salesGrouped
                         join c in _dbContext.Cars on s.CarId equals c.Id
                         select new SaleViewModel
                         {
                             CarName = c.Name,
                             CarColour = c.Colour,
                             Month = s.Month,
                             Year = s.Year,
                             Quantity = s.Count
                         };

            return result;
        }

        public async Task<Sale> Create(Sale sale)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {

                    if (!_dbContext.Cars.Any(t => t.Id == sale.CarId))
                    {
                        throw new ArgumentException(paramName: nameof(Sale.CarId), message: "Not found");
                    }

                    _dbContext.Sales.Add(sale);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return sale;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
