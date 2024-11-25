using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoAnCoSo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Controllers
{
    public class PhieuDangKiKhoaHocController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public PhieuDangKiKhoaHocController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register(int id)
        {
            var khoahoc = _context.Khoahocs.FirstOrDefault(k => k.Makh == id);
            if (khoahoc == null)
            {
                return NotFound();
            }

            var viewModel = new PhieuDangKiKhoaHocViewModel
            {
                Makh = khoahoc.Makh,
                Tenkh = khoahoc.Tenkh,
                Noidung = khoahoc.Noidung,
                Dongia = khoahoc.Dongia
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(PhieuDangKiKhoaHocViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mahv = HttpContext.Session.GetInt32("Mahv");
                if (mahv == null)
                {
                    return Unauthorized();
                }

                var phieuDangKi = new Phieudangkikhoahoc
                {
                    Mahv = mahv.Value,
                    Makh = model.Makh,
                    Ngaydki = DateTime.Now
                };

                _context.Phieudangkikhoahocs.Add(phieuDangKi);
                await _context.SaveChangesAsync();

                // Lưu thông tin vào lớp học
                var lophoc = new Lophoc
                {
                    Makh = model.Makh,
                    Tenlop = model.Tenkh,
                    Tgtao = DateTime.Now,
                    Ngaynhaphoc = DateTime.Now,
                    Ngayketthuc = DateTime.Now.AddMonths(3) // Giả sử khóa học kéo dài 3 tháng
                };

                _context.Lophocs.Add(lophoc);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Khoahoc");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Index()
        {
            // Lấy danh sách các phiếu đăng kí khóa học cho học viên đã đăng nhập
            var mahv = HttpContext.Session.GetInt32("Mahv");
            if (mahv == null)
            {
                return Unauthorized();
            }

            var phieus = _context.Phieudangkikhoahocs
                .Include(p => p.MakhNavigation)
                .Where(p => p.Mahv == mahv)
                .ToList();

            return View(phieus);
        }
        // Action chi tiết phiếu đăng kí
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuDangKi = await _context.Phieudangkikhoahocs
                .Include(p => p.MakhNavigation) // Load navigation property MakhNavigation
                .FirstOrDefaultAsync(p => p.Madki == id);

            if (phieuDangKi == null)
            {
                return NotFound();
            }

            return View(phieuDangKi);
        }

        // Action xóa phiếu đăng kí
        public async Task<IActionResult> Delete(int? id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin" && loaiuser != "Giangvien" && loaiuser != "Giảng viên" && loaiuser != "Nhân viên nhập liệu")
            {
                ViewData["Message"] = "Bạn không có quyền xóa";
                return View("AccessDenied");
            }
            if (id == null)
            {
                return NotFound();
            }

            var phieuDangKi = await _context.Phieudangkikhoahocs.FindAsync(id);

            if (phieuDangKi == null)
            {
                return NotFound();
            }

            _context.Phieudangkikhoahocs.Remove(phieuDangKi);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }

    public class PhieuDangKiKhoaHocViewModel
    {
        public int Makh { get; set; }
        public string Tenkh { get; set; }
        public string Noidung { get; set; }
        public decimal? Dongia { get; set; }
    }
}
