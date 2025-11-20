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
    (SELECT COUNT(*) FROM XeMay WHERE TrangThai = N'Sẵn sàng') AS XeSanSang,
    (SELECT COUNT(*) FROM GiaoDichBan WHERE CAST(NgayBan AS DATE) = CAST(GETDATE() AS DATE)) AS GiaoDichBanHomNay,
    (SELECT COUNT(*) FROM GiaoDichThue WHERE TrangThai = N'Đang thuê') AS ThueDangHoatDong,
    (SELECT ISNULL(SUM(ThanhTien),0) FROM HoaDon
        WHERE YEAR(NgayLap)=YEAR(GETDATE()) AND MONTH(NgayLap)=MONTH(GETDATE())) AS DoanhThuThangNay;";

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
                            GiaoDichBanHomNay = rd.IsDBNull(1) ? 0 : rd.GetInt32(1),
                            ThueDangHoatDong = rd.IsDBNull(2) ? 0 : rd.GetInt32(2),
                            DoanhThuThangNay = rd.IsDBNull(3) ? 0 : Convert.ToDecimal(rd[3])
                        };
                    }
                }
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
         THEN ISNULL(tt.GiaBan,0) ELSE ISNULL(tt.GiaThueNgay,0) END AS Gia,
    xm.TrangThai,
    xm.NgayMua
FROM XeMay xm
JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
JOIN HangXe hx ON lx.MaHang = hx.MaHang
JOIN DongXe dx ON lx.MaDong = dx.MaDong
LEFT JOIN ThongTinGiaXe tt ON tt.ID_Xe = xm.ID_Xe
    AND tt.PhanLoai = CASE WHEN xm.MucDichSuDung = N'Bán' THEN N'Bán' ELSE N'Thuê' END
ORDER BY xm.NgayMua DESC, xm.ID_Xe DESC;";

            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@TopN", SqlDbType.Int).Value = topN;
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
}