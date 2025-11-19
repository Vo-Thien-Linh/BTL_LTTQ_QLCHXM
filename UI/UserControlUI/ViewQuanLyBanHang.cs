using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using UI.FormUI;

namespace UI.UserControlUI
{
    public partial class ViewQuanLyBanHang : UserControl
    {
        private GiaoDichBanBLL giaoDichBanBLL;
        private GiaoDichThueBLL giaoDichThueBLL;
        private DataGridView dgvGiaoDich;
        private bool isViewingBan = true; // true = Bán, false = Thuê
        private string currentMaNV; // Mã nhân viên đang đăng nhập

        public ViewQuanLyBanHang(string maNV)
        {
            InitializeComponent();
            giaoDichBanBLL = new GiaoDichBanBLL();
            giaoDichThueBLL = new GiaoDichThueBLL();
            currentMaNV = maNV;

            InitializeDataGrid();
            LoadData();

            // Gán sự kiện
            this.Load += ViewQuanLyBanHang_Load;
            btnSearch.Click += BtnSearch_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnViewDetail.Click += BtnViewDetail_Click;
            btnApprove.Click += BtnApprove_Click;
            btnReject.Click += BtnReject_Click;
            cboLoaiGiaoDich.SelectedIndexChanged += CboLoaiGiaoDich_SelectedIndexChanged;
            cboTrangThai.SelectedIndexChanged += CboTrangThai_SelectedIndexChanged;
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnSearch_Click(s, e); };
        }

        private void InitializeDataGrid()
        {
            dgvGiaoDich = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(248, 250, 252) },
                RowHeadersVisible = false,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(25, 118, 210),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            };

            panelDataGrid.Controls.Add(dgvGiaoDich);

            // Xử lý double click để xem chi tiết
            dgvGiaoDich.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    BtnViewDetail_Click(s, e);
                }
            };

            // Xử lý selection changed
            dgvGiaoDich.SelectionChanged += DgvGiaoDich_SelectionChanged;
        }

        private void ViewQuanLyBanHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt;
                string trangThai = cboTrangThai.SelectedItem?.ToString();

                if (isViewingBan)
                {
                    // Load giao dịch bán
                    if (string.IsNullOrEmpty(trangThai) || trangThai == "Tất cả")
                    {
                        dt = giaoDichBanBLL.GetAllGiaoDichBan();
                    }
                    else
                    {
                        dt = giaoDichBanBLL.GetGiaoDichBanByTrangThai(trangThai);
                    }
                }
                else
                {
                    // Load giao dịch thuê
                    if (string.IsNullOrEmpty(trangThai) || trangThai == "Tất cả")
                    {
                        dt = giaoDichThueBLL.GetAllGiaoDichThue();
                    }
                    else
                    {
                        dt = giaoDichThueBLL.GetGiaoDichThueByTrangThai(trangThai);
                    }
                }

                dgvGiaoDich.DataSource = dt;
                UpdateRecordCount(dt.Rows.Count);
                FormatDataGridView();
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvGiaoDich.Columns.Count == 0) return;

            // Ẩn các cột không cần thiết
            if (dgvGiaoDich.Columns.Contains("MaTaiKhoan"))
                dgvGiaoDich.Columns["MaTaiKhoan"].Visible = false;
            if (dgvGiaoDich.Columns.Contains("NguoiDuyet"))
                dgvGiaoDich.Columns["NguoiDuyet"].Visible = false;
            if (dgvGiaoDich.Columns.Contains("GhiChuDuyet"))
                dgvGiaoDich.Columns["GhiChuDuyet"].Visible = false;

            // Đổi tên cột hiển thị
            if (isViewingBan)
            {
                if (dgvGiaoDich.Columns.Contains("MaGDBan"))
                    dgvGiaoDich.Columns["MaGDBan"].HeaderText = "Mã GD";
                if (dgvGiaoDich.Columns.Contains("NgayBan"))
                    dgvGiaoDich.Columns["NgayBan"].HeaderText = "Ngày Bán";
                if (dgvGiaoDich.Columns.Contains("GiaBan"))
                {
                    dgvGiaoDich.Columns["GiaBan"].HeaderText = "Giá Bán";
                    dgvGiaoDich.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                }
            }
            else
            {
                if (dgvGiaoDich.Columns.Contains("MaGDThue"))
                    dgvGiaoDich.Columns["MaGDThue"].HeaderText = "Mã GD";
                if (dgvGiaoDich.Columns.Contains("NgayBatDau"))
                    dgvGiaoDich.Columns["NgayBatDau"].HeaderText = "Ngày Bắt Đầu";
                if (dgvGiaoDich.Columns.Contains("NgayKetThuc"))
                    dgvGiaoDich.Columns["NgayKetThuc"].HeaderText = "Ngày Kết Thúc";
                if (dgvGiaoDich.Columns.Contains("SoNgayThue"))
                    dgvGiaoDich.Columns["SoNgayThue"].HeaderText = "Số Ngày";
                if (dgvGiaoDich.Columns.Contains("GiaThueNgay"))
                {
                    dgvGiaoDich.Columns["GiaThueNgay"].HeaderText = "Giá/Ngày";
                    dgvGiaoDich.Columns["GiaThueNgay"].DefaultCellStyle.Format = "N0";
                }
                if (dgvGiaoDich.Columns.Contains("TongGia"))
                {
                    dgvGiaoDich.Columns["TongGia"].HeaderText = "Tổng Giá";
                    dgvGiaoDich.Columns["TongGia"].DefaultCellStyle.Format = "N0";
                }
                if (dgvGiaoDich.Columns.Contains("SoTienCoc"))
                {
                    dgvGiaoDich.Columns["SoTienCoc"].HeaderText = "Tiền Cọc";
                    dgvGiaoDich.Columns["SoTienCoc"].DefaultCellStyle.Format = "N0";
                }
            }

            // Cột chung
            if (dgvGiaoDich.Columns.Contains("HoTenKH"))
                dgvGiaoDich.Columns["HoTenKH"].HeaderText = "Khách Hàng";
            if (dgvGiaoDich.Columns.Contains("SdtKhachHang"))
                dgvGiaoDich.Columns["SdtKhachHang"].HeaderText = "SĐT";
            if (dgvGiaoDich.Columns.Contains("TenXe"))
                dgvGiaoDich.Columns["TenXe"].HeaderText = "Xe";
            if (dgvGiaoDich.Columns.Contains("BienSo"))
                dgvGiaoDich.Columns["BienSo"].HeaderText = "Biển Số";
            if (dgvGiaoDich.Columns.Contains("TrangThaiDuyet"))
                dgvGiaoDich.Columns["TrangThaiDuyet"].HeaderText = "Trạng Thái";
            if (dgvGiaoDich.Columns.Contains("TrangThaiThanhToan"))
                dgvGiaoDich.Columns["TrangThaiThanhToan"].HeaderText = "Thanh Toán";
            if (dgvGiaoDich.Columns.Contains("NgayDuyet"))
                dgvGiaoDich.Columns["NgayDuyet"].HeaderText = "Ngày Duyệt";
            if (dgvGiaoDich.Columns.Contains("TenNhanVien"))
                dgvGiaoDich.Columns["TenNhanVien"].HeaderText = "Người Duyệt";

            // Set column width
            dgvGiaoDich.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Highlight trạng thái
            dgvGiaoDich.CellFormatting += DgvGiaoDich_CellFormatting;
        }

        private void DgvGiaoDich_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvGiaoDich.Columns[e.ColumnIndex].Name == "TrangThaiDuyet")
            {
                if (e.Value != null)
                {
                    string trangThai = e.Value.ToString();
                    if (trangThai == "Chờ duyệt")
                    {
                        e.CellStyle.BackColor = Color.FromArgb(255, 243, 205); // Vàng nhạt
                        e.CellStyle.ForeColor = Color.FromArgb(255, 152, 0);
                    }
                    else if (trangThai == "Đã duyệt")
                    {
                        e.CellStyle.BackColor = Color.FromArgb(200, 230, 201); // Xanh lá nhạt
                        e.CellStyle.ForeColor = Color.FromArgb(56, 142, 60);
                    }
                    else if (trangThai == "Từ chối")
                    {
                        e.CellStyle.BackColor = Color.FromArgb(255, 205, 210); // Đỏ nhạt
                        e.CellStyle.ForeColor = Color.FromArgb(211, 47, 47);
                    }
                }
            }
        }

        private void DgvGiaoDich_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = dgvGiaoDich.SelectedRows.Count > 0;
            bool isChoDuyet = false;

            if (hasSelection)
            {
                string trangThai = dgvGiaoDich.SelectedRows[0].Cells["TrangThaiDuyet"].Value.ToString();
                isChoDuyet = trangThai == "Chờ duyệt";
            }

            btnViewDetail.Enabled = hasSelection;
            btnApprove.Enabled = hasSelection && isChoDuyet;
            btnReject.Enabled = hasSelection && isChoDuyet;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            try
            {
                DataTable dt;
                if (isViewingBan)
                {
                    dt = giaoDichBanBLL.SearchGiaoDichBan(keyword);
                }
                else
                {
                    dt = giaoDichThueBLL.SearchGiaoDichThue(keyword);
                }

                dgvGiaoDich.DataSource = dt;
                UpdateRecordCount(dt.Rows.Count);
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboTrangThai.SelectedIndex = 0;
            LoadData();
        }

        private void CboLoaiGiaoDich_SelectedIndexChanged(object sender, EventArgs e)
        {
            isViewingBan = cboLoaiGiaoDich.SelectedIndex == 0;
            LoadData();
        }

        private void CboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnViewDetail_Click(object sender, EventArgs e)
        {
            if (dgvGiaoDich.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một giao dịch để xem chi tiết!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (isViewingBan)
                {
                    int maGDBan = Convert.ToInt32(dgvGiaoDich.SelectedRows[0].Cells["MaGDBan"].Value);
                    using (FormDuyetDonHang form = new FormDuyetDonHang(maGDBan, true, currentMaNV))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            LoadData();
                        }
                    }
                }
                else
                {
                    int maGDThue = Convert.ToInt32(dgvGiaoDich.SelectedRows[0].Cells["MaGDThue"].Value);
                    using (FormDuyetDonHang form = new FormDuyetDonHang(maGDThue, false, currentMaNV))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            LoadData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xem chi tiết: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnApprove_Click(object sender, EventArgs e)
        {
            if (dgvGiaoDich.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một giao dịch để duyệt!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn duyệt đơn hàng này?",
                "Xác nhận duyệt",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    string errorMessage;
                    bool success;

                    if (isViewingBan)
                    {
                        int maGDBan = Convert.ToInt32(dgvGiaoDich.SelectedRows[0].Cells["MaGDBan"].Value);
                        success = giaoDichBanBLL.ApproveGiaoDichBan(maGDBan, currentMaNV, "", out errorMessage);
                    }
                    else
                    {
                        int maGDThue = Convert.ToInt32(dgvGiaoDich.SelectedRows[0].Cells["MaGDThue"].Value);
                        success = giaoDichThueBLL.ApproveGiaoDichThue(maGDThue, currentMaNV, "", out errorMessage);
                    }

                    if (success)
                    {
                        MessageBox.Show("Duyệt đơn hàng thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi duyệt đơn hàng: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnReject_Click(object sender, EventArgs e)
        {
            if (dgvGiaoDich.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một giao dịch để từ chối!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị form nhập lý do từ chối
            using (FormNhapLyDo formLyDo = new FormNhapLyDo())
            {
                if (formLyDo.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string errorMessage;
                        bool success;

                        if (isViewingBan)
                        {
                            int maGDBan = Convert.ToInt32(dgvGiaoDich.SelectedRows[0].Cells["MaGDBan"].Value);
                            success = giaoDichBanBLL.RejectGiaoDichBan(maGDBan, currentMaNV, formLyDo.LyDo, out errorMessage);
                        }
                        else
                        {
                            int maGDThue = Convert.ToInt32(dgvGiaoDich.SelectedRows[0].Cells["MaGDThue"].Value);
                            success = giaoDichThueBLL.RejectGiaoDichThue(maGDThue, currentMaNV, formLyDo.LyDo, out errorMessage);
                        }

                        if (success)
                        {
                            MessageBox.Show("Từ chối đơn hàng thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show(errorMessage, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi từ chối đơn hàng: " + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void UpdateRecordCount(int count)
        {
            lblRecordCount.Text = $"Tổng số giao dịch: {count}";
            lblRecordCount.ForeColor = count > 0 ? Color.FromArgb(25, 118, 210) : Color.Gray;
        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}