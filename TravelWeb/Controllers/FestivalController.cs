using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class FestivalController : Controller
    {
        private const int PageSize = 6; // Số lượng festival mỗi trang

        [HttpGet]
        public IActionResult Index(int page = 1, string search = "")
        {
            var allFestivals = FestivalData.GetAll();
            
            // Lọc theo tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                allFestivals = allFestivals.Where(f => 
                    f.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    f.Province.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    f.Region.Contains(search, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            var totalItems = allFestivals.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
            
            var festivals = allFestivals
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItems = totalItems;
            ViewBag.SearchTerm = search;
            ViewBag.PageSize = PageSize;

            return View(festivals);
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
        public IActionResult Upcoming(int page = 1)
        {
            var today = DateTime.Now;
            var upcoming = FestivalData.GetAll()
                .Where(f => f.StartDate >= today)
                .OrderBy(f => f.StartDate)
                .ToList();

            var totalItems = upcoming.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
            
            var festivals = upcoming
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItems = totalItems;
            ViewBag.PageSize = PageSize;

            return View(festivals);
        }
    }
}
