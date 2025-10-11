namespace UserApi.Models.Dtos
{
    public class UpdateUserDto
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Role { get; set; } = "User";
        public bool IsActive { get; set; }
        public string PasswordHash { get; set; }
    }
}
