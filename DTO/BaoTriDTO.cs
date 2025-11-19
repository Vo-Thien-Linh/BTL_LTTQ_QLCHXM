using System;
using System.Collections.Generic;

namespace DTO
{
    // DTO cho bảng BaoTriXe
    public class BaoTriDTO
    {
        public int ID_BaoTri { get; set; }
        public string ID_Xe { get; set; }
        public string MaTaiKhoan { get; set; }
        public string GhiChuBaoTri { get; set; }

        // Thông tin mở rộng (không có trong DB)
        public string BienSoXe { get; set; }
        public string TenNhanVien { get; set; }
        public string TenHangXe { get; set; }
        public string TenDongXe { get; set; }
        public decimal TongChiPhi { get; set; }
        public DateTime NgayBaoTri { get; set; }

        public BaoTriDTO() { }

        public BaoTriDTO(int idBaoTri, string idXe, string maTaiKhoan, string ghiChu)
        {
            ID_BaoTri = idBaoTri;
            ID_Xe = idXe;
            MaTaiKhoan = maTaiKhoan;
            GhiChuBaoTri = ghiChu;
        }
    }

    // DTO cho bảng ChiTietBaoTri
    public class ChiTietBaoTriDTO
    {
        public int ID_ChiTiet { get; set; }
        public int ID_BaoTri { get; set; }
        public string MaPhuTung { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaSuDung { get; set; }
        public string GhiChu { get; set; }

        // Thông tin mở rộng
        public string TenPhuTung { get; set; }
        public string DonViTinh { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }

        public ChiTietBaoTriDTO() { }

        public ChiTietBaoTriDTO(int idChiTiet, int idBaoTri, string maPhuTung,
            int soLuong, decimal giaSuDung, string ghiChu)
        {
            ID_ChiTiet = idChiTiet;
            ID_BaoTri = idBaoTri;
            MaPhuTung = maPhuTung;
            SoLuong = soLuong;
            GiaSuDung = giaSuDung;
            GhiChu = ghiChu;
            ThanhTien = soLuong * giaSuDung;
        }
    }

    // DTO tổng hợp để hiển thị
    public class BaoTriChiTietDTO
    {
        public BaoTriDTO BaoTri { get; set; }
        public List<ChiTietBaoTriDTO> DanhSachChiTiet { get; set; }

        public BaoTriChiTietDTO()
        {
            BaoTri = new BaoTriDTO();
            DanhSachChiTiet = new List<ChiTietBaoTriDTO>();
        }
    }
}