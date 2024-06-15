using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnCoSo.Data;

namespace DoAnCoSo.Controllers
{
    public class BangluongController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public BangluongController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Bangluong
        public async Task<IActionResult> Index()
        {
            var quanLyTrungTamAnhNguContext = _context.Bangluongs.Include(b => b.MagvNavigation).Include(b => b.ManvNavigation);
            return View(await quanLyTrungTamAnhNguContext.ToListAsync());
        }

        // GET: Bangluong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bangluong = await _context.Bangluongs
                .Include(b => b.MagvNavigation)
                .Include(b => b.ManvNavigation)
                .FirstOrDefaultAsync(m => m.Mabang == id);
            if (bangluong == null)
            {
                return NotFound();
            }

            return View(bangluong);
        }

        // GET: Bangluong/Create
        public IActionResult Create()
        {
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv");
            ViewData["Manv"] = new SelectList(_context.Nhanviens, "Manv", "Manv");
            return View();
        }

        // POST: Bangluong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mabang,Manv,Magv,Tongsotiet,Hesoluong,Tongtienluong,Ghichu")] Bangluong bangluong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bangluong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", bangluong.Magv);
            ViewData["Manv"] = new SelectList(_context.Nhanviens, "Manv", "Manv", bangluong.Manv);
            return View(bangluong);
        }

        // GET: Bangluong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bangluong = await _context.Bangluongs.FindAsync(id);
            if (bangluong == null)
            {
                return NotFound();
            }
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", bangluong.Magv);
            ViewData["Manv"] = new SelectList(_context.Nhanviens, "Manv", "Manv", bangluong.Manv);
            return View(bangluong);
        }

        // POST: Bangluong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Mabang,Manv,Magv,Tongsotiet,Hesoluong,Tongtienluong,Ghichu")] Bangluong bangluong)
        {
            if (id != bangluong.Mabang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bangluong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BangluongExists(bangluong.Mabang))
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
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", bangluong.Magv);
            ViewData["Manv"] = new SelectList(_context.Nhanviens, "Manv", "Manv", bangluong.Manv);
            return View(bangluong);
        }

        // GET: Bangluong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bangluong = await _context.Bangluongs
                .Include(b => b.MagvNavigation)
                .Include(b => b.ManvNavigation)
                .FirstOrDefaultAsync(m => m.Mabang == id);
            if (bangluong == null)
            {
                return NotFound();
            }

            return View(bangluong);
        }

        // POST: Bangluong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bangluong = await _context.Bangluongs.FindAsync(id);
            if (bangluong != null)
            {
                _context.Bangluongs.Remove(bangluong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BangluongExists(int id)
        {
            return _context.Bangluongs.Any(e => e.Mabang == id);
        }
    }
}
