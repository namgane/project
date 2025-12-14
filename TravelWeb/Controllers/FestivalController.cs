using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class FestivalController : Controller
    {
        private const int PageSize = 6;

        // =========================
        // DANH SÁCH + FILTER
        // =========================
        [HttpGet]
        public IActionResult Index(
            int page = 1,
            string search = "",
            string region = "",
            string season = "",
            string type = ""
        )
        {
            var query = FestivalData.GetAll().AsQueryable();

            // SEARCH
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(f =>
                    f.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    f.Province.Contains(search, StringComparison.OrdinalIgnoreCase)
                );
            }

            // FILTERS
            if (!string.IsNullOrWhiteSpace(region))
                query = query.Where(f => f.Region == region);

            if (!string.IsNullOrWhiteSpace(season))
                query = query.Where(f => f.Season == season);

            if (!string.IsNullOrWhiteSpace(type))
                query = query.Where(f => f.Type == type);

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            page = page < 1 ? 1 : page;
            page = page > totalPages && totalPages > 0 ? totalPages : page;

            var festivals = query
                .OrderBy(f => f.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            // ViewBag
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItems = totalItems;

            ViewBag.SearchTerm = search;
            ViewBag.SelectedRegion = region;
            ViewBag.SelectedSeason = season;
            ViewBag.SelectedType = type;

            return View(festivals);
        }
        public IActionResult Details(int id)
        {
            var fest = FestivalData.GetAll()
                .FirstOrDefault(f => f.Id == id);

            if (fest == null)
                return NotFound("Không tìm thấy lễ hội");

            return View(fest);
        }
        // =========================
        // LỄ HỘI SẮP DIỄN RA
        // =========================
        [HttpGet]
        public IActionResult Upcoming(int page = 1)
        {
            var today = DateTime.Today;

            var query = FestivalData.GetAll()
                .Where(f => f.StartDate.Date >= today)
                .OrderBy(f => f.StartDate)
                .AsQueryable();

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            page = page < 1 ? 1 : page;
            page = page > totalPages && totalPages > 0 ? totalPages : page;

            var festivals = query
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItems = totalItems;

            return View(festivals);
        }
    }
}
