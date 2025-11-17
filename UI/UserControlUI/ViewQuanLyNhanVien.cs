using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;
using UI.FormUI;

namespace UI.UserControlUI
{
    public partial class ViewQuanLyNhanVien : UserControl
    {
        private NhanVienBLL nhanVienBLL;
        private DataGridView dgvNhanVien;

        public ViewQuanLyNhanVien()
        {
            InitializeComponent();
            nhanVienBLL = new NhanVienBLL();
            InitializeDataGrid();
            LoadData();

            // Gán sự kiện cho các nút và controls
            this.Load += ViewQuanLyNhanVien_Load; // Để đảm bảo load dữ liệu khi control được load
            btnSearch.Click += BtnSearch_Click;
            btn_AddEmployee.Click += Btn_AddEmployee_Click;
            btn_EditEmployee.Click += Btn_EditEmployee_Click;
            btn_DeleteEmployee.Click += Btn_DeleteEmployee_Click;
            btn_RefreshEmployee.Click += Btn_RefreshEmployee_Click;

            // Tùy chọn: Xử lý enter trên txtSearch để tìm kiếm
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnSearch_Click(s, e); };
        }

        private void InitializeDataGrid()
        {
            // Tạo và cấu hình DataGridView
            dgvNhanVien = new DataGridView
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


            panelDataGrid.Controls.Add(dgvNhanVien);

            // Xử lý sự kiện chọn row để highlight
            dgvNhanVien.SelectionChanged += (s, e) =>
            {
                if (dgvNhanVien.SelectedRows.Count > 0)
                {
                    dgvNhanVien.SelectedRows[0].DefaultCellStyle.BackColor = Color.FromArgb(230, 242, 255);
                }
            };
        }

        private void ViewQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = nhanVienBLL.GetAllNhanVien();
                dgvNhanVien.DataSource = dt;
                UpdateRecordCount(dt.Rows.Count);

                // Ẩn cột không cần thiết nếu có (ví dụ: AnhNhanVien nếu là byte[])
                if (dgvNhanVien.Columns.Contains("AnhNhanVien"))
                {
                    dgvNhanVien.Columns["AnhNhanVien"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchBy = cboSearchBy.SelectedItem?.ToString() ?? "Mã nhân viên";
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            try
            {
                DataTable dt = nhanVienBLL.SearchNhanVien(searchBy, keyword);
                dgvNhanVien.DataSource = dt;
                UpdateRecordCount(dt.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_RefreshEmployee_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboSearchBy.SelectedIndex = 0;
            LoadData();
        }

        private void Btn_AddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormQuanLiNV form = new FormQuanLiNV())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                        MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_EditEmployee_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maNV = dgvNhanVien.SelectedRows[0].Cells["MaNV"].Value.ToString();
                using (FormQuanLiNV form = new FormQuanLiNV(maNV))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                        MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_DeleteEmployee_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maNV = dgvNhanVien.SelectedRows[0].Cells["MaNV"].Value.ToString();
            string hoTenNV = dgvNhanVien.SelectedRows[0].Cells["HoTenNV"].Value.ToString();

            var confirmResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên '{hoTenNV}' (Mã: {maNV})?\n\nLưu ý: Thao tác này không thể hoàn tác!",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    string errorMessage;
                    bool success = nhanVienBLL.DeleteNhanVien(maNV, out errorMessage);

                    if (success)
                    {
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btn_AddEmployee_Click_1(object sender, EventArgs e)
        {

        }
    }
}