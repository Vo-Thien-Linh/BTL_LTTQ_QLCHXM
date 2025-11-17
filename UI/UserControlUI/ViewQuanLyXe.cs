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
using UI.FormHandleUI;

namespace UI.UserControlUI
{
    public partial class ViewQuanLyXe : UserControl
    {
        private XeMayBLL xeMayBLL = new XeMayBLL();
        private string currentPlaceholder = "Nhập từ khóa tìm kiếm...";
        public ViewQuanLyXe()
        {
            InitializeComponent();
            InitializeComboBox();
            SetupDataGridView();
            InitializeTimKiemTheoComboBox();
            LoadData();

        }
        // Thiết lập ComboBox
        private void InitializeComboBox()
        {
            cbbTrangThai.Items.Clear();
            cbbTrangThai.Items.Add("Tất cả");
            cbbTrangThai.Items.Add("Sẵn sàng");
            cbbTrangThai.Items.Add("Đang thuê");
            cbbTrangThai.Items.Add("Đã bán");
            cbbTrangThai.Items.Add("Đang bảo trì");
            cbbTrangThai.SelectedIndex = 0;
        }

        // Thiết lập ComboBox Tìm Kiếm Theo
        private void InitializeTimKiemTheoComboBox()
        {
            cbbTimKiemTheo.Items.Clear();
            cbbTimKiemTheo.Items.Add("Tất cả");
            cbbTimKiemTheo.Items.Add("Mã xe");
            cbbTimKiemTheo.Items.Add("Biển số");
            cbbTimKiemTheo.Items.Add("Hãng xe");
            cbbTimKiemTheo.Items.Add("Dòng xe");
            cbbTimKiemTheo.SelectedIndex = 0; // Mặc định là "Tất cả"

            
        }

        // Thiết lập DataGridView
        private void SetupDataGridView()
        {
            dgvQuanLyXe.AutoGenerateColumns = false;
            dgvQuanLyXe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvQuanLyXe.MultiSelect = false;
            dgvQuanLyXe.Columns.Clear();

            dgvQuanLyXe.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ID_Xe",
                HeaderText = "Mã Xe",
                Name = "colMaXe",
                Width = 80
            });

            dgvQuanLyXe.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BienSo",
                HeaderText = "Biển Số",
                Name = "colBienSo",
                Width = 100
            });

            dgvQuanLyXe.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenHang",
                HeaderText = "Hãng Xe",
                Name = "colHangXe",
                Width = 100
            });

            dgvQuanLyXe.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenDong",
                HeaderText = "Dòng Xe",
                Name = "colDongXe",
                Width = 120
            });

            dgvQuanLyXe.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenMau",
                HeaderText = "Màu Sắc",
                Name = "colMauSac",
                Width = 100
            });

            dgvQuanLyXe.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NamSX",
                HeaderText = "Năm SX",
                Name = "colNamSX",
                Width = 80
            });

            dgvQuanLyXe.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GiaMua",
                HeaderText = "Giá Mua",
                Name = "colGiaMua",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvQuanLyXe.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "KmDaChay",
                HeaderText = "Km Đã Chạy",
                Name = "colKmDaChay",
                Width = 100
            });

            dgvQuanLyXe.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TrangThai",
                HeaderText = "Trạng Thái",
                Name = "colTrangThai",
                Width = 120
            });
        }

        // Load dữ liệu
        private void LoadData(string searchKeyword = null, string trangThai = null)
        {
            try
            {
                DataTable dt;

                if (string.IsNullOrEmpty(searchKeyword) &&
                    (string.IsNullOrEmpty(trangThai) || trangThai == "Tất cả"))
                {
                    dt = xeMayBLL.GetAllXeMay();
                }
                else
                {
                    dt = xeMayBLL.SearchXeMay(searchKeyword, trangThai);
                }

                dgvQuanLyXe.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtTuKhoa.Text.Trim();
            string trangThai = cbbTrangThai.SelectedItem?.ToString();
            LoadData(searchKeyword, trangThai);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Clear();
            cbbTrangThai.SelectedIndex = 0;
            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvQuanLyXe.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn xe cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string idXe = dgvQuanLyXe.SelectedRows[0].Cells["colMaXe"].Value.ToString();
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa xe {idXe}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                bool success = xeMayBLL.DeleteXeMay(idXe);
                if (success)
                {
                    MessageBox.Show("Xóa xe thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Xóa xe thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa xe: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvQuanLyXe.SelectedRows.Count > 0)
            {
                string idXe = dgvQuanLyXe.SelectedRows[0].Cells["colMaXe"].Value.ToString();
                UI.FormHandleUI.FormSuaXe frm = new UI.FormHandleUI.FormSuaXe(idXe); // Truyền mã xe vào form
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Sau khi sửa xong tự reload danh sách
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn xe cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Mở Form thêm xe dạng dialog
            UI.FormHandleUI.FormThemXe frmThemXe = new UI.FormHandleUI.FormThemXe();
            var result = frmThemXe.ShowDialog();

            // Nếu thêm thành công (DialogResult.OK), load lại data
            if (result == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void dgvQuanLyXe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbTimKiemTheo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
