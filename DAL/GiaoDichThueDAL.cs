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
                    -- THÊM MỚI: Các cột khuyến mãi
                    gd.MaKM,
                    km.TenKM,
                    gd.SoTienGiam,
                    gd.TongTienTruocGiam,
                    gd.TongThanhToan,
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
                    LEFT JOIN KhuyenMai km ON gd.MaKM = km.MaKM
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
            string query = @"
                INSERT INTO GiaoDichThue 
                (ID_Xe, MaKH, NgayBatDau, NgayKetThuc, GiaThueNgay, TongGia, 
                 TrangThai, TrangThaiThanhToan, HinhThucThanhToan, SoTienCoc, 
                 GiayToGiuLai, MaTaiKhoan, TrangThaiDuyet,
                 MaKM, SoTienGiam, TongTienTruocGiam, TongThanhToan)
                VALUES 
                (@ID_Xe, @MaKH, @NgayBatDau, @NgayKetThuc, @GiaThueNgay, @TongGia, 
                 @TrangThai, @TrangThaiThanhToan, @HinhThucThanhToan, @SoTienCoc, 
                 @GiayToGiuLai, @MaTaiKhoan, @TrangThaiDuyet,
                 @MaKM, @SoTienGiam, @TongTienTruocGiam, @TongThanhToan)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID_Xe", gd.ID_Xe),
                new SqlParameter("@MaKH", gd.MaKH),
                new SqlParameter("@NgayBatDau", gd.NgayBatDau),
                new SqlParameter("@NgayKetThuc", gd.NgayKetThuc),
                new SqlParameter("@GiaThueNgay", gd.GiaThueNgay),
                new SqlParameter("@TongGia", gd.TongGia),
                new SqlParameter("@TrangThai", gd.TrangThai),
                new SqlParameter("@TrangThaiThanhToan", gd.TrangThaiThanhToan),
                new SqlParameter("@HinhThucThanhToan", (object)gd.HinhThucThanhToan ?? DBNull.Value),
                new SqlParameter("@SoTienCoc", (object)gd.SoTienCoc ?? DBNull.Value),
                new SqlParameter("@GiayToGiuLai", gd.GiayToGiuLai),
                new SqlParameter("@MaTaiKhoan", gd.MaTaiKhoan),
                new SqlParameter("@TrangThaiDuyet", gd.TrangThaiDuyet),
                // THÊM KHUYẾN MÃI
                new SqlParameter("@MaKM", string.IsNullOrWhiteSpace(gd.MaKM) ? (object)DBNull.Value : gd.MaKM),
                new SqlParameter("@SoTienGiam", gd.SoTienGiam),
                new SqlParameter("@TongTienTruocGiam", gd.TongTienTruocGiam ?? gd.TongGia),
                new SqlParameter("@TongThanhToan", gd.TongThanhToan ?? gd.TongGia)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
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

#region KHUYẾN MÃI

/// <summary>
/// Lấy danh sách khuyến mãi khả dụng cho thuê xe
/// </summary>
public DataTable GetKhuyenMaiThueXe(DateTime ngayThue)
{
    string query = @"
        SELECT 
            MaKM,
            TenKM,
            MoTa,
            NgayBatDau,
            NgayKetThuc,
            LoaiKhuyenMai,
            PhanTramGiam,
            SoTienGiam,
            GiaTriGiamToiDa,
            LoaiApDung,
            GhiChu,
            CASE 
                WHEN LoaiKhuyenMai = N'Giảm %' THEN 
                    CONCAT(N'Giảm ', PhanTramGiam, N'%', 
                        CASE WHEN GiaTriGiamToiDa IS NOT NULL 
                        THEN CONCAT(N' (Tối đa ', FORMAT(GiaTriGiamToiDa, 'N0'), N'đ)') 
                        ELSE N'' END)
                WHEN LoaiKhuyenMai = N'Giảm tiền' THEN 
                    CONCAT(N'Giảm ', FORMAT(SoTienGiam, 'N0'), N'đ')
                ELSE N''
            END AS MoTaGiam
        FROM KhuyenMai
        WHERE TrangThai = N'Hoạt động'
            AND @NgayThue BETWEEN NgayBatDau AND NgayKetThuc
            AND (LoaiApDung = N'Tất cả' OR LoaiApDung = N'Xe thuê')
        ORDER BY NgayBatDau DESC";

    SqlParameter[] parameters = new SqlParameter[]
    {
        new SqlParameter("@NgayThue", ngayThue)
    };

    return DataProvider.ExecuteQuery(query, parameters);
}

/// <summary>
/// Kiểm tra khuyến mãi có hợp lệ không
/// </summary>
public bool KiemTraKhuyenMaiHopLe(string maKM, DateTime ngayThue, out string errorMessage)
{
    errorMessage = "";

    if (string.IsNullOrWhiteSpace(maKM))
    {
        errorMessage = "Mã khuyến mãi không được để trống!";
        return false;
    }

    string query = @"
        SELECT COUNT(*) 
        FROM KhuyenMai
        WHERE MaKM = @MaKM
            AND TrangThai = N'Hoạt động'
            AND @NgayThue BETWEEN NgayBatDau AND NgayKetThuc
            AND (LoaiApDung = N'Tất cả' OR LoaiApDung = N'Xe thuê')";

    SqlParameter[] parameters = new SqlParameter[]
    {
        new SqlParameter("@MaKM", maKM),
        new SqlParameter("@NgayThue", ngayThue)
    };

    int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));

    if (count == 0)
    {
        errorMessage = "Mã khuyến mãi không hợp lệ hoặc không áp dụng cho thuê xe!";
        return false;
    }

    return true;
}

/// <summary>
/// Tính giá trị giảm từ khuyến mãi
/// </summary>
public decimal TinhGiaTriGiamKhuyenMai(string maKM, decimal tongTienThue, out string errorMessage)
{
    errorMessage = "";

    if (string.IsNullOrWhiteSpace(maKM))
    {
        return 0;
    }

    string query = @"
        SELECT 
            LoaiKhuyenMai,
            PhanTramGiam,
            SoTienGiam,
            GiaTriGiamToiDa
        FROM KhuyenMai
        WHERE MaKM = @MaKM";

    SqlParameter[] parameters = new SqlParameter[]
    {
        new SqlParameter("@MaKM", maKM)
    };

    DataTable dt = DataProvider.ExecuteQuery(query, parameters);

    if (dt.Rows.Count == 0)
    {
        errorMessage = "Không tìm thấy khuyến mãi!";
        return 0;
    }

    DataRow row = dt.Rows[0];
    string loaiKM = row["LoaiKhuyenMai"].ToString();
    decimal giaTriGiam = 0;

    if (loaiKM == "Giảm %")
    {
        decimal phanTramGiam = Convert.ToDecimal(row["PhanTramGiam"]);
        giaTriGiam = tongTienThue * phanTramGiam / 100;

        // Áp dụng giới hạn tối đa
        if (row["GiaTriGiamToiDa"] != DBNull.Value)
        {
            decimal giaTriGiamToiDa = Convert.ToDecimal(row["GiaTriGiamToiDa"]);
            if (giaTriGiam > giaTriGiamToiDa)
            {
                giaTriGiam = giaTriGiamToiDa;
            }
        }
    }
    else if (loaiKM == "Giảm tiền")
    {
        giaTriGiam = Convert.ToDecimal(row["SoTienGiam"]);
    }

    // Không giảm quá tổng tiền
    if (giaTriGiam > tongTienThue)
    {
        giaTriGiam = tongTienThue;
    }

    return giaTriGiam;
}

#endregion
    }
}