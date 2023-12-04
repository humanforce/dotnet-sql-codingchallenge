using CarsAndSales.Repository;
using CarsAndSales.Repository.Entity;
using CarsAndSales.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAndSales.Services.Class
{
    public class CarServices : ICarServices
    {
        protected readonly AppDbContext _dbContext;
        internal DbSet<Car> _dbSet;
        protected readonly ILogger<CarServices> _logger;

        public CarServices(AppDbContext dbContext, ILogger<CarServices> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            this._dbSet = dbContext.Set<Car>();
        }

        public async Task<bool> Add(Car entity)
        {
            entity.Id = Guid.NewGuid();
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddRange(List<Car> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Car>> All()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<bool> Delete(Car entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Car?> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> Update(Car entity)
        {
            _dbSet.Update(entity);

            return true;
        }
    }
}
