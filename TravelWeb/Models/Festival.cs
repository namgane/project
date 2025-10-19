using System;

namespace TravelWeb.Models
{
    public class Festival
    {
        public string Name { get; set; }          // Tên lễ hội
        public string Province { get; set; }      // Tỉnh/thành
        public string Region { get; set; }        // Miền Bắc / Trung / Nam
        public DateTime StartDate { get; set; }   // Ngày bắt đầu
        public DateTime EndDate { get; set; }     // Ngày kết thúc
        public string Description { get; set; }   // Mô tả ngắn
        public string Highlight { get; set; }     // Điểm đặc trưng (ẩm thực, văn hóa, tín ngưỡng…)
        public string ImageUrl { get; set; }      // Ảnh đại diện
    }
}
