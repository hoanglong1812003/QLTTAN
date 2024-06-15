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
    public class GiangvienController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public GiangvienController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Giangvien
        public async Task<IActionResult> Index()
        {
            var quanLyTrungTamAnhNguContext = _context.Giangviens.Include(g => g.UsernameNavigation);
            return View(await quanLyTrungTamAnhNguContext.ToListAsync());
        }

        // GET: Giangvien/Details/5
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

        // GET: Giangvien/Create
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

        // POST: Giangvien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Magv,Username,Tengv,Ngaysinh,Diachi,Gioitinh,Sdt,Socccd,Email,Trinhdo")] Giangvien giangvien)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền thêm mới";
                return View("AccessDenied");
            }

            if (ModelState.IsValid)
            {
                _context.Add(giangvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Username"] = new SelectList(_context.Taikhoans, "Username", "Username", giangvien.Username);
            return View(giangvien);
        }

        // GET: Giangvien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền chỉnh sửa";
                return View("AccessDenied");
            }

            ViewData["Username"] = new SelectList(_context.Taikhoans, "Username", "Username");
            return View();
        }

        // POST: Giangvien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Magv,Username,Tengv,Ngaysinh,Diachi,Gioitinh,Sdt,Socccd,Email,Trinhdo")] Giangvien giangvien)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền chỉnh sửa";
                return View("AccessDenied");
            }

            if (ModelState.IsValid)
            {
                _context.Add(giangvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Username"] = new SelectList(_context.Taikhoans, "Username", "Username", giangvien.Username);
            return View(giangvien);
        }

        // GET: Giangvien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền xóa";
                return View("AccessDenied");
            }

            ViewData["Username"] = new SelectList(_context.Taikhoans, "Username", "Username");
            return View();
        }

        // POST: Giangvien/Delete/5
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
