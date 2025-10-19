using System.ComponentModel.DataAnnotations;

namespace TravelWeb.Models
{
    public class User
    {
        [Key] // 🔹 BẮT BUỘC có để EF nhận đây là khóa chính
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; } // Admin / Hotel / Customer

        [StringLength(100)]
        public string? FullName { get; set; }

        [StringLength(200)]
        public string? Email { get; set; }

        [StringLength(200)]
        public string? Phone { get; set; }
    }
}
