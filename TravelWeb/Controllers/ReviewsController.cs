using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TravelWeb.Models;
using TravelWeb.Services;

namespace TravelWeb.Controllers
{
    public class ReviewsController : Controller
    {
        private const string SessionKey = "Reviews"; // Dictionary<string, List<ReviewItem>> by CuisineId

        private Dictionary<string, List<ReviewItem>> GetStore()
        {
            var json = HttpContext.Session.GetString(SessionKey);
            if (string.IsNullOrEmpty(json)) return new Dictionary<string, List<ReviewItem>>();
            return JsonSerializer.Deserialize<Dictionary<string, List<ReviewItem>>>(json) ?? new Dictionary<string, List<ReviewItem>>();
        }

        private void SaveStore(Dictionary<string, List<ReviewItem>> store)
        {
            var json = JsonSerializer.Serialize(store);
            HttpContext.Session.SetString(SessionKey, json);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(string cuisineId, string displayName, int rating, string comment, string returnProvince)
        {
            if (string.IsNullOrWhiteSpace(cuisineId) || rating < 1 || rating > 5 || string.IsNullOrWhiteSpace(displayName))
            {
                TempData["Error"] = "Vui lòng nhập tên và đánh giá hợp lệ (1-5 sao).";
                return RedirectToAction("Province", "Cuisine", new { name = returnProvince });
            }

            var store = GetStore();
            if (!store.ContainsKey(cuisineId)) store[cuisineId] = new List<ReviewItem>();

            store[cuisineId].Add(new ReviewItem
            {
                CuisineId = cuisineId,
                DisplayName = displayName,
                Rating = rating,
                Comment = comment ?? string.Empty
            });

            SaveStore(store);
            // Add gamification stamp
            var username = HttpContext.Session.GetString("Username") ?? "guest";
            GamificationPassportService.AddStamp(username, PassportStampType.CuisineReview, returnProvince);
            TempData["Success"] = "Cảm ơn đánh giá của bạn!";
            return RedirectToAction("Province", "Cuisine", new { name = returnProvince });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClearAll()
        {
            HttpContext.Session.Remove(SessionKey);
            TempData["Success"] = "Đã xóa tất cả đánh giá (trên phiên hiện tại).";
            return RedirectToAction("Index", "Favorites");
        }
    }
}


