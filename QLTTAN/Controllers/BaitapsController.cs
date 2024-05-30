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
    public class BaitapsController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public BaitapsController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Baitaps
        public async Task<IActionResult> Index()
        {
            var quanLyTrungTamAnhNguContext = _context.Baitaps.Include(b => b.MagvNavigation).Include(b => b.MahvNavigation);
            return View(await quanLyTrungTamAnhNguContext.ToListAsync());
        }

        // GET: Baitaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baitap = await _context.Baitaps
                .Include(b => b.MagvNavigation)
                .Include(b => b.MahvNavigation)
                .FirstOrDefaultAsync(m => m.Mabt == id);
            if (baitap == null)
            {
                return NotFound();
            }

            return View(baitap);
        }

        // GET: Baitaps/Create
        public IActionResult Create()
        {
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv");
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv");
            return View();
        }

        // POST: Baitaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mabt,Magv,Mahv,Tenbt,Tgbatdau,Tgketthuc,Ketqua,Danhgia")] Baitap baitap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baitap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", baitap.Magv);
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv", baitap.Mahv);
            return View(baitap);
        }

        // GET: Baitaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baitap = await _context.Baitaps.FindAsync(id);
            if (baitap == null)
            {
                return NotFound();
            }
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", baitap.Magv);
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv", baitap.Mahv);
            return View(baitap);
        }

        // POST: Baitaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Mabt,Magv,Mahv,Tenbt,Tgbatdau,Tgketthuc,Ketqua,Danhgia")] Baitap baitap)
        {
            if (id != baitap.Mabt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baitap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaitapExists(baitap.Mabt))
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
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", baitap.Magv);
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv", baitap.Mahv);
            return View(baitap);
        }

        // GET: Baitaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baitap = await _context.Baitaps
                .Include(b => b.MagvNavigation)
                .Include(b => b.MahvNavigation)
                .FirstOrDefaultAsync(m => m.Mabt == id);
            if (baitap == null)
            {
                return NotFound();
            }

            return View(baitap);
        }

        // POST: Baitaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baitap = await _context.Baitaps.FindAsync(id);
            if (baitap != null)
            {
                _context.Baitaps.Remove(baitap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaitapExists(int id)
        {
            return _context.Baitaps.Any(e => e.Mabt == id);
        }
    }
}
