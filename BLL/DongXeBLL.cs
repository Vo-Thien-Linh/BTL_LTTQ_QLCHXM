using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DongXeBLL
    {
        private DongXeDAL dal = new DongXeDAL();
        public DataTable GetDongXeByHang(string maHang)
        {
            return dal.GetDongXeByHang(maHang);
        }
    }
}
