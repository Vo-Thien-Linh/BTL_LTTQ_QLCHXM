using System.Data;
using DAL;
using DTO;

namespace BLL
{
    public class DashboardBLL
    {
        private readonly DashboardDAL _dal = new DashboardDAL();

        public DashboardDTO GetStats() => _dal.GetStats();
        public DataTable LayXeMoiNhap(int topN = 8) => _dal.LayXeMoiNhap(topN);
    }
}