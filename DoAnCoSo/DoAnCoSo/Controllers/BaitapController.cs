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
    public class BaitapController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public BaitapController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        // GET: Baitap
        public async Task<IActionResult> Index()
        {
            var quanLyTrungTamAnhNguContext = _context.Baitaps.Include(b => b.MagvNavigation).Include(b => b.MahvNavigation);
            return View(await quanLyTrungTamAnhNguContext.ToListAsync());
        }

        // GET: Baitap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baitap = await _context.Baitaps
                .Include(b => b.MagvNavigation)
                .Include(b => b.MahvNavigation)
                .FirstOrDefaultAsync(m => m.Mabt == id);
            if (baitap == null)
            {
                return NotFound();
            }

            return View(baitap);
        }

        // GET: Baitap/Create
        public IActionResult Create()
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Giangvien" && loaiuser != "Giảng viên")
            {
                ViewData["Message"] = "Bạn không có quyền thêm mới";
                return View("AccessDenied");
            }
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv");
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv");
            return View();
        }

        // POST: Baitap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mabt,Magv,Mahv,Tenbt,Tgbatdau,Tgketthuc,Ketqua,Danhgia")] Baitap baitap)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Giangvien" && loaiuser != "Giảng viên")
            {
                ViewData["Message"] = "Bạn không có quyền thêm mới";
                return View("AccessDenied");
            }
            if (ModelState.IsValid)
            {
                _context.Add(baitap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", baitap.Magv);
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv", baitap.Mahv);
            return View(baitap);
        }

        // GET: Baitap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Giangvien" && loaiuser != "Giảng viên")
            {
                ViewData["Message"] = "Bạn không có quyền chỉnh sửa";
                return View("AccessDenied");
            }
            if (id == null)
            {
                return NotFound();
            }

            var baitap = await _context.Baitaps.FindAsync(id);
            if (baitap == null)
            {
                return NotFound();
            }
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", baitap.Magv);
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv", baitap.Mahv);
            return View(baitap);
        }

        // POST: Baitap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Mabt,Magv,Mahv,Tenbt,Tgbatdau,Tgketthuc,Ketqua,Danhgia")] Baitap baitap)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Giangvien" && loaiuser != "Giảng viên")
            {
                ViewData["Message"] = "Bạn không có quyền chỉnh sửa";
                return View("AccessDenied");
            }
            if (id != baitap.Mabt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baitap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaitapExists(baitap.Mabt))
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
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", baitap.Magv);
            ViewData["Mahv"] = new SelectList(_context.Hocviens, "Mahv", "Mahv", baitap.Mahv);
            return View(baitap);
        }

        // GET: Baitap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Giangvien" && loaiuser != "Giảng viên")
            {
                ViewData["Message"] = "Bạn không có quyền xóa";
                return View("AccessDenied");
            }
            if (id == null)
            {
                return NotFound();
            }

            var baitap = await _context.Baitaps
                .Include(b => b.MagvNavigation)
                .Include(b => b.MahvNavigation)
                .FirstOrDefaultAsync(m => m.Mabt == id);
            if (baitap == null)
            {
                return NotFound();
            }

            return View(baitap);
        }

        // POST: Baitap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Giangvien" && loaiuser != "Giảng viên")
            {
                ViewData["Message"] = "Bạn không có quyền xóa";
                return View("AccessDenied");
            }
            var baitap = await _context.Baitaps.FindAsync(id);
            if (baitap != null)
            {
                _context.Baitaps.Remove(baitap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaitapExists(int id)
        {
            return _context.Baitaps.Any(e => e.Mabt == id);
        }
        [HttpPost]
        public async Task<IActionResult> UploadWordFile(int id, IFormFile file)
        {
           
            if (file == null || file.Length == 0)
            {
                return Content("file not selected");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var baikiemtra = await _context.Baikiemtras.FindAsync(id);
            if (baikiemtra == null)
            {
                return NotFound();
            }

            baikiemtra.FilePath = "/uploads/" + file.FileName;
            _context.Update(baikiemtra);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
