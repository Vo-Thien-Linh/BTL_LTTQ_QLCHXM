using System;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;

namespace UI.UserControlUI
{
    public partial class ViewTaiKhoanKH : UserControl
    {
        private string maKhachHang;
        private KhachHangBLL khachHangBLL;
        private bool isEditing = false;

        public ViewTaiKhoanKH(string maKH)
        {
            InitializeComponent();
            this.maKhachHang = maKH;
            khachHangBLL = new KhachHangBLL();

            LoadThongTin();
            SetReadOnlyMode();

            btnEdit.Click += BtnEdit_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            btnChangePassword.Click += BtnChangePassword_Click;
        }

        private void LoadThongTin()
        {
            try
            {
                // TODO: Implement GetKhachHangByMa in BLL
                // For now, use placeholder
                txtMaKH.Text = maKhachHang;
                txtHoTen.Text = "Nguyen Van A";
                dtpNgaySinh.Value = new DateTime(1990, 1, 1);
                cboGioiTinh.SelectedItem = "Nam";
                txtSDT.Text = "0123456789";
                txtEmail.Text = "example@email.com";
                txtDiaChi.Text = "123 Duong ABC, Quan XYZ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetReadOnlyMode()
        {
            isEditing = false;
            txtHoTen.ReadOnly = true;
            dtpNgaySinh.Enabled = false;
            cboGioiTinh.Enabled = false;
            txtSDT.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtDiaChi.ReadOnly = true;

            btnSave.Visible = false;
            btnCancel.Visible = false;
            btnEdit.Visible = true;

            // Set background color for readonly fields
            Color readOnlyColor = Color.FromArgb(245, 245, 245);
            txtHoTen.BackColor = readOnlyColor;
            txtSDT.BackColor = readOnlyColor;
            txtEmail.BackColor = readOnlyColor;
            txtDiaChi.BackColor = readOnlyColor;
        }

        private void SetEditMode()
        {
            isEditing = true;
            txtHoTen.ReadOnly = false;
            dtpNgaySinh.Enabled = true;
            cboGioiTinh.Enabled = true;
            txtSDT.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtDiaChi.ReadOnly = false;

            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnEdit.Visible = false;

            // Set background color for editable fields
            Color editableColor = Color.White;
            txtHoTen.BackColor = editableColor;
            txtSDT.BackColor = editableColor;
            txtEmail.BackColor = editableColor;
            txtDiaChi.BackColor = editableColor;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            SetEditMode();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn cập nhật thông tin?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // TODO: Implement UpdateKhachHang
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetReadOnlyMode();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            LoadThongTin();
            SetReadOnlyMode();
        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            // TODO: Open change password form
            MessageBox.Show("Chức năng đổi mật khẩu đang phát triển!", "Thông báo");
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            return true;
        }
    }
}