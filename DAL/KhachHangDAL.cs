using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class KhachHangDAL
    {
        /// <summary>
        /// Lấy tất cả khách hàng
        /// </summary>
        public DataTable GetAllKhachHang()
        {
            string query = @"SELECT MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                            NgayTao, NgayCapNhat 
                            FROM KhachHang 
                            ORDER BY MaKH DESC";
            return DataProvider.ExecuteQuery(query);
        }

        /// <summary>
        /// Tìm kiếm khách hàng theo nhiều tiêu chí
        /// </summary>
        public DataTable SearchKhachHang(string searchBy, string keyword)
        {
            string query = "";
            SqlParameter[] parameters = null;

            switch (searchBy)
            {
                case "Mã khách hàng":
                    query = @"SELECT MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                             NgayTao, NgayCapNhat 
                             FROM KhachHang 
                             WHERE MaKH LIKE @keyword 
                             ORDER BY MaKH DESC";
                    break;
                case "Họ và tên":
                    query = @"SELECT MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                             NgayTao, NgayCapNhat 
                             FROM KhachHang 
                             WHERE HoTenKH LIKE @keyword 
                             ORDER BY MaKH DESC";
                    break;
                case "Số điện thoại":
                    query = @"SELECT MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                             NgayTao, NgayCapNhat 
                             FROM KhachHang 
                             WHERE Sdt LIKE @keyword 
                             ORDER BY MaKH DESC";
                    break;
                case "Email":
                    query = @"SELECT MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                             NgayTao, NgayCapNhat 
                             FROM KhachHang 
                             WHERE Email LIKE @keyword 
                             ORDER BY MaKH DESC";
                    break;
                default:
                    return GetAllKhachHang();
            }

            parameters = new SqlParameter[]
            {
                new SqlParameter("@keyword", "%" + keyword + "%")
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Lấy thông tin khách hàng theo mã
        /// </summary>
        public DataTable GetKhachHangByMaKH(string maKH)
        {
            string query = @"SELECT MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                            NgayTao, NgayCapNhat 
                            FROM KhachHang 
                            WHERE MaKH = @MaKH";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH)
            };
            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Thêm khách hàng mới
        /// </summary>
        public bool InsertKhachHang(KhachHang kh)
        {
            string query = @"INSERT INTO KhachHang (MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, 
                            Email, DiaChi, NgayTao, NgayCapNhat)
                            VALUES (@MaKH, @HoTenKH, @NgaySinh, @GioiTinh, @Sdt, 
                            @Email, @DiaChi, GETDATE(), GETDATE())";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", kh.MaKH),
                new SqlParameter("@HoTenKH", kh.HoTenKH),
                new SqlParameter("@NgaySinh", (object)kh.NgaySinh ?? DBNull.Value),
                new SqlParameter("@GioiTinh", (object)kh.GioiTinh ?? DBNull.Value),
                new SqlParameter("@Sdt", (object)kh.Sdt ?? DBNull.Value),
                new SqlParameter("@Email", (object)kh.Email ?? DBNull.Value),
                new SqlParameter("@DiaChi", (object)kh.DiaChi ?? DBNull.Value)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        public bool UpdateKhachHang(KhachHang kh)
        {
            string query = @"UPDATE KhachHang 
                            SET HoTenKH = @HoTenKH, 
                                NgaySinh = @NgaySinh, 
                                GioiTinh = @GioiTinh, 
                                Sdt = @Sdt, 
                                Email = @Email, 
                                DiaChi = @DiaChi, 
                                NgayCapNhat = GETDATE()
                            WHERE MaKH = @MaKH";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", kh.MaKH),
                new SqlParameter("@HoTenKH", kh.HoTenKH),
                new SqlParameter("@NgaySinh", (object)kh.NgaySinh ?? DBNull.Value),
                new SqlParameter("@GioiTinh", (object)kh.GioiTinh ?? DBNull.Value),
                new SqlParameter("@Sdt", (object)kh.Sdt ?? DBNull.Value),
                new SqlParameter("@Email", (object)kh.Email ?? DBNull.Value),
                new SqlParameter("@DiaChi", (object)kh.DiaChi ?? DBNull.Value)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        public bool DeleteKhachHang(string maKH)
        {
            string query = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH)
            };
            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Kiểm tra mã khách hàng đã tồn tại
        /// </summary>
        public bool CheckMaKHExists(string maKH)
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE MaKH = @MaKH";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH)
            };
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        /// <summary>
        /// Kiểm tra số điện thoại đã tồn tại
        /// </summary>
        public bool CheckSdtExists(string sdt, string maKH = null)
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE Sdt = @Sdt";
            if (!string.IsNullOrEmpty(maKH))
            {
                query += " AND MaKH != @MaKH";
            }

            SqlParameter[] parameters;
            if (!string.IsNullOrEmpty(maKH))
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Sdt", sdt),
                    new SqlParameter("@MaKH", maKH)
                };
            }
            else
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Sdt", sdt)
                };
            }

            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        /// <summary>
        /// Kiểm tra email đã tồn tại
        /// </summary>
        public bool CheckEmailExists(string email, string maKH = null)
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE Email = @Email";
            if (!string.IsNullOrEmpty(maKH))
            {
                query += " AND MaKH != @MaKH";
            }

            SqlParameter[] parameters;
            if (!string.IsNullOrEmpty(maKH))
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Email", email),
                    new SqlParameter("@MaKH", maKH)
                };
            }
            else
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Email", email)
                };
            }

            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        /// <summary>
        /// Tự động tạo mã khách hàng mới
        /// </summary>
        public string GenerateMaKH()
        {
            string query = "SELECT TOP 1 MaKH FROM KhachHang ORDER BY MaKH DESC";
            object result = DataProvider.ExecuteScalar(query);

            if (result == null || result == DBNull.Value)
            {
                return "KH00000001";
            }

            string lastMaKH = result.ToString();
            int number = int.Parse(lastMaKH.Substring(2));
            number++;
            return "KH" + number.ToString("D8");
        }
        /// <summary>
        /// Kiểm tra số điện thoại đã tồn tại trong bảng NhanVien
        /// </summary>
        public bool CheckSdtExistsInNhanVien(string sdt)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE Sdt = @Sdt";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Sdt", sdt)
            };
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }
    }


}