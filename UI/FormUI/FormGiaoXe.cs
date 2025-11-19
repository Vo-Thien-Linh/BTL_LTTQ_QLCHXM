using System;
using System.Drawing;
using System.Windows.Forms;

namespace UI.FormHandleUI
{
    public partial class FormGiaoXe : Form
    {
        // Public properties để lấy dữ liệu từ form
        public int KmBatDau { get; private set; }
        public string GhiChu { get; private set; }

        public FormGiaoXe()
        {
            InitializeComponent();
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            // Đặt giá trị mặc định
            dtpNgayGiao.Value = DateTime.Now;
            dtpNgayGiao.MaxDate = DateTime.Now; // Không cho chọn ngày tương lai
            nudKmBatDau.Value = 0;
            txtGhiChu.Text = "";
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            // Xác nhận lần cuối
            DialogResult confirm = MessageBox.Show(
                $"Xác nhận giao xe với thông tin sau?\n\n" +
                $"📅 Ngày giao: {dtpNgayGiao.Value:dd/MM/yyyy HH:mm}\n" +
                $"🛣 Km bắt đầu: {nudKmBatDau.Value:N0} km\n" +
                $"📝 Ghi chú: {(string.IsNullOrWhiteSpace(txtGhiChu.Text) ? "(Không có)" : txtGhiChu.Text)}\n\n" +
                $"⚠ Sau khi giao xe:\n" +
                $"  • Trạng thái đơn → 'Đang thuê'\n" +
                $"  • Trạng thái xe → 'Đang thuê'\n" +
                $"  • Không thể hoàn tác!",
                "Xác nhận giao xe",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                // Lưu dữ liệu
                KmBatDau = (int)nudKmBatDau.Value;
                GhiChu = txtGhiChu.Text.Trim();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidateInput()
        {
            // 1. Kiểm tra ngày giao
            if (dtpNgayGiao.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show(
                    "⚠ Ngày giao xe không được lớn hơn ngày hiện tại!",
                    "Lỗi nhập liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                dtpNgayGiao.Focus();
                return false;
            }

            // 2. Kiểm tra km
            if (nudKmBatDau.Value < 0)
            {
                MessageBox.Show(
                    "⚠ Số km không hợp lệ!\n\nSố km phải >= 0",
                    "Lỗi nhập liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                nudKmBatDau.Focus();
                return false;
            }

            if (nudKmBatDau.Value > 999999)
            {
                MessageBox.Show(
                    "⚠ Số km quá lớn!\n\nSố km tối đa: 999,999 km",
                    "Lỗi nhập liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                nudKmBatDau.Focus();
                return false;
            }

            // 3. Cảnh báo nếu km lớn bất thường (không chặn)
            if (nudKmBatDau.Value > 500000)
            {
                DialogResult result = MessageBox.Show(
                    $"⚠ CẢNH BÁO: Số km bất thường!\n\n" +
                    $"Km bắt đầu: {nudKmBatDau.Value:N0} km\n\n" +
                    $"Đây là một số km rất lớn. Bạn có chắc chắn muốn tiếp tục?",
                    "Cảnh báo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.No)
                {
                    nudKmBatDau.Focus();
                    return false;
                }
            }

            return true;
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void NudKmBatDau_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}