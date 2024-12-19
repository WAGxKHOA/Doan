﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Doan.Models;
namespace Doan.Models
{
    public class SanPham
    {
        [DisplayName("Mã sản phẩm")]
        public int ID { get; set; }

        [DisplayName("Hãng sản xuất")]
        [Required(ErrorMessage = "Hãng sản xuất không được bỏ trống.")]
        public int HangSanXuatID { get; set; }

        [DisplayName("Loại sản phẩm")]
        [Required(ErrorMessage = "Loại sản phẩm không được bỏ trống.")]
        public int LoaiSanPhamID { get; set; }

        [StringLength(255)]
        [DisplayName("Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được bỏ trống.")]
        public string TenSanPham { get; set; }

        [StringLength(255)]
        [DisplayName("Tên sản phẩm không dấu")]
        public string? TenSanPhamKhongDau { get; set; }

        [DisplayName("Đơn giá")]
        [Required(ErrorMessage = "Đơn giá không được bỏ trống.")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public int DonGia { get; set; }

        [DisplayName("Số lượng")]
        [Required(ErrorMessage = "Số lượng không được bỏ trống.")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public int SoLuong { get; set; }

        [StringLength(255)]
        [DisplayName("Hình ảnh")]
        public string? HinhAnh { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Mô tả chi tiết")]
        [DataType(DataType.MultilineText)]
        public string? MoTa { get; set; }

        public LoaiSanPham? LoaiSanPham { get; set; }
        public HangSanXuat? HangSanXuat { get; set; }
        public ICollection<DatHang_ChiTiet>? DatHang_ChiTiet { get; set; }

        [NotMapped]
        [Display(Name = "Hình ảnh sản phẩm")]
        public IFormFile? DuLieuHinhAnh { get; set; }
    }
}