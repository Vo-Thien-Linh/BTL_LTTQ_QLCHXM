using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;

namespace UI.UserControlUI
{
    public partial class ViewLichSuDonHang : UserControl
    {
        private string maKhachHang;
        private GiaoDichBanBLL giaoDichBanBLL;
        private GiaoDichThueBLL giaoDichThueBLL;
        private DataGridView dgvDonHang;

        public ViewLichSuDonHang(string maKH)
        {
            InitializeComponent();
            this.maKhachHang = maKH;
            giaoDichBanBLL = new GiaoDichBanBLL();
            giaoDichThueBLL = new GiaoDichThueBLL();

            InitializeDataGrid();
            InitializeComboBox();
            LoadData();

            // Event handlers
            cboLoaiDonHang.SelectedIndexChanged += (s, e) => LoadData();
            btnRefresh.Click += (s, e) => LoadData();
        }

        private void InitializeComboBox()
        {
            cboLoaiDonHang.SelectedIndex = 0; // Mặc định chọn "Tất cả"
        }

        private void InitializeDataGrid()
        {
            dgvDonHang = new DataGridView
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
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(248, 250, 252)
                },
                RowHeadersVisible = false,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(33, 150, 243),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Padding = new Padding(5)
                },
                ColumnHeadersHeight = 40
            };

            panelData.Controls.Add(dgvDonHang);

            dgvDonHang.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    ViewChiTietDonHang();
                }
            };
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();
                string loai = cboLoaiDonHang.SelectedItem?.ToString();

                if (loai == "Don Mua")
                {
                    dt = giaoDichBanBLL.GetAllGiaoDichBan();
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = $"MaKH = '{maKhachHang}'";
                    dt = dv.ToTable();
                }
                else if (loai == "Don Thue")
                {
                    dt = giaoDichThueBLL.GetAllGiaoDichThue();
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = $"MaKH = '{maKhachHang}'";
                    dt = dv.ToTable();
                }
                else // Tất cả
                {
                    dt = CreateCombinedTable();
                }

                dgvDonHang.DataSource = dt;
                FormatDataGrid();
                lblCount.Text = $"Tong so don hang: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi tai du lieu: " + ex.Message, "Loi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable CreateCombinedTable()
        {
            DataTable combined = new DataTable();
            combined.Columns.Add("MaDonHang", typeof(string));
            combined.Columns.Add("LoaiDon", typeof(string));
            combined.Columns.Add("TenXe", typeof(string));
            combined.Columns.Add("NgayTao", typeof(DateTime));
            combined.Columns.Add("TongTien", typeof(decimal));
            combined.Columns.Add("TrangThai", typeof(string));

            try
            {
                // Lấy đơn bán
                DataTable dtBan = giaoDichBanBLL.GetAllGiaoDichBan();
                DataView dvBan = dtBan.DefaultView;
                dvBan.RowFilter = $"MaKH = '{maKhachHang}'";
                foreach (DataRowView row in dvBan)
                {
                    combined.Rows.Add(
                        "MB" + row["MaGDBan"].ToString(),
                        "Mua",
                        row["TenXe"].ToString(),
                        row["NgayBan"],
                        row["GiaBan"],
                        row["TrangThaiDuyet"].ToString()
                    );
                }

                // Lấy đơn thuê
                DataTable dtThue = giaoDichThueBLL.GetAllGiaoDichThue();
                DataView dvThue = dtThue.DefaultView;
                dvThue.RowFilter = $"MaKH = '{maKhachHang}'";
                foreach (DataRowView row in dvThue)
                {
                    combined.Rows.Add(
                        "MT" + row["MaGDThue"].ToString(),
                        "Thue",
                        row["TenXe"].ToString(),
                        row["NgayBatDau"],
                        row["TongGia"],
                        row["TrangThaiDuyet"].ToString()
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi ket hop du lieu: " + ex.Message);
            }

            return combined;
        }

        private void FormatDataGrid()
        {
            if (dgvDonHang.Columns.Count == 0) return;

            // Ẩn các cột không cần thiết
            if (dgvDonHang.Columns.Contains("MaKH"))
                dgvDonHang.Columns["MaKH"].Visible = false;
            if (dgvDonHang.Columns.Contains("MaTaiKhoan"))
                dgvDonHang.Columns["MaTaiKhoan"].Visible = false;

            // Format tiền
            if (dgvDonHang.Columns.Contains("TongGia"))
            {
                dgvDonHang.Columns["TongGia"].DefaultCellStyle.Format = "N0";
                dgvDonHang.Columns["TongGia"].HeaderText = "Tong tien";
            }
            if (dgvDonHang.Columns.Contains("GiaBan"))
            {
                dgvDonHang.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                dgvDonHang.Columns["GiaBan"].HeaderText = "Gia ban";
            }
            if (dgvDonHang.Columns.Contains("TongTien"))
            {
                dgvDonHang.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                dgvDonHang.Columns["TongTien"].HeaderText = "Tong tien";
            }

            // Đổi tên cột
            if (dgvDonHang.Columns.Contains("TrangThaiDuyet"))
                dgvDonHang.Columns["TrangThaiDuyet"].HeaderText = "Trang thai";
            if (dgvDonHang.Columns.Contains("TenXe"))
                dgvDonHang.Columns["TenXe"].HeaderText = "Ten xe";
            if (dgvDonHang.Columns.Contains("NgayBan"))
                dgvDonHang.Columns["NgayBan"].HeaderText = "Ngay mua";
            if (dgvDonHang.Columns.Contains("NgayBatDau"))
                dgvDonHang.Columns["NgayBatDau"].HeaderText = "Ngay bat dau";
            if (dgvDonHang.Columns.Contains("MaDonHang"))
                dgvDonHang.Columns["MaDonHang"].HeaderText = "Ma don hang";
            if (dgvDonHang.Columns.Contains("LoaiDon"))
                dgvDonHang.Columns["LoaiDon"].HeaderText = "Loai";

            dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Color coding cho trạng thái
            dgvDonHang.CellFormatting += DgvDonHang_CellFormatting;
        }

        private void DgvDonHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDonHang.Columns[e.ColumnIndex].Name == "TrangThaiDuyet" ||
                dgvDonHang.Columns[e.ColumnIndex].Name == "TrangThai")
            {
                if (e.Value != null)
                {
                    string status = e.Value.ToString();
                    if (status.Contains("Chờ duyệt") || status.Contains("Cho duyet"))
                    {
                        e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
                        e.CellStyle.ForeColor = Color.FromArgb(255, 152, 0);
                        e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    }
                    else if (status.Contains("Đã duyệt") || status.Contains("Da duyet"))
                    {
                        e.CellStyle.BackColor = Color.FromArgb(200, 230, 201);
                        e.CellStyle.ForeColor = Color.FromArgb(56, 142, 60);
                        e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    }
                    else if (status.Contains("Từ chối") || status.Contains("Tu choi"))
                    {
                        e.CellStyle.BackColor = Color.FromArgb(255, 205, 210);
                        e.CellStyle.ForeColor = Color.FromArgb(211, 47, 47);
                        e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    }
                }
            }
        }

        private void ViewChiTietDonHang()
        {
            if (dgvDonHang.SelectedRows.Count == 0)
                return;

            try
            {
                DataGridViewRow row = dgvDonHang.SelectedRows[0];
                string message = "THONG TIN DON HANG\n\n";

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Visible && cell.Value != null)
                    {
                        message += $"{dgvDonHang.Columns[cell.ColumnIndex].HeaderText}: {cell.Value}\n";
                    }
                }

                MessageBox.Show(message, "Chi tiet don hang",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi xem chi tiet: " + ex.Message, "Loi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}