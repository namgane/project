using Microsoft.AspNetCore.Mvc;
using TravelWeb.Models;
using System.Linq;

namespace TravelWeb.Controllers
{
    public class TransportController : Controller
    {
        public IActionResult Suggest()
        {
            // Lấy dữ liệu giống Trip/Suggestions
            var trips = TripGenerator.GenerateTrips()
                .Select((t, index) => new TripWithId
                {
                    Id = index + 1,
                    FromCity = t.FromCity,
                    ToProvince = t.ToProvince,
                    DepartureDate = t.DepartureDate,
                    TransportType = t.TransportType,
                    Price = t.Price,
                    FestivalName = t.FestivalName
                }).ToList();

            return View(trips);
        }

        public IActionResult Detail(int id)
        {
            var trips = TripGenerator.GenerateTrips()
                .Select((t, index) => new TripWithId
                {
                    Id = index + 1,
                    FromCity = t.FromCity,
                    ToProvince = t.ToProvince,
                    DepartureDate = t.DepartureDate,
                    TransportType = t.TransportType,
                    Price = t.Price,
                    FestivalName = t.FestivalName
                }).ToList();

            var trip = trips.FirstOrDefault(x => x.Id == id);
            if (trip == null)
                return NotFound();

            return View(trip);
        }
    }
}
