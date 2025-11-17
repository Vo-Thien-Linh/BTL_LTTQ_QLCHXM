using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhuTungDTO
    {
        public string MaPhuTung { get; set; }
        public string TenPhuTung { get; set; }
        public string MaHangXe { get; set; }
        public string MaDongXe { get; set; }
        public decimal GiaMua { get; set; }
        public decimal GiaBan { get; set; }
        public string DonViTinh { get; set; }
        public string GhiChu { get; set; }
        public int SoLuongTon { get; set; }
    }
}
