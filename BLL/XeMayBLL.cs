using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BLL
{
    public class XeMayBLL
    {
        private XeMayDAL xeMayDAL = new XeMayDAL();

        public string GenerateNewMaXe()
        {
            string lastCode = xeMayDAL.GetMaxID_Xe();
            if (!string.IsNullOrEmpty(lastCode) && lastCode.StartsWith("XE"))
            {
                int num = int.Parse(lastCode.Substring(2));
                num++;
                return "XE" + num.ToString("D8");
            }
            return "XE00000001";
        }

        // Lấy tất cả xe
        public DataTable GetAllXeMay()
        {
            try
            {
                return xeMayDAL.GetAllXeMay();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách xe: " + ex.Message);
            }
        }
      

        // Tìm kiếm xe
        public DataTable SearchXeMay(string keyword, string trangThai)
        {
            try
            {
                return xeMayDAL.SearchXeMay(keyword, trangThai);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm xe: " + ex.Message);
            }
        }

        // Lấy thông tin xe theo ID
        public XeMayDTO GetXeMayById(string idXe)
        {
            try
            {
                if (string.IsNullOrEmpty(idXe))
                {
                    throw new Exception("Mã xe không được để trống!");
                }

                return xeMayDAL.GetXeMayById(idXe);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin xe: " + ex.Message);
            }
        }

        // Thêm xe mới
        public bool InsertXeMay(XeMayDTO xe)
        {
            try
            {
                // Tạo list để chứa các lỗi
                List<string> errors = new List<string>();

                // Validate dữ liệu bắt buộc với tên trường cụ thể
                if (string.IsNullOrEmpty(xe.ID_Xe))
                    errors.Add("- Mã xe");

                if (string.IsNullOrEmpty(xe.ID_Loai))
                {
                    // Kiểm tra chi tiết hơn để biết thiếu trường nào
                    if (string.IsNullOrEmpty(xe.MaHang))
                        errors.Add("- Hãng xe");
                    if (string.IsNullOrEmpty(xe.MaDong))
                        errors.Add("- Dòng xe");
                    if (string.IsNullOrEmpty(xe.MaMau))
                        errors.Add("- Màu sắc");
                    if (xe.NamSX == null || xe.NamSX <= 0)
                        errors.Add("- Năm sản xuất");
                }

                if (string.IsNullOrEmpty(xe.TrangThai))
                    errors.Add("- Trạng thái xe");

                if (string.IsNullOrEmpty(xe.MucDichSuDung))
                    errors.Add("- Mục đích sử dụng (Cho thuê hoặc Bán)");

                if (xe.GiaMua == null || xe.GiaMua <= 0)
                    errors.Add("- Giá mua xe");

                if (xe.NgayMua == null)
                    errors.Add("- Ngày mua xe");

                if (string.IsNullOrEmpty(xe.ThongTinXang))
                    errors.Add("- Thông tin xăng");

                // Nếu có lỗi, thông báo chi tiết
                if (errors.Count > 0)
                {
                    string errorMessage = "Vui lòng nhập đầy đủ các thông tin sau:\n\n" + string.Join("\n", errors);
                    throw new Exception(errorMessage);
                }

                // Validate logic nghiệp vụ
                if (!string.IsNullOrEmpty(xe.BienSo) && xeMayDAL.IsBienSoExists(xe.BienSo))
                {
                    throw new Exception("Biển số xe '" + xe.BienSo + "' đã tồn tại trong hệ thống!");
                }

                // Validate mục đích sử dụng vs trạng thái
                if (xe.MucDichSuDung == "Cho thuê" && xe.TrangThai == "Đã bán")
                {
                    throw new Exception("Xe cho thuê không thể có trạng thái 'Đã bán'!\nVui lòng chọn trạng thái phù hợp.");
                }

                if (xe.MucDichSuDung == "Bán" && xe.TrangThai == "Đang thuê")
                {
                    throw new Exception("Xe bán không thể có trạng thái 'Đang thuê'!\nVui lòng chọn trạng thái phù hợp.");
                }

                // Validate năm sản xuất hợp lý
                int currentYear = DateTime.Now.Year;
                if (xe.NamSX != null && (xe.NamSX < 1900 || xe.NamSX > currentYear + 1))
                {
                    throw new Exception("Năm sản xuất không hợp lý!\nVui lòng nhập từ năm 1900 đến " + (currentYear + 1));
                }

                // Validate ngày mua
                if (xe.NgayMua != null && xe.NgayMua > DateTime.Now)
                {
                    throw new Exception("Ngày mua xe không được lớn hơn ngày hiện tại!");
                }

                //  Thêm xe vào bảng XeMay
                bool insertXeSuccess = xeMayDAL.InsertXeMay(xe);
                
                if (!insertXeSuccess)
                {
                    throw new Exception("Không thể thêm xe vào hệ thống!");
                }

                //  Tự động thêm giá vào bảng ThongTinGiaXe
                try
                {
                    if (xe.MucDichSuDung == "Cho thuê")
                    {
                        // Tính giá thuê mặc định: 1% giá mua/ngày
                        decimal giaThueNgay = (xe.GiaMua ?? 0) * 0.01m;

                        bool insertGiaSuccess = xeMayDAL.InsertThongTinGiaXe(
                            xe.ID_Xe, 
                            "Thuê", 
                            giaThueNgay,
                            null  // Không có giá bán
                        );

                        if (!insertGiaSuccess)
                        {
                            MessageBox.Show(
                                "Xe đã được thêm thành công!\n\n" +
                                "Tuy nhiên, không thể tự động tạo giá thuê.\n" +
                                "Vui lòng vào Quản Lý Giá để cập nhật thủ công.",
                                "Cảnh báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                    }
                    else if (xe.MucDichSuDung == "Bán")
                    {
                        // Giá bán = Giá mua + 10% lãi
                        decimal giaBan = (xe.GiaMua ?? 0) * 1.1m;
                
                        bool insertGiaSuccess = xeMayDAL.InsertThongTinGiaXe(
                            xe.ID_Xe, 
                            "Bán", 
                            null,    // Không có giá thuê
                            giaBan
                        );

                        if (!insertGiaSuccess)
                        {
                            MessageBox.Show(
                                "Xe đã được thêm thành công!\n\n" +
                                "Tuy nhiên, không thể tự động tạo giá bán.\n" +
                                "Vui lòng vào Quản Lý Giá để cập nhật thủ công.",
                                "Cảnh báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                    }
                }
                catch (Exception exGia)
                {
                    // Log lỗi nhưng không throw (xe đã thêm thành công)
                    System.Diagnostics.Debug.WriteLine($"Lỗi thêm giá: {exGia.Message}");
            
                    MessageBox.Show(
                        "Xe đã được thêm thành công!\n\n" +
                        "Tuy nhiên, có lỗi khi tạo giá tự động:\n" +
                        exGia.Message + "\n\n" +
                        "Vui lòng vào Quản Lý Giá để cập nhật thủ công.",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Cập nhật thông tin xe
        public bool UpdateXeMay(XeMayDTO xe)
        {
            try
            {
                // Tạo list để chứa các lỗi
                List<string> errors = new List<string>();

                // Validate dữ liệu bắt buộc với tên trường cụ thể
                if (string.IsNullOrEmpty(xe.ID_Xe))
                    errors.Add("- Mã xe");

                if (string.IsNullOrEmpty(xe.ID_Loai))
                {
                    // Kiểm tra chi tiết hơn để biết thiếu trường nào
                    if (string.IsNullOrEmpty(xe.MaHang))
                        errors.Add("- Hãng xe");
                    if (string.IsNullOrEmpty(xe.MaDong))
                        errors.Add("- Dòng xe");
                    if (string.IsNullOrEmpty(xe.MaMau))
                        errors.Add("- Màu sắc");
                    if (xe.NamSX == null || xe.NamSX <= 0)
                        errors.Add("- Năm sản xuất");
                }

                if (string.IsNullOrEmpty(xe.TrangThai))
                    errors.Add("- Trạng thái xe");

                if (string.IsNullOrEmpty(xe.MucDichSuDung))
                    errors.Add("- Mục đích sử dụng (Cho thuê hoặc Bán)");

                if (xe.GiaMua == null || xe.GiaMua <= 0)
                    errors.Add("- Giá mua xe");

                if (xe.NgayMua == null)
                    errors.Add("- Ngày mua xe");

                if (string.IsNullOrEmpty(xe.ThongTinXang))
                    errors.Add("- Thông tin xăng");

                // Nếu có lỗi, thông báo chi tiết
                if (errors.Count > 0)
                {
                    string errorMessage = "Vui lòng nhập đầy đủ các thông tin sau:\n\n" + string.Join("\n", errors);
                    throw new Exception(errorMessage);
                }

                // Validate logic nghiệp vụ
                if (!string.IsNullOrEmpty(xe.BienSo) && xeMayDAL.IsBienSoExists(xe.BienSo, xe.ID_Xe))
                {
                    throw new Exception("Biển số xe '" + xe.BienSo + "' đã tồn tại trong hệ thống!");
                }

                // Validate mục đích sử dụng vs trạng thái
                if (xe.MucDichSuDung == "Cho thuê" && xe.TrangThai == "Đã bán")
                {
                    throw new Exception("Xe cho thuê không thể có trạng thái 'Đã bán'!\nVui lòng chọn trạng thái phù hợp.");
                }

                if (xe.MucDichSuDung == "Bán" && xe.TrangThai == "Đang thuê")
                {
                    throw new Exception("Xe bán không thể có trạng thái 'Đang thuê'!\nVui lòng chọn trạng thái phù hợp.");
                }

                // Validate năm sản xuất hợp lý
                int currentYear = DateTime.Now.Year;
                if (xe.NamSX != null && (xe.NamSX < 1900 || xe.NamSX > currentYear + 1))
                {
                    throw new Exception("Năm sản xuất không hợp lý!\nVui lòng nhập từ năm 1900 đến " + (currentYear + 1));
                }

                // Validate ngày mua
                if (xe.NgayMua != null && xe.NgayMua > DateTime.Now)
                {
                    throw new Exception("Ngày mua xe không được lớn hơn ngày hiện tại!");
                }

                return xeMayDAL.UpdateXeMay(xe);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Xóa xe
        public bool DeleteXeMay(string idXe)
        {
            try
            {
                if (string.IsNullOrEmpty(idXe))
                {
                    throw new Exception("Mã xe không được để trống!");
                }

                // Có thể thêm logic kiểm tra ràng buộc trước khi xóa
                // Ví dụ: kiểm tra xe có đang trong hợp đồng thuê không

                return xeMayDAL.DeleteXeMay(idXe);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa xe: " + ex.Message);
            }
        }

        


        // Cập nhật trạng thái
        public bool UpdateTrangThai(string idXe, string trangThai)
        {
            try
            {
                if (string.IsNullOrEmpty(idXe))
                {
                    throw new Exception("Mã xe không được để trống!");
                }

                if (string.IsNullOrEmpty(trangThai))
                {
                    throw new Exception("Trạng thái không được để trống!");
                }

                // Validate trạng thái hợp lệ
                string[] validStates = { "Sẵn sàng", "Đang thuê", "Đã bán", "Đang bảo trì" };
                bool isValid = false;
                foreach (string state in validStates)
                {
                    if (trangThai == state)
                    {
                        isValid = true;
                        break;
                    }
                }

                if (!isValid)
                {
                    throw new Exception("Trạng thái không hợp lệ!");
                }

                return xeMayDAL.UpdateTrangThai(idXe, trangThai);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật trạng thái: " + ex.Message);
            }
        }

        // Cập nhật số lượng đã bán (gọi khi có hóa đơn bán xe)
        public bool CapNhatSoLuongBanRa(string idXe, int soLuongBan = 1)
        {
            try
            {
                // Lấy thông tin xe hiện tại
                var xe = xeMayDAL.GetXeMayById(idXe);
                if (xe == null)
                {
                    throw new Exception("Không tìm thấy xe!");
                }

                // Tăng số lượng đã bán
                xe.SoLuongBanRa = (xe.SoLuongBanRa ?? 0) + soLuongBan;
                
                // Giảm số lượng tồn kho
                xe.SoLuong = (xe.SoLuong ?? 1) - soLuongBan;
                if (xe.SoLuong < 0) xe.SoLuong = 0;

                // Nếu hết hàng và là xe bán, cập nhật trạng thái
                if (xe.SoLuong == 0 && xe.MucDichSuDung == "Bán")
                {
                    xe.TrangThai = "Đã bán";
                }

                return xeMayDAL.UpdateXeMay(xe);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật số lượng bán: " + ex.Message);
            }
        }

        // Lấy thống kê xe bán chạy
        public DataTable GetThongKeBanChay(int top = 10)
        {
            try
            {
                string query = $@"
                    SELECT TOP {top}
                        xm.ID_Xe,
                        xm.BienSo,
                        hx.TenHang,
                        dx.TenDong,
                        xm.SoLuongBanRa,
                        xm.GiaMua,
                        xm.GiaNhap,
                        (xm.GiaMua - xm.GiaNhap) * xm.SoLuongBanRa as DoanhThu
                    FROM XeMay xm
                    INNER JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                    INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                    INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                    WHERE xm.SoLuongBanRa > 0
                    ORDER BY xm.SoLuongBanRa DESC, DoanhThu DESC";
                
                return DAL.DataProvider.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thống kê: " + ex.Message);
            }
        }

        /// Lấy danh sách xe có thể bán
        public DataTable GetXeCoTheBan()
        {
            try
            {
                DataTable dt = xeMayDAL.GetXeCoTheBan();
                
                if (dt.Rows.Count == 0)
                {
                    throw new Exception("Hiện tại không có xe nào có thể bán!\nVui lòng kiểm tra lại kho xe.");
                }
                
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Lấy danh sách xe có thể cho thuê (không kiểm tra lịch)
        public DataTable GetXeCoTheThue()
        {
            try
            {
                DataTable dt = xeMayDAL.GetXeCoTheThue();
                
                if (dt.Rows.Count == 0)
                {
                    throw new Exception("Hiện tại không có xe nào có thể cho thuê!\nVui lòng kiểm tra lại kho xe.");
                }
                
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Lấy danh sách xe có thể cho thuê trong khoảng thời gian (kiểm tra lịch trùng)
        public DataTable GetXeCoTheThueTheoThoiGian(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            try
            {
                // Validate thời gian
                if (ngayBatDau.Date >= ngayKetThuc.Date)
                {
                    throw new Exception("Ngày kết thúc phải lớn hơn ngày bắt đầu!");
                }

                if (ngayBatDau.Date < DateTime.Now.Date)
                {
                    throw new Exception("Ngày bắt đầu không được nhỏ hơn ngày hiện tại!");
                }

                // Gọi DAL để lấy xe khả dụng
                DataTable dt = xeMayDAL.GetXeCoTheThueTheoThoiGian(ngayBatDau, ngayKetThuc);

                if (dt == null)
                {
                    throw new Exception("Lỗi khi lấy danh sách xe!");
                }

                if (dt.Rows.Count == 0)
                {
                    throw new Exception(
                        $"Không có xe nào khả dụng từ {ngayBatDau:dd/MM/yyyy} đến {ngayKetThuc:dd/MM/yyyy}!\n\n" +
                        "Lý do có thể:\n" +
                        "• Tất cả xe đều đã được đặt trong thời gian này\n" +
                        "• Không có xe nào trong kho cho thuê\n" +
                        "• Vui lòng chọn thời gian khác!"
                    );
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Kiểm tra xe có thể thuê trong khoảng thời gian không
        public bool KiemTraXeKhaDungTheoThoiGian(string idXe, DateTime ngayBatDau, DateTime ngayKetThuc, out string errorMessage)
        {
            errorMessage = "";
            
            try
            {
                // Kiểm tra xe tồn tại và trạng thái
                XeMayDTO xe = xeMayDAL.GetXeMayById(idXe);
                if (xe == null)
                {
                    errorMessage = "Không tìm thấy xe!";
                    return false;
                }

                if (xe.TrangThai != "Sẵn sàng")
                {
                    errorMessage = $"Xe hiện đang ở trạng thái '{xe.TrangThai}'!";
                    return false;
                }

                if (xe.MucDichSuDung != "Cho thuê")
                {
                    errorMessage = "Xe này không dành cho thuê!";
                    return false;
                }

                // Kiểm tra lịch trùng
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

                int count = Convert.ToInt32(DAL.DataProvider.ExecuteScalar(query, parameters));

                if (count > 0)
                {
                    errorMessage = "Xe đã có lịch thuê trong khoảng thời gian này!";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi kiểm tra: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Lấy giá xe theo loại (Bán hoặc Thuê)
        /// </summary>
        public DataTable GetGiaXe(string idXe, string phanLoai)
        {
            try
            {
                return xeMayDAL.GetGiaXe(idXe, phanLoai);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy giá xe: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách loại xe sẵn sàng bán
        /// </summary>
        public DataTable GetLoaiXeSanSangBan()
        {
            try
            {
                return xeMayDAL.GetLoaiXeSanSangBan();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách loại xe bán: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy xe theo loại để bán
        /// </summary>
        public DataTable GetXeTheoLoaiDeBan(string idLoai)
        {
            try
            {
                return xeMayDAL.GetXeTheoLoaiDeBan(idLoai);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy xe theo loại: " + ex.Message);
            }
        }
        // Thêm 3 methods này vào class XeMayBLL

        public bool SoftDeleteXeMay(string idXe)
        {
            try
            {
                // Kiểm tra xe đã có giao dịch chưa
                if (KiemTraXeCoGiaoDich(idXe))
                {
                    // Chuyển trạng thái thay vì xóa
                    return CapNhatTrangThaiXe(idXe, "Đã xóa");
                }
                else
                {
                    // Nếu chưa có giao dịch thì có thể xóa hẳn
                    return DeleteXeMay(idXe);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa xe: " + ex.Message);
            }
        }

        public bool KiemTraXeCoGiaoDich(string idXe)
        {
            return xeMayDAL.KiemTraXeCoGiaoDich(idXe);
        }

        public bool CapNhatTrangThaiXe(string idXe, string trangThai)
        {
            return xeMayDAL.CapNhatTrangThaiXe(idXe, trangThai);
        }

        /// <summary>
        /// Kiểm tra có thể xóa xe không
        /// </summary>
        public bool CanDeleteXe(string idXe, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(idXe))
                {
                    errorMessage = "Mã xe không hợp lệ!";
                    return false;
                }

                // 1. Kiểm tra xe đang được thuê
                if (xeMayDAL.IsXeDangThue(idXe))
                {
                    errorMessage = "Xe đang được thuê!\nKhông thể xóa xe đang trong giao dịch thuê.";
                    return false;
                }

                // 2. Kiểm tra xe trong giao dịch bán
                if (xeMayDAL.IsXeInGiaoDichBan(idXe))
                {
                    errorMessage = "Xe đang trong giao dịch bán!\nKhông thể xóa xe đang có đơn mua.";
                    return false;
                }

                // 3. Kiểm tra xe đang bảo trì
                if (xeMayDAL.IsXeDangBaoTri(idXe))
                {
                    errorMessage = "Xe đang bảo trì!\nVui lòng hoàn thành bảo trì trước khi xóa.";
                    return false;
                }

                // 4. Cảnh báo nếu có lịch sử
                if (KiemTraXeCoGiaoDich(idXe))
                {
                    errorMessage = "⚠ Xe có lịch sử giao dịch!\n" +
                                  "Xóa xe sẽ ẢNH HƯỞNG đến dữ liệu thống kê và báo cáo.";
                    // Vẫn cho phép xóa nhưng cảnh báo
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi kiểm tra ràng buộc: {ex.Message}";
                return false;
            }
        }

    }
}
