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
            var travelContext = _context.VirtualTours.Include(v => v.Tour);
            return View(await travelContext.ToListAsync());
        }

        // GET: VirtualTours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var virtualTour = await _context.VirtualTours
                .Include(v => v.Tour)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (virtualTour == null)
                return NotFound();

            return View(virtualTour);
        }

        // GET: VirtualTours/Create
        public IActionResult Create()
        {
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "DiaDiem");
            return View();
        }

        // POST: VirtualTours/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationName,StreetViewLink,TourId")] VirtualTour virtualTour)
        {
            // Debug: In ra lỗi ModelState
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine($"ModelState Error: {error.ErrorMessage}");
                }
            }

            // Kiểm tra link embed trước
            if (!string.IsNullOrEmpty(virtualTour.StreetViewLink) && !virtualTour.StreetViewLink.Contains("embed"))
            {
                ModelState.AddModelError("StreetViewLink", "Vui lòng nhập link nhúng hợp lệ (có chứa 'embed').");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(virtualTour);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Thêm Virtual Tour thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error saving: {ex.Message}");
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
                }
            }

            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "DiaDiem", virtualTour.TourId);
            return View(virtualTour);
        }

        // GET: VirtualTours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var virtualTour = await _context.VirtualTours.FindAsync(id);
            if (virtualTour == null)
                return NotFound();

            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "DiaDiem", virtualTour.TourId);
            return View(virtualTour);
        }

        // POST: VirtualTours/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LocationName,StreetViewLink,TourId")] VirtualTour virtualTour)
        {
            if (id != virtualTour.Id)
                return NotFound();

            // Kiểm tra link embed
            if (!string.IsNullOrEmpty(virtualTour.StreetViewLink) && !virtualTour.StreetViewLink.Contains("embed"))
            {
                ModelState.AddModelError("StreetViewLink", "Vui lòng nhập link nhúng hợp lệ (có chứa 'embed').");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(virtualTour);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật Virtual Tour thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VirtualTourExists(virtualTour.Id))
                        return NotFound();
                    else
                        throw;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error updating: {ex.Message}");
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật: " + ex.Message);
                }
            }

            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "DiaDiem", virtualTour.TourId);
            return View(virtualTour);
        }

        // GET: VirtualTours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var virtualTour = await _context.VirtualTours
                .Include(v => v.Tour)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (virtualTour == null)
                return NotFound();

            return View(virtualTour);
        }

        // POST: VirtualTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var virtualTour = await _context.VirtualTours.FindAsync(id);
            if (virtualTour != null)
            {
                _context.VirtualTours.Remove(virtualTour);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa Virtual Tour thành công!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VirtualTourExists(int id)
        {
            return _context.VirtualTours.Any(e => e.Id == id);
        }
    }
}