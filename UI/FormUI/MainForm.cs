using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.UserControlUI;

namespace UI.FormUI
{
    public partial class MainForm : Form
    {
        private Color primaryColor = Color.FromArgb(41, 128, 185);
        private Color hoverColor = Color.FromArgb(52, 152, 219);
        private Color sidebarColor = Color.FromArgb(44, 62, 80);
        private Color contentBgColor = Color.FromArgb(236, 240, 241);
        private Color buttonNormalColor = Color.FromArgb(52, 73, 94);

        private Color logoutNormalColor = Color.FromArgb(192, 57, 43);
        private Color logoutHoverColor = Color.FromArgb(231, 76, 60);
        private Color logoutClickColor = Color.FromArgb(169, 50, 38);

        private Dictionary<Button, Color> originalColors = new Dictionary<Button, Color>();
        private Dictionary<Button, Font> originalFonts = new Dictionary<Button, Font>();

        private int expandedWidth = 210;
        private int collapsedWidth = 60;
        private bool isExpanded = true;

        private Timer slideTimer;
        private int targetWidth;

        private Button currentSelectedButton = null;
        private List<Button> sidebarButtons;

        public MainForm()
        {
            InitializeComponent();
            CustomizeForm();
            InitializeSlideTimer();
            LanguageManagerBLL.Instance.InitResourceManagerFromUI(typeof(MainForm).Assembly);
            LanguageManagerBLL.Instance.LanguageChanged += OnLanguageChanged_Menu;
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            ApplyTheme(ThemeManager.Instance.CurrentTheme);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Unsubscribe events để tránh memory leak
            LanguageManagerBLL.Instance.LanguageChanged -= OnLanguageChanged_Menu;
            ThemeManager.Instance.ThemeChanged -= OnThemeChanged;

            // Cleanup timer
            if (slideTimer != null)
            {
                slideTimer.Stop();
                slideTimer.Dispose();
            }

            base.OnFormClosing(e);
        }

        // --- SỬA ĐÂY: Hàm chọn nút ---
        private void SelectSidebarButton(Button btn)
        {
            foreach (var b in sidebarButtons)
            {
                b.BackColor = buttonNormalColor;
                b.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            }
            btn.BackColor = primaryColor;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            currentSelectedButton = btn;
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
            }
            else
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
            }
        }

        private void OnLanguageChanged_Menu(object sender, EventArgs e)
        {
            ShowButtonText(isExpanded);
        }

        private void InitializeSlideTimer()
        {
            slideTimer = new Timer();
            slideTimer.Interval = 3;
            slideTimer.Tick += SlideTimer_Tick;
        }

        private void UpdateMenuLanguage()
        {
            var langMgr = LanguageManagerBLL.Instance;
            btnThongKe.Text = "🏠 " + langMgr.GetString("MenuDashboard");
            btnQuanLyNhanVien.Text = "👥 " + langMgr.GetString("MenuEmployee");
            btnQuanLyKhachHang.Text = "👤 " + langMgr.GetString("MenuCustomer");
            btnQuanLySanPham.Text = "📦 " + langMgr.GetString("MenuProduct");
            btnQuanLyBanHang.Text = "🛒 " + langMgr.GetString("MenuSales");
            btnQuanLyChoThue.Text = "🏢 " + langMgr.GetString("MenuRental");
            btnQuanLyXuLy.Text = "⚙️ " + langMgr.GetString("MenuProcessing");
            btnCaiDat.Text = "🛠️ " + langMgr.GetString("MenuSettings");
            btnDangXuat.Text = "🚪 " + langMgr.GetString("MenuLogout");
        }

        private void SlideTimer_Tick(object sender, EventArgs e)
        {
            int step = 150;
            if (isExpanded)
            {
                if (pnlMenuBar.Width < targetWidth)
                {
                    pnlMenuBar.Width += step;
                    if (pnlMenuBar.Width >= targetWidth)
                    {
                        pnlMenuBar.Width = targetWidth;
                        slideTimer.Stop();
                        ShowButtonText(true);
                        btnDangXuat.Visible = true;
                    }
                }
            }
            else
            {
                if (pnlMenuBar.Width > expandedWidth - 50 && pnlMenuBar.Width <= expandedWidth)
                {
                    ShowButtonText(false);
                    btnDangXuat.Visible = false;
                }

                if (pnlMenuBar.Width > targetWidth)
                {
                    pnlMenuBar.Width -= step;
                    if (pnlMenuBar.Width <= targetWidth)
                    {
                        pnlMenuBar.Width = targetWidth;
                        slideTimer.Stop();
                        ShowButtonText(false);
                    }
                }
            }
        }

        private void ShowButtonText(bool show)
        {
            var langMgr = LanguageManagerBLL.Instance;
            if (show)
            {
                btnThongKe.Text = "🏠 " + langMgr.GetString("MenuDashboard");
                btnQuanLyNhanVien.Text = "👥 " + langMgr.GetString("MenuEmployee");
                btnQuanLyKhachHang.Text = "👤 " + langMgr.GetString("MenuCustomer");
                btnQuanLySanPham.Text = "📦 " + langMgr.GetString("MenuProduct");
                btnQuanLyBanHang.Text = "🛒 " + langMgr.GetString("MenuSales");
                btnQuanLyChoThue.Text = "🏢 " + langMgr.GetString("MenuRental");
                btnQuanLyXuLy.Text = "⚙️ " + langMgr.GetString("MenuProcessing");
                btnCaiDat.Text = "🛠️ " + langMgr.GetString("MenuSettings");
                btnDangXuat.Text = "🚪 " + langMgr.GetString("MenuLogout");

                btnThongKe.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyNhanVien.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyKhachHang.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLySanPham.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyBanHang.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyChoThue.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyXuLy.TextAlign = ContentAlignment.MiddleLeft;
                btnCaiDat.TextAlign = ContentAlignment.MiddleLeft;
                btnDangXuat.TextAlign = ContentAlignment.MiddleLeft;

                btnThongKe.Padding = new Padding(15, 0, 0, 0);
                btnQuanLyNhanVien.Padding = new Padding(15, 0, 0, 0);
                btnQuanLyKhachHang.Padding = new Padding(15, 0, 0, 0);
                btnQuanLySanPham.Padding = new Padding(15, 0, 0, 0);
                btnQuanLyBanHang.Padding = new Padding(15, 0, 0, 0);
                btnQuanLyChoThue.Padding = new Padding(15, 0, 0, 0);
                btnQuanLyXuLy.Padding = new Padding(15, 0, 0, 0);
                btnCaiDat.Padding = new Padding(15, 0, 0, 0);
                btnDangXuat.Padding = new Padding(15, 0, 0, 0);
            }
            else
            {
                btnThongKe.Text = "🏠";
                btnQuanLyNhanVien.Text = "👥";
                btnQuanLyKhachHang.Text = "👤";
                btnQuanLySanPham.Text = "📦";
                btnQuanLyBanHang.Text = "🛒";
                btnQuanLyChoThue.Text = "🏢";
                btnQuanLyXuLy.Text = "⚙️";
                btnCaiDat.Text = "🛠️";
                btnDangXuat.Text = "🚪";

                btnThongKe.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyNhanVien.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyKhachHang.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLySanPham.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyBanHang.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyChoThue.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyXuLy.TextAlign = ContentAlignment.MiddleLeft;
                btnCaiDat.TextAlign = ContentAlignment.MiddleLeft;

                btnThongKe.Padding = new Padding(10, 0, 0, 0);
                btnQuanLyNhanVien.Padding = new Padding(10, 0, 0, 0);
                btnQuanLyKhachHang.Padding = new Padding(10, 0, 0, 0);
                btnQuanLySanPham.Padding = new Padding(10, 0, 0, 0);
                btnQuanLyBanHang.Padding = new Padding(10, 0, 0, 0);
                btnQuanLyChoThue.Padding = new Padding(10, 0, 0, 0);
                btnQuanLyXuLy.Padding = new Padding(10, 0, 0, 0);
                btnCaiDat.Padding = new Padding(10, 0, 0, 0);
            }
        }

        private void CustomizeForm()
        {
            this.BackColor = contentBgColor;
            pnlMenuBar.Width = expandedWidth;

            CustomizeButton(btnThongKe, "🏠 Dashboard");
            CustomizeButton(btnQuanLyNhanVien, "👥 Quản Lý Nhân Viên");
            CustomizeButton(btnQuanLyKhachHang, "👤 Quản Lý Khách Hàng");
            CustomizeButton(btnQuanLySanPham, "📦 Quản Lý Sản Phẩm");
            CustomizeButton(btnQuanLyBanHang, "🛒 Quản Lý Bán Hàng");
            CustomizeButton(btnQuanLyChoThue, "🏢 Quản Lý Cho Thuê");
            CustomizeButton(btnQuanLyXuLy, "⚙️ Quản Lý Xử Lý");
            CustomizeButton(btnCaiDat, "🛠️ Cài Đặt");
            CustomizeLogoutButton(btnDangXuat, "🚪 Đăng Xuất");

            sidebarButtons = new List<Button>
            {
                btnThongKe,
                btnQuanLyNhanVien,
                btnQuanLyKhachHang,
                btnQuanLySanPham,
                btnQuanLyBanHang,
                btnQuanLyChoThue,
                btnQuanLyXuLy,
                btnCaiDat
                // KHÔNG thêm btnDangXuat ở đây!
            };
        }

        private void CustomizeButton(Button btn, string text)
        {
            originalColors[btn] = btn.BackColor;
            originalFonts[btn] = btn.Font;

            btn.Text = text;
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = buttonNormalColor;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += Button_MouseEnter;
            btn.MouseLeave += Button_MouseLeave;
            btn.MouseDown += Button_MouseDown;
            btn.MouseUp += Button_MouseUp;
        }

        private void CustomizeLogoutButton(Button btn, string text)
        {
            originalColors[btn] = btn.BackColor;
            originalFonts[btn] = btn.Font;

            btn.Text = text;
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = logoutNormalColor;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += LogoutButton_MouseEnter;
            btn.MouseLeave += LogoutButton_MouseLeave;
            btn.MouseDown += LogoutButton_MouseDown;
            btn.MouseUp += LogoutButton_MouseUp;
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn != currentSelectedButton)
            {
                btn.BackColor = hoverColor;
                btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            }
            btn.Width += 5;
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn != currentSelectedButton)
            {
                btn.BackColor = buttonNormalColor;
                btn.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            }
            btn.Width -= 5;
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn != currentSelectedButton)
            {
                btn.BackColor = hoverColor;
            }
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn != currentSelectedButton)
            {
                btn.BackColor = hoverColor; // hoặc primaryColor tuỳ bạn muốn
            }
        }

        private void LogoutButton_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = logoutHoverColor;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn.Width += 5;
        }

        private void LogoutButton_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = logoutNormalColor;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            btn.Width -= 5;
        }

        private void LogoutButton_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = logoutClickColor;
        }

        private void LogoutButton_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = logoutHoverColor;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateMenuLanguage();
            ApplyPermissions(); // Áp dụng phân quyền theo chức vụ
        }

        /// <summary>
        /// Áp dụng phân quyền: Ẩn/hiện menu theo chức vụ
        /// </summary>
        private void ApplyPermissions()
        {
            // Hiển thị/ẩn các menu theo quyền
            btnThongKe.Visible = PermissionManager.CanViewDashboard();
            btnQuanLyNhanVien.Visible = PermissionManager.CanViewNhanVien();
            btnQuanLyKhachHang.Visible = PermissionManager.CanViewKhachHang();
            btnQuanLySanPham.Visible = PermissionManager.CanViewSanPham();
            btnQuanLyBanHang.Visible = PermissionManager.CanViewBanHang();
            btnQuanLyChoThue.Visible = PermissionManager.CanViewChoThue();
            btnQuanLyXuLy.Visible = PermissionManager.CanViewXuLy();
            btnCaiDat.Visible = PermissionManager.CanViewSettings();
            
            // NÚT ĐĂNG XUẤT LUÔN HIỂN THỊ CHO TẤT CẢ USER
            btnDangXuat.Visible = true;

            // Tự động dồn các button lên để không có khoảng trống
            ReorganizeMenuButtons();

            // Load view đầu tiên có quyền xem
            string maTaiKhoan = CurrentUser.MaTaiKhoan;
            string maNhanVien = CurrentUser.MaNV ?? "";
            
            if (PermissionManager.CanViewDashboard())
            {
                SelectSidebarButton(btnThongKe);
                LoadControl(new UserControlUI.ViewDashboardNew());
            }
            else if (PermissionManager.CanViewSanPham())
            {
                SelectSidebarButton(btnQuanLySanPham);
                LoadControl(new ViewQuanLySanPham());
            }
            else if (PermissionManager.CanViewBanHang())
            {
                SelectSidebarButton(btnQuanLyBanHang);
                LoadControl(new ViewQuanLyBanHang(maTaiKhoan));
            }
            else if (PermissionManager.CanViewChoThue())
            {
                SelectSidebarButton(btnQuanLyChoThue);
                LoadControl(new ViewQuanLyChoThue(maNhanVien));
            }
            else if (PermissionManager.CanViewXuLy())
            {
                SelectSidebarButton(btnQuanLyXuLy);
                LoadControl(new ViewQuanLyBaoTri());
            }
            else if (PermissionManager.CanViewKhachHang())
            {
                SelectSidebarButton(btnQuanLyKhachHang);
                LoadControl(new ViewQuanLyKhachHang());
            }
            else
            {
                // Nếu không có quyền gì, hiển thị thông báo
                MessageBox.Show(
                    "Tài khoản của bạn chưa được cấp quyền truy cập!\n" +
                    "Vui lòng liên hệ quản trị viên.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        /// <summary>
        /// Tự động dồn các button menu lên để không có khoảng trống khi một số button bị ẩn
        /// </summary>
        private void ReorganizeMenuButtons()
        {
            // Danh sách các button theo thứ tự hiển thị (THÊM btnDangXuat vào cuối)
            List<Button> menuButtons = new List<Button>
            {
                btnQuanLyNhanVien,
                btnQuanLyKhachHang,
                btnQuanLySanPham,
                btnQuanLyBanHang,
                btnQuanLyChoThue,
                btnQuanLyXuLy,
                btnThongKe,
                btnCaiDat,
                btnDangXuat  // ← THÊM NÚT ĐĂNG XUẤT
            };

            int currentY = 0;
            int buttonHeight = 80; // Chiều cao của mỗi button
            int buttonWidth = 267; // Chiều rộng của button

            foreach (Button btn in menuButtons)
            {
                if (btn.Visible)
                {
                    btn.Location = new Point(0, currentY);
                    btn.Size = new Size(buttonWidth, buttonHeight);
                    currentY += buttonHeight;
                }
            }
        }

        

        // --- Các sự kiện click nút sidebar ---
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            SelectSidebarButton(btnThongKe);
            LoadControl(new UserControlUI.ViewDashboardNew());
        }

        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            SelectSidebarButton(btnQuanLyNhanVien);
            LoadControl(new UserControlUI.ViewQuanLyNhanVien());
        }

        private void btnQuanLyKhachHang_Click(object sender, EventArgs e)
        {
            SelectSidebarButton(btnQuanLyKhachHang);
            LoadControl(new UserControlUI.ViewQuanLyKhachHang());
        }

        private void btnQuanLySanPham_Click(object sender, EventArgs e)
        {
            SelectSidebarButton(btnQuanLySanPham);
            LoadControl(new UserControlUI.ViewQuanLySanPham());
        }

        private void btnQuanLyBanHang_Click(object sender, EventArgs e)
        {
            SelectSidebarButton(btnQuanLyBanHang);
            LoadControl(new UserControlUI.ViewQuanLyBanHang(CurrentUser.MaTaiKhoan));
        }

        private void btnQuanLyChoThue_Click(object sender, EventArgs e)
        {
            SelectSidebarButton(btnQuanLyChoThue);
            LoadQuanLyChoThueWithTabs();
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {
        }
        private void pnlMenuBar_Paint(object sender, PaintEventArgs e)
        {
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void LoadQuanLyChoThueWithTabs()
        {
            pnlContent.Controls.Clear();

            var langMgr = LanguageManagerBLL.Instance;

            TabControl tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                ItemSize = new Size(200, 40),
                SizeMode = TabSizeMode.Fixed
            };

            // Tab 1: Quản lý cho thuê (tạo đơn thuê - Quản lý và nhân viên Bán hàng)
            if (CurrentUser.ChucVu == "Quản lý" || CurrentUser.ChucVu == "Bán hàng")
            {
                TabPage tabQuanLyChoThue = new TabPage(" " + langMgr.GetString("TabManageRental"));
                ViewQuanLyChoThue viewQuanLyChoThue = new ViewQuanLyChoThue(CurrentUser.MaNV);
                viewQuanLyChoThue.Dock = DockStyle.Fill;
                tabQuanLyChoThue.Controls.Add(viewQuanLyChoThue);
                tabControl.TabPages.Add(tabQuanLyChoThue);
            }

            // Tab 2: Duyệt đơn thuê (chỉ Quản lý)
            if (CurrentUser.LoaiTaiKhoan == "QuanLy")
            {
                TabPage tabDuyetDon = new TabPage(" " + langMgr.GetString("TabApproveRental"));
                ViewDuyetDonThue viewDuyetDon = new ViewDuyetDonThue();
                viewDuyetDon.SetMaNhanVien(CurrentUser.MaNV);
                viewDuyetDon.Dock = DockStyle.Fill;
                tabDuyetDon.Controls.Add(viewDuyetDon);
                tabControl.TabPages.Add(tabDuyetDon);
            }

            pnlContent.Controls.Add(tabControl);
            tabControl.BringToFront();
        }


        private void btnQuanLyXuLy_Click(object sender, EventArgs e)
        {
            SelectSidebarButton(btnQuanLyXuLy);
            LoadControl(new UserControlUI.ViewQuanLyBaoTri());
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            SelectSidebarButton(btnCaiDat);
            LoadControl(new ViewCaiDat());
        }

        private void btnDangxuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?",
        "Xác Nhận Đăng Xuất",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

            if (result == DialogResult.Yes)
            {
                // Cleanup trước khi đóng
                CleanupBeforeLogout();

                MessageBox.Show(
                    "Đăng xuất thành công! Hẹn gặp lại.",
                    "Thông Báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Đóng MainForm - LoginForm sẽ tự động hiện lại
                this.Close();
            }
        }

        private void CleanupBeforeLogout()
        {
            try
            {
                // Clear tất cả controls trong pnlContent
                pnlContent.Controls.Clear();

                // Unsubscribe events
                LanguageManagerBLL.Instance.LanguageChanged -= OnLanguageChanged_Menu;
                ThemeManager.Instance.ThemeChanged -= OnThemeChanged;

                // Clear event handlers của buttons
                if (sidebarButtons != null)
                {
                    foreach (var btn in sidebarButtons)
                    {
                        btn.MouseEnter -= Button_MouseEnter;
                        btn.MouseLeave -= Button_MouseLeave;
                        btn.MouseDown -= Button_MouseDown;
                        btn.MouseUp -= Button_MouseUp;
                    }
                }

                btnDangXuat.MouseEnter -= LogoutButton_MouseEnter;
                btnDangXuat.MouseLeave -= LogoutButton_MouseLeave;
                btnDangXuat.MouseDown -= LogoutButton_MouseDown;
                btnDangXuat.MouseUp -= LogoutButton_MouseUp;

                // Stop và dispose timer
                if (slideTimer != null)
                {
                    slideTimer.Stop();
                    slideTimer.Tick -= SlideTimer_Tick;
                    slideTimer.Dispose();
                    slideTimer = null;
                }

                // Clear current user
                CurrentUser.Clear();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi cleanup: {ex.Message}");
            }
        }

        



        // Các hàm hỗ trợ khác giữ nguyên...

        private void LoadControl(UserControl control)
        {
            pnlContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(control);
            control.BringToFront();
        }
        
        // Methods để navigate từ các view khác
        public void NavigateToBanXe(string idXe)
        {
            // Chuyển sang view Quản lý Bán hàng
            SelectSidebarButton(btnQuanLyBanHang);
            string maTaiKhoan = CurrentUser.MaTaiKhoan;
            LoadControl(new ViewQuanLyBanHang(maTaiKhoan));
            
            // Kiểm tra xe trước khi mở form
            XeMayBLL xeMayBLL = new XeMayBLL();
            string errorMessage;
            if (!xeMayBLL.KiemTraXeTruocKhiBan(idXe, 1, out errorMessage))
            {
                MessageBox.Show(
                    errorMessage,
                    "Không thể bán xe",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            
            // Mở form bán xe với xe đã chọn
            FormMuaXe formMuaXe = new FormMuaXe(maTaiKhoan, idXe);
            formMuaXe.ShowDialog();
        }
        
        public void NavigateToThueXe(string idXe)
        {
            // Chuyển sang view Quản lý Cho thuê
            SelectSidebarButton(btnQuanLyChoThue);
            string maNhanVien = CurrentUser.MaNV ?? "";
            LoadControl(new ViewQuanLyChoThue(maNhanVien));
            
            // Mở form thuê xe với xe đã chọn
            string maTaiKhoan = CurrentUser.MaTaiKhoan;
            FormThemDonThue formThemDonThue = new FormThemDonThue(maTaiKhoan, idXe);
            formThemDonThue.ShowDialog();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            if (isExpanded)
            {
                isExpanded = false;
                targetWidth = collapsedWidth;
                ShowButtonText(false);
                btnDangXuat.Visible = false;
            }
            else
            {
                isExpanded = true;
                targetWidth = expandedWidth;
            }
            slideTimer.Start();
        }
    }
}
