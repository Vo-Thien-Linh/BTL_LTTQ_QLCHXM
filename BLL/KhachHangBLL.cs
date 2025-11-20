using System;
using System.Data;
using System.Text.RegularExpressions;
using DAL;
using DTO;

namespace BLL
{
    public class KhachHangBLL
    {
        private KhachHangDAL khachHangDAL;

        public KhachHangBLL()
        {
            khachHangDAL = new KhachHangDAL();
        }

        public DataTable GetAllKhachHang()
        {
            return khachHangDAL.GetAllKhachHang();
        }

        public DataTable SearchKhachHang(string searchBy, string keyword)
        {
            return khachHangDAL.SearchKhachHang(searchBy, keyword);
        }

        public DataTable GetKhachHangByMaKH(string maKH)
        {
            return khachHangDAL.GetKhachHangByMaKH(maKH);
        }

        public bool InsertKhachHang(KhachHangDTO kh, out string errorMessage)
        {
            errorMessage = "";

            if (!ValidateKhachHang(kh, out errorMessage, false))
            {
                return false;
            }

            if (khachHangDAL.CheckMaKHExists(kh.MaKH))
            {
                errorMessage = "Mã khách hàng đã tồn tại!";
                return false;
            }

            // ✅ KIỂM TRA TRÙNG TRONG BẢNG KHÁCH HÀNG
            if (!string.IsNullOrEmpty(kh.Sdt) && khachHangDAL.CheckSdtExists(kh.Sdt))
            {
                errorMessage = "Số điện thoại đã được sử dụng bởi khách hàng khác!";
                return false;
            }

            // ✅ KIỂM TRA TRÙNG VỚI BẢNG NHÂN VIÊN (MỚI THÊM)
            if (!string.IsNullOrEmpty(kh.Sdt) && khachHangDAL.CheckSdtExistsInNhanVien(kh.Sdt))
            {
                errorMessage = "Số điện thoại đã được sử dụng bởi nhân viên trong hệ thống!";
                return false;
            }

            if (!string.IsNullOrEmpty(kh.Email) && khachHangDAL.CheckEmailExists(kh.Email))
            {
                errorMessage = "Email đã được sử dụng!";
                return false;
            }

            return khachHangDAL.InsertKhachHang(kh);
        }

        public bool UpdateKhachHang(KhachHangDTO kh, out string errorMessage)
        {
            errorMessage = "";

            if (!ValidateKhachHang(kh, out errorMessage, true))
            {
                return false;
            }

            // ✅ KIỂM TRA TRÙNG TRONG BẢNG KHÁCH HÀNG
            if (!string.IsNullOrEmpty(kh.Sdt) && khachHangDAL.CheckSdtExists(kh.Sdt, kh.MaKH))
            {
                errorMessage = "Số điện thoại đã được sử dụng bởi khách hàng khác!";
                return false;
            }

            // ✅ KIỂM TRA TRÙNG VỚI BẢNG NHÂN VIÊN (MỚI THÊM)
            if (!string.IsNullOrEmpty(kh.Sdt) && khachHangDAL.CheckSdtExistsInNhanVien(kh.Sdt))
            {
                errorMessage = "Số điện thoại đã được sử dụng bởi nhân viên trong hệ thống!";
                return false;
            }

            if (!string.IsNullOrEmpty(kh.Email) && khachHangDAL.CheckEmailExists(kh.Email, kh.MaKH))
            {
                errorMessage = "Email đã được sử dụng bởi khách hàng khác!";
                return false;
            }

            return khachHangDAL.UpdateKhachHang(kh);
        }

        public bool DeleteKhachHang(string maKH, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrEmpty(maKH))
            {
                errorMessage = "Mã khách hàng không hợp lệ!";
                return false;
            }

            try
            {
                return khachHangDAL.DeleteKhachHang(maKH);
            }
            catch
            {
                errorMessage = "Không thể xóa khách hàng! Có thể khách hàng đang có dữ liệu liên quan.";
                return false;
            }
        }

        public string GenerateMaKH()
        {
            return khachHangDAL.GenerateMaKH();
        }

        private bool ValidateKhachHang(KhachHangDTO kh, out string errorMessage, bool isUpdate)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(kh.MaKH))
            {
                errorMessage = "Mã khách hàng không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(kh.HoTenKH))
            {
                errorMessage = "Họ tên khách hàng không được để trống!";
                return false;
            }

            if (kh.HoTenKH.Length < 3 || kh.HoTenKH.Length > 100)
            {
                errorMessage = "Họ tên phải từ 3 đến 100 ký tự!";
                return false;
            }

            if (kh.NgaySinh.HasValue)
            {
                int age = DateTime.Now.Year - kh.NgaySinh.Value.Year;
                if (kh.NgaySinh > DateTime.Now.AddYears(-age)) age--;

                if (age < 0)
                {
                    errorMessage = "Ngày sinh không hợp lệ!";
                    return false;
                }

                if (age > 150)
                {
                    errorMessage = "Ngày sinh không hợp lệ!";
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(kh.Sdt))
            {
                if (!Regex.IsMatch(kh.Sdt, @"^0\d{9}$"))
                {
                    errorMessage = "Số điện thoại không hợp lệ! (10 số, bắt đầu bằng 0)";
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(kh.Email))
            {
                if (!Regex.IsMatch(kh.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    errorMessage = "Email không hợp lệ!";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Tìm khách hàng theo số điện thoại
        /// </summary>
        public KhachHangDTO GetKhachHangBySdt(string sdt)
        {
            return khachHangDAL.GetKhachHangBySdt(sdt);
        }

        /// <summary>
        /// Kiểm tra có thể xóa khách hàng không
        /// </summary>
        public bool CanDeleteKhachHang(string maKH, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // 1. Kiểm tra đang trong giao dịch thuê
                if (khachHangDAL.IsKhachHangInGiaoDichThue(maKH))
                {
                    errorMessage = "Khách hàng đang có giao dịch thuê xe chưa hoàn thành!\n" +
                                  "(Trạng thái: Chờ duyệt / Đã thanh toán / Đang thuê)";
                    return false;
                }

                // 2. Kiểm tra đang có đơn mua chờ duyệt
                if (khachHangDAL.IsKhachHangInGiaoDichBan(maKH))
                {
                    errorMessage = "Khách hàng đang có đơn mua xe chờ duyệt!";
                    return false;
                }

                // 3. Cảnh báo nếu có lịch sử giao dịch
                DataTable dtLichSu = khachHangDAL.GetLichSuGiaoDichKhachHang(maKH);
                if (dtLichSu.Rows.Count > 0)
                {
                    errorMessage = $"⚠ Khách hàng có {dtLichSu.Rows.Count} giao dịch trong lịch sử.\n" +
                                  "Xóa khách hàng sẽ ẢNH HƯỞNG đến dữ liệu thống kê!";
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

        /// <summary>
        /// Kiểm tra số điện thoại đã tồn tại (cho thêm mới)
        /// </summary>
        /// <param name="sdt">Số điện thoại cần kiểm tra</param>
        /// <returns>true nếu đã tồn tại, false nếu chưa</returns>
        public bool IsSDTExists(string sdt)
        {
            try
            {
                return khachHangDAL.IsSDTExists(sdt);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi kiểm tra số điện thoại: {ex.Message}");
            }
        }

        /// <summary>
        /// Kiểm tra số điện thoại đã tồn tại (cho sửa - loại trừ KH hiện tại)
        /// </summary>
        /// <param name="sdt">Số điện thoại cần kiểm tra</param>
        /// <param name="excludeMaKH">Mã KH cần loại trừ</param>
        /// <returns>true nếu đã tồn tại, false nếu chưa</returns>
        public bool IsSDTExists(string sdt, string excludeMaKH)
        {
            try
            {
                return khachHangDAL.IsSDTExists(sdt, excludeMaKH);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi kiểm tra số điện thoại: {ex.Message}");
            }
        }

        /// <summary>
        /// Kiểm tra email đã tồn tại (cho thêm mới)
        /// </summary>
        /// <param name="email">Email cần kiểm tra</param>
        /// <returns>true nếu đã tồn tại, false nếu chưa</returns>
        public bool IsEmailExists(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                return khachHangDAL.IsEmailExists(email);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi kiểm tra email: {ex.Message}");
            }
        }

        /// <summary>
        /// Kiểm tra email đã tồn tại (cho sửa - loại trừ KH hiện tại)
        /// </summary>
        /// <param name="email">Email cần kiểm tra</param>
        /// <param name="excludeMaKH">Mã KH cần loại trừ</param>
        /// <returns>true nếu đã tồn tại, false nếu chưa</returns>
        public bool IsEmailExists(string email, string excludeMaKH)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                return khachHangDAL.IsEmailExists(email, excludeMaKH);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi kiểm tra email: {ex.Message}");
            }
        }

       
    }
}