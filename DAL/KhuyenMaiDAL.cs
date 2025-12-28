using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class KhuyenMaiDAL
    {
        /// <summary>
        /// Lấy tất cả khuyến mãi
        /// </summary>
        public DataTable GetAllKhuyenMai()
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
                    TrangThai,
                    MaTaiKhoan,
                    NgayTao,
                    NgayCapNhat,
                    GhiChu
                FROM KhuyenMai
                ORDER BY NgayTao DESC";

            return DataProvider.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy khuyến mãi theo trạng thái
        /// </summary>
        public DataTable GetKhuyenMaiByTrangThai(string trangThai)
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
                    TrangThai,
                    GhiChu
                FROM KhuyenMai
                WHERE TrangThai = @TrangThai
                ORDER BY NgayBatDau DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@TrangThai", trangThai)
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Lấy khuyến mãi đang hoạt động theo loại áp dụng
        /// </summary>
        public DataTable GetKhuyenMaiHoatDong(string loaiApDung = null)
        {
            string query = @"
                SELECT 
                    MaKM,
                    TenKM,
                    LoaiKhuyenMai,
                    PhanTramGiam,
                    SoTienGiam,
                    GiaTriGiamToiDa,
                    LoaiApDung,
                    NgayBatDau,
                    NgayKetThuc
                FROM KhuyenMai
                WHERE TrangThai = N'Hoạt động'
                  AND GETDATE() BETWEEN NgayBatDau AND NgayKetThuc";

            if (!string.IsNullOrEmpty(loaiApDung))
            {
                query += " AND (LoaiApDung = N'Tất cả' OR LoaiApDung = @LoaiApDung)";
                SqlParameter[] parameters = {
                    new SqlParameter("@LoaiApDung", loaiApDung)
                };
                return DataProvider.ExecuteQuery(query, parameters);
            }

            return DataProvider.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy khuyến mãi còn hiệu lực theo ngày và loại áp dụng (tạm thời disabled)
        /// </summary>
        public DataTable GetKhuyenMaiHieuLuc(DateTime? ngayApDung = null, string loaiApDung = null)
        {
            // Tạm thời tắt chức năng khuyến mãi
            return new DataTable();
        }

        /// <summary>
        /// Tính giá trị giảm từ khuyến mãi (tạm thời disabled)
        /// </summary>
        public decimal TinhGiaTriGiam(string maKM, decimal giaTriDonHang, out string errorMessage)
        {
            errorMessage = "";
            
            // Tạm thời tắt chức năng khuyến mãi, không tính giảm giá
            return 0;
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// Thêm khuyến mãi mới
        /// </summary>
        public bool InsertKhuyenMai(KhuyenMaiDTO km, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                string query = @"
                    INSERT INTO KhuyenMai (
                        MaKM, TenKM, MoTa, NgayBatDau, NgayKetThuc,
                        LoaiKhuyenMai, PhanTramGiam, SoTienGiam, GiaTriGiamToiDa,
                        LoaiApDung, TrangThai, MaTaiKhoan, GhiChu
                    )
                    VALUES (
                        @MaKM, @TenKM, @MoTa, @NgayBatDau, @NgayKetThuc,
                        @LoaiKhuyenMai, @PhanTramGiam, @SoTienGiam, @GiaTriGiamToiDa,
                        @LoaiApDung, @TrangThai, @MaTaiKhoan, @GhiChu
                    )";

                SqlParameter[] parameters = {
                    new SqlParameter("@MaKM", km.MaKM),
                    new SqlParameter("@TenKM", km.TenKM),
                    new SqlParameter("@MoTa", (object)km.MoTa ?? DBNull.Value),
                    new SqlParameter("@NgayBatDau", km.NgayBatDau),
                    new SqlParameter("@NgayKetThuc", km.NgayKetThuc),
                    new SqlParameter("@LoaiKhuyenMai", km.LoaiKhuyenMai),
                    new SqlParameter("@PhanTramGiam", (object)km.PhanTramGiam ?? DBNull.Value),
                    new SqlParameter("@SoTienGiam", (object)km.SoTienGiam ?? DBNull.Value),
                    new SqlParameter("@GiaTriGiamToiDa", (object)km.GiaTriGiamToiDa ?? DBNull.Value),
                    new SqlParameter("@LoaiApDung", km.LoaiApDung),
                    new SqlParameter("@TrangThai", km.TrangThai),
                    new SqlParameter("@MaTaiKhoan", (object)km.MaTaiKhoan ?? DBNull.Value),
                    new SqlParameter("@GhiChu", (object)km.GhiChu ?? DBNull.Value)
                };

                int result = DataProvider.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Cập nhật khuyến mãi
        /// </summary>
        public bool UpdateKhuyenMai(KhuyenMaiDTO km, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                string query = @"
                    UPDATE KhuyenMai
                    SET TenKM = @TenKM,
                        MoTa = @MoTa,
                        NgayBatDau = @NgayBatDau,
                        NgayKetThuc = @NgayKetThuc,
                        LoaiKhuyenMai = @LoaiKhuyenMai,
                        PhanTramGiam = @PhanTramGiam,
                        SoTienGiam = @SoTienGiam,
                        GiaTriGiamToiDa = @GiaTriGiamToiDa,
                        LoaiApDung = @LoaiApDung,
                        TrangThai = @TrangThai,
                        GhiChu = @GhiChu,
                        NgayCapNhat = GETDATE()
                    WHERE MaKM = @MaKM";

                SqlParameter[] parameters = {
                    new SqlParameter("@MaKM", km.MaKM),
                    new SqlParameter("@TenKM", km.TenKM),
                    new SqlParameter("@MoTa", (object)km.MoTa ?? DBNull.Value),
                    new SqlParameter("@NgayBatDau", km.NgayBatDau),
                    new SqlParameter("@NgayKetThuc", km.NgayKetThuc),
                    new SqlParameter("@LoaiKhuyenMai", km.LoaiKhuyenMai),
                    new SqlParameter("@PhanTramGiam", (object)km.PhanTramGiam ?? DBNull.Value),
                    new SqlParameter("@SoTienGiam", (object)km.SoTienGiam ?? DBNull.Value),
                    new SqlParameter("@GiaTriGiamToiDa", (object)km.GiaTriGiamToiDa ?? DBNull.Value),
                    new SqlParameter("@LoaiApDung", km.LoaiApDung),
                    new SqlParameter("@TrangThai", km.TrangThai),
                    new SqlParameter("@GhiChu", (object)km.GhiChu ?? DBNull.Value)
                };

                int result = DataProvider.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Xóa khuyến mãi (soft delete - chuyển sang trạng thái Hủy)
        /// </summary>
        public bool DeleteKhuyenMai(string maKM, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                string query = @"
                    UPDATE KhuyenMai
                    SET TrangThai = N'Hủy',
                        NgayCapNhat = GETDATE()
                    WHERE MaKM = @MaKM";

                SqlParameter[] parameters = {
                    new SqlParameter("@MaKM", maKM)
                };

                int result = DataProvider.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra mã khuyến mãi đã tồn tại
        /// </summary>
        public bool CheckMaKMExists(string maKM)
        {
            string query = "SELECT COUNT(*) FROM KhuyenMai WHERE MaKM = @MaKM";
            SqlParameter[] parameters = {
                new SqlParameter("@MaKM", maKM)
            };

            int count = (int)DataProvider.ExecuteScalar(query, parameters);
            return count > 0;
        }

        /// <summary>
        /// Tự động tạo mã khuyến mãi
        /// </summary>
        public string GenerateMaKM()
        {
            string query = @"
                SELECT TOP 1 MaKM 
                FROM KhuyenMai 
                WHERE MaKM LIKE 'KM%' 
                ORDER BY MaKM DESC";

            object result = DataProvider.ExecuteScalar(query);

            if (result != null)
            {
                string lastMa = result.ToString();
                int number = int.Parse(lastMa.Substring(2)) + 1;
                return "KM" + number.ToString("D8");
            }

            return "KM00000001";
        }
    }
}
