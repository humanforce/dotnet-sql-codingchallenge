using CarSales.Common.Database;
using Microsoft.EntityFrameworkCore;
using CarSales.WebApi.Models;
using CarSales.WebApi.Exceptions;

namespace CarSales.WebApi.Services
{
    public class CarsService : ICarsService
    {
        private readonly CarSalesDbContext _dbContext;

        public CarsService(CarSalesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Car> Cars => _dbContext.Cars.AsNoTracking();

        public async Task<Car> Create(CarModifyDto car)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var record = car.ToDbModel();

                    _dbContext.Cars.Add(record);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return record;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<Car> Update(Guid id, CarModifyDto carDto)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                var record = await _dbContext.Cars.FirstOrDefaultAsync(t => t.Id == id);

                if (record == null)
                {
                    throw new NotFoundException();
                }

                try
                {
                    record = carDto.ToDbModel(record);

                    _dbContext.Cars.Update(record);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return record;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task Delete(Guid id)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                var car = await _dbContext.Cars.FirstOrDefaultAsync(t => t.Id == id);

                if (car == null)
                {
                    throw new NotFoundException();
                }

                if (await _dbContext.Sales.AnyAsync(t => t.CarId == id))
                {
                    throw new DeleteException("Cannot delete car record as it has associated sale records");
                }

                try
                {
                    _dbContext.Cars.Remove(car);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbUpdateException)
                {
                    await transaction.RollbackAsync();
                    throw new DeleteException("Cannot delete car record as it has associated sale records");
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
