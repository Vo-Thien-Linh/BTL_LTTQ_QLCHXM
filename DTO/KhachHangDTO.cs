using System;

namespace DTO
{
    public class KhachHangDTO
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
        
        // Thông tin giấy tờ để mua xe
        public string SoCCCD { get; set; }
        public string LoaiGiayTo { get; set; }
        public byte[] AnhGiayTo { get; set; }

        public KhachHangDTO()
        {
            NgayTao = DateTime.Now;
            NgayCapNhat = DateTime.Now;
        }

        public KhachHangDTO(string maKH, string hoTenKH, DateTime? ngaySinh, string gioiTinh,
            string sdt, string email, string diaChi, string soCCCD = null, 
            string loaiGiayTo = null, byte[] anhGiayTo = null)
        {
            MaKH = maKH;
            HoTenKH = hoTenKH;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            Sdt = sdt;
            Email = email;
            DiaChi = diaChi;
            SoCCCD = soCCCD;
            LoaiGiayTo = loaiGiayTo;
            AnhGiayTo = anhGiayTo;
            NgayTao = DateTime.Now;
            NgayCapNhat = DateTime.Now;
        }
    }
}