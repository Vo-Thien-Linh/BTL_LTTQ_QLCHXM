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
    public class XeMayDAL
    {
        public string GetMaxID_Xe()
        {
            string query = "SELECT TOP 1 ID_Xe FROM XeMay ORDER BY ID_Xe DESC";
            var dt = DataProvider.ExecuteQuery(query);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["ID_Xe"].ToString();
            return null;
        }
        // Lấy tất cả xe
        public DataTable GetAllXeMay()
        {
            string query = @"
                SELECT 
                    xm.ID_Xe,
                    xm.BienSo,
                    hx.TenHang,
                    dx.TenDong,
                    ms.TenMau,
                    lx.NamSX,
                    dx.PhanKhoi,
                    dx.LoaiXe,
                    xm.GiaMua,
                    xm.KmDaChay,
                    xm.TrangThai,
                    xm.ID_Loai,
                    xm.MaNCC,
                    xm.NgayMua,
                    xm.NgayDangKy,
                    xm.HetHanDangKy,
                    xm.HetHanBaoHiem,
                    xm.ThongTinXang,
                    xm.AnhXeXeBan
                FROM XeMay xm
                INNER JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                ORDER BY xm.ID_Xe";

            return DataProvider.ExecuteQuery(query);
        }

        // Tìm kiếm xe
        public DataTable SearchXeMay(string keyword, string trangThai)
        {
            string query = @"
                SELECT 
                    xm.ID_Xe,
                    xm.BienSo,
                    hx.TenHang,
                    dx.TenDong,
                    ms.TenMau,
                    lx.NamSX,
                    dx.PhanKhoi,
                    dx.LoaiXe,
                    xm.GiaMua,
                    xm.KmDaChay,
                    xm.TrangThai,
                    xm.ID_Loai,
                    xm.MaNCC,
                    xm.NgayMua,
                    xm.NgayDangKy,
                    xm.HetHanDangKy,
                    xm.HetHanBaoHiem,
                    xm.ThongTinXang,
                    xm.AnhXeXeBan
                FROM XeMay xm
                INNER JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
                INNER JOIN HangXe hx ON lx.MaHang = hx.MaHang
                INNER JOIN DongXe dx ON lx.MaDong = dx.MaDong
                INNER JOIN MauSac ms ON lx.MaMau = ms.MaMau
                WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(keyword))
            {
                query += @" AND (xm.BienSo LIKE @Keyword 
                            OR hx.TenHang LIKE @Keyword 
                            OR dx.TenDong LIKE @Keyword 
                            OR xm.ID_Xe LIKE @Keyword)";
                parameters.Add(new SqlParameter("@Keyword", "%" + keyword + "%"));
            }

            if (!string.IsNullOrEmpty(trangThai) && trangThai != "Tất cả")
            {
                query += " AND xm.TrangThai = @TrangThai";
                parameters.Add(new SqlParameter("@TrangThai", trangThai));
            }

            query += " ORDER BY xm.ID_Xe";

            return DataProvider.ExecuteQuery(query, parameters.ToArray());
        }

        // Lấy thông tin xe theo ID
        public XeMayDTO GetXeMayById(string idXe)
        {
            string query = @"
        SELECT xm.*, lx.MaHang, lx.MaDong, lx.MaMau, lx.NamSX
        FROM XeMay xm
        INNER JOIN LoaiXe lx ON xm.ID_Loai = lx.ID_Loai
        WHERE xm.ID_Xe = @ID_Xe";
            SqlParameter[] prm = { new SqlParameter("@ID_Xe", idXe) };
            DataTable dt = DataProvider.ExecuteQuery(query, prm);
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                return new XeMayDTO
                {
                    ID_Xe = r["ID_Xe"].ToString(),
                    BienSo = r["BienSo"].ToString(),
                    ID_Loai = r["ID_Loai"].ToString(),
                    MaNCC = r["MaNCC"].ToString(),
                    // ... các trường khác ...
                    MaHang = r["MaHang"].ToString(),
                    MaDong = r["MaDong"].ToString(),
                    MaMau = r["MaMau"].ToString(),
                    NamSX = r["NamSX"] != DBNull.Value ? Convert.ToInt32(r["NamSX"]) : (int?)null
                };
            }
            return null;
        }


        // Thêm xe mới
        public bool InsertXeMay(XeMayDTO xe)
        {
            string query = @"
                INSERT INTO XeMay (ID_Xe, BienSo, ID_Loai, MaNCC, NgayMua, GiaMua, 
                                   NgayDangKy, HetHanDangKy, HetHanBaoHiem, KmDaChay, 
                                   ThongTinXang, AnhXeXeBan, TrangThai)
                VALUES (@ID_Xe, @BienSo, @ID_Loai, @MaNCC, @NgayMua, @GiaMua, 
                        @NgayDangKy, @HetHanDangKy, @HetHanBaoHiem, @KmDaChay, 
                        @ThongTinXang, @AnhXeXeBan, @TrangThai)";

            SqlParameter[] parameters = {
                new SqlParameter("@ID_Xe", xe.ID_Xe),
                new SqlParameter("@BienSo", (object)xe.BienSo ?? DBNull.Value),
                new SqlParameter("@ID_Loai", xe.ID_Loai),
                new SqlParameter("@MaNCC", (object)xe.MaNCC ?? DBNull.Value),
                new SqlParameter("@NgayMua", (object)xe.NgayMua ?? DBNull.Value),
                new SqlParameter("@GiaMua", (object)xe.GiaMua ?? DBNull.Value),
                new SqlParameter("@NgayDangKy", (object)xe.NgayDangKy ?? DBNull.Value),
                new SqlParameter("@HetHanDangKy", (object)xe.HetHanDangKy ?? DBNull.Value),
                new SqlParameter("@HetHanBaoHiem", (object)xe.HetHanBaoHiem ?? DBNull.Value),
                new SqlParameter("@KmDaChay", (object)xe.KmDaChay ?? DBNull.Value),
                new SqlParameter("@ThongTinXang", (object)xe.ThongTinXang ?? DBNull.Value),
                new SqlParameter("@AnhXeXeBan", (object)xe.AnhXeXeBan ?? DBNull.Value),
                new SqlParameter("@TrangThai", xe.TrangThai)
            };

            int result = DataProvider.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        // Cập nhật thông tin xe
        public bool UpdateXeMay(XeMayDTO xe)
        {
            string query = @"
                UPDATE XeMay 
                SET BienSo = @BienSo,
                    ID_Loai = @ID_Loai,
                    MaNCC = @MaNCC,
                    NgayMua = @NgayMua,
                    GiaMua = @GiaMua,
                    NgayDangKy = @NgayDangKy,
                    HetHanDangKy = @HetHanDangKy,
                    HetHanBaoHiem = @HetHanBaoHiem,
                    KmDaChay = @KmDaChay,
                    ThongTinXang = @ThongTinXang,
                    AnhXeXeBan = @AnhXeXeBan,
                    TrangThai = @TrangThai
                WHERE ID_Xe = @ID_Xe";

            SqlParameter[] parameters = {
                new SqlParameter("@ID_Xe", xe.ID_Xe),
                new SqlParameter("@BienSo", (object)xe.BienSo ?? DBNull.Value),
                new SqlParameter("@ID_Loai", xe.ID_Loai),
                new SqlParameter("@MaNCC", (object)xe.MaNCC ?? DBNull.Value),
                new SqlParameter("@NgayMua", (object)xe.NgayMua ?? DBNull.Value),
                new SqlParameter("@GiaMua", (object)xe.GiaMua ?? DBNull.Value),
                new SqlParameter("@NgayDangKy", (object)xe.NgayDangKy ?? DBNull.Value),
                new SqlParameter("@HetHanDangKy", (object)xe.HetHanDangKy ?? DBNull.Value),
                new SqlParameter("@HetHanBaoHiem", (object)xe.HetHanBaoHiem ?? DBNull.Value),
                new SqlParameter("@KmDaChay", (object)xe.KmDaChay ?? DBNull.Value),
                new SqlParameter("@ThongTinXang", (object)xe.ThongTinXang ?? DBNull.Value),
                new SqlParameter("@AnhXeXeBan", (object)xe.AnhXeXeBan ?? DBNull.Value),
                new SqlParameter("@TrangThai", xe.TrangThai)
            };

            int result = DataProvider.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        // Xóa xe
        public bool DeleteXeMay(string idXe)
        {
            string query = "DELETE FROM XeMay WHERE ID_Xe = @ID_Xe";
            SqlParameter[] parameters = {
                new SqlParameter("@ID_Xe", idXe)
            };

            int result = DataProvider.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        // Cập nhật trạng thái xe
        public bool UpdateTrangThai(string idXe, string trangThai)
        {
            string query = "UPDATE XeMay SET TrangThai = @TrangThai WHERE ID_Xe = @ID_Xe";
            SqlParameter[] parameters = {
                new SqlParameter("@ID_Xe", idXe),
                new SqlParameter("@TrangThai", trangThai)
            };

            int result = DataProvider.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        // Kiểm tra biển số đã tồn tại
        public bool IsBienSoExists(string bienSo, string idXe = null)
        {
            string query = "SELECT COUNT(*) FROM XeMay WHERE BienSo = @BienSo";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@BienSo", bienSo)
            };

            if (!string.IsNullOrEmpty(idXe))
            {
                query += " AND ID_Xe != @ID_Xe";
                parameters.Add(new SqlParameter("@ID_Xe", idXe));
            }

            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, parameters.ToArray()));
            return count > 0;
        }
    }
}
