using System;
using System.Data;
using DAL;
using DTO;

namespace BLL
{
    public class HopDongThueBLL
    {
        private HopDongThueDAL hopDongThueDAL;

        public HopDongThueBLL()
        {
            hopDongThueDAL = new HopDongThueDAL();
        }

        /// Tạo hợp đồng thuê từ giao dịch thuê
        public bool TaoHopDongThue(int maGDThue, string maTaiKhoan, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                //  Kiểm tra đã có hợp đồng chưa
                if (hopDongThueDAL.IsHopDongExists(maGDThue))
                {
                    errorMessage = "Giao dịch này đã có hợp đồng!";
                    return false;
                }

                //  Lấy thông tin giao dịch thuê
                GiaoDichThueBLL gdBLL = new GiaoDichThueBLL();
                DataTable dt = gdBLL.GetGiaoDichThueById(maGDThue);

                if (dt.Rows.Count == 0)
                {
                    errorMessage = "Không tìm thấy giao dịch thuê!";
                    return false;
                }

                DataRow row = dt.Rows[0];

                //  Kiểm tra trạng thái
                string trangThaiDuyet = row["TrangThaiDuyet"].ToString();
                if (trangThaiDuyet != "Đã duyệt")
                {
                    errorMessage = "Chỉ tạo hợp đồng cho đơn đã được duyệt!";
                    return false;
                }

                //  Tạo hợp đồng
                HopDongThue hd = new HopDongThue
                {
                    MaGDThue = maGDThue,
                    MaKH = row["MaKH"].ToString(),
                    MaTaiKhoan = maTaiKhoan,
                    ID_Xe = row["ID_Xe"].ToString(),
                    NgayLap = DateTime.Now,
                    NgayBatDau = Convert.ToDateTime(row["NgayBatDau"]),
                    NgayKetThuc = Convert.ToDateTime(row["NgayKetThuc"]),
                    GiaThue = Convert.ToDecimal(row["TongGia"]),
                    TienDatCoc = row["SoTienCoc"] != DBNull.Value 
                        ? Convert.ToDecimal(row["SoTienCoc"]) : (decimal?)null,
                    DieuKhoan = TaoDieuKhoanMacDinh(),
                    GhiChu = $"Hợp đồng tự động tạo từ GD #{maGDThue}",
                    TrangThaiHopDong = "Đang hiệu lực"
                };

                bool success = hopDongThueDAL.InsertHopDongThue(hd);

                if (!success)
                {
                    errorMessage = "Lỗi khi lưu hợp đồng vào database!";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi: " + ex.Message;
                return false;
            }
        }

        /// Lấy hợp đồng theo mã giao dịch
        public DataTable GetHopDongByMaGDThue(int maGDThue)
        {
            return hopDongThueDAL.GetHopDongByMaGDThue(maGDThue);
        }

        /// Cập nhật trạng thái hợp đồng
        public bool UpdateTrangThai(int maHDT, string trangThai, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                return hopDongThueDAL.UpdateTrangThaiHopDong(maHDT, trangThai);
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi: " + ex.Message;
                return false;
            }
        }

        /// Tạo điều khoản mặc định
        private string TaoDieuKhoanMacDinh()
        {
            return @"
                1. Khách hàng cam kết sử dụng xe đúng mục đích, không vi phạm pháp luật.
                2. Khách hàng chịu trách nhiệm về mọi hành vi vi phạm giao thông trong thời gian thuê.
                3. Trả xe đúng hạn, nếu trễ sẽ tính phí 150% giá thuê/ngày.
                4. Bồi thường 100% giá trị xe nếu mất cắp hoặc hư hỏng nặng.
                5. Xe trả về phải đủ xăng như khi nhận.
                ";
        }
    }
}