namespace TravelWeb.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string MoTa { get; set; } = string.Empty; // URL ảnh chính
        public int Type { get; set; }

        // === THÊM CÁC TRƯỜNG MỚI ===
        public string? ChiTiet { get; set; } // Mô tả văn bản chi tiết
        public string? DiaChi { get; set; } // Địa chỉ cụ thể
        public string? GioCua { get; set; } // Ví dụ: "8:00 - 22:00"
        public string? GiaCa { get; set; } // Ví dụ: "50.000đ - 200.000đ"
        public string? SoDienThoai { get; set; }
        public float? Rating { get; set; } // Đánh giá 0-5 sao
    }
}