using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelWeb.Data;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class HotelsController : Controller
    {
        private readonly TravelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HotelsController(TravelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Hotels.ToListAsync());
        }

        // Create - chỉ admin
        public IActionResult Create()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username != "admin") return Unauthorized();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hotel hotel, IFormFile? ImageFile)
        {
            var username = HttpContext.Session.GetString("Username");
            if (username != "admin") return Unauthorized();

            // Xử lý upload hình ảnh
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/hotels");

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                hotel.HinhAnh = uniqueFileName;
            }

            if (ModelState.IsValid)
            {
                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm khách sạn thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        // Edit - chỉ admin
        public async Task<IActionResult> Edit(int id)
        {
            var username = HttpContext.Session.GetString("Username");
            if (username != "admin") return Unauthorized();
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Hotel hotel, IFormFile? ImageFile)
        {
            var username = HttpContext.Session.GetString("Username");
            if (username != "admin") return Unauthorized();
            if (id != hotel.Id) return BadRequest();

            // Xử lý upload hình ảnh mới
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Xóa hình cũ nếu có
                if (!string.IsNullOrEmpty(hotel.HinhAnh))
                {
                    string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images/hotels", hotel.HinhAnh);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/hotels");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                hotel.HinhAnh = uniqueFileName;
            }
            else
            {
                // Giữ lại hình ảnh cũ
                var existingHotel = await _context.Hotels.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
                if (existingHotel != null)
                {
                    hotel.HinhAnh = existingHotel.HinhAnh;
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật khách sạn thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Hotels.Any(e => e.Id == hotel.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        // Details - tất cả user đều xem được
        public async Task<IActionResult> Details(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        // Delete - chỉ admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var username = HttpContext.Session.GetString("Username");
            if (username != "admin") return Unauthorized();
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return NotFound();

            // Xóa hình ảnh nếu có
            if (!string.IsNullOrEmpty(hotel.HinhAnh))
            {
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images/hotels", hotel.HinhAnh);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Xóa khách sạn thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}