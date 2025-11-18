using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;
using UI.FormHandleUI;

namespace UI.UserControlUI
{
    public partial class ViewQuanLyKhachHang : UserControl
    {
        private KhachHangBLL khachHangBLL;
        private DataGridView dgvKhachHang;

        public ViewQuanLyKhachHang()
        {
            InitializeComponent();
            khachHangBLL = new KhachHangBLL();
            InitializeDataGrid();
            LoadData();

            // Gán sự kiện cho các nút và controls
            this.Load += ViewQuanLyKhachHang_Load;
            btnSearch.Click += BtnSearch_Click;
            btn_AddCustomer.Click += Btn_AddCustomer_Click;
            btn_EditCustomer.Click += Btn_EditCustomer_Click;
            btn_DeleteCustomer.Click += Btn_DeleteCustomer_Click;
            btn_RefreshCustomer.Click += Btn_RefreshCustomer_Click;

            // Xử lý enter trên txtSearch để tìm kiếm
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnSearch_Click(s, e); };

            // Xử lý double-click để sửa
            dgvKhachHang.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0) Btn_EditCustomer_Click(s, e);
            };
        }

        private void InitializeDataGrid()
        {
            // Tạo và cấu hình DataGridView
            dgvKhachHang = new DataGridView
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

            panelDataGrid.Controls.Add(dgvKhachHang);

            // Xử lý sự kiện chọn row để highlight
            dgvKhachHang.SelectionChanged += (s, e) =>
            {
            if (dgvKhachHang.SelectedRows.Count > 0)
            {
                dgvKhachHang.SelectedRows[0].DefaultCellStyle.BackColor = Color.FromArgb(230, 242, 255);
                }
            };
        }

        private void ViewQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = khachHangBLL.GetAllKhachHang();
                dgvKhachHang.DataSource = dt;
                UpdateRecordCount(dt.Rows.Count);

                // Tùy chỉnh hiển thị cột
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu khách hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvKhachHang.Columns.Count == 0) return;

            // Đổi tên cột hiển thị
            if (dgvKhachHang.Columns.Contains("MaKH"))
                dgvKhachHang.Columns["MaKH"].HeaderText = "Mã KH";
            if (dgvKhachHang.Columns.Contains("HoTenKH"))
                dgvKhachHang.Columns["HoTenKH"].HeaderText = "Họ Tên";
            if (dgvKhachHang.Columns.Contains("NgaySinh"))
            {
                dgvKhachHang.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                dgvKhachHang.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvKhachHang.Columns.Contains("GioiTinh"))
                dgvKhachHang.Columns["GioiTinh"].HeaderText = "Giới Tính";
            if (dgvKhachHang.Columns.Contains("Sdt"))
                dgvKhachHang.Columns["Sdt"].HeaderText = "SĐT";
            if (dgvKhachHang.Columns.Contains("Email"))
                dgvKhachHang.Columns["Email"].HeaderText = "Email";
            if (dgvKhachHang.Columns.Contains("DiaChi"))
                dgvKhachHang.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            if (dgvKhachHang.Columns.Contains("NgayTao"))
            {
                dgvKhachHang.Columns["NgayTao"].HeaderText = "Ngày Tạo";
                dgvKhachHang.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvKhachHang.Columns.Contains("NgayCapNhat"))
            {
                dgvKhachHang.Columns["NgayCapNhat"].HeaderText = "Cập Nhật";
                dgvKhachHang.Columns["NgayCapNhat"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            // Set column width
            dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchBy = cboSearchBy.SelectedItem?.ToString() ?? "Mã khách hàng";
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            try
            {
                DataTable dt = khachHangBLL.SearchKhachHang(searchBy, keyword);
                dgvKhachHang.DataSource = dt;
                UpdateRecordCount(dt.Rows.Count);
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_RefreshCustomer_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboSearchBy.SelectedIndex = 0;
            LoadData();
        }

        private void Btn_AddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                // Sử dụng constructor không tham số - chế độ THÊM
                using (FormQuanLyKhachHang form = new FormQuanLyKhachHang())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                        MessageBox.Show("Thêm khách hàng thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm khách hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_EditCustomer_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maKH = dgvKhachHang.SelectedRows[0].Cells["MaKH"].Value.ToString();

                // Sử dụng constructor có tham số - chế độ SỬA
                using (FormQuanLyKhachHang form = new FormQuanLyKhachHang(maKH))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                        MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa khách hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_DeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maKH = dgvKhachHang.SelectedRows[0].Cells["MaKH"].Value.ToString();
            string hoTenKH = dgvKhachHang.SelectedRows[0].Cells["HoTenKH"].Value.ToString();

            var confirmResult = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khách hàng '{hoTenKH}' (Mã: {maKH})?\n\nLưu ý: Thao tác này không thể hoàn tác!",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    string errorMessage;
                    bool success = khachHangBLL.DeleteKhachHang(maKH, out errorMessage);

                    if (success)
                    {
                        MessageBox.Show("Xóa khách hàng thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Lỗi xóa",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa khách hàng: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateRecordCount(int count)
        {
            lblRecordCount.Text = $"Tổng số bản ghi: {count}";
            lblRecordCount.ForeColor = count > 0 ? Color.FromArgb(25, 118, 210) : Color.Gray;
        }
    }
}