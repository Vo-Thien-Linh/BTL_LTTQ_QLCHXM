using System;
using System.Collections.Generic;
using System.Data;
using DAL;
using DTO;

namespace BLL
{
    public class BaoTriBLL
    {
        private BaoTriDAL baoTriDAL = new BaoTriDAL();

        // Lấy danh sách bảo trì
        public DataTable LayDanhSachBaoTri()
        {
            try
            {
                return baoTriDAL.LayDanhSachBaoTri();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách bảo trì: " + ex.Message);
            }
        }

        // Lấy thông tin bảo trì theo ID
        public BaoTriDTO LayBaoTriTheoID(int idBaoTri)
        {
            try
            {
                if (idBaoTri <= 0)
                {
                    throw new Exception("ID bảo trì không hợp lệ!");
                }
                return baoTriDAL.LayBaoTriTheoID(idBaoTri);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin bảo trì: " + ex.Message);
            }
        }

        // Lấy chi tiết bảo trì
        public List<ChiTietBaoTriDTO> LayChiTietBaoTri(int idBaoTri)
        {
            try
            {
                if (idBaoTri <= 0)
                {
                    throw new Exception("ID bảo trì không hợp lệ!");
                }
                return baoTriDAL.LayChiTietBaoTri(idBaoTri);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy chi tiết bảo trì: " + ex.Message);
            }
        }

        // Thêm bảo trì mới
        public bool ThemBaoTri(BaoTriDTO baoTri, List<ChiTietBaoTriDTO> chiTietList)
        {
            try
            {
                // Validate dữ liệu
                if (string.IsNullOrEmpty(baoTri.ID_Xe))
                {
                    throw new Exception("Vui lòng chọn xe cần bảo trì!");
                }

                if (chiTietList == null || chiTietList.Count == 0)
                {
                    throw new Exception("Vui lòng thêm ít nhất một phụ tùng!");
                }

                // Kiểm tra số lượng phụ tùng
                foreach (var chiTiet in chiTietList)
                {
                    if (chiTiet.SoLuong <= 0)
                    {
                        throw new Exception($"Số lượng phụ tùng {chiTiet.TenPhuTung} không hợp lệ!");
                    }
                }

                return baoTriDAL.ThemBaoTri(baoTri, chiTietList);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm bảo trì: " + ex.Message);
            }
        }

        // Cập nhật bảo trì
        public bool CapNhatBaoTri(BaoTriDTO baoTri)
        {
            try
            {
                // Validate dữ liệu
                if (baoTri.ID_BaoTri <= 0)
                {
                    throw new Exception("ID bảo trì không hợp lệ!");
                }

                if (string.IsNullOrEmpty(baoTri.ID_Xe))
                {
                    throw new Exception("Vui lòng chọn xe cần bảo trì!");
                }

                return baoTriDAL.CapNhatBaoTri(baoTri);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật bảo trì: " + ex.Message);
            }
        }

        // Xóa bảo trì
        public bool XoaBaoTri(int idBaoTri)
        {
            try
            {
                if (idBaoTri <= 0)
                {
                    throw new Exception("ID bảo trì không hợp lệ!");
                }

                return baoTriDAL.XoaBaoTri(idBaoTri);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa bảo trì: " + ex.Message);
            }
        }

        // Lấy danh sách xe
        public DataTable LayDanhSachXe()
        {
            try
            {
                return baoTriDAL.LayDanhSachXe();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách xe: " + ex.Message);
            }
        }

        // Lấy danh sách phụ tùng
        public DataTable LayDanhSachPhuTung()
        {
            try
            {
                return baoTriDAL.LayDanhSachPhuTung();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách phụ tùng: " + ex.Message);
            }
        }

        // Lấy danh sách nhân viên kỹ thuật
        public DataTable LayDanhSachNhanVienKyThuat()
        {
            try
            {
                return baoTriDAL.LayDanhSachNhanVienKyThuat();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách nhân viên: " + ex.Message);
            }
        }

        // Tìm kiếm bảo trì
        public DataTable TimKiemBaoTri(string tuKhoa)
        {
            try
            {
                if (string.IsNullOrEmpty(tuKhoa))
                {
                    return LayDanhSachBaoTri();
                }
                return baoTriDAL.TimKiemBaoTri(tuKhoa);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm bảo trì: " + ex.Message);
            }
        }

        // Tính tổng chi phí bảo trì
        public decimal TinhTongChiPhi(List<ChiTietBaoTriDTO> chiTietList)
        {
            decimal tongChiPhi = 0;
            foreach (var chiTiet in chiTietList)
            {
                tongChiPhi += chiTiet.SoLuong * chiTiet.GiaSuDung;
            }
            return tongChiPhi;
        }

        // Kiểm tra tồn kho phụ tùng
        public bool KiemTraTonKho(string maPhuTung, int soLuongCanDung)
        {
            try
            {
                DataTable dt = LayDanhSachPhuTung();
                foreach (DataRow row in dt.Rows)
                {
                    if (row["MaPhuTung"].ToString() == maPhuTung)
                    {
                        int tonKho = Convert.ToInt32(row["SoLuongTon"]);
                        return tonKho >= soLuongCanDung;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi kiểm tra tồn kho: " + ex.Message);
            }
        }

        // Validate dữ liệu chi tiết bảo trì
        public string ValidateChiTietBaoTri(ChiTietBaoTriDTO chiTiet)
        {
            if (string.IsNullOrEmpty(chiTiet.MaPhuTung))
            {
                return "Vui lòng chọn phụ tùng!";
            }

            if (chiTiet.SoLuong <= 0)
            {
                return "Số lượng phải lớn hơn 0!";
            }

            if (chiTiet.GiaSuDung <= 0)
            {
                return "Giá sử dụng phải lớn hơn 0!";
            }

            if (!KiemTraTonKho(chiTiet.MaPhuTung, chiTiet.SoLuong))
            {
                return "Số lượng phụ tùng trong kho không đủ!";
            }

            return null; // Không có lỗi
        }

        /// <summary>
        /// Kiểm tra có thể xóa bảo trì không
        /// </summary>
        public bool CanDeleteBaoTri(int idBaoTri, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                if (idBaoTri <= 0)
                {
                    errorMessage = "ID bảo trì không hợp lệ!";
                    return false;
                }

                // Kiểm tra trạng thái bảo trì (nếu có field TrangThai)
                var baoTri = LayBaoTriTheoID(idBaoTri);
                if (baoTri == null)
                {
                    errorMessage = "Không tìm thấy thông tin bảo trì!";
                    return false;
                }

                // Có thể thêm kiểm tra trạng thái nếu cần
                // Ví dụ: Chỉ cho xóa bảo trì chưa hoàn thành

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi kiểm tra ràng buộc: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Validate xe trước khi bảo trì
        /// </summary>
        public bool ValidateXeTruocBaoTri(string idXe, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(idXe))
                {
                    errorMessage = "Vui lòng chọn xe!";
                    return false;
                }

                // Kiểm tra xe có tồn tại và trạng thái
                // Cần có method trong XeMayDAL để kiểm tra
                
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi validate: {ex.Message}";
                return false;
            }
        }
    }
}