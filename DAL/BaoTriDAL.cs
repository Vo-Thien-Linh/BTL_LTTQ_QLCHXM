using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class BaoTriDAL
    {
        // Lấy connection string từ App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["QLCuaHangXeMayConn"].ConnectionString;

        // Lấy danh sách tất cả bảo trì
        public DataTable LayDanhSachBaoTri()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        bt.ID_BaoTri,
                        bt.ID_Xe,
                        xm.BienSo,
                        hx.TenHang + ' ' + dx.TenDong + ' ' + ms.TenMau AS TenXe,
                        bt.MaTaiKhoan,
                        nv.HoTenNV AS TenNhanVien,
                        bt.GhiChuBaoTri,
                        ISNULL(SUM(ctbt.GiaSuDung * ctbt.SoLuong), 0) AS TongChiPhi
                    FROM BaoTriXe bt
                    LEFT JOIN XeMay xm ON bt.ID_Xe = xm.ID_Xe
                    LEFT JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                    LEFT JOIN HangXe hx ON lx.MaHang = hx.MaHang
                    LEFT JOIN DongXe dx ON lx.MaDong = dx.MaDong
                    LEFT JOIN MauSac ms ON lx.MaMau = ms.MaMau
                    LEFT JOIN TaiKhoan tk ON bt.MaTaiKhoan = tk.MaTaiKhoan
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                    LEFT JOIN ChiTietBaoTri ctbt ON bt.ID_BaoTri = ctbt.ID_BaoTri
                    GROUP BY 
                        bt.ID_BaoTri, bt.ID_Xe, xm.BienSo, 
                        hx.TenHang, dx.TenDong, ms.TenMau,
                        bt.MaTaiKhoan, nv.HoTenNV, bt.GhiChuBaoTri
                    ORDER BY bt.ID_BaoTri DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            return dt;
        }

        // Lấy thông tin bảo trì theo ID
        public BaoTriDTO LayBaoTriTheoID(int idBaoTri)
        {
            BaoTriDTO baoTri = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        bt.ID_BaoTri,
                        bt.ID_Xe,
                        xm.BienSo,
                        bt.MaTaiKhoan,
                        nv.HoTenNV AS TenNhanVien,
                        bt.GhiChuBaoTri,
                        hx.TenHang,
                        dx.TenDong
                    FROM BaoTriXe bt
                    LEFT JOIN XeMay xm ON bt.ID_Xe = xm.ID_Xe
                    LEFT JOIN TaiKhoan tk ON bt.MaTaiKhoan = tk.MaTaiKhoan
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                    LEFT JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                    LEFT JOIN HangXe hx ON lx.MaHang = hx.MaHang
                    LEFT JOIN DongXe dx ON lx.MaDong = dx.MaDong
                    WHERE bt.ID_BaoTri = @ID_BaoTri";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_BaoTri", idBaoTri);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    baoTri = new BaoTriDTO
                    {
                        ID_BaoTri = Convert.ToInt32(reader["ID_BaoTri"]),
                        ID_Xe = reader["ID_Xe"].ToString(),
                        BienSoXe = reader["BienSo"].ToString(),
                        MaTaiKhoan = reader["MaTaiKhoan"].ToString(),
                        TenNhanVien = reader["TenNhanVien"].ToString(),
                        GhiChuBaoTri = reader["GhiChuBaoTri"].ToString(),
                        TenHangXe = reader["TenHang"].ToString(),
                        TenDongXe = reader["TenDong"].ToString()
                    };
                }
            }
            return baoTri;
        }

        // Lấy chi tiết bảo trì
        public List<ChiTietBaoTriDTO> LayChiTietBaoTri(int idBaoTri)
        {
            List<ChiTietBaoTriDTO> list = new List<ChiTietBaoTriDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        ctbt.ID_ChiTiet,
                        ctbt.ID_BaoTri,
                        ctbt.MaPhuTung,
                        pt.TenPhuTung,
                        pt.DonViTinh,
                        ctbt.SoLuong,
                        ctbt.GiaSuDung,
                        ctbt.SoLuong * ctbt.GiaSuDung AS ThanhTien,
                        ctbt.GhiChu
                    FROM ChiTietBaoTri ctbt
                    LEFT JOIN PhuTung pt ON ctbt.MaPhuTung = pt.MaPhuTung
                    WHERE ctbt.ID_BaoTri = @ID_BaoTri";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_BaoTri", idBaoTri);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ChiTietBaoTriDTO ct = new ChiTietBaoTriDTO
                    {
                        ID_ChiTiet = Convert.ToInt32(reader["ID_ChiTiet"]),
                        ID_BaoTri = Convert.ToInt32(reader["ID_BaoTri"]),
                        MaPhuTung = reader["MaPhuTung"].ToString(),
                        TenPhuTung = reader["TenPhuTung"].ToString(),
                        DonViTinh = reader["DonViTinh"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        GiaSuDung = Convert.ToDecimal(reader["GiaSuDung"]),
                        ThanhTien = Convert.ToDecimal(reader["ThanhTien"]),
                        GhiChu = reader["GhiChu"].ToString()
                    };
                    list.Add(ct);
                }
            }
            return list;
        }

        // Thêm bảo trì mới
        public bool ThemBaoTri(BaoTriDTO baoTri, List<ChiTietBaoTriDTO> chiTietList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Thêm bảo trì
                    string queryBaoTri = @"
                        INSERT INTO BaoTriXe (ID_Xe, MaTaiKhoan, GhiChuBaoTri)
                        VALUES (@ID_Xe, @MaTaiKhoan, @GhiChuBaoTri);
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdBaoTri = new SqlCommand(queryBaoTri, conn, transaction);
                    cmdBaoTri.Parameters.AddWithValue("@ID_Xe", baoTri.ID_Xe);
                    cmdBaoTri.Parameters.AddWithValue("@MaTaiKhoan", baoTri.MaTaiKhoan ?? (object)DBNull.Value);
                    cmdBaoTri.Parameters.AddWithValue("@GhiChuBaoTri", baoTri.GhiChuBaoTri ?? (object)DBNull.Value);

                    int idBaoTri = Convert.ToInt32(cmdBaoTri.ExecuteScalar());

                    // Thêm chi tiết bảo trì
                    foreach (var chiTiet in chiTietList)
                    {
                        string queryChiTiet = @"
                            INSERT INTO ChiTietBaoTri (ID_BaoTri, MaPhuTung, SoLuong, GiaSuDung, GhiChu)
                            VALUES (@ID_BaoTri, @MaPhuTung, @SoLuong, @GiaSuDung, @GhiChu);
                            
                            UPDATE KhoPhuTung 
                            SET SoLuongTon = SoLuongTon - @SoLuong,
                                NgayCapNhat = GETDATE()
                            WHERE MaPhuTung = @MaPhuTung";

                        SqlCommand cmdChiTiet = new SqlCommand(queryChiTiet, conn, transaction);
                        cmdChiTiet.Parameters.AddWithValue("@ID_BaoTri", idBaoTri);
                        cmdChiTiet.Parameters.AddWithValue("@MaPhuTung", chiTiet.MaPhuTung);
                        cmdChiTiet.Parameters.AddWithValue("@SoLuong", chiTiet.SoLuong);
                        cmdChiTiet.Parameters.AddWithValue("@GiaSuDung", chiTiet.GiaSuDung);
                        cmdChiTiet.Parameters.AddWithValue("@GhiChu", chiTiet.GhiChu ?? (object)DBNull.Value);

                        cmdChiTiet.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // Cập nhật bảo trì
        public bool CapNhatBaoTri(BaoTriDTO baoTri)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE BaoTriXe 
                    SET ID_Xe = @ID_Xe,
                        MaTaiKhoan = @MaTaiKhoan,
                        GhiChuBaoTri = @GhiChuBaoTri
                    WHERE ID_BaoTri = @ID_BaoTri";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_BaoTri", baoTri.ID_BaoTri);
                cmd.Parameters.AddWithValue("@ID_Xe", baoTri.ID_Xe);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", baoTri.MaTaiKhoan ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GhiChuBaoTri", baoTri.GhiChuBaoTri ?? (object)DBNull.Value);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        // Xóa bảo trì
        public bool XoaBaoTri(int idBaoTri)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Hoàn trả phụ tùng về kho
                    string queryHoanTra = @"
                        UPDATE KhoPhuTung
                        SET SoLuongTon = SoLuongTon + ctbt.SoLuong,
                            NgayCapNhat = GETDATE()
                        FROM KhoPhuTung kpt
                        INNER JOIN ChiTietBaoTri ctbt ON kpt.MaPhuTung = ctbt.MaPhuTung
                        WHERE ctbt.ID_BaoTri = @ID_BaoTri";

                    SqlCommand cmdHoanTra = new SqlCommand(queryHoanTra, conn, transaction);
                    cmdHoanTra.Parameters.AddWithValue("@ID_BaoTri", idBaoTri);
                    cmdHoanTra.ExecuteNonQuery();

                    // Xóa chi tiết bảo trì
                    string queryXoaChiTiet = "DELETE FROM ChiTietBaoTri WHERE ID_BaoTri = @ID_BaoTri";
                    SqlCommand cmdXoaChiTiet = new SqlCommand(queryXoaChiTiet, conn, transaction);
                    cmdXoaChiTiet.Parameters.AddWithValue("@ID_BaoTri", idBaoTri);
                    cmdXoaChiTiet.ExecuteNonQuery();

                    // Xóa bảo trì
                    string queryXoaBaoTri = "DELETE FROM BaoTriXe WHERE ID_BaoTri = @ID_BaoTri";
                    SqlCommand cmdXoaBaoTri = new SqlCommand(queryXoaBaoTri, conn, transaction);
                    cmdXoaBaoTri.Parameters.AddWithValue("@ID_BaoTri", idBaoTri);
                    cmdXoaBaoTri.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // Lấy danh sách xe có thể bảo trì (CHỈNH SỬA - lấy từ quản lý sản phẩm)
        public DataTable LayDanhSachXe()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // ✅ SỬA: Lấy tất cả xe từ quản lý sản phẩm (không chỉ "Sẵn sàng")
                // Bao gồm cả xe đang bảo trì, xe sẵn sàng, xe đang thuê
                string query = @"
                    SELECT 
                        xm.ID_Xe,
                        xm.BienSo,
                        hx.TenHang + ' ' + dx.TenDong + ' ' + ms.TenMau AS TenXe,
                        hx.TenHang,
                        dx.TenDong,
                        ms.TenMau,
                        lx.NamSX,
                        dx.PhanKhoi,
                        xm.TrangThai,
                        xm.MucDichSuDung,
                        xm.KmDaChay,
                        xm.BienSo + ' - ' + hx.TenHang + ' ' + dx.TenDong + ' (' + xm.TrangThai + ')' AS DisplayText
                    FROM XeMay xm
                    LEFT JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                    LEFT JOIN HangXe hx ON lx.MaHang = hx.MaHang
                    LEFT JOIN DongXe dx ON lx.MaDong = dx.MaDong
                    LEFT JOIN MauSac ms ON lx.MaMau = ms.MaMau
                    WHERE xm.BienSo IS NOT NULL 
                      AND xm.BienSo <> ''
                      AND xm.TrangThai IN (N'Sẵn sàng', N'Đang thuê', N'Đang bảo trì')
                    ORDER BY xm.TrangThai, xm.BienSo";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            return dt;
        }

        // Lấy danh sách phụ tùng
        public DataTable LayDanhSachPhuTung()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        pt.MaPhuTung,
                        pt.TenPhuTung,
                        pt.GiaBan,
                        pt.DonViTinh,
                        ISNULL(kpt.SoLuongTon, 0) AS SoLuongTon
                    FROM PhuTung pt
                    LEFT JOIN KhoPhuTung kpt ON pt.MaPhuTung = kpt.MaPhuTung
                    WHERE ISNULL(kpt.SoLuongTon, 0) > 0
                    ORDER BY pt.TenPhuTung";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            return dt;
        }

        // Lấy danh sách nhân viên kỹ thuật
        public DataTable LayDanhSachNhanVienKyThuat()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        tk.MaTaiKhoan,
                        nv.HoTenNV,
                        nv.Sdt,
                        tk.LoaiTaiKhoan
                    FROM TaiKhoan tk
                    INNER JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                    WHERE tk.LoaiTaiKhoan = 'KyThuat' 
                    AND tk.TrangThaiTaiKhoan = N'Hoạt động'
                    ORDER BY nv.HoTenNV";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            return dt;
        }

        // Tìm kiếm bảo trì
        public DataTable TimKiemBaoTri(string tuKhoa)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        bt.ID_BaoTri,
                        bt.ID_Xe,
                        xm.BienSo,
                        hx.TenHang + ' ' + dx.TenDong AS TenXe,
                        bt.MaTaiKhoan,
                        nv.HoTenNV AS TenNhanVien,
                        bt.GhiChuBaoTri,
                        ISNULL(SUM(ctbt.GiaSuDung * ctbt.SoLuong), 0) AS TongChiPhi
                    FROM BaoTriXe bt
                    LEFT JOIN XeMay xm ON bt.ID_Xe = xm.ID_Xe
                    LEFT JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                    LEFT JOIN HangXe hx ON lx.MaHang = hx.MaHang
                    LEFT JOIN DongXe dx ON lx.MaDong = dx.MaDong
                    LEFT JOIN TaiKhoan tk ON bt.MaTaiKhoan = tk.MaTaiKhoan
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                    LEFT JOIN ChiTietBaoTri ctbt ON bt.ID_BaoTri = ctbt.ID_BaoTri
                    WHERE xm.BienSo LIKE @TuKhoa 
                    OR hx.TenHang LIKE @TuKhoa
                    OR dx.TenDong LIKE @TuKhoa
                    OR nv.HoTenNV LIKE @TuKhoa
                    OR bt.GhiChuBaoTri LIKE @TuKhoa
                    GROUP BY 
                        bt.ID_BaoTri, bt.ID_Xe, xm.BienSo, 
                        hx.TenHang, dx.TenDong,
                        bt.MaTaiKhoan, nv.HoTenNV, bt.GhiChuBaoTri
                    ORDER BY bt.ID_BaoTri DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
    }
}