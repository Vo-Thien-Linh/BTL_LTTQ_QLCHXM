using System.Data;
using DAL;
using DTO;

namespace BLL
{
    public class DashboardBLL
    {
        private readonly DashboardDAL _dal = new DashboardDAL();

        // Lấy thống kê tổng quan (4 cards)
        public DashboardDTO GetStats() => _dal.GetStats();

        // Lấy doanh thu 7 ngày gần nhất
        public DataTable GetDoanhThu7Ngay() => _dal.GetDoanhThu7Ngay();

        // Lấy top 5 xe bán chạy/cho thuê nhiều
        public DataTable GetTop5Xe() => _dal.GetTop5Xe();

        // Lấy hoạt động gần đây
        public DataTable GetHoatDongGanDay(int top = 10) => _dal.GetHoatDongGanDay(top);

        // Lấy cảnh báo tồn kho
        public DataTable GetCanhBaoTonKho(int mucCanhBao = 30) => _dal.GetCanhBaoTonKho(mucCanhBao);

        // Giữ lại method cũ để tương thích
        public DataTable LayXeMoiNhap(int topN = 8) => _dal.LayXeMoiNhap(topN);
    }
}