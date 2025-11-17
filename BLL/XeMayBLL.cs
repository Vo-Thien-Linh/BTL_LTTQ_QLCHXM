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
                // Validate dữ liệu
                if (string.IsNullOrEmpty(xe.ID_Xe))
                {
                    throw new Exception("Mã xe không được để trống!");
                }

                if (string.IsNullOrEmpty(xe.ID_Loai))
                {
                    throw new Exception("Loại xe không được để trống!");
                }

                if (string.IsNullOrEmpty(xe.TrangThai))
                {
                    throw new Exception("Trạng thái không được để trống!");
                }

                // Kiểm tra biển số đã tồn tại
                if (!string.IsNullOrEmpty(xe.BienSo) && xeMayDAL.IsBienSoExists(xe.BienSo))
                {
                    throw new Exception("Biển số đã tồn tại trong hệ thống!");
                }

                return xeMayDAL.InsertXeMay(xe);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm xe: " + ex.Message);
            }
        }

        // Cập nhật thông tin xe
        public bool UpdateXeMay(XeMayDTO xe)
        {
            try
            {
                // Validate dữ liệu
                if (string.IsNullOrEmpty(xe.ID_Xe))
                {
                    throw new Exception("Mã xe không được để trống!");
                }

                if (string.IsNullOrEmpty(xe.ID_Loai))
                {
                    throw new Exception("Loại xe không được để trống!");
                }

                if (string.IsNullOrEmpty(xe.TrangThai))
                {
                    throw new Exception("Trạng thái không được để trống!");
                }

                // Kiểm tra biển số đã tồn tại (ngoại trừ xe hiện tại)
                if (!string.IsNullOrEmpty(xe.BienSo) && xeMayDAL.IsBienSoExists(xe.BienSo, xe.ID_Xe))
                {
                    throw new Exception("Biển số đã tồn tại trong hệ thống!");
                }

                return xeMayDAL.UpdateXeMay(xe);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật xe: " + ex.Message);
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
    }
}
