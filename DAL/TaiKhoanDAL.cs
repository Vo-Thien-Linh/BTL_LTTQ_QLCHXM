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
        /// Đăng nhập bằng Email + Mật khẩu
        /// Email lấy từ bảng NhanVien hoặc KhachHang
        /// </summary>
        public static DataRow DangNhapBangEmail(string email, string matKhau)
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
            nv.ChucVu,
            nv.Email AS EmailNV,
            kh.Email AS EmailKH
        FROM TaiKhoan tk
        LEFT JOIN KhachHang kh ON tk.MaKH = kh.MaKH
        LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
        WHERE (LTRIM(RTRIM(nv.Email)) = @Email OR LTRIM(RTRIM(kh.Email)) = @Email)
          AND LTRIM(RTRIM(tk.Password)) = @MatKhau
          AND tk.TrangThaiTaiKhoan = N'Hoạt động'";

            var parameters = new[]
            {
        new SqlParameter("@Email", email.Trim()),
        new SqlParameter("@MatKhau", matKhau.Trim())
    };

            try
            {
                System.Diagnostics.Debug.WriteLine("=== TaiKhoanDAL.DangNhapBangEmail ===");
                System.Diagnostics.Debug.WriteLine($"@Email = [{email}]");
                System.Diagnostics.Debug.WriteLine($"@MatKhau = [{matKhau}]");
                System.Diagnostics.Debug.WriteLine($"Query: {query}");

                DataTable dt = DataProvider.ExecuteQuery(query, parameters);

                System.Diagnostics.Debug.WriteLine($"Số dòng trả về: {dt.Rows.Count}");

                if (dt.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine("✅ Tìm thấy tài khoản!");
                    System.Diagnostics.Debug.WriteLine($"HoTen: {dt.Rows[0]["HoTen"]}");
                    System.Diagnostics.Debug.WriteLine($"Email: {dt.Rows[0]["EmailNV"] ?? dt.Rows[0]["EmailKH"]}");
                    return dt.Rows[0];
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("❌ KHÔNG TÌM THẤY TÀI KHOẢN");

                    // DEBUG: Kiểm tra có tài khoản với Email này không
                    string checkQuery = @"
                SELECT 
                    tk.Password,
                    tk.TrangThaiTaiKhoan,
                    nv.Email,
                    nv.HoTenNV,
                    nv.Sdt
                FROM TaiKhoan tk
                INNER JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                WHERE LTRIM(RTRIM(nv.Email)) = @Email";

                    var checkParams = new[] { new SqlParameter("@Email", email.Trim()) };
                    DataTable checkDt = DataProvider.ExecuteQuery(checkQuery, checkParams);

                    if (checkDt.Rows.Count > 0)
                    {
                        System.Diagnostics.Debug.WriteLine("⚠️ Tìm thấy tài khoản với Email này:");
                        System.Diagnostics.Debug.WriteLine($"   Email trong DB: [{checkDt.Rows[0]["Email"]}]");
                        System.Diagnostics.Debug.WriteLine($"   Password trong DB: [{checkDt.Rows[0]["Password"]}]");
                        System.Diagnostics.Debug.WriteLine($"   TrangThai trong DB: [{checkDt.Rows[0]["TrangThaiTaiKhoan"]}]");
                        System.Diagnostics.Debug.WriteLine($"   HoTenNV: [{checkDt.Rows[0]["HoTenNV"]}]");

                        // So sánh password
                        string dbPassword = checkDt.Rows[0]["Password"].ToString().Trim();
                        string inputPassword = matKhau.Trim();

                        if (dbPassword == inputPassword)
                        {
                            System.Diagnostics.Debug.WriteLine("✅ Password KHỚP!");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"❌ Password KHÔNG KHỚP!");
                            System.Diagnostics.Debug.WriteLine($"   Bạn nhập: [{inputPassword}] (length: {inputPassword.Length})");
                            System.Diagnostics.Debug.WriteLine($"   Trong DB: [{dbPassword}] (length: {dbPassword.Length})");
                        }

                        // Kiểm tra trạng thái
                        string dbTrangThai = checkDt.Rows[0]["TrangThaiTaiKhoan"].ToString().Trim();
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
                        System.Diagnostics.Debug.WriteLine("❌ KHÔNG có tài khoản nào với Email này");

                        // Kiểm tra xem có email nào khớp không (có thể do tài khoản chưa được tạo)
                        string checkEmailQuery = @"
                    SELECT TOP 5 
                        nv.Email, 
                        nv.HoTenNV,
                        CASE WHEN tk.MaTaiKhoan IS NULL THEN N'Chưa có TK' ELSE N'Đã có TK' END AS TrangThaiTK
                    FROM NhanVien nv
                    LEFT JOIN TaiKhoan tk ON nv.MaNV = tk.MaNV
                    WHERE nv.Email IS NOT NULL";

                        DataTable emailListDt = DataProvider.ExecuteQuery(checkEmailQuery);
                        System.Diagnostics.Debug.WriteLine("📋 Danh sách một số Email trong hệ thống:");
                        foreach (DataRow row in emailListDt.Rows)
                        {
                            System.Diagnostics.Debug.WriteLine($"   - Email: [{row["Email"]}] | Họ tên: {row["HoTenNV"]} | {row["TrangThaiTK"]}");
                        }
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
        /// Tạo mật khẩu ngẫu nhiên 8 ký tự (chữ hoa, chữ thường, số)
        /// </summary>
        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";
            Random random = new Random();
            char[] password = new char[8];
            
            for (int i = 0; i < 8; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }
            
            return new string(password);
        }

        /// <summary>
        /// Tự động tạo mã tài khoản mới
        /// </summary>
        public string GenerateMaTaiKhoan()
        {
            try
            {
                string query = "SELECT TOP 1 MaTaiKhoan FROM TaiKhoan ORDER BY MaTaiKhoan DESC";
                object result = DataProvider.ExecuteScalar(query);

                System.Console.WriteLine($"=== GenerateMaTaiKhoan ===");
                System.Console.WriteLine($"Query result: {result}");

                if (result == null || result == DBNull.Value)
                {
                    System.Console.WriteLine("Bảng TaiKhoan rỗng, tạo mã đầu tiên: TK00000001");
                    return "TK00000001";
                }

                string lastMaTK = result.ToString().Trim(); // Trim khoảng trắng
                System.Console.WriteLine($"Mã TK cuối cùng: [{lastMaTK}]");

                if (lastMaTK.Length < 3 || !lastMaTK.StartsWith("TK"))
                {
                    System.Console.WriteLine($"⚠️ Mã không hợp lệ, tạo mã mới: TK00000001");
                    return "TK00000001";
                }

                string numberPart = lastMaTK.Substring(2).Trim();
                System.Console.WriteLine($"Phần số: [{numberPart}]");

                if (!int.TryParse(numberPart, out int number))
                {
                    System.Console.WriteLine($"❌ Không parse được số từ: [{numberPart}]");
                    return "TK00000001";
                }

                number++;
                string newMaTK = "TK" + number.ToString("D8");
                System.Console.WriteLine($"✅ Mã TK mới: {newMaTK}");
                return newMaTK;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"❌ Lỗi GenerateMaTaiKhoan: {ex.Message}");
                return "TK00000001";
            }
        }

        /// <summary>
        /// Tạo tài khoản với mật khẩu do admin nhập
        /// </summary>
        public bool CreateAccountForEmployeeWithPassword(string maNV, string email, string password, string chucVu, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                // Tạo mã tài khoản tự động
                string maTaiKhoan = GenerateMaTaiKhoan();

                // Tên đăng nhập = email
                string tenDangNhap = email;

                // Mật khẩu = admin nhập
                string matKhau = password;

                // ✅ Xác định loại tài khoản dựa trên chức vụ
                string chucVuNormalized = chucVu?.Trim().ToLower() ?? "";
                string loaiTaiKhoan = "NhanVien"; // Mặc định
                
                if (chucVuNormalized == "quản lý" || chucVuNormalized == "quan ly")
                    loaiTaiKhoan = "QuanLy";
                else if (chucVuNormalized == "thu ngân" || chucVuNormalized == "thu ngan")
                    loaiTaiKhoan = "ThuNgan";
                else if (chucVuNormalized == "kỹ thuật" || chucVuNormalized == "ky thuat")
                    loaiTaiKhoan = "KyThuat";
                else if (chucVuNormalized == "bán hàng" || chucVuNormalized == "ban hang")
                    loaiTaiKhoan = "BanHang";

                // ✅ QUAN TRỌNG: Query phải đúng
                string query = @"INSERT INTO TaiKhoan 
                        (MaTaiKhoan, TenDangNhap, Password, LoaiTaiKhoan, TrangThaiTaiKhoan, MaNV, NgayTao, NgayCapNhat)
                        VALUES 
                        (@MaTaiKhoan, @TenDangNhap, @Password, @LoaiTaiKhoan, @TrangThaiTaiKhoan, @MaNV, @NgayTao, @NgayCapNhat)";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@MaTaiKhoan", maTaiKhoan),
            new SqlParameter("@TenDangNhap", tenDangNhap),
            new SqlParameter("@Password", matKhau),  // Lưu mật khẩu vào cột Password
            new SqlParameter("@LoaiTaiKhoan", loaiTaiKhoan),  // "QuanLy" hoặc "NhanVien"
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
                System.Diagnostics.Debug.WriteLine($"ChucVu: {chucVu}");
                System.Diagnostics.Debug.WriteLine($"LoaiTaiKhoan: {loaiTaiKhoan}");

                System.Console.WriteLine("=== BẮT ĐẦU TẠO TÀI KHOẢN ===");
                System.Console.WriteLine($"Mã NV: {maNV} | Email: {email}");

                int result = DataProvider.ExecuteNonQuery(query, parameters);

                if (result > 0)
                {
                    System.Diagnostics.Debug.WriteLine("✅ Tạo tài khoản thành công!");
                    System.Console.WriteLine($"✅ INSERT thành công! Rows affected: {result}");
                    return true;
                }
                else
                {
                    errorMessage = "Không thể thêm tài khoản vào database! (ExecuteNonQuery trả về 0)";
                    System.Console.WriteLine($"❌ INSERT thất bại! Result: {result}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi tạo tài khoản: " + ex.Message;
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                System.Console.WriteLine($"❌ EXCEPTION khi tạo tài khoản: {ex.Message}");
                System.Console.WriteLine($"❌ StackTrace: {ex.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// Tạo tài khoản cho nhân viên mới - TenDangNhap = Email, Password = Random
        /// </summary>
        public bool CreateAccountForEmployee(string maNV, string email, string sdt, out string errorMessage, out string generatedPassword)
        {
            errorMessage = "";
            generatedPassword = "";

            try
            {
                // Tạo mã tài khoản tự động
                string maTaiKhoan = GenerateMaTaiKhoan();

                // Tên đăng nhập = email
                string tenDangNhap = email;

                // Mật khẩu ngẫu nhiên
                string matKhau = GenerateRandomPassword();
                generatedPassword = matKhau; // Trả về mật khẩu đã tạo để hiển thị

                // ✅ QUAN TRỌNG: Query phải đúng
                string query = @"INSERT INTO TaiKhoan 
                        (MaTaiKhoan, TenDangNhap, Password, LoaiTaiKhoan, TrangThaiTaiKhoan, MaNV, NgayTao, NgayCapNhat)
                        VALUES 
                        (@MaTaiKhoan, @TenDangNhap, @Password, @LoaiTaiKhoan, @TrangThaiTaiKhoan, @MaNV, @NgayTao, @NgayCapNhat)";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@MaTaiKhoan", maTaiKhoan),
            new SqlParameter("@TenDangNhap", tenDangNhap),
            new SqlParameter("@Password", matKhau),  // Lưu mật khẩu vào cột Password
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

                System.Console.WriteLine("=== BẮT ĐẦU TẠO TÀI KHOẢN ===");
                System.Console.WriteLine($"Mã NV: {maNV} | Email: {email} | SĐT: {sdt}");

                int result = DataProvider.ExecuteNonQuery(query, parameters);

                if (result > 0)
                {
                    System.Diagnostics.Debug.WriteLine("✅ Tạo tài khoản thành công!");
                    System.Console.WriteLine($"✅ INSERT thành công! Rows affected: {result}");
                    return true;
                }
                else
                {
                    errorMessage = "Không thể thêm tài khoản vào database! (ExecuteNonQuery trả về 0)";
                    System.Console.WriteLine($"❌ INSERT thất bại! Result: {result}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi tạo tài khoản: " + ex.Message;
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                System.Console.WriteLine($"❌ EXCEPTION khi tạo tài khoản: {ex.Message}");
                System.Console.WriteLine($"❌ StackTrace: {ex.StackTrace}");
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