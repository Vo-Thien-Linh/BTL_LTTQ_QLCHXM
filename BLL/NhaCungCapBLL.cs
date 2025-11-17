using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NhaCungCapBLL
    {
        private NhaCungCapDAL dal = new NhaCungCapDAL();
        public DataTable GetAllNhaCungCap()
        {
            return dal.GetAllNhaCungCap();
        }
    }
}
