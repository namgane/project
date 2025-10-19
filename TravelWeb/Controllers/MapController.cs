using Microsoft.AspNetCore.Mvc;

namespace TravelWeb.Controllers
{
    public class MapController : Controller
    {
        // /Map
        public IActionResult Index(string city, double lat, double lng)
        {
            ViewBag.City = city;
            ViewBag.Lat = lat;
            ViewBag.Lng = lng;
            return View();
        }
    }
}
