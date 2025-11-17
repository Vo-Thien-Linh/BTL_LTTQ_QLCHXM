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
        public static string LoaiTaiKhoan { get; set; } // "KhachHang" hoặc "NhanVien"
        public static string HoTen { get; set; }
        public static string MaKH { get; set; }
        public static string MaNV { get; set; }
        public static string ChucVu { get; set; } // Chỉ có NV

        public static void Clear()
        {
            MaTaiKhoan = null;
            LoaiTaiKhoan = null;
            HoTen = null;
            MaKH = null;
            MaNV = null;
            ChucVu = null;
        }
    }
}
