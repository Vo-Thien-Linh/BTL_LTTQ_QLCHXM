using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class XeMayDTO
    {
        public string ID_Xe { get; set; }
        public string BienSo { get; set; }
        public string ID_Loai { get; set; }
        public string MaNCC { get; set; }
        public DateTime? NgayMua { get; set; }
        public decimal? GiaMua { get; set; }
        public DateTime? NgayDangKy { get; set; }
        public DateTime? HetHanDangKy { get; set; }
        public DateTime? HetHanBaoHiem { get; set; }
        public int? KmDaChay { get; set; }
        public string ThongTinXang { get; set; }
        public string AnhXeXeBan { get; set; }
        public string TrangThai { get; set; }

        // Thông tin JOIN từ các bảng khác
        public string TenHang { get; set; }
        public string TenDong { get; set; }
        public string TenMau { get; set; }
        public int? NamSX { get; set; }
        public int? PhanKhoi { get; set; }
        public string LoaiXe { get; set; }
        public string MaHang { get; set; }     // mới
        public string MaDong { get; set; }     // mới
        public string MaMau { get; set; }      // mới

        // Constructor mặc định
        public XeMayDTO() { }

        // Constructor đầy đủ
        public XeMayDTO(string idXe, string bienSo, string trangThai)
        {
            this.ID_Xe = idXe;
            this.BienSo = bienSo;
            this.TrangThai = trangThai;
        }
    }
}
