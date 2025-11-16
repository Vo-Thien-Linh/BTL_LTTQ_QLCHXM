using System;
using System.Data;
using DAL;
using DTO;

namespace BLL
{
    public class GiaoDichThueBLL
    {
        private GiaoDichThueDAL giaoDichThueDAL;

        public GiaoDichThueBLL()
        {
            giaoDichThueDAL = new GiaoDichThueDAL();
        }

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
                // Kiểm tra trạng thái (có thể thêm method GetById trong DAL nếu cần)
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

                // Duyệt đơn thuê
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
                // Kiểm tra trạng thái
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

                // Từ chối đơn thuê
                return giaoDichThueDAL.RejectGiaoDichThue(maGDThue, nguoiDuyet, lyDo);
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi từ chối giao dịch thuê: " + ex.Message;
                return false;
            }
        }

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
        /// Lấy thống kê giao dịch thuê
        /// </summary>
        public DataTable GetThongKeGiaoDichThue(DateTime tuNgay, DateTime denNgay)
        {
            // Có thể mở rộng thêm phương thức này trong DAL nếu cần
            return giaoDichThueDAL.GetAllGiaoDichThue();
        }
    }
}