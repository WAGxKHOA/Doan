using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Doan.Models;
namespace Doan.Models
{
    public class HangSanXuat
    {
        [DisplayName("Mã hãng sản xuất")]
        public int ID { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Tên hãng sản xuất không được bỏ trống.")]
        [DisplayName("Tên hãng sản xuất")]
        public string TenHangSanXuat { get; set; }

        [StringLength(255)]
        [DisplayName("Tên hãng sản xuất không dấu")]
        public string? TenHangSanXuatKhongDau { get; set; }

        [StringLength(255)]
        [DisplayName("Hình ảnh")]
        public string? HinhAnh { get; set; }

        public ICollection<SanPham>? SanPham { get; set; }

        [NotMapped]
        [Display(Name = "Hình ảnh hãng sản xuất")]
        public IFormFile? DuLieuHinhAnh { get; set; }
    }
}