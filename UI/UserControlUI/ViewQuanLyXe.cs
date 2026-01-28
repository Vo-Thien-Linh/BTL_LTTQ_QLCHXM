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

        private Dictionary<string, string> statusMap;


        public ViewQuanLyXe()
        {
            InitializeComponent();

            // Khởi tạo ComboBox TRƯỚC KHI gọi ApplyLanguage
            InitializeComboBox();
            InitializeTimKiemTheoComboBox();

            // Khởi tạo maps
            InitSearchFieldMap();
            InitStatusMap();

            // Khởi tạo UI khác
            InitializeCardView();

            // Áp dụng ngôn ngữ SAU KHI đã có items
            ApplyLanguage();

            // Load dữ liệu
            LoadData();

            // Theme
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            ApplyTheme(ThemeManager.Instance.CurrentTheme);

            // Đăng ký event language changed
            langMgr.LanguageChanged += OnLanguageChanged_ViewXe;

            // ✅ THÊM: Đăng ký event khi control bị dispose
            this.HandleDestroyed += ViewQuanLyXe_HandleDestroyed;

            // Phân quyền
            ApplyPermissions();
        }

        private void OnLanguageChanged_ViewXe(object sender, EventArgs e)
        {
            ApplyLanguage();
            LoadData();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                langMgr.LanguageChanged -= OnLanguageChanged_ViewXe;
            }
            base.Dispose(disposing);
        }
        private void InitStatusMap()
        {
            statusMap = new Dictionary<string, string>();
            UpdateStatusMap();
        }

        private void UpdateStatusMap()
        {
            statusMap.Clear();

            // Map từ text hiển thị sang giá trị DB (luôn là tiếng Việt)
            statusMap[langMgr.GetString("AllStatus")] = null; // "Tất cả" hoặc "All" -> null
            statusMap[langMgr.GetString("ReadyStatus")] = "Sẵn sàng";
            statusMap[langMgr.GetString("RentedStatus")] = "Đang thuê";
            statusMap[langMgr.GetString("SoldStatus")] = "Đã bán";
            statusMap[langMgr.GetString("MaintenanceStatus")] = "Đang bảo trì";
        }

        /// <summary>
        /// Áp dụng phân quyền cho các nút thao tác
        /// Chỉ Quản lý: Thêm/Sửa/Xóa
        /// Bán hàng, Kỹ thuật: Chỉ xem
        /// </summary>
        private void ApplyPermissions()
        {
            bool canEdit = PermissionManager.CanEditSanPham(); // Chỉ Quản lý
            btnThem.Visible = canEdit;
            btnSua.Visible = canEdit;
            btnXoa.Visible = canEdit;
            btnLamMoi.Visible = canEdit;
            
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
            }
        }

        private void InitSearchFieldMap()
        {
            searchFieldMap = new Dictionary<string, string>();
            UpdateSearchFieldMap();
        }

        private void UpdateSearchFieldMap()
        {
            searchFieldMap.Clear();

            searchFieldMap[langMgr.GetString("AllStatus")] = null;
            searchFieldMap[langMgr.GetString("VehicleID")] = "ID_Xe";
            searchFieldMap[langMgr.GetString("PlateNumber")] = "BienSo";
            searchFieldMap[langMgr.GetString("Brand")] = "TenHang";
            searchFieldMap[langMgr.GetString("Model")] = "TenDong";
        }


        private void ApplyLanguage()
        {
            // Cập nhật buttons và labels
            btnThem.Text = langMgr.GetString("AddBtn");
            btnSua.Text = langMgr.GetString("EditBtn");
            btnXoa.Text = langMgr.GetString("DeleteBtn");
            btnLamMoi.Text = langMgr.GetString("RefreshBtn");
            btnTimKiem.Text = langMgr.GetString("SearchBtn");

            lblTimKiemTheo.Text = langMgr.GetString("SearchBy");
            lblTrangThai.Text = langMgr.GetString("Status");
            lblTuKhoa.Text = langMgr.GetString("Keyword");

            // ✅ CHỈ cập nhật nếu ComboBox đã có items
            if (cbbTrangThai.Items.Count > 0)
            {
                int selectedIndex = cbbTrangThai.SelectedIndex;
                cbbTrangThai.Items.Clear();
                cbbTrangThai.Items.Add(langMgr.GetString("AllStatus"));
                cbbTrangThai.Items.Add(langMgr.GetString("ReadyStatus"));
                cbbTrangThai.Items.Add(langMgr.GetString("RentedStatus"));
                cbbTrangThai.Items.Add(langMgr.GetString("SoldStatus"));
                cbbTrangThai.Items.Add(langMgr.GetString("MaintenanceStatus"));
                cbbTrangThai.SelectedIndex = selectedIndex >= 0 ? selectedIndex : 0;
            }

            // ✅ CHỈ cập nhật nếu ComboBox đã có items
            if (cbbTimKiemTheo.Items.Count > 0)
            {
                int selectedIndex = cbbTimKiemTheo.SelectedIndex;
                cbbTimKiemTheo.Items.Clear();
                cbbTimKiemTheo.Items.Add(langMgr.GetString("AllStatus"));
                cbbTimKiemTheo.Items.Add(langMgr.GetString("VehicleID"));
                cbbTimKiemTheo.Items.Add(langMgr.GetString("PlateNumber"));
                cbbTimKiemTheo.Items.Add(langMgr.GetString("Brand"));
                cbbTimKiemTheo.Items.Add(langMgr.GetString("Model"));
                cbbTimKiemTheo.SelectedIndex = selectedIndex >= 0 ? selectedIndex : 0;
            }

            // Cập nhật maps
            UpdateStatusMap();
            UpdateSearchFieldMap();
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
            cbbTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
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
                Location = new Point(0, 75), // Bắt đầu từ trái, dời lên cao hơn
                Size = new Size(panel1.Width - 20, panel1.Height - 115),
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
                Location = new Point(0, 85), // Bắt đầu từ trái, dời lên cao hơn
                Size = new Size(panel1.Width - 20, panel1.Height - 115),
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
                Text = $"{langMgr.GetString("Color")}: {xe["TenMau"]} | {langMgr.GetString("Year")}: {xe["NamSX"]} | {xe["PhanKhoi"]}cc",
                Font = new Font("Segoe UI", 8F),
                Location = new Point(10, 198),
                Size = new Size(240, 18),
                ForeColor = Color.FromArgb(120, 120, 120)
            };

            // Badge trạng thái (nhỏ)
            string mucDichDisplay = isXeChoThue ? langMgr.GetString("ForRent") : langMgr.GetString("ForSale");
            Label lblTrangThai = new Label
            {
                Text = mucDichDisplay,
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
            string giaText = isXeChoThue
                ? (xe["GiaMua"] != DBNull.Value ? string.Format("{0:N0} {1}", Convert.ToDecimal(xe["GiaMua"]) / 30, langMgr.GetString("PerDay")) : "N/A")
                : (xe["GiaMua"] != DBNull.Value ? string.Format("{0:N0} VNĐ", xe["GiaMua"]) : "N/A");

            Label lblGia = new Label
            {
                Text = giaText,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(10, 252),
                Size = new Size(240, 28),
                ForeColor = Color.FromArgb(211, 47, 47),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Nút action (MUA NGAY / THUÊ NGAY)
            string btnText = isXeChoThue ? langMgr.GetString("RentNow") : langMgr.GetString("BuyNow");
            Button btnAction = new Button
            {
                Text = btnText,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Location = new Point(10, 290),
                Size = new Size(240, 38),
                BackColor = isXeChoThue ? Color.FromArgb(33, 150, 243) : Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAction.FlatAppearance.BorderSize = 0;
            btnAction.Click += (s, e) =>
            {
                string idXe = xe["ID_Xe"].ToString();

                Form mainForm = this.FindForm();
                while (mainForm != null && mainForm.GetType().Name != "MainForm")
                {
                    mainForm = mainForm.ParentForm;
                }

                if (mainForm != null)
                {
                    if (isXeChoThue)
                    {
                        var method = mainForm.GetType().GetMethod("NavigateToThueXe",
                            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                        method?.Invoke(mainForm, new object[] { idXe });
                    }
                    else
                    {
                        var method = mainForm.GetType().GetMethod("NavigateToBanXe",
                            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                        method?.Invoke(mainForm, new object[] { idXe });
                    }
                }
            };

            // Thêm controls vào card
            card.Controls.AddRange(new Control[] { 
                imagePanel, lblTenXe, lblThongTin, lblTrangThai,
                lblGia, btnAction
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
                string noImageText = langMgr.GetString("NoImage") ?? "NO IMAGE";
                g.DrawString(noImageText, fontBrand, Brushes.DarkGray, new RectangleF(0, 110, panel.Width, 40), sf);
            }
        }

        // Hiển thị chi tiết xe
        private void ShowXeDetail(DataRow xe)
        {
            selectedXeId = xe["ID_Xe"].ToString();
            panelXeDetail.Controls.Clear();
            panelXeDetail.AutoScroll = true;

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

            Label lblTitle = new Label
            {
                Text = langMgr.GetString("VehicleDetailTitle") ?? "THÔNG TIN CHI TIẾT XE MÁY",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(20, 15),
                Size = new Size(500, 30),
                ForeColor = Color.FromArgb(25, 118, 210)
            };

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

            int leftX = 390;
            int rightX = 750;
            int yPos = 70;

            CreateDetailLabel(langMgr.GetString("VehicleID") + ":", xe["ID_Xe"].ToString(), leftX, yPos, true);
            yPos += 40;
            CreateDetailLabel(langMgr.GetString("Brand") + ":", xe["TenHang"].ToString(), leftX, yPos);
            CreateDetailLabel(langMgr.GetString("Model") + ":", xe["TenDong"].ToString(), rightX, yPos);
            yPos += 35;
            CreateDetailLabel(langMgr.GetString("Color") + ":", xe["TenMau"].ToString(), leftX, yPos);
            CreateDetailLabel(langMgr.GetString("YearOfManufacture") + ":", xe["NamSX"]?.ToString(), rightX, yPos);
            yPos += 35;
            CreateDetailLabel(langMgr.GetString("EngineCapacity") + ":", xe["PhanKhoi"]?.ToString() + " cc", leftX, yPos);
            yPos += 35;

            string plateNumberText = xe["BienSo"] != DBNull.Value ? xe["BienSo"].ToString() : langMgr.GetString("NotYet");
            CreateDetailLabel(langMgr.GetString("PlateNumber") + ":", plateNumberText, leftX, yPos);

            string kmText = xe["KmDaChay"] != DBNull.Value ? string.Format("{0:N0} km", xe["KmDaChay"]) : "0 km";
            CreateDetailLabel(langMgr.GetString("Mileage") + ":", kmText, rightX, yPos);
            yPos += 35;

            CreateDetailLabel(langMgr.GetString("Purpose") + ":", xe["MucDichSuDung"]?.ToString() ?? "N/A", leftX, yPos);
            CreateDetailLabel(langMgr.GetString("Status") + ":", xe["TrangThai"]?.ToString(), rightX, yPos);
            yPos += 35;

            string priceText = xe["GiaMua"] != DBNull.Value ? string.Format("{0:N0} VNĐ", xe["GiaMua"]) : "N/A";
            CreateDetailLabel(langMgr.GetString("PurchasePrice") + ":", priceText, leftX, yPos);

            string purchaseDateText = xe["NgayMua"] != DBNull.Value ? Convert.ToDateTime(xe["NgayMua"]).ToString("dd/MM/yyyy") : "N/A";
            CreateDetailLabel(langMgr.GetString("PurchaseDate") + ":", purchaseDateText, rightX, yPos);
            yPos += 35;

            string regDateText = xe["NgayDangKy"] != DBNull.Value ? Convert.ToDateTime(xe["NgayDangKy"]).ToString("dd/MM/yyyy") : "N/A";
            CreateDetailLabel(langMgr.GetString("RegistrationDate") + ":", regDateText, leftX, yPos);

            string regExpText = xe["HetHanDangKy"] != DBNull.Value ? Convert.ToDateTime(xe["HetHanDangKy"]).ToString("dd/MM/yyyy") : "N/A";
            CreateDetailLabel(langMgr.GetString("RegistrationExpiry") + ":", regExpText, rightX, yPos);
            yPos += 35;

            string insuranceExpText = xe["HetHanBaoHiem"] != DBNull.Value ? Convert.ToDateTime(xe["HetHanBaoHiem"]).ToString("dd/MM/yyyy") : "N/A";
            CreateDetailLabel(langMgr.GetString("InsuranceExpiry") + ":", insuranceExpText, leftX, yPos);

            CreateDetailLabel(langMgr.GetString("FuelInfo") + ":", xe["ThongTinXang"]?.ToString() ?? "N/A", rightX, yPos);
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
            cbbTimKiemTheo.DropDownStyle = ComboBoxStyle.DropDownList;
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
                    MessageBox.Show(
                        langMgr.GetString("NoVehicleInDatabase"),
                        langMgr.GetString("Notification"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    flowPanelXe.Controls.Clear();
                    return;
                }

                DisplayXeCards(dt);

                // Cập nhật số lượng
                Label lblCount = this.Controls.Find("lblRecordCount", true).FirstOrDefault() as Label;
                if (lblCount != null)
                {
                    lblCount.Text = string.Format(langMgr.GetString("TotalVehicles"), dt.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    langMgr.GetString("ErrorLoadingData") + ": " + ex.Message + "\n\n" + ex.StackTrace,
                    langMgr.GetString("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

            // Lấy giá trị DB từ Dictionary
            string trangThaiDB = null;
            if (!string.IsNullOrEmpty(trangThaiDisplay) && statusMap.ContainsKey(trangThaiDisplay))
            {
                trangThaiDB = statusMap[trangThaiDisplay];
            }

            // Lấy search field từ Dictionary
            string searchField = null;
            if (!string.IsNullOrEmpty(displayField) && searchFieldMap.ContainsKey(displayField))
            {
                searchField = searchFieldMap[displayField];
            }

            try
            {
                DataTable dt;

                // TH1: Không có từ khóa, chỉ lọc theo trạng thái
                if (string.IsNullOrEmpty(keyword))
                {
                    if (string.IsNullOrEmpty(trangThaiDB))
                    {
                        dt = xeMayBLL.GetAllXeMay();
                    }
                    else
                    {
                        dt = xeMayBLL.SearchXeMay(null, null, trangThaiDB);
                    }
                }
                // TH2: Có từ khóa
                else
                {
                    dt = xeMayBLL.SearchXeMay(searchField, keyword, trangThaiDB);
                }

                // Hiển thị kết quả
                currentData = dt;

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        string.Format(
                            langMgr.GetString("NoVehicleFound"),
                            string.IsNullOrEmpty(keyword) ? "N/A" : keyword,
                            displayField ?? "N/A",
                            trangThaiDisplay ?? "N/A"
                        ),
                        langMgr.GetString("Notification"),
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
                        lblCount.Text = string.Format(langMgr.GetString("VehiclesFound"), dt.Rows.Count);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    langMgr.GetString("SearchError") + ": " + ex.Message,
                    langMgr.GetString("Error"),
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
                MessageBox.Show(
                    langMgr.GetString("PleaseSelectVehicleToDelete"),
                    langMgr.GetString("Notification"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                string.Format(langMgr.GetString("ConfirmDeleteVehicle"), selectedXeId),
                langMgr.GetString("ConfirmDelete"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                bool success = xeMayBLL.DeleteXeMay(selectedXeId);
                if (success)
                {
                    MessageBox.Show(
                        langMgr.GetString("DeleteVehicleSuccess"),
                        langMgr.GetString("Notification"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    panelXeDetail.Visible = false;
                    selectedXeId = null;
                    LoadData();
                }
                else
                {
                    MessageBox.Show(
                        langMgr.GetString("DeleteVehicleFailed"),
                        langMgr.GetString("Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    langMgr.GetString("ErrorDeletingVehicle") + ": " + ex.Message,
                    langMgr.GetString("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedXeId))
            {
                MessageBox.Show(
                    langMgr.GetString("PleaseSelectVehicleToEdit"),
                    langMgr.GetString("Notification"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
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
