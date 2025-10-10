using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelWeb.Data;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class ToursController : Controller
    {
        private readonly TravelContext _context;

        public ToursController(TravelContext context)
        {
            _context = context;
        }

        // GET: Tours
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tours.ToListAsync());
        }

        // GET: Tours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // GET: Tours/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenTour,DiaDiem,MoTa,SoNgay,Gia,HinhAnhFile")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                if (tour.HinhAnhFile != null && tour.HinhAnhFile.Length > 0)
                {
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    Directory.CreateDirectory(uploads);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(tour.HinhAnhFile.FileName);
                    var filePath = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await tour.HinhAnhFile.CopyToAsync(stream);
                    }

                    tour.HinhAnh = "/images/" + fileName;
                }
                else
                {
                    ModelState.AddModelError("HinhAnhFile", "Vui lòng chọn hình ảnh.");
                    return View(tour);
                }

                _context.Add(tour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }


        // GET: Tours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        // POST: Tours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenTour,DiaDiem,MoTa,SoNgay,Gia,HinhAnh,HinhAnhFile")] Tour tour)
        {
            if (id != tour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Nếu có upload file mới
                    if (tour.HinhAnhFile != null && tour.HinhAnhFile.Length > 0)
                    {
                        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        Directory.CreateDirectory(uploads);

                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(tour.HinhAnhFile.FileName);
                        var filePath = Path.Combine(uploads, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await tour.HinhAnhFile.CopyToAsync(stream);
                        }

                        tour.HinhAnh = "/images/" + fileName; // cập nhật đường dẫn mới
                    }
                    else
                    {
                        // Giữ ảnh cũ nếu không chọn file mới
                        _context.Entry(tour).Property(x => x.HinhAnh).IsModified = false;
                    }

                    _context.Update(tour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourExists(tour.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }


        // GET: Tours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour != null)
            {
                _context.Tours.Remove(tour);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourExists(int id)
        {
            return _context.Tours.Any(e => e.Id == id);
        }
    }
}
