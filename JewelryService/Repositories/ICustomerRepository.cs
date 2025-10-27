using JewelryService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JewelryService.Data.Repositories
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<Customer?> GetByPhoneAsync(string phone);
        Task<IEnumerable<Customer>> SearchByNameAsync(string keyword);
        Task<IEnumerable<Customer>> GetActiveCustomersAsync();
        Task<bool> IsPhoneTakenAsync(string phone);
    }
}
