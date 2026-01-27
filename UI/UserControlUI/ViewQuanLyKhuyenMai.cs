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
        private string currentMaKM = "";

        public ViewQuanLyKhuyenMai()
        {
            InitializeComponent();
            khuyenMaiBLL = new KhuyenMaiBLL();

            // Đăng ký sự kiện
            this.Load += ViewQuanLyKhuyenMai_Load;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            btnTimKiem.Click += BtnTimKiem_Click;
            dgvQuanLyKhuyenMai.SelectionChanged += DgvQuanLyKhuyenMai_SelectionChanged;
        }

        private void ViewQuanLyKhuyenMai_Load(object sender, EventArgs e)
        {
            try
            {
                // Tự động cập nhật trạng thái khuyến mãi hết hạn
                int updatedCount = khuyenMaiBLL.CapNhatTrangThaiKhuyenMaiHetHan();
                if (updatedCount > 0)
                {
                    MessageBox.Show($"Đã tự động cập nhật {updatedCount} khuyến mãi sang trạng thái 'Hết hạn'",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadComboBoxTimKiem();
                LoadDanhSachKhuyenMai();
                SetupDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                HeaderText = "Mã KM",
                Width = 100
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenKM",
                DataPropertyName = "TenKM",
                HeaderText = "Tên Khuyến Mãi",
                Width = 250
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MoTa",
                DataPropertyName = "MoTa",
                HeaderText = "Mô Tả",
                Width = 200
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayBatDau",
                DataPropertyName = "NgayBatDau",
                HeaderText = "Ngày Bắt Đầu",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayKetThuc",
                DataPropertyName = "NgayKetThuc",
                HeaderText = "Ngày Kết Thúc",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoaiKhuyenMai",
                DataPropertyName = "LoaiKhuyenMai",
                HeaderText = "Loại",
                Width = 100
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PhanTramGiam",
                DataPropertyName = "PhanTramGiam",
                HeaderText = "% Giảm",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoTienGiam",
                DataPropertyName = "SoTienGiam",
                HeaderText = "Tiền Giảm",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GiaTriGiamToiDa",
                DataPropertyName = "GiaTriGiamToiDa",
                HeaderText = "Giảm Tối Đa",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoaiApDung",
                DataPropertyName = "LoaiApDung",
                HeaderText = "Áp Dụng",
                Width = 100
            });

            dgvQuanLyKhuyenMai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TrangThai",
                DataPropertyName = "TrangThai",
                HeaderText = "Trạng Thái",
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
            cbbTimKiemTheo.Items.Add("Tất cả");
            cbbTimKiemTheo.Items.Add("Mã khuyến mãi");
            cbbTimKiemTheo.Items.Add("Tên khuyến mãi");
            cbbTimKiemTheo.Items.Add("Trạng thái");
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
                    string loaiTimKiem = cbbTimKiemTheo.SelectedItem?.ToString() ?? "Tất cả";

                    switch (loaiTimKiem)
                    {
                        case "Mã khuyến mãi":
                            dt.DefaultView.RowFilter = $"MaKM LIKE '%{searchText}%'";
                            break;
                        case "Tên khuyến mãi":
                            dt.DefaultView.RowFilter = $"TenKM LIKE '%{searchText}%'";
                            break;
                        case "Trạng thái":
                            dt.DefaultView.RowFilter = $"TrangThai LIKE '%{searchText}%'";
                            break;
                        default: // Tất cả
                            dt.DefaultView.RowFilter = $"MaKM LIKE '%{searchText}%' OR TenKM LIKE '%{searchText}%' OR TrangThai LIKE '%{searchText}%'";
                            break;
                    }

                    dt = dt.DefaultView.ToTable();
                }

                dgvQuanLyKhuyenMai.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách khuyến mãi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Thêm khuyến mãi thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(currentMaKM))
                {
                    MessageBox.Show("Vui lòng chọn khuyến mãi cần sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                FormThemSuaKhuyenMai frm = new FormThemSuaKhuyenMai(currentMaKM);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhSachKhuyenMai();
                    MessageBox.Show("Cập nhật khuyến mãi thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(currentMaKM))
                {
                    MessageBox.Show("Vui lòng chọn khuyến mãi cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa khuyến mãi '{currentMaKM}'?\nKhuyến mãi sẽ được chuyển sang trạng thái 'Hủy'.",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string errorMessage;
                    if (khuyenMaiBLL.DeleteKhuyenMai(currentMaKM, out errorMessage))
                    {
                        LoadDanhSachKhuyenMai();
                        currentMaKM = "";
                        MessageBox.Show("Xóa khuyến mãi thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Xóa khuyến mãi thất bại!\n" + errorMessage, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Text = "";
            cbbTimKiemTheo.SelectedIndex = 0;
            LoadDanhSachKhuyenMai();
            currentMaKM = "";
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
    }
}
