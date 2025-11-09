using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TravelWeb.Services;

namespace TravelWeb.Controllers
{
    public class PassportController : Controller
    {
        private string CurrentUser => HttpContext.Session.GetString("Username") ?? "guest";

        [HttpGet]
        public IActionResult Index()
        {
            var state = GamificationPassportService.GetOrCreate(CurrentUser);
            ViewBag.Leaderboard = GamificationPassportService.GetLeaderboard(10);
            ViewBag.Discounts = RewardStore.GetByUser(CurrentUser);
            return View(state);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFestival(string? province)
        {
            GamificationPassportService.AddStamp(CurrentUser, PassportStampType.Festival, province);
            TempData["Success"] = "Đã đóng tem lễ hội!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCuisineReview(string province)
        {
            GamificationPassportService.AddStamp(CurrentUser, PassportStampType.CuisineReview, province);
            TempData["Success"] = "Đã đóng tem ẩm thực!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTrip(string destination)
        {
            GamificationPassportService.AddStamp(CurrentUser, PassportStampType.TripPlan, destination);
            TempData["Success"] = "Đã đóng tem hành trình!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClaimReward()
        {
            var state = GamificationPassportService.GetOrCreate(CurrentUser);
            if (state.TotalStamps < 5)
            {
                TempData["Error"] = "Chưa đủ điều kiện để yêu cầu phần thưởng.";
                return RedirectToAction("Index");
            }
            RewardStore.AddRequest(CurrentUser);
            TempData["Success"] = "Yêu cầu phần thưởng đã gửi. Admin sẽ duyệt sớm!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AdminRewards()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username != "admin")
            {
                TempData["Error"] = "Bạn không có quyền truy cập.";
                return RedirectToAction("Index");
            }
            var list = RewardStore.GetAllRequests();
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignDiscount(System.Guid requestId, string code, string description, System.DateTime expiresAt)
        {
            var username = HttpContext.Session.GetString("Username");
            if (username != "admin")
            {
                TempData["Error"] = "Bạn không có quyền thực hiện thao tác này.";
                return RedirectToAction("Index");
            }
            RewardStore.Approve(requestId, code, description, expiresAt);
            TempData["Success"] = "Đã cấp mã giảm giá cho người dùng.";
            return RedirectToAction("AdminRewards");
        }
    }
}
