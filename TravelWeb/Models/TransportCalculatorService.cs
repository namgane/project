using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelWeb.Models
{
    public static class TransportCalculatorService
    {
        public static TransportCalculation CalculateTransport(
            string destination,
            string transportType,
            int numberOfPeople,
            string startTime = "09:00")
        {
            var calculation = new TransportCalculation
            {
                TransportType = transportType,
                NumberOfPeople = numberOfPeople,
                Segments = new List<RouteSegment>()
            };

            // Lấy thông tin phương tiện
            TransportMode mode;
            if (transportType == "Kết hợp tự động")
            {
                // Logic tự động chọn phương tiện phù hợp
                mode = AutoSelectTransport(destination, numberOfPeople);
                calculation.TransportType = mode.Name;
            }
            else
            {
                mode = TransportData.GetMode(transportType);
            }

            // Tính số phương tiện cần thiết
            calculation.VehiclesNeeded = (int)Math.Ceiling((double)numberOfPeople / mode.Capacity);

            // Lấy các chặng đường
            var routeSegments = TransportData.GetRouteSegments(destination);

            var currentTime = TimeSpan.Parse(startTime);
            double totalDistance = 0;
            double totalCost = 0;

            foreach (var segment in routeSegments)
            {
                var distance = segment.Distance;

                // Quyết định phương tiện cho chặng này
                var segmentMode = mode;
                var segmentTransportName = mode.Name;

                // ❌ BỎ LOGIC ĐI BỘ - Luôn dùng phương tiện đã chọn
                // Lý do: Người dùng đã chọn phương tiện, không nên tự động đổi

                // Tính chi phí chặng
                double segmentCost;
                if (segmentMode.IsPricedPerKm)
                {
                    segmentCost = distance * segmentMode.CostPerKm * calculation.VehiclesNeeded;
                }
                else
                {
                    segmentCost = segmentMode.CostPerPerson * numberOfPeople;
                }

                // Tính thời gian di chuyển
                double travelTimeHours = 0;
                if (segmentMode.AverageSpeed > 0)
                {
                    travelTimeHours = distance / segmentMode.AverageSpeed;
                }
                else
                {
                    // Fallback: giả sử tốc độ trung bình 30 km/h nếu không có dữ liệu
                    travelTimeHours = distance / 30.0;
                }
                double travelTimeMinutes = travelTimeHours * 60 + segmentMode.WaitingTime;

                var startTimeStr = currentTime.ToString(@"hh\:mm");
                currentTime = currentTime.Add(TimeSpan.FromMinutes(travelTimeMinutes));
                var endTimeStr = currentTime.ToString(@"hh\:mm");

                calculation.Segments.Add(new RouteSegment
                {
                    From = segment.From,
                    To = segment.To,
                    Distance = distance,
                    Cost = segmentCost,
                    Duration = travelTimeMinutes,
                    StartTime = startTimeStr,
                    EndTime = endTimeStr
                    // TransportType đã bị bỏ vì RouteSegment không có property này
                });

                totalDistance += distance;
                totalCost += segmentCost;
            }

            calculation.TotalDistance = totalDistance;
            calculation.TotalTransportCost = totalCost;

            // Tính tiền xăng (cho xe riêng, xe máy)
            if (mode.FuelConsumption > 0)
            {
                calculation.FuelCost = totalDistance * mode.FuelConsumption * mode.FuelPrice * calculation.VehiclesNeeded;
                calculation.TotalTransportCost += calculation.FuelCost;
            }

            // Tính tổng thời gian
            calculation.TotalDuration = calculation.Segments.Sum(s => s.Duration);

            return calculation;
        }

        private static TransportMode AutoSelectTransport(string destination, int numberOfPeople)
        {
            // Logic tự động: chọn phương tiện phù hợp nhất
            // Ưu tiên: tiết kiệm chi phí, thời gian hợp lý

            if (numberOfPeople <= 2)
            {
                return TransportData.GetMode("Xe máy"); // Rẻ, linh hoạt
            }
            else if (numberOfPeople <= 4)
            {
                return TransportData.GetMode("Taxi"); // Thoải mái, tiện lợi
            }
            else if (numberOfPeople <= 7)
            {
                return TransportData.GetMode("Xe riêng"); // Phù hợp gia đình nhỏ
            }
            else
            {
                return TransportData.GetMode("Xe bus"); // Phù hợp nhóm đông
            }
        }

        public static string FormatDuration(double minutes)
        {
            if (minutes < 60)
            {
                return $"{(int)minutes} phút";
            }
            else
            {
                int hours = (int)(minutes / 60);
                int mins = (int)(minutes % 60);
                return mins > 0 ? $"{hours} giờ {mins} phút" : $"{hours} giờ";
            }
        }
    }
}