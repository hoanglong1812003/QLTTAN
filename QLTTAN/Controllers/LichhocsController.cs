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
    public class LichhocsController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public LichhocsController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Lichhocs
        public async Task<IActionResult> Index()
        {
            var quanLyTrungTamAnhNguContext = _context.Lichhocs.Include(l => l.MahvNavigation).Include(l => l.MalopNavigation);
            return View(await quanLyTrungTamAnhNguContext.ToListAsync());
        }

        // GET: Lichhocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichhoc = await _context.Lichhocs
                .Include(l => l.MahvNavigation)
                .Include(l => l.MalopNavigation)
                .FirstOrDefaultAsync(m => m.Malh == id);
            if (lichhoc == null)
            {
                return NotFound();
            }

            return View(lichhoc);
        }

        // GET: Lichhocs/Create
        public IActionResult Create()
        {
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv");
            ViewData["Malop"] = new SelectList(_context.Lophocs, "Malop", "Malop");
            return View();
        }

        // POST: Lichhocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Malh,Mahv,Malop,Tuan,Cahoc,Ngayhoc,Ghichu")] Lichhoc lichhoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lichhoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv", lichhoc.Mahv);
            ViewData["Malop"] = new SelectList(_context.Lophocs, "Malop", "Malop", lichhoc.Malop);
            return View(lichhoc);
        }

        // GET: Lichhocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichhoc = await _context.Lichhocs.FindAsync(id);
            if (lichhoc == null)
            {
                return NotFound();
            }
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv", lichhoc.Mahv);
            ViewData["Malop"] = new SelectList(_context.Lophocs, "Malop", "Malop", lichhoc.Malop);
            return View(lichhoc);
        }

        // POST: Lichhocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Malh,Mahv,Malop,Tuan,Cahoc,Ngayhoc,Ghichu")] Lichhoc lichhoc)
        {
            if (id != lichhoc.Malh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lichhoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LichhocExists(lichhoc.Malh))
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
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv", lichhoc.Mahv);
            ViewData["Malop"] = new SelectList(_context.Lophocs, "Malop", "Malop", lichhoc.Malop);
            return View(lichhoc);
        }

        // GET: Lichhocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichhoc = await _context.Lichhocs
                .Include(l => l.MahvNavigation)
                .Include(l => l.MalopNavigation)
                .FirstOrDefaultAsync(m => m.Malh == id);
            if (lichhoc == null)
            {
                return NotFound();
            }

            return View(lichhoc);
        }

        // POST: Lichhocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lichhoc = await _context.Lichhocs.FindAsync(id);
            if (lichhoc != null)
            {
                _context.Lichhocs.Remove(lichhoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LichhocExists(int id)
        {
            return _context.Lichhocs.Any(e => e.Malh == id);
        }
    }
}
