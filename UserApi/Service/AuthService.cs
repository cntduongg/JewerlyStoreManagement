using UserApi.Models.Dtos;
using UserApi.Models.Entities;
using UserApi.Repositories;
using UserApi.Repositories.IRepo;
using UserApi.Service.IService;

namespace UserApi.Service
{
    public class AuthService : IAuthService
    {
        private readonly iUserRepo _userRepository;

        public AuthService(iUserRepo userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        private string HashPassword(string password)
        {
            try
            {
                // Tạo salt mới với work factor mặc định (10)
                return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Password hashing error: {ex.Message}");
                throw new InvalidOperationException("Failed to hash password", ex);
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Password verification error: {ex.Message}");
                return false;
            }
        }

        public async Task<(bool Success, string Message)> RegisterAsync(CreateUserDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password) ||
                    string.IsNullOrEmpty(dto.FullName) || string.IsNullOrEmpty(dto.Email))
                    return (false, "All fields are required");

                if (await _userRepository.ExistsAsync(dto.Username))
                    return (false, "Username already exists");

                var user = new User
                {
                    Username = dto.Username,
                    FullName = dto.FullName,
                    Email = dto.Email,
                    Role = dto.Role ?? "User",
                    PasswordHash = HashPassword(dto.Password),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _userRepository.AddAsync(user);
                return (true, "Registration successful");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Registration error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return (false, $"Registration failed: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> LoginAsync(LoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
                return (false, "Username and password are required");

            var user = await _userRepository.GetByAccountAsync(dto.Username);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                return (false, "Invalid username or password");

            return (true, "Login successful");
        }

        public Task LogoutAsync()
        {
            // Logic logout được xử lý trong controller (xóa cookie)
            return Task.CompletedTask;
        }
    }
}
