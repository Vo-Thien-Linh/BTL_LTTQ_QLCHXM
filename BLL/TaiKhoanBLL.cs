using System;
using DAL;
using System.Data;
using DTO;

namespace BLL
{
    public class TaiKhoanBLL
    {
        private TaiKhoanDAL taiKhoanDAL;

        public TaiKhoanBLL()
        {
            taiKhoanDAL = new TaiKhoanDAL();
        }

        /// <summary>
        /// Đăng nhập bằng Email + Mật khẩu (static method để gọi từ LoginForm)
        /// </summary>
        public static bool DangNhapBangEmail(string email, string matKhau)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== BẮT ĐẦU ĐĂNG NHẬP BẰNG EMAIL ===");
                System.Diagnostics.Debug.WriteLine($"Email nhận được: [{email}]");
                System.Diagnostics.Debug.WriteLine($"Mật khẩu nhận được: [{matKhau}]");

                DataRow row = TaiKhoanDAL.DangNhapBangEmail(email, matKhau);

                if (row != null)
                {
                    System.Diagnostics.Debug.WriteLine("✅ TaiKhoanDAL.DangNhapBangEmail trả về row != null");

                    // Lưu thông tin vào CurrentUser
                    CurrentUser.MaTaiKhoan = row["MaTaiKhoan"].ToString();
                    CurrentUser.LoaiTaiKhoan = row["LoaiTaiKhoan"].ToString();
                    CurrentUser.TrangThaiTaiKhoan = row["TrangThaiTaiKhoan"].ToString();
                    CurrentUser.HoTen = row["HoTen"].ToString();
                    CurrentUser.ChucVu = row["ChucVu"]?.ToString() ?? "";

                    System.Diagnostics.Debug.WriteLine($"CurrentUser.HoTen: {CurrentUser.HoTen}");

                    if (row["MaNV"] != DBNull.Value)
                    {
                        CurrentUser.MaNV = row["MaNV"].ToString();
                        CurrentUser.SoDienThoai = row["SdtNV"]?.ToString() ?? "";
                    }
                    else if (row["MaKH"] != DBNull.Value)
                    {
                        CurrentUser.MaKH = row["MaKH"].ToString();
                        CurrentUser.SoDienThoai = row["SdtKH"]?.ToString() ?? "";
                    }

                    System.Diagnostics.Debug.WriteLine("✅ ĐĂNG NHẬP THÀNH CÔNG");
                    return true;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("❌ TaiKhoanDAL.DangNhapBangEmail trả về NULL");
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ LỖI BLL: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// Đăng nhập (static method để gọi từ LoginForm)
        /// </summary>
        public static bool DangNhap(string soDienThoai, string matKhau)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== BẮT ĐẦU ĐĂNG NHẬP ===");
                System.Diagnostics.Debug.WriteLine($"SĐT nhận được: [{soDienThoai}]");
                System.Diagnostics.Debug.WriteLine($"Mật khẩu nhận được: [{matKhau}]");

                DataRow row = TaiKhoanDAL.DangNhap(soDienThoai, matKhau);

                if (row != null)
                {
                    System.Diagnostics.Debug.WriteLine("✅ TaiKhoanDAL.DangNhap trả về row != null");

                    // Lưu thông tin vào CurrentUser
                    CurrentUser.MaTaiKhoan = row["MaTaiKhoan"].ToString();
                    CurrentUser.LoaiTaiKhoan = row["LoaiTaiKhoan"].ToString();
                    CurrentUser.TrangThaiTaiKhoan = row["TrangThaiTaiKhoan"].ToString();
                    CurrentUser.HoTen = row["HoTen"].ToString();
                    CurrentUser.ChucVu = row["ChucVu"]?.ToString() ?? "";

                    System.Diagnostics.Debug.WriteLine($"CurrentUser.HoTen: {CurrentUser.HoTen}");

                    if (row["MaNV"] != DBNull.Value)
                    {
                        CurrentUser.MaNV = row["MaNV"].ToString();
                        CurrentUser.SoDienThoai = row["SdtNV"]?.ToString() ?? "";
                    }
                    else if (row["MaKH"] != DBNull.Value)
                    {
                        CurrentUser.MaKH = row["MaKH"].ToString();
                        CurrentUser.SoDienThoai = row["SdtKH"]?.ToString() ?? "";
                    }

                    System.Diagnostics.Debug.WriteLine("✅ ĐĂNG NHẬP THÀNH CÔNG");
                    return true;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("❌ TaiKhoanDAL.DangNhap trả về NULL");
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ LỖI BLL: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// Tạo tài khoản cho nhân viên mới - TenDangNhap = Email, Password = SĐT
        /// </summary>
        public bool CreateAccountForEmployee(string maNV, string email, string sdt, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrEmpty(email))
            {
                errorMessage = "Email không được để trống để tạo tài khoản!";
                System.Console.WriteLine($"❌ Không có Email cho nhân viên {maNV}");
                return false;
            }

            if (string.IsNullOrEmpty(sdt))
            {
                errorMessage = "Số điện thoại không được để trống để tạo mật khẩu!";
                System.Console.WriteLine($"❌ Không có SĐT cho nhân viên {maNV}");
                return false;
            }

            // Kiểm tra nhân viên đã có tài khoản chưa
            if (taiKhoanDAL.CheckEmployeeHasAccount(maNV))
            {
                errorMessage = "Nhân viên này đã có tài khoản!";
                System.Console.WriteLine($"❌ Nhân viên {maNV} đã có tài khoản rồi!");
                return false;
            }

            // Kiểm tra tên đăng nhập đã tồn tại chưa (kiểm tra Email)
            if (taiKhoanDAL.CheckTenDangNhapExists(email))
            {
                errorMessage = "Email này đã được dùng làm tài khoản!";
                System.Console.WriteLine($"❌ Email {email} đã được sử dụng làm tài khoản!");
                return false;
            }

            string generatedPassword;
            return taiKhoanDAL.CreateAccountForEmployee(maNV, email, sdt, out errorMessage, out generatedPassword);
        }

        /// <summary>
        /// Tạo tài khoản với mật khẩu nhập từ admin
        /// </summary>
        public bool CreateAccountForEmployeeWithPassword(string maNV, string email, string sdt, string password, string chucVu, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrEmpty(email))
            {
                errorMessage = "Email không được để trống để tạo tài khoản!";
                System.Console.WriteLine($"❌ Không có Email cho nhân viên {maNV}");
                return false;
            }

            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                errorMessage = "Mật khẩu phải có ít nhất 6 ký tự!";
                System.Console.WriteLine($"❌ Mật khẩu không hợp lệ cho nhân viên {maNV}");
                return false;
            }

            // Kiểm tra nhân viên đã có tài khoản chưa
            if (taiKhoanDAL.CheckEmployeeHasAccount(maNV))
            {
                errorMessage = "Nhân viên này đã có tài khoản!";
                System.Console.WriteLine($"❌ Nhân viên {maNV} đã có tài khoản rồi!");
                return false;
            }

            // Kiểm tra tên đăng nhập đã tồn tại chưa (kiểm tra Email)
            if (taiKhoanDAL.CheckTenDangNhapExists(email))
            {
                errorMessage = "Email này đã được dùng làm tài khoản!";
                System.Console.WriteLine($"❌ Email {email} đã được sử dụng làm tài khoản!");
                return false;
            }

            return taiKhoanDAL.CreateAccountForEmployeeWithPassword(maNV, email, password, chucVu, out errorMessage);
        }

        /// <summary>
        /// Tạo tài khoản với password trả về
        /// </summary>
        public bool CreateAccountForEmployee(string maNV, string email, string sdt, out string errorMessage, out string generatedPassword)
        {
            errorMessage = "";
            generatedPassword = "";

            if (string.IsNullOrEmpty(email))
            {
                errorMessage = "Email không được để trống để tạo tài khoản!";
                System.Console.WriteLine($"❌ Không có Email cho nhân viên {maNV}");
                return false;
            }

            if (string.IsNullOrEmpty(sdt))
            {
                errorMessage = "Số điện thoại không được để trống!";
                System.Console.WriteLine($"❌ Không có SĐT cho nhân viên {maNV}");
                return false;
            }

            // Kiểm tra nhân viên đã có tài khoản chưa
            if (taiKhoanDAL.CheckEmployeeHasAccount(maNV))
            {
                errorMessage = "Nhân viên này đã có tài khoản!";
                System.Console.WriteLine($"❌ Nhân viên {maNV} đã có tài khoản rồi!");
                return false;
            }

            // Kiểm tra tên đăng nhập đã tồn tại chưa (kiểm tra Email)
            if (taiKhoanDAL.CheckTenDangNhapExists(email))
            {
                errorMessage = "Email này đã được dùng làm tài khoản!";
                System.Console.WriteLine($"❌ Email {email} đã được sử dụng làm tài khoản!");
                return false;
            }

            return taiKhoanDAL.CreateAccountForEmployee(maNV, email, sdt, out errorMessage, out generatedPassword);
        }

        /// <summary>
        /// Xóa tài khoản khi xóa nhân viên
        /// </summary>
        public bool DeleteAccountByMaNV(string maNV)
        {
            return taiKhoanDAL.DeleteAccountByMaNV(maNV);
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        public bool UpdatePassword(string maTaiKhoan, string oldPassword, string newPassword, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrEmpty(newPassword))
            {
                errorMessage = "Mật khẩu mới không được để trống!";
                return false;
            }

            if (newPassword.Length < 6)
            {
                errorMessage = "Mật khẩu phải có ít nhất 6 ký tự!";
                return false;
            }

            // Có thể thêm validation kiểm tra mật khẩu cũ ở đây nếu cần
            // ...

            return taiKhoanDAL.UpdatePassword(maTaiKhoan, newPassword, out errorMessage);
        }

        /// <summary>
        /// Kiểm tra nhân viên đã có tài khoản chưa
        /// </summary>
        public bool CheckEmployeeHasAccount(string maNV)
        {
            return taiKhoanDAL.CheckEmployeeHasAccount(maNV);
        }

        /// <summary>
        /// Lấy tài khoản theo mã nhân viên
        /// </summary>
        public DataTable GetAccountByMaNV(string maNV)
        {
            return taiKhoanDAL.GetAccountByMaNV(maNV);
        }

        /// <summary>
        /// Khóa/Mở khóa tài khoản
        /// </summary>
        public bool UpdateAccountStatus(string maTaiKhoan, string trangThai, out string errorMessage)
        {
            errorMessage = "";

            if (trangThai != "Hoạt động" && trangThai != "Khóa")
            {
                errorMessage = "Trạng thái không hợp lệ!";
                return false;
            }

            return taiKhoanDAL.UpdateAccountStatus(maTaiKhoan, trangThai, out errorMessage);
        }

        /// <summary>
        /// Lấy tất cả tài khoản nhân viên
        /// </summary>
        public DataTable GetAllEmployeeAccounts()
        {
            return taiKhoanDAL.GetAllEmployeeAccounts();
        }
    }
}