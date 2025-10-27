using JewelryService.Models;
using JewelryService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JewelryService.Repositories.Implementations
{
    public class WarrantyRepository : RepositoryBase<Warranty>, IWarrantyRepository
    {
        public WarrantyRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Warranty>> GetActiveWarrantiesAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            return await _dbSet
                .Where(w => w.Enddate >= today)
                .ToListAsync();
        }

        public async Task<IEnumerable<Warranty>> GetExpiredWarrantiesAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            return await _dbSet
                .Where(w => w.Enddate < today)
                .ToListAsync();
        }

        public async Task<IEnumerable<Warranty>> GetWarrantiesByCustomerIdAsync(int customerId)
        {
            var warranties = _dbSet
                .Where(w => w.Customerid == customerId)
                .ToList();

            // You need to explicitly return the Task<List<Warranty>> returned by ToListAsync()
            return warranties;
        }
    }
}