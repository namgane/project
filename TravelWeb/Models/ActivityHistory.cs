using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWeb.Models
{
    public class ActivityHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string ActivityType { get; set; } = string.Empty; // Review, Favorite, Booking, Payment, etc.

        [StringLength(200)]
        public string? ItemId { get; set; } // ID của item liên quan (cuisineId, festivalId, etc.)

        [StringLength(200)]
        public string? ItemTitle { get; set; } // Tên item để hiển thị

        [StringLength(500)]
        public string? Description { get; set; } // Mô tả chi tiết hoạt động

        [StringLength(100)]
        public string? Location { get; set; } // Tỉnh/thành, địa điểm

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Metadata dạng JSON để lưu thông tin bổ sung
        [StringLength(1000)]
        public string? Metadata { get; set; }
    }
}


