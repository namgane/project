/*using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class TransportController : Controller
    {
        [HttpGet]
        public IActionResult Suggest(string from, string to)
        {
            var all = TransportData.GetSampleData();
            var options = all.Where(o => o.From == from && o.To == to).ToList();
            return View(options);
        }
    }
}
*/