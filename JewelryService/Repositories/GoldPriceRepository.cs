using JewelryService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryService.Data.Repositories
{
    public class GoldPriceRepository : RepositoryBase<Goldprice>, IGoldPriceRepository
    {
        public GoldPriceRepository(DbContext context) : base(context)
        {
        }

        // 🔹 Lấy giá vàng mới nhất theo loại vàng
        public async Task<Goldprice?> GetLatestByTypeAsync(string goldType)
        {
            return await _dbSet
                .Where(g => g.Goldtype == goldType)
                .OrderByDescending(g => g.Pricedate)
                .FirstOrDefaultAsync();
        }

        // 🔹 Lấy giá vàng theo ngày cụ thể
        public async Task<IEnumerable<Goldprice>> GetByDateAsync(DateOnly date)
        {
            return await _dbSet
                .Where(g => g.Pricedate == date)
                .OrderBy(g => g.Goldtype)
                .ToListAsync();
        }

        // 🔹 Lấy giá vàng gần đây (ví dụ 7 ngày gần nhất)
        public async Task<IEnumerable<Goldprice>> GetRecentPricesAsync(int days = 7)
        {
            var fromDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-days));
            return await _dbSet
                .Where(g => g.Pricedate >= fromDate)
                .OrderByDescending(g => g.Pricedate)
                .ThenBy(g => g.Goldtype)
                .ToListAsync();
        }
    }
}
