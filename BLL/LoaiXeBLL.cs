using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoaiXeBLL
    {
        private LoaiXeDAL dal = new LoaiXeDAL();
        
        public DataTable GetAllLoaiXe()
        {
            return dal.GetAllLoaiXe();
        }

        // Tìm hoặc tạo ID_Loai từ MaHang, MaDong, MaMau, NamSX
        public string GetOrCreateIDLoai(string maHang, string maDong, string maMau, int namSX)
        {
            try
            {
                // Tìm xem đã tồn tại chưa
                string existingID = dal.FindIDLoai(maHang, maDong, maMau, namSX);
                if (!string.IsNullOrEmpty(existingID))
                {
                    return existingID;
                }

                // Nếu chưa tồn tại, tạo mới
                string maxID = dal.GetMaxIDLoai();
                string newID = GenerateNewIDLoai(maxID);
                
                bool success = dal.InsertLoaiXe(newID, maHang, maDong, maMau, namSX);
                if (success)
                {
                    return newID;
                }
                
                throw new Exception("Không thể tạo loại xe mới!");
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xử lý loại xe: " + ex.Message);
            }
        }

        private string GenerateNewIDLoai(string maxID)
        {
            if (string.IsNullOrEmpty(maxID) || maxID == "LX000")
            {
                return "LX001";
            }
            
            // Tách phần số (ví dụ: "LX001" -> 1)
            string numberPart = maxID.Substring(2);
            int number = int.Parse(numberPart);
            number++;
            
            // Tạo mã mới (ví dụ: "LX002")
            return "LX" + number.ToString("D3");
        }
    }
}
