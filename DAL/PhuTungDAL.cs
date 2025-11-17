using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PhuTungDAL
    {
        public string GetMaxMaPhuTung()
        {
            string query = "SELECT TOP 1 MaPhuTung FROM PhuTung ORDER BY MaPhuTung DESC";
            var dt = DataProvider.ExecuteQuery(query);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["MaPhuTung"].ToString();
            return null;
        }

        public int InsertPhuTung(string maPT, string tenPT, string maHang, string maDong,
            decimal giaMua, decimal giaBan, string donVi, string ghiChu)
        {
            string query = "INSERT INTO PhuTung (MaPhuTung, TenPhuTung, MaHangXe, MaDongXe, GiaMua, GiaBan, DonViTinh, GhiChu) " +
                           "VALUES (@ma, @ten, @hang, @dong, @giamua, @giaban, @donvi, @ghichu)";
            SqlParameter[] pars = {
                new SqlParameter("@ma", maPT),
                new SqlParameter("@ten", tenPT),
                new SqlParameter("@hang", maHang),
                new SqlParameter("@dong", maDong),
                new SqlParameter("@giamua", giaMua),
                new SqlParameter("@giaban", giaBan),
                new SqlParameter("@donvi", donVi),
                new SqlParameter("@ghichu", ghiChu)
            };
            return DataProvider.ExecuteNonQuery(query, pars);
        }

        public int InsertKhoPhuTung(string maPT, int soLuong, string ghiChu)
        {
            string query = "INSERT INTO KhoPhuTung (MaPhuTung, SoLuongTon, GhiChu) VALUES (@ma, @sl, @ghichu)";
            SqlParameter[] pars = {
                new SqlParameter("@ma", maPT),
                new SqlParameter("@sl", soLuong),
                new SqlParameter("@ghichu", ghiChu)
            };
            return DataProvider.ExecuteNonQuery(query, pars);
        }

        // Lấy 1 phụ tùng theo mã, trả về DTO
        public PhuTungDTO GetPhuTungById(string maPT)
        {
            if (string.IsNullOrWhiteSpace(maPT))
                throw new ArgumentException("Mã phụ tùng không hợp lệ.");
            string query = @"SELECT pt.MaPhuTung, pt.TenPhuTung, pt.MaHangXe, pt.MaDongXe, pt.GiaMua, pt.GiaBan, pt.DonViTinh, pt.GhiChu, kpt.SoLuongTon
                                FROM PhuTung pt LEFT JOIN KhoPhuTung kpt ON pt.MaPhuTung = kpt.MaPhuTung
                                WHERE pt.MaPhuTung = @ma";
            SqlParameter[] pars = { new SqlParameter("@ma", maPT) };
            DataTable dt = DataProvider.ExecuteQuery(query, pars);
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                return new PhuTungDTO
                {
                    MaPhuTung = r["MaPhuTung"].ToString(),
                    TenPhuTung = r["TenPhuTung"].ToString(),
                    MaHangXe = r["MaHangXe"].ToString(),
                    MaDongXe = r["MaDongXe"].ToString(),
                    GiaMua = r["GiaMua"] != DBNull.Value ? Convert.ToDecimal(r["GiaMua"]) : 0,
                    GiaBan = r["GiaBan"] != DBNull.Value ? Convert.ToDecimal(r["GiaBan"]) : 0,
                    DonViTinh = r["DonViTinh"].ToString(),
                    GhiChu = r["GhiChu"].ToString(),
                    SoLuongTon = r["SoLuongTon"] != DBNull.Value ? Convert.ToInt32(r["SoLuongTon"]) : 0
                };
            }
            return null;
        }

        // Cập nhật thông tin phụ tùng (trừ số lượng kho)
        public int UpdatePhuTung(string maPT, string tenPT, string maHang, string maDong,
            decimal giaMua, decimal giaBan, string donVi, string ghiChu)
        {
            if (string.IsNullOrWhiteSpace(maPT))
                throw new ArgumentException("Mã phụ tùng không hợp lệ.");
            string query = @"UPDATE PhuTung SET TenPhuTung = @ten, MaHangXe = @hang, MaDongXe = @dong, GiaMua = @giamua, GiaBan = @giaban, DonViTinh = @donvi, GhiChu = @ghichu
                              WHERE MaPhuTung = @ma";
            SqlParameter[] pars = { new SqlParameter("@ten", tenPT),
                                    new SqlParameter("@hang", maHang),
                                    new SqlParameter("@dong", maDong),
                                    new SqlParameter("@giamua", giaMua),
                                    new SqlParameter("@giaban", giaBan),
                                    new SqlParameter("@donvi", donVi),
                                    new SqlParameter("@ghichu", ghiChu),
                                    new SqlParameter("@ma", maPT)};
            return DataProvider.ExecuteNonQuery(query, pars);
        }

        // Cập nhật kho phụ tùng (chỉ số lượng và ghi chú)
        public int UpdateKhoPhuTung(string maPT, int soLuong, string ghiChu)
        {
            if (string.IsNullOrWhiteSpace(maPT))
                throw new ArgumentException("Mã phụ tùng không hợp lệ.");
            string query = "UPDATE KhoPhuTung SET SoLuongTon = @sl, GhiChu = @ghichu WHERE MaPhuTung = @ma";
            SqlParameter[] pars = { new SqlParameter("@sl", soLuong),
                                    new SqlParameter("@ghichu", ghiChu),
                                    new SqlParameter("@ma", maPT)};
            return DataProvider.ExecuteNonQuery(query, pars);
        }


    }
}
