using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NhaCungCapDAL
    {
        public DataTable GetAllNhaCungCap()
        {
            string query = "SELECT MaNCC, TenNCC FROM NhaCungCap";
            return DataProvider.ExecuteQuery(query);
        }
    }
}
