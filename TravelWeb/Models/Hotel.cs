using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWeb.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khách sạn")]
        [StringLength(200)]
        [Display(Name = "Tên Khách Sạn")]
        public string TenKhachSan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [StringLength(300)]
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Mô Tả")]
        public string? MoTa { get; set; }

        [Display(Name = "Số Phòng")]
        [Range(1, 1000)]
        public int SoPhong { get; set; }

        [Display(Name = "Giá Mỗi Đêm")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaMoiDem { get; set; }

        [Display(Name = "Hình Ảnh")]
        [StringLength(255)]
        public string? HinhAnh { get; set; }

        // Khóa ngoại đến User (chủ khách sạn)
        public int? OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public User? Owner { get; set; }
    }
}
