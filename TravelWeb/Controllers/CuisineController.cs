using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelWeb.Data;
using System.Collections.Generic;
using System.Linq;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class CuisineController : Controller
    {
        private readonly TravelContext _context;

        public CuisineController(TravelContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var provinces = CuisineData.GetAllProvinces();
            return View(provinces);
        }

        [HttpGet]
        public IActionResult Search(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                TempData["Error"] = "Nhập tên tỉnh/thành để tìm kiếm.";
                return RedirectToAction(nameof(Index));
            }
            var canonical = CuisineData.CanonicalProvinceName(q);
            return RedirectToAction(nameof(Province), new { name = canonical });
        }

        public IActionResult Province(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "Vui lòng chọn tỉnh/thành phố.";
                return RedirectToAction(nameof(Index));
            }

            var items = CuisineData.GetTopByProvince(name, 10);

            if (items.Count == 0)
            {
                var suggestions = CuisineData.FindSimilarProvinces(name, 5);
                if (suggestions.Count > 0)
                {
                    TempData["Error"] = $"Chưa có dữ liệu chính xác cho '{name}'. Gợi ý: {string.Join(", ", suggestions)}";
                }
                else
                {
                    TempData["Error"] = $"Chưa có dữ liệu ẩm thực cho '{name}'.";
                }
                return RedirectToAction(nameof(Index));
            }

            var vm = new CuisineProvinceViewModel
            {
                Province = CuisineData.CanonicalProvinceName(name),
                Items = items,
                OverallAveragePrice = items.Any() ? items.Average(i => i.AveragePrice) : 0
            };

            // Load reviews from database and compute aggregates
            foreach (var it in items)
            {
                var id = $"cuisine:{vm.Province}:{it.Name}";
                var reviews = _context.Reviews
                    .Where(r => r.CuisineId == id)
                    .ToList();

                if (reviews.Any())
                {
                    vm.AverageRatings[it.Name] = reviews.Average(r => r.Rating);
                    vm.RatingsCount[it.Name] = reviews.Count;
                }
                else
                {
                    vm.AverageRatings[it.Name] = 0;
                    vm.RatingsCount[it.Name] = 0;
                }
            }

            return View(vm);
        }

        public IActionResult Details(string province, string name)
        {
            if (string.IsNullOrWhiteSpace(province) || string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "Thông tin món ăn không hợp lệ.";
                return RedirectToAction(nameof(Index));
            }

            var canonicalProvince = CuisineData.CanonicalProvinceName(province);
            var allItems = CuisineData.GetTopByProvince(canonicalProvince, 100);
            var item = allItems.FirstOrDefault(i => i.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                TempData["Error"] = $"Không tìm thấy món ăn '{name}' tại {canonicalProvince}.";
                return RedirectToAction(nameof(Province), new { name = canonicalProvince });
            }

            // Load reviews from database
            var cuisineId = $"cuisine:{canonicalProvince}:{item.Name}";
            var reviews = _context.Reviews
                .Where(r => r.CuisineId == cuisineId)
                .OrderByDescending(r => r.CreatedAt)
                .Include(r => r.User)
                .ToList();

            var averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;
            var ratingsCount = reviews.Count;

            // Get rating distribution
            var ratingDistribution = new Dictionary<int, int>
            {
                { 5, reviews.Count(r => r.Rating == 5) },
                { 4, reviews.Count(r => r.Rating == 4) },
                { 3, reviews.Count(r => r.Rating == 3) },
                { 2, reviews.Count(r => r.Rating == 2) },
                { 1, reviews.Count(r => r.Rating == 1) }
            };

            ViewBag.Province = canonicalProvince;
            ViewBag.Item = item;
            ViewBag.Reviews = reviews;
            ViewBag.AverageRating = averageRating;
            ViewBag.RatingsCount = ratingsCount;
            ViewBag.RatingDistribution = ratingDistribution;
            ViewBag.CuisineId = cuisineId;

            return View();
        }
    }
}


