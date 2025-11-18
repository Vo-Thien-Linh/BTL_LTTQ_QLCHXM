using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

                return xeMayDAL.InsertXeMay(xe);
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

        public DataTable GetXeCoTheThue()
        {
            return xeMayDAL.GetXeCoTheThue();
        }
    }
}
