using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class KhachHangDAL
    {
        /// <summary>
        /// Lấy tất cả khách hàng
        /// </summary>
        public DataTable GetAllKhachHang()
        {
            string query = @"SELECT MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                            NgayTao, NgayCapNhat, SoCCCD, LoaiGiayTo, AnhGiayTo 
                            FROM KhachHang 
                            ORDER BY MaKH DESC";
            return DataProvider.ExecuteQuery(query);
        }

        /// <summary>
        /// Tìm kiếm khách hàng theo nhiều tiêu chí
        /// </summary>
        public DataTable SearchKhachHang(string searchBy, string keyword)
        {
            string query = "";
            SqlParameter[] parameters = null;

            switch (searchBy)
            {
                case "Mã khách hàng":
                    query = @"SELECT MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                             NgayTao, NgayCapNhat, SoCCCD, LoaiGiayTo, AnhGiayTo 
                             FROM KhachHang 
                             WHERE MaKH LIKE @keyword 
                             ORDER BY MaKH DESC";
                    break;
                case "Họ và tên":
                    query = @"SELECT MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                             NgayTao, NgayCapNhat, SoCCCD, LoaiGiayTo, AnhGiayTo 
                             FROM KhachHang 
                             WHERE HoTenKH LIKE @keyword 
                             ORDER BY MaKH DESC";
                    break;
                case "Số điện thoại":
                    query = @"SELECT MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                             NgayTao, NgayCapNhat, SoCCCD, LoaiGiayTo, AnhGiayTo 
                             FROM KhachHang 
                             WHERE Sdt LIKE @keyword 
                             ORDER BY MaKH DESC";
                    break;
                case "Email":
                    query = @"SELECT MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                             NgayTao, NgayCapNhat, SoCCCD, LoaiGiayTo, AnhGiayTo 
                             FROM KhachHang 
                             WHERE Email LIKE @keyword 
                             ORDER BY MaKH DESC";
                    break;
                default:
                    return GetAllKhachHang();
            }

            parameters = new SqlParameter[]
            {
                new SqlParameter("@keyword", "%" + keyword + "%")
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Lấy thông tin khách hàng theo mã
        /// </summary>
        public DataTable GetKhachHangByMaKH(string maKH)
        {
            string query = @"SELECT MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                            NgayTao, NgayCapNhat, SoCCCD, LoaiGiayTo, AnhGiayTo 
                            FROM KhachHang 
                            WHERE MaKH = @MaKH";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH)
            };
            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Thêm khách hàng mới
        /// </summary>
        public bool InsertKhachHang(KhachHangDTO kh)
        {
            string query = @"INSERT INTO KhachHang (MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, 
                            Email, DiaChi, NgayTao, NgayCapNhat, SoCCCD, LoaiGiayTo, AnhGiayTo)
                            VALUES (@MaKH, @HoTenKH, @NgaySinh, @GioiTinh, @Sdt, 
                            @Email, @DiaChi, GETDATE(), GETDATE(), @SoCCCD, @LoaiGiayTo, @AnhGiayTo)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", kh.MaKH),
                new SqlParameter("@HoTenKH", kh.HoTenKH),
                new SqlParameter("@NgaySinh", (object)kh.NgaySinh ?? DBNull.Value),
                new SqlParameter("@GioiTinh", (object)kh.GioiTinh ?? DBNull.Value),
                new SqlParameter("@Sdt", (object)kh.Sdt ?? DBNull.Value),
                new SqlParameter("@Email", (object)kh.Email ?? DBNull.Value),
                new SqlParameter("@DiaChi", (object)kh.DiaChi ?? DBNull.Value),
                new SqlParameter("@SoCCCD", (object)kh.SoCCCD ?? DBNull.Value),
                new SqlParameter("@LoaiGiayTo", (object)kh.LoaiGiayTo ?? DBNull.Value),
                new SqlParameter("@AnhGiayTo", (object)kh.AnhGiayTo ?? DBNull.Value)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        public bool UpdateKhachHang(KhachHangDTO kh)
        {
            string query = @"UPDATE KhachHang 
                            SET HoTenKH = @HoTenKH, 
                                NgaySinh = @NgaySinh, 
                                GioiTinh = @GioiTinh, 
                                Sdt = @Sdt, 
                                Email = @Email, 
                                DiaChi = @DiaChi, 
                                SoCCCD = @SoCCCD,
                                LoaiGiayTo = @LoaiGiayTo,
                                AnhGiayTo = @AnhGiayTo,
                                NgayCapNhat = GETDATE()
                            WHERE MaKH = @MaKH";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", kh.MaKH),
                new SqlParameter("@HoTenKH", kh.HoTenKH),
                new SqlParameter("@NgaySinh", (object)kh.NgaySinh ?? DBNull.Value),
                new SqlParameter("@GioiTinh", (object)kh.GioiTinh ?? DBNull.Value),
                new SqlParameter("@Sdt", (object)kh.Sdt ?? DBNull.Value),
                new SqlParameter("@Email", (object)kh.Email ?? DBNull.Value),
                new SqlParameter("@DiaChi", (object)kh.DiaChi ?? DBNull.Value),
                new SqlParameter("@SoCCCD", (object)kh.SoCCCD ?? DBNull.Value),
                new SqlParameter("@LoaiGiayTo", (object)kh.LoaiGiayTo ?? DBNull.Value),
                new SqlParameter("@AnhGiayTo", (object)kh.AnhGiayTo ?? DBNull.Value)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        public bool DeleteKhachHang(string maKH)
        {
            string query = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH)
            };
            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Kiểm tra mã khách hàng đã tồn tại
        /// </summary>
        public bool CheckMaKHExists(string maKH)
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE MaKH = @MaKH";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH)
            };
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        /// <summary>
        /// Kiểm tra số điện thoại đã tồn tại
        /// </summary>
        public bool CheckSdtExists(string sdt, string maKH = null)
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE Sdt = @Sdt";
            if (!string.IsNullOrEmpty(maKH))
            {
                query += " AND MaKH != @MaKH";
            }

            SqlParameter[] parameters;
            if (!string.IsNullOrEmpty(maKH))
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Sdt", sdt),
                    new SqlParameter("@MaKH", maKH)
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
        /// Kiểm tra email đã tồn tại
        /// </summary>
        public bool CheckEmailExists(string email, string maKH = null)
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE Email = @Email";
            if (!string.IsNullOrEmpty(maKH))
            {
                query += " AND MaKH != @MaKH";
            }

            SqlParameter[] parameters;
            if (!string.IsNullOrEmpty(maKH))
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Email", email),
                    new SqlParameter("@MaKH", maKH)
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
        /// Tự động tạo mã khách hàng mới
        /// </summary>
        public string GenerateMaKH()
        {
            string query = "SELECT TOP 1 MaKH FROM KhachHang ORDER BY MaKH DESC";
            object result = DataProvider.ExecuteScalar(query);

            if (result == null || result == DBNull.Value)
            {
                return "KH00000001";
            }

            string lastMaKH = result.ToString();
            int number = int.Parse(lastMaKH.Substring(2));
            number++;
            return "KH" + number.ToString("D8");
        }

        /// <summary>
        /// Lấy khách hàng theo số điện thoại
        /// </summary>
        public KhachHangDTO GetKhachHangBySdt(string sdt)
        {
            string query = @"SELECT MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, 
                            NgayTao, NgayCapNhat, SoCCCD, LoaiGiayTo, AnhGiayTo 
                            FROM KhachHang 
                            WHERE Sdt = @Sdt";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Sdt", sdt)
            };

            DataTable dt = DataProvider.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new KhachHangDTO
                {
                    MaKH = row["MaKH"].ToString(),
                    HoTenKH = row["HoTenKH"].ToString(),
                    NgaySinh = row["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(row["NgaySinh"]) : (DateTime?)null,
                    GioiTinh = row["GioiTinh"] != DBNull.Value ? row["GioiTinh"].ToString() : null,
                    Sdt = row["Sdt"] != DBNull.Value ? row["Sdt"].ToString() : null,
                    Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                    DiaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : null,
                    SoCCCD = row["SoCCCD"] != DBNull.Value ? row["SoCCCD"].ToString() : null,
                    LoaiGiayTo = row["LoaiGiayTo"] != DBNull.Value ? row["LoaiGiayTo"].ToString() : null,
                    AnhGiayTo = row["AnhGiayTo"] != DBNull.Value ? (byte[])row["AnhGiayTo"] : null,
                    NgayTao = Convert.ToDateTime(row["NgayTao"]),
                    NgayCapNhat = Convert.ToDateTime(row["NgayCapNhat"])
                };
            }

            return null;
        }

        /// <summary>
        /// Kiểm tra số điện thoại đã tồn tại trong bảng NhanVien
        /// </summary>
        public bool CheckSdtExistsInNhanVien(string sdt)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE Sdt = @Sdt";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Sdt", sdt)
            };
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            return count > 0;
        }

        // ========== THÊM CÁC PHƯƠNG THỨC MỚI CHO VALIDATION ==========

        /// <summary>
        /// Kiểm tra SDT tồn tại (thêm mới) - Tương thích với tên method trong BLL
        /// </summary>
        public bool IsSDTExists(string sdt)
        {
            try
            {
                // Sử dụng lại method CheckSdtExists đã có
                return CheckSdtExists(sdt);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi kiểm tra SDT tồn tại: {ex.Message}");
            }
        }

        /// <summary>
        /// Kiểm tra SDT tồn tại (sửa - loại trừ KH hiện tại) - Tương thích với tên method trong BLL
        /// </summary>
        public bool IsSDTExists(string sdt, string excludeMaKH)
        {
            try
            {
                // Sử dụng lại method CheckSdtExists đã có
                return CheckSdtExists(sdt, excludeMaKH);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi kiểm tra SDT tồn tại: {ex.Message}");
            }
        }

        /// <summary>
        /// Kiểm tra Email tồn tại (thêm mới) - Tương thích với tên method trong BLL
        /// </summary>
        public bool IsEmailExists(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                // Sử dụng lại method CheckEmailExists đã có
                return CheckEmailExists(email);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi kiểm tra Email tồn tại: {ex.Message}");
            }
        }

        /// <summary>
        /// Kiểm tra Email tồn tại (sửa - loại trừ KH hiện tại) - Tương thích với tên method trong BLL
        /// </summary>
        public bool IsEmailExists(string email, string excludeMaKH)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                // Sử dụng lại method CheckEmailExists đã có
                return CheckEmailExists(email, excludeMaKH);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi kiểm tra Email tồn tại: {ex.Message}");
            }
        }

        /// <summary>
        /// Kiểm tra khách hàng có đang trong giao dịch thuê không
        /// </summary>
        public bool IsKhachHangInGiaoDichThue(string maKH)
        {
            try
            {
                string query = @"SELECT COUNT(*) FROM GiaoDichThue 
                                WHERE MaKH = @MaKH 
                                AND TrangThai IN (N'Chờ duyệt', N'Đã thanh toán', N'Đang thuê')";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKH", maKH)
                };

                int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi kiểm tra giao dịch thuê: {ex.Message}");
            }
        }

        /// <summary>
        /// Kiểm tra khách hàng có đang trong giao dịch bán không
        /// </summary>
        public bool IsKhachHangInGiaoDichBan(string maKH)
        {
            try
            {
                string query = @"SELECT COUNT(*) FROM GiaoDichBan 
                                WHERE MaKH = @MaKH 
                                AND TrangThai = N'Chờ duyệt'";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKH", maKH)
                };

                int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi kiểm tra giao dịch bán: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy thống kê khách hàng theo giới tính
        /// </summary>
        public DataTable GetThongKeTheoGioiTinh()
        {
            try
            {
                string query = @"SELECT GioiTinh, COUNT(*) AS SoLuong
                                FROM KhachHang
                                WHERE GioiTinh IS NOT NULL
                                GROUP BY GioiTinh";

                return DataProvider.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy thống kê giới tính: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy top khách hàng thuê nhiều nhất
        /// </summary>
        public DataTable GetTopKhachHangThueXe(int topN = 10)
        {
            try
            {
                string query = @"SELECT TOP (@TopN) 
                                    kh.MaKH, 
                                    kh.HoTenKH, 
                                    kh.Sdt, 
                                    COUNT(gd.MaGiaoDich) AS SoLanThue,
                                    SUM(gd.TongTien) AS TongChiTieu
                                FROM KhachHang kh
                                INNER JOIN GiaoDichThue gd ON kh.MaKH = gd.MaKH
                                WHERE gd.TrangThai = N'Đã hoàn thành'
                                GROUP BY kh.MaKH, kh.HoTenKH, kh.Sdt
                                ORDER BY SoLanThue DESC, TongChiTieu DESC";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TopN", topN)
                };

                return DataProvider.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy top khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy lịch sử giao dịch của khách hàng
        /// </summary>
        public DataTable GetLichSuGiaoDichKhachHang(string maKH)
        {
            try
            {
                string query = @"SELECT 
                                    'Thuê xe' AS LoaiGiaoDich,
                                    gd.MaGiaoDich,
                                    gd.NgayBatDau AS NgayGiaoDich,
                                    gd.TongTien,
                                    gd.TrangThai
                                FROM GiaoDichThue gd
                                WHERE gd.MaKH = @MaKH
                                UNION ALL
                                SELECT 
                                    'Mua xe' AS LoaiGiaoDich,
                                    gdb.MaGiaoDich,
                                    gdb.NgayBan AS NgayGiaoDich,
                                    gdb.TongTien,
                                    gdb.TrangThai
                                FROM GiaoDichBan gdb
                                WHERE gdb.MaKH = @MaKH
                                ORDER BY NgayGiaoDich DESC";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKH", maKH)
                };

                return DataProvider.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy lịch sử giao dịch: {ex.Message}");
            }
        }

        /// <summary>
        /// Đếm số lượng khách hàng mới trong khoảng thời gian
        /// </summary>
        public int CountKhachHangMoi(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                string query = @"SELECT COUNT(*) FROM KhachHang 
                                WHERE NgayTao >= @TuNgay AND NgayTao <= @DenNgay";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay)
                };

                return Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi đếm khách hàng mới: {ex.Message}");
            }
        }

        /// <summary>
        /// Validate dữ liệu khách hàng trước khi lưu
        /// </summary>
        public bool ValidateKhachHang(KhachHangDTO kh, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // Kiểm tra họ tên
                if (string.IsNullOrWhiteSpace(kh.HoTenKH))
                {
                    errorMessage = "Họ tên không được để trống!";
                    return false;
                }

                if (kh.HoTenKH.Length < 2)
                {
                    errorMessage = "Họ tên phải có ít nhất 2 ký tự!";
                    return false;
                }

                // Kiểm tra số điện thoại
                if (string.IsNullOrWhiteSpace(kh.Sdt))
                {
                    errorMessage = "Số điện thoại không được để trống!";
                    return false;
                }

                if (kh.Sdt.Length < 10 || kh.Sdt.Length > 11)
                {
                    errorMessage = "Số điện thoại phải có 10-11 số!";
                    return false;
                }

                // Kiểm tra ngày sinh (nếu có)
                if (kh.NgaySinh.HasValue)
                {
                    int tuoi = DateTime.Now.Year - kh.NgaySinh.Value.Year;
                    if (DateTime.Now.Month < kh.NgaySinh.Value.Month ||
                        (DateTime.Now.Month == kh.NgaySinh.Value.Month && DateTime.Now.Day < kh.NgaySinh.Value.Day))
                    {
                        tuoi--;
                    }

                    if (tuoi < 18)
                    {
                        errorMessage = "Khách hàng phải đủ 18 tuổi!";
                        return false;
                    }

                    if (tuoi > 120)
                    {
                        errorMessage = "Ngày sinh không hợp lệ!";
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi validate: {ex.Message}";
                return false;
            }
        }


    }
}