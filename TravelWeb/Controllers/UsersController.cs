using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelWeb.Data;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class UsersController : Controller
    {
        private readonly TravelContext _context;

        public UsersController(TravelContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin đăng nhập.";
                return View();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu.";
                return View();
            }

            // LƯU SESSION - BỔ SUNG UserId
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            TempData["Success"] = $"Xin chào {user.Username}!";

            // Redirect theo role
            if (user.Role == "Admin")
                return RedirectToAction("Dashboard", "Admin");
            else if (user.Role == "Hotel")
                return RedirectToAction("HotelBookings", "Bookings");
            else
                return RedirectToAction("Index", "Hotels");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Đã đăng xuất thành công!";
            return RedirectToAction("Login", "Users");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User model)
        {
            ModelState.Remove("Role");
            model.Role = "Customer";

            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                ViewBag.Error = $"Dữ liệu không hợp lệ: {errors}";
                return View(model);
            }

            var exists = await _context.Users.AnyAsync(u => u.Username == model.Username);
            if (exists)
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại.";
                return View(model);
            }

            _context.Users.Add(model);
            try
            {
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"LỖI: {ex.InnerException?.Message ?? ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            return View(user);
        }
    }
}