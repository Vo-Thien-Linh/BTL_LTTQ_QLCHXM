using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MauSacDAL
    {
        public DataTable GetAllMauSac()
        {
            string query = "SELECT MaMau, TenMau FROM MauSac";
            return DataProvider.ExecuteQuery(query);
        }
    }
}
