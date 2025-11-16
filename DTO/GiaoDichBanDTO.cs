using System;

namespace DTO
{
    public class GiaoDichBan
    {
        public int MaGDBan { get; set; }
        public string MaKH { get; set; }
        public string ID_Xe { get; set; }
        public DateTime NgayBan { get; set; }
        public decimal GiaBan { get; set; }
        public string TrangThaiThanhToan { get; set; }
        public string HinhThucThanhToan { get; set; }
        public string MaTaiKhoan { get; set; }
        public string TrangThaiDuyet { get; set; }
        public string NguoiDuyet { get; set; }
        public DateTime? NgayDuyet { get; set; }
        public string GhiChuDuyet { get; set; }

        // Thông tin bổ sung từ các bảng liên quan (không lưu DB)
        public string TenKhachHang { get; set; }
        public string SdtKhachHang { get; set; }
        public string TenXe { get; set; }
        public string BienSo { get; set; }
        public string TenNhanVien { get; set; }

        public GiaoDichBan()
        {
            TrangThaiDuyet = "Chờ duyệt";
            TrangThaiThanhToan = "Chưa thanh toán";
        }

        public GiaoDichBan(int maGDBan, string maKH, string iD_Xe, DateTime ngayBan,
            decimal giaBan, string trangThaiThanhToan, string hinhThucThanhToan,
            string maTaiKhoan, string trangThaiDuyet, string nguoiDuyet,
            DateTime? ngayDuyet, string ghiChuDuyet)
        {
            MaGDBan = maGDBan;
            MaKH = maKH;
            ID_Xe = iD_Xe;
            NgayBan = ngayBan;
            GiaBan = giaBan;
            TrangThaiThanhToan = trangThaiThanhToan;
            HinhThucThanhToan = hinhThucThanhToan;
            MaTaiKhoan = maTaiKhoan;
            TrangThaiDuyet = trangThaiDuyet;
            NguoiDuyet = nguoiDuyet;
            NgayDuyet = ngayDuyet;
            GhiChuDuyet = ghiChuDuyet;
        }
    }
}