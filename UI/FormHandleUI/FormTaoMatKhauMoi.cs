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
        private string originalButtonText;
        private Color originalButtonColor;

        public FormTaoMatKhauMoi()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void FormTaoMatKhauMoi_Load(object sender, EventArgs e)
        {
            originalButtonText = btnXacNhan.Text;
            originalButtonColor = btnXacNhan.BackColor;

            txtMatKhauMoi.UseSystemPasswordChar = true;
            txtXacNhanMatKhau.UseSystemPasswordChar = true;
        }

        private async void btnXacNhan_Click(object sender, EventArgs e)
        {
            string mkMoi = txtMatKhauMoi.Text.Trim();
            string xacNhan = txtXacNhanMatKhau.Text.Trim();

            if (string.IsNullOrWhiteSpace(mkMoi))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }

            if (mkMoi.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(xacNhan))
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Focus();
                return;
            }

            if (mkMoi != xacNhan)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!\n\nVui lòng nhập lại.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtXacNhanMatKhau.Clear();
                txtXacNhanMatKhau.Focus();
                return;
            }

            SetLoadingState(true);

            try
            {
                var bll = new QuenMatKhauBLL();

                bool success = await Task.Run(() => bll.DoiMatKhau(OTPManager.Email, mkMoi, xacNhan));

                if (success)
                {
                    MessageBox.Show(
                        "✅ Đặt lại mật khẩu thành công!\n\n" +
                        $"📧 Email: {OTPManager.Email}\n" +
                        "🔐 Mật khẩu mới đã được cập nhật\n\n" +
                        "Bạn có thể đăng nhập ngay bây giờ!",
                        "Hoàn tất",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    OTPManager.Clear();

                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "❌ Không thể đổi mật khẩu!\n\n" +
                        "Nguyên nhân có thể:\n" +
                        "- Mật khẩu không hợp lệ (< 6 ký tự)\n" +
                        "- Mật khẩu xác nhận không khớp\n" +
                        "- Lỗi kết nối database\n\n" +
                        "Vui lòng thử lại!",
                        "Thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Đã xảy ra lỗi trong quá trình đổi mật khẩu!\n\n" +
                    $"Chi tiết: {ex.Message}",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                System.Diagnostics.Debug.WriteLine($"Lỗi đổi mật khẩu: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        /// <summary>
        /// Bật/tắt trạng thái loading cho button
        /// </summary>
        private void SetLoadingState(bool isLoading)
        {
            if (isLoading)
            {
                btnXacNhan.Enabled = false;
                btnXacNhan.Text = "Đang xử lý...";
                btnXacNhan.BackColor = Color.Gray;
                txtMatKhauMoi.Enabled = false;
                txtXacNhanMatKhau.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                btnXacNhan.Enabled = true;
                btnXacNhan.Text = originalButtonText;
                btnXacNhan.BackColor = originalButtonColor;
                txtMatKhauMoi.Enabled = true;
                txtXacNhanMatKhau.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }
    }
}