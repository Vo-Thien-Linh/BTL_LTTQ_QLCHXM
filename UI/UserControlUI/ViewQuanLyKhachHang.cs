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

        /// <summary>
        /// Kiểm tra quyền truy cập
        /// </summary>
        private bool CheckPermission(string action)
        {
            try
            {
                if (!SessionManager.IsLoggedIn)
                {
                    MessageBox.Show("Bạn chưa đăng nhập!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (SessionManager.IsSessionExpired())
                {
                    MessageBox.Show("Phiên làm việc đã hết hạn!\nVui lòng đăng nhập lại.",
                        "Hết phiên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SessionManager.Logout();
                    return false;
                }

                SessionManager.UpdateActivity();

                if (!SessionManager.HasPermission("KhachHang", action))
                {
                    MessageBox.Show(
                        $"Bạn không có quyền {action} khách hàng!\n" +
                        $"Chức vụ: {SessionManager.CurrentUser?.ChucVu}",
                        "Không đủ quyền",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kiểm tra quyền: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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
                Cursor = Cursors.WaitCursor;
                
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
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void FormatDataGridView()
        {
            if (dgvKhachHang.Columns.Count == 0) return;

            // Ẩn các cột nhạy cảm
            if (dgvKhachHang.Columns.Contains("SoCCCD"))
                dgvKhachHang.Columns["SoCCCD"].Visible = false;
            if (dgvKhachHang.Columns.Contains("LoaiGiayTo"))
                dgvKhachHang.Columns["LoaiGiayTo"].Visible = false;
            if (dgvKhachHang.Columns.Contains("AnhGiayTo"))
                dgvKhachHang.Columns["AnhGiayTo"].Visible = false;

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
                Cursor = Cursors.WaitCursor;
                
                DataTable dt = khachHangBLL.SearchKhachHang(searchBy, keyword);
                dgvKhachHang.DataSource = dt;
                UpdateRecordCount(dt.Rows.Count);
                FormatDataGridView();
                
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        $"Không tìm thấy khách hàng với từ khóa '{keyword}'",
                        "Kết quả tìm kiếm",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
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
            if (!CheckPermission("ADD"))
                return;

            try
            {
                // Sử dụng constructor không tham số - chế độ THÊM
                using (FormQuanLyKhachHang form = new FormQuanLyKhachHang())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
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
            if (!CheckPermission("EDIT"))
                return;

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
            if (!CheckPermission("DELETE"))
                return;

            if (dgvKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maKH = dgvKhachHang.SelectedRows[0].Cells["MaKH"].Value.ToString();
                string hoTenKH = dgvKhachHang.SelectedRows[0].Cells["HoTenKH"].Value.ToString();

                // Kiểm tra ràng buộc nghiệp vụ
                string errorMessage;
                if (!khachHangBLL.CanDeleteKhachHang(maKH, out errorMessage))
                {
                    MessageBox.Show(
                        $"Không thể xóa khách hàng!\n\n{errorMessage}",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Xác nhận với người dùng
                var confirmResult = MessageBox.Show(
                    $"⚠ XÁC NHẬN XÓA KHÁCH HÀNG\n\n" +
                    $"Họ tên: {hoTenKH}\n" +
                    $"Mã KH: {maKH}\n\n" +
                    (!string.IsNullOrEmpty(errorMessage) ? errorMessage + "\n\n" : "") +
                    $"Bạn có chắc chắn muốn xóa?\n" +
                    $"Thao tác này KHÔNG THỂ HOÀN TÁC!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2
                );

                if (confirmResult == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;
                    
                    bool success = khachHangBLL.DeleteKhachHang(maKH, out errorMessage);

                    if (success)
                    {
                        MessageBox.Show(
                            $"Xóa khách hàng '{hoTenKH}' thành công!",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(
                            $"Lỗi xóa khách hàng:\n{errorMessage}",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi không mong muốn:\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                Cursor = Cursors.Default;
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