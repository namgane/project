using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class CuisineController : Controller
    {
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

            // Load reviews from session and compute aggregates
            var json = HttpContext.Session.GetString("Reviews");
            var store = string.IsNullOrEmpty(json)
                ? new Dictionary<string, List<ReviewItem>>()
                : (JsonSerializer.Deserialize<Dictionary<string, List<ReviewItem>>>(json) ?? new Dictionary<string, List<ReviewItem>>());

            foreach (var it in items)
            {
                var id = $"cuisine:{vm.Province}:{it.Name}";
                if (store.ContainsKey(id) && store[id].Count > 0)
                {
                    vm.AverageRatings[it.Name] = store[id].Average(r => r.Rating);
                    vm.RatingsCount[it.Name] = store[id].Count;
                }
                else
                {
                    vm.AverageRatings[it.Name] = 0;
                    vm.RatingsCount[it.Name] = 0;
                }
            }

            return View(vm);
        }
    }
}


