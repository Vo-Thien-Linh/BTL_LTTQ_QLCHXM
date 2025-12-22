using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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
        private FlowLayoutPanel flowPanelXe;
        private Panel panelXeDetail;
        private DataTable currentData;
        private string selectedXeId = null;

        private LanguageManagerBLL langMgr = LanguageManagerBLL.Instance;

        private Dictionary<string, string> searchFieldMap;


        public ViewQuanLyXe()
        {
            InitializeComponent();
            InitializeComboBox();
            InitializeCardView();
            InitializeTimKiemTheoComboBox();
            LoadData();
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            ApplyTheme(ThemeManager.Instance.CurrentTheme);

            langMgr.LanguageChanged += (s, e) => { ApplyLanguage(); LoadData(); };
            ApplyLanguage();
            
            InitSearchFieldMap();

        }

        private void InitSearchFieldMap()
        {
            searchFieldMap = new Dictionary<string, string>
    {
        { langMgr.GetString("Biển số"), "BienSo" },
        { langMgr.GetString("Hãng"), "TenHang" },
        { langMgr.GetString("Dòng"), "TenDong" },
        // Có thể có thêm field khác nếu cần thiết
    };
        }


        private void ApplyLanguage()
        {
            btnThem.Text = langMgr.GetString("AddBtn");
            btnSua.Text = langMgr.GetString("EditBtn");
            btnXoa.Text = langMgr.GetString("DeleteBtn");
            btnLamMoi.Text = langMgr.GetString("RefreshBtn");
            btnTimKiem.Text = langMgr.GetString("SearchBtn");

            lblTimKiemTheo.Text = langMgr.GetString("SearchBy");
            lblTrangThai.Text = langMgr.GetString("Status");
            lblTuKhoa.Text = langMgr.GetString("Keyword");

            cbbTrangThai.Items[0] = langMgr.GetString("AllStatus");
            cbbTrangThai.Items[1] = langMgr.GetString("ReadyStatus");
            cbbTrangThai.Items[2] = langMgr.GetString("RentedStatus");
            cbbTrangThai.Items[3] = langMgr.GetString("SoldStatus");
            cbbTrangThai.Items[4] = langMgr.GetString("MaintenanceStatus");

            cbbTimKiemTheo.Items[0] = langMgr.GetString("AllStatus");
            cbbTimKiemTheo.Items[1] = langMgr.GetString("VehicleID");
            cbbTimKiemTheo.Items[2] = langMgr.GetString("PlateNumber");
            cbbTimKiemTheo.Items[3] = langMgr.GetString("Brand");
            cbbTimKiemTheo.Items[4] = langMgr.GetString("Model");
        }



        private void OnThemeChanged(object sender, EventArgs e)
        {
            ApplyTheme(ThemeManager.Instance.CurrentTheme);
        }

        private void ApplyTheme(string theme)
        {
            if (theme == "Dark")
            {
                this.BackColor = Color.FromArgb(45, 45, 48);
                this.ForeColor = Color.White;
                // đổi màu cho child controls...
            }
            else
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
                // đổi màu cho child controls...
            }
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

        
        // Khởi tạo Card View
        private void InitializeCardView()
        {
            // Ẩn DataGridView cũ
            if (dgvQuanLyXe != null)
            {
                dgvQuanLyXe.Visible = false;
                dgvQuanLyXe.Dock = DockStyle.None;
            }

            // Tạo FlowLayoutPanel để chứa các card
            flowPanelXe = new FlowLayoutPanel
            {
                Location = new Point(0, 110), // Bắt đầu từ trái (0, không phải 70)
                Size = new Size(panel1.Width - 20, panel1.Height - 140),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoScroll = true,
                Padding = new Padding(70, 10, 10, 10), // Padding bên trong thay vì Location
                BackColor = Color.FromArgb(245, 247, 250),
                WrapContents = true,
                Name = "flowPanelXe"
            };

            // Tạo Panel detail (ban đầu ẩn)
            panelXeDetail = CreateDetailPanel();
            panelXeDetail.Visible = false;
            panelXeDetail.Name = "panelXeDetail";

            // Thêm vào panel1 (container chính)
            panel1.Controls.Add(flowPanelXe);
            panel1.Controls.Add(panelXeDetail);
            
            flowPanelXe.BringToFront();
            panelXeDetail.BringToFront();
        }

        // Tạo panel chi tiết
        private Panel CreateDetailPanel()
        {
            Panel panel = new Panel
            {
                Location = new Point(0, 110), // Bắt đầu từ trái
                Size = new Size(panel1.Width - 20, panel1.Height - 140),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(70, 20, 20, 20) // Padding bên trong
            };
            return panel;
        }

        // Hiển thị cards
        private void DisplayXeCards(DataTable dt)
        {
            flowPanelXe.Controls.Clear();
            flowPanelXe.SuspendLayout();

            foreach (DataRow row in dt.Rows)
            {
                Panel card = CreateXeCard(row);
                flowPanelXe.Controls.Add(card);
            }

            flowPanelXe.ResumeLayout();
        }

        // Biến cho drag & drop
        private Point dragStartPoint;
        private bool isDragging = false;

        // Tạo card cho mỗi xe
        private Panel CreateXeCard(DataRow xe)
        {
            string trangThai = xe["TrangThai"]?.ToString() ?? "";
            string mucDichSuDung = xe["MucDichSuDung"]?.ToString() ?? "Bán";
            bool isXeChoThue = mucDichSuDung == "Cho thuê";
            bool isXeBan = mucDichSuDung == "Bán";

            Panel card = new Panel
            {
                Size = new Size(260, 380),
                Margin = new Padding(10),
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                AllowDrop = true
            };

            // Bo góc và shadow
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                using (GraphicsPath path = GetRoundedRectangle(rect, 8))
                {
                    card.Region = new Region(path);
                    using (Pen pen = new Pen(Color.FromArgb(230, 230, 230), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };

            // Panel ảnh xe
            Panel imagePanel = new Panel
            {
                Size = new Size(260, 160),
                Location = new Point(0, 0),
                BackColor = isXeChoThue ? Color.FromArgb(230, 240, 255) : Color.FromArgb(255, 240, 245)
            };

            // Vẽ ảnh xe hoặc placeholder
            imagePanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                
                if (xe["AnhXe"] != DBNull.Value)
                {
                    try
                    {
                        byte[] imageBytes = (byte[])xe["AnhXe"];
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            using (Image img = Image.FromStream(ms))
                            {
                                e.Graphics.DrawImage(img, 0, 0, imagePanel.Width, imagePanel.Height);
                            }
                        }
                        else
                        {
                            DrawPlaceholder(e.Graphics, imagePanel);
                        }
                    }
                    catch
                    {
                        DrawPlaceholder(e.Graphics, imagePanel);
                    }
                }
                else
                {
                    DrawPlaceholder(e.Graphics, imagePanel);
                }
            };

            // Tên xe (Hãng + Dòng)
            Label lblTenXe = new Label
            {
                Text = $"{xe["TenHang"]} {xe["TenDong"]}",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(0, 170),
                Size = new Size(240, 26),
                ForeColor = Color.FromArgb(33, 33, 33)
            };

            // Thông tin chi tiết
            Label lblThongTin = new Label
            {
                Text = $"Màu: {xe["TenMau"]} | Năm: {xe["NamSX"]} | {xe["PhanKhoi"]}cc",
                Font = new Font("Segoe UI", 8F),
                Location = new Point(10, 198),
                Size = new Size(240, 18),
                ForeColor = Color.FromArgb(120, 120, 120)
            };

            // Badge trạng thái (nhỏ)
            Label lblTrangThai = new Label
            {
                Text = mucDichSuDung,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Location = new Point(10, 222),
                Size = new Size(70, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = isXeChoThue ? Color.FromArgb(76, 175, 80) : Color.FromArgb(33, 150, 243),
                ForeColor = Color.White
            };
            lblTrangThai.Paint += (s, e) =>
            {
                using (GraphicsPath path = GetRoundedRectangle(lblTrangThai.ClientRectangle, 4))
                {
                    lblTrangThai.Region = new Region(path);
                }
            };

            // Giá (lớn và nổi bật)
            Label lblGia = new Label
            {
                Text = isXeChoThue 
                    ? (xe["GiaMua"] != DBNull.Value ? string.Format("{0:N0} VNĐ/ngày", Convert.ToDecimal(xe["GiaMua"]) / 30) : "N/A")
                    : (xe["GiaMua"] != DBNull.Value ? string.Format("{0:N0} VNĐ", xe["GiaMua"]) : "N/A"),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(10, 252),
                Size = new Size(240, 28),
                ForeColor = Color.FromArgb(211, 47, 47),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Nút action (MUA NGAY / THUÊ NGAY)
            Button btnAction = new Button
            {
                Text = isXeChoThue ? "THUÊ NGAY" : "MUA NGAY",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Location = new Point(10, 290),
                Size = new Size(115, 38),
                BackColor = isXeChoThue ? Color.FromArgb(33, 150, 243) : Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAction.FlatAppearance.BorderSize = 0;
            btnAction.Click += (s, e) => 
            {
                e = new EventArgs();
                ShowXeDetail(xe);
            };

            // Nút Sửa
            Button btnEdit = new Button
            {
                Text = "✏️ Sửa",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Location = new Point(130, 290),
                Size = new Size(55, 38),
                BackColor = Color.FromArgb(255, 152, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += (s, e) =>
            {
                e = new EventArgs();
                selectedXeId = xe["ID_Xe"].ToString();
                btnSua_Click(s, e);
            };

            // Nút Xóa
            Button btnDelete = new Button
            {
                Text = "🗑️",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(190, 290),
                Size = new Size(60, 38),
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += (s, e) =>
            {
                e = new EventArgs();
                selectedXeId = xe["ID_Xe"].ToString();
                btnXoa_Click(s, e);
            };

            // Thêm controls vào card
            card.Controls.AddRange(new Control[] { 
                imagePanel, lblTenXe, lblThongTin, lblTrangThai,
                lblGia, btnAction, btnEdit, btnDelete
            });

            // Hover effect
            card.MouseEnter += (s, e) =>
            {
                if (!isDragging)
                    card.BackColor = Color.FromArgb(250, 250, 250);
            };
            card.MouseLeave += (s, e) =>
            {
                if (!isDragging)
                    card.BackColor = Color.White;
            };

            // Drag & Drop cho card
            card.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    dragStartPoint = e.Location;
                    isDragging = false;
                }
            };

            card.MouseMove += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    int dragDistance = Math.Abs(e.X - dragStartPoint.X) + Math.Abs(e.Y - dragStartPoint.Y);
                    if (dragDistance > 5 && !isDragging)
                    {
                        isDragging = true;
                        card.Cursor = Cursors.SizeAll;
                        card.DoDragDrop(card, DragDropEffects.Move);
                    }
                }
            };

            card.MouseUp += (s, e) =>
            {
                isDragging = false;
                card.Cursor = Cursors.Hand;
            };

            card.DragEnter += (s, e) =>
            {
                if (e.Data.GetDataPresent(typeof(Panel)))
                {
                    e.Effect = DragDropEffects.Move;
                }
            };

            card.DragDrop += (s, e) =>
            {
                Panel draggedCard = (Panel)e.Data.GetData(typeof(Panel));
                if (draggedCard != null && draggedCard != card)
                {
                    int draggedIndex = flowPanelXe.Controls.IndexOf(draggedCard);
                    int targetIndex = flowPanelXe.Controls.IndexOf(card);
                    
                    flowPanelXe.Controls.SetChildIndex(draggedCard, targetIndex);
                    flowPanelXe.Invalidate();
                }
                isDragging = false;
            };

            // Click vào card (ngoài nút) để xem chi tiết
            card.Click += (s, e) => 
            {
                if (!isDragging)
                    ShowXeDetail(xe);
            };
            imagePanel.Click += (s, e) => 
            {
                if (!isDragging)
                    ShowXeDetail(xe);
            };
            lblTenXe.Click += (s, e) => 
            {
                if (!isDragging)
                    ShowXeDetail(xe);
            };
            lblThongTin.Click += (s, e) => 
            {
                if (!isDragging)
                    ShowXeDetail(xe);
            };

            return card;
        }

        // Vẽ placeholder cho ảnh
        private void DrawPlaceholder(Graphics g, Panel panel)
        {
            using (Font fontBrand = new Font("Segoe UI", 24F, FontStyle.Bold))
            using (Font fontIcon = new Font("Segoe UI", 40F))
            using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                // Background gradient
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    panel.ClientRectangle, 
                    Color.FromArgb(220, 220, 220), 
                    Color.FromArgb(240, 240, 240), 
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, panel.ClientRectangle);
                }
                
                // Icon
                g.DrawString("🏍️", fontIcon, Brushes.Gray, new RectangleF(0, 20, panel.Width, 80), sf);
                
                // Text placeholder
                g.DrawString("NO IMAGE", fontBrand, Brushes.DarkGray, new RectangleF(0, 110, panel.Width, 40), sf);
            }
        }

        // Hiển thị chi tiết xe
        private void ShowXeDetail(DataRow xe)
        {
            selectedXeId = xe["ID_Xe"].ToString();
            panelXeDetail.Controls.Clear();
            panelXeDetail.AutoScroll = true;

            // Nút đóng
            Button btnClose = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Size = new Size(40, 40),
                Location = new Point(panelXeDetail.Width - 60, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) =>
            {
                panelXeDetail.Visible = false;
                selectedXeId = null;
            };

            // Tiêu đề
            Label lblTitle = new Label
            {
                Text = "THÔNG TIN CHI TIẾT XE MÁY",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(20, 15),
                Size = new Size(500, 30),
                ForeColor = Color.FromArgb(25, 118, 210)
            };

            // Ảnh xe lớn
            PictureBox picXe = new PictureBox
            {
                Size = new Size(350, 250),
                Location = new Point(20, 60),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };

            if (xe["AnhXe"] != DBNull.Value)
            {
                try
                {
                    byte[] imageBytes = (byte[])xe["AnhXe"];
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            picXe.Image = Image.FromStream(ms);
                        }
                    }
                }
                catch { }
            }

            // Thông tin chi tiết - Layout 2 cột
            int leftX = 390;
            int rightX = 750;
            int yPos = 70;

            CreateDetailLabel("Mã xe:", xe["ID_Xe"].ToString(), leftX, yPos, true);
            yPos += 40;
            CreateDetailLabel("Hãng xe:", xe["TenHang"].ToString(), leftX, yPos);
            CreateDetailLabel("Dòng xe:", xe["TenDong"].ToString(), rightX, yPos);
            yPos += 35;
            CreateDetailLabel("Màu sắc:", xe["TenMau"].ToString(), leftX, yPos);
            CreateDetailLabel("Năm SX:", xe["NamSX"]?.ToString(), rightX, yPos);
            yPos += 35;
            CreateDetailLabel("Phân khối:", xe["PhanKhoi"]?.ToString() + " cc", leftX, yPos);
            CreateDetailLabel("Loại xe:", xe["LoaiXe"]?.ToString(), rightX, yPos);
            yPos += 35;
            CreateDetailLabel("Biển số:", xe["BienSo"] != DBNull.Value ? xe["BienSo"].ToString() : "Chưa có", leftX, yPos);
            CreateDetailLabel("Km đã chạy:", xe["KmDaChay"] != DBNull.Value ? string.Format("{0:N0} km", xe["KmDaChay"]) : "0 km", rightX, yPos);
            yPos += 35;
            CreateDetailLabel("Mục đích:", xe["MucDichSuDung"]?.ToString() ?? "N/A", leftX, yPos);
            CreateDetailLabel("Trạng thái:", xe["TrangThai"]?.ToString(), rightX, yPos);
            yPos += 35;
            CreateDetailLabel("Giá mua:", xe["GiaMua"] != DBNull.Value ? string.Format("{0:N0} VNĐ", xe["GiaMua"]) : "N/A", leftX, yPos);
            CreateDetailLabel("Ngày mua:", xe["NgayMua"] != DBNull.Value ? Convert.ToDateTime(xe["NgayMua"]).ToString("dd/MM/yyyy") : "N/A", rightX, yPos);
            yPos += 35;
            CreateDetailLabel("Ngày ĐK:", xe["NgayDangKy"] != DBNull.Value ? Convert.ToDateTime(xe["NgayDangKy"]).ToString("dd/MM/yyyy") : "N/A", leftX, yPos);
            CreateDetailLabel("Hết hạn ĐK:", xe["HetHanDangKy"] != DBNull.Value ? Convert.ToDateTime(xe["HetHanDangKy"]).ToString("dd/MM/yyyy") : "N/A", rightX, yPos);
            yPos += 35;
            CreateDetailLabel("Hết hạn BH:", xe["HetHanBaoHiem"] != DBNull.Value ? Convert.ToDateTime(xe["HetHanBaoHiem"]).ToString("dd/MM/yyyy") : "N/A", leftX, yPos);
            CreateDetailLabel("Xăng:", xe["ThongTinXang"]?.ToString() ?? "N/A", rightX, yPos);
            yPos += 35;

            panelXeDetail.Controls.Add(btnClose);
            panelXeDetail.Controls.Add(lblTitle);
            panelXeDetail.Controls.Add(picXe);
            panelXeDetail.AutoScrollMinSize = new Size(0, yPos + 30);
            panelXeDetail.Visible = true;
        }

        // Tạo label detail
        private void CreateDetailLabel(string title, string value, int x, int y, bool isTitle = false)
        {
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", isTitle ? 11F : 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(66, 66, 66),
                AutoSize = false,
                Size = new Size(100, 22),
                Location = new Point(x, y),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelXeDetail.Controls.Add(lblTitle);

            Label lblValue = new Label
            {
                Text = value ?? "N/A",
                Font = new Font("Segoe UI", isTitle ? 11F : 9.5F),
                ForeColor = isTitle ? Color.FromArgb(25, 118, 210) : Color.FromArgb(97, 97, 97),
                AutoSize = false,
                Size = new Size(220, 22),
                Location = new Point(x + 105, y),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelXeDetail.Controls.Add(lblValue);
        }

        // Lấy màu theo trạng thái
        private Color GetTrangThaiColor(string trangThai)
        {
            switch (trangThai)
            {
                case "Sẵn sàng": return Color.FromArgb(76, 175, 80);
                case "Đang thuê": return Color.FromArgb(255, 152, 0);
                case "Đã bán": return Color.FromArgb(96, 125, 139);
                case "Đang bảo trì": return Color.FromArgb(244, 67, 54);
                default: return Color.Gray;
            }
        }

        // Hàm vẽ rounded rectangle
        private GraphicsPath GetRoundedRectangle(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
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
            cbbTimKiemTheo.SelectedIndex = 0;
        }

        // Load dữ liệu
        private void LoadData(string searchField = null, string searchKeyword = null, string trangThai = null)
        {
            try
            {
                DataTable dt;

                if (string.IsNullOrEmpty(searchKeyword) &&
                    (string.IsNullOrEmpty(trangThai) || trangThai == "Tất cả") &&
                    string.IsNullOrEmpty(searchField))
                {
                    dt = xeMayBLL.GetAllXeMay();
                }
                else
                {
                    dt = xeMayBLL.SearchXeMay(searchField, searchKeyword, trangThai);
                }

                currentData = dt;

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Database chưa có xe nào!\nVui lòng thêm xe mới.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flowPanelXe.Controls.Clear();
                    return;
                }

                DisplayXeCards(dt);

                // Cập nhật số lượng
                Label lblCount = this.Controls.Find("lblRecordCount", true).FirstOrDefault() as Label;
                if (lblCount != null)
                {
                    lblCount.Text = $"Tổng số xe: {dt.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cbbTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbTimKiemTheo.DropDownStyle = ComboBoxStyle.DropDownList;

        }




        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTuKhoa.Text.Trim();
            string trangThaiDisplay = cbbTrangThai.SelectedItem?.ToString();
            string displayField = cbbTimKiemTheo.SelectedItem?.ToString();

            // ✅ Chuyển đổi trạng thái hiển thị sang giá trị DB
            string trangThaiDB = null;
            if (!string.IsNullOrEmpty(trangThaiDisplay))
            {
                switch (trangThaiDisplay)
                {
                    case "Tất cả":
                        trangThaiDB = null;
                        break;
                    case "Sẵn sàng":
                    case "ReadyStatus": // Tiếng Anh nếu có
                        trangThaiDB = "Sẵn sàng";
                        break;
                    case "Đang thuê":
                    case "RentedStatus":
                        trangThaiDB = "Đang thuê";
                        break;
                    case "Đã bán":
                    case "SoldStatus":
                        trangThaiDB = "Đã bán";
                        break;
                    case "Đang bảo trì":
                    case "MaintenanceStatus":
                        trangThaiDB = "Đang bảo trì";
                        break;
                    default:
                        trangThaiDB = trangThaiDisplay;
                        break;
                }
            }

            // ✅ Chuyển đổi field hiển thị sang tên cột DB
            string searchField = null;

            if (!string.IsNullOrEmpty(displayField))
            {
                // Kiểm tra nếu là "Tất cả" hoặc "AllStatus"
                if (displayField == "Tất cả" || displayField == langMgr.GetString("AllStatus"))
                {
                    searchField = null; // Tìm tất cả
                }
                else
                {
                    // Map từ tên hiển thị sang tên cột
                    switch (displayField)
                    {
                        case "Mã xe":
                        case "VehicleID":
                            searchField = "ID_Xe";
                            break;
                        case "Biển số":
                        case "PlateNumber":
                            searchField = "BienSo";
                            break;
                        case "Hãng":
                        case "Hãng xe":
                        case "Brand":
                            searchField = "TenHang";
                            break;
                        case "Dòng":
                        case "Dòng xe":
                        case "Model":
                            searchField = "TenDong";
                            break;
                        default:
                            // Thử dùng searchFieldMap nếu có
                            if (!searchFieldMap.TryGetValue(displayField, out searchField))
                            {
                                searchField = null;
                            }
                            break;
                    }
                }
            }

            // ✅ Xử lý logic tìm kiếm
            try
            {
                DataTable dt;

                // TH1: Không có từ khóa, chỉ lọc theo trạng thái
                if (string.IsNullOrEmpty(keyword))
                {
                    if (string.IsNullOrEmpty(trangThaiDB) || trangThaiDB == "Tất cả")
                    {
                        // Hiển thị tất cả
                        dt = xeMayBLL.GetAllXeMay();
                    }
                    else
                    {
                        // Lọc theo trạng thái
                        dt = xeMayBLL.SearchXeMay(null, null, trangThaiDB);
                    }
                }
                // TH2: Có từ khóa
                else
                {
                    if (string.IsNullOrEmpty(searchField))
                    {
                        // Tìm theo tất cả field + trạng thái
                        dt = xeMayBLL.SearchXeMay(null, keyword, trangThaiDB);
                    }
                    else
                    {
                        // Tìm theo field cụ thể + trạng thái
                        dt = xeMayBLL.SearchXeMay(searchField, keyword, trangThaiDB);
                    }
                }

                // Hiển thị kết quả
                currentData = dt;

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy xe nào phù hợp với điều kiện tìm kiếm!\n\n" +
                        $"Từ khóa: {keyword}\n" +
                        $"Trường: {displayField}\n" +
                        $"Trạng thái: {trangThaiDisplay}",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    flowPanelXe.Controls.Clear();
                }
                else
                {
                    DisplayXeCards(dt);

                    // Cập nhật số lượng
                    Label lblCount = this.Controls.Find("lblRecordCount", true).FirstOrDefault() as Label;
                    if (lblCount != null)
                    {
                        lblCount.Text = $"Tìm thấy: {dt.Rows.Count} xe";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi tìm kiếm xe!\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                System.Diagnostics.Debug.WriteLine($"Lỗi tìm kiếm: {ex.Message}\n{ex.StackTrace}");
            }
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
            if (string.IsNullOrEmpty(selectedXeId))
            {
                MessageBox.Show("Vui lòng chọn xe cần xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa xe {selectedXeId}?", 
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result != DialogResult.Yes)
                return;

            try
            {
                bool success = xeMayBLL.DeleteXeMay(selectedXeId);
                if (success)
                {
                    MessageBox.Show("Xóa xe thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    panelXeDetail.Visible = false;
                    selectedXeId = null;
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Xóa xe thất bại!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa xe: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedXeId))
            {
                MessageBox.Show("Vui lòng chọn xe cần sửa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UI.FormHandleUI.FormSuaXe frm = new UI.FormHandleUI.FormSuaXe(selectedXeId);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                panelXeDetail.Visible = false;
                selectedXeId = null;
                LoadData();
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
