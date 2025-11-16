using Microsoft.AspNetCore.Mvc;
using TravelWeb.Models;
using System.Linq;

namespace TravelWeb.Controllers
{
    public class DestinationController : Controller
    {
        // GET: /Destination/Check
        public IActionResult Check()
        {
            var destinations = DestinationData.GetAll()
                .OrderBy(d => d.Name)
                .ToList();

            return View("DestinationChecker", destinations);
        }

        // API: Kiểm tra một địa điểm cụ thể
        [HttpGet]
        public IActionResult CheckDestination(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(new { success = false, message = "Vui lòng nhập tên địa điểm" });
            }

            var destination = DestinationData.GetByName(name);

            if (destination == null)
            {
                return Json(new
                {
                    success = false,
                    message = $"Không tìm thấy địa điểm '{name}'",
                    availableDestinations = DestinationData.GetAll().Select(d => d.Name).ToList()
                });
            }

            var routes = DestinationData.GetRouteSegmentsWithCoordinates(name);

            return Json(new
            {
                success = true,
                destination = new
                {
                    destination.Name,
                    destination.Description,
                    destination.Province,
                    destination.Latitude,
                    destination.Longitude,
                    AttractionsCount = destination.Attractions.Count,
                    Attractions = destination.Attractions.Select(a => new
                    {
                        a.Name,
                        a.Description,
                        a.Type,
                        a.Latitude,
                        a.Longitude,
                        a.VisitDuration,
                        a.EntranceFee
                    }).ToList()
                },
                routes = routes.Select(r => new
                {
                    From = r.From,
                    To = r.To,
                    Distance = r.Distance
                }).ToList(),
                totalDistance = routes.Sum(r => r.Distance),
                totalAttractions = destination.Attractions.Count,
                totalVisitTime = destination.Attractions.Sum(a => a.VisitDuration),
                totalEntranceFee = destination.Attractions.Sum(a => a.EntranceFee)
            });
        }

        // API: Tính khoảng cách giữa 2 điểm
        [HttpGet]
        public IActionResult CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var distance = DestinationData.CalculateDistance(lat1, lon1, lat2, lon2);

            return Json(new
            {
                success = true,
                distance = System.Math.Round(distance, 2),
                unit = "km"
            });
        }
    }
}