using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWeb.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Khách Hàng")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        [Display(Name = "Khách Sạn")]
        public int HotelId { get; set; }

        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày Nhận Phòng")]
        public DateTime CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày Trả Phòng")]
        public DateTime CheckOutDate { get; set; }

        [Range(1, 20)]
        [Display(Name = "Số Lượng Phòng")]
        public int SoLuongPhong { get; set; }

        [Display(Name = "Trạng Thái")]
        public string TrangThai { get; set; } = "Chờ Xác Nhận"; // hoặc: Đã Xác Nhận / Đã Hủy
    }
}
