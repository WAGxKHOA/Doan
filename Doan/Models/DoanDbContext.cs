using Microsoft.EntityFrameworkCore;

namespace Doan.Models
{
    public class DoanDbContext:DbContext
    {
        public DoanDbContext(DbContextOptions<DoanDbContext> options) : base(options) { }

        public DbSet<LoaiSanPham> LoaiSanPham { get; set; }
        public DbSet<HangSanXuat> HangSanXuat { get; set; }
        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<TinhTrang> TinhTrang { get; set; }
        public DbSet<DatHang> DatHang { get; set; }
        public DbSet<DatHang_ChiTiet> DatHang_ChiTiet { get; set; }
    }
}
