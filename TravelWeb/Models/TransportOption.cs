namespace TravelWeb.Models
{
    public class TransportOption
    {
        public string From { get; set; }        // Điểm xuất phát (tỉnh hoặc TP)
        public string To { get; set; }          // Điểm đến
        public string Mode { get; set; }        // "Xe khách", "Tàu hỏa", "Máy bay"
        public string Provider { get; set; }    // Tên hãng, ví dụ: VietJet, Futa Bus...
        public string Duration { get; set; }    // Thời gian di chuyển
        public decimal Price { get; set; }      // Giá vé trung bình (VNĐ)
        public string BookingUrl { get; set; }  // Link đặt vé
        public string Note { get; set; }        // Ghi chú thêm
    }
}
