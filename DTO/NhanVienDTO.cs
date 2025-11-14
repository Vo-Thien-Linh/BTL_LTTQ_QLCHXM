using System;

namespace DTO
{
    public class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTenNV { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string ChucVu { get; set; }
        public decimal? LuongCoBan { get; set; }
        public string TinhTrangLamViec { get; set; }
        public string CCCD { get; set; }
        public string TrinhDoHocVan { get; set; }
        public byte[] AnhNhanVien { get; set; }
        public DateTime? NgayVaoLam { get; set; }

        public NhanVien()
        {
        }

        public NhanVien(string maNV, string hoTenNV, DateTime ngaySinh, string gioiTinh,
            string sdt, string email, string diaChi, string chucVu, decimal? luongCoBan,
            string tinhTrangLamViec, string cccd, string trinhDoHocVan, byte[] anhNhanVien, DateTime? ngayVaoLam)
        {
            MaNV = maNV;
            HoTenNV = hoTenNV;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            Sdt = sdt;
            Email = email;
            DiaChi = diaChi;
            ChucVu = chucVu;
            LuongCoBan = luongCoBan;
            TinhTrangLamViec = tinhTrangLamViec;
            CCCD = cccd;
            TrinhDoHocVan = trinhDoHocVan;
            AnhNhanVien = anhNhanVien;
            NgayVaoLam = ngayVaoLam;
        }
    }
}

