using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class QuenMatKhauDAL
    {
        // 1. Kiểm tra email có tồn tại không
        public bool KiemTraEmailTonTai(string email)
        {
            string query = "SELECT COUNT(1) FROM NhanVien WHERE Email = @email AND Email IS NOT NULL";
            var param = new SqlParameter[] {
                new SqlParameter("@email", email)
            };

            return (int)DataProvider.ExecuteScalar(query, param) > 0;
        }

        // 2. Đổi mật khẩu mới (lưu text thường)
        public bool DoiMatKhau(QuenMatKhauDTO dto)
        {
            string query = @"
                UPDATE TaiKhoan 
                SET MatKhau = @matkhau 
                FROM TaiKhoan tk
                INNER JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                WHERE nv.Email = @email";

            var parameters = new SqlParameter[] {
                new SqlParameter("@email", dto.Email),
                new SqlParameter("@matkhau", dto.MatKhauMoi)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}
