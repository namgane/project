using Microsoft.AspNetCore.Mvc;
using TravelWeb.Data;
using TravelWeb.Models;
using System.Linq;

namespace TravelWeb.Controllers
{
    public class MapController : Controller
    {
        private readonly TravelContext _context;

        public MapController(TravelContext context)
        {
            _context = context;
        }

        // ======= Hiển thị bản đồ =======
        public IActionResult Index(string city, double lat, double lng)
        {
            ViewBag.City = city;
            ViewBag.Lat = lat;
            ViewBag.Lng = lng;

            // Lấy toàn bộ danh sách địa điểm từ database
            ViewBag.Locations = _context.Locations.ToList();

            return View();
        }

        // ======= API: Thêm địa điểm mới =======
        [HttpPost]
        public IActionResult AddLocation([FromBody] Location model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Locations.Add(model);
                    _context.SaveChanges();
                    return Ok(new { success = true, message = "Đã lưu địa điểm thành công!" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { success = false, message = $"Lỗi lưu vào DB: {ex.Message}" });
                }
            }

            return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("Không có ảnh được chọn.");

            var fileName = Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            var imagePath = $"/images/{fileName}";

            return Json(new { imagePath });
        }
    }
}
