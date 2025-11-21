using BLL;
using DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using UI.FormHandleUI;
using UI.FormUI;

namespace UI.UserControlUI
{
    public partial class ViewQuanLyKhachHang : UserControl
    {
        private KhachHangBLL khachHangBLL;
        private DataGridView dgvKhachHang;
        private LanguageManagerBLL langMgr = LanguageManagerBLL.Instance;


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

            // Đăng ký cập nhật ngôn ngữ động
            langMgr.LanguageChanged += (s, e) => { ApplyLanguage(); LoadData(); };
            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            lblTitle.Text = langMgr.GetString("CustomerTitle");
            lblSearchBy.Text = langMgr.GetString("SearchBy");
            lblSearch.Text = langMgr.GetString("Keyword");
            btnSearch.Text = langMgr.GetString("SearchBtn");
            btn_AddCustomer.Text = langMgr.GetString("AddBtn");
            btn_EditCustomer.Text = langMgr.GetString("EditBtn");
            btn_DeleteCustomer.Text = langMgr.GetString("DeleteBtn");
            btn_RefreshCustomer.Text = langMgr.GetString("RefreshBtn");

            // Cập nhật combobox tìm kiếm (cập nhật giá trị phù hợp cấu hình combobox)
            if (cboSearchBy.Items.Count > 0)
            {
                cboSearchBy.Items[0] = langMgr.GetString("CustomerID");
                cboSearchBy.Items[1] = langMgr.GetString("FullName");
                cboSearchBy.Items[2] = langMgr.GetString("Phone");
                // ... nếu còn ...
            }
            UpdateRecordCount(dgvKhachHang.RowCount);
            FormatDataGridView();
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

            if (dgvKhachHang.Columns.Contains("MaKH"))
                dgvKhachHang.Columns["MaKH"].HeaderText = langMgr.GetString("CustomerID");
            if (dgvKhachHang.Columns.Contains("HoTenKH"))
                dgvKhachHang.Columns["HoTenKH"].HeaderText = langMgr.GetString("FullName");
            if (dgvKhachHang.Columns.Contains("NgaySinh"))
            {
                dgvKhachHang.Columns["NgaySinh"].HeaderText = langMgr.GetString("BirthDate");
                dgvKhachHang.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvKhachHang.Columns.Contains("GioiTinh"))
                dgvKhachHang.Columns["GioiTinh"].HeaderText = langMgr.GetString("Gender");
            if (dgvKhachHang.Columns.Contains("Sdt"))
                dgvKhachHang.Columns["Sdt"].HeaderText = langMgr.GetString("Phone");
            if (dgvKhachHang.Columns.Contains("Email"))
                dgvKhachHang.Columns["Email"].HeaderText = langMgr.GetString("Email");
            if (dgvKhachHang.Columns.Contains("DiaChi"))
                dgvKhachHang.Columns["DiaChi"].HeaderText = langMgr.GetString("Address");
            if (dgvKhachHang.Columns.Contains("NgayTao"))
            {
                dgvKhachHang.Columns["NgayTao"].HeaderText = langMgr.GetString("CreatedDate");
                dgvKhachHang.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvKhachHang.Columns.Contains("NgayCapNhat"))
            {
                dgvKhachHang.Columns["NgayCapNhat"].HeaderText = langMgr.GetString("UpdatedDate");
                dgvKhachHang.Columns["NgayCapNhat"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
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
                // DÙNG FORM MỚI: FormThemKhachHang (không tham số = chế độ thêm mới)
                using (var form = new FormThemKhachHang())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData(); // Refresh danh sách sau khi thêm thành công
                        MessageBox.Show("Thêm khách hàng thành công!", "Thành công",
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

        // === THAY ĐỔI HÀM NÀY ===
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

                // Lấy dữ liệu khách hàng hiện tại từ database để truyền vào form
                DataTable dt = khachHangBLL.GetKhachHangByMaKH(maKH);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow row = dt.Rows[0];

                KhachHangDTO kh = new KhachHangDTO
                {
                    MaKH = row["MaKH"].ToString(),
                    HoTenKH = row["HoTenKH"].ToString(),
                    NgaySinh = row["NgaySinh"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NgaySinh"]),
                    GioiTinh = row["GioiTinh"].ToString(),
                    Sdt = row["Sdt"].ToString(),
                    Email = row["Email"] == DBNull.Value ? null : row["Email"].ToString(),
                    DiaChi = row["DiaChi"] == DBNull.Value ? null : row["DiaChi"].ToString(),
                    SoCCCD = row["SoCCCD"] == DBNull.Value ? null : row["SoCCCD"].ToString(),
                    LoaiGiayTo = row["LoaiGiayTo"] == DBNull.Value ? null : row["LoaiGiayTo"].ToString(),
                    AnhGiayTo = row["AnhGiayTo"] == DBNull.Value ? null : (byte[])row["AnhGiayTo"]
                };

                // DÙNG FORM MỚI: truyền vào đối tượng KhachHangDTO = chế độ sửa
                using (var form = new FormThemKhachHang(kh))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData(); // Refresh lại danh sách
                        MessageBox.Show("Cập nhật khách hàng thành công!", "Thành công",
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

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblSearchBy_Click(object sender, EventArgs e)
        {

        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }

        private void btn_AddCustomer_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void panelSearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelDataGrid_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}