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

        public bool InsertKhachHang(KhachHang kh, out string errorMessage)
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

        public bool UpdateKhachHang(KhachHang kh, out string errorMessage)
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

        private bool ValidateKhachHang(KhachHang kh, out string errorMessage, bool isUpdate)
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
    }
}