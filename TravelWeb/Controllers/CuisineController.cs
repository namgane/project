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
        private const string ReviewSessionKey = "REVIEWS_STORE";

        public IActionResult Index()
        {
            var model = new CuisineProvinceViewModel
            {
                Provinces = CuisineData.GetAllProvinces().ToList(),
                ProvincesByRegion = CuisineData
                .GetProvincesGroupedByRegion()
                .ToDictionary(x => x.Key, x => x.Value)
            };

            return View(model);
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
        private Dictionary<string, List<ReviewItem>> LoadReviewStore()
        {
            var json = HttpContext.Session.GetString(ReviewSessionKey);

            if (string.IsNullOrWhiteSpace(json))
                return new Dictionary<string, List<ReviewItem>>();

            try
            {
                return JsonSerializer.Deserialize<Dictionary<string, List<ReviewItem>>>(
                           json,
                           new JsonSerializerOptions
                           {
                               PropertyNameCaseInsensitive = true
                           })
                       ?? new Dictionary<string, List<ReviewItem>>();
            }
            catch
            {
                // Nếu JSON lỗi → reset store để tránh crash
                return new Dictionary<string, List<ReviewItem>>();
            }
        }


        private void SaveReviewStore(Dictionary<string, List<ReviewItem>> store)
        {
            HttpContext.Session.SetString(
                ReviewSessionKey,
                JsonSerializer.Serialize(store)
            );
        }


        public IActionResult Province(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "Vui lòng chọn tỉnh/thành phố.";
                return RedirectToAction(nameof(Index));
            }

            var canonicalName = CuisineData.CanonicalProvinceName(name);
            var items = CuisineData.GetTopByProvince(canonicalName, 20);

            if (!items.Any())
            {
                TempData["Error"] = $"Không có dữ liệu ẩm thực cho '{canonicalName}'.";
                return RedirectToAction(nameof(Index));
            }

            // TÍNH TRỰC TIẾP TỪ ITEMS
            var overallAvg = items.Any()
                 ? items.Average(i => i.AveragePrice)
                 : 0m;

            var priceMin = items.Any()
                ? items.Min(i => i.AveragePrice)
                : 0m;

            var priceMax = items.Any()
                ? items.Max(i => i.AveragePrice)
                : 0m;

            var vm = new CuisineProvinceViewModel
            {
                Province = canonicalName,
                Items = items,
                OverallAveragePrice = overallAvg,
                PriceMin = priceMin,
                PriceMax = priceMax
            };

            // nếu bạn muốn giữ thêm các trường min/max/count trong VM, gán thêm ở đây
            // vm.PriceMin = priceMin; vm.PriceMax = priceMax; vm.TotalItems = items.Count;

            // Load review store (giữ nguyên)
            var reviewStore = LoadReviewStore();
            foreach (var food in items)
            {
                string id = BuildReviewId(canonicalName, food.Name);
                if (reviewStore.TryGetValue(id, out var list))
                {
                    vm.Reviews[food.Name] = list;
                    vm.AverageRatings[food.Name] = list.Any() ? list.Average(r => r.Rating) : 0;
                    vm.RatingsCount[food.Name] = list.Count;
                }
                else
                {
                    vm.Reviews[food.Name] = new List<ReviewItem>();
                    vm.AverageRatings[food.Name] = 0;
                    vm.RatingsCount[food.Name] = 0;
                }
            }

            return View(vm);
        }

      
        [HttpPost]
        public IActionResult AddReview(
                string province,
                string foodName,
                string displayName,
                int rating,
                string comment)
        {
            // 1️⃣ Load store từ Session
            var store = LoadReviewStore();

            // 2️⃣ Build key duy nhất cho từng món
            string reviewId = BuildReviewId(province, foodName);

            // 3️⃣ Nếu chưa có thì tạo mới
            if (!store.ContainsKey(reviewId))
                store[reviewId] = new List<ReviewItem>();

            // 4️⃣ Thêm review
            store[reviewId].Add(new ReviewItem
            {
                UserName = string.IsNullOrWhiteSpace(displayName)
                    ? "Ẩn danh"
                    : displayName.Trim(),

                Rating = Math.Clamp(rating, 1, 5), // ⭐ chống lỗi

                Comment = comment?.Trim() ?? ""
            });

            // 5️⃣ Lưu lại Session
            SaveReviewStore(store);

            // Debug (OK để giữ)
            Console.WriteLine("SESSION ID: " + HttpContext.Session.Id);

            // 6️⃣ Quay lại trang tỉnh
            return RedirectToAction("Province", new { name = province });
        }




        // -------------------------
        // Helpers
        // -------------------------


        public IActionResult Details(int id)
        {
            var item = CuisineData.GetAll()
                                  .FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }


        private string BuildReviewId(string province, string foodName)
        {
            return $"cuisine:{province}:{foodName}";
        }
    }
}
