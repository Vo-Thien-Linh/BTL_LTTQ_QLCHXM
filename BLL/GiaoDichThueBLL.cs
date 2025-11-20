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
        private XeMayDAL xeMayDAL;
        private HopDongThueBLL hopDongThueBLL;

        public GiaoDichThueBLL()
        {
            giaoDichThueDAL = new GiaoDichThueDAL();
            giaoDichTraThueDAL = new GiaoDichTraThueDAL();
            xeMayDAL = new XeMayDAL();
            hopDongThueBLL = new HopDongThueBLL();
        }

        #region Lấy dữ liệu

        public DataTable GetAllGiaoDichThue()
        {
            return giaoDichThueDAL.GetAllGiaoDichThue();
        }

        public DataTable GetGiaoDichThueByTrangThai(string trangThaiDuyet)
        {
            return giaoDichThueDAL.GetGiaoDichThueByTrangThai(trangThaiDuyet);
        }

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
                    xe.AnhXe AS AnhXeXeBan,
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
                    gd.NgayGiaoXeThucTe,
                    gd.KmBatDau,
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
                WHERE gd.TrangThaiDuyet IN (N'Chờ duyệt', N'Đã duyệt')
                ORDER BY 
                    CASE gd.TrangThaiDuyet
                        WHEN N'Chờ duyệt' THEN 0
                        ELSE 1
                    END,
                    CASE 
                        WHEN gd.NgayKetThuc < GETDATE() AND gd.TrangThai = N'Đang thuê' THEN 0 
                        ELSE 1 
                    END,
                    gd.NgayBatDau DESC";

            return DataProvider.ExecuteQuery(query);
        }

        public DataTable GetGiaoDichThueById(int maGDThue)
        {
            // ✅ SỬA: LẤY TRỰC TIẾP TỪ DATABASE, KHÔNG QUA CACHE
            string query = @"
                SELECT 
                    gd.MaGDThue, 
                    gd.MaKH, 
                    kh.HoTenKH, 
                    kh.Sdt AS SdtKhachHang,
                    gd.ID_Xe, 
                    CONCAT(hx.TenHang, ' ', dx.TenDong, ' - ', ms.TenMau) AS TenXe,
                    xe.BienSo,
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
                    gd.NguoiDuyet,
                    gd.NgayDuyet,
                    gd.GhiChuDuyet,
                    gd.NgayGiaoXeThucTe,
                    gd.KmBatDau,
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
                WHERE gd.MaGDThue = @MaGDThue";

            SqlParameter[] parameters = { new SqlParameter("@MaGDThue", maGDThue) };
            return DataProvider.ExecuteQuery(query, parameters);
        }



        /// Thêm validation đầy đủ khi tạo đơn thuê
        public bool InsertGiaoDichThue(GiaoDichThue gd, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                // 1. Validate dữ liệu đầu vào
                if (!ValidateGiaoDichThue(gd, out errorMessage))
                {
                    return false;
                }

                // 2. Kiểm tra khách hàng tồn tại
                if (!ValidateKhachHang(gd.MaKH, out errorMessage))
                {
                    return false;
                }

                // 3. Kiểm tra trạng thái xe
                if (!ValidateTrangThaiXe(gd.ID_Xe, out errorMessage))
                {
                    return false;
                }

                // 4. Kiểm tra xe có trùng lịch không
                if (IsXeDangThue(gd.ID_Xe, gd.NgayBatDau, gd.NgayKetThuc, out errorMessage))
                {
                    return false;
                }

                // 5. Đảm bảo các trường mặc định
                if (string.IsNullOrWhiteSpace(gd.TrangThai))
                {
                    gd.TrangThai = "Chờ xác nhận";
                }

                if (string.IsNullOrWhiteSpace(gd.TrangThaiThanhToan))
                {
                    gd.TrangThaiThanhToan = "Chưa thanh toán";
                }

                if (string.IsNullOrWhiteSpace(gd.TrangThaiDuyet))
                {
                    gd.TrangThaiDuyet = "Chờ duyệt";
                }

                // 6. Gọi DAL để insert
                bool success = giaoDichThueDAL.InsertGiaoDichThue(gd);

                if (!success)
                {
                    errorMessage = "Không thể tạo đơn thuê! Vui lòng kiểm tra lại thông tin.";
                    return false;
                }

                return true;
            }
            catch (SqlException sqlEx)
            {
                // Xử lý lỗi SQL cụ thể
                if (sqlEx.Message.Contains("FK_"))
                {
                    errorMessage = "Lỗi ràng buộc dữ liệu: Khách hàng hoặc xe không tồn tại!";
                }
                else if (sqlEx.Message.Contains("UNIQUE"))
                {
                    errorMessage = "Lỗi: Dữ liệu bị trùng lặp!";
                }
                else
                {
                    errorMessage = "Lỗi cơ sở dữ liệu: " + sqlEx.Message;
                }

                System.Diagnostics.Debug.WriteLine($"SQL Error: {sqlEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi tạo đơn thuê: " + ex.Message;
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return false;
            }
        }
        ///  Duyệt đơn với validation nghiệp vụ đầy đủ
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

                DataRow row = rows[0];

                // Kiểm tra trạng thái duyệt
                string trangThaiHienTai = row["TrangThaiDuyet"].ToString();
                if (trangThaiHienTai != "Chờ duyệt")
                {
                    errorMessage = $"Không thể duyệt đơn hàng có trạng thái '{trangThaiHienTai}'!";
                    return false;
                }

                //  Kiểm tra ngày bắt đầu đã quá hạn chưa
                DateTime ngayBatDau = Convert.ToDateTime(row["NgayBatDau"]);
                DateTime ngayKetThuc = Convert.ToDateTime(row["NgayKetThuc"]);
                DateTime ngayHienTai = DateTime.Now.Date;

                if (ngayKetThuc.Date < ngayHienTai)
                {
                    errorMessage = $"Không thể duyệt đơn đã quá hạn!\n" +
                                 $"Ngày kết thúc: {ngayKetThuc:dd/MM/yyyy}\n" +
                                 $"Ngày hiện tại: {ngayHienTai:dd/MM/yyyy}";
                    return false;
                }

                //  Cảnh báo nếu ngày bắt đầu đã qua
                if (ngayBatDau.Date < ngayHienTai)
                {
                    ghiChu += $" [CẢNH BÁO: Ngày bắt đầu đã qua ({ngayBatDau:dd/MM/yyyy})]";
                }

                //  Kiểm tra lại xe có khả dụng không
                string idXe = row["ID_Xe"].ToString();
                if (IsXeDangThue(idXe, ngayBatDau, ngayKetThuc, out string xeError))
                {
                    errorMessage = xeError;
                    return false;
                }

                //  Kiểm tra trạng thái xe
                if (!ValidateTrangThaiXe(idXe, out errorMessage))
                {
                    return false;
                }

                //  Duyệt đơn
                bool approveSuccess = giaoDichThueDAL.ApproveGiaoDichThue(maGDThue, nguoiDuyet, ghiChu);
                
                if (!approveSuccess)
                {
                    errorMessage = "Lỗi khi duyệt giao dịch!";
                    return false;
                }

                // TẠO HỢP ĐỒNG SAU KHI DUYỆT THÀNH CÔNG
                string hopDongError;
                bool hopDongSuccess = hopDongThueBLL.TaoHopDongThue(maGDThue, nguoiDuyet, out hopDongError);
                
                if (!hopDongSuccess)
                {
                    // ❌ SỬA: THÔNG BÁO CHO USER THAY VÌ CHỈ LOG
                    errorMessage = $"Duyệt đơn thành công NHƯNG không tạo được hợp đồng!\n\n" +
                   $"Lý do: {hopDongError}\n\n" +
                   $"Vui lòng kiểm tra lại hoặc tạo hợp đồng thủ công.";
    
                    System.Diagnostics.Debug.WriteLine($"Cảnh báo: Không tạo được hợp đồng - {hopDongError}");
    
                    // VẪN RETURN TRUE VÌ DUYỆT ĐÃ THÀNH CÔNG
                    return true;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi duyệt giao dịch thuê: " + ex.Message;
                return false;
            }
        }

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

        public bool XacNhanThanhToan(int maGDThue, string maNV, string hinhThuc, out string errorMessage)
        {
            errorMessage = "";

            try
            {
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

        ///  Xác nhận giao xe với validation đầy đủ
        public bool XacNhanGiaoXe(int maGDThue, string maNV, int kmBatDau, string ghiChu, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                DataTable dt = GetGiaoDichThueById(maGDThue);
                if (dt.Rows.Count == 0)
                {
                    errorMessage = "Không tìm thấy giao dịch!";
                    return false;
                }

                DataRow row = dt.Rows[0];

                // Kiểm tra thanh toán
                if (row["TrangThaiThanhToan"].ToString() != "Đã thanh toán")
                {
                    errorMessage = "Khách hàng chưa thanh toán!";
                    return false;
                }

                //  Kiểm tra trạng thái
                string trangThai = row["TrangThai"].ToString();
                if (trangThai == "Đang thuê")
                {
                    errorMessage = "Xe đã được giao!";
                    return false;
                }

                if (trangThai != "Chờ giao xe")
                {
                    errorMessage = $"Không thể giao xe với trạng thái '{trangThai}'!";
                    return false;
                }

                //  Kiểm tra ngày giao xe
                DateTime ngayBatDau = Convert.ToDateTime(row["NgayBatDau"]);
                DateTime ngayHienTai = DateTime.Now.Date;

                if (ngayHienTai < ngayBatDau.Date)
                {
                    errorMessage = $"Chưa đến ngày giao xe!\n" +
                                 $"Ngày bắt đầu: {ngayBatDau:dd/MM/yyyy}\n" +
                                 $"Ngày hiện tại: {ngayHienTai:dd/MM/yyyy}";
                    return false;
                }

                //Cảnh báo nếu giao xe muộn
                if (ngayHienTai > ngayBatDau.Date)
                {
                    int soNgayMuon = (ngayHienTai - ngayBatDau.Date).Days;
                    ghiChu += $" [CẢNH BÁO: Giao xe muộn {soNgayMuon} ngày]";
                }

                // Validate số km
                if (kmBatDau < 0)
                {
                    errorMessage = "Số km không hợp lệ!";
                    return false;
                }

                if (kmBatDau > 999999)
                {
                    errorMessage = "Số km quá lớn!";
                    return false;
                }

                string idXe = row["ID_Xe"].ToString();

                // Gọi DAL để cập nhật
                bool success = giaoDichThueDAL.XacNhanGiaoXe(maGDThue, maNV, kmBatDau, ghiChu);

                if (!success)
                {
                    errorMessage = "Không thể cập nhật giao dịch!";
                    return false;
                }

                // Cập nhật trạng thái xe
                string queryXe = @"
                    UPDATE XeMay 
                    SET TrangThai = N'Đang thuê'
                    WHERE ID_Xe = @ID_Xe";

                SqlParameter[] paramsXe = { new SqlParameter("@ID_Xe", idXe) };
                DataProvider.ExecuteNonQuery(queryXe, paramsXe);

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi: " + ex.Message;
                return false;
            }
        }

        /// Xác nhận trả xe với hỗ trợ trả sớm
        public bool XacNhanTraXe(
            int maGDThue,
            string maNV,
            string tinhTrangXe,
            decimal chiPhiPhatSinh,
            int kmKetThuc,
            bool isTraSom,
            int soNgayTraSom,
            string ghiChu,
            out string errorMessage)
        {
            errorMessage = "";

            try
            {
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

                // Validate km
                int kmBatDau = row["KmBatDau"] != DBNull.Value ? Convert.ToInt32(row["KmBatDau"]) : 0;
                if (kmKetThuc < kmBatDau)
                {
                    errorMessage = $"Số km kết thúc ({kmKetThuc:N0}) không thể nhỏ hơn km bắt đầu ({kmBatDau:N0})!";
                    return false;
                }

                int kmChay = kmKetThuc - kmBatDau;
                if (kmChay > 10000)
                {
                    // Cảnh báo nhưng vẫn cho phép
                    ghiChu += $" [CẢNH BÁO: Xe chạy {kmChay:N0}km]";
                }

                string idXe = row["ID_Xe"].ToString();
                DateTime ngayKetThuc = Convert.ToDateTime(row["NgayKetThuc"]);
                decimal giaThueNgay = Convert.ToDecimal(row["GiaThueNgay"]);
                decimal soTienCoc = row["SoTienCoc"] != DBNull.Value
                    ? Convert.ToDecimal(row["SoTienCoc"]) : 0;

                // Tính phí phạt nếu trả muộn
                decimal tienPhat = TinhPhiPhat(ngayKetThuc, giaThueNgay);

                // Tính tiền hoàn lại nếu trả sớm
                decimal tienHoanTraSom = 0;
                if (isTraSom && soNgayTraSom > 0)
                {
                    tienHoanTraSom = TinhTienHoanTraSom(soNgayTraSom, giaThueNgay);
                }

                // Tính tiền hoàn cọc
                decimal tienHoanCoc = TinhTienHoanCoc(soTienCoc, chiPhiPhatSinh, tienPhat, tienHoanTraSom);

                // Tạo giao dịch trả xe
                GiaoDichTraThue gdTra = new GiaoDichTraThue
                {
                    MaGDThue = maGDThue,
                    NgayTraXe = DateTime.Now,
                    TinhTrangXe = tinhTrangXe,
                    ChiPhiPhatSinh = chiPhiPhatSinh,
                    TienHoanCoc = tienHoanCoc,
                    TienPhat = tienPhat,
                    KmKetThuc = kmKetThuc,
                    SoNgayTraSom = soNgayTraSom,
                    TienHoanTraSom = tienHoanTraSom,
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
                    SET TrangThai = N'Đã thuê',
                        NgayTraXeThucTe = @NgayTra
                    WHERE MaGDThue = @MaGDThue";

                SqlParameter[] params1 =
                {
                    new SqlParameter("@MaGDThue", maGDThue),
                    new SqlParameter("@NgayTra", DateTime.Now)
                };
                DataProvider.ExecuteNonQuery(query1, params1);

                // Cập nhật trạng thái xe về sẵn sàng
                string query2 = @"
                    UPDATE XeMay 
                    SET TrangThai = N'Sẵn sàng',
                        KmDaChay = @KmKetThuc
                    WHERE ID_Xe = @ID_Xe";

                SqlParameter[] params2 = {
                    new SqlParameter("@ID_Xe", idXe),
                    new SqlParameter("@KmKetThuc", kmKetThuc)
                };
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

        public DataTable SearchGiaoDichThue(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetAllGiaoDichThue();
            }

            return giaoDichThueDAL.SearchGiaoDichThue(keyword);
        }

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

        public decimal TinhTongGiaThue(DateTime ngayBatDau, DateTime ngayKetThuc, decimal giaThueNgay)
        {
            int soNgayThue = (ngayKetThuc - ngayBatDau).Days;
            if (soNgayThue <= 0)
            {
                return 0;
            }

            return giaThueNgay * soNgayThue;
        }

        public decimal TinhPhiPhat(DateTime ngayKetThuc, decimal giaThueNgay)
        {
            int soNgayQuaHan = (DateTime.Now.Date - ngayKetThuc.Date).Days;
            if (soNgayQuaHan <= 0) return 0;

            return soNgayQuaHan * giaThueNgay * 1.5m;
        }

        public decimal TinhTienHoanTraSom(int soNgayTraSom, decimal giaThueNgay)
        {
            if (soNgayTraSom <= 0) return 0;

            return soNgayTraSom * giaThueNgay * 0.7m;
        }

        public int TinhSoNgayQuaHan(DateTime ngayKetThuc)
        {
            int soNgay = (DateTime.Now.Date - ngayKetThuc.Date).Days;
            return soNgay > 0 ? soNgay : 0;
        }

        public decimal TinhTienHoanCoc(decimal soTienCoc, decimal chiPhiPhatSinh, decimal tienPhat, decimal tienHoanTraSom = 0)
        {
            decimal tienHoan = soTienCoc - chiPhiPhatSinh - tienPhat + tienHoanTraSom;
            return tienHoan;
        }

        ///  Kiểm tra xe có đang được thuê trong khoảng thời gian không
        public bool IsXeDangThue(string idXe, DateTime ngayBatDau, DateTime ngayKetThuc, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM GiaoDichThue 
                    WHERE ID_Xe = @ID_Xe 
                      AND TrangThaiDuyet = N'Đã duyệt'
                      AND TrangThai IN (N'Chờ giao xe', N'Đang thuê')
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
                return true;
            }
        }

        /// Kiểm tra trạng thái xe
        private bool ValidateTrangThaiXe(string idXe, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                string query = "SELECT TrangThai FROM XeMay WHERE ID_Xe = @ID_Xe";
                SqlParameter[] parameters = { new SqlParameter("@ID_Xe", idXe) };

                object result = DataProvider.ExecuteScalar(query, parameters);

                if (result == null)
                {
                    errorMessage = "Không tìm thấy xe!";
                    return false;
                }

                string trangThai = result.ToString();

                if (trangThai != "Sẵn sàng")
                {
                    errorMessage = $"Xe đang ở trạng thái '{trangThai}', không thể thuê!";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi kiểm tra trạng thái xe: " + ex.Message;
                return false;
            }
        }

        /// Kiểm tra thông tin khách hàng
        private bool ValidateKhachHang(string maKH, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                string query = "SELECT COUNT(*) FROM KhachHang WHERE MaKH = @MaKH";
                SqlParameter[] parameters = { new SqlParameter("@MaKH", maKH) };

                int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters));

                if (count == 0)
                {
                    errorMessage = "Không tìm thấy khách hàng!";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi kiểm tra khách hàng: " + ex.Message;
                return false;
            }
        }

        ///  Validate đầy đủ khi tạo đơn thuê
        private bool ValidateGiaoDichThue(GiaoDichThue gd, out string errorMessage)
        {
            errorMessage = "";

            // 1. Kiểm tra mã khách hàng
            if (string.IsNullOrWhiteSpace(gd.MaKH))
            {
                errorMessage = "Mã khách hàng không được để trống!";
                return false;
            }

            //  Kiểm tra mã xe
            if (string.IsNullOrWhiteSpace(gd.ID_Xe))
            {
                errorMessage = "Mã xe không được để trống!";
                return false;
            }

            // Kiểm tra ngày bắt đầu phải >= ngày hiện tại
            if (gd.NgayBatDau.Date < DateTime.Now.Date)
            {
                errorMessage = $"Ngày bắt đầu không được nhỏ hơn ngày hiện tại!\n" +
                             $"Ngày bắt đầu: {gd.NgayBatDau:dd/MM/yyyy}\n" +
                             $"Ngày hiện tại: {DateTime.Now:dd/MM/yyyy}";
                return false;
            }

            //  Kiểm tra ngày bắt đầu không quá xa (3 tháng)
            DateTime ngayToiDa = DateTime.Now.Date.AddMonths(3);
            if (gd.NgayBatDau.Date > ngayToiDa)
            {
                errorMessage = $"Ngày bắt đầu không được vượt quá 3 tháng kể từ hôm nay!\n" +
                             $"Ngày bắt đầu: {gd.NgayBatDau:dd/MM/yyyy}\n" +
                             $"Ngày tối đa: {ngayToiDa:dd/MM/yyyy}";
                return false;
            }

            //  Kiểm tra ngày kết thúc > ngày bắt đầu
            if (gd.NgayKetThuc <= gd.NgayBatDau)
            {
                errorMessage = "Ngày kết thúc phải lớn hơn ngày bắt đầu!";
                return false;
            }

            // Kiểm tra số ngày thuê
            int soNgayThue = (gd.NgayKetThuc - gd.NgayBatDau).Days;

            if (soNgayThue <= 0)
            {
                errorMessage = "Số ngày thuê phải lớn hơn 0!";
                return false;
            }

            // Thời gian thuê tối thiểu 1 ngày
            if (soNgayThue < 1)
            {
                errorMessage = "Thời gian thuê tối thiểu là 1 ngày!";
                return false;
            }

            //  Kiểm tra số ngày thuê tối đa
            if (soNgayThue > 365)
            {
                errorMessage = "Số ngày thuê không được vượt quá 365 ngày!";
                return false;
            }

            //  Kiểm tra giá thuê
            if (gd.GiaThueNgay <= 0)
            {
                errorMessage = "Giá thuê ngày phải lớn hơn 0!";
                return false;
            }

            //  Kiểm tra tổng giá
            if (gd.TongGia <= 0)
            {
                errorMessage = "Tổng giá phải lớn hơn 0!";
                return false;
            }

            //  Kiểm tra tính toán tổng giá
            decimal tongGiaExpected = gd.GiaThueNgay * soNgayThue;
            if (Math.Abs(gd.TongGia - tongGiaExpected) > 1)
            {
                errorMessage = $"Tổng giá không khớp!\n" +
                             $"Tính được: {tongGiaExpected:N0}đ\n" +
                             $"Nhập vào: {gd.TongGia:N0}đ";
                return false;
            }

            // Kiểm tra tiền cọc
            if (gd.SoTienCoc.HasValue)
            {
                if (gd.SoTienCoc.Value < 0)
                {
                    errorMessage = "Tiền cọc không được âm!";
                    return false;
                }

                if (gd.SoTienCoc.Value > gd.TongGia)
                {
                    errorMessage = "Tiền cọc không được lớn hơn tổng tiền thuê!";
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Thống kê

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