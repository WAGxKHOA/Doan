using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doan.Models;
using SlugGenerator;


namespace Doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HangSanXuatController : Controller
    {
        private readonly DoanDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HangSanXuatController(DoanDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: HangSanXuat
        public async Task<IActionResult> Index()
        {
            return View(await _context.HangSanXuat.ToListAsync());
        }

        // GET: HangSanXuat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangSanXuat = await _context.HangSanXuat
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hangSanXuat == null)
            {
                return NotFound();
            }

            return View(hangSanXuat);
        }

        // GET: HangSanXuat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HangSanXuat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TenHangSanXuat,TenHangSanXuatKhongDau,DuLieuHinhAnh")] HangSanXuat hangSanXuat)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(hangSanXuat.TenHangSanXuatKhongDau))
                {
                    hangSanXuat.TenHangSanXuatKhongDau = hangSanXuat.TenHangSanXuat.GenerateSlug();
                }
                string path = "";

                // Nếu hình ảnh không bỏ trống thì upload
                if (hangSanXuat.DuLieuHinhAnh != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string folder = "/uploads/";
                    string fileExtension = Path.GetExtension(hangSanXuat.DuLieuHinhAnh.FileName).ToLower();
                    string fileName = hangSanXuat.TenHangSanXuat;
                    string fileNameSluged = fileName.GenerateSlug();
                    path = fileNameSluged + fileExtension;
                    string physicalPath = Path.Combine(wwwRootPath + folder, fileNameSluged + fileExtension);
                    using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                    {
                        await hangSanXuat.DuLieuHinhAnh.CopyToAsync(fileStream);
                    }
                }

                // Cập nhật đường dẫn vào CSDL
                hangSanXuat.HinhAnh = path ?? null;
                _context.Add(hangSanXuat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hangSanXuat);
        }

        // GET: HangSanXuat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangSanXuat = await _context.HangSanXuat.FindAsync(id);
            if (hangSanXuat == null)
            {
                return NotFound();
            }
            return View(hangSanXuat);
        }

        // POST: HangSanXuat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TenHangSanXuat,TenHangSanXuatKhongDau,DuLieuHinhAnh")] HangSanXuat hangSanXuat)
        {
            if (id != hangSanXuat.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(hangSanXuat.TenHangSanXuatKhongDau))
                    {
                        hangSanXuat.TenHangSanXuatKhongDau = hangSanXuat.TenHangSanXuat.GenerateSlug();
                    }

                    string path = "";

                    // Nếu hình ảnh không bỏ trống thì upload ảnh mới
                    if (hangSanXuat.DuLieuHinhAnh != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string folder = "/uploads/";
                        string fileExtension = Path.GetExtension(hangSanXuat.DuLieuHinhAnh.FileName).ToLower();
                        string fileName = hangSanXuat.TenHangSanXuat;
                        string fileNameSluged = fileName.GenerateSlug();
                        path = fileNameSluged + fileExtension;
                        string physicalPath = Path.Combine(wwwRootPath + folder, fileNameSluged + fileExtension);
                        using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                        {
                            await hangSanXuat.DuLieuHinhAnh.CopyToAsync(fileStream);
                        }
                    }
                    _context.Update(hangSanXuat);
                    if (string.IsNullOrEmpty(path))
                        _context.Entry(hangSanXuat).Property(x => x.HinhAnh).IsModified = false;
                    else
                        hangSanXuat.HinhAnh = path;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangSanXuatExists(hangSanXuat.ID))
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
            return View(hangSanXuat);
        }

        // GET: HangSanXuat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangSanXuat = await _context.HangSanXuat
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hangSanXuat == null)
            {
                return NotFound();
            }

            return View(hangSanXuat);
        }

        // POST: HangSanXuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hangSanXuat = await _context.HangSanXuat.FindAsync(id);
            if (hangSanXuat != null)
            {
                // Xóa hình ảnh (nếu có)
                if (!string.IsNullOrEmpty(hangSanXuat.HinhAnh))
                {
                    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "uploads", hangSanXuat.HinhAnh);
                    if (System.IO.File.Exists(imagePath)) System.IO.File.Delete(imagePath);
                }
                _context.HangSanXuat.Remove(hangSanXuat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HangSanXuatExists(int id)
        {
            return _context.HangSanXuat.Any(e => e.ID == id);
        }
    }
}
