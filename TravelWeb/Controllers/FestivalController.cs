using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class FestivalController : Controller
    {
        [HttpGet]
<<<<<<< Updated upstream
        public IActionResult Index()
        {
            var allFestivals = FestivalData.GetAll();
            return View(allFestivals);
=======
        public IActionResult Index(
            int page = 1,
            string search = "",
            string region = "",
            string province = "",
            int? month = null,
            string timeframe = "",
            string view = "grid")
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

            if (!string.IsNullOrWhiteSpace(region))
            {
                allFestivals = allFestivals
                    .Where(f => f.Region.Equals(region, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(province))
            {
                allFestivals = allFestivals
                    .Where(f => f.Province.Contains(province, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (month.HasValue && month.Value >= 1 && month.Value <= 12)
            {
                allFestivals = allFestivals
                    .Where(f => f.StartDate.Month == month.Value || f.EndDate.Month == month.Value)
                    .ToList();
            }

            var today = DateTime.Today;
            if (!string.IsNullOrWhiteSpace(timeframe))
            {
                if (timeframe == "thisweek")
                {
                    var end = today.AddDays(7);
                    allFestivals = allFestivals
                        .Where(f => f.StartDate <= end && f.EndDate >= today)
                        .ToList();
                }
                else if (timeframe == "nextmonth")
                {
                    var start = new DateTime(today.Year, today.Month, 1).AddMonths(1);
                    var end = start.AddMonths(1).AddDays(-1);
                    allFestivals = allFestivals
                        .Where(f => f.StartDate <= end && f.EndDate >= start)
                        .ToList();
                }
                else if (timeframe == "upcoming")
                {
                    allFestivals = allFestivals
                        .Where(f => f.StartDate >= today)
                        .ToList();
                }
            }

            // Sắp xếp theo ngày bắt đầu (gần nhất lên đầu)
            allFestivals = allFestivals
                .OrderBy(f => f.StartDate)
                .ThenBy(f => f.Name)
                .ToList();

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
            ViewBag.SelectedRegion = region;
            ViewBag.SelectedProvince = province;
            ViewBag.SelectedMonth = month;
            ViewBag.SelectedTimeframe = timeframe;
            ViewBag.SelectedView = view;
            ViewBag.PageSize = PageSize;

            // Province cache per region to reuse on client
            var provincesByRegion = allFestivals
                .GroupBy(f => f.Region)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Province).Distinct().OrderBy(x => x).ToList());
            ViewBag.ProvincesByRegion = provincesByRegion;

            // Timeline data (group by month)
            var monthGroups = allFestivals
                .GroupBy(f => f.StartDate.Month)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.OrderBy(x => x.StartDate).ToList());
            ViewBag.MonthGroups = monthGroups;

            return View(festivals);
>>>>>>> Stashed changes
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
