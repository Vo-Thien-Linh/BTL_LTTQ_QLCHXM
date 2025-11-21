using System;
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

        public DashboardDTO GetStats()
        {
            const string sql = @"
            SELECT
                -- 1. Đếm xe sẵn sàng
                (SELECT COUNT(*) 
                 FROM XeMay 
                 WHERE TrangThai = N'Sẵn sàng' 
                   AND (TrangThai IS NULL OR TrangThai != N'Đã xóa')
                ) AS XeSanSang,
    
                -- 2. Đếm giao dịch bán hôm nay
                (SELECT COUNT(*) 
                 FROM GiaoDichBan 
                 WHERE CAST(NgayBan AS DATE) = CAST(GETDATE() AS DATE)
                ) AS GiaoDichBanHomNay,
    
                -- 3. Đếm giao dịch thuê đang hoạt động
                (SELECT COUNT(*) 
                 FROM GiaoDichThue 
                 WHERE TrangThai = N'Đang thuê'
                   AND TrangThaiDuyet = N'Đã duyệt'
                ) AS ThueDangHoatDong,
    
                -- 4. Tính doanh thu tháng này (từ CÙNG cả bán và thuê)
                (
                    -- Doanh thu từ bán xe
                    SELECT ISNULL(SUM(GiaBan), 0) 
                    FROM GiaoDichBan 
                    WHERE YEAR(NgayBan) = YEAR(GETDATE()) 
                      AND MONTH(NgayBan) = MONTH(GETDATE())
                ) + 
                (
                    -- Doanh thu từ thuê xe (chỉ tính đơn đã trả xe)
                    SELECT ISNULL(SUM(TongGia), 0) 
                    FROM GiaoDichThue 
                    WHERE TrangThai = N'Đã thuê'
                      AND TrangThaiThanhToan = N'Đã thanh toán'
                      AND YEAR(NgayBatDau) = YEAR(GETDATE()) 
                      AND MONTH(NgayBatDau) = MONTH(GETDATE())
                ) AS DoanhThuThangNay;";

            try
            {
                using (var conn = new SqlConnection(_connStr))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    System.Diagnostics.Debug.WriteLine("========== BẮT ĐẦU QUERY THỐNG KÊ ==========");

                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            var stats = new DashboardDTO
                            {
                                XeSanSang = rd.IsDBNull(0) ? 0 : rd.GetInt32(0),
                                GiaoDichBanHomNay = rd.IsDBNull(1) ? 0 : rd.GetInt32(1),
                                ThueDangHoatDong = rd.IsDBNull(2) ? 0 : rd.GetInt32(2),
                                DoanhThuThangNay = rd.IsDBNull(3) ? 0 : Convert.ToDecimal(rd[3])
                            };

                            System.Diagnostics.Debug.WriteLine($"✅ Xe sẵn sàng: {stats.XeSanSang}");
                            System.Diagnostics.Debug.WriteLine($"✅ GD bán hôm nay: {stats.GiaoDichBanHomNay}");
                            System.Diagnostics.Debug.WriteLine($"✅ Thuê đang hoạt động: {stats.ThueDangHoatDong}");
                            System.Diagnostics.Debug.WriteLine($"✅ Doanh thu tháng: {stats.DoanhThuThangNay:N0} VNĐ");
                            System.Diagnostics.Debug.WriteLine("========== KẾT THÚC QUERY ==========\n");

                            return stats;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("⚠️ WARNING: Query không trả về dữ liệu!");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine($" SQL ERROR: {sqlEx.Message}");
                System.Diagnostics.Debug.WriteLine($"   Error Number: {sqlEx.Number}");
                System.Diagnostics.Debug.WriteLine($"   Line Number: {sqlEx.LineNumber}");
                System.Diagnostics.Debug.WriteLine($"   StackTrace: {sqlEx.StackTrace}");

                // Trả về object mặc định
                return new DashboardDTO
                {
                    XeSanSang = -1,  // -1 để biết có lỗi
                    GiaoDichBanHomNay = 0,
                    ThueDangHoatDong = 0,
                    DoanhThuThangNay = 0
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ GENERAL ERROR: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"   StackTrace: {ex.StackTrace}");

                return new DashboardDTO();
            }

            return new DashboardDTO();
        }

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
            WHERE xm.TrangThai != N'Đã xóa' 
               OR xm.TrangThai IS NULL
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

                System.Diagnostics.Debug.WriteLine($" [LayXeMoiNhap] Đã load {dt.Rows.Count} xe");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($" [Lỗi LayXeMoiNhap] {ex.Message}");
            }

            return dt;
        }
    }
}