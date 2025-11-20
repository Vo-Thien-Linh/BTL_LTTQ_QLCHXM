using DAL;
using DTO;
using System;

namespace BLL
{
    /// <summary>
    /// Quản lý phiên đăng nhập và quyền truy cập
    /// </summary>
    public static class SessionManager
    {
        // Thông tin tài khoản đang đăng nhập
        public static TaiKhoanDTO CurrentUser { get; set; }

        // Kiểm tra trạng thái đăng nhập
        public static bool IsLoggedIn => CurrentUser != null;

        // Kiểm tra quyền Admin
        public static bool IsAdmin => CurrentUser?.ChucVu == "Admin";

        // Kiểm tra quyền Quản lý
        public static bool IsQuanLy => CurrentUser?.ChucVu == "Quản lý";

        // Kiểm tra quyền Nhân viên
        public static bool IsNhanVien => CurrentUser?.ChucVu == "Nhân viên";

        // Kiểm tra có quyền sửa dữ liệu không (Admin hoặc Quản lý)
        public static bool CanModifyData => IsAdmin || IsQuanLy;

        // Kiểm tra có quyền duyệt giao dịch không
        public static bool CanApproveTransaction => IsAdmin || IsQuanLy;

        // Kiểm tra có quyền xem báo cáo không
        public static bool CanViewReport => IsAdmin || IsQuanLy;

        // Thời gian đăng nhập
        public static DateTime? LoginTime { get; set; }

        // Thời gian hoạt động cuối
        public static DateTime? LastActivityTime { get; set; }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        public static void Login(TaiKhoanDTO user)
        {
            CurrentUser = user;
            LoginTime = DateTime.Now;
            LastActivityTime = DateTime.Now;
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        public static void Logout()
        {
            CurrentUser = null;
            LoginTime = null;
            LastActivityTime = null;
        }

        /// <summary>
        /// Cập nhật thời gian hoạt động
        /// </summary>
        public static void UpdateActivity()
        {
            LastActivityTime = DateTime.Now;
        }

        /// <summary>
        /// Kiểm tra phiên có hết hạn không (timeout 30 phút)
        /// </summary>
        public static bool IsSessionExpired(int timeoutMinutes = 30)
        {
            if (!IsLoggedIn || LastActivityTime == null)
                return true;

            TimeSpan idle = DateTime.Now - LastActivityTime.Value;
            return idle.TotalMinutes > timeoutMinutes;
        }

        /// <summary>
        /// Kiểm tra quyền truy cập chức năng
        /// </summary>
        public static bool HasPermission(string feature, string action)
        {
            if (!IsLoggedIn)
                return false;

            // Admin có tất cả quyền
            if (IsAdmin)
                return true;

            // Quản lý
            if (IsQuanLy)
            {
                // Quản lý có hầu hết quyền trừ quản lý tài khoản
                if (feature == "TaiKhoan" && action == "DELETE")
                    return false;
                return true;
            }

            // Nhân viên
            if (IsNhanVien)
            {
                // Nhân viên chỉ có quyền xem và thêm mới (không sửa/xóa)
                switch (feature)
                {
                    case "KhachHang":
                    case "XeMay":
                    case "DonThue":
                    case "DonBan":
                        return action == "VIEW" || action == "ADD";

                    case "BaoTri":
                        return action == "VIEW";

                    default:
                        return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Lấy tên hiển thị của user
        /// </summary>
        public static string GetDisplayName()
        {
            if (!IsLoggedIn)
                return "Guest";

            // Kiểm tra nếu CurrentUser có HoTenNV
            if (CurrentUser != null)
            {
                // Thử lấy từ các property có thể có
                var type = CurrentUser.GetType();
                var hoTenProp = type.GetProperty("HoTenNV");
                if (hoTenProp != null)
                {
                    var hoTen = hoTenProp.GetValue(CurrentUser)?.ToString();
                    if (!string.IsNullOrEmpty(hoTen))
                        return hoTen;
                }
            }

            return CurrentUser?.MaTaiKhoan ?? "Guest";
        }

        /// <summary>
        /// Kiểm tra password có hết hạn không (3 tháng)
        /// </summary>
        public static bool IsPasswordExpired(int monthsValid = 3)
        {
            if (!IsLoggedIn)
                return false;

            // Kiểm tra nếu có NgayCapNhat
            var type = CurrentUser.GetType();
            var ngayCapNhatProp = type.GetProperty("NgayCapNhat");
            if (ngayCapNhatProp != null)
            {
                var ngayCapNhat = ngayCapNhatProp.GetValue(CurrentUser) as DateTime?;
                if (ngayCapNhat.HasValue)
                {
                    TimeSpan timeSinceUpdate = DateTime.Now - ngayCapNhat.Value;
                    return timeSinceUpdate.TotalDays > (monthsValid * 30);
                }
            }

            return false;
        }
    }
}
