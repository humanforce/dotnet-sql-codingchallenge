using CarsAndSales.Repository.Entity;
using CarsAndSales.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAndSales.Services.Interface
{
    public interface ISalesServices
    {
        Task<IEnumerable<Sales>> All();
        Task<bool> Add(Sales entity);
        Task<IEnumerable<CarSalesModel>> GetSales(int month, int year);
    }
}
