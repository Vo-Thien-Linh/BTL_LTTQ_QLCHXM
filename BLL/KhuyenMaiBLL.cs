using System;
using System.Data;
using DAL;
using DTO;

namespace BLL
{
    public class KhuyenMaiBLL
    {
        private KhuyenMaiDAL khuyenMaiDAL;

        public KhuyenMaiBLL()
        {
            khuyenMaiDAL = new KhuyenMaiDAL();
        }

        /// <summary>
        /// Lấy tất cả khuyến mãi
        /// </summary>
        public DataTable GetAllKhuyenMai()
        {
            return khuyenMaiDAL.GetAllKhuyenMai();
        }

        /// <summary>
        /// Lấy khuyến mãi theo trạng thái
        /// </summary>
        public DataTable GetKhuyenMaiByTrangThai(string trangThai)
        {
            return khuyenMaiDAL.GetKhuyenMaiByTrangThai(trangThai);
        }

        /// <summary>
        /// Lấy khuyến mãi đang hoạt động
        /// </summary>
        public DataTable GetKhuyenMaiHoatDong(string loaiApDung = null)
        {
            return khuyenMaiDAL.GetKhuyenMaiHoatDong(loaiApDung);
        }

        /// <summary>
        /// Lấy khuyến mãi còn hiệu lực theo ngày và loại áp dụng
        /// </summary>
        public DataTable GetKhuyenMaiHieuLuc(DateTime? ngayApDung = null, string loaiApDung = null)
        {
            return khuyenMaiDAL.GetKhuyenMaiHieuLuc(ngayApDung, loaiApDung);
        }

        /// <summary>
        /// Tính giá trị giảm từ khuyến mãi
        /// </summary>
        public decimal TinhGiaTriGiam(string maKM, decimal giaTriDonHang, out string errorMessage)
        {
            errorMessage = "";
            
            if (string.IsNullOrWhiteSpace(maKM))
            {
                errorMessage = "Mã khuyến mãi không được để trống!";
                return 0;
            }

            if (giaTriDonHang <= 0)
            {
                errorMessage = "Giá trị đơn hàng phải lớn hơn 0!";
                return 0;
            }

            try
            {
                return khuyenMaiDAL.TinhGiaTriGiam(maKM, giaTriDonHang, out errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi tính giá trị giảm: " + ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// Tự động cập nhật trạng thái của các khuyến mãi đã hết hạn
        /// </summary>
        /// <returns>Số lượng khuyến mãi đã được cập nhật</returns>
        public int CapNhatTrangThaiKhuyenMaiHetHan()
        {
            try
            {
                return khuyenMaiDAL.CapNhatTrangThaiKhuyenMaiHetHan();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"❌ Lỗi BLL CapNhatTrangThaiKhuyenMaiHetHan: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Kiểm tra khuyến mãi có còn hiệu lực không
        /// </summary>
        public bool KiemTraKhuyenMaiHieuLuc(string maKM, out string errorMessage)
        {
            errorMessage = "";
            
            if (string.IsNullOrWhiteSpace(maKM))
            {
                errorMessage = "Mã khuyến mãi không được để trống!";
                return false;
            }

            try
            {
                string query = @"
                    SELECT 
                        NgayBatDau,
                        NgayKetThuc,
                        TrangThai,
                        TenKM
                    FROM KhuyenMai
                    WHERE MaKM = @MaKM";

                System.Data.SqlClient.SqlParameter[] parameters = {
                    new System.Data.SqlClient.SqlParameter("@MaKM", maKM)
                };

                DataTable dt = DAL.DataProvider.ExecuteQuery(query, parameters);

                if (dt.Rows.Count == 0)
                {
                    errorMessage = "Khuyến mãi không tồn tại!";
                    return false;
                }

                DataRow row = dt.Rows[0];
                string trangThai = row["TrangThai"].ToString();
                DateTime ngayBatDau = Convert.ToDateTime(row["NgayBatDau"]);
                DateTime ngayKetThuc = Convert.ToDateTime(row["NgayKetThuc"]);
                DateTime ngayHienTai = DateTime.Now;

                if (trangThai != "Hoạt động")
                {
                    errorMessage = $"Khuyến mãi '{row["TenKM"]}' đang ở trạng thái: {trangThai}";
                    return false;
                }

                if (ngayHienTai < ngayBatDau)
                {
                    errorMessage = $"Khuyến mãi '{row["TenKM"]}' chưa đến thời gian áp dụng!\n" +
                                  $"Ngày bắt đầu: {ngayBatDau:dd/MM/yyyy}\n" +
                                  $"Ngày hiện tại: {ngayHienTai:dd/MM/yyyy}";
                    return false;
                }

                if (ngayHienTai > ngayKetThuc)
                {
                    errorMessage = $"Khuyến mãi '{row["TenKM"]}' đã hết hạn!\n" +
                                  $"Ngày kết thúc: {ngayKetThuc:dd/MM/yyyy}\n" +
                                  $"Ngày hiện tại: {ngayHienTai:dd/MM/yyyy}";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi kiểm tra khuyến mãi: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Thêm khuyến mãi mới
        /// </summary>
        public bool InsertKhuyenMai(KhuyenMaiDTO km, out string errorMessage)
        {
            errorMessage = "";

            // Validate
            if (!ValidateKhuyenMai(km, out errorMessage))
                return false;

            // Kiểm tra trùng mã
            if (khuyenMaiDAL.CheckMaKMExists(km.MaKM))
            {
                errorMessage = "Mã khuyến mãi đã tồn tại!";
                return false;
            }

            return khuyenMaiDAL.InsertKhuyenMai(km, out errorMessage);
        }

        /// <summary>
        /// Cập nhật khuyến mãi
        /// </summary>
        public bool UpdateKhuyenMai(KhuyenMaiDTO km, out string errorMessage)
        {
            errorMessage = "";

            // Validate
            if (!ValidateKhuyenMai(km, out errorMessage))
                return false;

            return khuyenMaiDAL.UpdateKhuyenMai(km, out errorMessage);
        }

        /// <summary>
        /// Xóa khuyến mãi
        /// </summary>
        public bool DeleteKhuyenMai(string maKM, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(maKM))
            {
                errorMessage = "Mã khuyến mãi không được để trống!";
                return false;
            }

            return khuyenMaiDAL.DeleteKhuyenMai(maKM, out errorMessage);
        }

        /// <summary>
        /// Tự động tạo mã khuyến mãi
        /// </summary>
        public string GenerateMaKM()
        {
            return khuyenMaiDAL.GenerateMaKM();
        }

        /// <summary>
        /// Validate dữ liệu khuyến mãi
        /// </summary>
        private bool ValidateKhuyenMai(KhuyenMaiDTO km, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(km.MaKM))
            {
                errorMessage = "Mã khuyến mãi không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(km.TenKM))
            {
                errorMessage = "Tên khuyến mãi không được để trống!";
                return false;
            }

            if (km.NgayKetThuc < km.NgayBatDau)
            {
                errorMessage = "Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!";
                return false;
            }

            if (km.LoaiKhuyenMai == "Giảm %")
            {
                if (!km.PhanTramGiam.HasValue || km.PhanTramGiam <= 0 || km.PhanTramGiam > 100)
                {
                    errorMessage = "Phần trăm giảm phải từ 0 đến 100!";
                    return false;
                }
            }
            else if (km.LoaiKhuyenMai == "Giảm tiền")
            {
                if (!km.SoTienGiam.HasValue || km.SoTienGiam <= 0)
                {
                    errorMessage = "Số tiền giảm phải lớn hơn 0!";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Tính giá trị giảm dựa trên khuyến mãi
        /// </summary>
        public decimal TinhGiaTriGiam(KhuyenMaiDTO km, decimal giaTriDonHang)
        {
            decimal giaTriGiam = 0;

            if (km.LoaiKhuyenMai == "Giảm %")
            {
                giaTriGiam = giaTriDonHang * (km.PhanTramGiam.Value / 100);

                // Áp dụng giới hạn tối đa nếu có
                if (km.GiaTriGiamToiDa.HasValue && giaTriGiam > km.GiaTriGiamToiDa.Value)
                {
                    giaTriGiam = km.GiaTriGiamToiDa.Value;
                }
            }
            else if (km.LoaiKhuyenMai == "Giảm tiền")
            {
                giaTriGiam = km.SoTienGiam.Value;
            }

            // Không giảm quá giá trị đơn hàng
            if (giaTriGiam > giaTriDonHang)
            {
                giaTriGiam = giaTriDonHang;
            }

            return giaTriGiam;
        }
    }
}
