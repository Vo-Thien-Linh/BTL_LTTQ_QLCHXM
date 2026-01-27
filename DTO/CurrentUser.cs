using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CurrentUser
    {
        public static string MaTaiKhoan { get; set; }
        public static string LoaiTaiKhoan { get; set; }
        public static string TrangThaiTaiKhoan { get; set; }
        public static string MaKH { get; set; }
        public static string MaNV { get; set; }
        public static string HoTen { get; set; }
        public static string SoDienThoai { get; set; }
        public static string Email { get; set; }
        public static string ChucVu { get; set; }


        /// <summary>
        /// Kiểm tra người dùng có phải Admin không
        /// </summary>
        public static bool IsAdmin()
        {
            return LoaiTaiKhoan == "NhanVien" && ChucVu == "Admin";
        }

        /// <summary>
        /// Kiểm tra người dùng có phải Quản lý không
        /// </summary>
        public static bool IsQuanLy()
        {
            return LoaiTaiKhoan == "NhanVien" && ChucVu == "Quản lý";
        }

        /// <summary>
        /// Kiểm tra người dùng có phải Nhân viên không
        /// </summary>
        public static bool IsNhanVien()
        {
            return LoaiTaiKhoan == "NhanVien";
        }

        /// <summary>
        /// Kiểm tra người dùng có phải Khách hàng không
        /// </summary>
        public static bool IsKhachHang()
        {
            return LoaiTaiKhoan == "KhachHang";
        }

        /// <summary>
        /// Xóa thông tin khi đăng xuất
        /// </summary>
        public static void Clear()
        {
            MaTaiKhoan = null;
            LoaiTaiKhoan = null;
            TrangThaiTaiKhoan = null;
            MaKH = null;
            MaNV = null;
            HoTen = null;
            SoDienThoai = null;
            Email = null;
            ChucVu = null;
        }
    }
}
