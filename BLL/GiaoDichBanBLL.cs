using System;
using System.Data;
using DAL;
using DTO;

namespace BLL
{
    public class GiaoDichBanBLL
    {
        private GiaoDichBanDAL giaoDichBanDAL;

        public GiaoDichBanBLL()
        {
            giaoDichBanDAL = new GiaoDichBanDAL();
        }

        /// <summary>
        /// Lấy tất cả giao dịch bán
        /// </summary>
        public DataTable GetAllGiaoDichBan()
        {
            return giaoDichBanDAL.GetAllGiaoDichBan();
        }

        /// <summary>
        /// Lấy giao dịch bán theo trạng thái duyệt
        /// </summary>
        public DataTable GetGiaoDichBanByTrangThai(string trangThaiDuyet)
        {
            return giaoDichBanDAL.GetGiaoDichBanByTrangThai(trangThaiDuyet);
        }

        /// <summary>
        /// Lấy giao dịch bán theo mã
        /// </summary>
        public DataTable GetGiaoDichBanByMa(int maGDBan)
        {
            return giaoDichBanDAL.GetGiaoDichBanByMa(maGDBan);
        }

        /// <summary>
        /// Thêm giao dịch bán mới
        /// </summary>
        public bool InsertGiaoDichBan(GiaoDichBan gd, out string errorMessage)
        {
            errorMessage = "";

            // Validate dữ liệu
            if (!ValidateGiaoDichBan(gd, out errorMessage))
            {
                return false;
            }

            try
            {
                return giaoDichBanDAL.InsertGiaoDichBan(gd);
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi thêm giao dịch bán: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Duyệt đơn hàng bán
        /// </summary>
        public bool ApproveGiaoDichBan(int maGDBan, string nguoiDuyet, string ghiChu, out string errorMessage)
        {
            errorMessage = "";

            if (maGDBan <= 0)
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
                // Lấy thông tin giao dịch
                DataTable dt = giaoDichBanDAL.GetGiaoDichBanByMa(maGDBan);
                if (dt.Rows.Count == 0)
                {
                    errorMessage = "Không tìm thấy giao dịch!";
                    return false;
                }

                string trangThaiHienTai = dt.Rows[0]["TrangThaiDuyet"].ToString();
                if (trangThaiHienTai != "Chờ duyệt")
                {
                    errorMessage = $"Không thể duyệt đơn hàng có trạng thái '{trangThaiHienTai}'!";
                    return false;
                }

                // Duyệt đơn hàng
                bool approveResult = giaoDichBanDAL.ApproveGiaoDichBan(maGDBan, nguoiDuyet, ghiChu);

                if (approveResult)
                {
                    // Cập nhật trạng thái xe thành "Đã bán"
                    string idXe = dt.Rows[0]["ID_Xe"].ToString();
                    giaoDichBanDAL.UpdateTrangThaiXe(idXe, "Đã bán");
                }

                return approveResult;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi duyệt giao dịch: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Từ chối đơn hàng bán
        /// </summary>
        public bool RejectGiaoDichBan(int maGDBan, string nguoiDuyet, string lyDo, out string errorMessage)
        {
            errorMessage = "";

            if (maGDBan <= 0)
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
                DataTable dt = giaoDichBanDAL.GetGiaoDichBanByMa(maGDBan);
                if (dt.Rows.Count == 0)
                {
                    errorMessage = "Không tìm thấy giao dịch!";
                    return false;
                }

                string trangThaiHienTai = dt.Rows[0]["TrangThaiDuyet"].ToString();
                if (trangThaiHienTai != "Chờ duyệt")
                {
                    errorMessage = $"Không thể từ chối đơn hàng có trạng thái '{trangThaiHienTai}'!";
                    return false;
                }

                // Từ chối đơn hàng
                return giaoDichBanDAL.RejectGiaoDichBan(maGDBan, nguoiDuyet, lyDo);
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi từ chối giao dịch: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Tìm kiếm giao dịch bán
        /// </summary>
        public DataTable SearchGiaoDichBan(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetAllGiaoDichBan();
            }

            return giaoDichBanDAL.SearchGiaoDichBan(keyword);
        }

        /// <summary>
        /// Validate dữ liệu giao dịch bán
        /// </summary>
        private bool ValidateGiaoDichBan(GiaoDichBan gd, out string errorMessage)
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

            if (gd.GiaBan <= 0)
            {
                errorMessage = "Giá bán phải lớn hơn 0!";
                return false;
            }

            if (gd.NgayBan > DateTime.Now)
            {
                errorMessage = "Ngày bán không được lớn hơn ngày hiện tại!";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Lấy thống kê giao dịch bán
        /// </summary>
        public DataTable GetThongKeGiaoDichBan(DateTime tuNgay, DateTime denNgay)
        {
            // Có thể mở rộng thêm phương thức này trong DAL nếu cần
            return giaoDichBanDAL.GetAllGiaoDichBan();
        }
    }
}