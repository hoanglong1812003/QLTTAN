using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoAnCoSo.Data;
using Microsoft.AspNetCore.Http;

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
    }

    public class PhieuDangKiKhoaHocViewModel
    {
        public int Makh { get; set; }
        public string Tenkh { get; set; }
        public string Noidung { get; set; }
        public decimal? Dongia { get; set; }
    }
}
