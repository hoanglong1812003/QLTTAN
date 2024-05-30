using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTTAN.Data;

namespace QLTTAN.Controllers
{
    public class HoadonchitieuxController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public HoadonchitieuxController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Hoadonchitieux
        public async Task<IActionResult> Index()
        {
            var quanLyTrungTamAnhNguContext = _context.Hoadonchitieus.Include(h => h.ManvNavigation);
            return View(await quanLyTrungTamAnhNguContext.ToListAsync());
        }

        // GET: Hoadonchitieux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoadonchitieu = await _context.Hoadonchitieus
                .Include(h => h.ManvNavigation)
                .FirstOrDefaultAsync(m => m.Mahd == id);
            if (hoadonchitieu == null)
            {
                return NotFound();
            }

            return View(hoadonchitieu);
        }

        // GET: Hoadonchitieux/Create
        public IActionResult Create()
        {
            ViewData["Manv"] = new SelectList(_context.Nhanviens, "Manv", "Manv");
            return View();
        }

        // POST: Hoadonchitieux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mahd,Manv,Noidung,Sotien")] Hoadonchitieu hoadonchitieu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoadonchitieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Manv"] = new SelectList(_context.Nhanviens, "Manv", "Manv", hoadonchitieu.Manv);
            return View(hoadonchitieu);
        }

        // GET: Hoadonchitieux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoadonchitieu = await _context.Hoadonchitieus.FindAsync(id);
            if (hoadonchitieu == null)
            {
                return NotFound();
            }
            ViewData["Manv"] = new SelectList(_context.Nhanviens, "Manv", "Manv", hoadonchitieu.Manv);
            return View(hoadonchitieu);
        }

        // POST: Hoadonchitieux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Mahd,Manv,Noidung,Sotien")] Hoadonchitieu hoadonchitieu)
        {
            if (id != hoadonchitieu.Mahd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoadonchitieu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoadonchitieuExists(hoadonchitieu.Mahd))
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
            ViewData["Manv"] = new SelectList(_context.Nhanviens, "Manv", "Manv", hoadonchitieu.Manv);
            return View(hoadonchitieu);
        }

        // GET: Hoadonchitieux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoadonchitieu = await _context.Hoadonchitieus
                .Include(h => h.ManvNavigation)
                .FirstOrDefaultAsync(m => m.Mahd == id);
            if (hoadonchitieu == null)
            {
                return NotFound();
            }

            return View(hoadonchitieu);
        }

        // POST: Hoadonchitieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoadonchitieu = await _context.Hoadonchitieus.FindAsync(id);
            if (hoadonchitieu != null)
            {
                _context.Hoadonchitieus.Remove(hoadonchitieu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoadonchitieuExists(int id)
        {
            return _context.Hoadonchitieus.Any(e => e.Mahd == id);
        }
    }
}
