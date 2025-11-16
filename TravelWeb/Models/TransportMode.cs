namespace TravelWeb.Models
{
    public class TransportMode
    {
        public string Name { get; set; } = string.Empty;
        public double CostPerKm { get; set; } // Chi phí/km (cho xe riêng, xe máy, taxi)
        public double CostPerPerson { get; set; } // Chi phí/người (cho xe bus)
        public double AverageSpeed { get; set; } // Tốc độ trung bình (km/h)
        public int Capacity { get; set; } // Sức chứa (người)
        public double FuelConsumption { get; set; } // Mức tiêu hao nhiên liệu (lít/km)
        public double FuelPrice { get; set; } // Giá xăng (đồng/lít)
        public int WaitingTime { get; set; } // Thời gian chờ (phút)
        public bool IsPricedPerKm { get; set; } // true = tính theo km, false = tính theo người
    }

    public class RouteSegment
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public double Distance { get; set; } // Khoảng cách (km)
        public double Cost { get; set; } // Chi phí chặng này
        public double Duration { get; set; } // Thời gian di chuyển (phút)
        public string StartTime { get; set; } = string.Empty; // Giờ bắt đầu
        public string EndTime { get; set; } = string.Empty; // Giờ kết thúc
    }

    public class TransportCalculation
    {
        public string TransportType { get; set; } = string.Empty;
        public int NumberOfPeople { get; set; }
        public int VehiclesNeeded { get; set; }
        public double TotalDistance { get; set; } // Tổng khoảng cách (km)
        public double TotalTransportCost { get; set; } // Tổng chi phí di chuyển
        public double FuelCost { get; set; } // Chi phí xăng
        public double TotalDuration { get; set; } // Tổng thời gian (phút)
        public List<RouteSegment> Segments { get; set; } = new List<RouteSegment>();
    }
}