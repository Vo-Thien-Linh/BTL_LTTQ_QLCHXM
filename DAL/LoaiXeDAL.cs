using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LoaiXeDAL
    {
        public DataTable GetAllLoaiXe()
        {
            string query = "SELECT ID_Loai, MaHang, MaDong, MaMau, NamSX FROM LoaiXe";
            return DataProvider.ExecuteQuery(query);
        }

        // Tìm ID_Loai dựa trên MaHang, MaDong, MaMau, NamSX
        public string FindIDLoai(string maHang, string maDong, string maMau, int namSX)
        {
            string query = @"SELECT ID_Loai FROM LoaiXe 
                           WHERE MaHang = @MaHang AND MaDong = @MaDong 
                           AND MaMau = @MaMau AND NamSX = @NamSX";
            
            SqlParameter[] parameters = {
                new SqlParameter("@MaHang", maHang),
                new SqlParameter("@MaDong", maDong),
                new SqlParameter("@MaMau", maMau),
                new SqlParameter("@NamSX", namSX)
            };
            
            DataTable dt = DataProvider.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["ID_Loai"].ToString();
            }
            return null;
        }

        // Lấy ID_Loai lớn nhất để tạo mã mới
        public string GetMaxIDLoai()
        {
            string query = "SELECT MAX(ID_Loai) FROM LoaiXe";
            DataTable dt = DataProvider.ExecuteQuery(query);
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                return dt.Rows[0][0].ToString();
            }
            return "LX000";
        }

        // Thêm loại xe mới
        public bool InsertLoaiXe(string idLoai, string maHang, string maDong, string maMau, int namSX)
        {
            string query = @"INSERT INTO LoaiXe (ID_Loai, MaHang, MaDong, MaMau, NamSX)
                           VALUES (@ID_Loai, @MaHang, @MaDong, @MaMau, @NamSX)";
            
            SqlParameter[] parameters = {
                new SqlParameter("@ID_Loai", idLoai),
                new SqlParameter("@MaHang", maHang),
                new SqlParameter("@MaDong", maDong),
                new SqlParameter("@MaMau", maMau),
                new SqlParameter("@NamSX", namSX)
            };
            
            int result = DataProvider.ExecuteNonQuery(query, parameters);
            return result > 0;
        }
    }
}
