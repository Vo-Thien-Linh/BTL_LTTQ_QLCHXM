using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.FormUI
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            btnDangKy.BackColor = Color.MediumSlateBlue;
            btnDangKy.ForeColor = Color.White;
            btnDangKy.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnDangKy.FlatStyle = FlatStyle.Flat;
            btnDangKy.FlatAppearance.BorderSize = 0;
            btnDangKy.MouseEnter += (s, e2) => btnDangKy.BackColor = Color.RoyalBlue;
            btnDangKy.MouseLeave += (s, e2) => btnDangKy.BackColor = Color.MediumSlateBlue;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {

        }

        private void lnkDangNhapNgay_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }
    }
}
