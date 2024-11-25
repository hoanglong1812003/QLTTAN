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
    public class ThietbiController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public ThietbiController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Thietbi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Thietbis.ToListAsync());
        }

        // GET: Thietbi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thietbi = await _context.Thietbis
                .FirstOrDefaultAsync(m => m.Matb == id);
            if (thietbi == null)
            {
                return NotFound();
            }

            return View(thietbi);
        }

        // GET: Thietbi/Create
        public IActionResult Create()
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Nhân viên kỹ thuật")
            {
                ViewData["Message"] = "Bạn không có quyền thêm mới";
                return View("AccessDenied");
            }
            return View();
        }

        // POST: Thietbi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matb,Tentb,Giatien,Dvt")] Thietbi thietbi)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Nhân viên kỹ thuật")
            {
                ViewData["Message"] = "Bạn không có quyền thêm mới";
                return View("AccessDenied");
            }
            if (ModelState.IsValid)
            {
                _context.Add(thietbi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thietbi);
        }

        // GET: Thietbi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Nhân viên kỹ thuật")
            {
                ViewData["Message"] = "Bạn không có quyền chỉnh sửa";
                return View("AccessDenied");
            }
            if (id == null)
            {
                return NotFound();
            }

            var thietbi = await _context.Thietbis.FindAsync(id);
            if (thietbi == null)
            {
                return NotFound();
            }
            return View(thietbi);
        }

        // POST: Thietbi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Matb,Tentb,Giatien,Dvt")] Thietbi thietbi)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Nhân viên kỹ thuật")
            {
                ViewData["Message"] = "Bạn không có quyền chỉnh sửa";
                return View("AccessDenied");
            }
            if (id != thietbi.Matb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thietbi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThietbiExists(thietbi.Matb))
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
            return View(thietbi);
        }

        // GET: Thietbi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Nhân viên kỹ thuật")
            {
                ViewData["Message"] = "Bạn không có quyền xóa";
                return View("AccessDenied");
            }
            if (id == null)
            {
                return NotFound();
            }

            var thietbi = await _context.Thietbis
                .FirstOrDefaultAsync(m => m.Matb == id);
            if (thietbi == null)
            {
                return NotFound();
            }

            return View(thietbi);
        }

        // POST: Thietbi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Nhân viên kỹ thuật")
            {
                ViewData["Message"] = "Bạn không có quyền xóa";
                return View("AccessDenied");
            }
            var thietbi = await _context.Thietbis.FindAsync(id);
            if (thietbi != null)
            {
                _context.Thietbis.Remove(thietbi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThietbiExists(int id)
        {
            return _context.Thietbis.Any(e => e.Matb == id);
        }
    }
}
