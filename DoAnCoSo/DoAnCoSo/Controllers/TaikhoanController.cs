using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoAnCoSo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace DoAnCoSo.Controllers
{
    public class TaikhoanController : Controller
    {
        private readonly QuanLyTrungTamAnhNguContext _context;

        public TaikhoanController(QuanLyTrungTamAnhNguContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var loaiuser = HttpContext.Session.GetString("Loaiuser");
            if (loaiuser != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền tạo tài khoản";
                return View("AccessDenied");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string loaiuser)
        {
            var currentUserRole = HttpContext.Session.GetString("Loaiuser");
            if (currentUserRole != "Admin")
            {
                ViewData["Message"] = "Bạn không có quyền tạo tài khoản";
                return View("AccessDenied");
            }

            if (ModelState.IsValid)
            {
                var newUser = new Taikhoan
                {
                    Username = username,
                    Password = password,
                    Loaiuser = loaiuser
                };

                _context.Taikhoans.Add(newUser);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _context.Taikhoans.SingleOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Lấy thông tin học viên
                var hocvien = _context.Hocviens.SingleOrDefault(h => h.Username == username);

                // Set user in session
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Loaiuser", user.Loaiuser);

                if (hocvien != null)
                {
                    HttpContext.Session.SetInt32("Mahv", hocvien.Mahv);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }


        [AllowAnonymous]
        public IActionResult Logout()
        {
            // Clear session or cookie
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var accounts = _context.Taikhoans.ToList();
            return View(accounts);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Username,Password,Loaiuser")] Taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taikhoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taikhoan);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikhoan = _context.Taikhoans.Find(id);
            if (taikhoan == null)
            {
                return NotFound();
            }
            return View(taikhoan);
        }
    }
}
