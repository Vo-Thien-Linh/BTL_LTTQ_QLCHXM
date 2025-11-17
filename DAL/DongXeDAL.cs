using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DongXeDAL
    {
        public DataTable GetDongXeByHang(string maHang)
        {
            string query = "SELECT MaDong, TenDong FROM DongXe WHERE MaHang = @MaHang";
            SqlParameter[] parameters = { new SqlParameter("@MaHang", maHang) };
            return DataProvider.ExecuteQuery(query, parameters);
        }
    }
}
