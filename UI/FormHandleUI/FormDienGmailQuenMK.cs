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

        private string originalButtonText;
        private Color originalButtonColor;

        public FormDienGmailQuenMK()
        {
            InitializeComponent();
        }

        private void FormDienGmailQuenMK_Load(object sender, EventArgs e)
        {
            originalButtonText = btnGuiMa.Text;
            originalButtonColor = btnGuiMa.BackColor;
        }

        private async void btnGuiMa_Click(object sender, EventArgs e)
        {
            string email = txtGmail.Text.Trim();

            // Validate email cơ bản
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGmail.Focus();
                return;
            }

            var bll = new QuenMatKhauBLL();

            // Kiểm tra email tồn tại
            if (!bll.KiemTraEmail(email))
            {
                MessageBox.Show("Email không tồn tại trong hệ thống!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGmail.Focus();
                return;
            }

            SetLoadingState(true);

            try
            {
                string otp = new Random().Next(100000, 999999).ToString();

                bool success = await Task.Run(() => EmailHelper.GuiOTP(email, otp));

                if (success)
                {
                    OTPManager.Email = email;
                    OTPManager.MaOTP = otp;
                    OTPManager.ThoiGianGui = DateTime.Now;

                    MessageBox.Show("Đã gửi mã OTP đến email!\n\nVui lòng kiểm tra hộp thư (kể cả thư rác).",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FormDienMaQuenMK formDienMa = new FormDienMaQuenMK();
                    formDienMa.FormClosed += (s, args) => this.Close();
                    formDienMa.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Gửi email thất bại!\n\nVui lòng kiểm tra:\n" +
                                  "- Kết nối Internet\n" +
                                  "- Cấu hình email server\n" +
                                  "- Email người nhận có chính xác không",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi gửi email!\n\nChi tiết: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Lỗi gửi OTP: {ex.Message}\n{ex.StackTrace}");
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
                btnGuiMa.Enabled = false;
                btnGuiMa.Text = "Đang gửi...";
                btnGuiMa.BackColor = Color.Gray;
                txtGmail.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                btnGuiMa.Enabled = true;
                btnGuiMa.Text = originalButtonText;
                btnGuiMa.BackColor = originalButtonColor;
                txtGmail.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }
    }
}
