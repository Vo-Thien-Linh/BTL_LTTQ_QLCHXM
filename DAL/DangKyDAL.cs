using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class DangKyDAL
    {
        public static DataTable GetChucVu()
        {
            string query = "SELECT DISTINCT ChucVu FROM NhanVien";
            return DataProvider.ExecuteQuery(query);
        }

        public static bool IsPhoneExists(string sdt)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE Sdt = @sdt";
            SqlParameter[] param = { new SqlParameter("@sdt", sdt) };
            return Convert.ToInt32(DataProvider.ExecuteScalar(query, param)) > 0;
        }

        public static bool IsEmailExists(string email)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE Email = @Email";
            SqlParameter[] param = { new SqlParameter("@Email", email) };
            return Convert.ToInt32(DataProvider.ExecuteScalar(query, param)) > 0;
        }

        public static bool IsCCCDExists(string cccd)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE CCCD = @CCCD";
            SqlParameter[] param = { new SqlParameter("@CCCD", cccd) };
            return Convert.ToInt32(DataProvider.ExecuteScalar(query, param)) > 0;
        }

        public static string GetNextMaNV()
        {
            string query = "SELECT TOP 1 MaNV FROM NhanVien WHERE MaNV LIKE 'NV%' ORDER BY MaNV DESC";
            object lastMaNV = DataProvider.ExecuteScalar(query);
            if (lastMaNV != null && lastMaNV.ToString().StartsWith("NV"))
            {
                string number = lastMaNV.ToString().Substring(2);
                int next = int.Parse(number) + 1;
                return "NV" + next.ToString("D8");
            }
            else
            {
                return "NV00000001";
            }
        }

        public static string GetNextMaTaiKhoan()
        {
            string query = "SELECT TOP 1 MaTaiKhoan FROM TaiKhoan WHERE MaTaiKhoan LIKE 'TK%' ORDER BY MaTaiKhoan DESC";
            object lastMaTK = DataProvider.ExecuteScalar(query);
            if (lastMaTK != null && lastMaTK.ToString().StartsWith("TK"))
            {
                string number = lastMaTK.ToString().Substring(2);
                int next = int.Parse(number) + 1;
                return "TK" + next.ToString("D8");
            }
            else
            {
                return "TK00000001";
            }
        }

        public static string InsertNhanVien(
            string hoTen, DateTime ngaySinh, string gioiTinh,
            string sdt, string email, string diaChi, string chucVu, string cccd)
        {
            string maNV = GetNextMaNV();
            string query = @"INSERT INTO NhanVien
            (MaNV, HoTenNV, NgaySinh, GioiTinh, Sdt, Email, DiaChi, ChucVu, CCCD, NgayTao, NgayCapNhat)
            VALUES (@MaNV, @HoTenNV, @NgaySinh, @GioiTinh, @Sdt, @Email, @DiaChi, @ChucVu, @CCCD, GETDATE(), GETDATE())";
            SqlParameter[] param = {
                new SqlParameter("@MaNV", maNV),
                new SqlParameter("@HoTenNV", hoTen),
                new SqlParameter("@NgaySinh", ngaySinh),
                new SqlParameter("@GioiTinh", gioiTinh ?? (object)DBNull.Value),
                new SqlParameter("@Sdt", sdt),
                new SqlParameter("@Email", email),
                new SqlParameter("@DiaChi", diaChi ?? (object)DBNull.Value),
                new SqlParameter("@ChucVu", chucVu),
                new SqlParameter("@CCCD", cccd)
            };
            int rows = DataProvider.ExecuteNonQuery(query, param);
            return rows > 0 ? maNV : null;
        }

        public static bool InsertTaiKhoan(string maNV, string password)
        {
            string maTK = GetNextMaTaiKhoan();
            string query = @"INSERT INTO TaiKhoan
            (MaTaiKhoan, TenDangNhap, Password, LoaiTaiKhoan, TrangThaiTaiKhoan, MaNV, NgayTao, NgayCapNhat)
            VALUES (@MaTaiKhoan, NULL, @Password, N'NhanVien', N'Hoạt động', @MaNV, GETDATE(), GETDATE())";
            SqlParameter[] param = {
                new SqlParameter("@MaTaiKhoan", maTK),
                new SqlParameter("@Password", password),
                new SqlParameter("@MaNV", maNV)
            };
            return DataProvider.ExecuteNonQuery(query, param) > 0;
        }
    }
}
