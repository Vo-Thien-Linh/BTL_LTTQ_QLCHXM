using System;
using System.Data;
using System.Data.SqlClient;
using DAL;
using DTO;

namespace BLL
{
    public class GiaoDichThueBLL
    {
        private GiaoDichThueDAL giaoDichThueDAL;
        private GiaoDichTraThueDAL giaoDichTraThueDAL;

        public GiaoDichThueBLL()
        {
            giaoDichThueDAL = new GiaoDichThueDAL();
            giaoDichTraThueDAL = new GiaoDichTraThueDAL();
        }

        #region Lấy dữ liệu

        /// <summary>
        /// Lấy tất cả giao dịch thuê
        /// </summary>
        public DataTable GetAllGiaoDichThue()
        {
            return giaoDichThueDAL.GetAllGiaoDichThue();
        }

        /// <summary>
        /// Lấy giao dịch thuê theo trạng thái duyệt
        /// </summary>
        public DataTable GetGiaoDichThueByTrangThai(string trangThaiDuyet)
        {
            return giaoDichThueDAL.GetGiaoDichThueByTrangThai(trangThaiDuyet);
        }

        /// <summary>
        /// Lấy đơn cho thuê đã duyệt (dành cho quản lý cho thuê)
        /// Bao gồm cả tính toán số ngày quá hạn
        /// </summary>
        public DataTable GetDonChoThue()
        {
            string query = @"
                SELECT 
                    gd.MaGDThue,
                    gd.MaKH,
                    kh.HoTenKH,
                    kh.Sdt AS SdtKhachHang,
                    kh.Email AS EmailKhachHang,
                    gd.ID_Xe,
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau) AS TenXe,
                    xe.BienSo,
                    xe.TrangThai AS TrangThaiXe,
                    xe.AnhXeXeBan,
                    gd.NgayBatDau,
                    gd.NgayKetThuc,
                    DATEDIFF(DAY, gd.NgayBatDau, gd.NgayKetThuc) AS SoNgayThue,
                    gd.GiaThueNgay,
                    gd.TongGia,
                    gd.TrangThai,
                    gd.TrangThaiThanhToan,
                    gd.HinhThucThanhToan,
                    gd.SoTienCoc,
                    gd.GiayToGiuLai,
                    gd.TrangThaiDuyet,
                    CASE 
                        WHEN gd.NgayKetThuc < GETDATE() AND gd.TrangThai = N'Đang thuê' 
                        THEN DATEDIFF(DAY, gd.NgayKetThuc, GETDATE())
                        ELSE 0 
                    END AS SoNgayQuaHan,
                    CASE 
                        WHEN gd.NgayKetThuc < GETDATE() AND gd.TrangThai = N'Đang thuê' 
                        THEN DATEDIFF(DAY, gd.NgayKetThuc, GETDATE()) * gd.GiaThueNgay * 1.5
                        ELSE 0 
                    END AS TienPhatDuKien,
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
                WHERE gd.TrangThaiDuyet = N'Đã duyệt'
                ORDER BY 
                    CASE 
                        WHEN gd.NgayKetThuc < GETDATE() AND gd.TrangThai = N'Đang thuê' THEN 0 
                        ELSE 1 
                    END,
                    gd.NgayBatDau DESC";

            return DataProvider.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy đơn quá hạn (chưa trả xe)
        /// </summary>
        public DataTable GetDonQuaHan()
        {
            string query = @"
                SELECT 
                    gd.*,
                    kh.HoTenKH,
                    kh.Sdt AS SdtKhachHang,
                    CONCAT(hx.TenHang, ' ', dx.TenDong) AS TenXe,
                    xe.BienSo,
                    DATEDIFF(DAY, gd.NgayKetThuc, GETDATE()) AS SoNgayQuaHan,
                    DATEDIFF(DAY, gd.NgayKetThuc, GETDATE()) * gd.GiaThueNgay * 1.5 AS TienPhat
                FROM GiaoDichThue gd
                INNER JOIN KhachHang kh ON gd.MaKH = kh.MaKH
                INNER JOIN XeMay xe ON gd.ID_Xe = xe.ID_Xe
                INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                WHERE gd.TrangThai = N'Đang thuê'
                  AND gd.NgayKetThuc < GETDATE()
                ORDER BY SoNgayQuaHan DESC";

            return DataProvider.ExecuteQuery(query);
        }

        /// <summary>
        /// Lấy giao dịch thuê theo ID
        /// </summary>
        public DataTable GetGiaoDichThueById(int maGDThue)
        {
            DataTable dt = GetAllGiaoDichThue();
            DataView dv = dt.DefaultView;
            dv.RowFilter = $"MaGDThue = {maGDThue}";
            return dv.ToTable();
        }

        #endregion

        #region Thêm/Sửa giao dịch

        /// <summary>
        /// Thêm giao dịch thuê mới
        /// </summary>
        public bool InsertGiaoDichThue(GiaoDichThue gd, out string errorMessage)
        {
            errorMessage = "";

            // Validate dữ liệu
            if (!ValidateGiaoDichThue(gd, out errorMessage))
            {
                return false;
            }

            try
            {
                return giaoDichThueDAL.InsertGiaoDichThue(gd);
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi thêm giao dịch thuê: " + ex.Message;
                return false;
            }
        }

        #endregion

        #region Duyệt đơn thuê

        /// <summary>
        /// Duyệt đơn thuê xe
        /// </summary>
        public bool ApproveGiaoDichThue(int maGDThue, string nguoiDuyet, string ghiChu, out string errorMessage)
        {
            errorMessage = "";

            if (maGDThue <= 0)
            {
                errorMessage = "Mã giao dịch không hợp lệ!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(nguoiDuyet))
            {
                errorMessage = "Người duyệt không được để trống!";
                return false;
            }

            try
            {
                DataTable dt = giaoDichThueDAL.GetAllGiaoDichThue();
                DataRow[] rows = dt.Select($"MaGDThue = {maGDThue}");

                if (rows.Length == 0)
                {
                    errorMessage = "Không tìm thấy giao dịch!";
                    return false;
                }

                string trangThaiHienTai = rows[0]["TrangThaiDuyet"].ToString();
                if (trangThaiHienTai != "Chờ duyệt")
                {
                    errorMessage = $"Không thể duyệt đơn hàng có trạng thái '{trangThaiHienTai}'!";
                    return false;
                }

                return giaoDichThueDAL.ApproveGiaoDichThue(maGDThue, nguoiDuyet, ghiChu);
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi duyệt giao dịch thuê: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Từ chối đơn thuê xe
        /// </summary>
        public bool RejectGiaoDichThue(int maGDThue, string nguoiDuyet, string lyDo, out string errorMessage)
        {
            errorMessage = "";

            if (maGDThue <= 0)
            {
                errorMessage = "Mã giao dịch không hợp lệ!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(nguoiDuyet))
            {
                errorMessage = "Người duyệt không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(lyDo))
            {
                errorMessage = "Vui lòng nhập lý do từ chối!";
                return false;
            }

            try
            {
                DataTable dt = giaoDichThueDAL.GetAllGiaoDichThue();
                DataRow[] rows = dt.Select($"MaGDThue = {maGDThue}");

                if (rows.Length == 0)
                {
                    errorMessage = "Không tìm thấy giao dịch!";
                    return false;
                }

                string trangThaiHienTai = rows[0]["TrangThaiDuyet"].ToString();
                if (trangThaiHienTai != "Chờ duyệt")
                {
                    errorMessage = $"Không thể từ chối đơn hàng có trạng thái '{trangThaiHienTai}'!";
                    return false;
                }

                return giaoDichThueDAL.RejectGiaoDichThue(maGDThue, nguoiDuyet, lyDo);
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi từ chối giao dịch thuê: " + ex.Message;
                return false;
            }
        }

        #endregion

        #region Quản lý cho thuê (Thanh toán, Giao xe, Trả xe)

        /// <summary>
        /// Xác nhận thanh toán
        /// </summary>
        public bool XacNhanThanhToan(int maGDThue, string maNV, string hinhThuc, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                // Kiểm tra điều kiện
                DataTable dt = GetGiaoDichThueById(maGDThue);
                if (dt.Rows.Count == 0)
                {
                    errorMessage = "Không tìm thấy giao dịch!";
                    return false;
                }

                DataRow row = dt.Rows[0];

                if (row["TrangThaiDuyet"].ToString() != "Đã duyệt")
                {
                    errorMessage = "Đơn hàng chưa được duyệt!";
                    return false;
                }

                if (row["TrangThaiThanhToan"].ToString() == "Đã thanh toán")
                {
                    errorMessage = "Đơn hàng đã được thanh toán rồi!";
                    return false;
                }

                // Cập nhật
                string query = @"
                    UPDATE GiaoDichThue 
                    SET TrangThaiThanhToan = N'Đã thanh toán',
                        HinhThucThanhToan = @HinhThuc
                    WHERE MaGDThue = @MaGDThue";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaGDThue", maGDThue),
                    new SqlParameter("@HinhThuc", hinhThuc)
                };

                int result = DataProvider.ExecuteNonQuery(query, parameters);

                if (result == 0)
                {
                    errorMessage = "Cập nhật thất bại!";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Xác nhận giao xe cho khách hàng
        /// </summary>
        public bool XacNhanGiaoXe(int maGDThue, string maNV, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                // Kiểm tra điều kiện
                DataTable dt = GetGiaoDichThueById(maGDThue);
                if (dt.Rows.Count == 0)
                {
                    errorMessage = "Không tìm thấy giao dịch!";
                    return false;
                }

                DataRow row = dt.Rows[0];

                if (row["TrangThaiThanhToan"].ToString() != "Đã thanh toán")
                {
                    errorMessage = "Khách hàng chưa thanh toán!";
                    return false;
                }

                if (row["TrangThai"].ToString() == "Đang thuê")
                {
                    errorMessage = "Xe đã được giao!";
                    return false;
                }

                string idXe = row["ID_Xe"].ToString();

                // Cập nhật trạng thái giao dịch
                string query1 = @"
                    UPDATE GiaoDichThue 
                    SET TrangThai = N'Đang thuê'
                    WHERE MaGDThue = @MaGDThue";

                SqlParameter[] params1 = { new SqlParameter("@MaGDThue", maGDThue) };
                DataProvider.ExecuteNonQuery(query1, params1);

                // Cập nhật trạng thái xe
                string query2 = @"
                    UPDATE XeMay 
                    SET TrangThai = N'Đang thuê'
                    WHERE ID_Xe = @ID_Xe";

                SqlParameter[] params2 = { new SqlParameter("@ID_Xe", idXe) };
                DataProvider.ExecuteNonQuery(query2, params2);

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Xác nhận trả xe từ khách hàng
        /// </summary>
        public bool XacNhanTraXe(int maGDThue, string maNV, string tinhTrangXe,
            decimal chiPhiPhatSinh, string ghiChu, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                // Lấy thông tin giao dịch
                DataTable dt = GetGiaoDichThueById(maGDThue);
                if (dt.Rows.Count == 0)
                {
                    errorMessage = "Không tìm thấy giao dịch!";
                    return false;
                }

                DataRow row = dt.Rows[0];

                if (row["TrangThai"].ToString() != "Đang thuê")
                {
                    errorMessage = "Xe chưa được giao cho khách!";
                    return false;
                }

                string idXe = row["ID_Xe"].ToString();
                DateTime ngayKetThuc = Convert.ToDateTime(row["NgayKetThuc"]);
                decimal giaThueNgay = Convert.ToDecimal(row["GiaThueNgay"]);
                decimal soTienCoc = row["SoTienCoc"] != DBNull.Value
                    ? Convert.ToDecimal(row["SoTienCoc"]) : 0;

                // Tính phí phạt nếu trả muộn
                int soNgayQuaHan = (DateTime.Now.Date - ngayKetThuc.Date).Days;
                decimal tienPhat = 0;

                if (soNgayQuaHan > 0)
                {
                    tienPhat = TinhPhiPhat(ngayKetThuc, giaThueNgay);
                }

                // Tính tiền hoàn cọc
                decimal tienHoanCoc = soTienCoc - chiPhiPhatSinh - tienPhat;
                if (tienHoanCoc < 0) tienHoanCoc = 0;

                // Tạo giao dịch trả xe
                GiaoDichTraThue gdTra = new GiaoDichTraThue
                {
                    MaGDThue = maGDThue,
                    NgayTraXe = DateTime.Now,
                    TinhTrangXe = tinhTrangXe,
                    ChiPhiPhatSinh = chiPhiPhatSinh,
                    TienHoanCoc = tienHoanCoc,
                    TienPhat = tienPhat,
                    GhiChu = ghiChu,
                    MaTaiKhoan = maNV
                };

                bool insertSuccess = giaoDichTraThueDAL.InsertGiaoDichTraXe(gdTra);
                if (!insertSuccess)
                {
                    errorMessage = "Không thể tạo giao dịch trả xe!";
                    return false;
                }

                // Cập nhật trạng thái giao dịch thuê
                string query1 = @"
                    UPDATE GiaoDichThue 
                    SET TrangThai = N'Đã thuê'
                    WHERE MaGDThue = @MaGDThue";

                SqlParameter[] params1 = { new SqlParameter("@MaGDThue", maGDThue) };
                DataProvider.ExecuteNonQuery(query1, params1);

                // Cập nhật trạng thái xe về sẵn sàng
                string query2 = @"
                    UPDATE XeMay 
                    SET TrangThai = N'Sẵn sàng'
                    WHERE ID_Xe = @ID_Xe";

                SqlParameter[] params2 = { new SqlParameter("@ID_Xe", idXe) };
                DataProvider.ExecuteNonQuery(query2, params2);

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi: " + ex.Message;
                return false;
            }
        }

        #endregion

        #region Tìm kiếm và lọc

        /// <summary>
        /// Tìm kiếm giao dịch thuê
        /// </summary>
        public DataTable SearchGiaoDichThue(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetAllGiaoDichThue();
            }

            return giaoDichThueDAL.SearchGiaoDichThue(keyword);
        }

        /// <summary>
        /// Tìm kiếm trong quản lý cho thuê (chỉ đơn đã duyệt)
        /// </summary>
        public DataTable SearchDonChoThue(string keyword)
        {
            DataTable dt = GetDonChoThue();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return dt;
            }

            DataView dv = dt.DefaultView;
            dv.RowFilter = $@"
                HoTenKH LIKE '%{keyword}%' OR 
                SdtKhachHang LIKE '%{keyword}%' OR 
                BienSo LIKE '%{keyword}%' OR 
                TenXe LIKE '%{keyword}%' OR 
                Convert(MaGDThue, 'System.String') LIKE '%{keyword}%'";

            return dv.ToTable();
        }

        /// <summary>
        /// Lọc đơn cho thuê theo trạng thái
        /// </summary>
        public DataTable FilterDonChoThueByStatus(string trangThai)
        {
            DataTable dt = GetDonChoThue();

            if (string.IsNullOrWhiteSpace(trangThai) || trangThai == "Tất cả")
            {
                return dt;
            }

            DataView dv = dt.DefaultView;
            dv.RowFilter = $"TrangThai = '{trangThai}'";
            return dv.ToTable();
        }

        #endregion

        #region Tính toán

        /// <summary>
        /// Tính toán tổng giá thuê
        /// </summary>
        public decimal TinhTongGiaThue(DateTime ngayBatDau, DateTime ngayKetThuc, decimal giaThueNgay)
        {
            int soNgayThue = (ngayKetThuc - ngayBatDau).Days;
            if (soNgayThue <= 0)
            {
                return 0;
            }

            return giaThueNgay * soNgayThue;
        }

        /// <summary>
        /// Tính phí phạt trả muộn (150% giá thuê/ngày)
        /// </summary>
        public decimal TinhPhiPhat(DateTime ngayKetThuc, decimal giaThueNgay)
        {
            int soNgayQuaHan = (DateTime.Now.Date - ngayKetThuc.Date).Days;
            if (soNgayQuaHan <= 0) return 0;

            return soNgayQuaHan * giaThueNgay * 1.5m; // Phạt 150%
        }

        /// <summary>
        /// Tính số ngày quá hạn
        /// </summary>
        public int TinhSoNgayQuaHan(DateTime ngayKetThuc)
        {
            int soNgay = (DateTime.Now.Date - ngayKetThuc.Date).Days;
            return soNgay > 0 ? soNgay : 0;
        }

        /// <summary>
        /// Tính tiền hoàn cọc
        /// </summary>
        public decimal TinhTienHoanCoc(decimal soTienCoc, decimal chiPhiPhatSinh, decimal tienPhat)
        {
            decimal tienHoan = soTienCoc - chiPhiPhatSinh - tienPhat;
            return tienHoan > 0 ? tienHoan : 0;
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validate dữ liệu giao dịch thuê
        /// </summary>
        private bool ValidateGiaoDichThue(GiaoDichThue gd, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(gd.MaKH))
            {
                errorMessage = "Mã khách hàng không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(gd.ID_Xe))
            {
                errorMessage = "Mã xe không được để trống!";
                return false;
            }

            if (gd.NgayBatDau >= gd.NgayKetThuc)
            {
                errorMessage = "Ngày kết thúc phải lớn hơn ngày bắt đầu!";
                return false;
            }

            if (gd.NgayBatDau < DateTime.Now.Date)
            {
                errorMessage = "Ngày bắt đầu không được nhỏ hơn ngày hiện tại!";
                return false;
            }

            if (gd.GiaThueNgay <= 0)
            {
                errorMessage = "Giá thuê ngày phải lớn hơn 0!";
                return false;
            }

            if (gd.TongGia <= 0)
            {
                errorMessage = "Tổng giá phải lớn hơn 0!";
                return false;
            }

            // Validate số ngày thuê hợp lý
            int soNgayThue = (gd.NgayKetThuc - gd.NgayBatDau).Days;
            if (soNgayThue <= 0)
            {
                errorMessage = "Số ngày thuê phải lớn hơn 0!";
                return false;
            }

            if (soNgayThue > 365)
            {
                errorMessage = "Số ngày thuê không được vượt quá 365 ngày!";
                return false;
            }

            // Validate tổng giá
            decimal tongGiaExpected = gd.GiaThueNgay * soNgayThue;
            if (Math.Abs(gd.TongGia - tongGiaExpected) > 1) // Cho phép sai số 1đ
            {
                errorMessage = $"Tổng giá không khớp! Tính được: {tongGiaExpected:N0}đ";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Kiểm tra xe có đang được thuê không
        /// </summary>
        public bool IsXeDangThue(string idXe, DateTime ngayBatDau, DateTime ngayKetThuc, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM GiaoDichThue 
                    WHERE ID_Xe = @ID_Xe 
                      AND TrangThai IN (N'Đang thuê', N'Chờ xác nhận')
                      AND (
                          (@NgayBatDau BETWEEN NgayBatDau AND NgayKetThuc) OR
                          (@NgayKetThuc BETWEEN NgayBatDau AND NgayKetThuc) OR
                          (NgayBatDau BETWEEN @NgayBatDau AND @NgayKetThuc)
                      )";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID_Xe", idXe),
                    new SqlParameter("@NgayBatDau", ngayBatDau),
                    new SqlParameter("@NgayKetThuc", ngayKetThuc)
                };

                int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));

                if (count > 0)
                {
                    errorMessage = "Xe đã có lịch thuê trong khoảng thời gian này!";
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi kiểm tra: " + ex.Message;
                return true; // Trả về true để không cho thuê khi có lỗi
            }
        }

        #endregion

        #region Thống kê

        /// <summary>
        /// Lấy thống kê giao dịch thuê
        /// </summary>
        public DataTable GetThongKeGiaoDichThue(DateTime tuNgay, DateTime denNgay)
        {
            string query = @"
                SELECT 
                    gd.*,
                    kh.HoTenKH,
                    CONCAT(hx.TenHang, ' ', dx.TenDong) AS TenXe,
                    xe.BienSo
                FROM GiaoDichThue gd
                INNER JOIN KhachHang kh ON gd.MaKH = kh.MaKH
                INNER JOIN XeMay xe ON gd.ID_Xe = xe.ID_Xe
                INNER JOIN LoaiXe lx ON xe.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                WHERE gd.NgayBatDau BETWEEN @TuNgay AND @DenNgay
                ORDER BY gd.NgayBatDau DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TuNgay", tuNgay),
                new SqlParameter("@DenNgay", denNgay)
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Đếm số đơn theo trạng thái
        /// </summary>
        public int CountByStatus(string trangThai)
        {
            DataTable dt = GetDonChoThue();
            DataView dv = dt.DefaultView;
            dv.RowFilter = $"TrangThai = '{trangThai}'";
            return dv.Count;
        }

        #endregion
    }
}