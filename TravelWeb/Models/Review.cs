using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWeb.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string CuisineId { get; set; } = string.Empty; // cuisine:{Province}:{Name}

        [StringLength(100)]
        public string DisplayName { get; set; } = string.Empty; // Tên hiển thị (có thể là tên thật hoặc anonymous)

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; } // 1-5 sao

        [StringLength(1000)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key to User (nullable để hỗ trợ đánh giá anonymous)
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}


