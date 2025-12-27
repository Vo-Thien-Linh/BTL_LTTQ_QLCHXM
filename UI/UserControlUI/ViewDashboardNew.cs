using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BLL;
using DTO;

namespace UI.UserControlUI
{
    public partial class ViewDashboardNew : UserControl
    {
        private readonly DashboardBLL _bll = new DashboardBLL();
        private Timer refreshTimer;

        public ViewDashboardNew()
        {
            InitializeComponent();
            SetupChartTitles();
            StyleDataGridViews();
            SetupRefreshTimer();
            SetupPanelHoverEffects();
            this.Resize += ViewDashboardNew_Resize;
        }

        private void ViewDashboardNew_Resize(object sender, EventArgs e)
        {
            if (this.Width > 0 && this.Height > 0)
            {
                AdjustLayout();
            }
        }

        private void AdjustLayout()
        {
            int padding = 15;
            int cardGap = 10;
            int availableWidth = this.Width - (padding * 2);
            int cardWidth = (availableWidth - (cardGap * 3)) / 4;
            int cardHeight = 100; // Giảm từ 130 xuống 100
            
            // Điều chỉnh 4 stat cards - tăng khoảng cách từ top
            int cardY = 100; // Tăng từ 70 lên 100 để tránh đè lên datetime
            pnlXeSanSang.SetBounds(padding, cardY, cardWidth, cardHeight);
            pnlDoanhThu.SetBounds(padding + cardWidth + cardGap, cardY, cardWidth, cardHeight);
            pnlKhachHang.SetBounds(padding + (cardWidth + cardGap) * 2, cardY, cardWidth, cardHeight);
            pnlGiaoDich.SetBounds(padding + (cardWidth + cardGap) * 3, cardY, cardWidth, cardHeight);
            
            // Điều chỉnh charts - giảm khoảng cách
            int chartY = cardY + cardHeight + 15; // Giảm khoảng cách từ 20 xuống 15
            int chartWidth = (availableWidth - cardGap) / 2;
            int chartHeight = 280; // Giảm từ 320 xuống 280
            
            chartDoanhThu.SetBounds(padding, chartY, chartWidth, chartHeight);
            chartTop5Xe.SetBounds(padding + chartWidth + cardGap, chartY, chartWidth, chartHeight);
            
            // Điều chỉnh tables - giảm khoảng cách
            int tableY = chartY + chartHeight + 15; // Giảm khoảng cách từ 20 xuống 15
            int tableHeight = 220; // Giảm từ 260 xuống 220
            
            pnlHoatDongGanDay.SetBounds(padding, tableY, chartWidth, tableHeight);
            pnlCanhBaoTonKho.SetBounds(padding + chartWidth + cardGap, tableY, chartWidth, tableHeight);
            
            // Điều chỉnh DataGridViews bên trong panels
            if (dgvHoatDongGanDay.Parent != null)
            {
                dgvHoatDongGanDay.Width = pnlHoatDongGanDay.Width - 20;
                dgvHoatDongGanDay.Height = pnlHoatDongGanDay.Height - 55;
            }
            
            if (dgvCanhBaoTonKho.Parent != null)
            {
                dgvCanhBaoTonKho.Width = pnlCanhBaoTonKho.Width - 20;
                dgvCanhBaoTonKho.Height = pnlCanhBaoTonKho.Height - 55;
            }
        }

        private void SetupChartTitles()
        {
            // Setup chart Doanh Thu
            chartDoanhThu.Titles.Clear();
            Title titleDoanhThu = new Title
            {
                Text = "Doanh Thu 7 Ngày Qua",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                Docking = Docking.Top,
                Alignment = ContentAlignment.MiddleLeft
            };
            chartDoanhThu.Titles.Add(titleDoanhThu);

            // Setup chart Top 5 Xe
            chartTop5Xe.Titles.Clear();
            Title titleTop5 = new Title
            {
                Text = "Top 5 Xe Bán Chạy Nhất",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                Docking = Docking.Top,
                Alignment = ContentAlignment.MiddleLeft
            };
            chartTop5Xe.Titles.Add(titleTop5);
        }

        private void StyleDataGridViews()
        {
            StyleDataGridView(dgvHoatDongGanDay);
            StyleDataGridView(dgvCanhBaoTonKho);
        }

        private void StyleDataGridView(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 35;
            
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
        }

        private void SetupPanelHoverEffects()
        {
            SetupCardHoverEffect(pnlXeSanSang, Color.FromArgb(76, 175, 80));
            SetupCardHoverEffect(pnlDoanhThu, Color.FromArgb(33, 150, 243));
            SetupCardHoverEffect(pnlKhachHang, Color.FromArgb(255, 152, 0));
            SetupCardHoverEffect(pnlGiaoDich, Color.FromArgb(156, 39, 176));
        }

        private void SetupCardHoverEffect(Panel panel, Color originalColor)
        {
            panel.MouseEnter += (s, e) => panel.BackColor = ControlPaint.Light(originalColor, 0.1f);
            panel.MouseLeave += (s, e) => panel.BackColor = originalColor;
            
            // Paint event for rounded corners
            panel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectangle(panel.ClientRectangle, 10))
                {
                    e.Graphics.FillPath(new SolidBrush(panel.BackColor), path);
                }
            };
        }

        #region Load Data
        private void ViewDashboardNew_Load(object sender, EventArgs e)
        {
            try
            {
                lblDateTime.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy");
                LoadAllData();
                AdjustLayout(); // Điều chỉnh layout lần đầu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải Dashboard: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllData()
        {
            LoadStatCards();
            LoadDoanhThuChart();
            LoadTop5XeChart();
            LoadHoatDongGanDay();
            LoadCanhBaoTonKho();
        }

        private void LoadStatCards()
        {
            try
            {
                var stats = _bll.GetStats();
                if (stats != null)
                {
                    // Stat Card 1: Xe Sẵn Sàng
                    lblXeSanSangValue.Text = stats.XeSanSang.ToString("N0");
                    
                    // Stat Card 2: Doanh Thu (tháng này)
                    lblDoanhThuValue.Text = (stats.DoanhThuThangNay / 1000000).ToString("N1") + "M";
                    
                    // Stat Card 3: Khách Hàng
                    lblKhachHangValue.Text = stats.TongKhachHang.ToString("N0");
                    
                    // Stat Card 4: Giao Dịch (tháng này)
                    lblGiaoDichValue.Text = stats.TongGiaoDich.ToString("N0");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi LoadStatCards: {ex.Message}");
                // Hiển thị giá trị mặc định nếu lỗi
                lblXeSanSangValue.Text = "0";
                lblDoanhThuValue.Text = "0M";
                lblKhachHangValue.Text = "0";
                lblGiaoDichValue.Text = "0";
            }
        }

        private void LoadDoanhThuChart()
        {
            try
            {
                chartDoanhThu.Series.Clear();
                
                // Tạo 2 series: Doanh thu bán và Doanh thu thuê
                Series seriesBan = new Series
                {
                    Name = "Bán xe",
                    ChartType = SeriesChartType.Column,
                    Color = Color.FromArgb(33, 150, 243),
                    BorderWidth = 2
                };

                Series seriesThue = new Series
                {
                    Name = "Cho thuê",
                    ChartType = SeriesChartType.Column,
                    Color = Color.FromArgb(76, 175, 80),
                    BorderWidth = 2
                };

                // Lấy dữ liệu từ database
                DataTable dt = _bll.GetDoanhThu7Ngay();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                        decimal doanhThuBan = Convert.ToDecimal(row["DoanhThuBan"]);
                        decimal doanhThuThue = Convert.ToDecimal(row["DoanhThuThue"]);
                        
                        seriesBan.Points.AddXY(ngay.ToString("dd/MM"), doanhThuBan / 1000000);
                        seriesThue.Points.AddXY(ngay.ToString("dd/MM"), doanhThuThue / 1000000);
                    }
                }
                else
                {
                    // Dữ liệu mặc định nếu không có dữ liệu
                    for (int i = 6; i >= 0; i--)
                    {
                        DateTime date = DateTime.Now.AddDays(-i);
                        seriesBan.Points.AddXY(date.ToString("dd/MM"), 0);
                        seriesThue.Points.AddXY(date.ToString("dd/MM"), 0);
                    }
                }

                chartDoanhThu.Series.Add(seriesBan);
                chartDoanhThu.Series.Add(seriesThue);
                
                chartDoanhThu.ChartAreas[0].AxisY.LabelStyle.Format = "N1";
                chartDoanhThu.ChartAreas[0].AxisY.Title = "Triệu VNĐ";
                chartDoanhThu.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartDoanhThu.Legends[0].Enabled = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi LoadDoanhThuChart: {ex.Message}");
            }
        }

        private void LoadTop5XeChart()
        {
            try
            {
                chartTop5Xe.Series.Clear();
                
                Series series = new Series
                {
                    Name = "Số lượng giao dịch",
                    ChartType = SeriesChartType.Bar,
                    Color = Color.FromArgb(76, 175, 80),
                    BorderWidth = 2
                };

                // Lấy dữ liệu từ database
                DataTable dt = _bll.GetTop5Xe();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string tenXe = row["TenXe"].ToString();
                        int soLuong = Convert.ToInt32(row["SoLuong"]);
                        
                        if (tenXe.Length > 20) tenXe = tenXe.Substring(0, 17) + "...";
                        series.Points.AddXY(tenXe, soLuong);
                    }
                }
                else
                {
                    // Dữ liệu mặc định nếu chưa có giao dịch
                    series.Points.AddXY("Chưa có dữ liệu", 0);
                }

                chartTop5Xe.Series.Add(series);
                chartTop5Xe.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi LoadTop5XeChart: {ex.Message}");
            }
        }

        private void LoadHoatDongGanDay()
        {
            try
            {
                DataTable dt = _bll.GetHoatDongGanDay(10);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Tạo DataTable mới để hiển thị
                    DataTable displayTable = new DataTable();
                    displayTable.Columns.Add("Ngày", typeof(string));
                    displayTable.Columns.Add("Loại", typeof(string));
                    displayTable.Columns.Add("Khách hàng", typeof(string));
                    displayTable.Columns.Add("Xe", typeof(string));
                    displayTable.Columns.Add("Giá trị", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        DateTime ngayGD = Convert.ToDateTime(row["NgayGiaoDich"]);
                        string loai = row["LoaiGiaoDich"].ToString();
                        string khachHang = row["TenKhachHang"].ToString();
                        string xe = row["ThongTinXe"].ToString();
                        decimal giaTri = Convert.ToDecimal(row["GiaTri"]);

                        displayTable.Rows.Add(
                            ngayGD.ToString("dd/MM/yyyy"),
                            loai,
                            khachHang,
                            xe,
                            (giaTri / 1000000).ToString("N1") + "M"
                        );
                    }

                    dgvHoatDongGanDay.DataSource = displayTable;
                }
                else
                {
                    // Dữ liệu mặc định
                    DataTable emptyTable = new DataTable();
                    emptyTable.Columns.Add("Ngày", typeof(string));
                    emptyTable.Columns.Add("Loại", typeof(string));
                    emptyTable.Columns.Add("Khách hàng", typeof(string));
                    emptyTable.Columns.Add("Xe", typeof(string));
                    emptyTable.Columns.Add("Giá trị", typeof(string));
                    emptyTable.Rows.Add("Chưa có dữ liệu", "", "", "", "");
                    dgvHoatDongGanDay.DataSource = emptyTable;
                }

                if (dgvHoatDongGanDay.Columns.Count > 0)
                {
                    dgvHoatDongGanDay.Columns["Ngày"].Width = 90;
                    dgvHoatDongGanDay.Columns["Loại"].Width = 60;
                    dgvHoatDongGanDay.Columns["Khách hàng"].Width = 120;
                    dgvHoatDongGanDay.Columns["Xe"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvHoatDongGanDay.Columns["Giá trị"].Width = 80;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi LoadHoatDongGanDay: {ex.Message}");
            }
        }

        private void LoadCanhBaoTonKho()
        {
            try
            {
                DataTable dt = _bll.GetCanhBaoTonKho(30);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Tạo DataTable mới để hiển thị
                    DataTable displayTable = new DataTable();
                    displayTable.Columns.Add("Phụ tùng", typeof(string));
                    displayTable.Columns.Add("Tồn kho", typeof(string));
                    displayTable.Columns.Add("Trạng thái", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        string tenPT = row["TenPhuTung"].ToString();
                        int soLuong = Convert.ToInt32(row["SoLuongTon"]);
                        string trangThai = row["TrangThai"].ToString();
                        string donVi = row["DonViTinh"].ToString();

                        // Thêm biểu tượng cho trạng thái
                        string icon = "";
                        if (trangThai == "Hết hàng") icon = "🔴";
                        else if (trangThai == "Sắp hết") icon = "⚠";

                        displayTable.Rows.Add(
                            tenPT,
                            soLuong + " " + donVi,
                            icon + " " + trangThai
                        );
                    }

                    dgvCanhBaoTonKho.DataSource = displayTable;
                }
                else
                {
                    // Dữ liệu mặc định
                    DataTable emptyTable = new DataTable();
                    emptyTable.Columns.Add("Phụ tùng", typeof(string));
                    emptyTable.Columns.Add("Tồn kho", typeof(string));
                    emptyTable.Columns.Add("Trạng thái", typeof(string));
                    emptyTable.Rows.Add("✅ Tất cả phụ tùng đều đủ hàng", "", "");
                    dgvCanhBaoTonKho.DataSource = emptyTable;
                }

                if (dgvCanhBaoTonKho.Columns.Count > 0)
                {
                    dgvCanhBaoTonKho.Columns["Phụ tùng"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvCanhBaoTonKho.Columns["Tồn kho"].Width = 100;
                    dgvCanhBaoTonKho.Columns["Trạng thái"].Width = 120;
                }

                dgvCanhBaoTonKho.CellFormatting += DgvCanhBaoTonKho_CellFormatting;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi LoadCanhBaoTonKho: {ex.Message}");
            }
        }

        private void DgvCanhBaoTonKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.Value != null)  // Cột Trạng thái
            {
                string status = e.Value.ToString();
                if (status.Contains("Hết hàng"))
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else if (status.Contains("Sắp hết"))
                {
                    e.CellStyle.ForeColor = Color.Orange;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
            }
        }
        #endregion

        #region Auto Refresh
        private void SetupRefreshTimer()
        {
            refreshTimer = new Timer
            {
                Interval = 60000 // 1 minute
            };
            refreshTimer.Tick += (s, e) =>
            {
                lblDateTime.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy HH:mm");
                LoadAllData();
            };
            refreshTimer.Start();
        }
        #endregion

        #region Helper Methods
        private GraphicsPath GetRoundedRectangle(Rectangle rectangle, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rectangle.X, rectangle.Y, radius, radius, 180, 90);
            path.AddArc(rectangle.Right - radius, rectangle.Y, radius, radius, 270, 90);
            path.AddArc(rectangle.Right - radius, rectangle.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rectangle.X, rectangle.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }
        #endregion
    }
}
