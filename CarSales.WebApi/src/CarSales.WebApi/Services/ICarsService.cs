using CarSales.Common.Database;
using CarSales.WebApi.Models;

namespace CarSales.WebApi.Services
{
    public interface ICarsService
    {
        IQueryable<Car> Cars { get; }

        /// <summary>
        /// Add Car record to database
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        Task<Car> Create(CarModifyDto car);

        /// <summary>
        /// Updates specified Car record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carDto"></param>
        /// <exception cref="NotFoundException">Thrown if record is not found</exception>
        /// <returns></returns>
        Task<Car> Update(Guid id, CarModifyDto carDto);

        /// <summary>
        /// Deletes Car record using <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotFoundException">Thrown if record is not found</exception>
        /// <exception cref="DeleteException">car record has associated sale records</exception>
        /// <returns></returns>
        Task Delete(Guid id);
    }
}
