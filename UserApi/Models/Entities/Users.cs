using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserApi.Models.Entities
{
    [Table("users", Schema = "public")]
    public class User
    {
        [Key]
        [Column("userid")]
        public int UserId { get; set; }

        [Column("username")]
        [MaxLength(50)]
        public string Username { get; set; }

        [Column("passwordhash")]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Column("fullname")]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Column("email")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column("role")]
        [MaxLength(50)]
        public string Role { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }  = DateTime.UtcNow;

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
