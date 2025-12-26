using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class GiaoDichThueDAL
    {
        /// Lấy tất cả giao dịch thuê
        public DataTable GetAllGiaoDichThue()
        {
            string query = @"
                SELECT 
                    gd.MaGDThue, 
                    gd.MaKH, 
                    kh.HoTenKH, 
                    kh.Sdt AS SdtKhachHang,
                    gd.ID_Xe, 
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau) AS TenXe,
                    xe.BienSo,
                    xe.AnhXe,   
                    gd.NgayBatDau, 
                    gd.NgayKetThuc,
                    DATEDIFF(DAY, gd.NgayBatDau, gd.NgayKetThuc) AS SoNgayThue,
                    gd.GiaThueNgay,
                    gd.TongGia, 
                    gd.TrangThai,
                    gd.TrangThaiThanhToan, 
                    gd.HinhThucThanhToan,
                    gd.SoTienCoc,
                    gd.TrangThaiDuyet,
                    gd.NguoiDuyet,
                    gd.NgayDuyet,
                    gd.GhiChuDuyet,
                    nv.HoTenNV AS TenNhanVien
                    FROM GiaoDichThue gd
                    INNER JOIN KhachHang kh ON gd.MaKH = kh.MaKH
                    INNER JOIN XeMay xe ON gd.ID_Xe = xe.ID_Xe
                    INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                    INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                    INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                    INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                    LEFT JOIN TaiKhoan tk ON gd.MaTaiKhoan = tk.MaTaiKhoan
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                    ORDER BY gd.NgayBatDau DESC";

            return DataProvider.ExecuteQuery(query);
        }

        /// Lấy giao dịch thuê theo trạng thái duyệt
        public DataTable GetGiaoDichThueByTrangThai(string trangThaiDuyet)
        {
            string query = @"
                SELECT 
                    gd.MaGDThue, 
                    gd.MaKH, 
                    kh.HoTenKH, 
                    kh.Sdt AS SdtKhachHang,
                    gd.ID_Xe, 
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau) AS TenXe,
                    xe.BienSo,
                    xe.AnhXe,
                    gd.NgayBatDau, 
                    gd.NgayKetThuc,
                    DATEDIFF(DAY, gd.NgayBatDau, gd.NgayKetThuc) AS SoNgayThue,
                    gd.GiaThueNgay,
                    gd.TongGia, 
                    gd.TrangThai,
                    gd.TrangThaiThanhToan, 
                    gd.HinhThucThanhToan,
                    gd.SoTienCoc,
                    gd.TrangThaiDuyet,
                    gd.NguoiDuyet,
                    gd.NgayDuyet,
                    gd.GhiChuDuyet,
                    nv.HoTenNV AS TenNhanVien
                    FROM GiaoDichThue gd
                    INNER JOIN KhachHang kh ON gd.MaKH = kh.MaKH
                    INNER JOIN XeMay xe ON gd.ID_Xe = xe.ID_Xe
                    INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                    INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                    INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                    INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                    LEFT JOIN TaiKhoan tk ON gd.MaTaiKhoan = tk.MaTaiKhoan
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                    WHERE gd.TrangThaiDuyet = @TrangThaiDuyet
                    ORDER BY gd.NgayBatDau DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TrangThaiDuyet", trangThaiDuyet)
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// Thêm giao dịch thuê mới
        public bool InsertGiaoDichThue(GiaoDichThue gd)
        {
            try
            {
                string query = @"
                INSERT INTO GiaoDichThue (
                    ID_Xe, MaKH, NgayBatDau, NgayKetThuc,
                    GiaThueNgay, TongGia, TrangThai, TrangThaiThanhToan,
                    SoTienCoc, GiayToGiuLai, MaTaiKhoan, TrangThaiDuyet,
                    HinhThucThanhToan
                )
                VALUES (
                    @ID_Xe, @MaKH, @NgayBatDau, @NgayKetThuc,
                    @GiaThueNgay, @TongGia, @TrangThai, @TrangThaiThanhToan,
                    @SoTienCoc, @GiayToGiuLai, @MaTaiKhoan, @TrangThaiDuyet,
                    @HinhThucThanhToan
                )";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ID_Xe", gd.ID_Xe),
            new SqlParameter("@MaKH", gd.MaKH),
            new SqlParameter("@NgayBatDau", gd.NgayBatDau),
            new SqlParameter("@NgayKetThuc", gd.NgayKetThuc),
            new SqlParameter("@GiaThueNgay", gd.GiaThueNgay),
            new SqlParameter("@TongGia", gd.TongGia),
            new SqlParameter("@TrangThai", gd.TrangThai ?? "Chờ xác nhận"),
            new SqlParameter("@TrangThaiThanhToan", gd.TrangThaiThanhToan ?? "Chưa thanh toán"),
            new SqlParameter("@SoTienCoc", (object)gd.SoTienCoc ?? DBNull.Value),
            new SqlParameter("@GiayToGiuLai", gd.GiayToGiuLai ?? ""),
            new SqlParameter("@MaTaiKhoan", gd.MaTaiKhoan),
            new SqlParameter("@TrangThaiDuyet", gd.TrangThaiDuyet ?? "Chờ duyệt"),
            new SqlParameter("@HinhThucThanhToan", (object)gd.HinhThucThanhToan ?? DBNull.Value)
                };

                int result = DataProvider.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DAL Insert Error: {ex.Message}");
                throw new Exception("Lỗi khi thêm giao dịch thuê vào database: " + ex.Message);
            }
        }

        /// Duyệt đơn thuê
        public bool ApproveGiaoDichThue(int maGDThue, string nguoiDuyet, string ghiChu)
        {
            string query = @"
            UPDATE GiaoDichThue 
            SET TrangThaiDuyet = N'Đã duyệt',
                NguoiDuyet = @NguoiDuyet, 
                NgayDuyet = @NgayDuyet,
                GhiChuDuyet = @GhiChu
            WHERE MaGDThue = @MaGDThue 
              AND TrangThaiDuyet = N'Chờ duyệt'"; // Chỉ duyệt đơn đang chờ

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaGDThue", maGDThue),
        new SqlParameter("@NguoiDuyet", nguoiDuyet),
        new SqlParameter("@NgayDuyet", DateTime.Now),
        new SqlParameter("@GhiChu", (object)ghiChu ?? DBNull.Value)
            };

            int rowsAffected = DataProvider.ExecuteNonQuery(query, parameters);

            // Kiểm tra xem có cập nhật được không (nếu = 0 => đơn không ở trạng thái "Chờ duyệt")
            return rowsAffected > 0;
        }
        public bool XacNhanGiaoXe(int maGDThue, string nguoiGiao, int kmBatDau, string ghiChu)
        {
            string query = @"
                UPDATE GiaoDichThue 
                SET TrangThai = N'Đang thuê',
                    NgayGiaoXeThucTe = @NgayGiao,
                    KmBatDau = @KmBatDau,
                    GhiChuGiaoXe = @GhiChu
                WHERE MaGDThue = @MaGDThue";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaGDThue", maGDThue),
                new SqlParameter("@NgayGiao", DateTime.Now),
                new SqlParameter("@KmBatDau", kmBatDau),
                new SqlParameter("@GhiChu", (object)ghiChu ?? DBNull.Value)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }
        /// Từ chối đơn thuê
        public bool RejectGiaoDichThue(int maGDThue, string nguoiDuyet, string lyDo)
        {
            string query = @"
                UPDATE GiaoDichThue 
                SET TrangThaiDuyet = N'Từ chối', 
                    TrangThai = N'Hủy',
                    NguoiDuyet = @NguoiDuyet, 
                    NgayDuyet = @NgayDuyet,
                    GhiChuDuyet = @LyDo
                WHERE MaGDThue = @MaGDThue";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaGDThue", maGDThue),
                new SqlParameter("@NguoiDuyet", nguoiDuyet),
                new SqlParameter("@NgayDuyet", DateTime.Now),
                new SqlParameter("@LyDo", lyDo)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// Tìm kiếm giao dịch thuê
        public DataTable SearchGiaoDichThue(string keyword)
        {
            string query = @"
                SELECT 
                    gd.MaGDThue, 
                    gd.MaKH, 
                    kh.HoTenKH, 
                    kh.Sdt AS SdtKhachHang,
                    gd.ID_Xe, 
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau) AS TenXe,
                    xe.BienSo,
                    xe.AnhXe,
                    gd.NgayBatDau, 
                    gd.NgayKetThuc,
                    DATEDIFF(DAY, gd.NgayBatDau, gd.NgayKetThuc) AS SoNgayThue,
                    gd.GiaThueNgay,
                    gd.TongGia, 
                    gd.TrangThai,
                    gd.TrangThaiThanhToan, 
                    gd.TrangThaiDuyet,
                    nv.HoTenNV AS TenNhanVien
                    FROM GiaoDichThue gd
                    INNER JOIN KhachHang kh ON gd.MaKH = kh.MaKH
                    INNER JOIN XeMay xe ON gd.ID_Xe = xe.ID_Xe
                    INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                    INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                    INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                    INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                    LEFT JOIN TaiKhoan tk ON gd.MaTaiKhoan = tk.MaTaiKhoan
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                    WHERE kh.HoTenKH LIKE @Keyword 
                       OR kh.Sdt LIKE @Keyword
                       OR xe.BienSo LIKE @Keyword
                       OR CAST(gd.MaGDThue AS NVARCHAR) LIKE @Keyword
                    ORDER BY gd.NgayBatDau DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", "%" + keyword + "%")
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }
    }
}