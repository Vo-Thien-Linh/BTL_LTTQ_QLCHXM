using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoaiXeBLL
    {
        private LoaiXeDAL dal = new LoaiXeDAL();
        public DataTable GetAllLoaiXe()
        {
            return dal.GetAllLoaiXe();
        }
    }
}
