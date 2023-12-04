using CarsAndSales.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAndSales.Services.Interface
{
    public interface ICarServices
    {
        Task<IEnumerable<Car>> All();
        Task<Car> GetById(Guid id);
        Task<bool> Add(Car entity);
        Task<bool> AddRange(List<Car> entities);
        Task<bool> Update(Car entity);
        Task<bool> Delete(Car entity);

    }
}
