using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class HopDongThueDAL
    {
        /// Tạo hợp đồng thuê xe
        public bool InsertHopDongThue(HopDongThue hd)
        {
            string query = @"
                INSERT INTO HopDongThue 
                (MaGDThue, MaKH, MaTaiKhoan, ID_Xe, NgayLap, NgayBatDau, NgayKetThuc, 
                 GiaThue, TienDatCoc, DieuKhoan, GhiChu, TrangThaiHopDong, FileHopDong)
                VALUES 
                (@MaGDThue, @MaKH, @MaTaiKhoan, @ID_Xe, @NgayLap, @NgayBatDau, @NgayKetThuc, 
                 @GiaThue, @TienDatCoc, @DieuKhoan, @GhiChu, @TrangThaiHopDong, @FileHopDong)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaGDThue", hd.MaGDThue),
                new SqlParameter("@MaKH", hd.MaKH),
                new SqlParameter("@MaTaiKhoan", hd.MaTaiKhoan),
                new SqlParameter("@ID_Xe", hd.ID_Xe),
                new SqlParameter("@NgayLap", hd.NgayLap),
                new SqlParameter("@NgayBatDau", hd.NgayBatDau),
                new SqlParameter("@NgayKetThuc", hd.NgayKetThuc),
                new SqlParameter("@GiaThue", hd.GiaThue),
                new SqlParameter("@TienDatCoc", (object)hd.TienDatCoc ?? DBNull.Value),
                new SqlParameter("@DieuKhoan", (object)hd.DieuKhoan ?? DBNull.Value),
                new SqlParameter("@GhiChu", (object)hd.GhiChu ?? DBNull.Value),
                new SqlParameter("@TrangThaiHopDong", hd.TrangThaiHopDong),
                new SqlParameter("@FileHopDong", (object)hd.FileHopDong ?? DBNull.Value)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// Lấy hợp đồng theo mã giao dịch thuê
        public DataTable GetHopDongByMaGDThue(int maGDThue)
        {
            string query = @"
                SELECT 
                    hd.*,
                    kh.HoTenKH,
                    kh.Sdt AS SdtKhachHang,
                    nv.HoTenNV AS TenNhanVien,
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau) AS TenXe,
                    xe.BienSo
                    FROM HopDongThue hd
                    INNER JOIN KhachHang kh ON hd.MaKH = kh.MaKH
                    INNER JOIN TaiKhoan tk ON hd.MaTaiKhoan = tk.MaTaiKhoan
                    INNER JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                    INNER JOIN XeMay xe ON hd.ID_Xe = xe.ID_Xe
                    INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                    INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                    INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                    INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                    WHERE hd.MaGDThue = @MaGDThue";

            SqlParameter[] parameters = { new SqlParameter("@MaGDThue", maGDThue) };
            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// Cập nhật trạng thái hợp đồng
        public bool UpdateTrangThaiHopDong(int maHDT, string trangThai)
        {
            string query = @"
                UPDATE HopDongThue 
                SET TrangThaiHopDong = @TrangThai
                WHERE MaHDT = @MaHDT";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHDT", maHDT),
                new SqlParameter("@TrangThai", trangThai)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// Kiểm tra xem giao dịch đã có hợp đồng chưa
        public bool IsHopDongExists(int maGDThue)
        {
            string query = "SELECT COUNT(*) FROM HopDongThue WHERE MaGDThue = @MaGDThue";
            SqlParameter[] parameters = { new SqlParameter("@MaGDThue", maGDThue) };
            
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }
    }
}