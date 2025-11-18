using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ThongTinGiaXeDAL
    {
        /// <summary>
        /// Lấy thông tin xe kèm giá cho khách hàng (chỉ xe sẵn sàng)
        /// </summary>
        public DataTable GetXeForCustomer(string phanLoai = null)
        {
            string query = @"
                SELECT 
                    xm.ID_Xe,
                    xm.BienSo,
                    hx.TenHang,
                    dx.TenDong,
                    ms.TenMau,
                    lx.NamSX,
                    dx.PhanKhoi,
                    dx.LoaiXe,
                    xm.KmDaChay,
                    xm.ThongTinXang,
                    xm.AnhXeXeBan,
                    xm.TrangThai,
                    tg.PhanLoai,
                    tg.GiaBan,
                    tg.GiaThueNgay,
                    tg.TienDatCoc,
                    tg.GhiChu
                FROM XeMay xm
                INNER JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                INNER JOIN ThongTinGiaXe tg ON xm.ID_Xe = tg.ID_Xe
                WHERE xm.TrangThai = N'Sẵn sàng'";

            SqlParameter[] parameters = null;

            if (!string.IsNullOrEmpty(phanLoai))
            {
                query += " AND tg.PhanLoai = @PhanLoai";
                parameters = new SqlParameter[] { new SqlParameter("@PhanLoai", phanLoai) };
            }

            query += " ORDER BY tg.NgayCapNhat DESC, xm.ID_Xe";

            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Tìm kiếm xe theo từ khóa
        /// </summary>
        public DataTable SearchXeForCustomer(string keyword, string phanLoai = null)
        {
            string query = @"
                SELECT 
                    xm.ID_Xe,
                    xm.BienSo,
                    hx.TenHang,
                    dx.TenDong,
                    ms.TenMau,
                    lx.NamSX,
                    dx.PhanKhoi,
                    dx.LoaiXe,
                    xm.KmDaChay,
                    xm.ThongTinXang,
                    xm.AnhXeXeBan,
                    xm.TrangThai,
                    tg.PhanLoai,
                    tg.GiaBan,
                    tg.GiaThueNgay,
                    tg.TienDatCoc,
                    tg.GhiChu
                FROM XeMay xm
                INNER JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                INNER JOIN ThongTinGiaXe tg ON xm.ID_Xe = tg.ID_Xe
                WHERE xm.TrangThai = N'Sẵn sàng'
                AND (hx.TenHang LIKE @Keyword 
                    OR dx.TenDong LIKE @Keyword 
                    OR ms.TenMau LIKE @Keyword
                    OR xm.BienSo LIKE @Keyword
                    OR dx.LoaiXe LIKE @Keyword)";

            SqlParameter[] parameters;

            if (!string.IsNullOrEmpty(phanLoai))
            {
                query += " AND tg.PhanLoai = @PhanLoai";
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Keyword", "%" + keyword + "%"),
                    new SqlParameter("@PhanLoai", phanLoai)
                };
            }
            else
            {
                parameters = new SqlParameter[] { new SqlParameter("@Keyword", "%" + keyword + "%") };
            }

            query += " ORDER BY tg.NgayCapNhat DESC, xm.ID_Xe";

            return DataProvider.ExecuteQuery(query, parameters);
        }
    }
}