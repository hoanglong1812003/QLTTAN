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
    public class LophocController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public LophocController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Lophoc
        public async Task<IActionResult> Index()
        {
            var quanLyTrungTamAnhNguContext = _context.Lophocs.Include(l => l.MakhNavigation);
            return View(await quanLyTrungTamAnhNguContext.ToListAsync());
        }

        // GET: Lophoc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lophoc = await _context.Lophocs
                .Include(l => l.MakhNavigation)
                .FirstOrDefaultAsync(m => m.Malop == id);
            if (lophoc == null)
            {
                return NotFound();
            }

            return View(lophoc);
        }

        // GET: Lophoc/Create
        public IActionResult Create()
        {
            ViewData["Makh"] = new SelectList(_context.Khoahocs, "Makh", "Makh");
            return View();
        }

        // POST: Lophoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Malop,Makh,Tenlop,Tgtao,Ngaynhaphoc,Ngayketthuc")] Lophoc lophoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lophoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Makh"] = new SelectList(_context.Khoahocs, "Makh", "Makh", lophoc.Makh);
            return View(lophoc);
        }

        // GET: Lophoc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lophoc = await _context.Lophocs.FindAsync(id);
            if (lophoc == null)
            {
                return NotFound();
            }
            ViewData["Makh"] = new SelectList(_context.Khoahocs, "Makh", "Makh", lophoc.Makh);
            return View(lophoc);
        }

        // POST: Lophoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Malop,Makh,Tenlop,Tgtao,Ngaynhaphoc,Ngayketthuc")] Lophoc lophoc)
        {
            if (id != lophoc.Malop)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lophoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LophocExists(lophoc.Malop))
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
            ViewData["Makh"] = new SelectList(_context.Khoahocs, "Makh", "Makh", lophoc.Makh);
            return View(lophoc);
        }

        // GET: Lophoc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lophoc = await _context.Lophocs
                .Include(l => l.MakhNavigation)
                .FirstOrDefaultAsync(m => m.Malop == id);
            if (lophoc == null)
            {
                return NotFound();
            }

            return View(lophoc);
        }

        // POST: Lophoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lophoc = await _context.Lophocs.FindAsync(id);
            if (lophoc != null)
            {
                _context.Lophocs.Remove(lophoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LophocExists(int id)
        {
            return _context.Lophocs.Any(e => e.Malop == id);
        }
    }
}
