namespace TravelWeb.Models
{
    public class Activity
    {
        public string Time { get; set; }  // Ví dụ: "Sáng", "Chiều", "Tối"
        public string Name { get; set; }  // VD: "Tham quan Thung lũng Tình Yêu"
        public double Cost { get; set; }  // Chi phí dự kiến
        public string Type { get; set; }  // "Ăn uống", "Vui chơi", "Tham quan"
    }
}
