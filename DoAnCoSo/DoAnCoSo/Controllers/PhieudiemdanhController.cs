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
    public class PhieudiemdanhController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public PhieudiemdanhController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Phieudiemdanh
        public async Task<IActionResult> Index()
        {
            var phieudiemdanhContext = _context.Phieudiemdanhs.Include(p => p.MagvNavigation).Include(p => p.MahvNavigation).Include(p => p.MalhNavigation);
            return View(await phieudiemdanhContext.ToListAsync());
        }

        // GET: Phieudiemdanh/Create
        public IActionResult Create()
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền thêm mới";
                return View("AccessDenied");
            }
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "TenGv");
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "TenHv");
            ViewData["Malh"] = new SelectList(_context.Lichhocs, "Malh", "TenLh");
            return View();
        }

        // POST: Phieudiemdanh/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Madd,Magv,Malh,Trangthai,Ghichu,Mahv")] Phieudiemdanh phieudiemdanh)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền thêm mới";
                return View("AccessDenied");
            }
            if (ModelState.IsValid)
            {
                phieudiemdanh.MagvNavigation = await _context.Giangviens.FindAsync(phieudiemdanh.Magv);
                phieudiemdanh.MahvNavigation = await _context.Hocviens.FindAsync(phieudiemdanh.Mahv);
                phieudiemdanh.MalhNavigation = await _context.Lichhocs.FindAsync(phieudiemdanh.Malh);

                _context.Add(phieudiemdanh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "TenGv", phieudiemdanh.Magv);
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "TenHv", phieudiemdanh.Mahv);
            ViewData["Malh"] = new SelectList(_context.Lichhocs, "Malh", "TenLh", phieudiemdanh.Malh);
            return View(phieudiemdanh);
        }

        // GET: Phieudiemdanh/Edit/5
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

            var phieudiemdanh = await _context.Phieudiemdanhs.FindAsync(id);
            if (phieudiemdanh == null)
            {
                return NotFound();
            }
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "TenGv", phieudiemdanh.Magv);
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "TenHv", phieudiemdanh.Mahv);
            ViewData["Malh"] = new SelectList(_context.Lichhocs, "Malh", "TenLh", phieudiemdanh.Malh);
            return View(phieudiemdanh);
        }

        // POST: Phieudiemdanh/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Madd,Magv,Malh,Trangthai,Ghichu,Mahv")] Phieudiemdanh phieudiemdanh)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền chỉnh sửa";
                return View("AccessDenied");
            }
            if (id != phieudiemdanh.Madd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    phieudiemdanh.MagvNavigation = await _context.Giangviens.FindAsync(phieudiemdanh.Magv);
                    phieudiemdanh.MahvNavigation = await _context.Hocviens.FindAsync(phieudiemdanh.Mahv);
                    phieudiemdanh.MalhNavigation = await _context.Lichhocs.FindAsync(phieudiemdanh.Malh);

                    _context.Update(phieudiemdanh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieudiemdanhExists(phieudiemdanh.Madd))
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
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "TenGv", phieudiemdanh.Magv);
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "TenHv", phieudiemdanh.Mahv);
            ViewData["Malh"] = new SelectList(_context.Lichhocs, "Malh", "TenLh", phieudiemdanh.Malh);
            return View(phieudiemdanh);
        }

        // GET: Phieudiemdanh/Delete/5
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

            var phieudiemdanh = await _context.Phieudiemdanhs
                .Include(p => p.MagvNavigation)
                .Include(p => p.MahvNavigation)
                .Include(p => p.MalhNavigation)
                .FirstOrDefaultAsync(m => m.Madd == id);
            if (phieudiemdanh == null)
            {
                return NotFound();
            }

            return View(phieudiemdanh);
        }

        // POST: Phieudiemdanh/Delete/5
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
            var phieudiemdanh = await _context.Phieudiemdanhs.FindAsync(id);
            _context.Phieudiemdanhs.Remove(phieudiemdanh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieudiemdanhExists(int id)
        {
            return _context.Phieudiemdanhs.Any(e => e.Madd == id);
        }
    }
}
