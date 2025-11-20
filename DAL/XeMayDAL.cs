
﻿using DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class XeMayDAL
    {
        private readonly string _connStr =
      ConfigurationManager.ConnectionStrings["QLCuaHangXeMayConn"].ConnectionString;
        public string GetMaxID_Xe()
        {
            string query = "SELECT TOP 1 ID_Xe FROM XeMay ORDER BY ID_Xe DESC";
            var dt = DataProvider.ExecuteQuery(query);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["ID_Xe"].ToString();
            return null;
        }
        // Lấy tất cả xe
        public DataTable GetAllXeMay()
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
                    xm.GiaMua,
                    xm.KmDaChay,
                    xm.TrangThai,
                    xm.ID_Loai,
                    xm.MaNCC,
                    xm.NgayMua,
                    xm.NgayDangKy,
                    xm.HetHanDangKy,
                    xm.HetHanBaoHiem,
                    xm.ThongTinXang,
                    xm.AnhXe,
                    xm.MucDichSuDung,
                    xm.GiaNhap,
                    xm.SoLuong,
                    xm.SoLuongBanRa
                FROM XeMay xm
                INNER JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                ORDER BY xm.ID_Xe";

            return DataProvider.ExecuteQuery(query);
        }

        // Tìm kiếm xe
        public DataTable SearchXeMay(string keyword, string trangThai)
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
                    xm.GiaMua,
                    xm.KmDaChay,
                    xm.TrangThai,
                    xm.ID_Loai,
                    xm.MaNCC,
                    xm.NgayMua,
                    xm.NgayDangKy,
                    xm.HetHanDangKy,
                    xm.HetHanBaoHiem,
                    xm.ThongTinXang,
                    xm.AnhXe,
                    xm.MucDichSuDung,
                    xm.GiaNhap,
                    xm.SoLuong,
                    xm.SoLuongBanRa
                FROM XeMay xm
                INNER JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(keyword))
            {
                query += @" AND (xm.BienSo LIKE @Keyword 
                            OR hx.TenHang LIKE @Keyword 
                            OR dx.TenDong LIKE @Keyword 
                            OR xm.ID_Xe LIKE @Keyword)";
                parameters.Add(new SqlParameter("@Keyword", "%" + keyword + "%"));
            }

            if (!string.IsNullOrEmpty(trangThai) && trangThai != "Tất cả")
            {
                query += " AND xm.TrangThai = @TrangThai";
                parameters.Add(new SqlParameter("@TrangThai", trangThai));
            }

            query += " ORDER BY xm.ID_Xe";

            return DataProvider.ExecuteQuery(query, parameters.ToArray());
        }

        // Lấy thông tin xe theo ID
        // Lấy thông tin xe theo ID
        public XeMayDTO GetXeMayById(string idXe)
        {
            string query = @"
                SELECT 
                    xm.*,
                    lx.MaHang,
                    lx.MaDong,
                    lx.MaMau,
                    lx.NamSX,
                    hx.TenHang,
                    dx.TenDong,
                    ms.TenMau
                FROM XeMay xm
                INNER JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                WHERE xm.ID_Xe = @ID_Xe";

            SqlParameter[] prm = { new SqlParameter("@ID_Xe", idXe) };
            DataTable dt = DataProvider.ExecuteQuery(query, prm);
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                return new XeMayDTO
                {
                    ID_Xe = r["ID_Xe"].ToString(),
                    BienSo = r["BienSo"] != DBNull.Value ? r["BienSo"].ToString() : null,
                    ID_Loai = r["ID_Loai"].ToString(),
                    MaNCC = r["MaNCC"] != DBNull.Value ? r["MaNCC"].ToString() : null,
                    NgayMua = r["NgayMua"] != DBNull.Value ? (DateTime?)r["NgayMua"] : null,
                    GiaMua = r["GiaMua"] != DBNull.Value ? (decimal?)r["GiaMua"] : null,
                    NgayDangKy = r["NgayDangKy"] != DBNull.Value ? (DateTime?)r["NgayDangKy"] : null,
                    HetHanDangKy = r["HetHanDangKy"] != DBNull.Value ? (DateTime?)r["HetHanDangKy"] : null,
                    HetHanBaoHiem = r["HetHanBaoHiem"] != DBNull.Value ? (DateTime?)r["HetHanBaoHiem"] : null,
                    KmDaChay = r["KmDaChay"] != DBNull.Value ? Convert.ToInt32(r["KmDaChay"]) : (int?)null,
                    ThongTinXang = r["ThongTinXang"] != DBNull.Value ? r["ThongTinXang"].ToString() : null,
                    AnhXe = r["AnhXe"] != DBNull.Value ? (byte[])r["AnhXe"] : null,
                    TrangThai = r["TrangThai"].ToString(),
                    MucDichSuDung = r["MucDichSuDung"] != DBNull.Value ? r["MucDichSuDung"].ToString() : null,
                    GiaNhap = r["GiaNhap"] != DBNull.Value ? (decimal?)r["GiaNhap"] : null,
                    SoLuong = r["SoLuong"] != DBNull.Value ? Convert.ToInt32(r["SoLuong"]) : (int?)null,
                    SoLuongBanRa = r["SoLuongBanRa"] != DBNull.Value ? Convert.ToInt32(r["SoLuongBanRa"]) : (int?)null,
                    MaHang = r["MaHang"].ToString(),
                    MaDong = r["MaDong"].ToString(),
                    MaMau = r["MaMau"].ToString(),
                    NamSX = r["NamSX"] != DBNull.Value ? Convert.ToInt32(r["NamSX"]) : (int?)null,
                    TenHang = r["TenHang"] != DBNull.Value ? r["TenHang"].ToString() : null,
                    TenDong = r["TenDong"] != DBNull.Value ? r["TenDong"].ToString() : null,
                    TenMau = r["TenMau"] != DBNull.Value ? r["TenMau"].ToString() : null
                };
            }
            return null;
        }

        // Thêm xe mới
        public bool InsertXeMay(XeMayDTO xe)
        {
            string query = @"
                INSERT INTO XeMay (ID_Xe, BienSo, ID_Loai, MaNCC, NgayMua, GiaMua, 
                                   NgayDangKy, HetHanDangKy, HetHanBaoHiem, KmDaChay, 
                                   ThongTinXang, AnhXe, TrangThai, MucDichSuDung,
                                   GiaNhap, SoLuong, SoLuongBanRa)
                VALUES (@ID_Xe, @BienSo, @ID_Loai, @MaNCC, @NgayMua, @GiaMua, 
                        @NgayDangKy, @HetHanDangKy, @HetHanBaoHiem, @KmDaChay, 
                        @ThongTinXang, @AnhXe, @TrangThai, @MucDichSuDung,
                        @GiaNhap, @SoLuong, @SoLuongBanRa)";

            SqlParameter[] parameters = {
                new SqlParameter("@ID_Xe", xe.ID_Xe),
                new SqlParameter("@BienSo", (object)xe.BienSo ?? DBNull.Value),
                new SqlParameter("@ID_Loai", xe.ID_Loai),
                new SqlParameter("@MaNCC", (object)xe.MaNCC ?? DBNull.Value),
                new SqlParameter("@NgayMua", (object)xe.NgayMua ?? DBNull.Value),
                new SqlParameter("@GiaMua", (object)xe.GiaMua ?? DBNull.Value),
                new SqlParameter("@NgayDangKy", (object)xe.NgayDangKy ?? DBNull.Value),
                new SqlParameter("@HetHanDangKy", (object)xe.HetHanDangKy ?? DBNull.Value),
                new SqlParameter("@HetHanBaoHiem", (object)xe.HetHanBaoHiem ?? DBNull.Value),
                new SqlParameter("@KmDaChay", (object)xe.KmDaChay ?? DBNull.Value),
                new SqlParameter("@ThongTinXang", (object)xe.ThongTinXang ?? DBNull.Value),
                new SqlParameter("@AnhXe", (object)xe.AnhXe ?? DBNull.Value),
                new SqlParameter("@TrangThai", xe.TrangThai),
                new SqlParameter("@MucDichSuDung", (object)xe.MucDichSuDung ?? DBNull.Value),
                new SqlParameter("@GiaNhap", (object)xe.GiaNhap ?? DBNull.Value),
                new SqlParameter("@SoLuong", (object)xe.SoLuong ?? 1), // Mặc định = 1
                new SqlParameter("@SoLuongBanRa", (object)xe.SoLuongBanRa ?? 0) // Mặc định = 0
            };

            int result = DataProvider.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        // Cập nhật thông tin xe
        public bool UpdateXeMay(XeMayDTO xe)
        {
            string query = @"
                UPDATE XeMay 
                SET BienSo = @BienSo,
                    ID_Loai = @ID_Loai,
                    MaNCC = @MaNCC,
                    NgayMua = @NgayMua,
                    GiaMua = @GiaMua,
                    NgayDangKy = @NgayDangKy,
                    HetHanDangKy = @HetHanDangKy,
                    HetHanBaoHiem = @HetHanBaoHiem,
                    KmDaChay = @KmDaChay,
                    ThongTinXang = @ThongTinXang,
                    AnhXe = @AnhXe,
                    TrangThai = @TrangThai,
                    MucDichSuDung = @MucDichSuDung,
                    GiaNhap = @GiaNhap,
                    SoLuong = @SoLuong,
                    SoLuongBanRa = @SoLuongBanRa
                WHERE ID_Xe = @ID_Xe";

            SqlParameter[] parameters = {
                new SqlParameter("@ID_Xe", xe.ID_Xe),
                new SqlParameter("@BienSo", (object)xe.BienSo ?? DBNull.Value),
                new SqlParameter("@ID_Loai", xe.ID_Loai),
                new SqlParameter("@MaNCC", (object)xe.MaNCC ?? DBNull.Value),
                new SqlParameter("@NgayMua", (object)xe.NgayMua ?? DBNull.Value),
                new SqlParameter("@GiaMua", (object)xe.GiaMua ?? DBNull.Value),
                new SqlParameter("@NgayDangKy", (object)xe.NgayDangKy ?? DBNull.Value),
                new SqlParameter("@HetHanDangKy", (object)xe.HetHanDangKy ?? DBNull.Value),
                new SqlParameter("@HetHanBaoHiem", (object)xe.HetHanBaoHiem ?? DBNull.Value),
                new SqlParameter("@KmDaChay", (object)xe.KmDaChay ?? DBNull.Value),
                new SqlParameter("@ThongTinXang", (object)xe.ThongTinXang ?? DBNull.Value),
                new SqlParameter("@AnhXe", (object)xe.AnhXe ?? DBNull.Value),
                new SqlParameter("@TrangThai", xe.TrangThai),
                new SqlParameter("@MucDichSuDung", (object)xe.MucDichSuDung ?? DBNull.Value),
                new SqlParameter("@GiaNhap", (object)xe.GiaNhap ?? DBNull.Value),
                new SqlParameter("@SoLuong", (object)xe.SoLuong ?? DBNull.Value),
                new SqlParameter("@SoLuongBanRa", (object)xe.SoLuongBanRa ?? DBNull.Value)
            };

            int result = DataProvider.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        // Xóa xe
        public bool DeleteXeMay(string idXe)
        {
            string query = "DELETE FROM XeMay WHERE ID_Xe = @ID_Xe";
            SqlParameter[] parameters = {
                new SqlParameter("@ID_Xe", idXe)
            };

            int result = DataProvider.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        // Cập nhật trạng thái xe
        public bool UpdateTrangThai(string idXe, string trangThai)
        {
            string query = "UPDATE XeMay SET TrangThai = @TrangThai WHERE ID_Xe = @ID_Xe";
            SqlParameter[] parameters = {
                new SqlParameter("@ID_Xe", idXe),
                new SqlParameter("@TrangThai", trangThai)
            };

            int result = DataProvider.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        // Kiểm tra biển số đã tồn tại
        public bool IsBienSoExists(string bienSo, string idXe = null)
        {
            string query = "SELECT COUNT(*) FROM XeMay WHERE BienSo = @BienSo";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@BienSo", bienSo)
            };

            if (!string.IsNullOrEmpty(idXe))
            {
                query += " AND ID_Xe != @ID_Xe";
                parameters.Add(new SqlParameter("@ID_Xe", idXe));
            }

            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters.ToArray()));
            return count > 0;
        }

        /// Lấy danh sách xe có thể bán (Sẵn sàng + có giá bán + SoLuong > 0)
        public DataTable GetXeCoTheBan()
        {
            string query = @"
        SELECT 
            xe.ID_Xe,
            CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau, ' (', lx.NamSX, ')', 
                   ' - ', dx.PhanKhoi, 'cc') AS TenXe,
            xe.BienSo,
            hx.TenHang,
            dx.TenDong,
            ms.TenMau,
            lx.NamSX,
            dx.PhanKhoi,
            xe.ThongTinXang,
            xe.SoLuong,
            xe.TrangThai,
            xe.MucDichSuDung,
            xe.AnhXe,
            ISNULL(COALESCE(tg.GiaBan, xe.GiaNhap, xe.GiaMua), 0) AS GiaBan
            FROM XeMay xe
            INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
            INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
            INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
            INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
            LEFT JOIN ThongTinGiaXe tg ON xe.ID_Xe = tg.ID_Xe AND tg.PhanLoai = N'Bán'
            WHERE xe.TrangThai = N'Sẵn sàng'
              AND (xe.MucDichSuDung = N'Bán' OR xe.MucDichSuDung IS NULL)
              AND xe.SoLuong IS NOT NULL
              AND xe.SoLuong > 0
              AND (tg.GiaBan IS NOT NULL OR xe.GiaNhap IS NOT NULL OR xe.GiaMua IS NOT NULL)
            ORDER BY hx.TenHang, dx.TenDong, lx.NamSX DESC";

            return DataProvider.ExecuteQuery(query);
        }

        /// Lấy danh sách xe có thể cho thuê (Sẵn sàng + có giá thuê + không bị trùng lịch)
        public DataTable GetXeCoTheThue()
        {
            string query = @"
        SELECT 
            xe.ID_Xe,
            CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau, ' (', lx.NamSX, ')') AS TenXe,
            xe.BienSo,
            hx.TenHang,
            dx.TenDong,
            ms.TenMau,
            lx.NamSX,
            dx.PhanKhoi,
            xe.KmDaChay,
            xe.TrangThai,
            xe.AnhXe,
            tg.GiaThueNgay  -- ✅ Chỉ lấy GiaThueNgay
            FROM XeMay xe
            INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
            INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
            INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
            INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
            INNER JOIN ThongTinGiaXe tg ON xe.ID_Xe = tg.ID_Xe
            WHERE xe.TrangThai = N'Sẵn sàng'
              AND xe.MucDichSuDung = N'Cho thuê'
              AND tg.PhanLoai = N'Thuê'
              AND tg.GiaThueNgay IS NOT NULL
              AND tg.GiaThueNgay > 0
            ORDER BY hx.TenHang, dx.TenDong, lx.NamSX DESC";

            return DataProvider.ExecuteQuery(query);
        }

        /// Lấy danh sách xe có thể cho thuê trong khoảng thời gian cụ thể (kiểm tra lịch trùng)
        public DataTable GetXeCoTheThueTheoThoiGian(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            string query = @"
        SELECT 
            xe.ID_Xe,
            CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau, ' (', lx.NamSX, ')') AS TenXe,
            xe.BienSo,
            hx.TenHang,
            dx.TenDong,
            ms.TenMau,
            lx.NamSX,
            dx.PhanKhoi,
            xe.KmDaChay,
            xe.TrangThai,
            xe.AnhXe,
            tg.GiaThueNgay  -- ✅ Chỉ lấy GiaThueNgay
            FROM XeMay xe
            INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
            INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
            INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
            INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
            INNER JOIN ThongTinGiaXe tg ON xe.ID_Xe = tg.ID_Xe
            WHERE xe.TrangThai = N'Sẵn sàng'
              AND xe.MucDichSuDung = N'Cho thuê'
              AND tg.PhanLoai = N'Thuê'
              AND tg.GiaThueNgay IS NOT NULL
              AND tg.GiaThueNgay > 0
              -- Kiểm tra không trùng lịch thuê
              AND xe.ID_Xe NOT IN (
                  SELECT DISTINCT ID_Xe
                  FROM GiaoDichThue
                  WHERE TrangThaiDuyet = N'Đã duyệt'
                    AND TrangThai IN (N'Chờ giao xe', N'Đang thuê')
                    AND (
                        (@NgayBatDau BETWEEN NgayBatDau AND NgayKetThuc) OR
                        (@NgayKetThuc BETWEEN NgayBatDau AND NgayKetThuc) OR
                        (NgayBatDau BETWEEN @NgayBatDau AND @NgayKetThuc)
                    )
              )
            ORDER BY hx.TenHang, dx.TenDong, lx.NamSX DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@NgayBatDau", ngayBatDau),
        new SqlParameter("@NgayKetThuc", ngayKetThuc)
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }
        /// Thêm thông tin giá cho xe vào bảng ThongTinGiaXe
        public bool InsertThongTinGiaXe(string idXe, string phanLoai, decimal? giaThueNgay = null, decimal? giaBan = null)
        {
            try
            {
                string query = @"
            INSERT INTO ThongTinGiaXe (ID_Xe, PhanLoai, GiaThueNgay, GiaBan)
            VALUES (@ID_Xe, @PhanLoai, @GiaThueNgay, @GiaBan)";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ID_Xe", idXe),
            new SqlParameter("@PhanLoai", phanLoai),
            new SqlParameter("@GiaThueNgay", (object)giaThueNgay ?? DBNull.Value),
            new SqlParameter("@GiaBan", (object)giaBan ?? DBNull.Value)
                };

                int result = DataProvider.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi InsertThongTinGiaXe: {ex.Message}");
                return false;
            }
        }

        /// Kiểm tra xe đã có giá trong ThongTinGiaXe chưa
        public bool IsGiaXeExists(string idXe, string phanLoai)
        {
            try
            {
                string query = @"
            SELECT COUNT(*) 
            FROM ThongTinGiaXe 
            WHERE ID_Xe = @ID_Xe AND PhanLoai = @PhanLoai";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ID_Xe", idXe),
            new SqlParameter("@PhanLoai", phanLoai)
                };

                int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
                return count > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy giá xe theo loại
        /// </summary>
        public DataTable GetGiaXe(string idXe, string phanLoai)
        {
            try
            {
                string query = @"
                    SELECT ID_ThongTinGia, ID_Xe, PhanLoai, GiaBan, GiaThueNgay, TienDatCoc, NgayCapNhat, GhiChu
                    FROM ThongTinGiaXe
                    WHERE ID_Xe = @ID_Xe AND PhanLoai = @PhanLoai";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID_Xe", idXe),
                    new SqlParameter("@PhanLoai", phanLoai)
                };

                return DataProvider.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetGiaXe: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Lấy danh sách loại xe sẵn sàng bán (nhóm theo loại)
        /// </summary>
        public DataTable GetLoaiXeSanSangBan()
        {
            try
            {
                string query = @"
                    SELECT 
                        xe.ID_Loai,
                        hx.TenHang,
                        dx.TenDong,
                        ms.TenMau,
                        lx.NamSX,
                        dx.PhanKhoi,
                        COUNT(xe.ID_Xe) AS SoLuong,
                        MAX(ISNULL(xe.GiaMua, xe.GiaNhap)) AS GiaBanGanNhat
                    FROM XeMay xe
                    INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                    INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                    INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                    INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                    WHERE xe.MucDichSuDung = N'Bán' 
                      AND xe.TrangThai = N'Sẵn sàng'
                      AND (xe.SoLuong IS NULL OR xe.SoLuong > 0)
                    GROUP BY xe.ID_Loai, hx.TenHang, dx.TenDong, ms.TenMau, lx.NamSX, dx.PhanKhoi
                    HAVING COUNT(xe.ID_Xe) > 0
                    ORDER BY hx.TenHang, dx.TenDong";

                return DataProvider.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetLoaiXeSanSangBan: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Lấy 1 xe cụ thể từ loại xe để bán
        /// </summary>
        public DataTable GetXeTheoLoaiDeBan(string idLoai)
        {
            try
            {
                string query = @"
                    SELECT TOP 1
                        xe.ID_Xe,
                        xe.BienSo,
                        hx.TenHang,
                        dx.TenDong,
                        ms.TenMau,
                        lx.NamSX,
                        ISNULL(xe.GiaMua, xe.GiaNhap) AS GiaBan,
                        xe.SoLuong
                    FROM XeMay xe
                    INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                    INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                    INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                    INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                    WHERE xe.ID_Loai = @ID_Loai
                      AND xe.MucDichSuDung = N'Bán'
                      AND xe.TrangThai = N'Sẵn sàng'
                      AND (xe.SoLuong IS NULL OR xe.SoLuong > 0)
                    ORDER BY xe.ID_Xe";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID_Loai", idLoai)
                };

                return DataProvider.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetXeTheoLoaiDeBan: {ex.Message}");
                return null;
            }
        }
        // Thêm 2 methods này vào class XeMayDAL

        public bool KiemTraXeCoGiaoDich(string idXe)
        {
            string sql = @"
        SELECT COUNT(*) 
        FROM (
            SELECT ID_Xe FROM GiaoDichBan WHERE ID_Xe = @IdXe
            UNION ALL
            SELECT ID_Xe FROM GiaoDichThue WHERE ID_Xe = @IdXe
        ) AS GiaoDich";

            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IdXe", idXe);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool CapNhatTrangThaiXe(string idXe, string trangThai)
        {
            string sql = "UPDATE XeMay SET TrangThai = @TrangThai WHERE ID_Xe = @IdXe";

            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                cmd.Parameters.AddWithValue("@IdXe", idXe);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Kiểm tra xe đang được thuê
        /// </summary>
        public bool IsXeDangThue(string idXe)
        {
            string query = @"SELECT COUNT(*) 
                            FROM GiaoDichThue 
                            WHERE ID_Xe = @ID_Xe 
                            AND TrangThai IN (N'Chờ duyệt', N'Đã thanh toán', N'Đang thuê')";
            
            SqlParameter[] parameters = { new SqlParameter("@ID_Xe", idXe) };
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        /// <summary>
        /// Kiểm tra xe trong giao dịch bán
        /// </summary>
        public bool IsXeInGiaoDichBan(string idXe)
        {
            string query = @"SELECT COUNT(*) 
                            FROM GiaoDichBan 
                            WHERE ID_Xe = @ID_Xe 
                            AND TrangThai = N'Chờ duyệt'";
            
            SqlParameter[] parameters = { new SqlParameter("@ID_Xe", idXe) };
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        /// <summary>
        /// Kiểm tra xe đang bảo trì
        /// </summary>
        public bool IsXeDangBaoTri(string idXe)
        {
            string query = @"SELECT COUNT(*) 
                            FROM BaoTri 
                            WHERE ID_Xe = @ID_Xe";
            
            SqlParameter[] parameters = { new SqlParameter("@ID_Xe", idXe) };
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }
    }
}
