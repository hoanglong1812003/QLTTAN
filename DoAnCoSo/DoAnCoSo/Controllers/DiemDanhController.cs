using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoAnCoSo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Controllers
{
    public class DiemDanhController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public DiemDanhController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");

            if (loaiuser == "Hocvien")
            {
                int? mahv = HttpContext.Session.GetInt32("Mahv");
                if (mahv == null)
                {
                    return Unauthorized();
                }

                var diemDanhs = await _context.Phieudiemdanhs
                                        .Where(p => p.Mahv == mahv)
                                        .Include(p => p.MagvNavigation)
                                        .Include(p => p.MalhNavigation)
                                        .Include(p => p.MahvNavigation)
                                        .ToListAsync();
                return View(diemDanhs);
            }
            else if (loaiuser == "Giangvien" || loaiuser == "Admin")
            {
                var diemDanhs = await _context.Phieudiemdanhs
                                        .Include(p => p.MagvNavigation)
                                        .Include(p => p.MalhNavigation)
                                        .Include(p => p.MahvNavigation)
                                        .ToListAsync();
                return View(diemDanhs);
            }
            else
            {
                return Unauthorized();
            }
        }

        public IActionResult Create()
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Giangvien" && loaiuser != "Admin")
            {
                return Unauthorized();
            }

            ViewBag.Giangviens = _context.Giangviens.ToList();
            ViewBag.Lichhocs = _context.Lichhocs.ToList();
            ViewBag.Hocviens = _context.Hocviens.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int magv, int malh, int mahv, string trangthai, string ghichu)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Giangvien" && loaiuser != "Admin")
            {
                return Unauthorized();
            }

            var phieuDiemDanh = new Phieudiemdanh
            {
                Magv = magv,
                Malh = malh,
                Mahv = mahv,
                Trangthai = trangthai,
                Ghichu = ghichu
            };

            _context.Phieudiemdanhs.Add(phieuDiemDanh);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Giangvien" && loaiuser != "Admin")
            {
                return Unauthorized();
            }

            var phieuDiemDanh = await _context.Phieudiemdanhs.FindAsync(id);
            if (phieuDiemDanh != null)
            {
                _context.Phieudiemdanhs.Remove(phieuDiemDanh);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
