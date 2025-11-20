using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// DTO cho bảng TaiKhoan
    /// Quản lý thông tin tài khoản của Khách hàng và Nhân viên
    /// </summary>
    public class TaiKhoanDTO
    {
        // ========== PROPERTIES CHÍNH (MAPPING VỚI DATABASE) ==========

        /// <summary>
        /// Mã tài khoản (Primary Key) - Format: TK00000001
        /// </summary>
        public string MaTaiKhoan { get; set; }

        /// <summary>
        /// Tên đăng nhập (có thể NULL - dùng SĐT thay thế)
        /// </summary>
        public string TenDangNhap { get; set; }

        /// <summary>
        /// Mật khẩu (đã mã hóa hoặc plain text)
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Loại tài khoản: "KhachHang" hoặc "NhanVien"
        /// </summary>
        public string LoaiTaiKhoan { get; set; }

        /// <summary>
        /// Trạng thái tài khoản: "Hoạt động", "Khóa", "Chờ kích hoạt"
        /// </summary>
        public string TrangThaiTaiKhoan { get; set; }

        /// <summary>
        /// Mã khách hàng (nếu là tài khoản khách hàng)
        /// </summary>
        public string MaKH { get; set; }

        /// <summary>
        /// Mã nhân viên (nếu là tài khoản nhân viên)
        /// </summary>
        public string MaNV { get; set; }

        /// <summary>
        /// Ngày tạo tài khoản
        /// </summary>
        public DateTime NgayTao { get; set; }

        /// <summary>
        /// Ngày cập nhật cuối cùng
        /// </summary>
        public DateTime NgayCapNhat { get; set; }

        // ========== PROPERTIES BỔ SUNG (KHÔNG LƯU DB - CHỈ ĐỂ HIỂN THỊ) ==========

        /// <summary>
        /// Họ tên (lấy từ KhachHang.HoTenKH hoặc NhanVien.HoTenNV)
        /// </summary>
        public string HoTen { get; set; }

        /// <summary>
        /// Số điện thoại (lấy từ KhachHang.Sdt hoặc NhanVien.Sdt)
        /// </summary>
        public string SoDienThoai { get; set; }

        /// <summary>
        /// Email (lấy từ KhachHang.Email hoặc NhanVien.Email)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Chức vụ (chỉ áp dụng cho Nhân viên)
        /// Giá trị: "Admin", "Quản lý", "Nhân viên"
        /// </summary>
        public string ChucVu { get; set; }

        /// <summary>
        /// Ngày đăng nhập cuối cùng
        /// </summary>
        public DateTime? NgayDangNhapCuoi { get; set; }

        // ========== CONSTRUCTORS ==========

        /// <summary>
        /// Constructor mặc định
        /// </summary>
        public TaiKhoanDTO()
        {
            TrangThaiTaiKhoan = "Hoạt động"; // Mặc định là hoạt động
            NgayTao = DateTime.Now;
            NgayCapNhat = DateTime.Now;
        }

        /// <summary>
        /// Constructor đầy đủ cho tài khoản Nhân viên
        /// </summary>
        public TaiKhoanDTO(string maTaiKhoan, string password, string loaiTaiKhoan, 
            string trangThaiTaiKhoan, string maNV)
        {
            MaTaiKhoan = maTaiKhoan;
            Password = password;
            LoaiTaiKhoan = loaiTaiKhoan;
            TrangThaiTaiKhoan = trangThaiTaiKhoan;
            MaNV = maNV;
            NgayTao = DateTime.Now;
            NgayCapNhat = DateTime.Now;
        }

        /// <summary>
        /// Constructor đầy đủ cho tài khoản Khách hàng
        /// </summary>
        public TaiKhoanDTO(string maTaiKhoan, string password, string loaiTaiKhoan, 
            string trangThaiTaiKhoan, string maKH, bool isKhachHang)
        {
            MaTaiKhoan = maTaiKhoan;
            Password = password;
            LoaiTaiKhoan = loaiTaiKhoan;
            TrangThaiTaiKhoan = trangThaiTaiKhoan;
            MaKH = maKH;
            NgayTao = DateTime.Now;
            NgayCapNhat = DateTime.Now;
        }

        /// <summary>
        /// Constructor đầy đủ với thông tin bổ sung
        /// </summary>
        public TaiKhoanDTO(string maTaiKhoan, string tenDangNhap, string password, 
            string loaiTaiKhoan, string trangThaiTaiKhoan, string maKH, string maNV, 
            DateTime ngayTao, DateTime ngayCapNhat)
        {
            MaTaiKhoan = maTaiKhoan;
            TenDangNhap = tenDangNhap;
            Password = password;
            LoaiTaiKhoan = loaiTaiKhoan;
            TrangThaiTaiKhoan = trangThaiTaiKhoan;
            MaKH = maKH;
            MaNV = maNV;
            NgayTao = ngayTao;
            NgayCapNhat = ngayCapNhat;
        }

        // ========== HELPER METHODS ==========

        /// <summary>
        /// Kiểm tra tài khoản có phải là Admin không
        /// </summary>
        public bool IsAdmin()
        {
            return LoaiTaiKhoan == "NhanVien" && ChucVu == "Admin";
        }

        /// <summary>
        /// Kiểm tra tài khoản có phải là Quản lý không
        /// </summary>
        public bool IsQuanLy()
        {
            return LoaiTaiKhoan == "NhanVien" && ChucVu == "Quản lý";
        }

        /// <summary>
        /// Kiểm tra tài khoản có phải là Nhân viên không
        /// </summary>
        public bool IsNhanVien()
        {
            return LoaiTaiKhoan == "NhanVien" && ChucVu == "Nhân viên";
        }

        /// <summary>
        /// Kiểm tra tài khoản có phải là Khách hàng không
        /// </summary>
        public bool IsKhachHang()
        {
            return LoaiTaiKhoan == "KhachHang";
        }

        /// <summary>
        /// Kiểm tra tài khoản có đang hoạt động không
        /// </summary>
        public bool IsActive()
        {
            return TrangThaiTaiKhoan == "Hoạt động";
        }

        /// <summary>
        /// Kiểm tra tài khoản có bị khóa không
        /// </summary>
        public bool IsLocked()
        {
            return TrangThaiTaiKhoan == "Khóa";
        }

        /// <summary>
        /// Kiểm tra tài khoản có quyền quản trị không (Admin hoặc Quản lý)
        /// </summary>
        public bool HasAdminPrivilege()
        {
            return IsAdmin() || IsQuanLy();
        }

        /// <summary>
        /// Lấy tên hiển thị của tài khoản
        /// </summary>
        public string GetDisplayName()
        {
            if (!string.IsNullOrWhiteSpace(HoTen))
                return HoTen;

            if (!string.IsNullOrWhiteSpace(TenDangNhap))
                return TenDangNhap;

            return MaTaiKhoan;
        }

        /// <summary>
        /// Lấy mô tả vai trò
        /// </summary>
        public string GetRoleDescription()
        {
            if (LoaiTaiKhoan == "KhachHang")
                return "Khách hàng";

            if (!string.IsNullOrWhiteSpace(ChucVu))
                return ChucVu;

            return "Nhân viên";
        }

        /// <summary>
        /// Validate dữ liệu tài khoản
        /// </summary>
        public bool Validate(out string errorMessage)
        {
            errorMessage = string.Empty;

            // Kiểm tra mã tài khoản
            if (string.IsNullOrWhiteSpace(MaTaiKhoan))
            {
                errorMessage = "Mã tài khoản không được để trống!";
                return false;
            }

            // Kiểm tra mật khẩu
            if (string.IsNullOrWhiteSpace(Password))
            {
                errorMessage = "Mật khẩu không được để trống!";
                return false;
            }

            if (Password.Length < 6)
            {
                errorMessage = "Mật khẩu phải có ít nhất 6 ký tự!";
                return false;
            }

            // Kiểm tra loại tài khoản
            if (LoaiTaiKhoan != "KhachHang" && LoaiTaiKhoan != "NhanVien")
            {
                errorMessage = "Loại tài khoản không hợp lệ!";
                return false;
            }

            // Kiểm tra phải có MaKH hoặc MaNV
            if (string.IsNullOrWhiteSpace(MaKH) && string.IsNullOrWhiteSpace(MaNV))
            {
                errorMessage = "Tài khoản phải liên kết với Khách hàng hoặc Nhân viên!";
                return false;
            }

            // Kiểm tra không được có cả MaKH và MaNV
            if (!string.IsNullOrWhiteSpace(MaKH) && !string.IsNullOrWhiteSpace(MaNV))
            {
                errorMessage = "Tài khoản không thể vừa là Khách hàng vừa là Nhân viên!";
                return false;
            }

            // Kiểm tra tính nhất quán loại tài khoản
            if (LoaiTaiKhoan == "KhachHang" && string.IsNullOrWhiteSpace(MaKH))
            {
                errorMessage = "Tài khoản Khách hàng phải có MaKH!";
                return false;
            }

            if (LoaiTaiKhoan == "NhanVien" && string.IsNullOrWhiteSpace(MaNV))
            {
                errorMessage = "Tài khoản Nhân viên phải có MaNV!";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Override ToString để hiển thị thông tin tài khoản
        /// </summary>
        public override string ToString()
        {
            return $"{MaTaiKhoan} - {GetDisplayName()} ({GetRoleDescription()}) - {TrangThaiTaiKhoan}";
        }
    }
}
