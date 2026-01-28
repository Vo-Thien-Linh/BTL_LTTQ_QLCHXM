using DTO;
using System;

namespace BLL
{
    /// <summary>
    /// Quản lý phân quyền theo chức vụ
    /// Các chức vụ:
    /// - Quản lý: Full quyền (Dashboard, Quản lý NV, Quản lý KH-Sửa, Quản lý Xe-Sửa, Quản lý PT-Sửa, Bán hàng, Cho thuê, Bảo trì-Xem, Duyệt đơn, Cài đặt)
    /// - Thu ngân: Quản lý KH-Sửa, Bán hàng, Cho thuê
    /// - Bán hàng: Quản lý KH-Xem, Quản lý Xe-Xem, Quản lý PT-Xem, Bán hàng, Cho thuê
    /// - Kỹ thuật: Quản lý Xe-Xem, Quản lý PT-Xem, Bảo trì-Sửa
    /// </summary>
    public static class PermissionManager
    {
        /// <summary>
        /// Quản lý và Thu ngân có thể xem Dashboard
        /// </summary>
        public static bool CanViewDashboard()
        {
            return CurrentUser.ChucVu == "Quản lý" || CurrentUser.ChucVu == "Thu ngân";
        }

        /// <summary>
        /// Chỉ Quản lý có thể quản lý nhân viên
        /// </summary>
        public static bool CanViewNhanVien()
        {
            return CurrentUser.ChucVu == "Quản lý";
        }

        /// <summary>
        /// Quản lý-Sửa, Thu ngân-Sửa, Bán hàng-Xem có thể xem khách hàng
        /// </summary>
        public static bool CanViewKhachHang()
        {
            return CurrentUser.ChucVu == "Quản lý" || 
                   CurrentUser.ChucVu == "Thu ngân" || 
                   CurrentUser.ChucVu == "Bán hàng";
        }

        /// <summary>
        /// Kiểm tra có quyền sửa khách hàng (Chỉ Quản lý)
        /// </summary>
        public static bool CanEditKhachHang()
        {
            return CurrentUser.ChucVu == "Quản lý";
        }

        /// <summary>
        /// Quản lý-Sửa, Bán hàng-Xem, Kỹ thuật-Xem có thể xem sản phẩm/xe
        /// </summary>
        public static bool CanViewSanPham()
        {
            return CurrentUser.ChucVu == "Quản lý" || 
                   CurrentUser.ChucVu == "Bán hàng" || 
                   CurrentUser.ChucVu == "Kỹ thuật";
        }

        /// <summary>
        /// Kiểm tra có quyền sửa sản phẩm/xe (chỉ Quản lý)
        /// </summary>
        public static bool CanEditSanPham()
        {
            return CurrentUser.ChucVu == "Quản lý";
        }

        /// <summary>
        /// Quản lý, Thu ngân, Bán hàng có thể bán hàng
        /// </summary>
        public static bool CanViewBanHang()
        {
            return CurrentUser.ChucVu == "Quản lý" || 
                   CurrentUser.ChucVu == "Thu ngân" || 
                   CurrentUser.ChucVu == "Bán hàng";
        }

        /// <summary>
        /// Quản lý, Bán hàng có thể cho thuê (Thu ngân không)
        /// </summary>
        public static bool CanViewChoThue()
        {
            return CurrentUser.ChucVu == "Quản lý" || 
                   CurrentUser.ChucVu == "Bán hàng";
        }

        /// <summary>
        /// Quản lý-Xem, Kỹ thuật-Sửa có thể xem bảo trì/xử lý
        /// </summary>
        public static bool CanViewXuLy()
        {
            return CurrentUser.ChucVu == "Quản lý" || CurrentUser.ChucVu == "Kỹ thuật";
        }

        /// <summary>
        /// Kiểm tra có quyền sửa bảo trì (Kỹ thuật hoặc Quản lý)
        /// </summary>
        public static bool CanEditBaoTri()
        {
            return CurrentUser.ChucVu == "Kỹ thuật" || CurrentUser.ChucVu == "Quản lý";
        }

        /// <summary>
        /// Kiểm tra có phải Admin không (chỉ Admin mới có quyền quản lý khuyến mãi)
        /// </summary>
        public static bool IsAdmin()
        {
            return CurrentUser.ChucVu == "Admin" || CurrentUser.ChucVu == "Quản lý";
        }

        /// <summary>
        /// Bảo trì giống xử lý
        /// </summary>
        public static bool CanViewBaoTri()
        {
            return CanViewXuLy();
        }

        /// <summary>
        /// Chỉ Quản lý có thể duyệt đơn
        /// </summary>
        public static bool CanViewDuyetDon()
        {
            return CurrentUser.ChucVu == "Quản lý";
        }

        /// <summary>
        /// Chỉ Quản lý có thể xem cài đặt
        /// </summary>
        public static bool CanViewSettings()
        {
            return CurrentUser.ChucVu == "Quản lý";
        }

        /// <summary>
        /// Lấy tên vai trò để hiển thị
        /// </summary>
        public static string GetRoleName()
        {
            if (CurrentUser.ChucVu == "Quản lý") return "Quản lý";
            if (CurrentUser.ChucVu == "Thu ngân") return "Thu ngân";
            if (CurrentUser.ChucVu == "Kỹ thuật") return "Kỹ thuật viên";
            if (CurrentUser.ChucVu == "Bán hàng") return "Nhân viên bán hàng";
            if (CurrentUser.IsKhachHang()) return "Khách hàng";
            return "Nhân viên";
        }
    }
}
