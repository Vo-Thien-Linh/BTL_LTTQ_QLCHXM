using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class HopDongMuaDAL
    {
        /// <summary>
        /// Thêm hợp đồng mua mới
        /// </summary>
        public int InsertHopDongMua(HopDongMuaDTO hopDong, out string errorMessage)
        {
            errorMessage = "";
            try
            {
                string query = @"
                    INSERT INTO HopDongMua 
                        (MaGDBan, MaKH, MaTaiKhoan, ID_Xe, NgayLap, GiaBan, DieuKhoan, GhiChu, TrangThaiHopDong, FileHopDong)
                    VALUES 
                        (@MaGDBan, @MaKH, @MaTaiKhoan, @ID_Xe, @NgayLap, @GiaBan, @DieuKhoan, @GhiChu, @TrangThaiHopDong, @FileHopDong);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaGDBan", hopDong.MaGDBan),
                    new SqlParameter("@MaKH", hopDong.MaKH),
                    new SqlParameter("@MaTaiKhoan", hopDong.MaTaiKhoan),
                    new SqlParameter("@ID_Xe", hopDong.ID_Xe),
                    new SqlParameter("@NgayLap", hopDong.NgayLap),
                    new SqlParameter("@GiaBan", hopDong.GiaBan),
                    new SqlParameter("@DieuKhoan", (object)hopDong.DieuKhoan ?? DBNull.Value),
                    new SqlParameter("@GhiChu", (object)hopDong.GhiChu ?? DBNull.Value),
                    new SqlParameter("@TrangThaiHopDong", hopDong.TrangThaiHopDong),
                    new SqlParameter("@FileHopDong", (object)hopDong.FileHopDong ?? DBNull.Value)
                };

                object result = DataProvider.ExecuteScalar(query, parameters);
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// Lấy danh sách hợp đồng mua
        /// </summary>
        public DataTable GetAllHopDongMua()
        {
            string query = @"
                SELECT 
                    hdm.MaHDM,
                    hdm.MaGDBan,
                    hdm.MaKH,
                    kh.HoTenKH,
                    kh.Sdt,
                    hdm.ID_Xe,
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau) AS TenXe,
                    xe.BienSo,
                    hdm.NgayLap,
                    hdm.GiaBan,
                    hdm.DieuKhoan,
                    hdm.GhiChu,
                    hdm.TrangThaiHopDong,
                    hdm.FileHopDong,
                    nv.HoTenNV AS TenNhanVien
                FROM HopDongMua hdm
                INNER JOIN KhachHang kh ON hdm.MaKH = kh.MaKH
                INNER JOIN XeMay xe ON hdm.ID_Xe = xe.ID_Xe
                INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                LEFT JOIN TaiKhoan tk ON hdm.MaTaiKhoan = tk.MaTaiKhoan
                LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                ORDER BY hdm.NgayLap DESC";

            return DataProvider.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy hợp đồng theo mã giao dịch bán
        /// </summary>
        public DataTable GetHopDongByMaGDBan(int maGDBan)
        {
            string query = @"
                SELECT 
                    hdm.MaHDM,
                    hdm.MaGDBan,
                    hdm.MaKH,
                    kh.HoTenKH,
                    kh.Sdt,
                    kh.DiaChi,
                    kh.SoCCCD,
                    hdm.ID_Xe,
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau) AS TenXe,
                    xe.BienSo,
                    hdm.NgayLap,
                    hdm.GiaBan,
                    hdm.DieuKhoan,
                    hdm.GhiChu,
                    hdm.TrangThaiHopDong,
                    hdm.FileHopDong,
                    nv.HoTenNV AS TenNhanVien
                FROM HopDongMua hdm
                INNER JOIN KhachHang kh ON hdm.MaKH = kh.MaKH
                INNER JOIN XeMay xe ON hdm.ID_Xe = xe.ID_Xe
                INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                LEFT JOIN TaiKhoan tk ON hdm.MaTaiKhoan = tk.MaTaiKhoan
                LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                WHERE hdm.MaGDBan = @MaGDBan";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaGDBan", maGDBan)
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Cập nhật trạng thái hợp đồng
        /// </summary>
        public bool UpdateTrangThaiHopDong(int maHDM, string trangThai)
        {
            string query = @"
                UPDATE HopDongMua 
                SET TrangThaiHopDong = @TrangThai
                WHERE MaHDM = @MaHDM";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHDM", maHDM),
                new SqlParameter("@TrangThai", trangThai)
            };

            int rowsAffected = DataProvider.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
    }
}
