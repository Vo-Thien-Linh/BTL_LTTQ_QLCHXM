using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class GiaoDichThueDAL
    {
        /// <summary>
        /// Lấy tất cả giao dịch thuê
        /// </summary>
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

        /// <summary>
        /// Lấy giao dịch thuê theo trạng thái duyệt
        /// </summary>
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

        /// <summary>
        /// Thêm giao dịch thuê mới
        /// </summary>
        public bool InsertGiaoDichThue(GiaoDichThue gd)
        {
            string query = @"
                INSERT INTO GiaoDichThue (ID_Xe, MaKH, NgayBatDau, NgayKetThuc, GiaThueNgay, 
                    TongGia, TrangThai, TrangThaiThanhToan, HinhThucThanhToan, SoTienCoc, 
                    GiayToGiuLai, MaTaiKhoan, TrangThaiDuyet)
                VALUES (@ID_Xe, @MaKH, @NgayBatDau, @NgayKetThuc, @GiaThueNgay, 
                    @TongGia, @TrangThai, @TrangThaiThanhToan, @HinhThucThanhToan, @SoTienCoc, 
                    @GiayToGiuLai, @MaTaiKhoan, @TrangThaiDuyet)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID_Xe", gd.ID_Xe),
                new SqlParameter("@MaKH", gd.MaKH),
                new SqlParameter("@NgayBatDau", gd.NgayBatDau),
                new SqlParameter("@NgayKetThuc", gd.NgayKetThuc),
                new SqlParameter("@GiaThueNgay", gd.GiaThueNgay),
                new SqlParameter("@TongGia", gd.TongGia),
                new SqlParameter("@TrangThai", (object)gd.TrangThai ?? DBNull.Value),
                new SqlParameter("@TrangThaiThanhToan", (object)gd.TrangThaiThanhToan ?? DBNull.Value),
                new SqlParameter("@HinhThucThanhToan", (object)gd.HinhThucThanhToan ?? DBNull.Value),
                new SqlParameter("@SoTienCoc", (object)gd.SoTienCoc ?? DBNull.Value),
                new SqlParameter("@GiayToGiuLai", (object)gd.GiayToGiuLai ?? DBNull.Value),
                new SqlParameter("@MaTaiKhoan", (object)gd.MaTaiKhoan ?? DBNull.Value),
                new SqlParameter("@TrangThaiDuyet", gd.TrangThaiDuyet)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Duyệt đơn thuê
        /// </summary>
        public bool ApproveGiaoDichThue(int maGDThue, string nguoiDuyet, string ghiChu)
        {
            string query = @"
                UPDATE GiaoDichThue 
                SET TrangThaiDuyet = N'Đã duyệt', 
                    TrangThai = N'Đang thuê',
                    NguoiDuyet = @NguoiDuyet, 
                    NgayDuyet = @NgayDuyet,
                    GhiChuDuyet = @GhiChu
                WHERE MaGDThue = @MaGDThue";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaGDThue", maGDThue),
                new SqlParameter("@NguoiDuyet", nguoiDuyet),
                new SqlParameter("@NgayDuyet", DateTime.Now),
                new SqlParameter("@GhiChu", (object)ghiChu ?? DBNull.Value)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Từ chối đơn thuê
        /// </summary>
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

        /// <summary>
        /// Tìm kiếm giao dịch thuê
        /// </summary>
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