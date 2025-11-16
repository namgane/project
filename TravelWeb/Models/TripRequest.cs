using System.ComponentModel.DataAnnotations;

namespace TravelWeb.Models
{
    public class TripRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập địa điểm du lịch!")]
        [StringLength(100, ErrorMessage = "Tên địa điểm không được quá 100 ký tự!")]
        [Display(Name = "Địa điểm du lịch")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngân sách!")]
        [Range(1000000, 100000000, ErrorMessage = "Ngân sách phải từ 1,000,000 đến 100,000,000 VNĐ!")]
        [Display(Name = "Ngân sách (VNĐ)")]
        public double Budget { get; set; }

        [Range(1, 30, ErrorMessage = "Số ngày du lịch phải từ 1 đến 30 ngày!")]
        [Display(Name = "Số ngày du lịch")]
        public int? Days { get; set; }

        [Range(1, 50, ErrorMessage = "Số người phải từ 1 đến 50!")]
        [Display(Name = "Số người đi")]
        public int NumberOfPeople { get; set; } = 1;

        [Display(Name = "Phương tiện di chuyển")]
        public string TransportType { get; set; } = "Kết hợp tự động";
    }
}