using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;

namespace UI.UserControlUI
{
    public partial class FormThemSuaKhuyenMai : Form
    {
        private KhuyenMaiBLL khuyenMaiBLL;
        private string maKM; // null = thêm mới, có giá trị = sửa
        private bool isEditMode;

        public FormThemSuaKhuyenMai(string maKhuyenMai)
        {
            InitializeComponent();
            khuyenMaiBLL = new KhuyenMaiBLL();
            this.maKM = maKhuyenMai;
            this.isEditMode = !string.IsNullOrWhiteSpace(maKhuyenMai);
        }

        private void FormThemSuaKhuyenMai_Load(object sender, EventArgs e)
        {
            try
            {
                LoadComboBoxes();

                if (isEditMode)
                {
                    this.Text = "Sửa Khuyến Mãi";
                    lblTitle.Text = "SỬA KHUYẾN MÃI";
                    LoadKhuyenMaiInfo();
                    txtMaKM.Enabled = false;
                }
                else
                {
                    this.Text = "Thêm Khuyến Mãi Mới";
                    lblTitle.Text = "THÊM KHUYẾN MÃI MỚI";
                    txtMaKM.Enabled = false;
                    
                    // Tự động tạo mã khuyến mãi
                    txtMaKM.Text = khuyenMaiBLL.GenerateMaKM();
                    
                    // Set giá trị mặc định
                    dtpNgayBatDau.Value = DateTime.Now;
                    dtpNgayKetThuc.Value = DateTime.Now.AddDays(30);
                    cboLoaiKhuyenMai.SelectedIndex = 0;
                    cboLoaiApDung.SelectedIndex = 0;
                    cboTrangThai.SelectedIndex = 0;
                }

                UpdateLoaiKhuyenMaiControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxes()
        {
            // ComboBox Loại khuyến mãi
            cboLoaiKhuyenMai.Items.Clear();
            cboLoaiKhuyenMai.Items.AddRange(new string[] { "Giảm %", "Giảm tiền" });
            cboLoaiKhuyenMai.SelectedIndex = 0;

            // ComboBox Loại áp dụng
            cboLoaiApDung.Items.Clear();
            cboLoaiApDung.Items.AddRange(new string[] { "Tất cả", "Xe bán", "Xe thuê", "Phụ tùng" });
            cboLoaiApDung.SelectedIndex = 0;

            // ComboBox Trạng thái
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new string[] { "Hoạt động", "Tạm dừng", "Hết hạn", "Hủy" });
            cboTrangThai.SelectedIndex = 0;
        }

        private void LoadKhuyenMaiInfo()
        {
            try
            {
                DataTable dt = khuyenMaiBLL.GetAllKhuyenMai();
                DataRow[] rows = dt.Select($"MaKM = '{maKM}'");

                if (rows.Length > 0)
                {
                    DataRow row = rows[0];

                    txtMaKM.Text = row["MaKM"].ToString();
                    txtTenKM.Text = row["TenKM"].ToString();
                    txtMoTa.Text = row["MoTa"].ToString();

                    if (row["NgayBatDau"] != DBNull.Value)
                        dtpNgayBatDau.Value = Convert.ToDateTime(row["NgayBatDau"]);

                    if (row["NgayKetThuc"] != DBNull.Value)
                        dtpNgayKetThuc.Value = Convert.ToDateTime(row["NgayKetThuc"]);

                    string loaiKM = row["LoaiKhuyenMai"].ToString();
                    cboLoaiKhuyenMai.SelectedItem = loaiKM;

                    if (loaiKM == "Giảm %")
                    {
                        if (row["PhanTramGiam"] != DBNull.Value)
                            numPhanTramGiam.Value = Convert.ToDecimal(row["PhanTramGiam"]);

                        if (row["GiaTriGiamToiDa"] != DBNull.Value)
                            numGiaTriToiDa.Value = Convert.ToDecimal(row["GiaTriGiamToiDa"]);
                    }
                    else
                    {
                        if (row["SoTienGiam"] != DBNull.Value)
                            numSoTienGiam.Value = Convert.ToDecimal(row["SoTienGiam"]);
                    }

                    cboLoaiApDung.SelectedItem = row["LoaiApDung"].ToString();
                    cboTrangThai.SelectedItem = row["TrangThai"].ToString();
                    txtGhiChu.Text = row["GhiChu"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi load thông tin khuyến mãi: " + ex.Message);
            }
        }

        private void UpdateLoaiKhuyenMaiControls()
        {
            if (cboLoaiKhuyenMai.SelectedItem.ToString() == "Giảm %")
            {
                lblPhanTramGiam.Visible = true;
                numPhanTramGiam.Visible = true;
                lblGiaTriToiDa.Visible = true;
                numGiaTriToiDa.Visible = true;

                lblSoTienGiam.Visible = false;
                numSoTienGiam.Visible = false;

                // Reset giá trị
                numSoTienGiam.Value = 0;
            }
            else // Giảm tiền
            {
                lblPhanTramGiam.Visible = false;
                numPhanTramGiam.Visible = false;
                lblGiaTriToiDa.Visible = false;
                numGiaTriToiDa.Visible = false;

                lblSoTienGiam.Visible = true;
                numSoTienGiam.Visible = true;

                // Reset giá trị
                numPhanTramGiam.Value = 0;
                numGiaTriToiDa.Value = 0;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate
                if (string.IsNullOrWhiteSpace(txtMaKM.Text))
                {
                    MessageBox.Show("Lỗi: Không thể tạo mã khuyến mãi tự động!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenKM.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khuyến mãi!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenKM.Focus();
                    return;
                }

                if (dtpNgayKetThuc.Value < dtpNgayBatDau.Value)
                {
                    MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpNgayKetThuc.Focus();
                    return;
                }

                // Kiểm tra ngày bắt đầu và ngày kết thúc không được nhỏ hơn ngày hiện tại khi thêm mới
                if (!isEditMode)
                {
                    DateTime today = DateTime.Now.Date;
                    
                    if (dtpNgayBatDau.Value.Date < today)
                    {
                        MessageBox.Show("Ngày bắt đầu không được nhỏ hơn ngày hiện tại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dtpNgayBatDau.Focus();
                        return;
                    }

                    if (dtpNgayKetThuc.Value.Date < today)
                    {
                        MessageBox.Show("Ngày kết thúc không được nhỏ hơn ngày hiện tại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dtpNgayKetThuc.Focus();
                        return;
                    }
                }

                // Validate theo loại khuyến mãi
                if (cboLoaiKhuyenMai.SelectedItem.ToString() == "Giảm %")
                {
                    if (numPhanTramGiam.Value <= 0 || numPhanTramGiam.Value > 100)
                    {
                        MessageBox.Show("Phần trăm giảm phải từ 0 đến 100%!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        numPhanTramGiam.Focus();
                        return;
                    }
                }
                else
                {
                    if (numSoTienGiam.Value <= 0)
                    {
                        MessageBox.Show("Số tiền giảm phải lớn hơn 0!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        numSoTienGiam.Focus();
                        return;
                    }
                }

                // Tạo DTO
                KhuyenMaiDTO km = new KhuyenMaiDTO
                {
                    MaKM = txtMaKM.Text.Trim(),
                    TenKM = txtTenKM.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim(),
                    NgayBatDau = dtpNgayBatDau.Value.Date,
                    NgayKetThuc = dtpNgayKetThuc.Value.Date,
                    LoaiKhuyenMai = cboLoaiKhuyenMai.SelectedItem.ToString(),
                    LoaiApDung = cboLoaiApDung.SelectedItem.ToString(),
                    TrangThai = cboTrangThai.SelectedItem.ToString(),
                    GhiChu = txtGhiChu.Text.Trim(),
                    MaTaiKhoan = CurrentUser.MaTaiKhoan
                };

                if (km.LoaiKhuyenMai == "Giảm %")
                {
                    km.PhanTramGiam = numPhanTramGiam.Value;
                    if (numGiaTriToiDa.Value > 0)
                        km.GiaTriGiamToiDa = numGiaTriToiDa.Value;
                    km.SoTienGiam = null;
                }
                else
                {
                    km.SoTienGiam = numSoTienGiam.Value;
                    km.PhanTramGiam = null;
                    km.GiaTriGiamToiDa = null;
                }

                string errorMessage;
                bool success;

                if (isEditMode)
                {
                    success = khuyenMaiBLL.UpdateKhuyenMai(km, out errorMessage);
                }
                else
                {
                    success = khuyenMaiBLL.InsertKhuyenMai(km, out errorMessage);
                }

                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lỗi: " + errorMessage, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cboLoaiKhuyenMai_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLoaiKhuyenMaiControls();
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
