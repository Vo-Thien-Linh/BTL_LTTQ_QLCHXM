using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using UI.FormUI;

namespace UI.FormUI
{
    public partial class FormDanhSachHopDongMua : Form
    {
        private HopDongMuaBLL hopDongMuaBLL;
        private DataGridView dgvHopDong;

        public FormDanhSachHopDongMua()
        {
            InitializeComponent();
            hopDongMuaBLL = new HopDongMuaBLL();

            InitializeDataGrid();
            LoadComboBoxes();
            LoadHopDong();
        }

        private void LoadComboBoxes()
        {
            // ComboBox tìm kiếm theo
            cboTimKiemTheo.Items.Clear();
            cboTimKiemTheo.Items.Add("Tất cả");
            cboTimKiemTheo.Items.Add("Mã hợp đồng");
            cboTimKiemTheo.Items.Add("Mã giao dịch");
            cboTimKiemTheo.Items.Add("Khách hàng");
            cboTimKiemTheo.Items.Add("Số điện thoại");
            cboTimKiemTheo.Items.Add("Biển số xe");
            cboTimKiemTheo.Items.Add("Tên xe");
            cboTimKiemTheo.SelectedIndex = 0;

            // ComboBox trạng thái
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Tất cả");
            cboTrangThai.Items.Add("Đang hiệu lực");
            cboTrangThai.Items.Add("Hết hạn");
            cboTrangThai.Items.Add("Hủy");
            cboTrangThai.SelectedIndex = 0;

            // Set ngày mặc định
            dtpTuNgay.Value = DateTime.Now.AddMonths(-3);
            dtpDenNgay.Value = DateTime.Now;
        }

        private void InitializeDataGrid()
        {
            dgvHopDong = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false,
                ColumnHeadersHeight = 40
            };

            // Style header
            dgvHopDong.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            dgvHopDong.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHopDong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvHopDong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Style rows
            dgvHopDong.RowTemplate.Height = 35;
            dgvHopDong.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvHopDong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 222, 251);
            dgvHopDong.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvHopDong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Thêm nút xem
            DataGridViewButtonColumn btnXem = new DataGridViewButtonColumn
            {
                Name = "btnXem",
                HeaderText = "Chi tiết",
                Text = "🔍 Xem",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvHopDong.Columns.Add(btnXem);

            // Event
            dgvHopDong.CellClick += DgvHopDong_CellClick;

            panelDataGrid.Controls.Add(dgvHopDong);
        }

        private void LoadHopDong()
        {
            try
            {
                DataTable dt = hopDongMuaBLL.GetAllHopDongMua();
                dgvHopDong.DataSource = dt;

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Ẩn các cột không cần thiết
                    if (dgvHopDong.Columns.Contains("MaTaiKhoan"))
                        dgvHopDong.Columns["MaTaiKhoan"].Visible = false;
                    if (dgvHopDong.Columns.Contains("FileHopDong"))
                        dgvHopDong.Columns["FileHopDong"].Visible = false;
                    if (dgvHopDong.Columns.Contains("DieuKhoan"))
                        dgvHopDong.Columns["DieuKhoan"].Visible = false;
                    if (dgvHopDong.Columns.Contains("ID_Xe"))
                        dgvHopDong.Columns["ID_Xe"].Visible = false;

                    // Đặt tên cột
                    dgvHopDong.Columns["MaHDM"].HeaderText = "Mã HĐM";
                    dgvHopDong.Columns["MaHDM"].Width = 80;
                    dgvHopDong.Columns["MaGDBan"].HeaderText = "Mã GD";
                    dgvHopDong.Columns["MaGDBan"].Width = 80;
                    dgvHopDong.Columns["MaKH"].HeaderText = "Mã KH";
                    dgvHopDong.Columns["MaKH"].Width = 80;
                    dgvHopDong.Columns["HoTenKH"].HeaderText = "Khách hàng";
                    dgvHopDong.Columns["Sdt"].HeaderText = "SĐT";
                    dgvHopDong.Columns["Sdt"].Width = 110;
                    dgvHopDong.Columns["TenXe"].HeaderText = "Xe";
                    dgvHopDong.Columns["BienSo"].HeaderText = "Biển số";
                    dgvHopDong.Columns["BienSo"].Width = 100;
                    dgvHopDong.Columns["NgayLap"].HeaderText = "Ngày lập";
                    dgvHopDong.Columns["NgayLap"].Width = 100;
                    dgvHopDong.Columns["GiaBan"].HeaderText = "Giá bán";
                    dgvHopDong.Columns["GiaBan"].Width = 120;
                    dgvHopDong.Columns["TrangThaiHopDong"].HeaderText = "Trạng thái";
                    dgvHopDong.Columns["TrangThaiHopDong"].Width = 120;
                    dgvHopDong.Columns["TenNhanVien"].HeaderText = "Nhân viên";
                    dgvHopDong.Columns["GhiChu"].HeaderText = "Ghi chú";

                    // Format giá
                    dgvHopDong.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                    dgvHopDong.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    // Format ngày
                    dgvHopDong.Columns["NgayLap"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvHopDong.Columns["NgayLap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Màu trạng thái
                    foreach (DataGridViewRow row in dgvHopDong.Rows)
                    {
                        string trangThai = row.Cells["TrangThaiHopDong"].Value?.ToString();
                        if (trangThai == "Đang hiệu lực")
                            row.Cells["TrangThaiHopDong"].Style.ForeColor = Color.Green;
                        else if (trangThai == "Hết hạn")
                            row.Cells["TrangThaiHopDong"].Style.ForeColor = Color.Orange;
                        else if (trangThai == "Hủy")
                            row.Cells["TrangThaiHopDong"].Style.ForeColor = Color.Red;
                    }
                }

                lblRecordCount.Text = $"Tổng số hợp đồng: {dt?.Rows.Count ?? 0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvHopDong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Click nút Xem
            if (e.ColumnIndex == dgvHopDong.Columns["btnXem"].Index)
            {
                int maGDBan = Convert.ToInt32(dgvHopDong.Rows[e.RowIndex].Cells["MaGDBan"].Value);
                FormXemHopDongMua formXem = new FormXemHopDongMua(maGDBan);
                formXem.ShowDialog();
            }
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = hopDongMuaBLL.GetAllHopDongMua();
                DataView dv = dt.DefaultView;
                string filter = "";

                // Lọc theo từ khóa
                string keyword = txtTimKiem.Text.Trim();
                string timKiemTheo = cboTimKiemTheo.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(keyword) && timKiemTheo != "Tất cả")
                {
                    switch (timKiemTheo)
                    {
                        case "Mã hợp đồng":
                            // MaHDM có thể là chuỗi
                            filter = $"MaHDM LIKE '%{keyword}%'";
                            break;
                        case "Mã giao dịch":
                            // MaGDBan là số nguyên
                            if (int.TryParse(keyword, out int maGD))
                            {
                                filter = $"MaGDBan = {maGD}";
                            }
                            else
                            {
                                filter = "1=0"; // Không tìm thấy
                            }
                            break;
                        case "Khách hàng":
                            filter = $"HoTenKH LIKE '%{keyword}%'";
                            break;
                        case "Số điện thoại":
                            filter = $"Sdt LIKE '%{keyword}%'";
                            break;
                        case "Biển số xe":
                            filter = $"BienSo LIKE '%{keyword}%'";
                            break;
                        case "Tên xe":
                            filter = $"TenXe LIKE '%{keyword}%'";
                            break;
                    }
                }
                else if (!string.IsNullOrEmpty(keyword))
                {
                    // Tìm kiếm tất cả
                    List<string> conditions = new List<string>();
                    
                    // Tìm trong các trường chuỗi
                    conditions.Add($"HoTenKH LIKE '%{keyword}%'");
                    conditions.Add($"Sdt LIKE '%{keyword}%'");
                    conditions.Add($"BienSo LIKE '%{keyword}%'");
                    conditions.Add($"TenXe LIKE '%{keyword}%'");
                    conditions.Add($"MaHDM LIKE '%{keyword}%'");
                    
                    // Thử tìm MaGDBan nếu là số
                    if (int.TryParse(keyword, out int maGD))
                    {
                        conditions.Add($"MaGDBan = {maGD}");
                    }
                    
                    filter = string.Join(" OR ", conditions);
                }

                // Lọc theo ngày
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

                string dateFilter = $"NgayLap >= #{tuNgay:MM/dd/yyyy}# AND NgayLap <= #{denNgay:MM/dd/yyyy}#";
                
                if (!string.IsNullOrEmpty(filter))
                    filter += " AND " + dateFilter;
                else
                    filter = dateFilter;

                // Lọc theo trạng thái
                string trangThai = cboTrangThai.SelectedItem?.ToString();
                if (trangThai != "Tất cả" && !string.IsNullOrEmpty(trangThai))
                {
                    filter += $" AND TrangThaiHopDong = '{trangThai}'";
                }

                dv.RowFilter = filter;
                dgvHopDong.DataSource = dv.ToTable();
                lblRecordCount.Text = $"Tổng số hợp đồng: {dv.Count}";

                if (dv.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy hợp đồng nào phù hợp với điều kiện tìm kiếm!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboTimKiemTheo.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            dtpTuNgay.Value = DateTime.Now.AddMonths(-3);
            dtpDenNgay.Value = DateTime.Now;
            LoadHopDong();
        }

        private void BtnXuatFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvHopDong.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    Title = "Xuất danh sách hợp đồng",
                    FileName = $"DanhSachHopDongMua_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Lấy dữ liệu từ DataGridView
                        DataTable dt = hopDongMuaBLL.GetAllHopDongMua();

                        // Xuất PDF
                        PDFHelper.ExportDanhSachHopDong(dt, saveDialog.FileName);

                        MessageBox.Show(
                            "Xuất file PDF thành công!\n\n" +
                            $"File đã được lưu tại:\n{saveDialog.FileName}",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Mở file PDF
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Lỗi khi xuất file PDF:\n" + ex.Message,
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
