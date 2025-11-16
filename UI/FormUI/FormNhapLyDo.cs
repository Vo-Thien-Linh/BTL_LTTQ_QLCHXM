using System;
using System.Drawing;
using System.Windows.Forms;

namespace UI.FormUI
{
    public partial class FormNhapLyDo : Form
    {
        public string LyDo { get; private set; }

        public FormNhapLyDo()
        {
            InitializeComponent();
            InitializeFormSettings();
        }

        private void InitializeFormSettings()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;

            // Gán sự kiện
            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;
            txtLyDo.KeyDown += TxtLyDo_KeyDown;
        }

        private void TxtLyDo_KeyDown(object sender, KeyEventArgs e)
        {
            // Cho phép Ctrl+Enter để submit
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                BtnOK_Click(sender, e);
            }
            // ESC để cancel
            else if (e.KeyCode == Keys.Escape)
            {
                BtnCancel_Click(sender, e);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                LyDo = txtLyDo.Text.Trim();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtLyDo.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập lý do từ chối!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtLyDo.Focus();
                return false;
            }

            if (txtLyDo.Text.Trim().Length < 10)
            {
                MessageBox.Show(
                    "Lý do từ chối phải có ít nhất 10 ký tự!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtLyDo.Focus();
                return false;
            }

            if (txtLyDo.Text.Trim().Length > 500)
            {
                MessageBox.Show(
                    "Lý do từ chối không được vượt quá 500 ký tự!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtLyDo.Focus();
                return false;
            }

            return true;
        }
    }
}