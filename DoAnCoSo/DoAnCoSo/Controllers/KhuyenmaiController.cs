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
    public class KhuyenmaiController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public KhuyenmaiController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Khuyenmai
        public async Task<IActionResult> Index()
        {
            return View(await _context.Khuyenmais.ToListAsync());
        }

        // GET: Khuyenmai/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khuyenmai = await _context.Khuyenmais
                .FirstOrDefaultAsync(m => m.Makm == id);
            if (khuyenmai == null)
            {
                return NotFound();
            }

            return View(khuyenmai);
        }

        // GET: Khuyenmai/Create
        public IActionResult Create()
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền thêm mới";
                return View("AccessDenied");
            }
            return View();
        }

        // POST: Khuyenmai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Makm,Noidung")] Khuyenmai khuyenmai)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền thêm mới";
                return View("AccessDenied");
            }
            if (ModelState.IsValid)
            {
                _context.Add(khuyenmai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khuyenmai);
        }

        // GET: Khuyenmai/Edit/5
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

            var khuyenmai = await _context.Khuyenmais.FindAsync(id);
            if (khuyenmai == null)
            {
                return NotFound();
            }
            return View(khuyenmai);
        }

        // POST: Khuyenmai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Makm,Noidung")] Khuyenmai khuyenmai)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền chỉnh sửa";
                return View("AccessDenied");
            }
            if (id != khuyenmai.Makm)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khuyenmai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhuyenmaiExists(khuyenmai.Makm))
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
            return View(khuyenmai);
        }

        // GET: Khuyenmai/Delete/5
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

            var khuyenmai = await _context.Khuyenmais
                .FirstOrDefaultAsync(m => m.Makm == id);
            if (khuyenmai == null)
            {
                return NotFound();
            }

            return View(khuyenmai);
        }

        // POST: Khuyenmai/Delete/5
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
            var khuyenmai = await _context.Khuyenmais.FindAsync(id);
            if (khuyenmai != null)
            {
                _context.Khuyenmais.Remove(khuyenmai);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhuyenmaiExists(int id)
        {
            return _context.Khuyenmais.Any(e => e.Makm == id);
        }
    }
}
