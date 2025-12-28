using System;

namespace DTO
{
    public class KhuyenMaiDTO
    {
        public string MaKM { get; set; }
        public string TenKM { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string LoaiKhuyenMai { get; set; } // "Giảm %" hoặc "Giảm tiền"
        public decimal? PhanTramGiam { get; set; }
        public decimal? SoTienGiam { get; set; }
        public decimal? GiaTriGiamToiDa { get; set; }
        public string LoaiApDung { get; set; } // "Tất cả", "Xe bán", "Xe thuê", "Phụ tùng"
        public string TrangThai { get; set; } // "Hoạt động", "Tạm dừng", "Hết hạn", "Hủy"
        public string MaTaiKhoan { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public string GhiChu { get; set; }

        public KhuyenMaiDTO()
        {
            NgayBatDau = DateTime.Now;
            NgayKetThuc = DateTime.Now.AddDays(30);
            LoaiKhuyenMai = "Giảm %";
            LoaiApDung = "Tất cả";
            TrangThai = "Hoạt động";
        }
    }
}
