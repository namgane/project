using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelWeb.Data;
using TravelWeb.Models;
using TravelWeb.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TravelWeb.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly TravelContext _context;

        public ReviewsController(TravelContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy UserId từ session (null nếu chưa đăng nhập)
        /// </summary>
        private int? GetCurrentUserId()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
                return null;
            return userId;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(string cuisineId, string displayName, int rating, string comment, string returnProvince, string returnTo = "Province")
        {
            if (string.IsNullOrWhiteSpace(cuisineId) || rating < 1 || rating > 5 || string.IsNullOrWhiteSpace(displayName))
            {
                TempData["Error"] = "Vui lòng nhập tên và đánh giá hợp lệ (1-5 sao).";
                return RedirectToAction("Province", "Cuisine", new { name = returnProvince });
            }

            var userId = GetCurrentUserId();

            // Tạo review mới
            var review = new Review
            {
                CuisineId = cuisineId,
                DisplayName = displayName,
                Rating = rating,
                Comment = comment ?? string.Empty,
                UserId = userId, // null nếu anonymous
                CreatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // Log activity nếu user đã đăng nhập
            if (userId.HasValue)
            {
                // Parse cuisineId để lấy thông tin
                var parts = cuisineId.Replace("cuisine:", "").Split(':');
                var province = parts.Length > 0 ? parts[0] : returnProvince;
                var dishName = parts.Length > 1 ? parts[1] : "Món ăn";

                await ActivityHistoryService.LogActivityAsync(
                    _context,
                    userId.Value,
                    "Review",
                    cuisineId,
                    dishName,
                    $"Đánh giá {rating} sao cho món ăn",
                    province
                );
            }

            // Add gamification stamp (giữ lại logic cũ)
            var username = HttpContext.Session.GetString("Username") ?? "guest";
            GamificationPassportService.AddStamp(username, PassportStampType.CuisineReview, returnProvince);
            
            TempData["Success"] = "Cảm ơn đánh giá của bạn!";
            
            // Redirect based on returnTo parameter
            if (returnTo == "Details")
            {
                var parts = cuisineId.Replace("cuisine:", "").Split(':');
                if (parts.Length == 2)
                {
                    return RedirectToAction("Details", "Cuisine", new { province = parts[0], name = parts[1] });
                }
            }
            
            return RedirectToAction("Province", "Cuisine", new { name = returnProvince });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearAll()
        {
            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                TempData["Error"] = "Bạn cần đăng nhập để thực hiện thao tác này.";
                return RedirectToAction("Index", "Favorites");
            }

            // Xóa tất cả reviews của user (chỉ xóa reviews có UserId)
            var userReviews = await _context.Reviews
                .Where(r => r.UserId == userId)
                .ToListAsync();

            _context.Reviews.RemoveRange(userReviews);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đã xóa tất cả đánh giá của bạn.";
            return RedirectToAction("Index", "Favorites");
        }

        /// <summary>
        /// Lấy tất cả reviews cho một cuisine (public, mọi người đều xem được)
        /// </summary>
        public async Task<IActionResult> GetByCuisine(string cuisineId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.CuisineId == cuisineId)
                .OrderByDescending(r => r.CreatedAt)
                .Include(r => r.User)
                .ToListAsync();

            return Json(reviews.Select(r => new
            {
                r.Id,
                r.DisplayName,
                r.Rating,
                r.Comment,
                r.CreatedAt,
                UserName = r.User?.Username
            }));
        }
    }
}
