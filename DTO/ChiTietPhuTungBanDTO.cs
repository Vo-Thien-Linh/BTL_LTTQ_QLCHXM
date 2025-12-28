using System;

namespace DTO
{
    /// <summary>
    /// DTO cho bảng ChiTietPhuTungBan - chi tiết phụ tùng bán kèm xe
    /// </summary>
    public class ChiTietPhuTungBanDTO
    {
        public int ID_ChiTiet { get; set; }
        public int MaGDBan { get; set; }
        public string MaPhuTung { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string MaKM { get; set; }
        public decimal SoTienGiam { get; set; }
        public string GhiChu { get; set; }

        // Thông tin bổ sung (không lưu DB)
        public string TenPhuTung { get; set; }
        public string DonViTinh { get; set; }

        public ChiTietPhuTungBanDTO()
        {
            SoLuong = 1;
            SoTienGiam = 0;
        }

        /// <summary>
        /// Tính thành tiền = SoLuong * DonGia
        /// </summary>
        public void TinhThanhTien()
        {
            ThanhTien = SoLuong * DonGia;
        }

        /// <summary>
        /// Tính tiền sau giảm
        /// </summary>
        public decimal TinhTienSauGiam()
        {
            return ThanhTien - SoTienGiam;
        }
    }
}
