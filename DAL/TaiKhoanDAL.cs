using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TaiKhoanDAL
    {
        /// <summary>
        /// Đăng nhập bằng SĐT + Mật khẩu
        /// Hỗ trợ cả Khách hàng và Nhân viên
        /// </summary>
        public static DataRow DangNhap(string soDienThoai, string matKhau)
        {
            string query = @"
                SELECT 
                    tk.MaTaiKhoan,
                    tk.LoaiTaiKhoan,
                    tk.TrangThaiTaiKhoan,
                    tk.MaKH,
                    tk.MaNV,
                    ISNULL(kh.HoTenKH, nv.HoTenNV) AS HoTen,
                    kh.Sdt AS SdtKH,
                    nv.Sdt AS SdtNV,
                    nv.ChucVu
                FROM TaiKhoan tk
                LEFT JOIN KhachHang kh ON tk.MaKH = kh.MaKH
                LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                WHERE (kh.Sdt = @Sdt OR nv.Sdt = @Sdt)
                  AND tk.Password = @MatKhau
                  AND tk.TrangThaiTaiKhoan = 'Hoạt động'";

            var parameters = new[]
            {
                new SqlParameter("@Sdt", soDienThoai),
                new SqlParameter("@MatKhau", matKhau)
            };

            DataTable dt = DataProvider.ExecuteQuery(query, parameters);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
    }
}
