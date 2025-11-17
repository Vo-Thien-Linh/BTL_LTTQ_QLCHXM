using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            txtMatKhau.UseSystemPasswordChar = true;


            cbbChucVu.DataSource = DangKyBLL.LayDanhSachChucVu();
            cbbChucVu.DisplayMember = "ChucVu";
            cbbChucVu.ValueMember = "ChucVu";

            // Đổ dữ liệu giới tính vào combobox giới tính
            cbbGioiTinh.Items.Clear();
            cbbGioiTinh.Items.Add("Nam");
            cbbGioiTinh.Items.Add("Nữ");
            cbbGioiTinh.Items.Add("Khác"); // Nếu muốn thêm lựa chọn khác
            cbbGioiTinh.SelectedIndex = 0; // Mặc định chọn Nam
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            DateTime ngaySinh = dtpNgaySinh.Value;
            string gioiTinh = cbbGioiTinh.SelectedItem?.ToString();
            string sdt = txtSoDienThoai.Text.Trim();
            string email = txtGmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string chucVu = cbbChucVu.SelectedValue?.ToString();
            string cccd = txtCCCD.Text.Trim();
            string matKhau = txtMatKhau.Text;

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(cccd) ||
                string.IsNullOrEmpty(chucVu) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các trường bắt buộc!");
                return;
            }

            if (DangKyBLL.KiemTraSoDienThoaiTrung(sdt))
            {
                MessageBox.Show("Số điện thoại đã tồn tại!");
                return;
            }
            if (DangKyBLL.KiemTraEmailTrung(email))
            {
                MessageBox.Show("Email đã tồn tại!");
                return;
            }
            if (DangKyBLL.KiemTraCCCDTrung(cccd))
            {
                MessageBox.Show("CCCD đã tồn tại!");
                return;
            }

            bool success = DangKyBLL.DangKyNhanVien(hoTen, ngaySinh, gioiTinh, sdt, email, diaChi, chucVu, cccd, matKhau);
            if (success)
            {
                MessageBox.Show("Đăng ký thành công!");
                LoginForm f = new LoginForm();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại! Kiểm tra lại dữ liệu.");
            }

        }

        private void lnkDangNhapNgay_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void cbbChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpNgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtSoDienThoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtGmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCCCD_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
