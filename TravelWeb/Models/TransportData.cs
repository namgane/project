using System.Collections.Generic;
using System.Linq;

namespace TravelWeb.Models
{
    public static class TransportData
    {
        private static readonly Dictionary<string, TransportMode> Modes = new Dictionary<string, TransportMode>
        {
            { "Xe riêng", new TransportMode
            {
                Name = "Xe riêng",
                CostPerKm = 5000,
                AverageSpeed = 60,
                Capacity = 4,
                FuelConsumption = 0.08, // 8 lít/100km = 0.08 lít/km
                FuelPrice = 23000,
                WaitingTime = 0,
                IsPricedPerKm = true
            }},
            { "Xe bus", new TransportMode
            {
                Name = "Xe bus",
                CostPerPerson = 30000,
                AverageSpeed = 50,
                Capacity = 40,
                FuelConsumption = 0,
                FuelPrice = 0,
                WaitingTime = 15,
                IsPricedPerKm = false
            }},
            { "Đi bộ", new TransportMode
            {
                Name = "Đi bộ",
                CostPerKm = 0,
                AverageSpeed = 5,
                Capacity = 1,
                FuelConsumption = 0,
                FuelPrice = 0,
                WaitingTime = 0,
                IsPricedPerKm = true
            }},
            { "Taxi", new TransportMode
            {
                Name = "Taxi",
                CostPerKm = 15000,
                AverageSpeed = 50,
                Capacity = 4,
                FuelConsumption = 0,
                FuelPrice = 0,
                WaitingTime = 5,
                IsPricedPerKm = true
            }},
            { "Xe máy", new TransportMode
            {
                Name = "Xe máy",
                CostPerKm = 3000,
                AverageSpeed = 40,
                Capacity = 2,
                FuelConsumption = 0.025, // 2.5 lít/100km
                FuelPrice = 23000,
                WaitingTime = 0,
                IsPricedPerKm = true
            }}
        };

        public static TransportMode GetMode(string modeName)
        {
            if (Modes.TryGetValue(modeName, out var mode))
            {
                return mode;
            }
            return Modes["Xe bus"]; // Default
        }

        public static List<string> GetAllModes()
        {
            return new List<string>
            {
                "Xe riêng",
                "Xe bus",
                "Đi bộ",
                "Taxi",
                "Xe máy",
                "Kết hợp tự động"
            };
        }

        // Dữ liệu khoảng cách giữa các điểm trong một số tỉnh phổ biến
        private static readonly Dictionary<string, List<(string From, string To, double Distance)>> ProvinceRoutes = new Dictionary<string, List<(string, string, double)>>
        {
            { "Đà Lạt", new List<(string, string, double)>
                {
                    ("Trung tâm Đà Lạt", "Hồ Xuân Hương", 1.5),
                    ("Hồ Xuân Hương", "Dinh Bảo Đại", 3.0),
                    ("Dinh Bảo Đại", "Thác Datanla", 5.0),
                    ("Thác Datanla", "Langbiang", 12.0),
                    ("Langbiang", "Trung tâm Đà Lạt", 15.0)
                }
            },
            { "Hà Nội", new List<(string, string, double)>
                {
                    ("Hồ Gươm", "Văn Miếu", 2.5),
                    ("Văn Miếu", "Lăng Bác", 4.0),
                    ("Lăng Bác", "Chùa Một Cột", 1.0),
                    ("Chùa Một Cột", "Bảo tàng Lịch sử", 2.0),
                    ("Bảo tàng Lịch sử", "Hồ Gươm", 3.0)
                }
            },
            { "Đà Nẵng", new List<(string, string, double)>
                {
                    ("Trung tâm Đà Nẵng", "Cầu Rồng", 2.0),
                    ("Cầu Rồng", "Bãi biển Mỹ Khê", 3.5),
                    ("Bãi biển Mỹ Khê", "Ngũ Hành Sơn", 8.0),
                    ("Ngũ Hành Sơn", "Bán đảo Sơn Trà", 10.0),
                    ("Bán đảo Sơn Trà", "Trung tâm Đà Nẵng", 12.0)
                }
            },
            { "Huế", new List<(string, string, double)>
                {
                    ("Trung tâm Huế", "Đại Nội", 1.5),
                    ("Đại Nội", "Chùa Thiên Mụ", 4.0),
                    ("Chùa Thiên Mụ", "Lăng Khải Định", 8.0),
                    ("Lăng Khải Định", "Lăng Tự Đức", 3.0),
                    ("Lăng Tự Đức", "Trung tâm Huế", 6.0)
                }
            },
            { "TP.HCM", new List<(string, string, double)>
                {
                    ("Bến Thành", "Nhà thờ Đức Bà", 1.0),
                    ("Nhà thờ Đức Bà", "Dinh Độc Lập", 0.8),
                    ("Dinh Độc Lập", "Bảo tàng Chứng tích chiến tranh", 1.2),
                    ("Bảo tàng Chứng tích chiến tranh", "Phố đi bộ Nguyễn Huệ", 2.0),
                    ("Phố đi bộ Nguyễn Huệ", "Bến Thành", 1.5)
                }
            }
        };

        public static List<(string From, string To, double Distance)> GetRouteSegments(string destination)
        {
            // Ưu tiên sử dụng tọa độ thực tế từ DestinationData
            var routeWithCoords = DestinationData.GetRouteSegmentsWithCoordinates(destination);
            if (routeWithCoords != null && routeWithCoords.Count > 0)
            {
                return routeWithCoords;
            }

            // Fallback: Tìm khớp tên tỉnh trong dữ liệu cũ
            foreach (var key in ProvinceRoutes.Keys)
            {
                if (destination.Contains(key, System.StringComparison.OrdinalIgnoreCase) ||
                    key.Contains(destination, System.StringComparison.OrdinalIgnoreCase))
                {
                    return ProvinceRoutes[key];
                }
            }

            // Default route nếu không tìm thấy
            return new List<(string, string, double)>
            {
                ("Điểm xuất phát", "Điểm đến 1", 5.0),
                ("Điểm đến 1", "Điểm đến 2", 8.0),
                ("Điểm đến 2", "Điểm đến 3", 6.0),
                ("Điểm đến 3", "Điểm xuất phát", 7.0)
            };
        }
    }
}