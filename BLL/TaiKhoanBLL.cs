using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TaiKhoanBLL
    {
        /// <summary>
        /// Đăng nhập – trả về true nếu thành công
        /// </summary>
        public static bool DangNhap(string soDienThoai, string matKhau)
        {
            try
            {
                DataRow row = TaiKhoanDAL.DangNhap(soDienThoai, matKhau);

                if (row != null)
                {
                    // Lưu vào CurrentUser
                    CurrentUser.MaTaiKhoan = row["MaTaiKhoan"].ToString();
                    CurrentUser.LoaiTaiKhoan = row["LoaiTaiKhoan"].ToString();
                    CurrentUser.HoTen = row["HoTen"].ToString();
                    CurrentUser.MaKH = row["MaKH"] != DBNull.Value ? row["MaKH"].ToString() : null;
                    CurrentUser.MaNV = row["MaNV"] != DBNull.Value ? row["MaNV"].ToString() : null;
                    CurrentUser.ChucVu = row["ChucVu"] != DBNull.Value ? row["ChucVu"].ToString() : null;

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Đăng xuất – xóa CurrentUser
        /// </summary>
        public static void DangXuat()
        {
            CurrentUser.Clear();
        }
    }
}
