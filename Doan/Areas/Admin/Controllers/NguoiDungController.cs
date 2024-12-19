using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Doan.Models;
using Doan.Models;
using BC = BCrypt.Net.BCrypt;

namespace Doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NguoiDungController : Controller
    {
        private readonly DoanDbContext _context;

        public NguoiDungController(DoanDbContext context)
        {
            _context = context;
        }

        // GET: NguoiDung
        public async Task<IActionResult> Index()
        {
            return View(await _context.NguoiDung.ToListAsync());
        }

        // GET: NguoiDung/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NguoiDung/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HoVaTen,Email,DienThoai,DiaChi,TenDangNhap,MatKhau,XacNhanMatKhau, Quyen")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                nguoiDung.MatKhau = BC.HashPassword(nguoiDung.MatKhau);
                nguoiDung.XacNhanMatKhau = nguoiDung.MatKhau;
                _context.Add(nguoiDung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nguoiDung);
        }

        // GET: NguoiDung/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDung.FindAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return View(new NguoiDung_ChinhSua(nguoiDung));
        }

        // POST: NguoiDung/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,HoVaTen,Email,DienThoai,DiaChi,TenDangNhap,MatKhau,XacNhanMatKhau,Quyen")] NguoiDung_ChinhSua nguoiDung)
        {
            if (id != nguoiDung.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var n = await _context.NguoiDung.FindAsync(id);

                    // Giữ nguyên mật khẩu cũ
                    if (nguoiDung.MatKhau == null)
                    {
                        n.ID = nguoiDung.ID;
                        n.HoVaTen = nguoiDung.HoVaTen;
                        n.DienThoai = nguoiDung.DienThoai;
                        n.DiaChi = nguoiDung.DiaChi;
                        n.TenDangNhap = nguoiDung.TenDangNhap;
                        n.XacNhanMatKhau = n.MatKhau;
                        n.Quyen = nguoiDung.Quyen;
                    }
                    else // Cập nhật mật khẩu mới
                    {
                        n.ID = nguoiDung.ID;
                        n.HoVaTen = nguoiDung.HoVaTen;
                        n.DienThoai = nguoiDung.DienThoai;
                        n.DiaChi = nguoiDung.DiaChi;
                        n.TenDangNhap = nguoiDung.TenDangNhap;
                        n.MatKhau = BC.HashPassword(nguoiDung.MatKhau);
                        n.XacNhanMatKhau = n.MatKhau;
                        n.Quyen = nguoiDung.Quyen;
                    }
                    _context.Update(n);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguoiDungExists(nguoiDung.ID))
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
            return View(nguoiDung);
        }

        // GET: NguoiDung/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDung
            .FirstOrDefaultAsync(m => m.ID == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // POST: NguoiDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nguoiDung = await _context.NguoiDung.FindAsync(id);
            if (nguoiDung != null)
            {
                _context.NguoiDung.Remove(nguoiDung);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NguoiDungExists(int id)
        {
            return _context.NguoiDung.Any(e => e.ID == id);
        }
    }
}