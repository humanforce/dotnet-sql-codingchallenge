using CarSales.Common.Database;
using CarSales.WebApi.Models;

namespace CarSales.WebApi.Services
{
    public interface ISalesService
    {
        public IEnumerable<SaleViewModel> Find(DateOnly? startDate, DateOnly? endDate);
        Task<Sale> Create(Sale sale);
    }
}
