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
                .Include(p => p.Category) // Join với bảng categories
                .OrderBy(p => p.Productid)
                .ToListAsync();
        }
    }
    }

