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

        /// <summary>
        /// Tạo hợp đồng thuê từ giao dịch thuê đã được duyệt
        /// </summary>
        public bool TaoHopDongThue(int maGDThue, string maTaiKhoan, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                //  Validate tham số
                if (maGDThue <= 0)
                {
                    errorMessage = "Mã giao dịch không hợp lệ!";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(maTaiKhoan))
                {
                    errorMessage = "Mã tài khoản không được để trống!";
                    return false;
                }

                // Kiểm tra đã có hợp đồng chưa
                if (hopDongThueDAL.IsHopDongExists(maGDThue))
                {
                    errorMessage = "Giao dịch này đã có hợp đồng!";
                    System.Diagnostics.Debug.WriteLine($"[INFO] GD #{maGDThue} đã có hợp đồng");
                    return false;
                }

                // Lấy thông tin giao dịch thuê
                GiaoDichThueBLL gdBLL = new GiaoDichThueBLL();
                DataTable dt = gdBLL.GetGiaoDichThueById(maGDThue);

                if (dt.Rows.Count == 0)
                {
                    errorMessage = "Không tìm thấy giao dịch thuê!";
                    return false;
                }

                DataRow row = dt.Rows[0];

                // Kiểm tra trạng thái
                string trangThaiDuyet = row["TrangThaiDuyet"].ToString();
                if (trangThaiDuyet != "Đã duyệt")
                {
                    errorMessage = $"Chỉ tạo hợp đồng cho đơn đã được duyệt! (Trạng thái hiện tại: {trangThaiDuyet})";
                    return false;
                }

                string dieuKhoan = TaoDieuKhoanMacDinh();
                
                //  Kiểm tra độ dài trước khi insert
                int maxLength = 500; // Thay bằng độ dài thực tế của cột DieuKhoan trong DB
                if (dieuKhoan.Length > maxLength)
                {
                    System.Diagnostics.Debug.WriteLine($"[CẢNH BÁO] Điều khoản quá dài ({dieuKhoan.Length} ký tự), đã cắt xuống {maxLength}");
                    dieuKhoan = dieuKhoan.Substring(0, maxLength);
                }

                // Tạo hợp đồng
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
                    DieuKhoan = dieuKhoan, //  Sử dụng điều khoản đã kiểm tra
                    GhiChu = $"Hợp đồng tự động tạo từ GD #{maGDThue} vào lúc {DateTime.Now:dd/MM/yyyy HH:mm}",
                    TrangThaiHopDong = "Đang hiệu lực",
                    FileHopDong = null  //  Có thể sinh file PDF sau
                };

                // Log thông tin trước khi insert
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Tạo hợp đồng:");
                System.Diagnostics.Debug.WriteLine($"  - MaGDThue: {hd.MaGDThue}");
                System.Diagnostics.Debug.WriteLine($"  - MaKH: {hd.MaKH}");
                System.Diagnostics.Debug.WriteLine($"  - MaTaiKhoan: {hd.MaTaiKhoan}");
                System.Diagnostics.Debug.WriteLine($"  - ID_Xe: {hd.ID_Xe}");
                System.Diagnostics.Debug.WriteLine($"  - GiaThue: {hd.GiaThue:N0}");

                bool success = hopDongThueDAL.InsertHopDongThue(hd);

                if (!success)
                {
                    errorMessage = "Lỗi khi lưu hợp đồng vào database!";
                    return false;
                }

                System.Diagnostics.Debug.WriteLine($"[THÀNH CÔNG] Đã tạo hợp đồng cho GD #{maGDThue}");
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi tạo hợp đồng: " + ex.Message;
                System.Diagnostics.Debug.WriteLine($"[LỖI] TaoHopDongThue: {ex.Message}\n{ex.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// Tạo điều khoản mặc định cho hợp đồng thuê (RÚT NGẮN)
        /// </summary>
        private string TaoDieuKhoanMacDinh()
        {
            return @"ĐIỀU KHOẢN HỢP ĐỒNG THUÊ XE

            1. BÊN THUÊ cam kết sử dụng xe đúng mục đích, không vi phạm pháp luật.
            2. BÊN THUÊ chịu trách nhiệm về mọi hành vi vi phạm giao thông.
            3. Trả xe đúng hạn, nếu trễ tính phí 150%/ngày.
            4. Bồi thường 100% giá trị xe nếu mất cắp/hư hỏng nặng.
            5. Xe trả về phải đủ xăng như lúc nhận.
            6. Hai bên cam kết thực hiện đúng hợp đồng.
            7. Mọi tranh chấp giải quyết theo pháp luật Việt Nam.";
        }
    }
}