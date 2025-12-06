using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization; // Cần thêm cái này để xử lý định dạng thời gian
using TravelWeb.Data;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly TravelContext _context;

        public OrderController(TravelContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(int userId, string location, string time)
        {
            DateTime bookingTime;
            bool isValidTime = false;

            // 1. Thử parse trực tiếp (Trường hợp gửi lên đầy đủ ngày giờ)
            if (DateTime.TryParse(time, out bookingTime))
            {
                isValidTime = true;
            }
            // 2. Nếu thất bại, thử ghép Ngày hiện tại + Giờ gửi lên
            // Ví dụ: time = "14:30" => ghép thành "2023-12-06 14:30"
            else
            {
                string timeWithDate = $"{DateTime.Now:yyyy-MM-dd} {time}";
                if (DateTime.TryParse(timeWithDate, out bookingTime))
                {
                    isValidTime = true;
                }
            }

            // Nếu vẫn không được thì báo lỗi
            if (!isValidTime)
            {
                // Mẹo: In ra console hoặc debug để xem time nhận được là chuỗi gì
                Console.WriteLine($"Lỗi parse time. Chuỗi nhận được: '{time}'");
                return Json(new { success = false, message = $"Thời gian '{time}' không hợp lệ! Vui lòng nhập đúng định dạng (VD: 14:30)." });
            }

            try
            {
                var order = new Order
                {
                    UserId = userId, // Sử dụng biến userId truyền vào thay vì số cứng 3
                    Location = location,
                    Time = bookingTime,
                    CreatedDate = DateTime.Now
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                return Json(new { success = true, message = "Đặt lịch thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi server: " + ex.Message });
            }
        }
    }
}