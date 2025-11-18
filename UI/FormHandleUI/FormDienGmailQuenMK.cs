using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.FormHandleUI
{
    public partial class FormDienGmailQuenMK : Form
    {
        public FormDienGmailQuenMK()
        {
            InitializeComponent();
        }

        private void FormDienGmailQuenMK_Load(object sender, EventArgs e)
        {

        }

        private void btnGuiMa_Click(object sender, EventArgs e)
        {
            string email = txtGmail.Text.Trim();

            var bll = new QuenMatKhauBLL();
            if (!bll.KiemTraEmail(email))
            {
                MessageBox.Show("Email không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string otp = new Random().Next(100000, 999999).ToString();

            if (EmailHelper.GuiOTP(email, otp))
            {
                OTPManager.Email = email;
                OTPManager.MaOTP = otp;
                OTPManager.ThoiGianGui = DateTime.Now;

                MessageBox.Show("Đã gửi mã OTP đến email!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new FormDienMaQuenMK().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Gửi email thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
