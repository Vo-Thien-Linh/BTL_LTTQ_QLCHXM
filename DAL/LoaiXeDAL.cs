using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LoaiXeDAL
    {
        public DataTable GetAllLoaiXe()
        {
            string query = "SELECT ID_Loai, MaHang, MaDong, MaMau, NamSX FROM LoaiXe";
            return DataProvider.ExecuteQuery(query);
        }
    }
}
