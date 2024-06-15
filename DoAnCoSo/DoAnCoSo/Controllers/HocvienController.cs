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
    public class HocvienController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public HocvienController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Hocvien
        public async Task<IActionResult> Index()
        {
            var quanLyTrungTamAnhNguContext = _context.Hocviens.Include(h => h.UsernameNavigation);
            return View(await quanLyTrungTamAnhNguContext.ToListAsync());
        }

        // GET: Hocvien/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocvien = await _context.Hocviens
                .Include(h => h.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Mahv == id);
            if (hocvien == null)
            {
                return NotFound();
            }

            return View(hocvien);
        }

        // GET: Hocvien/Create
        public IActionResult Create()
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền thêm mới";
                return View("AccessDenied");
            }

            ViewData["Username"] = new SelectList(_context.Taikhoans, "Username", "Username");
            return View();
        }

        // POST: Hocvien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mahv,Username,Tenhv,Ngaysinh,Gioitinh,Diachi,Socccd,Sdt,Email")] Hocvien hocvien)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền thêm mới";
                return View("AccessDenied");
            }

            if (ModelState.IsValid)
            {
                _context.Add(hocvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Username"] = new SelectList(_context.Taikhoans, "Username", "Username", hocvien.Username);
            return View(hocvien);
        }

        // GET: Hocvien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền chỉnh sửa";
                return View("AccessDenied");
            }

            if (id == null)
            {
                return NotFound();
            }

            var hocvien = await _context.Hocviens.FindAsync(id);
            if (hocvien == null)
            {
                return NotFound();
            }
            ViewData["Username"] = new SelectList(_context.Taikhoans, "Username", "Username", hocvien.Username);
            return View(hocvien);
        }

        // POST: Hocvien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Mahv,Username,Tenhv,Ngaysinh,Gioitinh,Diachi,Socccd,Sdt,Email")] Hocvien hocvien)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền chỉnh sửa";
                return View("AccessDenied");
            }

            if (id != hocvien.Mahv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hocvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HocvienExists(hocvien.Mahv))
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
            ViewData["Username"] = new SelectList(_context.Taikhoans, "Username", "Username", hocvien.Username);
            return View(hocvien);
        }

        // GET: Hocvien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền xóa";
                return View("AccessDenied");
            }

            if (id == null)
            {
                return NotFound();
            }

            var hocvien = await _context.Hocviens
                .Include(h => h.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Mahv == id);
            if (hocvien == null)
            {
                return NotFound();
            }

            return View(hocvien);
        }

        // POST: Hocvien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền xóa";
                return View("AccessDenied");
            }

            var hocvien = await _context.Hocviens.FindAsync(id);
            if (hocvien != null)
            {
                _context.Hocviens.Remove(hocvien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HocvienExists(int id)
        {
            return _context.Hocviens.Any(e => e.Mahv == id);
        }
    }
}
