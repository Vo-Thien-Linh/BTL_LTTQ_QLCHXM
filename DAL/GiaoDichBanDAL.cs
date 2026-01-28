using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class GiaoDichBanDAL
    {
        /// <summary>
        /// Lấy lịch sử giao dịch tổng hợp (Bán + Thuê) với thông tin gọn
        /// </summary>
        public DataTable GetLichSuGiaoDichTongHop()
        {
            string query = @"
                -- Giao dịch BÁN XE (có xe)
                SELECT 
                    'Bán Xe' AS LoaiGiaoDich,
                    gd.NgayBan AS NgayGiaoDich, 
                    kh.HoTenKH AS KhachHang,
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau) AS SanPham,
                    gd.TongThanhToan AS ThanhToan,
                    gd.TongGiaPhuTung AS TienPhuTung,
                    gd.TrangThaiThanhToan,
                    nv.HoTenNV AS NhanVien
                FROM GiaoDichBan gd
                INNER JOIN KhachHang kh ON gd.MaKH = kh.MaKH
                INNER JOIN XeMay xe ON gd.ID_Xe = xe.ID_Xe
                INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                LEFT JOIN TaiKhoan tk ON gd.MaTaiKhoan = tk.MaTaiKhoan
                LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV

                UNION ALL

                -- Giao dịch THUÊ
                SELECT 
                    'Cho Thuê' AS LoaiGiaoDich,
                    gd.NgayBatDau AS NgayGiaoDich, 
                    kh.HoTenKH AS KhachHang,
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau, ' (', DATEDIFF(DAY, gd.NgayBatDau, gd.NgayKetThuc), ' ngày)') AS SanPham,
                    gd.TongThanhToan AS ThanhToan,
                    0 AS TienPhuTung,
                    gd.TrangThaiThanhToan,
                    nv.HoTenNV AS NhanVien
                FROM GiaoDichThue gd
                INNER JOIN KhachHang kh ON gd.MaKH = kh.MaKH
                INNER JOIN XeMay xe ON gd.ID_Xe = xe.ID_Xe
                INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                LEFT JOIN TaiKhoan tk ON gd.MaTaiKhoan = tk.MaTaiKhoan
                LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV

                ORDER BY NgayGiaoDich DESC";

            return DataProvider.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy tất cả giao dịch bán với thông tin chi tiết
        /// </summary>
        public DataTable GetAllGiaoDichBan()
        {
            string query = @"
                SELECT 
                    gd.MaGDBan, 
                    gd.MaKH, 
                    kh.HoTenKH, 
                    kh.Sdt AS SdtKhachHang,
                    gd.ID_Xe, 
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau) AS TenXe,
                    xe.BienSo,
                    gd.NgayBan, 
                    gd.GiaBan, 
                    gd.TrangThaiThanhToan, 
                    gd.HinhThucThanhToan,
                    gd.MaKM,
                    gd.SoTienGiam,
                    gd.TongGiaPhuTung,
                    gd.TongGiamPhuTung,
                    gd.TongThanhToan,
                    nv.HoTenNV AS TenNhanVien
                FROM GiaoDichBan gd
                INNER JOIN KhachHang kh ON gd.MaKH = kh.MaKH
                INNER JOIN XeMay xe ON gd.ID_Xe = xe.ID_Xe
                INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                LEFT JOIN TaiKhoan tk ON gd.MaTaiKhoan = tk.MaTaiKhoan
                LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                ORDER BY gd.NgayBan DESC";

            return DataProvider.ExecuteQuery(query);
        }



        /// <summary>
        /// Lấy giao dịch bán theo trạng thái duyệt
        /// </summary>
        public DataTable GetGiaoDichBanByTrangThai(string trangThaiThanhToan)
        {
            string query = @"
                SELECT 
                    gd.MaGDBan, 
                    gd.MaKH, 
                    kh.HoTenKH, 
                    kh.Sdt AS SdtKhachHang,
                    gd.ID_Xe, 
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau) AS TenXe,
                    xe.BienSo,
                    gd.NgayBan, 
                    gd.GiaBan, 
                    gd.TrangThaiThanhToan, 
                    gd.HinhThucThanhToan,
                    gd.MaKM,
                    gd.SoTienGiam,
                    gd.TongGiaPhuTung,
                    gd.TongGiamPhuTung,
                    gd.TongThanhToan,
                    nv.HoTenNV AS TenNhanVien
                FROM GiaoDichBan gd
                INNER JOIN KhachHang kh ON gd.MaKH = kh.MaKH
                INNER JOIN XeMay xe ON gd.ID_Xe = xe.ID_Xe
                INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                LEFT JOIN TaiKhoan tk ON gd.MaTaiKhoan = tk.MaTaiKhoan
                LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                WHERE gd.TrangThaiThanhToan = @TrangThaiThanhToan
                ORDER BY gd.NgayBan DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TrangThaiThanhToan", trangThaiThanhToan)
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Lấy thông tin chi tiết giao dịch bán theo mã
        /// </summary>
        public DataTable GetGiaoDichBanByMa(int maGDBan)
        {
            string query = @"
                SELECT 
                    gd.MaGDBan, 
                    gd.MaKH, 
                    kh.HoTenKH, 
                    kh.Sdt AS SdtKhachHang,
                    kh.Email AS EmailKhachHang,
                    kh.DiaChi AS DiaChiKhachHang,
                    gd.ID_Xe, 
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau) AS TenXe,
                    xe.BienSo,
                    gd.NgayBan, 
                    gd.GiaBan, 
                    gd.TrangThaiThanhToan, 
                    gd.HinhThucThanhToan,
                    gd.MaTaiKhoan,
                    gd.MaKM,
                    gd.SoTienGiam,
                    gd.TongGiaPhuTung,
                    gd.TongGiamPhuTung,
                    gd.TongThanhToan,
                    km.TenKM,
                    nv.HoTenNV AS TenNhanVien
                FROM GiaoDichBan gd
                INNER JOIN KhachHang kh ON gd.MaKH = kh.MaKH
                INNER JOIN XeMay xe ON gd.ID_Xe = xe.ID_Xe
                INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                LEFT JOIN TaiKhoan tk ON gd.MaTaiKhoan = tk.MaTaiKhoan
                LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                LEFT JOIN KhuyenMai km ON gd.MaKM = km.MaKM
                WHERE gd.MaGDBan = @MaGDBan";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaGDBan", maGDBan)
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Thêm giao dịch bán mới và trả về MaGDBan
        /// </summary>
        public int InsertGiaoDichBan(GiaoDichBan gd, out string errorMessage)
        {
            errorMessage = "";
            using (SqlConnection connection = DataProvider.GetConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Chỉ kiểm tra số lượng tồn nếu có xe
                    if (!string.IsNullOrEmpty(gd.ID_Xe))
                    {
                        string checkQuery = "SELECT SoLuong FROM XeMay WHERE ID_Xe = @ID_Xe";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, connection, transaction);
                        checkCmd.Parameters.AddWithValue("@ID_Xe", gd.ID_Xe);
                        
                        object result = checkCmd.ExecuteScalar();
                        if (result == null || Convert.ToInt32(result) <= 0)
                        {
                            transaction.Rollback();
                            errorMessage = "Xe đã hết hàng!";
                            return 0;
                        }
                    }

                    // Thêm giao dịch bán và lấy MaGDBan
                    string insertQuery = @"
                        INSERT INTO GiaoDichBan (MaKH, ID_Xe, NgayBan, GiaBan, TrangThaiThanhToan, 
                            HinhThucThanhToan, MaTaiKhoan, MaKM, SoTienGiam, TongGiaPhuTung, TongGiamPhuTung, TongThanhToan)
                        VALUES (@MaKH, @ID_Xe, @NgayBan, @GiaBan, @TrangThaiThanhToan, 
                            @HinhThucThanhToan, @MaTaiKhoan, @MaKM, @SoTienGiam, @TongGiaPhuTung, @TongGiamPhuTung, @TongThanhToan);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    SqlCommand insertCmd = new SqlCommand(insertQuery, connection, transaction);
                    insertCmd.Parameters.AddWithValue("@MaKH", gd.MaKH);
                    insertCmd.Parameters.AddWithValue("@ID_Xe", (object)gd.ID_Xe ?? DBNull.Value);
                    insertCmd.Parameters.AddWithValue("@NgayBan", gd.NgayBan);
                    insertCmd.Parameters.AddWithValue("@GiaBan", gd.GiaBan);
                    insertCmd.Parameters.AddWithValue("@TrangThaiThanhToan", (object)gd.TrangThaiThanhToan ?? DBNull.Value);
                    insertCmd.Parameters.AddWithValue("@HinhThucThanhToan", (object)gd.HinhThucThanhToan ?? DBNull.Value);
                    insertCmd.Parameters.AddWithValue("@MaTaiKhoan", (object)gd.MaTaiKhoan ?? DBNull.Value);
                    insertCmd.Parameters.AddWithValue("@MaKM", (object)gd.MaKM ?? DBNull.Value);
                    insertCmd.Parameters.AddWithValue("@SoTienGiam", gd.SoTienGiam);
                    insertCmd.Parameters.AddWithValue("@TongGiaPhuTung", gd.TongGiaPhuTung);
                    insertCmd.Parameters.AddWithValue("@TongGiamPhuTung", gd.TongGiamPhuTung);
                    insertCmd.Parameters.AddWithValue("@TongThanhToan", gd.TongThanhToan);
                    
                    int maGDBan = Convert.ToInt32(insertCmd.ExecuteScalar());

                    // Chỉ cập nhật xe nếu có xe
                    if (!string.IsNullOrEmpty(gd.ID_Xe))
                    {
                        string updateQuery = @"
                            UPDATE XeMay 
                            SET SoLuong = SoLuong - 1,
                                SoLuongBanRa = ISNULL(SoLuongBanRa, 0) + 1,
                                TrangThai = CASE 
                                    WHEN SoLuong - 1 = 0 THEN N'Đã bán'
                                    ELSE TrangThai
                                END
                            WHERE ID_Xe = @ID_Xe";

                        SqlCommand updateCmd = new SqlCommand(updateQuery, connection, transaction);
                        updateCmd.Parameters.AddWithValue("@ID_Xe", gd.ID_Xe);
                        updateCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return maGDBan;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    errorMessage = ex.Message;
                    System.Diagnostics.Debug.WriteLine($"Lỗi InsertGiaoDichBan: {ex.Message}");
                    return 0;
                }
            }
        }

        /// <summary>
        /// Duyệt đơn hàng
        /// </summary>
        public bool ApproveGiaoDichBan(int maGDBan, string nguoiDuyet, string ghiChu)
        {
            string query = @"
                UPDATE GiaoDichBan 
                SET TrangThaiDuyet = N'Đã duyệt', 
                    NguoiDuyet = @NguoiDuyet, 
                    NgayDuyet = @NgayDuyet,
                    GhiChuDuyet = @GhiChu
                WHERE MaGDBan = @MaGDBan";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaGDBan", maGDBan),
                new SqlParameter("@NguoiDuyet", nguoiDuyet),
                new SqlParameter("@NgayDuyet", DateTime.Now),
                new SqlParameter("@GhiChu", (object)ghiChu ?? DBNull.Value)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Từ chối đơn hàng
        /// </summary>
        public bool RejectGiaoDichBan(int maGDBan, string nguoiDuyet, string lyDo)
        {
            string query = @"
                UPDATE GiaoDichBan 
                SET TrangThaiDuyet = N'Từ chối', 
                    NguoiDuyet = @NguoiDuyet, 
                    NgayDuyet = @NgayDuyet,
                    GhiChuDuyet = @LyDo
                WHERE MaGDBan = @MaGDBan";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaGDBan", maGDBan),
                new SqlParameter("@NguoiDuyet", nguoiDuyet),
                new SqlParameter("@NgayDuyet", DateTime.Now),
                new SqlParameter("@LyDo", lyDo)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Cập nhật trạng thái xe sau khi duyệt
        /// </summary>
        public bool UpdateTrangThaiXe(string idXe, string trangThai)
        {
            string query = "UPDATE XeMay SET TrangThai = @TrangThai WHERE ID_Xe = @ID_Xe";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID_Xe", idXe),
                new SqlParameter("@TrangThai", trangThai)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Tìm kiếm giao dịch bán
        /// </summary>
        public DataTable SearchGiaoDichBan(string keyword)
        {
            string query = @"
                SELECT 
                    gd.MaGDBan, 
                    gd.MaKH, 
                    kh.HoTenKH, 
                    kh.Sdt AS SdtKhachHang,
                    gd.ID_Xe, 
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau) AS TenXe,
                    xe.BienSo,
                    gd.NgayBan, 
                    gd.GiaBan, 
                    gd.TrangThaiThanhToan, 
                    gd.HinhThucThanhToan,
                    gd.TrangThaiDuyet,
                    gd.NguoiDuyet,
                    gd.NgayDuyet,
                    gd.GhiChuDuyet,
                    nv.HoTenNV AS TenNhanVien
                FROM GiaoDichBan gd
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
                   OR CAST(gd.MaGDBan AS NVARCHAR) LIKE @Keyword
                ORDER BY gd.NgayBan DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", "%" + keyword + "%")
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Thêm giao dịch bán kèm phụ tùng sử dụng stored procedure
        /// </summary>
        public int InsertGiaoDichBanKemPhuTung(
            string maKH, string idXe, DateTime ngayBan, decimal giaBan,
            string trangThaiThanhToan, string hinhThucThanhToan, string maTaiKhoan,
            string maKM_Xe, decimal soTienGiam_Xe, string danhSachPhuTungJson,
            out string errorMessage)
        {
            errorMessage = "";
            
            try
            {
                using (SqlConnection connection = DataProvider.GetConnection())
                {
                    connection.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("sp_ThemGiaoDichBanKemPhuTung", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        // Thêm các parameters
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.Parameters.AddWithValue("@ID_Xe", idXe);
                        cmd.Parameters.AddWithValue("@NgayBan", ngayBan);
                        cmd.Parameters.AddWithValue("@GiaBan", giaBan);
                        cmd.Parameters.AddWithValue("@TrangThaiThanhToan", trangThaiThanhToan ?? "");
                        cmd.Parameters.AddWithValue("@HinhThucThanhToan", hinhThucThanhToan ?? "");
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan ?? "");
                        cmd.Parameters.AddWithValue("@MaKM_Xe", string.IsNullOrEmpty(maKM_Xe) ? (object)DBNull.Value : maKM_Xe);
                        cmd.Parameters.AddWithValue("@SoTienGiam_Xe", soTienGiam_Xe);
                        cmd.Parameters.AddWithValue("@DanhSachPhuTung", string.IsNullOrEmpty(danhSachPhuTungJson) ? (object)DBNull.Value : danhSachPhuTungJson);

                        // Output parameter
                        SqlParameter outputParam = new SqlParameter("@MaGDBan", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        // Execute
                        cmd.ExecuteNonQuery();

                        // Lấy kết quả
                        int maGDBan = Convert.ToInt32(outputParam.Value);
                        return maGDBan;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                System.Diagnostics.Debug.WriteLine($"Lỗi InsertGiaoDichBanKemPhuTung: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Lấy chi tiết phụ tùng đã bán kèm theo MaGDBan
        /// </summary>
        public DataTable GetChiTietPhuTungBan(int maGDBan)
        {
            string query = @"
                SELECT 
                    ct.ID_ChiTiet,
                    ct.MaGDBan,
                    ct.MaPhuTung,
                    pt.TenPhuTung,
                    ct.SoLuong,
                    ct.DonGia,
                    ct.ThanhTien
                FROM ChiTietPhuTungBan ct
                INNER JOIN PhuTung pt ON ct.MaPhuTung = pt.MaPhuTung
                WHERE ct.MaGDBan = @MaGDBan
                ORDER BY ct.ID_ChiTiet";

            SqlParameter[] parameters = {
                new SqlParameter("@MaGDBan", maGDBan)
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Thêm chi tiết phụ tùng bán
        /// </summary>
        public bool InsertChiTietPhuTungBan(ChiTietPhuTungBanDTO chiTiet)
        {
            string query = @"
                INSERT INTO ChiTietPhuTungBan (MaGDBan, MaPhuTung, SoLuong, DonGia, ThanhTien)
                VALUES (@MaGDBan, @MaPhuTung, @SoLuong, @DonGia, @ThanhTien)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaGDBan", chiTiet.MaGDBan),
                new SqlParameter("@MaPhuTung", chiTiet.MaPhuTung),
                new SqlParameter("@SoLuong", chiTiet.SoLuong),
                new SqlParameter("@DonGia", chiTiet.DonGia),
                new SqlParameter("@ThanhTien", chiTiet.SoLuong * chiTiet.DonGia)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Lấy lịch sử bán phụ tùng lẻ
        /// </summary>
        public DataTable GetLichSuBanPhuTungLe()
        {
            string query = @"
                SELECT 
                    ls.NgayBan AS NgayGiaoDich,
                    ls.TenPhuTung AS SanPham,
                    ls.SoLuong,
                    ls.ThanhTien,
                    nv.HoTenNV AS NhanVien
                FROM LichSuBanPhuTungLe ls
                LEFT JOIN TaiKhoan tk ON ls.MaTaiKhoan = tk.MaTaiKhoan
                LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                ORDER BY ls.NgayBan DESC";

            return DataProvider.ExecuteQuery(query);
        }
    }
}