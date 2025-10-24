using Microsoft.EntityFrameworkCore;
using ProductsApi.Data;
using ProductsApi.Models;
using ProductsApi.Repo.IRepo;

namespace ProductsApi.Repo
{
    public class ProductsRepo : IProductsRepo
    {
        private readonly AppDb _context;

        public ProductsRepo(AppDb context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.products
                .Include(p => p.Category) // Join với bảng categories
                .OrderBy(p => p.Productid)
                .ToListAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
    }

