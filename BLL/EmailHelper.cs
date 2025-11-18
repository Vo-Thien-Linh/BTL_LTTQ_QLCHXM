using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EmailHelper
    {
        private const string EMAIL = "congducpham2010@gmail.com";
        private const string APP_PASSWORD = "cyqw jmqj xuyh eimb";  // App Password đúng

        public static bool GuiOTP(string toEmail, string otp)
        {
            try
            {
                var from = new MailAddress(EMAIL, "Cửa hàng xe máy Wings motorbikes");
                var to = new MailAddress(toEmail);

                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(EMAIL, APP_PASSWORD),
                    EnableSsl = true
                };

                // Sửa ở đây: dùng kiểu cũ thay vì "using var"
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = "Mã OTP đặt lại mật khẩu";
                mail.Body = $"<h2>Mã OTP của bạn là:</h2><h1 style='color:blue'>{otp}</h1><p>Hiệu lực 5 phút</p>";
                mail.IsBodyHtml = true;

                smtp.Send(mail);  // Gửi luôn ở đây
                return true;
            }
            catch (Exception ex)
            {
                // Tùy chọn: ghi log lỗi để debug sau
                System.Diagnostics.Debug.WriteLine("Lỗi gửi mail: " + ex.Message);
                return false;
            }
        }
    }
}
