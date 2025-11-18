using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class QuenMatKhauDTO
    {
        public string Email { get; set; }
        public string MatKhauMoi { get; set; }

        // Constructor không tham số (bắt buộc khi dùng với một số hàm cũ)
        public QuenMatKhauDTO()
        {
        }

        // Constructor có tham số (tiện khi tạo nhanh)
        public QuenMatKhauDTO(string email, string matKhauMoi)
        {
            Email = email;
            MatKhauMoi = matKhauMoi;
        }
    }
}
