using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class DashboardDAL
    {
        private readonly string _connStr =
            ConfigurationManager.ConnectionStrings["QLCuaHangXeMayConn"].ConnectionString;

        // ============================================================
        // 1. LẤY THỐNG KÊ TỔNG QUAN (4 CARDS)
        // ============================================================
        public DashboardDTO GetStats()
        {
            const string sql = @"
            SELECT
                -- 1. Xe sẵn sàng (bao gồm cả xe bán và cho thuê)
                (SELECT COUNT(*) 
                 FROM XeMay 
                 WHERE TrangThai = N'Sẵn sàng'
                ) AS XeSanSang,
    
                -- 2. Doanh thu tháng này (từ cả bán xe và thuê xe)
                (
                    -- Doanh thu từ bán xe (đã bao gồm xe + phụ tùng - giảm giá)
                    SELECT ISNULL(SUM(TongThanhToan), 0) 
                    FROM GiaoDichBan 
                    WHERE YEAR(NgayBan) = YEAR(GETDATE()) 
                      AND MONTH(NgayBan) = MONTH(GETDATE())
                      AND TrangThaiThanhToan = N'Đã thanh toán'
                ) + 
                (
                    -- Doanh thu từ thuê xe (chỉ tính đơn đã hoàn thành)
                    SELECT ISNULL(SUM(TongGia), 0) 
                    FROM GiaoDichThue 
                    WHERE TrangThai = N'Đã thuê'
                      AND TrangThaiThanhToan = N'Đã thanh toán'
                      AND YEAR(NgayBatDau) = YEAR(GETDATE()) 
                      AND MONTH(NgayBatDau) = MONTH(GETDATE())
                ) AS DoanhThuThangNay,

                -- 3. Tổng số khách hàng
                (SELECT COUNT(*) FROM KhachHang) AS TongKhachHang,

                -- 4. Tổng giao dịch tháng này
                (
                    (SELECT COUNT(*) FROM GiaoDichBan 
                     WHERE YEAR(NgayBan) = YEAR(GETDATE()) 
                       AND MONTH(NgayBan) = MONTH(GETDATE()))
                    +
                    (SELECT COUNT(*) FROM GiaoDichThue 
                     WHERE YEAR(NgayBatDau) = YEAR(GETDATE()) 
                       AND MONTH(NgayBatDau) = MONTH(GETDATE())
                       AND TrangThai != N'Hủy')
                ) AS TongGiaoDich;";

            try
            {
                using (var conn = new SqlConnection(_connStr))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            return new DashboardDTO
                            {
                                XeSanSang = rd.IsDBNull(0) ? 0 : rd.GetInt32(0),
                                DoanhThuThangNay = rd.IsDBNull(1) ? 0 : rd.GetDecimal(1),
                                TongKhachHang = rd.IsDBNull(2) ? 0 : rd.GetInt32(2),
                                TongGiaoDich = rd.IsDBNull(3) ? 0 : rd.GetInt32(3)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ DashboardDAL.GetStats Error: {ex.Message}");
            }

            return new DashboardDTO();
        }

        // ============================================================
        // 2. LẤY DOANH THU 7 NGÀY GẦN NHẤT (CHO CHART)
        // ============================================================
        public DataTable GetDoanhThu7Ngay()
        {
            var dt = new DataTable();
            const string sql = @"
            WITH Last7Days AS (
                SELECT CAST(DATEADD(DAY, -n, GETDATE()) AS DATE) AS Ngay
                FROM (VALUES (0),(1),(2),(3),(4),(5),(6)) AS Numbers(n)
            )
            SELECT 
                d.Ngay,
                -- Doanh thu từ bán (bao gồm cả xe và phụ tùng, đã trừ giảm giá)
                ISNULL((
                    SELECT SUM(TongThanhToan) 
                    FROM GiaoDichBan 
                    WHERE CAST(NgayBan AS DATE) = d.Ngay
                      AND TrangThaiThanhToan = N'Đã thanh toán'
                ), 0) AS DoanhThuBan,
                -- Doanh thu từ cho thuê
                ISNULL((
                    SELECT SUM(TongGia) 
                    FROM GiaoDichThue 
                    WHERE CAST(NgayBatDau AS DATE) = d.Ngay
                      AND TrangThai = N'Đã thuê'
                      AND TrangThaiThanhToan = N'Đã thanh toán'
                ), 0) AS DoanhThuThue
            FROM Last7Days d
            ORDER BY d.Ngay ASC;";

            try
            {
                using (var conn = new SqlConnection(_connStr))
                using (var da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ GetDoanhThu7Ngay Error: {ex.Message}");
            }

            return dt;
        }

        // ============================================================
        // 3. LẤY TOP 5 XE BÁN CHẠY/CHO THUÊ NHIỀU NHẤT
        // ============================================================
        public DataTable GetTop5Xe()
        {
            var dt = new DataTable();
            const string sql = @"
            WITH XeBan AS (
                SELECT 
                    hx.TenHang + ' ' + dx.TenDong AS TenXe,
                    COUNT(*) AS SoLuong,
                    SUM(gdb.GiaBan) AS DoanhThu
                FROM GiaoDichBan gdb
                JOIN XeMay xm ON gdb.ID_Xe = xm.ID_Xe
                JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                JOIN HangXe hx ON lx.MaHang = hx.MaHang
                JOIN DongXe dx ON lx.MaDong = dx.MaDong
                WHERE YEAR(gdb.NgayBan) = YEAR(GETDATE())
                  AND MONTH(gdb.NgayBan) = MONTH(GETDATE())
                GROUP BY hx.TenHang, dx.TenDong
            ),
            XeThue AS (
                SELECT 
                    hx.TenHang + ' ' + dx.TenDong AS TenXe,
                    COUNT(*) AS SoLuong,
                    SUM(gdt.TongGia) AS DoanhThu
                FROM GiaoDichThue gdt
                JOIN XeMay xm ON gdt.ID_Xe = xm.ID_Xe
                JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                JOIN HangXe hx ON lx.MaHang = hx.MaHang
                JOIN DongXe dx ON lx.MaDong = dx.MaDong
                WHERE YEAR(gdt.NgayBatDau) = YEAR(GETDATE())
                  AND MONTH(gdt.NgayBatDau) = MONTH(GETDATE())
                  AND gdt.TrangThai != N'Hủy'
                GROUP BY hx.TenHang, dx.TenDong
            ),
            Combined AS (
                SELECT TenXe, SUM(SoLuong) AS TongSoLuong, SUM(DoanhThu) AS TongDoanhThu
                FROM (
                    SELECT * FROM XeBan
                    UNION ALL
                    SELECT * FROM XeThue
                ) AS AllTransactions
                GROUP BY TenXe
            )
            SELECT TOP 5 TenXe, TongSoLuong AS SoLuong, TongDoanhThu AS DoanhThu
            FROM Combined
            ORDER BY TongSoLuong DESC, TongDoanhThu DESC;";

            try
            {
                using (var conn = new SqlConnection(_connStr))
                using (var da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ GetTop5Xe Error: {ex.Message}");
            }

            return dt;
        }

        // ============================================================
        // 4. LẤY 10 HOẠT ĐỘNG GẦN ĐÂY
        // ============================================================
        public DataTable GetHoatDongGanDay(int top = 10)
        {
            var dt = new DataTable();
            const string sql = @"
            WITH RecentActivities AS (
                -- Giao dịch bán
                SELECT 
                    gdb.NgayBan AS NgayGiaoDich,
                    N'Bán' AS LoaiGiaoDich,
                    kh.HoTenKH AS TenKhachHang,
                    hx.TenHang + ' ' + dx.TenDong + ' ' + ms.TenMau AS ThongTinXe,
                    gdb.TongThanhToan AS GiaTri
                FROM GiaoDichBan gdb
                JOIN KhachHang kh ON gdb.MaKH = kh.MaKH
                JOIN XeMay xm ON gdb.ID_Xe = xm.ID_Xe
                JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                JOIN HangXe hx ON lx.MaHang = hx.MaHang
                JOIN DongXe dx ON lx.MaDong = dx.MaDong
                JOIN MauSac ms ON lx.MaMau = ms.MaMau
                
                UNION ALL
                
                -- Giao dịch thuê
                SELECT 
                    gdt.NgayBatDau AS NgayGiaoDich,
                    N'Thuê' AS LoaiGiaoDich,
                    kh.HoTenKH AS TenKhachHang,
                    hx.TenHang + ' ' + dx.TenDong + ' ' + ms.TenMau AS ThongTinXe,
                    gdt.TongGia AS GiaTri
                FROM GiaoDichThue gdt
                JOIN KhachHang kh ON gdt.MaKH = kh.MaKH
                JOIN XeMay xm ON gdt.ID_Xe = xm.ID_Xe
                JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                JOIN HangXe hx ON lx.MaHang = hx.MaHang
                JOIN DongXe dx ON lx.MaDong = dx.MaDong
                JOIN MauSac ms ON lx.MaMau = ms.MaMau
                WHERE gdt.TrangThai != N'Hủy'
            )
            SELECT TOP (@Top) *
            FROM RecentActivities
            ORDER BY NgayGiaoDich DESC;";

            try
            {
                using (var conn = new SqlConnection(_connStr))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@Top", SqlDbType.Int).Value = top;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ GetHoatDongGanDay Error: {ex.Message}");
            }

            return dt;
        }

        // ============================================================
        // 5. LẤY CẢNH BÁO TỒN KHO (PHỤ TÙNG SẮP HẾT)
        // ============================================================
        public DataTable GetCanhBaoTonKho(int mucCanhBao = 10)
        {
            var dt = new DataTable();
            const string sql = @"
            SELECT 
                pt.TenPhuTung,
                kpt.SoLuongTon,
                @MucCanhBao AS MucCanhBao,
                CASE 
                    WHEN kpt.SoLuongTon = 0 THEN N'Hết hàng'
                    WHEN kpt.SoLuongTon <= 5 THEN N'Sắp hết'
                    WHEN kpt.SoLuongTon <= 10 THEN N'Thấp'
                    ELSE N'Còn hàng'
                END AS TrangThai,
                pt.DonViTinh
            FROM KhoPhuTung kpt
            JOIN PhuTung pt ON kpt.MaPhuTung = pt.MaPhuTung
            ORDER BY kpt.SoLuongTon ASC;";

            try
            {
                using (var conn = new SqlConnection(_connStr))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@MucCanhBao", SqlDbType.Int).Value = mucCanhBao;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ GetCanhBaoTonKho Error: {ex.Message}");
            }

            return dt;
        }

        // ============================================================
        // CŨ - GIỮ LẠI ĐỂ TƯƠNG THÍCH
        // ============================================================
        public DataTable LayXeMoiNhap(int topN)
        {
            var dt = new DataTable();
            const string sql = @"
            SELECT TOP (@TopN)
                xm.ID_Xe AS IDXe,
                hx.TenHang AS Hang,
                dx.TenDong AS DongXe,
                xm.BienSo,
                xm.MucDichSuDung AS PhanLoai,
                CASE WHEN xm.MucDichSuDung = N'Bán'
                     THEN ISNULL(tt.GiaBan,0) 
                     ELSE ISNULL(tt.GiaThueNgay,0) 
                END AS Gia,
                xm.TrangThai,
                xm.NgayMua
            FROM XeMay xm
            JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
            JOIN HangXe hx ON lx.MaHang = hx.MaHang
            JOIN DongXe dx ON lx.MaDong = dx.MaDong
            LEFT JOIN ThongTinGiaXe tt ON tt.ID_Xe = xm.ID_Xe
                AND tt.PhanLoai = CASE WHEN xm.MucDichSuDung = N'Bán' THEN N'Bán' ELSE N'Thuê' END
            WHERE xm.TrangThai != N'Đã bán'
            ORDER BY xm.NgayMua DESC, xm.ID_Xe DESC;";

            try
            {
                using (var conn = new SqlConnection(_connStr))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@TopN", SqlDbType.Int).Value = topN;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ LayXeMoiNhap Error: {ex.Message}");
            }

            return dt;
        }
    }
}