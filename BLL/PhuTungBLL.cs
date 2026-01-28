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

        public bool UpdateKhoPhuTungSoLuong(string maPT, int soLuongMoi)
        {
            string query = "UPDATE KhoPhuTung SET SoLuongTon = @sl WHERE MaPhuTung = @ma";
            var pars = new[]
            {
        new SqlParameter("@sl", soLuongMoi),
        new SqlParameter("@ma", maPT)
    };
            return DataProvider.ExecuteNonQuery(query, pars) > 0;
        }

        /// <summary>
        /// Lưu lịch sử bán phụ tùng lẻ
        /// </summary>
        public void LuuLichSuBanLe(string maPT, string tenPT, int soLuong, decimal donGia, 
            decimal giamGia, decimal thanhTien, string maTK, string maKM)
        {
            string query = @"
                INSERT INTO LichSuBanPhuTungLe (MaPhuTung, TenPhuTung, SoLuong, DonGia, 
                    SoTienGiam, ThanhTien, NgayBan, MaTaiKhoan, MaKM)
                VALUES (@MaPT, @TenPT, @SoLuong, @DonGia, @GiamGia, @ThanhTien, 
                    @NgayBan, @MaTK, @MaKM)";
            
            SqlParameter[] pars = new[]
            {
                new SqlParameter("@MaPT", maPT),
                new SqlParameter("@TenPT", tenPT),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@DonGia", donGia),
                new SqlParameter("@GiamGia", giamGia),
                new SqlParameter("@ThanhTien", thanhTien),
                new SqlParameter("@NgayBan", DateTime.Now),
                new SqlParameter("@MaTK", (object)maTK ?? DBNull.Value),
                new SqlParameter("@MaKM", (object)maKM ?? DBNull.Value)
            };
            
            DataProvider.ExecuteNonQuery(query, pars);
        }
    }
}
