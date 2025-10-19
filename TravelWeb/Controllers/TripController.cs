using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class TripController : Controller
    {
        public IActionResult Suggestions()
        {
            var trips = TripGenerator.GenerateTrips();
            return View(trips);
        }
    }
}
