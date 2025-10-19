using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class FestivalController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var allFestivals = FestivalData.GetAll();
            return View(allFestivals);
        }

        [HttpGet]
        public IActionResult Details(string name)
        {
            var festival = FestivalData.GetAll()
                .FirstOrDefault(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (festival == null)
                return NotFound();

            return View(festival);
        }

        [HttpGet]
        public IActionResult Upcoming()
        {
            var today = DateTime.Now;
            var upcoming = FestivalData.GetAll()
                .Where(f => f.StartDate >= today)
                .OrderBy(f => f.StartDate)
                .ToList();

            return View(upcoming);
        }
    }
}
