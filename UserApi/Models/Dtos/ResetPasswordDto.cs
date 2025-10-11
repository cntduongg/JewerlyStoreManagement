using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Dtos
{
    public class ResetPasswordDto
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string NewPassword { get; set; } = string.Empty;
    }
}
