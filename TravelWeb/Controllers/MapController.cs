using Microsoft.AspNetCore.Mvc;
using TravelWeb.Data;

namespace TravelWeb.Controllers
{
    public class MapController : Controller
    {
        private readonly TravelContext _context;

        public MapController(TravelContext context)
        {
            _context = context;
        }

        public IActionResult Index(string city, double lat, double lng)
        {
            ViewBag.City = city;
            ViewBag.Lat = lat;
            ViewBag.Lng = lng;
            ViewBag.Locations = _context.Locations.ToList();

            return View();
        }
    }
}
