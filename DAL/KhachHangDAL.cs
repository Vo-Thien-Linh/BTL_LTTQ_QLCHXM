using DTO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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
                            NgayTao, NgayCapNhat, SoCCCD, LoaiGiayTo, AnhGiayTo 
                            FROM KhachHang 
                            ORDER BY MaKH DESC";
            return DataProvider.ExecuteQuery(query);
        }

        /// <summary>
        /// Tìm kiếm khách hàng theo nhiều tiêu chí
        /// </summary>
        public DataTable SearchKhachHang(string searchBy, string keyword)
        {
            string[] VALID_FIELDS = { "MaKH", "HoTenKH", "Sdt", "Email" };

            if (!VALID_FIELDS.Contains(searchBy))
                return GetAllKhachHang();

            string query = $@"SELECT * FROM KhachHang WHERE {searchBy} LIKE @keyword ORDER BY MaKH";
            SqlParameter[] parameters = new SqlParameter[] {
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
                            NgayTao, NgayCapNhat, SoCCCD, LoaiGiayTo, AnhGiayTo 
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
        public bool InsertKhachHang(KhachHangDTO kh)
        {
            string query = @"INSERT INTO KhachHang (MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, 
                            Email, DiaChi, NgayTao, NgayCapNhat, SoCCCD, LoaiGiayTo, AnhGiayTo)
                            VALUES (@MaKH, @HoTenKH, @NgaySinh, @GioiTinh, @Sdt, 
                            @Email, @DiaChi, GETDATE(), GETDATE(), @SoCCCD, @LoaiGiayTo, @AnhGiayTo)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", kh.MaKH),
                new SqlParameter("@HoTenKH", kh.HoTenKH),
                new SqlParameter("@NgaySinh", (object)kh.NgaySinh ?? DBNull.Value),
                new SqlParameter("@GioiTinh", (object)kh.GioiTinh ?? DBNull.Value),
                new SqlParameter("@Sdt", (object)kh.Sdt ?? DBNull.Value),
                new SqlParameter("@Email", (object)kh.Email ?? DBNull.Value),
                new SqlParameter("@DiaChi", (object)kh.DiaChi ?? DBNull.Value),
                new SqlParameter("@SoCCCD", (object)kh.SoCCCD ?? DBNull.Value),
                new SqlParameter("@LoaiGiayTo", (object)kh.LoaiGiayTo ?? DBNull.Value),
                new SqlParameter("@AnhGiayTo", (object)kh.AnhGiayTo ?? DBNull.Value)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        public bool UpdateKhachHang(KhachHangDTO kh)
        {
            string query = @"UPDATE KhachHang 
                            SET HoTenKH = @HoTenKH, 
                                NgaySinh = @NgaySinh, 
                                GioiTinh = @GioiTinh, 
                                Sdt = @Sdt, 
                                Email = @Email, 
                                DiaChi = @DiaChi, 
                                SoCCCD = @SoCCCD,
                                LoaiGiayTo = @LoaiGiayTo,
                                AnhGiayTo = @AnhGiayTo,
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
                new SqlParameter("@DiaChi", (object)kh.DiaChi ?? DBNull.Value),
                new SqlParameter("@SoCCCD", (object)kh.SoCCCD ?? DBNull.Value),
                new SqlParameter("@LoaiGiayTo", (object)kh.LoaiGiayTo ?? DBNull.Value),
                new SqlParameter("@AnhGiayTo", (object)kh.AnhGiayTo ?? DBNull.Value)
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
        /// Lấy khách hàng theo số điện thoại
        /// </summary>
        public KhachHangDTO GetKhachHangBySdt(string sdt)
        {
            string query = @"SELECT MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                            NgayTao, NgayCapNhat, SoCCCD, LoaiGiayTo, AnhGiayTo 
                            FROM KhachHang 
                            WHERE Sdt = @Sdt";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Sdt", sdt)
            };

            DataTable dt = DataProvider.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new KhachHangDTO
                {
                    MaKH = row["MaKH"].ToString(),
                    HoTenKH = row["HoTenKH"].ToString(),
                    NgaySinh = row["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(row["NgaySinh"]) : (DateTime?)null,
                    GioiTinh = row["GioiTinh"] != DBNull.Value ? row["GioiTinh"].ToString() : null,
                    Sdt = row["Sdt"] != DBNull.Value ? row["Sdt"].ToString() : null,
                    Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                    DiaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : null,
                    SoCCCD = row["SoCCCD"] != DBNull.Value ? row["SoCCCD"].ToString() : null,
                    LoaiGiayTo = row["LoaiGiayTo"] != DBNull.Value ? row["LoaiGiayTo"].ToString() : null,
                    AnhGiayTo = row["AnhGiayTo"] != DBNull.Value ? (byte[])row["AnhGiayTo"] : null,
                    NgayTao = Convert.ToDateTime(row["NgayTao"]),
                    NgayCapNhat = Convert.ToDateTime(row["NgayCapNhat"])
                };
            }

            return null;
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