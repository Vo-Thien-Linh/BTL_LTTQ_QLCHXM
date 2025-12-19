using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using DAL;
using DTO;

namespace BLL
{
    public class NhanVienBLL
    {
        private NhanVienDAL nhanVienDAL = new NhanVienDAL();

        public DataTable GetAllNhanVien() => nhanVienDAL.GetAllNhanVien();
        public DataTable SearchNhanVien(string searchBy, string keyword) => nhanVienDAL.SearchNhanVien(searchBy, keyword);
        public DataTable GetNhanVienByMaNV(string maNV) => nhanVienDAL.GetNhanVienByMaNV(maNV);
        public string GenerateMaNV() => nhanVienDAL.GenerateMaNV();

        // ================== THÊM NHÂN VIÊN + TẠO TÀI KHOẢN ==================
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

            // ✅ KIỂM TRA TRÙNG VỚI BẢNG KHÁCH HÀNG
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

            // ✅ 1. THÊM NHÂN VIÊN TRƯỚC
            bool insertSuccess = nhanVienDAL.InsertNhanVien(nv);

            if (!insertSuccess)
            {
                errorMessage = "Không thể thêm nhân viên vào database!";
                return false;
            }

            // ✅ 2. SAU ĐÓ MỚI TẠO TÀI KHOẢN
            if (!string.IsNullOrEmpty(nv.Email) && !string.IsNullOrEmpty(nv.Sdt) && !string.IsNullOrEmpty(nv.Password))
            {
                try
                {
                    TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();
                    string taiKhoanError;
                    bool taiKhoanSuccess = taiKhoanBLL.CreateAccountForEmployeeWithPassword(nv.MaNV, nv.Email, nv.Sdt, nv.Password, nv.ChucVu, out taiKhoanError);

                    if (!taiKhoanSuccess)
                    {
                        // ⚠️ Đã thêm nhân viên nhưng không tạo được tài khoản
                        System.Diagnostics.Debug.WriteLine($"⚠️ Cảnh báo: Không tạo được tài khoản - {taiKhoanError}");
                        System.Console.WriteLine($"⚠️ CẢNH BÁO: Không tạo được tài khoản cho nhân viên {nv.MaNV}");
                        System.Console.WriteLine($"⚠️ Lý do: {taiKhoanError}");

                        // Có thể rollback nhân viên hoặc để user tạo tài khoản sau
                        // nhanVienDAL.DeleteNhanVien(nv.MaNV); // Nếu muốn rollback

                        errorMessage = $"Đã thêm nhân viên thành công nhưng không tạo được tài khoản.\n\nLý do: {taiKhoanError}\n\nVui lòng kiểm tra lại thông tin hoặc tạo tài khoản thủ công sau.";
                        // ⚠️ Vẫn return true vì nhân viên đã được thêm
                        return true;
                    }
                    else
                    {
                        System.Console.WriteLine($"✅ Tạo tài khoản thành công cho nhân viên {nv.MaNV}");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ Lỗi tạo tài khoản: {ex.Message}");
                    System.Console.WriteLine($"❌ LỖI: {ex.Message}");
                    errorMessage = $"Đã thêm nhân viên thành công nhưng lỗi khi tạo tài khoản: {ex.Message}";
                    return true; // Vẫn return true vì nhân viên đã được thêm
                }
            }

            return true; // ✅ Nhân viên đã được thêm thành công
        }

        // ================== SỬA NHÂN VIÊN + ĐỔI MẬT KHẨU (nếu có nhập) ==================
        public bool UpdateNhanVien(NhanVien nv, out string errorMessage)
        {
            errorMessage = "";

            if (!ValidateNhanVien(nv, out errorMessage, true)) return false;

            // Kiểm tra trùng (trừ bản thân)
            if (!string.IsNullOrEmpty(nv.Sdt) && nhanVienDAL.CheckSdtExists(nv.Sdt, nv.MaNV)) { errorMessage = "SĐT đã được dùng!"; return false; }
            if (!string.IsNullOrEmpty(nv.Sdt) && nhanVienDAL.CheckSdtExistsInKhachHang(nv.Sdt)) { errorMessage = "SĐT đã dùng cho khách hàng!"; return false; }
            if (!string.IsNullOrEmpty(nv.Email) && nhanVienDAL.CheckEmailExists(nv.Email, nv.MaNV)) { errorMessage = "Email đã tồn tại!"; return false; }
            if (!string.IsNullOrEmpty(nv.CCCD) && nhanVienDAL.CheckCCCDExists(nv.CCCD, nv.MaNV)) { errorMessage = "CCCD đã tồn tại!"; return false; }

            // Nếu có nhập mật khẩu mới thì kiểm tra độ dài
            if (!string.IsNullOrWhiteSpace(nv.Password) && nv.Password.Length < 6)
            { errorMessage = "Mật khẩu mới phải từ 6 ký tự!"; return false; }

            // Cập nhật thông tin nhân viên
            if (!nhanVienDAL.UpdateNhanVien(nv))
            { errorMessage = "Cập nhật thất bại!"; return false; }

            // ✅ Cập nhật LoaiTaiKhoan dựa trên ChucVu
            string chucVuNormalized = nv.ChucVu?.Trim().ToLower() ?? "";
            string loaiTaiKhoan = "NhanVien"; // Mặc định
            
            if (chucVuNormalized == "quản lý" || chucVuNormalized == "quan ly")
                loaiTaiKhoan = "QuanLy";
            else if (chucVuNormalized == "thu ngân" || chucVuNormalized == "thu ngan")
                loaiTaiKhoan = "ThuNgan";
            else if (chucVuNormalized == "kỹ thuật" || chucVuNormalized == "ky thuat")
                loaiTaiKhoan = "KyThuat";
            else if (chucVuNormalized == "bán hàng" || chucVuNormalized == "ban hang")
                loaiTaiKhoan = "BanHang";
            
            string queryUpdateLoaiTK = "UPDATE TaiKhoan SET LoaiTaiKhoan = @LoaiTaiKhoan WHERE MaNV = @MaNV";
            SqlParameter[] parsLoaiTK = {
                new SqlParameter("@LoaiTaiKhoan", loaiTaiKhoan),
                new SqlParameter("@MaNV", nv.MaNV)
            };
            DataProvider.ExecuteNonQuery(queryUpdateLoaiTK, parsLoaiTK);

            // Nếu có nhập mật khẩu mới → đổi mật khẩu
            if (!string.IsNullOrEmpty(nv.Password))
            {
                string matKhauMaHoa = MaHoaMD5(nv.Password);
                string query = "UPDATE TaiKhoan SET MatKhau = @MatKhau WHERE MaNV = @MaNV";
                SqlParameter[] pars = {
                    new SqlParameter("@MatKhau", matKhauMaHoa),
                    new SqlParameter("@MaNV", nv.MaNV)
                };
                DataProvider.ExecuteNonQuery(query, pars);
            }

            return true;
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
                // ✅ XÓA TÀI KHOẢN TRƯỚC KHI XÓA NHÂN VIÊN (để tránh lỗi Foreign Key)
                TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();
                taiKhoanBLL.DeleteAccountByMaNV(maNV);

                // Xóa nhân viên
                return nhanVienDAL.DeleteNhanVien(maNV);
            }
            catch (Exception ex)
            {
                errorMessage = "Không thể xóa nhân viên! Có thể nhân viên đang có dữ liệu liên quan.";
                return false;
            }
        }

        // ================== HÀM MÃ HÓA MD5 ĐƠN GIẢN ==================
        private string MaHoaMD5(string input)
        {
            string salt = "MyApp2025@"; // salt cố định, đủ dùng cho đồ án
            string text = input + salt;

            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        // ================== VALIDATE GIỮ NGUYÊN ==================
        private bool ValidateNhanVien(NhanVien nv, out string errorMessage, bool isUpdate)
        {
            errorMessage = "";
            if (string.IsNullOrWhiteSpace(nv.MaNV)) { errorMessage = "Mã nhân viên không được trống!"; return false; }
            if (string.IsNullOrWhiteSpace(nv.HoTenNV)) { errorMessage = "Họ tên không được trống!"; return false; }
            if (nv.HoTenNV.Length < 3 || nv.HoTenNV.Length > 100) { errorMessage = "Họ tên từ 3-100 ký tự!"; return false; }

            int age = DateTime.Now.Year - nv.NgaySinh.Year;
            if (nv.NgaySinh > DateTime.Now.AddYears(-age)) age--;
            if (age < 18) { errorMessage = "Phải từ 18 tuổi!"; return false; }
            if (age > 65) { errorMessage = "Không quá 65 tuổi!"; return false; }

            if (!string.IsNullOrEmpty(nv.Sdt) && !Regex.IsMatch(nv.Sdt, @"^0\d{9}$"))
            { errorMessage = "SĐT phải 10 số, bắt đầu bằng 0!"; return false; }

            if (!string.IsNullOrEmpty(nv.Email) && !Regex.IsMatch(nv.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            { errorMessage = "Email không hợp lệ!"; return false; }

            if (!string.IsNullOrEmpty(nv.CCCD) && !Regex.IsMatch(nv.CCCD, @"^\d{12}$"))
            { errorMessage = "CCCD phải đúng 12 số!"; return false; }

            if (nv.LuongCoBan.HasValue && nv.LuongCoBan < 0)
            { errorMessage = "Lương không được âm!"; return false; }

            return true;
        }
    }
}