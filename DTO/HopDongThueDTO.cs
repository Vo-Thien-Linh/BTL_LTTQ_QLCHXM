using System;

namespace DTO
{
    public class HopDongThue
    {
        public int MaHDT { get; set; }
        public int MaGDThue { get; set; }
        public string MaKH { get; set; }
        public string MaTaiKhoan { get; set; }
        public string ID_Xe { get; set; }
        public DateTime NgayLap { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public decimal GiaThue { get; set; }
        public decimal? TienDatCoc { get; set; }
        public string DieuKhoan { get; set; }
        public string GhiChu { get; set; }
        public string TrangThaiHopDong { get; set; }
        public string FileHopDong { get; set; }

        // Thông tin bổ sung
        public string HoTenKH { get; set; }
        public string SdtKhachHang { get; set; }
        public string TenNhanVien { get; set; }
        public string TenXe { get; set; }
        public string BienSo { get; set; }

        public HopDongThue()
        {
            NgayLap = DateTime.Now;
            TrangThaiHopDong = "Đang hiệu lực";
        }
    }
}