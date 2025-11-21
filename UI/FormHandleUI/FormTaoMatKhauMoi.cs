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
using UI.FormUI;

namespace UI.FormHandleUI
{
    public partial class FormTaoMatKhauMoi : Form
    {
        public FormTaoMatKhauMoi()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormTaoMatKhauMoi_Load(object sender, EventArgs e)
        {
               
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string mkMoi = txtMatKhauMoi.Text;
            string xacNhan = txtXacNhanMatKhau.Text;

            var bll = new QuenMatKhauBLL();
            if (bll.DoiMatKhau(OTPManager.Email, mkMoi, xacNhan))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công!\nGiờ bạn có thể đăng nhập.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OTPManager.Clear();
                new LoginForm().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra hoặc mật khẩu không hợp lệ!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
