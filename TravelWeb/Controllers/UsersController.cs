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

        // ===================== LOGIN =====================
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

            // 🔹 Kiểm tra Email để xác định trạng thái phê duyệt
            // Quy ước: Email = "pending@approval.com" → Chờ duyệt
            //          Email = "rejected@approval.com" → Bị từ chối
            if (user.Email == "pending@approval.com")
            {
                ViewBag.Error = "Tài khoản của bạn đang chờ phê duyệt. Vui lòng đợi Admin xác nhận.";
                return View();
            }

            if (user.Email == "rejected@approval.com")
            {
                ViewBag.Error = "Tài khoản của bạn đã bị từ chối. Vui lòng liên hệ Admin.";
                return View();
            }

            // Lưu SESSION
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            TempData["Success"] = $"Xin chào {user.Username}!";

            // Redirect theo role
            return user.Role switch
            {
                "Admin" => RedirectToAction("PendingUsers", "Users"),
                "Hotel" => RedirectToAction("HotelBookings", "Bookings"),
                "Flight" => RedirectToAction("Index", "Tours"), // Tạm redirect về Tours
                "Train" => RedirectToAction("Index", "Tours"),
                "Bus" => RedirectToAction("Index", "Tours"),
                _ => RedirectToAction("Index", "Tours")
            };
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Đã đăng xuất thành công!";
            return RedirectToAction("Login", "Users");
        }

        // ===================== REGISTER =====================
        public IActionResult Register()
        {
            ViewBag.Roles = new List<string> { "Customer", "Hotel", "Flight", "Train", "Bus" };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User model)
        {
            ViewBag.Roles = new List<string> { "Customer", "Hotel", "Flight", "Train", "Bus" };

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

            // 🔹 Xử lý Email theo Role
            string originalEmail = model.Email; // Lưu email thật của user

            if (model.Role == "Customer")
            {
                // Customer → Duyệt tự động, giữ nguyên email
                // Không cần làm gì
            }
            else
            {
                // Hotel, Flight, Train, Bus → Chờ phê duyệt
                // Lưu email thật vào Phone field tạm thời, set Email = "pending@approval.com"
                model.Phone = $"EMAIL:{originalEmail}|PHONE:{model.Phone}"; // Lưu cả email và phone
                model.Email = "pending@approval.com"; // Đánh dấu chờ duyệt
            }

            _context.Users.Add(model);
            try
            {
                await _context.SaveChangesAsync();

                if (model.Role == "Customer")
                {
                    TempData["Success"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                }
                else
                {
                    TempData["Success"] = "Đăng ký thành công! Tài khoản của bạn đang chờ Admin phê duyệt.";
                }

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"LỖI: {ex.InnerException?.Message ?? ex.Message}";
                return View(model);
            }
        }

        // ===================== ADMIN: QUẢN LÝ PHÊ DUYỆT =====================
        public async Task<IActionResult> PendingUsers()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                TempData["Error"] = "Bạn không có quyền truy cập trang này.";
                return RedirectToAction("Index", "Tours");
            }

            // Lấy users có Email = "pending@approval.com"
            var pendingUsers = await _context.Users
.Where(u => u.Email == "pending@approval.com")
                .ToListAsync();

            return View(pendingUsers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveUser(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return Json(new { success = false, message = "Không có quyền" });
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return Json(new { success = false, message = "Không tìm thấy user" });
            }

            // 🔹 Khôi phục Email và Phone từ trường Phone
            if (user.Phone != null && user.Phone.StartsWith("EMAIL:"))
            {
                var parts = user.Phone.Split("|");
                var emailPart = parts[0].Replace("EMAIL:", "");
                var phonePart = parts.Length > 1 ? parts[1].Replace("PHONE:", "") : "";

                user.Email = emailPart; // Khôi phục email thật
                user.Phone = phonePart; // Khôi phục phone thật
            }
            else
            {
                user.Email = "approved@travel.com"; // Email mặc định nếu không có
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = $"Đã phê duyệt tài khoản {user.Username}";
            return RedirectToAction("PendingUsers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectUser(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return Json(new { success = false, message = "Không có quyền" });
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return Json(new { success = false, message = "Không tìm thấy user" });
            }

            // 🔹 Đánh dấu từ chối
            user.Email = "rejected@approval.com";
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Đã từ chối tài khoản {user.Username}";
            return RedirectToAction("PendingUsers");
        }

        // ===================== USER LIST =====================
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // ===================== DETAILS =====================
        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            return View(user);
        }
    }
}