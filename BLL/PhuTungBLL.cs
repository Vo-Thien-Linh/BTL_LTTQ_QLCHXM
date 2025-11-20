using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PhuTungBLL
    {
        private PhuTungDAL dal = new PhuTungDAL();
        public string GetMaxMaPhuTung()
        {
            return dal.GetMaxMaPhuTung();
        }

        public bool UpdatePhuTungKho(string maPT, string tenPT, string maHang, string maDong, decimal giaMua, decimal giaBan, string donVi, string ghiChu, int soLuong)
        {
            int r1 = dal.UpdatePhuTung(maPT, tenPT, maHang, maDong, giaMua, giaBan, donVi, ghiChu);
            int r2 = dal.UpdateKhoPhuTung(maPT, soLuong, ghiChu);
            return (r1 > 0 && r2 > 0);
        }

        public PhuTungDTO GetPhuTungById(string maPT)
        {
            return dal.GetPhuTungById(maPT);
        }
        public DataTable GetAllPhuTungWithTonKho()
        {
            string query = @"
            SELECT pt.MaPhuTung, pt.TenPhuTung, hx.TenHang, dx.TenDong,
                pt.GiaMua, pt.GiaBan, pt.DonViTinh, kpt.SoLuongTon, pt.GhiChu
            FROM PhuTung pt
            LEFT JOIN HangXe hx ON pt.MaHangXe = hx.MaHang
            LEFT JOIN DongXe dx ON pt.MaDongXe = dx.MaDong
            LEFT JOIN KhoPhuTung kpt ON pt.MaPhuTung = kpt.MaPhuTung
            ORDER BY pt.MaPhuTung";
            return DataProvider.ExecuteQuery(query);
        }
        public DataTable SearchPhuTung(string keyword, string searchType)
        {
            string query = @"SELECT pt.MaPhuTung, pt.TenPhuTung, hx.TenHang, dx.TenDong, pt.GiaMua, pt.GiaBan, pt.DonViTinh, kpt.SoLuongTon, pt.GhiChu
                   FROM PhuTung pt
                   LEFT JOIN HangXe hx ON pt.MaHangXe = hx.MaHang
                   LEFT JOIN DongXe dx ON pt.MaDongXe = dx.MaDong
                   LEFT JOIN KhoPhuTung kpt ON pt.MaPhuTung = kpt.MaPhuTung
                   WHERE 1=1";
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(keyword) && searchType != "Tất cả")
            {
                switch (searchType)
                {
                    case "Mã phụ tùng":
                        query += " AND pt.MaPhuTung LIKE @kw";
                        break;
                    case "Tên phụ tùng":
                        query += " AND pt.TenPhuTung LIKE @kw";
                        break;
                    case "Hãng xe":
                        query += " AND hx.TenHang LIKE @kw";
                        break;
                    case "Dòng xe":
                        query += " AND dx.TenDong LIKE @kw";
                        break;
                    default:
                        query += " AND (pt.MaPhuTung LIKE @kw OR pt.TenPhuTung LIKE @kw OR hx.TenHang LIKE @kw OR dx.TenDong LIKE @kw)";
                        break;
                }
                parameters.Add(new SqlParameter("@kw", "%" + keyword + "%"));
            }
            query += " ORDER BY pt.MaPhuTung";
            return DataProvider.ExecuteQuery(query, parameters.ToArray());
        }
        public bool DeletePhuTung(string maPhuTung)
        {
            string queryXoaKho = "DELETE FROM KhoPhuTung WHERE MaPhuTung = @ma";
            SqlParameter[] param1 = { new SqlParameter("@ma", maPhuTung) };
            DataProvider.ExecuteNonQuery(queryXoaKho, param1);

            string queryXoaPT = "DELETE FROM PhuTung WHERE MaPhuTung = @ma";
            SqlParameter[] param2 = { new SqlParameter("@ma", maPhuTung) };
            int rows = DataProvider.ExecuteNonQuery(queryXoaPT, param2);
            return rows > 0;
        }

        public bool InsertPhuTungKho(string maPT, string tenPT, string maHang, string maDong,
            decimal giaMua, decimal giaBan, string donVi, string ghiChu, int soLuong)
        {
            // Nên dùng transaction khi thực tế, ở đây đơn giản hóa
            int r1 = dal.InsertPhuTung(maPT, tenPT, maHang, maDong, giaMua, giaBan, donVi, ghiChu);
            if (r1 <= 0) return false;
            int r2 = dal.InsertKhoPhuTung(maPT, soLuong, ghiChu);
            return r2 > 0;
        }

        /// <summary>
        /// Kiểm tra có thể xóa phụ tùng không
        /// </summary>
        public bool CanDeletePhuTung(string maPhuTung, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(maPhuTung))
                {
                    errorMessage = "Mã phụ tùng không hợp lệ!";
                    return false;
                }

                // 1. Kiểm tra phụ tùng đang được sử dụng trong bảo trì
                if (dal.IsPhuTungInBaoTri(maPhuTung))
                {
                    errorMessage = "Phụ tùng đang được sử dụng trong bảo trì!\n" +
                                  "Không thể xóa phụ tùng đang có trong danh sách bảo trì.";
                    return false;
                }

                // 2. Cảnh báo nếu có tồn kho
                var phuTung = dal.GetPhuTungById(maPhuTung);
                if (phuTung != null)
                {
                    // Lấy số lượng tồn kho
                    string queryTonKho = "SELECT SoLuongTon FROM KhoPhuTung WHERE MaPhuTung = @MaPhuTung";
                    SqlParameter[] parameters = { new SqlParameter("@MaPhuTung", maPhuTung) };
                    object result = DataProvider.ExecuteScalar(queryTonKho, parameters);
                    
                    if (result != null && Convert.ToInt32(result) > 0)
                    {
                        errorMessage = $"⚠ Phụ tùng còn {result} sản phẩm trong kho!\n" +
                                      "Bạn có chắc chắn muốn xóa?";
                        // Vẫn cho phép xóa nhưng cảnh báo
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi kiểm tra ràng buộc: {ex.Message}";
                return false;
            }
        }

    }
}
