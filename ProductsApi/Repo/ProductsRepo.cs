using Microsoft.EntityFrameworkCore;
using ProductsApi.Data;
using ProductsApi.Models;
using ProductsApi.Repo.IRepo;

namespace ProductsApi.Repo
{
    public class ProductsRepo : IProductsRepo
    {
        private readonly AppDbContext _context;

        public ProductsRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => !p.Isdeleted)
                .OrderBy(p => p.Productid)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Productid == id && !p.Isdeleted);
        }

        public async Task<Product> GetByProductCodeAsync(string productCode)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Productcode == productCode && !p.Isdeleted);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            product.Createdat = DateTime.UtcNow;
            product.Updatedat = DateTime.UtcNow;
            product.Isdeleted = false;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(product.Productid);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            product.Updatedat = DateTime.UtcNow;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(product.Productid);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            // Soft delete
            product.Isdeleted = true;
            product.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products
                .AnyAsync(p => p.Productid == id && !p.Isdeleted);
        }

        public async Task<bool> ProductCodeExistsAsync(string productCode, int? excludeProductId = null)
        {
            var query = _context.Products.Where(p => p.Productcode == productCode && !p.Isdeleted);

            if (excludeProductId.HasValue)
            {
                query = query.Where(p => p.Productid != excludeProductId.Value);
            }

            return await query.AnyAsync();
        }
    }
}