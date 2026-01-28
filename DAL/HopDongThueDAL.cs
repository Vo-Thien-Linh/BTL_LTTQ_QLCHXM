using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class HopDongThueDAL
    {
        /// <summary>
        /// Tạo hợp đồng thuê xe
        /// </summary>
        public bool InsertHopDongThue(HopDongThue hd)
        {
            string query = @"
                INSERT INTO HopDongThue 
                (MaGDThue, MaKH, MaTaiKhoan, ID_Xe, NgayLap, NgayBatDau, NgayKetThuc, 
                 GiaThue, TienDatCoc, DieuKhoan, GhiChu, TrangThaiHopDong, FileHopDong)
                VALUES 
                (@MaGDThue, @MaKH, @MaTaiKhoan, @ID_Xe, @NgayLap, @NgayBatDau, @NgayKetThuc, 
                 @GiaThue, @TienDatCoc, @DieuKhoan, @GhiChu, @TrangThaiHopDong, @FileHopDong)";

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaGDThue", hd.MaGDThue),
                    new SqlParameter("@MaKH", hd.MaKH ?? (object)DBNull.Value),
                    new SqlParameter("@MaTaiKhoan", hd.MaTaiKhoan ?? (object)DBNull.Value),
                    new SqlParameter("@ID_Xe", hd.ID_Xe ?? (object)DBNull.Value),
                    new SqlParameter("@NgayLap", hd.NgayLap),
                    new SqlParameter("@NgayBatDau", hd.NgayBatDau),
                    new SqlParameter("@NgayKetThuc", hd.NgayKetThuc),
                    new SqlParameter("@GiaThue", hd.GiaThue),
                    new SqlParameter("@TienDatCoc", (object)hd.TienDatCoc ?? DBNull.Value),
                    new SqlParameter("@DieuKhoan", (object)hd.DieuKhoan ?? DBNull.Value),
                    new SqlParameter("@GhiChu", (object)hd.GhiChu ?? DBNull.Value),
                    new SqlParameter("@TrangThaiHopDong", hd.TrangThaiHopDong ?? (object)DBNull.Value),
                    new SqlParameter("@FileHopDong", (object)hd.FileHopDong ?? DBNull.Value)
                };

                //  Log parameters
                System.Diagnostics.Debug.WriteLine("[DAL] Executing INSERT HopDongThue:");
                foreach (var p in parameters)
                {
                    System.Diagnostics.Debug.WriteLine($"  {p.ParameterName} = {p.Value}");
                }

                int result = DataProvider.ExecuteNonQuery(query, parameters);
                
                //  Ghi log thành công
                if (result > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"[SUCCESS] Đã insert HopDongThue cho GD #{hd.MaGDThue}, rows affected: {result}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[WARN] Insert HopDongThue trả về 0 rows affected");
                }
                
                return result > 0;
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine($"[SQL ERROR] InsertHopDongThue:");
                System.Diagnostics.Debug.WriteLine($"  Message: {sqlEx.Message}");
                System.Diagnostics.Debug.WriteLine($"  Number: {sqlEx.Number}");
                System.Diagnostics.Debug.WriteLine($"  State: {sqlEx.State}");
                System.Diagnostics.Debug.WriteLine($"  StackTrace: {sqlEx.StackTrace}");
                
                //  Phân tích lỗi cụ thể
                if (sqlEx.Number == 547) // Foreign key violation
                {
                    throw new Exception($"Lỗi ràng buộc khóa ngoại: {sqlEx.Message}", sqlEx);
                }
                else if (sqlEx.Number == 2627 || sqlEx.Number == 2601) // Duplicate key
                {
                    throw new Exception($"Dữ liệu bị trùng lặp: {sqlEx.Message}", sqlEx);
                }
                else if (sqlEx.Number == 8152) // String truncation
                {
                    throw new Exception($"Dữ liệu quá dài: {sqlEx.Message}", sqlEx);
                }
                else
                {
                    throw new Exception($"Lỗi SQL: {sqlEx.Message}", sqlEx);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ERROR] InsertHopDongThue: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Lấy hợp đồng theo mã giao dịch thuê
        /// </summary>
        public DataTable GetHopDongByMaGDThue(int maGDThue)
        {
            string query = @"
                SELECT 
                    hd.*,
                    kh.HoTenKH,
                    kh.Sdt,
                    kh.SoCCCD,
                    kh.DiaChi,
                    nv.HoTenNV AS TenNhanVien,
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau) AS TenXe,
                    xe.BienSo,
                    hx.TenHang AS TenHangXe,
                    dx.TenDong AS TenDongXe,
                    ms.TenMau AS TenMauSac,
                    lx.NamSX AS NamSanXuat,
                    dx.PhanKhoi,
                    dx.LoaiXe,
                    xe.ThongTinXang
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

        /// <summary>
        /// Cập nhật trạng thái hợp đồng
        /// </summary>
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

        /// <summary>
        /// Kiểm tra xem giao dịch đã có hợp đồng chưa
        /// </summary>
        public bool IsHopDongExists(int maGDThue)
        {
            string query = "SELECT COUNT(*) FROM HopDongThue WHERE MaGDThue = @MaGDThue";
            SqlParameter[] parameters = { new SqlParameter("@MaGDThue", maGDThue) };
            
            try
            {
                int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
                System.Diagnostics.Debug.WriteLine($"[DAL] IsHopDongExists(GD #{maGDThue}): {count > 0}");
                return count > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ERROR] IsHopDongExists: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lấy tất cả hợp đồng thuê xe
        /// </summary>
        public DataTable GetAllHopDongThue()
        {
            string query = "SELECT * FROM HopDongThue";
            return DataProvider.ExecuteQuery(query);
        }
    }
}