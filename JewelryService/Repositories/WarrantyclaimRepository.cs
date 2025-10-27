using JewelryService.Models;
using Microsoft.EntityFrameworkCore;

namespace JewelryService.Repositories
{
    public class WarrantyclaimRepository : RepositoryBase<Warrantyclaim>, IWarrantyclaimRepository
    {
        public WarrantyclaimRepository(DbContext context) : base(context)
        {
        }

        
    }
}
