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

            // ✅ Thêm: Cho phép nhấn Enter để đăng nhập
            txtMatKhau.KeyDown += TxtMatKhau_KeyDown;
            txtSoDienThoai.KeyDown += TxtSoDienThoai_KeyDown;
        }

        private void TxtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangNhap_Click(sender, e);
            }
        }

        private void TxtSoDienThoai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMatKhau.Focus();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // ✅ Trim cả 2 trường
            string sdt = txtSoDienThoai.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim(); // ✅ Đã thêm Trim()

            // Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(sdt) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validate số điện thoại
            if (sdt.Length < 10 || sdt.Length > 11 || !sdt.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!\nVui lòng nhập 10-11 chữ số.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                return;
            }

            try
            {
                // ✅ Vô hiệu hóa nút đăng nhập trong khi xử lý
                btnDangNhap.Enabled = false;
                btnDangNhap.Text = "Đang đăng nhập...";
                this.Cursor = Cursors.WaitCursor;

                // === GỌI BLL (KHÔNG GỌI DAL) ===
                if (TaiKhoanBLL.DangNhap(sdt, matKhau))
                {
                    MessageBox.Show($"Chào mừng {CurrentUser.HoTen}!\n\n" +
                                  $"Vai trò: {CurrentUser.ChucVu ?? "Khách hàng"}",
                        "Đăng nhập thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ✅ Mở MainForm và đóng LoginForm đúng cách
                    MainForm mainForm = new MainForm();
                    mainForm.FormClosed += (s, args) => this.Close(); // Đóng LoginForm khi MainForm đóng
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Số điện thoại hoặc mật khẩu không đúng!\n\n" +
                                  "Vui lòng kiểm tra lại thông tin đăng nhập.",
                        "Đăng nhập thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // ✅ Clear mật khẩu và focus lại
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
            }
            catch (Exception ex)
            {
                // ✅ Xử lý lỗi bất ngờ
                MessageBox.Show($"Đã xảy ra lỗi trong quá trình đăng nhập!\n\n" +
                              $"Chi tiết lỗi: {ex.Message}\n\n" +
                              $"Vui lòng kiểm tra kết nối database hoặc liên hệ quản trị viên.",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // ✅ Ghi log lỗi
                System.Diagnostics.Debug.WriteLine($"Lỗi đăng nhập: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                // ✅ Khôi phục lại nút đăng nhập
                btnDangNhap.Enabled = true;
                btnDangNhap.Text = "Đăng nhập";
                this.Cursor = Cursors.Default;
            }
        }

        private void lnkDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.FormClosed += (s, args) => this.Show(); // ✅ Hiện lại LoginForm khi đóng RegisterForm
            registerForm.Show();
            this.Hide();
        }

        private void lnkQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormDienGmailQuenMK formDienGmailQuenMK = new FormDienGmailQuenMK();
            formDienGmailQuenMK.FormClosed += (s, args) => this.Show(); // ✅ Hiện lại LoginForm khi đóng
            formDienGmailQuenMK.Show();
            this.Hide();
        }
    }
}