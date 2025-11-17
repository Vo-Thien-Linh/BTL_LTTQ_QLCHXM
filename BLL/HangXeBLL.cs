using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class HangXeBLL
    {
        private HangXeDAL dal = new HangXeDAL();
        public DataTable GetAllHangXe()
        {
            return dal.GetAllHangXe();
        }
    }
}
