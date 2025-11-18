using System;

namespace DTO
{
    public class KhachHang
    {
        public string MaKH { get; set; }
        public string HoTenKH { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }

        public KhachHang()
        {
            NgayTao = DateTime.Now;
            NgayCapNhat = DateTime.Now;
        }

        public KhachHang(string maKH, string hoTenKH, DateTime? ngaySinh, string gioiTinh,
            string sdt, string email, string diaChi)
        {
            MaKH = maKH;
            HoTenKH = hoTenKH;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            Sdt = sdt;
            Email = email;
            DiaChi = diaChi;
            NgayTao = DateTime.Now;
            NgayCapNhat = DateTime.Now;
        }
    }
}