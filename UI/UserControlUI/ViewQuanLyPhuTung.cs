using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.UserControlUI
{
    public partial class ViewQuanLyPhuTung : UserControl
    {
        private PhuTungBLL phuTungBLL = new PhuTungBLL();
        private LanguageManagerBLL langMgr = LanguageManagerBLL.Instance;
        public ViewQuanLyPhuTung()
        {
            InitializeComponent();
            SetupDataGridView();
            InitializeTimKiemComboBox();
            LoadData();

            langMgr.LanguageChanged += (s, e) => { ApplyLanguage(); LoadData(); };
            ApplyLanguage();
            
            // Áp dụng phân quyền cho nút Thêm/Sửa/Xóa
            ApplyPermissions();
        }

        /// <summary>
        /// Áp dụng phân quyền cho các nút thao tác
        /// Chỉ Quản lý: Thêm/Sửa/Xóa
        /// Bán hàng, Kỹ thuật: Chỉ xem
        /// Kỹ thuật: Không được bán phụ tùng
        /// </summary>
        private void ApplyPermissions()
        {
            bool canEdit = PermissionManager.CanEditSanPham(); // Chỉ Quản lý
            bool isKyThuat = PermissionManager.IsKyThuat();
            
            btnThem.Visible = canEdit;
            btnSua.Visible = canEdit;
            btnXoa.Visible = canEdit;
            btnLamMoi.Visible = canEdit;
            
            // Kỹ thuật không được bán phụ tùng
            btnBan.Visible = !isKyThuat;
            
            ReorganizeButtons();
        }

        /// <summary>
        /// Tự động dồn các button sang trái khi một số button bị ẩn
        /// </summary>
        private void ReorganizeButtons()
        {
            List<System.Windows.Forms.Button> buttons = new List<System.Windows.Forms.Button> { btnThem, btnSua, btnXoa, btnLamMoi };
            int currentX = 66; // Vị trí X ban đầu
            int spacing = 160; // Khoảng cách giữa các button
            int y = 17; // Vị trí Y cố định

            foreach (System.Windows.Forms.Button btn in buttons)
            {
                if (btn.Visible)
                {
                    btn.Location = new Point(currentX, y);
                    currentX += spacing;
                }
            }        }

        private void ApplyLanguage()
        {
            btnThem.Text = langMgr.GetString("AddBtn");
            btnSua.Text = langMgr.GetString("EditBtn");
            btnXoa.Text = langMgr.GetString("DeleteBtn");
            btnLamMoi.Text = langMgr.GetString("RefreshBtn");
            btnTimKiem.Text = langMgr.GetString("SearchBtn");
            btnBan.Text = "Bán";
            lblTimKiemTheo.Text = langMgr.GetString("SearchBy");
            
            lblTuKhoa.Text = langMgr.GetString("Keyword");
        }
        private void InitializeTimKiemComboBox()
        {
            cbbTimKiemTheo.Items.Clear();
            cbbTimKiemTheo.Items.Add("Tất cả");
            cbbTimKiemTheo.Items.Add("Mã phụ tùng");
            cbbTimKiemTheo.Items.Add("Tên phụ tùng");
            cbbTimKiemTheo.Items.Add("Hãng xe");
            cbbTimKiemTheo.Items.Add("Dòng xe");
            cbbTimKiemTheo.SelectedIndex = 0;
        }
        private void SetupDataGridView()
        {
            dgvQuanLyPhuTung.AutoGenerateColumns = false;
            dgvQuanLyPhuTung.Columns.Clear();

            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaPhuTung",
                DataPropertyName = "MaPhuTung",
                HeaderText = "Mã phụ tùng",
                Width = 80
            });

            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenPhuTung",
                DataPropertyName = "TenPhuTung",
                HeaderText = "Tên phụ tùng",
                Width = 160
            });

            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenHang",
                DataPropertyName = "TenHang",
                HeaderText = "Hãng xe",
                Width = 80
            });

            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenDong",
                DataPropertyName = "TenDong",
                HeaderText = "Dòng xe",
                Width = 110
            });

            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colGiaMua",
                DataPropertyName = "GiaMua",
                HeaderText = "Giá mua",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colGiaBan",
                DataPropertyName = "GiaBan",
                HeaderText = "Giá bán",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDonViTinh",
                DataPropertyName = "DonViTinh",
                HeaderText = "Đơn vị",
                Width = 75
            });

            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoLuongTon",
                DataPropertyName = "SoLuongTon",
                HeaderText = "Tồn kho",
                Width = 70
            });

            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colGhiChu",
                DataPropertyName = "GhiChu",
                HeaderText = "Ghi chú",
                Width = 120
            });
        }

        private void LoadData()
        {
            dgvQuanLyPhuTung.DataSource = phuTungBLL.GetAllPhuTungWithTonKho();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbbTimKiemTheo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtTuKhoa_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTuKhoa.Text.Trim();
            string searchType = cbbTimKiemTheo.SelectedItem?.ToString() ?? "Tất cả";
            if (string.IsNullOrWhiteSpace(keyword) && (searchType == "Tất cả" || string.IsNullOrWhiteSpace(searchType)))
            {
                LoadData();
                return;
            }
            dgvQuanLyPhuTung.DataSource = phuTungBLL.SearchPhuTung(keyword, searchType);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            UI.FormHandleUI.FormThemPhuTung frmThemPhuTung = new UI.FormHandleUI.FormThemPhuTung();
            var result = frmThemPhuTung.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvQuanLyPhuTung.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phụ tùng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maPT = dgvQuanLyPhuTung.SelectedRows[0].Cells["colMaPhuTung"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(maPT))
            {
                MessageBox.Show("Không lấy được mã phụ tùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var frm = new UI.FormHandleUI.FormSuaPhuTung(maPT);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData(); 
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvQuanLyPhuTung.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phụ tùng cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Có thể chọn cách lấy cột: bằng name hoặc theo index
            var cell = dgvQuanLyPhuTung.SelectedRows[0].Cells["colMaPhuTung"];
            if (cell == null || cell.Value == null)
            {
                MessageBox.Show("Không thể xác định được mã phụ tùng!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string maPT = cell.Value.ToString();

            DialogResult dr =
                MessageBox.Show($"Bạn có chắc chắn muốn xóa phụ tùng {maPT}?",
                                "Xác nhận",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            try
            {
                if (phuTungBLL.DeletePhuTung(maPT))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Text = string.Empty;
            if (cbbTimKiemTheo.Items.Count > 0) cbbTimKiemTheo.SelectedIndex = 0;
            LoadData();
        }

        private void dgvQuanLyPhuTung_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            // Kiểm tra có chọn dòng nào không
            if (dgvQuanLyPhuTung.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phụ tùng cần bán!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dgvQuanLyPhuTung.SelectedRows[0];

            string maPT = row.Cells["colMaPhuTung"].Value?.ToString();
            string tenPT = row.Cells["colTenPhuTung"].Value?.ToString() ?? "Không xác định";
            int tonKhoHienTai = Convert.ToInt32(row.Cells["colSoLuongTon"].Value ?? 0);
            decimal giaBan = Convert.ToDecimal(row.Cells["colGiaBan"].Value ?? 0);
            string donVi = row.Cells["colDonViTinh"].Value?.ToString() ?? "cái";

            if (string.IsNullOrWhiteSpace(maPT))
            {
                MessageBox.Show("Không xác định được mã phụ tùng!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tonKhoHienTai <= 0)
            {
                MessageBox.Show($"Phụ tùng [{maPT}] - {tenPT} hiện đang hết hàng (tồn kho: 0)!",
                    "Hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // Mở form nhập số lượng bán
            using (var frm = new FormHandleUI.FormBanPhuTung(maPT, tenPT, tonKhoHienTai, giaBan))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();

                    // Optional: Thông báo thành công (bỏ DonVi vì chưa có)
                    MessageBox.Show($"Đã bán thành công {frm.SoLuongBan} phụ tùng {tenPT}",
                        "Bán hàng thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
