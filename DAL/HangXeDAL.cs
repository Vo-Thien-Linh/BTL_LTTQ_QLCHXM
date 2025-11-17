using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HangXeDAL
    {
        public DataTable GetAllHangXe()
        {
            string query = "SELECT MaHang, TenHang FROM HangXe";
            return DataProvider.ExecuteQuery(query);
        }
    }
}
