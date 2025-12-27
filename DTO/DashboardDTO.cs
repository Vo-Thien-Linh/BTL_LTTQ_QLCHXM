using System;

namespace DTO
{
    public class DashboardDTO
    {
        // Stat Cards
        public int XeSanSang { get; set; }
        public decimal DoanhThuThangNay { get; set; }
        public int TongKhachHang { get; set; }
        public int TongGiaoDich { get; set; }
    }

    // DTO cho hoạt động gần đây
    public class HoatDongGanDayDTO
    {
        public DateTime NgayGiaoDich { get; set; }
        public string LoaiGiaoDich { get; set; }  // "Bán" hoặc "Thuê"
        public string TenKhachHang { get; set; }
        public string ThongTinXe { get; set; }  // Ví dụ: "Honda Wave Đỏ"
        public decimal GiaTri { get; set; }
    }

    // DTO cho top xe
    public class TopXeDTO
    {
        public string TenXe { get; set; }  // Ví dụ: "Honda Wave"
        public int SoLuong { get; set; }
        public decimal DoanhThu { get; set; }
    }

    // DTO cho cảnh báo tồn kho
    public class CanhBaoTonKhoDTO
    {
        public string TenPhuTung { get; set; }
        public int SoLuongTon { get; set; }
        public int MucCanhBao { get; set; }  // Ngưỡng cảnh báo
        public string TrangThai { get; set; }  // "Hết hàng", "Sắp hết", "Bình thường"
    }

    // DTO cho doanh thu theo ngày
    public class DoanhThuTheoNgayDTO
    {
        public DateTime Ngay { get; set; }
        public decimal DoanhThuBan { get; set; }
        public decimal DoanhThuThue { get; set; }
        public decimal TongDoanhThu { get; set; }
    }
}