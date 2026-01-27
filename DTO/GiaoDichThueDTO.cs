using System;

namespace DTO
{
    public class GiaoDichThue
    {
        public int MaGDThue { get; set; }
        public string ID_Xe { get; set; }
        public string MaKH { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public decimal GiaThueNgay { get; set; }
        public decimal TongGia { get; set; }
        public string TrangThai { get; set; }
        public string TrangThaiThanhToan { get; set; }
        public string HinhThucThanhToan { get; set; }
        public decimal? SoTienCoc { get; set; }
        public string GiayToGiuLai { get; set; }
        public string MaTaiKhoan { get; set; }
        public string TrangThaiDuyet { get; set; }
        public string NguoiDuyet { get; set; }
        public DateTime? NgayDuyet { get; set; }
        public string GhiChuDuyet { get; set; }

        //  Các trường mới
        public DateTime? NgayGiaoXeThucTe { get; set; }
        public DateTime? NgayTraXeThucTe { get; set; }
        public string GhiChuGiaoXe { get; set; }
        public int? KmBatDau { get; set; }

        // ===== THÊM MỚI: KHUYẾN MÃI =====
        public string MaKM { get; set; }
        public decimal SoTienGiam { get; set; }
        public decimal? TongTienTruocGiam { get; set; }
        public decimal? TongThanhToan { get; set; }

        // Thông tin bổ sung
        public string TenKhachHang { get; set; }
        public string SdtKhachHang { get; set; }
        public string TenXe { get; set; }
        public string BienSo { get; set; }
        public string TenNhanVien { get; set; }
        public int SoNgayThue { get; set; }

        public GiaoDichThue()
        {
            TrangThaiDuyet = "Chờ duyệt";
            TrangThai = "Chờ xác nhận";
            TrangThaiThanhToan = "Chưa thanh toán";
            SoTienGiam = 0;
        }

        public GiaoDichThue(int maGDThue, string iD_Xe, string maKH, DateTime ngayBatDau,
            DateTime ngayKetThuc, decimal giaThueNgay, decimal tongGia, string trangThai,
            string trangThaiThanhToan, string hinhThucThanhToan, decimal? soTienCoc,
            string giayToGiuLai, string maTaiKhoan, string trangThaiDuyet,
            string nguoiDuyet, DateTime? ngayDuyet, string ghiChuDuyet)
        {
            MaGDThue = maGDThue;
            ID_Xe = iD_Xe;
            MaKH = maKH;
            NgayBatDau = ngayBatDau;
            NgayKetThuc = ngayKetThuc;
            GiaThueNgay = giaThueNgay;
            TongGia = tongGia;
            TrangThai = trangThai;
            TrangThaiThanhToan = trangThaiThanhToan;
            HinhThucThanhToan = hinhThucThanhToan;
            SoTienCoc = soTienCoc;
            GiayToGiuLai = giayToGiuLai;
            MaTaiKhoan = maTaiKhoan;
            TrangThaiDuyet = trangThaiDuyet;
            NguoiDuyet = nguoiDuyet;
            NgayDuyet = ngayDuyet;
            GhiChuDuyet = ghiChuDuyet;
            SoTienGiam = 0;
        }
    }
}