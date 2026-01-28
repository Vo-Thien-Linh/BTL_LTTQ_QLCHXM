using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace UI.UserControlUI
{
    public partial class ViewQuanLyKhuyenMai : UserControl
    {
        private KhuyenMaiBLL khuyenMaiBLL;
        private LanguageManagerBLL langMgr = LanguageManagerBLL.Instance;
        private string currentMaKM = "";

        public ViewQuanLyKhuyenMai()
        {
            InitializeComponent();
            khuyenMaiBLL = new KhuyenMaiBLL();

            langMgr.LanguageChanged += (s, e) => ApplyLanguage();

            // Đăng ký sự kiện
            this.Load += ViewQuanLyKhuyenMai_Load;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            btnTimKiem.Click += BtnTimKiem_Click;
            dgvQuanLyKhuyenMai.SelectionChanged += DgvQuanLyKhuyenMai_SelectionChanged;
            
            // Áp dụng phân quyền
            ApplyPermissions();

            ApplyLanguage();
        }

        /// <summary>
        /// Áp dụng ngôn ngữ cho các control
        /// </summary>
        private void ApplyLanguage()
        {
            // Các nút
            btnThem.Text = langMgr.GetString("AddBtn");
            btnSua.Text = langMgr.GetString("EditBtn");
            btnXoa.Text = langMgr.GetString("DeleteBtn");
            btnLamMoi.Text = langMgr.GetString("RefreshBtn");
            btnTimKiem.Text = langMgr.GetString("SearchBtn");

            

            // ComboBox tìm kiếm
            LoadComboBoxTimKiem();

            // Cập nhật header của DataGridView
            UpdateDataGridViewHeaders();
        }

        /// <summary>
        /// Cập nhật header của DataGridView theo ngôn ngữ
        /// </summary>
        private void UpdateDataGridViewHeaders()
        {
            if (dgvQuanLyKhuyenMai.Columns.Count > 0)
            {
                dgvQuanLyKhuyenMai.Columns["MaKM"].HeaderText = langMgr.GetString("PromotionCode");
                dgvQuanLyKhuyenMai.Columns["TenKM"].HeaderText = langMgr.GetString("PromotionName");
                dgvQuanLyKhuyenMai.Columns["MoTa"].HeaderText = langMgr.GetString("Description");
                dgvQuanLyKhuyenMai.Columns["NgayBatDau"].HeaderText = langMgr.GetString("StartDate");
                dgvQuanLyKhuyenMai.Columns["NgayKetThuc"].HeaderText = langMgr.GetString("EndDate");
                dgvQuanLyKhuyenMai.Columns["LoaiKhuyenMai"].HeaderText = langMgr.GetString("PromotionType");
                dgvQuanLyKhuyenMai.Columns["PhanTramGiam"].HeaderText = langMgr.GetString("DiscountPercent");
                dgvQuanLyKhuyenMai.Columns["SoTienGiam"].HeaderText = langMgr.GetString("DiscountAmount");
                dgvQuanLyKhuyenMai.Columns["GiaTriGiamToiDa"].HeaderText = langMgr.GetString("MaxDiscount");
                dgvQuanLyKhuyenMai.Columns["LoaiApDung"].HeaderText = langMgr.GetString("ApplyTo");
                dgvQuanLyKhuyenMai.Columns["TrangThai"].HeaderText = langMgr.GetString("Status");
            }
        }

        /// <summary>
        /// Áp dụng phân quyền - chỉ Admin mới có quyền thêm/sửa/xóa/làm mới khuyến mãi
        /// </summary>
        private void ApplyPermissions()
        {
            bool isAdmin = PermissionManager.IsAdmin();
            
            // Chỉ Admin mới thấy và sử dụng các nút thêm/sửa/xóa/làm mới
            btnThem.Visible = isAdmin;
            btnSua.Visible = isAdmin;
            btnXoa.Visible = isAdmin;
            btnLamMoi.Visible = isAdmin;
        }

        private void ViewQuanLyKhuyenMai_Load(object sender, EventArgs e)
        {
            try
            {
                // Tự động cập nhật trạng thái khuyến mãi hết hạn
                int updatedCount = khuyenMaiBLL.CapNhatTrangThaiKhuyenMaiHetHan();
                if (updatedCount > 0)
                {
                    MessageBox.Show(
                        string.Format(langMgr.GetString("AutoUpdatedExpiredPromotions"), updatedCount),
                        langMgr.GetString("Notification"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                LoadComboBoxTimKiem();
                LoadDanhSachKhuyenMai();
                SetupDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    langMgr.GetString("LoadDataError") + ": " + ex.Message,
                    langMgr.GetString("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #region Setup UI

        private void SetupDataGridView()
        {
            dgvQuanLyKhuyenMai.AutoGenerateColumns = false;
            dgvQuanLyKhuyenMai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvQuanLyKhuyenMai.MultiSelect = false;
            dgvQuanLyKhuyenMai.AllowUserToAddRows = false;
            dgvQuanLyKhuyenMai.AllowUserToDeleteRows = false;
            dgvQuanLyKhuyenMai.ReadOnly = true;
            dgvQuanLyKhuyenMai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Clear columns nếu đã có
            dgvQuanLyKhuyenMai.Columns.Clear();

            // Thêm các cột
            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaKM",
                DataPropertyName = "MaKM",
                HeaderText = langMgr.GetString("PromotionCode"),
                Width = 100
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenKM",
                DataPropertyName = "TenKM",
                HeaderText = langMgr.GetString("PromotionName"),
                Width = 250
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MoTa",
                DataPropertyName = "MoTa",
                HeaderText = langMgr.GetString("Description"),
                Width = 200
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayBatDau",
                DataPropertyName = "NgayBatDau",
                HeaderText = langMgr.GetString("StartDate"),
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayKetThuc",
                DataPropertyName = "NgayKetThuc",
                HeaderText = langMgr.GetString("EndDate"),
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoaiKhuyenMai",
                DataPropertyName = "LoaiKhuyenMai",
                HeaderText = langMgr.GetString("PromotionType"),
                Width = 100
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PhanTramGiam",
                DataPropertyName = "PhanTramGiam",
                HeaderText = langMgr.GetString("DiscountPercent"),
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoTienGiam",
                DataPropertyName = "SoTienGiam",
                HeaderText = langMgr.GetString("DiscountAmount"),
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GiaTriGiamToiDa",
                DataPropertyName = "GiaTriGiamToiDa",
                HeaderText = langMgr.GetString("MaxDiscount"),
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoaiApDung",
                DataPropertyName = "LoaiApDung",
                HeaderText = langMgr.GetString("ApplyTo"),
                Width = 100
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TrangThai",
                DataPropertyName = "TrangThai",
                HeaderText = langMgr.GetString("Status"),
                Width = 100
            });

            // Màu sắc cho trạng thái
            dgvQuanLyKhuyenMai.CellFormatting += DgvQuanLyKhuyenMai_CellFormatting;
        }

        private void DgvQuanLyKhuyenMai_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvQuanLyKhuyenMai.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string trangThai = e.Value.ToString();
                switch (trangThai)
                {
                    case "Hoạt động":
                        e.CellStyle.BackColor = Color.LightGreen;
                        e.CellStyle.ForeColor = Color.DarkGreen;
                        break;
                    case "Tạm dừng":
                        e.CellStyle.BackColor = Color.LightYellow;
                        e.CellStyle.ForeColor = Color.DarkOrange;
                        break;
                    case "Hết hạn":
                        e.CellStyle.BackColor = Color.LightGray;
                        e.CellStyle.ForeColor = Color.DarkGray;
                        break;
                    case "Hủy":
                        e.CellStyle.BackColor = Color.LightCoral;
                        e.CellStyle.ForeColor = Color.DarkRed;
                        break;
                }
            }
        }

        private void LoadComboBoxTimKiem()
        {
            cbbTimKiemTheo.Items.Clear();
            cbbTimKiemTheo.Items.Add(langMgr.GetString("All"));
            cbbTimKiemTheo.Items.Add(langMgr.GetString("PromotionCode"));
            cbbTimKiemTheo.Items.Add(langMgr.GetString("PromotionName"));
            cbbTimKiemTheo.Items.Add(langMgr.GetString("Status"));
            cbbTimKiemTheo.SelectedIndex = 0;
        }

        #endregion

        #region Load Data

        private void LoadDanhSachKhuyenMai(string timKiem = "")
        {
            try
            {
                DataTable dt = khuyenMaiBLL.GetAllKhuyenMai();

                // Tìm kiếm nếu có
                if (!string.IsNullOrWhiteSpace(timKiem))
                {
                    string searchText = timKiem.Trim().Replace("'", "''");
                    string loaiTimKiem = cbbTimKiemTheo.SelectedItem?.ToString() ?? langMgr.GetString("All");

                    if (loaiTimKiem == langMgr.GetString("PromotionCode"))
                    {
                        dt.DefaultView.RowFilter = $"MaKM LIKE '%{searchText}%'";
                    }
                    else if (loaiTimKiem == langMgr.GetString("PromotionName"))
                    {
                        dt.DefaultView.RowFilter = $"TenKM LIKE '%{searchText}%'";
                    }
                    else if (loaiTimKiem == langMgr.GetString("Status"))
                    {
                        dt.DefaultView.RowFilter = $"TrangThai LIKE '%{searchText}%'";
                    }
                    else // Tất cả
                    {
                        dt.DefaultView.RowFilter = $"MaKM LIKE '%{searchText}%' OR TenKM LIKE '%{searchText}%' OR TrangThai LIKE '%{searchText}%'";
                    }

                    dt = dt.DefaultView.ToTable();
                }

                dgvQuanLyKhuyenMai.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    langMgr.GetString("LoadPromotionListError") + ": " + ex.Message,
                    langMgr.GetString("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion

        #region CRUD Operations

        private void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                FormThemSuaKhuyenMai frm = new FormThemSuaKhuyenMai(null);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhSachKhuyenMai();
                    MessageBox.Show(
                        langMgr.GetString("AddPromotionSuccess"),
                        langMgr.GetString("Notification"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    langMgr.GetString("Error") + ": " + ex.Message,
                    langMgr.GetString("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(currentMaKM))
                {
                    MessageBox.Show(
                        langMgr.GetString("PleaseSelectPromotionToEdit"),
                        langMgr.GetString("Notification"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                FormThemSuaKhuyenMai frm = new FormThemSuaKhuyenMai(currentMaKM);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhSachKhuyenMai();
                    MessageBox.Show(
                        langMgr.GetString("UpdatePromotionSuccess"),
                        langMgr.GetString("Notification"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    langMgr.GetString("Error") + ": " + ex.Message,
                    langMgr.GetString("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(currentMaKM))
                {
                    MessageBox.Show(
                        langMgr.GetString("PleaseSelectPromotionToDelete"),
                        langMgr.GetString("Notification"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    string.Format(langMgr.GetString("ConfirmDeletePromotion"), currentMaKM),
                    langMgr.GetString("ConfirmDelete"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string errorMessage;
                    if (khuyenMaiBLL.DeleteKhuyenMai(currentMaKM, out errorMessage))
                    {
                        LoadDanhSachKhuyenMai();
                        currentMaKM = "";
                        MessageBox.Show(
                            langMgr.GetString("DeletePromotionSuccess"),
                            langMgr.GetString("Notification"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            langMgr.GetString("DeletePromotionFailed") + "\n" + errorMessage,
                            langMgr.GetString("Error"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    langMgr.GetString("Error") + ": " + ex.Message,
                    langMgr.GetString("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                // Reset các trường tìm kiếm
                txtTuKhoa.Text = "";
                cbbTimKiemTheo.SelectedIndex = 0;
                currentMaKM = "";

                // Xóa DataSource hiện tại
                dgvQuanLyKhuyenMai.DataSource = null;

                // Tự động cập nhật trạng thái khuyến mãi hết hạn
                int updatedCount = khuyenMaiBLL.CapNhatTrangThaiKhuyenMaiHetHan();
                if (updatedCount > 0)
                {
                    MessageBox.Show(
                        string.Format(langMgr.GetString("AutoUpdatedExpiredPromotions"), updatedCount),
                        langMgr.GetString("Notification"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                // Load lại danh sách
                LoadDanhSachKhuyenMai();

                MessageBox.Show(
                    langMgr.GetString("RefreshDataSuccess"),
                    langMgr.GetString("Notification"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    langMgr.GetString("RefreshDataError") + ": " + ex.Message,
                    langMgr.GetString("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            LoadDanhSachKhuyenMai(txtTuKhoa.Text);
        }

        #endregion

        #region Event Handlers

        private void DgvQuanLyKhuyenMai_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvQuanLyKhuyenMai.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvQuanLyKhuyenMai.SelectedRows[0];
                currentMaKM = row.Cells["MaKM"].Value?.ToString() ?? "";
            }
        }

        private void txtTuKhoa_TextChanged(object sender, EventArgs e)
        {
            // Tìm kiếm realtime khi gõ
            LoadDanhSachKhuyenMai(txtTuKhoa.Text);
        }

        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
