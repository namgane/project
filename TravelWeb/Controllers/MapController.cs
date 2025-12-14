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
            try
            {
                ViewBag.City = string.IsNullOrEmpty(city) ? "TP. Hồ Chí Minh" : city;
                
                // Nếu không có tọa độ, dùng tọa độ mặc định theo thành phố
                if (!lat.HasValue || !lng.HasValue)
                {
                    if (city == "Hà Nội")
                    {
                        // Tọa độ Hồ Hoàn Kiếm
                        ViewBag.Lat = 21.0285;
                        ViewBag.Lng = 105.8542;
                    }
                    else
                    {
                        // Mặc định là TP.HCM
                        ViewBag.Lat = 10.7769;
                        ViewBag.Lng = 106.7009;
                    }
                }
                else
                {
                    ViewBag.Lat = lat.Value;
                    ViewBag.Lng = lng.Value;
                }

                // Lấy danh sách địa điểm thường
                ViewBag.Locations = _context.Locations?.ToList() ?? new List<Location>();

                // ✅ QUAN TRỌNG: Lấy danh sách Virtual Tour để hiển thị con mắt
                ViewBag.VirtualTours = _context.VirtualTours?.ToList() ?? new List<VirtualTour>();

                return View();
            }
            catch (Exception ex)
            {
                // Log lỗi và trả về view với dữ liệu rỗng
                ViewBag.Locations = new List<Location>();
                ViewBag.VirtualTours = new List<VirtualTour>();
                ViewBag.ErrorMessage = "Có lỗi xảy ra khi tải dữ liệu bản đồ. Vui lòng thử lại sau.";
                return View();
            }
        }

        // 2. Upload ảnh (Dùng cho form thêm địa điểm)
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                    return BadRequest(new { message = "Chưa chọn ảnh" });

                if (string.IsNullOrEmpty(imageFile.FileName))
                    return BadRequest(new { message = "Tên file không hợp lệ" });

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                
                // Đảm bảo thư mục tồn tại
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }
                
                var path = Path.Combine(imagesPath, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return Json(new { imagePath = "/images/" + fileName });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi upload ảnh: " + ex.Message });
            }
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