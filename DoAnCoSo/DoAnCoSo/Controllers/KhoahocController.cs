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
    public class KhoahocController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public KhoahocController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Khoahoc
        public async Task<IActionResult> Index()
        {
            return View(await _context.Khoahocs.ToListAsync());
        }

        // GET: Khoahoc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoahoc = await _context.Khoahocs
                .FirstOrDefaultAsync(m => m.Makh == id);
            if (khoahoc == null)
            {
                return NotFound();
            }

            return View(khoahoc);
        }

        // GET: Khoahoc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Khoahoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Makh,Tenkh,Noidung,Dongia")] Khoahoc khoahoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khoahoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khoahoc);
        }

        // GET: Khoahoc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoahoc = await _context.Khoahocs.FindAsync(id);
            if (khoahoc == null)
            {
                return NotFound();
            }
            return View(khoahoc);
        }

        // POST: Khoahoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Makh,Tenkh,Noidung,Dongia")] Khoahoc khoahoc)
        {
            if (id != khoahoc.Makh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khoahoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhoahocExists(khoahoc.Makh))
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
            return View(khoahoc);
        }

        // GET: Khoahoc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoahoc = await _context.Khoahocs
                .FirstOrDefaultAsync(m => m.Makh == id);
            if (khoahoc == null)
            {
                return NotFound();
            }

            return View(khoahoc);
        }

        // POST: Khoahoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khoahoc = await _context.Khoahocs.FindAsync(id);
            if (khoahoc != null)
            {
                _context.Khoahocs.Remove(khoahoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhoahocExists(int id)
        {
            return _context.Khoahocs.Any(e => e.Makh == id);
        }
    }
}
