using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using UserApi.Models.Dtos;
using UserApi.Models.Entities;
using UserApi.Repositories;
using UserApi.Repositories.IRepo;
using UserApi.Service.IService;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly iUserRepo _userRepository;

        public AuthController(IAuthService authService, iUserRepo userRepository)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto registerDto)
        {
            if (registerDto == null || string.IsNullOrEmpty(registerDto.Username) || string.IsNullOrEmpty(registerDto.Password) ||
                string.IsNullOrEmpty(registerDto.FullName) || string.IsNullOrEmpty(registerDto.Email))
            {
                return BadRequest("All fields (Username, Password, FullName, Email) are required");
            }

            var (success, message) = await _authService.RegisterAsync(registerDto);
            if (!success)
                return BadRequest(message);

            var user = await _userRepository.GetByAccountAsync(registerDto.Username);
            if (user == null)
                return BadRequest("User not found after registration");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role ?? "User"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", user.FullName ?? string.Empty)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(1)
                });

            return Ok(new { Message = message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Username and password are required");
            }

            var (success, message) = await _authService.LoginAsync(loginDto);
            if (!success)
                return Unauthorized(message);

            var user = await _userRepository.GetByAccountAsync(loginDto.Username);
            if (user == null)
                return Unauthorized("User not found");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role ?? "User"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", user.FullName ?? string.Empty)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(1)
                });

            return Ok(new { Message = message });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _authService.LogoutAsync();
            return Ok(new { Message = "Logout successful" });
        }

        [HttpGet("check-users")]
        public async Task<IActionResult> CheckUsers()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return Ok(new { 
                    Count = users.Count(), 
                    Users = users.Select(u => new { 
                        u.UserId, 
                        u.Username, 
                        u.FullName, 
                        u.Email, 
                        u.Role,
                        u.CreatedAt 
                    }) 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { Message = "API is running", Timestamp = DateTime.Now });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            try
            {
                var user = await _userRepository.GetByAccountAsync(dto.Username);
                if (user == null)
                    return BadRequest("User not found");

                // Hash password mới với salt mới
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword, BCrypt.Net.BCrypt.GenerateSalt());
                user.UpdatedAt = DateTime.Now;
                
                await _userRepository.SaveAsync();
                return Ok(new { Message = "Password reset successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin()
        {
            try
            {
                // Kiểm tra xem admin đã tồn tại chưa
                if (await _userRepository.ExistsAsync("admin"))
                    return BadRequest("Admin account already exists");

                var adminUser = new User
                {
                    Username = "admin",
                    FullName = "Administrator",
                    Email = "admin@jewelry.com",
                    Role = "Admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123", BCrypt.Net.BCrypt.GenerateSalt()),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _userRepository.AddAsync(adminUser);
                return Ok(new { 
                    Message = "Admin account created successfully",
                    Username = "admin",
                    Password = "admin123",
                    Note = "Please change password after first login"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message, StackTrace = ex.StackTrace });
            }
        }
    }
}