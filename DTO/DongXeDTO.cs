using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DongXeDTO
    {
        public string MaDong { get; set; }
        public string TenDong { get; set; }
        public string MaHang { get; set; }
        public int? PhanKhoi { get; set; }
        public string LoaiXe { get; set; }
        public string MoTa { get; set; }

        public DongXeDTO() { }
    }
}
