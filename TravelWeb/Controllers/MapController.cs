using Microsoft.AspNetCore.Mvc;
using TravelWeb.Data;
using TravelWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelWeb.Controllers
{
    public class MapController : Controller
    {
        private readonly TravelContext _context;

        public MapController(TravelContext context)
        {
            _context = context;
        }

        // 1. Hiển thị Map
        public IActionResult Index(string city, double? lat, double? lng)
        {
            ViewBag.City = string.IsNullOrEmpty(city) ? "TP. Hồ Chí Minh" : city;
            ViewBag.Lat = lat ?? 10.7769;
            ViewBag.Lng = lng ?? 106.7009;

            // Lấy danh sách địa điểm thường
            ViewBag.Locations = _context.Locations.ToList();

            // ✅ QUAN TRỌNG: Lấy danh sách Virtual Tour để hiển thị con mắt
            ViewBag.VirtualTours = _context.VirtualTours.ToList();

            return View();
        }

        // 2. Upload ảnh (Dùng cho form thêm địa điểm)
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest(new { message = "Chưa chọn ảnh" });

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return Json(new { imagePath = "/images/" + fileName });
        }

        // 3. API Thêm địa điểm thường (JSON)
        [HttpPost]
        public IActionResult AddLocation([FromBody] Location model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Locations.Add(model);
                    _context.SaveChanges();
                    return Ok(new { success = true, message = "Lưu thành công!" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { success = false, message = ex.Message });
                }
            }
            return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });
        }
    }
}