using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class NhanVienDAL
    {
        
        /// Lấy tất cả nhân viên
        public DataTable GetAllNhanVien()
        {
            string query = @"SELECT MaNV, HoTenNV, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                     ChucVu, LuongCoBan, TinhTrangLamViec, CCCD, TrinhDoHocVan, 
                     AnhNhanVien, NgayVaoLam  -- Đảm bảo có AnhNhanVien
                     FROM NhanVien 
                     ORDER BY MaNV";
            return DataProvider.ExecuteQuery(query);
        }

        
        /// Tìm kiếm nhân viên theo nhiều tiêu chí
        public DataTable SearchNhanVien(string searchBy, string keyword)
        {
            string query = "";
            SqlParameter[] parameters = null;

            switch (searchBy)
            {
                case "Mã nhân viên":
                    query = @"SELECT MaNV, HoTenNV, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                             ChucVu, LuongCoBan, TinhTrangLamViec, CCCD, TrinhDoHocVan, 
                             AnhNhanVien, NgayVaoLam 
                             FROM NhanVien 
                             WHERE MaNV LIKE @keyword 
                             ORDER BY MaNV";
                    break;
                case "Họ và tên":
                    query = @"SELECT MaNV, HoTenNV, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                             ChucVu, LuongCoBan, TinhTrangLamViec, CCCD, TrinhDoHocVan, 
                             AnhNhanVien, NgayVaoLam 
                             FROM NhanVien 
                             WHERE HoTenNV LIKE @keyword 
                             ORDER BY MaNV";
                    break;
                case "Số điện thoại":
                    query = @"SELECT MaNV, HoTenNV, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                             ChucVu, LuongCoBan, TinhTrangLamViec, CCCD, TrinhDoHocVan, 
                             AnhNhanVien, NgayVaoLam 
                             FROM NhanVien 
                             WHERE Sdt LIKE @keyword 
                             ORDER BY MaNV";
                    break;
                case "Email":
                    query = @"SELECT MaNV, HoTenNV, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                             ChucVu, LuongCoBan, TinhTrangLamViec, CCCD, TrinhDoHocVan, 
                             AnhNhanVien, NgayVaoLam 
                             FROM NhanVien 
                             WHERE Email LIKE @keyword 
                             ORDER BY MaNV";
                    break;
                default:
                    return GetAllNhanVien();
            }

            parameters = new SqlParameter[]
            {
                new SqlParameter("@keyword", "%" + keyword + "%")
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        
        /// Lấy thông tin nhân viên theo mã
        public DataTable GetNhanVienByMaNV(string maNV)
        {
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
            string query = @"INSERT INTO NhanVien (MaNV, HoTenNV, NgaySinh, GioiTinh, Sdt, Email, 
                            DiaChi, ChucVu, LuongCoBan, TinhTrangLamViec, CCCD, TrinhDoHocVan, 
                            AnhNhanVien, NgayVaoLam)
                            VALUES (@MaNV, @HoTenNV, @NgaySinh, @GioiTinh, @Sdt, @Email, 
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

        
        /// Cập nhật thông tin nhân viên
       
        public bool UpdateNhanVien(NhanVien nv)
        {
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
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        
        /// Xóa nhân viên
        public bool DeleteNhanVien(string maNV)
        {
            string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV)
            };
            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        
        /// Kiểm tra mã nhân viên đã tồn tại
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

        
        /// Kiểm tra số điện thoại đã tồn tại
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

        /// Kiểm tra email đã tồn tại
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

        
        /// Kiểm tra CCCD đã tồn tại
       
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

        
        /// Tự động tạo mã nhân viên mới
        public string GenerateMaNV()
        {
            string query = "SELECT TOP 1 MaNV FROM NhanVien ORDER BY MaNV DESC";
            object result = DataProvider.ExecuteScalar(query);

            if (result == null || result == DBNull.Value)
            {
                return "NV00000001";
            }

            string lastMaNV = result.ToString();
            int number = int.Parse(lastMaNV.Substring(2));
            number++;
            return "NV" + number.ToString("D8");
        }
    }
}

