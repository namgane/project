using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWeb.Models
{
    public class Tour
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên tour")]
        [StringLength(200, ErrorMessage = "Tên tour không được quá 200 ký tự")]
        [Display(Name = "Tên Tour")]
        public string TenTour { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa điểm")]
        [StringLength(200, ErrorMessage = "Địa điểm không được quá 200 ký tự")]
        [Display(Name = "Địa Điểm")]
        public string DiaDiem { get; set; }

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string? MoTa { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số ngày")]
        [Range(1, 365, ErrorMessage = "Số ngày phải từ 1 đến 365")]
        [Display(Name = "Số Ngày")]
        public int SoNgay { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá tour")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá tour phải lớn hơn 0")]
        [Display(Name = "Giá Tour")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Gia { get; set; }

        [Display(Name = "Hình Ảnh")]
        [NotMapped] // Không lưu vào database
        public IFormFile? HinhAnhFile { get; set; } // Dùng để upload file

        [StringLength(255)]
        public string? HinhAnh { get; set; } // Lưu tên file trong database
    }
}