using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BLL;
using DTO;
using Sunny.UI;

namespace UI.UserControlUI
{
    public partial class ViewDashboard : UserControl
    {
        private readonly DashboardBLL _bll = new DashboardBLL();

        // THÊM MỚI: Biến lưu trữ dữ liệu gốc
        private DataTable _originalData;

        public ViewDashboard()
        {
            InitializeComponent();
            EnableDoubleBuffer(dgvXeMoiNhap);
            // Icons đã được setup trong Designer nên không cần setup ở đây nữa
        }

        private void ViewDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                StyleGrid();
                LoadDashboardData();
            }
            catch (Exception ex)
            {
                UIMessageBox.Show("Lỗi nạp Dashboard: " + ex.Message, "Lỗi",
                    UIStyle.Red, UIMessageBoxButtons.OK);
            }
        }

        private void LoadDashboardData()
        {
            // 1) Lấy số liệu thống kê
            var stats = _bll.GetStats() ?? new DashboardDTO();
            SetStats(stats.XeSanSang, stats.GiaoDichBanHomNay, stats.ThueDangHoatDong, stats.DoanhThuThangNay);

            // 2) Lấy danh sách xe mới nhập (Top 8)
            var dt = _bll.LayXeMoiNhap(8);
            LoadXeMoiNhap(dt);

            // 3) Badge màu theo trạng thái
            dgvXeMoiNhap.CellFormatting -= dgvXeMoiNhap_CellFormatting;
            dgvXeMoiNhap.CellFormatting += dgvXeMoiNhap_CellFormatting;
        }

        private void SetStats(int xeSanSang, int giaoDichBanHomNay, int thueDangHoatDong, decimal doanhThuThangNay)
        {
            lblStat1.Text = xeSanSang.ToString("N0");
            lblStat2.Text = giaoDichBanHomNay.ToString("N0");
            lblStat3.Text = thueDangHoatDong.ToString("N0");
            lblStat4.Text = doanhThuThangNay.ToString("#,0") + "đ";
        }

        private void LoadXeMoiNhap(DataTable dt)
        {
            // THÊM MỚI: Lưu dữ liệu gốc để dùng cho filter
            _originalData = dt;

            dgvXeMoiNhap.Columns.Clear();
            dgvXeMoiNhap.DataSource = dt;

            // Định dạng các cột
            SetHeaderIfExists("IDXe", "ID Xe");
            SetHeaderIfExists("Hang", "Hãng");
            SetHeaderIfExists("DongXe", "Dòng xe");
            SetHeaderIfExists("BienSo", "Biển số");
            SetHeaderIfExists("PhanLoai", "Phân loại");
            SetHeaderIfExists("Gia", "Giá");
            SetHeaderIfExists("TrangThai", "Trạng thái");

            // Ẩn cột NgayMua nếu có
            if (dgvXeMoiNhap.Columns["NgayMua"] != null)
                dgvXeMoiNhap.Columns["NgayMua"].Visible = false;

            // Format cột giá
            if (dgvXeMoiNhap.Columns["Gia"] != null)
                dgvXeMoiNhap.Columns["Gia"].DefaultCellStyle.Format = "#,0";

            // Thêm cột hành động
            AddActionColumns();

            // Mặc định Active nút Tất cả
            UpdateButtonState(btnFilterAll);
        }

        // --- PHẦN THÊM MỚI: LOGIC XỬ LÝ LỌC ---

        private void btnFilterAll_Click(object sender, EventArgs e)
        {
            ApplyFilter("");
            UpdateButtonState(btnFilterAll);
        }

        private void btnFilterRent_Click(object sender, EventArgs e)
        {
            // Lọc xe có phân loại là Thuê (dựa vào cột PhanLoai trả về từ SQL)
            ApplyFilter("PhanLoai LIKE '%Thuê%'");
            UpdateButtonState(btnFilterRent);
        }

        private void btnFilterSale_Click(object sender, EventArgs e)
        {
            // Lọc xe có phân loại là Bán
            ApplyFilter("PhanLoai LIKE '%Bán%'");
            UpdateButtonState(btnFilterSale);
        }

        private void ApplyFilter(string filterExpr)
        {
            if (_originalData == null) return;

            try
            {
                DataView dv = _originalData.DefaultView;
                dv.RowFilter = filterExpr;
                dgvXeMoiNhap.DataSource = dv;

                // Khi gán lại DataSource, cần đảm bảo Action Columns vẫn hiển thị đúng
                AddActionColumns();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lọc: " + ex.Message);
            }
        }

        private void UpdateButtonState(Button activeBtn)
        {
            // Style cho nút Active (Xanh, chữ Trắng)
            Color activeBack = Color.FromArgb(80, 160, 255);
            Color activeFore = Color.White;

            // Style cho nút Inactive (Trắng, viền xám, chữ đen)
            Color inactiveBack = Color.White;
            Color inactiveFore = Color.FromArgb(64, 64, 64);

            // Reset tất cả về inactive
            SetButtonStyle(btnFilterAll, inactiveBack, inactiveFore, 1);
            SetButtonStyle(btnFilterRent, inactiveBack, inactiveFore, 1);
            SetButtonStyle(btnFilterSale, inactiveBack, inactiveFore, 1);

            // Set nút được chọn
            SetButtonStyle(activeBtn, activeBack, activeFore, 0);
        }

        private void SetButtonStyle(Button btn, Color back, Color fore, int borderSize)
        {
            btn.BackColor = back;
            btn.ForeColor = fore;
            btn.FlatAppearance.BorderSize = borderSize;
        }

        // --- HẾT PHẦN THÊM MỚI ---

        private void AddActionColumns()
        {
            // Xóa các cột hành động cũ nếu có
            if (dgvXeMoiNhap.Columns["colView"] != null)
                dgvXeMoiNhap.Columns.Remove("colView");
            if (dgvXeMoiNhap.Columns["colEdit"] != null)
                dgvXeMoiNhap.Columns.Remove("colEdit");
            if (dgvXeMoiNhap.Columns["colDelete"] != null)
                dgvXeMoiNhap.Columns.Remove("colDelete");

            // Cột Xem
            var colView = new DataGridViewButtonColumn
            {
                Name = "colView",
                HeaderText = "Hành động",
                Text = "Xem",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            colView.DefaultCellStyle.BackColor = Color.FromArgb(80, 160, 255);
            colView.DefaultCellStyle.ForeColor = Color.White;
            colView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 140, 235);
            colView.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvXeMoiNhap.Columns.Add(colView);

            // Cột Sửa
            var colEdit = new DataGridViewButtonColumn
            {
                Name = "colEdit",
                HeaderText = "",
                Text = "Sửa",
                UseColumnTextForButtonValue = true,
                Width = 70,
                FlatStyle = FlatStyle.Flat
            };
            colEdit.DefaultCellStyle.BackColor = Color.FromArgb(255, 184, 0);
            colEdit.DefaultCellStyle.ForeColor = Color.White;
            colEdit.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 164, 0);
            colEdit.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvXeMoiNhap.Columns.Add(colEdit);

            // Cột Xóa
            var colDelete = new DataGridViewButtonColumn
            {
                Name = "colDelete",
                HeaderText = "",
                Text = "Xóa",
                UseColumnTextForButtonValue = true,
                Width = 70,
                FlatStyle = FlatStyle.Flat
            };
            colDelete.DefaultCellStyle.BackColor = Color.FromArgb(220, 71, 71);
            colDelete.DefaultCellStyle.ForeColor = Color.White;
            colDelete.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 51, 51);
            colDelete.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvXeMoiNhap.Columns.Add(colDelete);
        }

        private void StyleGrid()
        {
            dgvXeMoiNhap.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgvXeMoiNhap.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(80, 160, 255);
            dgvXeMoiNhap.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvXeMoiNhap.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgvXeMoiNhap.DefaultCellStyle.SelectionBackColor = Color.FromArgb(155, 200, 255);
            dgvXeMoiNhap.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvXeMoiNhap.ColumnHeadersHeight = 38;
            dgvXeMoiNhap.RowTemplate.Height = 45;
            dgvXeMoiNhap.MultiSelect = false;
            dgvXeMoiNhap.AllowUserToResizeRows = false;
            dgvXeMoiNhap.BorderStyle = BorderStyle.None;
            dgvXeMoiNhap.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvXeMoiNhap.GridColor = Color.FromArgb(230, 230, 230);
            dgvXeMoiNhap.RowHeadersVisible = false;
            dgvXeMoiNhap.BackgroundColor = Color.White;
        }

        private void SetHeaderIfExists(string columnName, string headerText)
        {
            if (dgvXeMoiNhap.Columns[columnName] != null)
                dgvXeMoiNhap.Columns[columnName].HeaderText = headerText;
        }

        // Badge màu cho trạng thái
        private void dgvXeMoiNhap_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var col = dgvXeMoiNhap.Columns[e.ColumnIndex].Name;
            if (col != "TrangThai" || e.Value == null) return;

            string status = e.Value.ToString();
            Color back, fore = Color.Black;

            switch (status)
            {
                case "Sẵn sàng":
                    back = Color.FromArgb(110, 190, 40);
                    fore = Color.White;
                    break;
                case "Đang thuê":
                    back = Color.FromArgb(255, 184, 0);
                    fore = Color.White;
                    break;
                case "Đã bán":
                    back = Color.FromArgb(220, 71, 71);
                    fore = Color.White;
                    break;
                default:
                    back = Color.FromArgb(200, 200, 200);
                    fore = Color.Black;
                    break;
            }

            e.CellStyle.BackColor = back;
            e.CellStyle.SelectionBackColor = back;
            e.CellStyle.ForeColor = fore;
            e.CellStyle.SelectionForeColor = fore;
            e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            e.CellStyle.Padding = new Padding(8, 3, 8, 3);
        }

        private void dgvXeMoiNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var colName = dgvXeMoiNhap.Columns[e.ColumnIndex].Name;
            var idXe = dgvXeMoiNhap.Rows[e.RowIndex].Cells["IDXe"].Value?.ToString() ?? "";

            switch (colName)
            {
                case "colView":
                    ViewDetail(idXe);
                    break;
                case "colEdit":
                    EditVehicle(idXe);
                    break;
                case "colDelete":
                    DeleteVehicle(idXe);
                    break;
            }
        }

        private void ViewDetail(string idXe)
        {
            UIMessageBox.Show($"Xem chi tiết xe: {idXe}", "Thông tin",
                UIStyle.Blue, UIMessageBoxButtons.OK);
        }

        private void EditVehicle(string idXe)
        {
            UIMessageBox.Show($"Sửa thông tin xe: {idXe}", "Chỉnh sửa",
                UIStyle.Orange, UIMessageBoxButtons.OK);
        }

        private void DeleteVehicle(string idXe)
        {
            bool result = UIMessageBox.Show($"Bạn có chắc muốn xóa xe {idXe}?", "Xác nhận",
                UIStyle.Red, UIMessageBoxButtons.OKCancel);

            if (result)
            {
                try
                {
                    UIMessageBox.Show("Đã xóa xe thành công!", "Thành công",
                        UIStyle.Green, UIMessageBoxButtons.OK);
                    LoadDashboardData(); // Reload data
                }
                catch (Exception ex)
                {
                    UIMessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi",
                        UIStyle.Red, UIMessageBoxButtons.OK);
                }
            }
        }

        private void btnXemTatCa_Click(object sender, EventArgs e)
        {
            UIMessageBox.Show("Đi tới danh sách xe đầy đủ...", "Thông báo",
                UIStyle.Blue, UIMessageBoxButtons.OK);
        }

        private void grpXeMoiNhap_Enter(object sender, EventArgs e) { }

        private void lblStat2_Click(object sender, EventArgs e)
        {
            UIMessageBox.Show("Xem chi tiết giao dịch bán hôm nay", "Thông báo",
                UIStyle.Blue, UIMessageBoxButtons.OK);
        }

        private void lblDesc2_Click(object sender, EventArgs e)
        {
            lblStat2_Click(sender, e);
        }

        private void card1_Paint(object sender, PaintEventArgs e)
        {
            DrawCardBorder(sender as Panel, e.Graphics, Color.FromArgb(80, 160, 255));
        }

        private void card2_Paint(object sender, PaintEventArgs e)
        {
            DrawCardBorder(sender as Panel, e.Graphics, Color.FromArgb(110, 190, 40));
        }

        private void card3_Paint(object sender, PaintEventArgs e)
        {
            DrawCardBorder(sender as Panel, e.Graphics, Color.FromArgb(220, 155, 40));
        }

        private void card4_Paint(object sender, PaintEventArgs e)
        {
            DrawCardBorder(sender as Panel, e.Graphics, Color.FromArgb(220, 71, 71));
        }

        private void DrawCardBorder(Panel panel, Graphics g, Color accentColor)
        {
            if (panel == null) return;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = panel.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            using (var path = GetRoundedRect(rect, 6))
            using (var pen = new Pen(Color.FromArgb(216, 216, 216), 1))
            {
                g.DrawPath(pen, path);
            }

            using (var accentBrush = new SolidBrush(accentColor))
            {
                var accentRect = new Rectangle(0, 0, rect.Width + 1, 4);
                using (var accentPath = GetRoundedRectTop(accentRect, 6))
                {
                    g.FillPath(accentBrush, accentPath);
                }
            }
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        private GraphicsPath GetRoundedRectTop(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
            path.CloseFigure();

            return path;
        }

        private void EnableDoubleBuffer(DataGridView dgv)
        {
            try
            {
                var prop = dgv.GetType().GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                prop?.SetValue(dgv, true, null);
            }
            catch { /* ignore */ }
        }

        public void RefreshDashboard()
        {
            LoadDashboardData();
        }

        private void lblStat3_Click(object sender, EventArgs e) { }

        private void lblDesc3_Click(object sender, EventArgs e) { }
    }
}