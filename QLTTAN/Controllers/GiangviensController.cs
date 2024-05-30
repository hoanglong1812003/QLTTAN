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
    public class GiangviensController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public GiangviensController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Giangviens
        public async Task<IActionResult> Index()
        {
            var quanLyTrungTamAnhNguContext = _context.Giangviens.Include(g => g.UsernameNavigation);
            return View(await quanLyTrungTamAnhNguContext.ToListAsync());
        }

        // GET: Giangviens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giangvien = await _context.Giangviens
                .Include(g => g.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Magv == id);
            if (giangvien == null)
            {
                return NotFound();
            }

            return View(giangvien);
        }

        // GET: Giangviens/Create
        public IActionResult Create()
        {
            ViewData["Username"] = new SelectList(_context.Taikhoans, "Username", "Username");
            return View();
        }

        // POST: Giangviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Magv,Username,Tengv,Ngaysinh,Diachi,Gioitinh,Sdt,Socccd,Email,Trinhdo")] Giangvien giangvien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giangvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Username"] = new SelectList(_context.Taikhoans, "Username", "Username", giangvien.Username);
            return View(giangvien);
        }

        // GET: Giangviens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giangvien = await _context.Giangviens.FindAsync(id);
            if (giangvien == null)
            {
                return NotFound();
            }
            ViewData["Username"] = new SelectList(_context.Taikhoans, "Username", "Username", giangvien.Username);
            return View(giangvien);
        }

        // POST: Giangviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Magv,Username,Tengv,Ngaysinh,Diachi,Gioitinh,Sdt,Socccd,Email,Trinhdo")] Giangvien giangvien)
        {
            if (id != giangvien.Magv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giangvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiangvienExists(giangvien.Magv))
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
            ViewData["Username"] = new SelectList(_context.Taikhoans, "Username", "Username", giangvien.Username);
            return View(giangvien);
        }

        // GET: Giangviens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giangvien = await _context.Giangviens
                .Include(g => g.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Magv == id);
            if (giangvien == null)
            {
                return NotFound();
            }

            return View(giangvien);
        }

        // POST: Giangviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giangvien = await _context.Giangviens.FindAsync(id);
            if (giangvien != null)
            {
                _context.Giangviens.Remove(giangvien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiangvienExists(int id)
        {
            return _context.Giangviens.Any(e => e.Magv == id);
        }
    }
}
