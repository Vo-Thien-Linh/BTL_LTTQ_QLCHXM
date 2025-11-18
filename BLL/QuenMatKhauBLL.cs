using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL
{
    public class QuenMatKhauBLL
    {
        private readonly QuenMatKhauDAL dal = new QuenMatKhauDAL();

        public bool KiemTraEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                return false;
            return dal.KiemTraEmailTonTai(email);
        }

        public bool DoiMatKhau(string email, string matKhauMoi, string xacNhanMK)
        {
            if (string.IsNullOrWhiteSpace(matKhauMoi) || matKhauMoi.Length < 6)
                return false;
            if (matKhauMoi != xacNhanMK)
                return false;

            var dto = new QuenMatKhauDTO
            {
                Email = email,
                MatKhauMoi = matKhauMoi
            };

            return dal.DoiMatKhau(dto);
        }
    }
}
