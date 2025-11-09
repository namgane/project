using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TravelWeb.Services;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class TransportController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Places = TransportService.GetAllPlaces();
            return View();
        }

        [HttpGet]
        public IActionResult Suggest(string from, string to)
        {
            var options = TransportService.Suggest(from, to);
            ViewBag.From = from;
            ViewBag.To = to;
            return View(options);
        }
    }
}