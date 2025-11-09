using Microsoft.AspNetCore.Mvc;
using TravelWeb.Models;
using Microsoft.AspNetCore.Http;
using TravelWeb.Services;

namespace TravelWeb.Controllers
{
    public class TripPlannerController : Controller
    {
        [HttpGet]
        public IActionResult Index(string? destination = null, double? budget = null, int? days = null)
        {
            var model = new TripRequest
            {
                Destination = destination ?? string.Empty,
                Budget = budget ?? 0,
                Days = days
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(TripRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var plan = TripPlannerService.GenerateDetailedPlan(request);
            var username = HttpContext.Session.GetString("Username") ?? "guest";
            GamificationPassportService.AddStamp(username, PassportStampType.TripPlan, request.Destination);
            return View("Result", plan);
        }

        // Permalink endpoint that accepts query params and renders Result directly
        [HttpGet]
        public IActionResult Result(string destination, double budget, int? days)
        {
            if (string.IsNullOrWhiteSpace(destination) || budget <= 0)
            {
                TempData["ErrorMessage"] = "Thiếu thông tin để tạo kế hoạch. Vui lòng nhập lại.";
                return RedirectToAction("Index", new { destination, budget, days });
            }
            var req = new TripRequest { Destination = destination, Budget = budget, Days = days };
            var plan = TripPlannerService.GenerateDetailedPlan(req);
            var username = HttpContext.Session.GetString("Username") ?? "guest";
            GamificationPassportService.AddStamp(username, PassportStampType.TripPlan, destination);
            return View(plan);
        }
    }
}
