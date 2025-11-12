using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelWeb.Data;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class VirtualToursController : Controller
    {
        private readonly TravelContext _context;

        public VirtualToursController(TravelContext context)
        {
            _context = context;
        }

        // GET: VirtualTours
        public async Task<IActionResult> Index()
        {
            var tours = await _context.VirtualTours.Include(v => v.Tour).ToListAsync();
            return View(tours);
        }

        // GET: VirtualTours/Create
        public IActionResult Create(double? lat, double? lng)
        {
            ViewBag.Lat = lat;
            ViewBag.Lng = lng;
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "TenTour");
            return View();
        }

        // POST: VirtualTours/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(double? lat, double? lng, string StreetViewLink)
        {
            if (lat == null || lng == null)
            {
                TempData["ErrorMessage"] = "Không có tọa độ hợp lệ.";
                return RedirectToAction("Index", "Map");
            }

            // Lấy Tour đầu tiên (có thể sửa sau để chọn cụ thể)
            var firstTour = await _context.Tours.FirstOrDefaultAsync();
            if (firstTour == null)
            {
                TempData["ErrorMessage"] = "Chưa có Tour nào trong hệ thống!";
                return RedirectToAction("Index", "Map");
            }

            // Đảm bảo định dạng dấu chấm (.) cho số thập phân
            var locationName = $"{lat.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)},{lng.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)}";

            var virtualTour = new VirtualTour
            {
                LocationName = locationName,
                StreetViewLink = StreetViewLink,
                TourId = firstTour.Id
            };

            try
            {
                _context.Add(virtualTour);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Thêm Virtual Tour thành công!";
                // Chuyển hướng trở lại bản đồ, hiển thị đúng vị trí mới thêm
                return RedirectToAction("Index", "Map", new { city = "TP.HCM", lat = lat.Value, lng = lng.Value });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi lưu Virtual Tour: {ex.Message}";
                return RedirectToAction("Index", "Map");
            }
        }

        // GET: VirtualTours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var vt = await _context.VirtualTours
                .Include(v => v.Tour)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vt == null) return NotFound();

            return View(vt);
        }

        // POST: VirtualTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vt = await _context.VirtualTours.FindAsync(id);
            if (vt != null)
            {
                _context.VirtualTours.Remove(vt);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa Virtual Tour thành công!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool VirtualTourExists(int id)
        {
            return _context.VirtualTours.Any(e => e.Id == id);
        }
    }
}
