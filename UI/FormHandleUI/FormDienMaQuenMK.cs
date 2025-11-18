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
    public partial class FormDienMaQuenMK : Form
    {
        public FormDienMaQuenMK()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormDienMaQuenMK_Load(object sender, EventArgs e)
        {

        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (OTPManager.KiemTra(txtDienMa.Text.Trim()))
            {
                MessageBox.Show("Xác minh thành công!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new FormTaoMatKhauMoi().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Mã OTP sai hoặc đã hết hạn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
