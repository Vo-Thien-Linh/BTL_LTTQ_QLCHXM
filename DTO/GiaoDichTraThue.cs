using System;

namespace DTO
{
    public class GiaoDichTraThue
    {
        public int MaGDTraThue { get; set; }
        public int MaGDThue { get; set; }
        public DateTime NgayTraXe { get; set; }
        public string TinhTrangXe { get; set; } // "Tốt", "Trầy xước nhẹ", "Hư hỏng"
        public decimal? ChiPhiPhatSinh { get; set; }
        public decimal? TienHoanCoc { get; set; }
        public decimal? TienPhat { get; set; } // Phí phạt trả muộn
        public string GhiChu { get; set; }
        public string MaTaiKhoan { get; set; }

        // Thông tin bổ sung
        public string TenNhanVien { get; set; }
        public int SoNgayQuaHan { get; set; }

        public GiaoDichTraThue()
        {
            NgayTraXe = DateTime.Now;
            TinhTrangXe = "Tốt";
        }
    }
}