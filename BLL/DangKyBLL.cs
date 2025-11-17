using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class DangKyBLL
    {
        public static DataTable LayDanhSachChucVu()
        {
            return DangKyDAL.GetChucVu();
        }

        public static bool KiemTraSoDienThoaiTrung(string sdt)
        {
            return DangKyDAL.IsPhoneExists(sdt);
        }

        public static bool KiemTraEmailTrung(string email)
        {
            return DangKyDAL.IsEmailExists(email);
        }

        public static bool KiemTraCCCDTrung(string cccd)
        {
            return DangKyDAL.IsCCCDExists(cccd);
        }

        public static bool DangKyNhanVien(
            string hoTen, DateTime ngaySinh, string gioiTinh,
            string sdt, string email, string diaChi, string chucVu, string cccd, string matKhau)
        {
            if (KiemTraSoDienThoaiTrung(sdt)) return false;
            if (KiemTraEmailTrung(email)) return false;
            if (KiemTraCCCDTrung(cccd)) return false;

            string maNV = DangKyDAL.InsertNhanVien(
                hoTen, ngaySinh, gioiTinh, sdt, email, diaChi, chucVu, cccd);

            if (maNV == null) return false;
            return DangKyDAL.InsertTaiKhoan(maNV, matKhau);
        }
    }
}
