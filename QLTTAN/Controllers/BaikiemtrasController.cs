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
    public class BaikiemtrasController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public BaikiemtrasController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Baikiemtras
        public async Task<IActionResult> Index()
        {
            var quanLyTrungTamAnhNguContext = _context.Baikiemtras.Include(b => b.MagvNavigation).Include(b => b.MahvNavigation);
            return View(await quanLyTrungTamAnhNguContext.ToListAsync());
        }

        // GET: Baikiemtras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baikiemtra = await _context.Baikiemtras
                .Include(b => b.MagvNavigation)
                .Include(b => b.MahvNavigation)
                .FirstOrDefaultAsync(m => m.Mabaikt == id);
            if (baikiemtra == null)
            {
                return NotFound();
            }

            return View(baikiemtra);
        }

        // GET: Baikiemtras/Create
        public IActionResult Create()
        {
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv");
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv");
            return View();
        }

        // POST: Baikiemtras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mabaikt,Magv,Mahv,Tenbaikt,Ngaykt,Tgbatdau,Tgketthuc,Ketqua,Danhgia")] Baikiemtra baikiemtra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baikiemtra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", baikiemtra.Magv);
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv", baikiemtra.Mahv);
            return View(baikiemtra);
        }

        // GET: Baikiemtras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baikiemtra = await _context.Baikiemtras.FindAsync(id);
            if (baikiemtra == null)
            {
                return NotFound();
            }
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", baikiemtra.Magv);
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv", baikiemtra.Mahv);
            return View(baikiemtra);
        }

        // POST: Baikiemtras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Mabaikt,Magv,Mahv,Tenbaikt,Ngaykt,Tgbatdau,Tgketthuc,Ketqua,Danhgia")] Baikiemtra baikiemtra)
        {
            if (id != baikiemtra.Mabaikt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baikiemtra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaikiemtraExists(baikiemtra.Mabaikt))
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
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", baikiemtra.Magv);
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv", baikiemtra.Mahv);
            return View(baikiemtra);
        }

        // GET: Baikiemtras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baikiemtra = await _context.Baikiemtras
                .Include(b => b.MagvNavigation)
                .Include(b => b.MahvNavigation)
                .FirstOrDefaultAsync(m => m.Mabaikt == id);
            if (baikiemtra == null)
            {
                return NotFound();
            }

            return View(baikiemtra);
        }

        // POST: Baikiemtras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baikiemtra = await _context.Baikiemtras.FindAsync(id);
            if (baikiemtra != null)
            {
                _context.Baikiemtras.Remove(baikiemtra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaikiemtraExists(int id)
        {
            return _context.Baikiemtras.Any(e => e.Mabaikt == id);
        }
    }
}
