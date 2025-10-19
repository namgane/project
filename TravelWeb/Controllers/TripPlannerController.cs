using Microsoft.AspNetCore.Mvc;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class TripPlannerController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(TripRequest request)
        {
            var plan = TripPlannerService.GenerateDetailedPlan(request);
            return View("Result", plan);
        }
    }
}
