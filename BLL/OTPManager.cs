using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OTPManager
    {
        public static string Email { get; set; }
        public static string MaOTP { get; set; }
        public static DateTime ThoiGianGui { get; set; } = DateTime.MinValue;

        public static bool KiemTra(string maNhap)
        {
            if (string.IsNullOrEmpty(MaOTP)) return false;
            if (DateTime.Now > ThoiGianGui.AddMinutes(5)) return false;
            return MaOTP == maNhap.Trim();
        }

        public static void Clear()
        {
            Email = MaOTP = null;
            ThoiGianGui = DateTime.MinValue;
        }
    }
}
