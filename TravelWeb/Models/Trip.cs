using System;

namespace TravelWeb.Models
{
    public class Trip
    {
        public string FromCity { get; set; }
        public string ToProvince { get; set; }
        public DateTime DepartureDate { get; set; }
        public string TransportType { get; set; } // Xe khách, tàu, máy bay
        public double Price { get; set; }          // Giá vé dự kiến
        public string FestivalName { get; set; }   // Lễ hội liên quan (nếu có)
    }
}
