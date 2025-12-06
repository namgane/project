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
    public class FavoritesController : Controller
    {
        private readonly TravelContext _context;

        public FavoritesController(TravelContext context)
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                TempData["Error"] = "Vui lòng đăng nhập để xem bộ sưu tập của bạn.";
                return RedirectToAction("Login", "Users");
            }

            var favorites = await _context.Favorites
                .Where(f => f.UserId == userId.Value)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();

            return View(favorites);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(string Id, string Type, string Title, string Subtitle, string Url, string ImageUrl)
        {
            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                TempData["Error"] = "Vui lòng đăng nhập để lưu vào bộ sưu tập.";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(Type) || string.IsNullOrWhiteSpace(Title))
            {
                TempData["Error"] = "Mục không hợp lệ.";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            // Kiểm tra xem đã có trong favorites chưa
            var exists = await _context.Favorites
                .AnyAsync(f => f.UserId == userId.Value && f.ItemId == Id && f.Type == Type);

            if (exists)
            {
                TempData["Info"] = "Mục này đã có trong Bộ sưu tập.";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            // Tạo favorite mới
            var favorite = new Favorite
            {
                UserId = userId.Value,
                ItemId = Id,
                Type = Type,
                Title = Title,
                Subtitle = Subtitle,
                Url = Url,
                ImageUrl = ImageUrl,
                CreatedAt = DateTime.UtcNow
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            // Log activity
            var location = ExtractLocationFromItemId(Id);
            await ActivityHistoryService.LogActivityAsync(
                _context,
                userId.Value,
                "Favorite",
                Id,
                Title,
                $"Đã lưu {Type} vào bộ sưu tập",
                location
            );

            TempData["Success"] = "Đã lưu vào Bộ sưu tập.";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                TempData["Error"] = "Vui lòng đăng nhập để thực hiện thao tác này.";
                return RedirectToAction("Index");
            }

            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId.Value);

            if (favorite == null)
            {
                TempData["Error"] = "Không tìm thấy mục trong bộ sưu tập.";
                return RedirectToAction("Index");
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đã xóa khỏi Bộ sưu tập.";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Extract location từ ItemId (ví dụ: cuisine:Hà Nội:Phở bò -> Hà Nội)
        /// </summary>
        private string? ExtractLocationFromItemId(string itemId)
        {
            if (string.IsNullOrEmpty(itemId)) return null;
            var parts = itemId.Split(':');
            return parts.Length > 1 ? parts[1] : null;
        }
    }
}
