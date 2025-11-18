using BLL;
using DTO;
using System;
using System.Windows.Forms;

namespace UI.FormHandleUI
{
    public partial class FormQuanLyKhachHang : Form
    {
        private KhachHangBLL khachHangBLL;
        private string maKH;
        private bool isEditMode;

        public FormQuanLyKhachHang()
        {
            InitializeComponent();
            khachHangBLL = new KhachHangBLL();
            isEditMode = false;
            InitializeForm();
            LoadComboBoxData();
            GenerateMaKH();
        }

        public FormQuanLyKhachHang(string maKH)
        {
            InitializeComponent();
            khachHangBLL = new KhachHangBLL();
            this.maKH = maKH;
            isEditMode = true;
            InitializeForm();
            LoadComboBoxData();
            LoadKhachHangData();
        }

        private void InitializeForm()
        {
            this.Text = isEditMode ? "Sửa Thông Tin Khách Hàng" : "Thêm Khách Hàng Mới";
            lblTitle.Text = isEditMode ? "SỬA THÔNG TIN KHÁCH HÀNG" : "THÊM KHÁCH HÀNG MỚI";
            txtMaKH.ReadOnly = isEditMode;
        }

        private void LoadComboBoxData()
        {
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.AddRange(new string[] { "Nam", "Nữ", "Khác" });
            cboGioiTinh.SelectedIndex = 0;
        }

        private void GenerateMaKH()
        {
            try
            {
                txtMaKH.Text = khachHangBLL.GenerateMaKH();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo mã khách hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadKhachHangData()
        {
            try
            {
                var dt = khachHangBLL.GetKhachHangByMaKH(maKH);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    txtMaKH.Text = row["MaKH"].ToString();
                    txtHoTen.Text = row["HoTenKH"].ToString();

                    if (row["NgaySinh"] != DBNull.Value)
                    {
                        dtpNgaySinh.Value = Convert.ToDateTime(row["NgaySinh"]);
                        chkNgaySinh.Checked = true;
                    }
                    else
                    {
                        chkNgaySinh.Checked = false;
                        dtpNgaySinh.Enabled = false;
                    }

                    cboGioiTinh.SelectedItem = row["GioiTinh"] != DBNull.Value ? row["GioiTinh"].ToString() : "Nam";
                    txtSdt.Text = row["Sdt"] != DBNull.Value ? row["Sdt"].ToString() : "";
                    txtEmail.Text = row["Email"] != DBNull.Value ? row["Email"].ToString() : "";
                    txtDiaChi.Text = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                KhachHang kh = new KhachHang
                {
                    MaKH = txtMaKH.Text.Trim(),
                    HoTenKH = txtHoTen.Text.Trim(),
                    NgaySinh = chkNgaySinh.Checked ? (DateTime?)dtpNgaySinh.Value : null,
                    GioiTinh = cboGioiTinh.SelectedItem.ToString(),
                    Sdt = txtSdt.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim()
                };

                string errorMessage;
                bool result;

                if (isEditMode)
                {
                    result = khachHangBLL.UpdateKhachHang(kh, out errorMessage);
                }
                else
                {
                    result = khachHangBLL.InsertKhachHang(kh, out errorMessage);
                }

                if (result)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(errorMessage, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên khách hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (cboGioiTinh.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGioiTinh.Focus();
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ChkNgaySinh_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgaySinh.Enabled = chkNgaySinh.Checked;
            if (!chkNgaySinh.Checked)
            {
                dtpNgaySinh.Value = DateTime.Now;
            }
        }
    }
}