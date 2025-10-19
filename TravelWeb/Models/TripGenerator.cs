using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelWeb.Models
{
    public static class TripGenerator
    {
        private static readonly string[] FromCities = { "TP.HCM", "Hà Nội", "Đà Nẵng", "Cần Thơ", "Hải Phòng" };
        private static readonly string[] TransportTypes = { "Xe khách", "Tàu hỏa", "Máy bay" };
        private static readonly Random rand = new Random();

        public static List<Trip> GenerateTrips()
        {
            var now = DateTime.Now;
            var allFestivals = FestivalData.GetAll();

            // 🎯 Lấy các lễ hội trong 3 tháng tới
            var upcoming = allFestivals
                .Where(f => f.StartDate >= now && f.StartDate <= now.AddMonths(3))
                .ToList();

            var trips = new List<Trip>();

            foreach (var fest in upcoming)
            {
                foreach (var city in FromCities)
                {
                    var baseDate = fest.StartDate.AddDays(-rand.Next(1, 10)); // đi trước lễ hội 1–10 ngày
                    var type = TransportTypes[rand.Next(TransportTypes.Length)];
                    var price = rand.Next(300_000, 2_000_000);

                    trips.Add(new Trip
                    {
                        FromCity = city,
                        ToProvince = fest.Province,
                        DepartureDate = baseDate,
                        TransportType = type,
                        Price = price,
                        FestivalName = fest.Name
                    });
                }
            }

            return trips.OrderBy(t => t.DepartureDate).ToList();
        }
    }
}
