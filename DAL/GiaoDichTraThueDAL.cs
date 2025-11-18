using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class GiaoDichTraThueDAL
    {
        /// <summary>
        /// Thêm giao dịch trả xe
        /// </summary>
        public bool InsertGiaoDichTraXe(GiaoDichTraThue gdTra)
        {
            string query = @"
                INSERT INTO GiaoDichTraThue 
                (MaGDThue, NgayTraXe, TinhTrangXe, ChiPhiPhatSinh, GhiChu, MaTaiKhoan)
                VALUES 
                (@MaGDThue, @NgayTraXe, @TinhTrangXe, @ChiPhiPhatSinh, @GhiChu, @MaTaiKhoan)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaGDThue", gdTra.MaGDThue),
                new SqlParameter("@NgayTraXe", gdTra.NgayTraXe),
                new SqlParameter("@TinhTrangXe", gdTra.TinhTrangXe),
                new SqlParameter("@ChiPhiPhatSinh", (object)gdTra.ChiPhiPhatSinh ?? DBNull.Value),
                new SqlParameter("@GhiChu", (object)gdTra.GhiChu ?? DBNull.Value),
                new SqlParameter("@MaTaiKhoan", (object)gdTra.MaTaiKhoan ?? DBNull.Value)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Lấy thông tin trả xe theo mã giao dịch thuê
        /// </summary>
        public DataTable GetByMaGDThue(int maGDThue)
        {
            string query = @"
                SELECT 
                    gdt.*,
                    nv.HoTenNV AS TenNhanVien
                FROM GiaoDichTraThue gdt
                LEFT JOIN TaiKhoan tk ON gdt.MaTaiKhoan = tk.MaTaiKhoan
                LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                WHERE gdt.MaGDThue = @MaGDThue";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaGDThue", maGDThue)
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }
    }
}