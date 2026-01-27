using DTO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class NhanVienDAL
    {
       
        /// Lấy tất cả nhân viên
        public DataTable GetAllNhanVien()
        {
            string query = @"SELECT MaNV, HoTenNV, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                     ChucVu, LuongCoBan, TinhTrangLamViec, CCCD, TrinhDoHocVan, 
                     AnhNhanVien, NgayVaoLam
                     FROM NhanVien 
                     ORDER BY MaNV";
            return DataProvider.ExecuteQuery(query);
        }

        /// Tìm kiếm nhân viên theo nhiều tiêu chí
        public DataTable SearchNhanVien(string searchBy, string keyword)
        {
            // Chỉ cho phép search các trường hợp lệ
            string[] VALID_FIELDS = { "MaNV", "HoTenNV", "Sdt", "Email", "ChucVu" };
            if (!VALID_FIELDS.Contains(searchBy))
                return GetAllNhanVien();

            string query = $@"SELECT MaNV, HoTenNV, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                    ChucVu, LuongCoBan, TinhTrangLamViec, CCCD, TrinhDoHocVan, 
                    AnhNhanVien, NgayVaoLam
             FROM NhanVien 
             WHERE {searchBy} LIKE @keyword
             ORDER BY MaNV";
            SqlParameter[] parameters = new SqlParameter[] {
        new SqlParameter("@keyword", "%" + keyword + "%")
    };
            return DataProvider.ExecuteQuery(query, parameters);
        }


        /// <summary>
        /// Lấy thông tin nhân viên theo mã
        /// </summary>
        public DataTable GetNhanVienByMaNV(string maNV)
        {
            // ✅ BỎ CỘT Password VÌ KHÔNG TỒN TẠI TRONG BẢNG NhanVien
            string query = @"SELECT MaNV, HoTenNV, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                            ChucVu, LuongCoBan, TinhTrangLamViec, CCCD, TrinhDoHocVan, 
                            AnhNhanVien, NgayVaoLam
                            FROM NhanVien 
                            WHERE MaNV = @MaNV";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV)
            };
            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// Thêm nhân viên mới
        public bool InsertNhanVien(NhanVien nv)
        {
            string query = @"INSERT INTO NhanVien 
                    (MaNV, HoTenNV, NgaySinh, GioiTinh, Sdt, Email, 
                    DiaChi, ChucVu, LuongCoBan, TinhTrangLamViec, CCCD, TrinhDoHocVan, 
                    AnhNhanVien, NgayVaoLam)
                    VALUES 
                    (@MaNV, @HoTenNV, @NgaySinh, @GioiTinh, @Sdt, @Email, 
                    @DiaChi, @ChucVu, @LuongCoBan, @TinhTrangLamViec, @CCCD, @TrinhDoHocVan, 
                    @AnhNhanVien, @NgayVaoLam)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", nv.MaNV),
                new SqlParameter("@HoTenNV", nv.HoTenNV),
                new SqlParameter("@NgaySinh", nv.NgaySinh),
                new SqlParameter("@GioiTinh", nv.GioiTinh),
                new SqlParameter("@Sdt", (object)nv.Sdt ?? DBNull.Value),
                new SqlParameter("@Email", (object)nv.Email ?? DBNull.Value),
                new SqlParameter("@DiaChi", (object)nv.DiaChi ?? DBNull.Value),
                new SqlParameter("@ChucVu", (object)nv.ChucVu ?? DBNull.Value),
                new SqlParameter("@LuongCoBan", (object)nv.LuongCoBan ?? DBNull.Value),
                new SqlParameter("@TinhTrangLamViec", nv.TinhTrangLamViec),
                new SqlParameter("@CCCD", (object)nv.CCCD ?? DBNull.Value),
                new SqlParameter("@TrinhDoHocVan", (object)nv.TrinhDoHocVan ?? DBNull.Value),
                new SqlParameter("@AnhNhanVien", (object)nv.AnhNhanVien ?? DBNull.Value),
                new SqlParameter("@NgayVaoLam", (object)nv.NgayVaoLam ?? DateTime.Now)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        public bool UpdateNhanVien(NhanVien nv)
        {
            // ✅ BỎ DÒNG password = @Password VÌ KHÔNG CÓ CỘT NÀY
            string query = @"UPDATE NhanVien 
                            SET HoTenNV = @HoTenNV, 
                                NgaySinh = @NgaySinh, 
                                GioiTinh = @GioiTinh, 
                                Sdt = @Sdt, 
                                Email = @Email, 
                                DiaChi = @DiaChi, 
                                ChucVu = @ChucVu, 
                                LuongCoBan = @LuongCoBan, 
                                TinhTrangLamViec = @TinhTrangLamViec, 
                                CCCD = @CCCD, 
                                TrinhDoHocVan = @TrinhDoHocVan, 
                                AnhNhanVien = @AnhNhanVien
                            WHERE MaNV = @MaNV";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", nv.MaNV),
                new SqlParameter("@HoTenNV", nv.HoTenNV),
                new SqlParameter("@NgaySinh", nv.NgaySinh),
                new SqlParameter("@GioiTinh", nv.GioiTinh),
                new SqlParameter("@Sdt", (object)nv.Sdt ?? DBNull.Value),
                new SqlParameter("@Email", (object)nv.Email ?? DBNull.Value),
                new SqlParameter("@DiaChi", (object)nv.DiaChi ?? DBNull.Value),
                new SqlParameter("@ChucVu", (object)nv.ChucVu ?? DBNull.Value),
                new SqlParameter("@LuongCoBan", (object)nv.LuongCoBan ?? DBNull.Value),
                new SqlParameter("@TinhTrangLamViec", nv.TinhTrangLamViec),
                new SqlParameter("@CCCD", (object)nv.CCCD ?? DBNull.Value),
                new SqlParameter("@TrinhDoHocVan", (object)nv.TrinhDoHocVan ?? DBNull.Value),
                new SqlParameter("@AnhNhanVien", (object)nv.AnhNhanVien ?? DBNull.Value)
                // ✅ BỎ @Password VÌ KHÔNG CÓ CỘT NÀY TRONG BẢNG NhanVien
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        public bool DeleteNhanVien(string maNV)
        {
            string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV)
            };
            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Kiểm tra mã nhân viên đã tồn tại
        /// </summary>
        public bool CheckMaNVExists(string maNV)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV)
            };
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        /// <summary>
        /// Kiểm tra số điện thoại đã tồn tại
        /// </summary>
        public bool CheckSdtExists(string sdt, string maNV = null)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE Sdt = @Sdt";
            if (!string.IsNullOrEmpty(maNV))
            {
                query += " AND MaNV != @MaNV";
            }

            SqlParameter[] parameters;
            if (!string.IsNullOrEmpty(maNV))
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Sdt", sdt),
                    new SqlParameter("@MaNV", maNV)
                };
            }
            else
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Sdt", sdt)
                };
            }

            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        /// <summary>
        /// Kiểm tra số điện thoại đã tồn tại trong bảng KhachHang
        /// </summary>
        public bool CheckSdtExistsInKhachHang(string sdt)
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE Sdt = @Sdt";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Sdt", sdt)
            };
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        /// <summary>
        /// Kiểm tra email đã tồn tại
        /// </summary>
        public bool CheckEmailExists(string email, string maNV = null)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE Email = @Email";
            if (!string.IsNullOrEmpty(maNV))
            {
                query += " AND MaNV != @MaNV";
            }

            SqlParameter[] parameters;
            if (!string.IsNullOrEmpty(maNV))
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Email", email),
                    new SqlParameter("@MaNV", maNV)
                };
            }
            else
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Email", email)
                };
            }

            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        /// <summary>
        /// Kiểm tra CCCD đã tồn tại
        /// </summary>
        public bool CheckCCCDExists(string cccd, string maNV = null)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE CCCD = @CCCD";
            if (!string.IsNullOrEmpty(maNV))
            {
                query += " AND MaNV != @MaNV";
            }

            SqlParameter[] parameters;
            if (!string.IsNullOrEmpty(maNV))
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@CCCD", cccd),
                    new SqlParameter("@MaNV", maNV)
                };
            }
            else
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@CCCD", cccd)
                };
            }

            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        /// <summary>
        /// Tự động tạo mã nhân viên mới (đảm bảo không trùng)
        /// </summary>
        public string GenerateMaNV()
        {
            try
            {
                string query = "SELECT TOP 1 MaNV FROM NhanVien ORDER BY MaNV DESC";
                object result = DataProvider.ExecuteScalar(query);

                int number = 1;
                if (result != null && result != DBNull.Value)
                {
                    string lastMaNV = result.ToString().Trim();
                    if (lastMaNV.Length >= 2 && lastMaNV.StartsWith("NV"))
                    {
                        string numberPart = lastMaNV.Substring(2);
                        if (int.TryParse(numberPart, out int lastNumber))
                        {
                            number = lastNumber + 1;
                        }
                    }
                }

                // Kiểm tra mã mới có trùng không, nếu trùng thì tăng lên
                string newMaNV;
                int maxAttempts = 1000; // Giới hạn số lần thử để tránh vòng lặp vô hạn
                int attempts = 0;
                
                do
                {
                    newMaNV = "NV" + number.ToString("D8");
                    
                    // Kiểm tra mã đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV";
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@MaNV", newMaNV)
                    };
                    
                    int count = Convert.ToInt32(DataProvider.ExecuteScalar(checkQuery, parameters));
                    
                    if (count == 0)
                    {
                        // Mã không trùng, trả về
                        return newMaNV;
                    }
                    
                    // Mã bị trùng, tăng số lên
                    number++;
                    attempts++;
                    
                } while (attempts < maxAttempts);

                // Nếu vẫn không tìm được mã duy nhất sau maxAttempts lần thử
                // Sử dụng timestamp để tạo mã unique
                number = (int)(DateTime.Now.Ticks % 100000000);
                return "NV" + number.ToString("D8");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"❌ Lỗi GenerateMaNV: {ex.Message}");
                // Trường hợp lỗi, trả về mã với timestamp
                int number = (int)(DateTime.Now.Ticks % 100000000);
                return "NV" + number.ToString("D8");
            }
        }
    }
}