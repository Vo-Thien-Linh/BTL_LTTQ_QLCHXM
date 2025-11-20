using System;

namespace DTO
{
    public class HopDongMuaDTO
    {
        public int MaHDM { get; set; }
        public int MaGDBan { get; set; }
        public string MaKH { get; set; }
        public string MaTaiKhoan { get; set; }
        public string ID_Xe { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal GiaBan { get; set; }
        public string DieuKhoan { get; set; }
        public string GhiChu { get; set; }
        public string TrangThaiHopDong { get; set; }
        public string FileHopDong { get; set; }

        public HopDongMuaDTO()
        {
            NgayLap = DateTime.Now;
            TrangThaiHopDong = "Đang hiệu lực";
        }

        public HopDongMuaDTO(int maGDBan, string maKH, string maTaiKhoan, string idXe,
            DateTime ngayLap, decimal giaBan, string dieuKhoan = null, string ghiChu = null)
        {
            MaGDBan = maGDBan;
            MaKH = maKH;
            MaTaiKhoan = maTaiKhoan;
            ID_Xe = idXe;
            NgayLap = ngayLap;
            GiaBan = giaBan;
            DieuKhoan = dieuKhoan;
            GhiChu = ghiChu;
            TrangThaiHopDong = "Đang hiệu lực";
        }
    }
}
