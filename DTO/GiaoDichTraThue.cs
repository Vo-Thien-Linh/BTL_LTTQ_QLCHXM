using System;

namespace DTO
{
    public class GiaoDichTraThue
    {
        public int MaGDTraThue { get; set; }
        public int MaGDThue { get; set; }
        public DateTime NgayTraXe { get; set; }
        public string TinhTrangXe { get; set; }
        public decimal? ChiPhiPhatSinh { get; set; }
        public decimal? TienHoanCoc { get; set; }
        public decimal? TienPhat { get; set; }
        public string GhiChu { get; set; }
        public string MaTaiKhoan { get; set; }

        //  Các trường mới
        public int? KmKetThuc { get; set; }
        public int? SoNgayTraSom { get; set; }
        public decimal? TienHoanTraSom { get; set; }

        // Thông tin bổ sung
        public string TenNhanVien { get; set; }
        public int SoNgayQuaHan { get; set; }

        public GiaoDichTraThue()
        {
            NgayTraXe = DateTime.Now;
            TinhTrangXe = "Tốt";
            SoNgayTraSom = 0;
            TienHoanTraSom = 0;
        }
    }
}