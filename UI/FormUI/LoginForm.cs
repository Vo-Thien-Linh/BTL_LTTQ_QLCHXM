using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.FormHandleUI;

namespace UI.FormUI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = true;

            btnDangNhap.BackColor = Color.MediumSlateBlue;
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.MouseEnter += (s, e2) => btnDangNhap.BackColor = Color.RoyalBlue;
            btnDangNhap.MouseLeave += (s, e2) => btnDangNhap.BackColor = Color.MediumSlateBlue;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string sdt = txtSoDienThoai.Text.Trim();
            string matKhau = txtMatKhau.Text;

            if (string.IsNullOrWhiteSpace(sdt) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ!", "Thiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // === GỌI BLL (KHÔNG GỌI DAL) ===
            if (TaiKhoanBLL.DangNhap(sdt, matKhau))
            {
                MessageBox.Show($"Chào {CurrentUser.HoTen}!", "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new MainForm().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("SĐT hoặc mật khẩu sai!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        private void lnkQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormDienGmailQuenMK formDienGmailQuenMK = new FormDienGmailQuenMK();
            formDienGmailQuenMK.Show();
            this.Hide();
        }
    }
}
    