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
        private bool isLoggingIn = false;
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

            txtMatKhau.KeyDown += TxtMatKhau_KeyDown;
            txtSoDienThoai.KeyDown += TxtSoDienThoai_KeyDown;
            this.FormClosing += LoginForm_FormClosing;
            
            // Style cho nút show/hide password
            btnTogglePassword.Cursor = Cursors.Hand;
            btnTogglePassword.TabStop = false;

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
            string taiKhoan = txtSoDienThoai.Text.Trim(); 
            string matKhau = txtMatKhau.Text.Trim();

            // Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(taiKhoan) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnDangNhap.Enabled = false;
                btnDangNhap.Text = "Đang đăng nhập...";
                this.Cursor = Cursors.WaitCursor;

                bool isEmail = IsValidEmail(taiKhoan);
                bool loginSuccess = false;

                if (isEmail)
                {
                    // Đăng nhập bằng Email
                    System.Diagnostics.Debug.WriteLine("👉 Đăng nhập bằng EMAIL");
                    loginSuccess = TaiKhoanBLL.DangNhapBangEmail(taiKhoan, matKhau);
                }
                else
                {
                    if (taiKhoan.Length < 10 || taiKhoan.Length > 11 || !taiKhoan.All(char.IsDigit))
                    {
                        MessageBox.Show("Vui lòng nhập Email hoặc Số điện thoại hợp lệ (10-11 chữ số)!",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSoDienThoai.Focus();
                        return;
                    }

                    // Đăng nhập bằng SĐT
                    System.Diagnostics.Debug.WriteLine("👉 Đăng nhập bằng SĐT");
                    loginSuccess = TaiKhoanBLL.DangNhap(taiKhoan, matKhau);
                }

                if (loginSuccess)
                {
                    MessageBox.Show($"Chào mừng {CurrentUser.HoTen}!\n\n" +
                                  $"Vai trò: {CurrentUser.ChucVu ?? "Khách hàng"}",
                        "Đăng nhập thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ✅ Mở MainForm và đóng LoginForm đúng cách
                    ShowMainFormAndWait();
                }
                else
                {
                    MessageBox.Show("Email/SĐT hoặc mật khẩu không đúng!\n\n" +
                                  "Vui lòng kiểm tra lại thông tin đăng nhập.",
                        "Đăng nhập thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi trong quá trình đăng nhập!\n\n" +
                              $"Chi tiết lỗi: {ex.Message}\n\n" +
                              $"Vui lòng kiểm tra kết nối database hoặc liên hệ quản trị viên.",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                System.Diagnostics.Debug.WriteLine($"Lỗi đăng nhập: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                btnDangNhap.Enabled = true;
                btnDangNhap.Text = "Đăng nhập";
                this.Cursor = Cursors.Default;
            }
        }

        private void ShowMainFormAndWait()
        {
            isLoggingIn = true; // Set flag trước khi hide
            this.Hide();

            try
            {
                MainForm mainForm = new MainForm();
                DialogResult result = mainForm.ShowDialog(); // Dùng ShowDialog thay vì Show

                // Khi MainForm đóng, quay lại đây
                System.Diagnostics.Debug.WriteLine("MainForm đã đóng, quay lại LoginForm");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở MainForm: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isLoggingIn = false;

                // Reset form và hiển thị lại
                ResetLoginForm();
                this.Show();
                this.Activate();
                this.BringToFront();
            }
        }


        public void ResetLoginForm()
        {
            txtSoDienThoai.Clear();
            txtMatKhau.Clear();
            txtSoDienThoai.Focus();

            CurrentUser.Clear();
        }

        /// <summary>
        /// Kiểm tra chuỗi có phải là Email hợp lệ không
        /// </summary>
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void lnkDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.FormClosed += (s, args) => this.Show(); 
            registerForm.Show();
            this.Hide();
        }

        private void lnkQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormDienGmailQuenMK formDienGmailQuenMK = new FormDienGmailQuenMK();
            formDienGmailQuenMK.FormClosed += (s, args) => this.Show(); 
            formDienGmailQuenMK.Show();
            this.Hide();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nếu đang trong quá trình login, không cho phép đóng
            if (isLoggingIn)
            {
                e.Cancel = true;
                return;
            }

            // Chỉ hỏi confirm khi form đang visible (user thật sự muốn thoát)
            if (this.Visible)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn thoát chương trình?",
                    "Thoát",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                // Cleanup trước khi thoát
                Application.Exit();
            }
        }

        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.UseSystemPasswordChar)
            {
                txtMatKhau.UseSystemPasswordChar = false;
                btnTogglePassword.Text = "🙈"; // Ẩn mật khẩu
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = true;
                btnTogglePassword.Text = "👁"; // Hiện mật khẩu
            }
        }

    }
}