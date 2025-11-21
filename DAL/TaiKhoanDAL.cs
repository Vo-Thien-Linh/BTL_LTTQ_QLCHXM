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
          AND tk.TrangThaiTaiKhoan = N'Hoạt động'";

            var parameters = new[]
            {
        new SqlParameter("@Sdt", soDienThoai),
        new SqlParameter("@MatKhau", matKhau)
    };

            try
            {
                System.Diagnostics.Debug.WriteLine("=== TaiKhoanDAL.DangNhap ===");
                System.Diagnostics.Debug.WriteLine($"@Sdt = [{soDienThoai}]");
                System.Diagnostics.Debug.WriteLine($"@MatKhau = [{matKhau}]");
                System.Diagnostics.Debug.WriteLine($"Query: {query}");

                DataTable dt = DataProvider.ExecuteQuery(query, parameters);

                System.Diagnostics.Debug.WriteLine($"Số dòng trả về: {dt.Rows.Count}");

                if (dt.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine("✅ Tìm thấy tài khoản!");
                    System.Diagnostics.Debug.WriteLine($"HoTen: {dt.Rows[0]["HoTen"]}");
                    return dt.Rows[0];
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("❌ KHÔNG TÌM THẤY TÀI KHOẢN");

                    // DEBUG: Kiểm tra có tài khoản với SĐT này không
                    string checkQuery = @"
                SELECT 
                    tk.Password,
                    tk.TrangThaiTaiKhoan,
                    nv.Sdt,
                    nv.HoTenNV
                FROM TaiKhoan tk
                INNER JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                WHERE nv.Sdt = @Sdt";

                    var checkParams = new[] { new SqlParameter("@Sdt", soDienThoai) };
                    DataTable checkDt = DataProvider.ExecuteQuery(checkQuery, checkParams);

                    if (checkDt.Rows.Count > 0)
                    {
                        System.Diagnostics.Debug.WriteLine("⚠️ Tìm thấy tài khoản với SĐT này:");
                        System.Diagnostics.Debug.WriteLine($"   Password trong DB: [{checkDt.Rows[0]["Password"]}]");
                        System.Diagnostics.Debug.WriteLine($"   TrangThai trong DB: [{checkDt.Rows[0]["TrangThaiTaiKhoan"]}]");
                        System.Diagnostics.Debug.WriteLine($"   HoTenNV: [{checkDt.Rows[0]["HoTenNV"]}]");

                        // So sánh password
                        string dbPassword = checkDt.Rows[0]["Password"].ToString();
                        if (dbPassword == matKhau)
                        {
                            System.Diagnostics.Debug.WriteLine("✅ Password KHỚP!");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"❌ Password KHÔNG KHỚP!");
                            System.Diagnostics.Debug.WriteLine($"   Bạn nhập: [{matKhau}] (length: {matKhau.Length})");
                            System.Diagnostics.Debug.WriteLine($"   Trong DB: [{dbPassword}] (length: {dbPassword.Length})");
                        }

                        // Kiểm tra trạng thái
                        string dbTrangThai = checkDt.Rows[0]["TrangThaiTaiKhoan"].ToString();
                        if (dbTrangThai == "Hoạt động")
                        {
                            System.Diagnostics.Debug.WriteLine("✅ TrangThai KHỚP!");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"❌ TrangThai KHÔNG KHỚP!");
                            System.Diagnostics.Debug.WriteLine($"   Trong DB: [{dbTrangThai}]");
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("❌ KHÔNG có tài khoản nào với SĐT này");
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ LỖI DAL: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                return null;
            }
        }

        /// <summary>
        /// Tự động tạo mã tài khoản mới
        /// </summary>
        public string GenerateMaTaiKhoan()
        {
            string query = "SELECT TOP 1 MaTaiKhoan FROM TaiKhoan ORDER BY MaTaiKhoan DESC";
            object result = DataProvider.ExecuteScalar(query);

            if (result == null || result == DBNull.Value)
            {
                return "TK00000001";
            }

            string lastMaTK = result.ToString();
            int number = int.Parse(lastMaTK.Substring(2));
            number++;
            return "TK" + number.ToString("D8");
        }

        /// <summary>
        /// Tạo tài khoản cho nhân viên mới
        /// </summary>
        public bool CreateAccountForEmployee(string maNV, string sdt, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                // Tạo mã tài khoản tự động
                string maTaiKhoan = GenerateMaTaiKhoan();

                // Tên đăng nhập = số điện thoại
                string tenDangNhap = sdt;

                // Mật khẩu mặc định = số điện thoại
                string matKhau = sdt;

                // ✅ QUAN TRỌNG: Query phải đúng
                string query = @"INSERT INTO TaiKhoan 
                        (MaTaiKhoan, TenDangNhap, Password, LoaiTaiKhoan, TrangThaiTaiKhoan, MaNV, NgayTao, NgayCapNhat)
                        VALUES 
                        (@MaTaiKhoan, @TenDangNhap, @Password, @LoaiTaiKhoan, @TrangThaiTaiKhoan, @MaNV, @NgayTao, @NgayCapNhat)";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@MaTaiKhoan", maTaiKhoan),
            new SqlParameter("@TenDangNhap", tenDangNhap),
            new SqlParameter("@Password", matKhau),  // ✅ ĐÂY LÀ PARAMETER BỊ THIẾU
            new SqlParameter("@LoaiTaiKhoan", "NhanVien"),
            new SqlParameter("@TrangThaiTaiKhoan", "Hoạt động"),
            new SqlParameter("@MaNV", maNV),
            new SqlParameter("@NgayTao", DateTime.Now),
            new SqlParameter("@NgayCapNhat", DateTime.Now)
                };

                System.Diagnostics.Debug.WriteLine("=== Tạo tài khoản nhân viên ===");
                System.Diagnostics.Debug.WriteLine($"MaTaiKhoan: {maTaiKhoan}");
                System.Diagnostics.Debug.WriteLine($"TenDangNhap: {tenDangNhap}");
                System.Diagnostics.Debug.WriteLine($"Password: {matKhau}");
                System.Diagnostics.Debug.WriteLine($"MaNV: {maNV}");

                int result = DataProvider.ExecuteNonQuery(query, parameters);

                if (result > 0)
                {
                    System.Diagnostics.Debug.WriteLine("✅ Tạo tài khoản thành công!");
                    return true;
                }
                else
                {
                    errorMessage = "Không thể thêm tài khoản vào database!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi tạo tài khoản: " + ex.Message;
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra tên đăng nhập đã tồn tại
        /// </summary>
        public bool CheckTenDangNhapExists(string tenDangNhap)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", tenDangNhap)
            };
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        /// <summary>
        /// Xóa tài khoản theo mã nhân viên
        /// </summary>
        public bool DeleteAccountByMaNV(string maNV)
        {
            try
            {
                string query = "DELETE FROM TaiKhoan WHERE MaNV = @MaNV";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNV", maNV)
                };
                return DataProvider.ExecuteNonQuery(query, parameters) >= 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra nhân viên đã có tài khoản chưa
        /// </summary>
        public bool CheckEmployeeHasAccount(string maNV)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoan WHERE MaNV = @MaNV";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV)
            };
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        /// <summary>
        /// Cập nhật mật khẩu
        /// </summary>
        public bool UpdatePassword(string maTaiKhoan, string newPassword, out string errorMessage)
        {
            errorMessage = "";
            try
            {
                string query = @"UPDATE TaiKhoan 
                                SET Password = @Password, 
                                    NgayCapNhat = @NgayCapNhat
                                WHERE MaTaiKhoan = @MaTaiKhoan";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaTaiKhoan", maTaiKhoan),
                    new SqlParameter("@Password", newPassword),
                    new SqlParameter("@NgayCapNhat", DateTime.Now)
                };

                return DataProvider.ExecuteNonQuery(query, parameters) > 0;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi cập nhật mật khẩu: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Lấy thông tin tài khoản theo mã tài khoản
        /// </summary>
        public DataTable GetAccountById(string maTaiKhoan)
        {
            string query = @"SELECT tk.*, 
                            ISNULL(kh.HoTenKH, nv.HoTenNV) AS HoTen,
                            ISNULL(kh.Sdt, nv.Sdt) AS SoDienThoai,
                            nv.ChucVu
                            FROM TaiKhoan tk
                            LEFT JOIN KhachHang kh ON tk.MaKH = kh.MaKH
                            LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                            WHERE tk.MaTaiKhoan = @MaTaiKhoan";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaTaiKhoan", maTaiKhoan)
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Lấy tài khoản theo mã nhân viên
        /// </summary>
        public DataTable GetAccountByMaNV(string maNV)
        {
            string query = @"SELECT tk.*, nv.HoTenNV, nv.Sdt, nv.ChucVu
                            FROM TaiKhoan tk
                            INNER JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                            WHERE tk.MaNV = @MaNV";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV)
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Khóa/Mở khóa tài khoản
        /// </summary>
        public bool UpdateAccountStatus(string maTaiKhoan, string trangThai, out string errorMessage)
        {
            errorMessage = "";
            try
            {
                string query = @"UPDATE TaiKhoan 
                                SET TrangThaiTaiKhoan = @TrangThai, 
                                    NgayCapNhat = @NgayCapNhat
                                WHERE MaTaiKhoan = @MaTaiKhoan";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaTaiKhoan", maTaiKhoan),
                    new SqlParameter("@TrangThai", trangThai),
                    new SqlParameter("@NgayCapNhat", DateTime.Now)
                };

                return DataProvider.ExecuteNonQuery(query, parameters) > 0;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi cập nhật trạng thái: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Lấy tất cả tài khoản nhân viên
        /// </summary>
        public DataTable GetAllEmployeeAccounts()
        {
            string query = @"SELECT tk.MaTaiKhoan, tk.TenDangNhap, tk.TrangThaiTaiKhoan,
                            tk.NgayTao, nv.MaNV, nv.HoTenNV, nv.ChucVu, nv.Sdt
                            FROM TaiKhoan tk
                            INNER JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                            WHERE tk.LoaiTaiKhoan = N'NhanVien'
                            ORDER BY tk.NgayTao DESC";

            return DataProvider.ExecuteQuery(query);
        }
    }
}