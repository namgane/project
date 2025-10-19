using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelWeb.Data;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class BookingsController : Controller
    {
        private readonly TravelContext _context;
        public BookingsController(TravelContext context) => _context = context;

        // Customer tạo booking
        [HttpGet]
        public async Task<IActionResult> Create(int hotelId)
        {
            // Kiểm tra đăng nhập
            var username = HttpContext.Session.GetString("Username");
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
            {
                TempData["Error"] = "Vui lòng đăng nhập để đặt phòng!";
                return RedirectToAction("Login", "Users");
            }

            // Kiểm tra quyền: chỉ Customer mới được đặt phòng
            if (role != "Customer")
            {
                TempData["Error"] = "Chỉ khách hàng mới có thể đặt phòng!";
                return RedirectToAction("Index", "Hotels");
            }

            var hotel = await _context.Hotels.FindAsync(hotelId);
            if (hotel == null)
            {
                TempData["Error"] = "Không tìm thấy khách sạn!";
                return RedirectToAction("Index", "Hotels");
            }

            // Kiểm tra phòng còn trống
            if (hotel.SoPhong <= 0)
            {
                TempData["Error"] = "Khách sạn hiện đã hết phòng!";
                return RedirectToAction("Details", "Hotels", new { id = hotelId });
            }

            // Lấy UserId từ username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                TempData["Error"] = "Không tìm thấy thông tin người dùng!";
                return RedirectToAction("Login", "Users");
            }

            ViewBag.Hotel = hotel;

            // Khởi tạo model với giá trị mặc định
            var booking = new Booking
            {
                HotelId = hotelId,
                UserId = user.Id,
                CheckInDate = DateTime.Now.AddDays(1),
                CheckOutDate = DateTime.Now.AddDays(2),
                SoLuongPhong = 1
            };

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            var username = HttpContext.Session.GetString("Username");
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
            {
                TempData["Error"] = "Vui lòng đăng nhập để đặt phòng!";
                return RedirectToAction("Login", "Users");
            }

            if (role != "Customer")
            {
                TempData["Error"] = "Chỉ khách hàng mới có thể đặt phòng!";
                return RedirectToAction("Index", "Hotels");
            }

            // Lấy UserId từ username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                TempData["Error"] = "Không tìm thấy thông tin người dùng!";
                return RedirectToAction("Login", "Users");
            }

            // Lấy thông tin khách sạn
            var hotel = await _context.Hotels.FindAsync(booking.HotelId);
            if (hotel == null)
            {
                TempData["Error"] = "Không tìm thấy khách sạn!";
                return RedirectToAction("Index", "Hotels");
            }

            // Kiểm tra số phòng còn đủ không
            if (hotel.SoPhong < booking.SoLuongPhong)
            {
                ModelState.AddModelError("SoLuongPhong", $"Khách sạn chỉ còn {hotel.SoPhong} phòng trống");
            }

            // Kiểm tra ngày hợp lệ
            if (booking.CheckOutDate <= booking.CheckInDate)
            {
                ModelState.AddModelError("CheckOutDate", "Ngày trả phòng phải sau ngày nhận phòng");
            }

            if (booking.CheckInDate < DateTime.Now.Date)
            {
                ModelState.AddModelError("CheckInDate", "Ngày nhận phòng không được là ngày trong quá khứ");
            }

            if (ModelState.IsValid)
            {
                booking.UserId = user.Id;
                booking.TrangThai = "Chờ Xác Nhận";

                // Tính tổng tiền
                int soNgay = (booking.CheckOutDate - booking.CheckInDate).Days;
                decimal tongTien = soNgay * booking.SoLuongPhong * hotel.GiaMoiDem;

                // Lưu thông tin tổng tiền vào TempData để hiển thị
                TempData["SoNgay"] = soNgay;
                TempData["TongTien"] = tongTien.ToString("N0");

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                TempData["Success"] = $"Đặt phòng thành công! Tổng tiền: {tongTien.ToString("C")} ({soNgay} đêm × {booking.SoLuongPhong} phòng). Chờ xác nhận từ khách sạn!";
                return RedirectToAction("MyBookings");
            }

            // Nếu ModelState không hợp lệ, load lại hotel
            ViewBag.Hotel = hotel;
            return View(booking);
        }

        // Customer xem lịch sử booking
        public async Task<IActionResult> MyBookings()
        {
            var username = HttpContext.Session.GetString("Username");
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
            {
                TempData["Error"] = "Vui lòng đăng nhập để xem lịch sử đặt phòng!";
                return RedirectToAction("Login", "Users");
            }

            if (role != "Customer")
            {
                TempData["Error"] = "Bạn không có quyền truy cập trang này!";
                return RedirectToAction("Index", "Hotels");
            }

            // Lấy UserId từ username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                TempData["Error"] = "Không tìm thấy thông tin người dùng!";
                return RedirectToAction("Login", "Users");
            }

            var bookings = await _context.Bookings
                .Include(b => b.Hotel)
                .Where(b => b.UserId == user.Id)
                .OrderByDescending(b => b.Id)
                .ToListAsync();
            return View(bookings);
        }

        // Customer hủy booking (chỉ hủy trước ngày nhận phòng)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var username = HttpContext.Session.GetString("Username");
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
            {
                TempData["Error"] = "Vui lòng đăng nhập!";
                return RedirectToAction("Login", "Users");
            }

            // Lấy UserId từ username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                TempData["Error"] = "Không tìm thấy thông tin người dùng!";
                return RedirectToAction("Login", "Users");
            }

            var booking = await _context.Bookings
                .Include(b => b.Hotel)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                TempData["Error"] = "Không tìm thấy đặt phòng!";
                return RedirectToAction("MyBookings");
            }

            if (booking.UserId != user.Id)
            {
                TempData["Error"] = "Bạn không có quyền hủy đặt phòng này!";
                return RedirectToAction("MyBookings");
            }

            // Kiểm tra xem đã đến ngày nhận phòng chưa
            if (booking.CheckInDate.Date <= DateTime.Now.Date)
            {
                TempData["Error"] = "Không thể hủy đặt phòng đã đến ngày nhận phòng hoặc đã qua ngày nhận phòng!";
                return RedirectToAction("MyBookings");
            }

            // Chỉ cho phép hủy nếu chưa đến ngày nhận phòng
            if (booking.TrangThai == "Chờ Xác Nhận")
            {
                booking.TrangThai = "Đã Hủy";
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã hủy đặt phòng thành công!";
            }
            else if (booking.TrangThai == "Đã Xác Nhận")
            {
                // Nếu đã xác nhận, khi hủy thì trả lại phòng
                booking.TrangThai = "Đã Hủy";
                booking.Hotel.SoPhong += booking.SoLuongPhong;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã hủy đặt phòng và hoàn lại phòng thành công!";
            }
            else
            {
                TempData["Error"] = "Không thể hủy đặt phòng này!";
            }

            return RedirectToAction("MyBookings");
        }

        // Hotel hoặc Admin xem các booking
        public async Task<IActionResult> HotelBookings()
        {
            var username = HttpContext.Session.GetString("Username");
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(username))
            {
                TempData["Error"] = "Vui lòng đăng nhập!";
                return RedirectToAction("Login", "Users");
            }

            // Admin thì xem tất cả bookings
            if (username == "admin")
            {
                var allBookings = await _context.Bookings
                    .Include(b => b.User)
                    .Include(b => b.Hotel)
                    .OrderByDescending(b => b.Id)
                    .ToListAsync();

                ViewBag.IsAdmin = true;
                return View(allBookings);
            }

            // Hotel chỉ xem bookings của khách sạn mình
            if (role != "Hotel")
            {
                TempData["Error"] = "Bạn không có quyền truy cập trang này!";
                return RedirectToAction("Index", "Hotels");
            }

            var hotel = await _context.Hotels
                .Include(h => h.Owner)
                .FirstOrDefaultAsync(h => h.Owner.Username == username);

            if (hotel == null)
            {
                TempData["Error"] = "Không tìm thấy khách sạn của bạn!";
                return RedirectToAction("Index", "Hotels");
            }

            var bookings = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Hotel)
                .Where(b => b.HotelId == hotel.Id)
                .OrderByDescending(b => b.Id)
                .ToListAsync();

            ViewBag.IsAdmin = false;
            return View(bookings);
        }

        // Hotel hoặc Admin xác nhận/từ chối booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var username = HttpContext.Session.GetString("Username");
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(username))
            {
                TempData["Error"] = "Vui lòng đăng nhập!";
                return RedirectToAction("Login", "Users");
            }

            var booking = await _context.Bookings
                .Include(b => b.Hotel)
                .ThenInclude(h => h.Owner)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                TempData["Error"] = "Không tìm thấy đặt phòng!";
                return RedirectToAction("HotelBookings");
            }

            // Kiểm tra quyền: Admin hoặc chủ khách sạn
            bool isAdmin = username == "admin";
            bool isHotelOwner = booking.Hotel?.Owner?.Username == username;

            if (!isAdmin && !isHotelOwner)
            {
                TempData["Error"] = "Bạn không có quyền cập nhật đặt phòng này!";
                return RedirectToAction("HotelBookings");
            }

            // Chỉ cho phép cập nhật nếu đang ở trạng thái "Chờ Xác Nhận"
            if (booking.TrangThai == "Chờ Xác Nhận")
            {
                if (status == "Đã Xác Nhận")
                {
                    // Kiểm tra lại số phòng trước khi xác nhận
                    if (booking.Hotel.SoPhong < booking.SoLuongPhong)
                    {
                        TempData["Error"] = "Không đủ phòng trống để xác nhận đặt phòng này!";
                        return RedirectToAction("HotelBookings");
                    }

                    // Trừ số phòng khi xác nhận
                    booking.TrangThai = status;
                    booking.Hotel.SoPhong -= booking.SoLuongPhong;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Đã xác nhận đặt phòng thành công!";
                }
                else if (status == "Đã Hủy")
                {
                    // Từ chối không trừ phòng
                    booking.TrangThai = status;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Đã từ chối đặt phòng!";
                }
                else
                {
                    booking.TrangThai = status;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Đã cập nhật trạng thái đặt phòng!";
                }
            }
            else
            {
                TempData["Error"] = "Không thể cập nhật trạng thái đặt phòng này!";
            }

            return RedirectToAction("HotelBookings");
        }

        // Background service sẽ gọi action này để tự động cập nhật phòng
        // Hoặc có thể chạy bằng cron job
        public async Task<IActionResult> AutoUpdateRooms()
        {
            // Lấy tất cả booking đã xác nhận và đã đến ngày trả phòng
            var expiredBookings = await _context.Bookings
                .Include(b => b.Hotel)
                .Where(b => b.TrangThai == "Đã Xác Nhận" && b.CheckOutDate.Date <= DateTime.Now.Date)
                .ToListAsync();

            foreach (var booking in expiredBookings)
            {
                // Cập nhật trạng thái và trả lại phòng
                booking.TrangThai = "Đã Hoàn Thành";
                booking.Hotel.SoPhong += booking.SoLuongPhong;
            }

            if (expiredBookings.Any())
            {
                await _context.SaveChangesAsync();
            }

            return Ok(new { updated = expiredBookings.Count });
        }
    }
}