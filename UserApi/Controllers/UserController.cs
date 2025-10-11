using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UserApi.Data;
using UserApi.Models.Dtos;

namespace UserApi.Controllers
{
    [ApiController] // 🟢 Thêm dòng này
    [Route("api/[controller]")] // 🟢 Thêm dòng này
    public class UserController : ControllerBase
    {
        private readonly AppDBContext _context;

        public UserController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok("GetUsers endpoint");
        }





    }
}