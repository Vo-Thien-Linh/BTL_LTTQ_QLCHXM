using System;
using System.Data;
using System.Text.RegularExpressions;
using DAL;
using DTO;

namespace BLL
{
    public class NhanVienBLL
    {
        private NhanVienDAL nhanVienDAL;

        public NhanVienBLL()
        {
            nhanVienDAL = new NhanVienDAL();
        }

        public DataTable GetAllNhanVien()
        {
            return nhanVienDAL.GetAllNhanVien();
        }

        public DataTable SearchNhanVien(string searchBy, string keyword)
        {
            return nhanVienDAL.SearchNhanVien(searchBy, keyword);
        }

        public DataTable GetNhanVienByMaNV(string maNV)
        {
            return nhanVienDAL.GetNhanVienByMaNV(maNV);
        }

        public bool InsertNhanVien(NhanVien nv, out string errorMessage)
        {
            errorMessage = "";

            if (!ValidateNhanVien(nv, out errorMessage, false))
            {
                return false;
            }

            if (nhanVienDAL.CheckMaNVExists(nv.MaNV))
            {
                errorMessage = "Mã nhân viên đã tồn tại!";
                return false;
            }

            // ✅ KIỂM TRA TRÙNG TRONG BẢNG NHÂN VIÊN
            if (!string.IsNullOrEmpty(nv.Sdt) && nhanVienDAL.CheckSdtExists(nv.Sdt))
            {
                errorMessage = "Số điện thoại đã được sử dụng bởi nhân viên khác!";
                return false;
            }

            // ✅ KIỂM TRA TRÙNG VỚI BẢNG KHÁCH HÀNG (MỚI THÊM)
            if (!string.IsNullOrEmpty(nv.Sdt) && nhanVienDAL.CheckSdtExistsInKhachHang(nv.Sdt))
            {
                errorMessage = "Số điện thoại đã được sử dụng bởi khách hàng trong hệ thống!";
                return false;
            }

            if (!string.IsNullOrEmpty(nv.Email) && nhanVienDAL.CheckEmailExists(nv.Email))
            {
                errorMessage = "Email đã được sử dụng!";
                return false;
            }

            if (!string.IsNullOrEmpty(nv.CCCD) && nhanVienDAL.CheckCCCDExists(nv.CCCD))
            {
                errorMessage = "CCCD đã được sử dụng!";
                return false;
            }

            return nhanVienDAL.InsertNhanVien(nv);
        }

        public bool UpdateNhanVien(NhanVien nv, out string errorMessage)
        {
            errorMessage = "";

            if (!ValidateNhanVien(nv, out errorMessage, true))
            {
                return false;
            }

            // ✅ KIỂM TRA TRÙNG TRONG BẢNG NHÂN VIÊN
            if (!string.IsNullOrEmpty(nv.Sdt) && nhanVienDAL.CheckSdtExists(nv.Sdt, nv.MaNV))
            {
                errorMessage = "Số điện thoại đã được sử dụng bởi nhân viên khác!";
                return false;
            }

            // ✅ KIỂM TRA TRÙNG VỚI BẢNG KHÁCH HÀNG (MỚI THÊM)
            if (!string.IsNullOrEmpty(nv.Sdt) && nhanVienDAL.CheckSdtExistsInKhachHang(nv.Sdt))
            {
                errorMessage = "Số điện thoại đã được sử dụng bởi khách hàng trong hệ thống!";
                return false;
            }

            if (!string.IsNullOrEmpty(nv.Email) && nhanVienDAL.CheckEmailExists(nv.Email, nv.MaNV))
            {
                errorMessage = "Email đã được sử dụng bởi nhân viên khác!";
                return false;
            }

            if (!string.IsNullOrEmpty(nv.CCCD) && nhanVienDAL.CheckCCCDExists(nv.CCCD, nv.MaNV))
            {
                errorMessage = "CCCD đã được sử dụng bởi nhân viên khác!";
                return false;
            }

            return nhanVienDAL.UpdateNhanVien(nv);
        }
        public bool DeleteNhanVien(string maNV, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrEmpty(maNV))
            {
                errorMessage = "Mã nhân viên không hợp lệ!";
                return false;
            }

            try
            {
                return nhanVienDAL.DeleteNhanVien(maNV);
            }
            catch (Exception ex)
            {
                errorMessage = "Không thể xóa nhân viên! Có thể nhân viên đang có dữ liệu liên quan.";
                return false;
            }
        }

        public string GenerateMaNV()
        {
            return nhanVienDAL.GenerateMaNV();
        }

        private bool ValidateNhanVien(NhanVien nv, out string errorMessage, bool isUpdate)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(nv.MaNV))
            {
                errorMessage = "Mã nhân viên không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(nv.HoTenNV))
            {
                errorMessage = "Họ tên nhân viên không được để trống!";
                return false;
            }

            if (nv.HoTenNV.Length < 3 || nv.HoTenNV.Length > 100)
            {
                errorMessage = "Họ tên phải từ 3 đến 100 ký tự!";
                return false;
            }

            int age = DateTime.Now.Year - nv.NgaySinh.Year;
            if (nv.NgaySinh > DateTime.Now.AddYears(-age)) age--;

            if (age < 18)
            {
                errorMessage = "Nhân viên phải từ 18 tuổi trở lên!";
                return false;
            }

            if (age > 65)
            {
                errorMessage = "Nhân viên không được quá 65 tuổi!";
                return false;
            }

            if (!string.IsNullOrEmpty(nv.Sdt))
            {
                if (!Regex.IsMatch(nv.Sdt, @"^0\d{9}$"))
                {
                    errorMessage = "Số điện thoại không hợp lệ! (10 số, bắt đầu bằng 0)";
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(nv.Email))
            {
                if (!Regex.IsMatch(nv.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    errorMessage = "Email không hợp lệ!";
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(nv.CCCD))
            {
                if (!Regex.IsMatch(nv.CCCD, @"^\d{12}$"))
                {
                    errorMessage = "CCCD không hợp lệ! (12 số)";
                    return false;
                }
            }

            if (nv.LuongCoBan.HasValue && nv.LuongCoBan.Value < 0)
            {
                errorMessage = "Lương cơ bản không được âm!";
                return false;
            }

            return true;
        }
    }
}

