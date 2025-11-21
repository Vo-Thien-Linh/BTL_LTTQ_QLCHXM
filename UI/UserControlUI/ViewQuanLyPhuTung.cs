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
        }

        private void ApplyLanguage()
        {
            btnThem.Text = langMgr.GetString("AddBtn");
            btnSua.Text = langMgr.GetString("EditBtn");
            btnXoa.Text = langMgr.GetString("DeleteBtn");
            btnLamMoi.Text = langMgr.GetString("RefreshBtn");
            btnTimKiem.Text = langMgr.GetString("SearchBtn");

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
                DataPropertyName = "MaPhuTung",
                Name = "colMaPhuTung",
                HeaderText = "Mã phụ tùng",
                Width = 80
            });
            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenPhuTung", HeaderText = "Tên phụ tùng", Width = 160 });
            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenHang", HeaderText = "Hãng xe", Width = 80 });
            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenDong", HeaderText = "Dòng xe", Width = 110 });
            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GiaMua", HeaderText = "Giá mua", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GiaBan", HeaderText = "Giá bán", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonViTinh", HeaderText = "Đơn vị", Width = 75 });
            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuongTon", HeaderText = "Tồn kho", Width = 70 });
            dgvQuanLyPhuTung.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GhiChu", HeaderText = "Ghi chú", Width = 120 });
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
    }
}
