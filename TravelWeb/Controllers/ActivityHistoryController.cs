/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelWeb.Data;
using TravelWeb.Models;
using TravelWeb.Services;
using System.Linq;
using System.Threading.Tasks;

namespace TravelWeb.Controllers
{
    public class ActivityHistoryController : Controller
    {
        private readonly TravelContext _context;

        public ActivityHistoryController(TravelContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy UserId từ session
        /// </summary>
        private int? GetCurrentUserId()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
                return null;
            return userId;
        }

        /// <summary>
        /// Xem lịch sử hoạt động của user hiện tại
        /// </summary>
        public async Task<IActionResult> Index(string? activityType = null, int page = 1, int pageSize = 20)
        {
            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                TempData["Error"] = "Vui lòng đăng nhập để xem lịch sử hoạt động.";
                return RedirectToAction("Login", "Users");
            }

            var query = _context.ActivityHistories
     .Where(a => a.UserId == userId.Value);

            // Filter by activity type if provided
            if (!string.IsNullOrEmpty(activityType))
            {
                query = query.Where(a => a.ActivityType == activityType);
            }

            // Always order after filtering
            query = query.OrderByDescending(a => a.CreatedAt);

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var activities = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(a => a.User)
                .ToListAsync();


            // Get statistics
            var stats = new
            {
                Total = await _context.ActivityHistories.CountAsync(a => a.UserId == userId.Value),
                Reviews = await _context.ActivityHistories.CountAsync(a => a.UserId == userId.Value && a.ActivityType == "Review"),
                Favorites = await _context.ActivityHistories.CountAsync(a => a.UserId == userId.Value && a.ActivityType == "Favorite"),
                Bookings = await _context.ActivityHistories.CountAsync(a => a.UserId == userId.Value && a.ActivityType == "Booking"),
                Payments = await _context.ActivityHistories.CountAsync(a => a.UserId == userId.Value && a.ActivityType == "Payment")
            };

            ViewBag.Activities = activities;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItems = totalItems;
            ViewBag.PageSize = pageSize;
            ViewBag.ActivityType = activityType;
            ViewBag.Stats = stats;
            ViewBag.ActivityTypes = new[] { "Review", "Favorite", "Booking", "Payment", "TripPlan" };

            return View();
        }
    }
}


*/