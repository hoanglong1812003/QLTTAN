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
    public class HoadonchitieuController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public HoadonchitieuController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Hoadonchitieu
        public async Task<IActionResult> Index()
        {
            var quanLyTrungTamAnhNguContext = _context.Hoadonchitieus.Include(h => h.ManvNavigation);
            return View(await quanLyTrungTamAnhNguContext.ToListAsync());
        }

        // GET: Hoadonchitieu/Details/5
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

        // GET: Hoadonchitieu/Create
        public IActionResult Create()
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Nhân viên nhập liệu")
            {
                ViewData["Message"] = "Bạn không có quyền thêm mới";
                return View("AccessDenied");
            }
            ViewData["Manv"] = new SelectList(_context.Nhanviens, "Manv", "Manv");
            return View();
        }

        // POST: Hoadonchitieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mahd,Manv,Noidung,Sotien")] Hoadonchitieu hoadonchitieu)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Nhân viên nhập liệu")
            {
                ViewData["Message"] = "Bạn không có quyền thêm mới";
                return View("AccessDenied");
            }
            if (ModelState.IsValid)
            {
                _context.Add(hoadonchitieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Manv"] = new SelectList(_context.Nhanviens, "Manv", "Manv", hoadonchitieu.Manv);
            return View(hoadonchitieu);
        }

        // GET: Hoadonchitieu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Nhân viên nhập liệu")
            {
                ViewData["Message"] = "Bạn không có quyền chỉnh sửa";
                return View("AccessDenied");
            }
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

        // POST: Hoadonchitieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Mahd,Manv,Noidung,Sotien")] Hoadonchitieu hoadonchitieu)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Nhân viên nhập liệu")
            {
                ViewData["Message"] = "Bạn không có quyền chỉnh sửa";
                return View("AccessDenied");
            }
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

        // GET: Hoadonchitieu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Nhân viên nhập liệu")
            {
                ViewData["Message"] = "Bạn không có quyền xóa";
                return View("AccessDenied");
            }
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

        // POST: Hoadonchitieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Nhân viên nhập liệu")
            {
                ViewData["Message"] = "Bạn không có quyền xóa";
                return View("AccessDenied");
            }
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
