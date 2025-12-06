using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelWeb.Data;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    // Class DTO để nhận dữ liệu từ Javascript
    public class VirtualTourRequest
    {
        public string LocationName { get; set; }
        public string StreetViewLink { get; set; }
    }

    public class VirtualToursController : Controller
    {
        private readonly TravelContext _context;

        public VirtualToursController(TravelContext context)
        {
            _context = context;
        }

        // ✅ API nhận JSON từ bản đồ
        [HttpPost]
        public async Task<IActionResult> CreateJson([FromBody] VirtualTourRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.StreetViewLink))
            {
                return Json(new { success = false, message = "Link không hợp lệ!" });
            }

            // 1. Tìm Tour mặc định (Cái bạn vừa Insert vào DB)
            var defaultTour = await _context.Tours.FirstOrDefaultAsync();

            // Phòng hờ nếu DB vẫn rỗng
            if (defaultTour == null)
            {
                return Json(new { success = false, message = "Lỗi: Không tìm thấy Tour nào trong Database (Vui lòng kiểm tra bảng Tours)." });
            }

            // 2. Tạo Virtual Tour
            var vt = new VirtualTour
            {
                LocationName = request.LocationName,
                StreetViewLink = request.StreetViewLink,
                TourId = defaultTour.Id, // Gán ID của Default Tour
                ImageUrl = "default.jpg"
            };

            // 3. Lưu
            try
            {
                _context.VirtualTours.Add(vt);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Thêm Virtual Tour thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi Server: " + ex.Message });
            }
        }
    }
}