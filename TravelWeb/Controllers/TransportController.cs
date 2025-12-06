/*using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class TransportController : Controller
    {
        [HttpGet]
        public IActionResult Suggest(string from, string to)
        {
<<<<<<< Updated upstream
            var all = TransportData.GetSampleData();
            var options = all.Where(o => o.From == from && o.To == to).ToList();
=======
            // Lấy dữ liệu từ Trip/Suggestions (TripGenerator)
            var trips = TripGenerator.GenerateTrips();
            
            // Lọc theo from và to nếu có
            if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
            {
                trips = trips.Where(t => 
                    t.FromCity.ToLower().Contains(from.ToLower()) && 
                    t.ToProvince.ToLower().Contains(to.ToLower())
                ).ToList();
            }
            else if (!string.IsNullOrEmpty(to))
            {
                // Chỉ lọc theo điểm đến nếu không có điểm xuất phát
                trips = trips.Where(t => t.ToProvince.ToLower().Contains(to.ToLower())).ToList();
            }
            else if (!string.IsNullOrEmpty(from))
            {
                // Chỉ lọc theo điểm xuất phát nếu không có điểm đến
                trips = trips.Where(t => t.FromCity.ToLower().Contains(from.ToLower())).ToList();
            }

            // Chuyển đổi Trip thành TransportOption
            var options = trips.Select(trip => new TransportOption
            {
                From = trip.FromCity,
                To = trip.ToProvince,
                Mode = trip.TransportType,
                Provider = GetProviderByTransportType(trip.TransportType),
                Duration = GetDurationByTransportType(trip.TransportType, trip.FromCity, trip.ToProvince),
                Price = (decimal)trip.Price,
                BookingUrl = GetBookingUrl(trip.TransportType),
                Note = !string.IsNullOrEmpty(trip.FestivalName) ? $"Lễ hội: {trip.FestivalName}" : null
            }).ToList();

            ViewBag.From = from ?? "Tất cả";
            ViewBag.To = to ?? "Tất cả";
            ViewBag.Trips = trips; // Lưu trips để hiển thị thông tin lễ hội
            ViewBag.TotalOptions = options.Count;
            
>>>>>>> Stashed changes
            return View(options);
        }

        private string GetProviderByTransportType(string transportType)
        {
            return transportType switch
            {
                "Máy bay" => new[] { "VietJet Air", "Vietnam Airlines", "Bamboo Airways" }[new System.Random().Next(3)],
                "Tàu hỏa" => "SE",
                "Xe khách" => new[] { "Futa Bus", "Phương Trang", "Hoàng Long", "Sapa Express" }[new System.Random().Next(4)],
                _ => "N/A"
            };
        }

        private string GetDurationByTransportType(string transportType, string from, string to)
        {
            return transportType switch
            {
                "Máy bay" => "1h15 - 1h30",
                "Tàu hỏa" => "12h - 18h",
                "Xe khách" => "3h - 8h",
                _ => "N/A"
            };
        }

        private string GetBookingUrl(string transportType)
        {
            return transportType switch
            {
                "Máy bay" => "https://vietjetair.com",
                "Tàu hỏa" => "https://dsvn.vn",
                "Xe khách" => "https://futabus.vn",
                _ => "#"
            };
        }
    }
}
*/