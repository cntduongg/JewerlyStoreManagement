using JewelryService.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JewelryService.Data.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetActiveUsersAsync();
        Task<bool> IsEmailTakenAsync(string email);
        Task<bool> IsPhoneTakenAsync(string phone);
    }
}
