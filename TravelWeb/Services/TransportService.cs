using System.Collections.Generic;
using System.Linq;
using TravelWeb.Models;

namespace TravelWeb.Services
{
    public static class TransportService
    {
        public static List<TransportOption> GetSampleData()
        {
            return new List<TransportOption>
            {
                new TransportOption { From = "Hà Nội", To = "Đà Nẵng", Mode = "Máy bay", Provider = "VietJet", Duration = "1h20", Price = 1200000, BookingUrl = "https://vietjetair.com", Note = "Bay thẳng" },
                new TransportOption { From = "Hà Nội", To = "Đà Nẵng", Mode = "Máy bay", Provider = "Vietnam Airlines", Duration = "1h15", Price = 1500000, BookingUrl = "https://vietnamairlines.com", Note = "Hành lý 23kg" },
                new TransportOption { From = "Hà Nội", To = "Đà Nẵng", Mode = "Tàu hỏa", Provider = "SE3", Duration = "15h", Price = 700000, BookingUrl = "https://dsvn.vn", Note = "Giường nằm" },
                new TransportOption { From = "TP.HCM", To = "Đà Nẵng", Mode = "Máy bay", Provider = "Bamboo Airways", Duration = "1h25", Price = 1400000, BookingUrl = "https://bambooairways.com", Note = "Bay thẳng" },
                new TransportOption { From = "TP.HCM", To = "Cần Thơ", Mode = "Xe khách", Provider = "Futa Bus", Duration = "3h30", Price = 150000, BookingUrl = "https://futabus.vn", Note = "Giường nằm" },
                new TransportOption { From = "Đà Nẵng", To = "Huế", Mode = "Xe khách", Provider = "HN Bus", Duration = "2h30", Price = 100000, BookingUrl = "#", Note = "Đèo Hải Vân" },
                new TransportOption { From = "Hà Nội", To = "Sapa", Mode = "Xe khách", Provider = "Sapa Express", Duration = "6h", Price = 300000, BookingUrl = "#", Note = "Limousine" },
            };
        }

        public static List<TransportOption> Suggest(string from, string to)
        {
            var all = GetSampleData();
            return all.Where(o => o.From.ToLower() == (from ?? "").ToLower() && o.To.ToLower() == (to ?? "").ToLower()).ToList();
        }

        public static IEnumerable<string> GetAllPlaces()
        {
            var all = GetSampleData();
            return all.SelectMany(o => new[] { o.From, o.To }).Distinct().OrderBy(x => x);
        }
    }
}


