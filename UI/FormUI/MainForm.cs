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
        // Màu sắc chủ đạo
        private Color primaryColor = Color.FromArgb(41, 128, 185);
        private Color hoverColor = Color.FromArgb(52, 152, 219);
        private Color sidebarColor = Color.FromArgb(44, 62, 80);
        private Color contentBgColor = Color.FromArgb(236, 240, 241);
        private Color buttonNormalColor = Color.FromArgb(52, 73, 94);

        private Color logoutNormalColor = Color.FromArgb(192, 57, 43); 
        private Color logoutHoverColor = Color.FromArgb(231, 76, 60); 
        private Color logoutClickColor = Color.FromArgb(169, 50, 38);   

        // Lưu màu và font gốc của button
        private Dictionary<Button, Color> originalColors = new Dictionary<Button, Color>();
        private Dictionary<Button, Font> originalFonts = new Dictionary<Button, Font>();

        // Kích thước panel menu
        private int expandedWidth = 210;
        private int collapsedWidth = 60;
        private bool isExpanded = true;

        // Timer cho animation
        private Timer slideTimer;
        private int targetWidth;

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

            AddMouseEventToControlAndChildren(pnlMenuBar);

            CustomizeButton(btnThongKe, "🏠 Dashboard");
            CustomizeButton(btnQuanLyNhanVien, "👥 Quản Lý Nhân Viên");
            CustomizeButton(btnQuanLyKhachHang, "👤 Quản Lý Khách Hàng");
            CustomizeButton(btnQuanLySanPham, "📦 Quản Lý Sản Phẩm");
            CustomizeButton(btnQuanLyBanHang, "🛒 Quản Lý Bán Hàng");
            CustomizeButton(btnQuanLyChoThue, "🏢 Quản Lý Cho Thuê");
            CustomizeButton(btnQuanLyXuLy, "⚙️ Quản Lý Xử Lý");
            CustomizeButton(btnCaiDat, "🛠️ Cài Đặt");

            CustomizeLogoutButton(btnDangXuat, "🚪 Đăng Xuất");
        }

        private void AddMouseEventToControlAndChildren(Control control)
        {
            control.MouseEnter += PnlMenuBar_MouseEnter;
            control.MouseLeave += PnlMenuBar_MouseLeave;

            foreach (Control child in control.Controls)
            {
                AddMouseEventToControlAndChildren(child);
            }
        }

        private void PnlMenuBar_MouseEnter(object sender, EventArgs e)
        {
            isExpanded = true;
            targetWidth = expandedWidth;
            slideTimer.Start();
        }

        private void PnlMenuBar_MouseLeave(object sender, EventArgs e)
        {
            if (!pnlMenuBar.ClientRectangle.Contains(pnlMenuBar.PointToClient(Control.MousePosition)))
            {
                isExpanded = false;
                targetWidth = collapsedWidth;
                ShowButtonText(false);
                btnDangXuat.Visible = false;
                slideTimer.Start();
            }
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
            btn.BackColor = primaryColor;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn.Width += 5;
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = buttonNormalColor;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            btn.Width -= 5;
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = hoverColor;
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = primaryColor;
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
            ShowWelcomeMessage();
            UpdateMenuLanguage();
        }

        private void ShowWelcomeMessage()
        {
            MessageBox.Show(
                "Chào mừng đến với Hệ Thống Quản Lý Bán Hàng!",
                "Chào Mừng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControlUI.ViewQuanLyNhanVien());
        }

        private void btnQuanLyKhachHang_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControlUI.ViewQuanLyKhachHang());
        }

        private void btnQuanLySanPham_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControlUI.ViewQuanLySanPham());
        }

        private void btnQuanLyBanHang_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControlUI.ViewQuanLyBanHang(CurrentUser.MaNV));
        }

        private void btnQuanLyChoThue_Click(object sender, EventArgs e)
        {
            LoadQuanLyChoThueWithTabs();
        }

        private void LoadQuanLyChoThueWithTabs()
        {
            pnlContent.Controls.Clear();

            // Tạo TabControl
            TabControl tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                ItemSize = new Size(200, 40),
                SizeMode = TabSizeMode.Fixed
            };

            //// Tab 1: Duyệt đơn thuê (CHỈ ADMIN/QUẢN LÝ)
            if (CurrentUser.LoaiTaiKhoan == "QuanLy")
            {
                TabPage tabDuyetDon = new TabPage(" Duyệt Đơn Thuê");
                ViewDuyetDonThue viewDuyetDon = new ViewDuyetDonThue();
                viewDuyetDon.SetMaNhanVien(CurrentUser.MaNV); // ✅ QUAN TRỌNG
                viewDuyetDon.Dock = DockStyle.Fill;
                tabDuyetDon.Controls.Add(viewDuyetDon);
                tabControl.TabPages.Add(tabDuyetDon);
            }

            // Tab 2: Quản lý cho thuê (TẤT CẢ)
            TabPage tabQuanLy = new TabPage(" Quản Lý Cho Thuê");
            ViewQuanLyChoThue viewQuanLy = new ViewQuanLyChoThue(CurrentUser.MaNV);
            viewQuanLy.Dock = DockStyle.Fill;
            tabQuanLy.Controls.Add(viewQuanLy);
            tabControl.TabPages.Add(tabQuanLy);

            pnlContent.Controls.Add(tabControl);
            tabControl.BringToFront();
        }

        private void btnQuanLyXuLy_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControlUI.ViewQuanLyBaoTri());
        }

        private void btnDashBoard_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControlUI.ViewDashboard());
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
                MessageBox.Show(
                    "Đăng xuất thành công! Hẹn gặp lại.",
                    "Thông Báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Hide();

                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();
                this.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlMenuBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadControl(UserControl control)
        {
            pnlContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(control);
            control.BringToFront();
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            LoadControl(new ViewCaiDat());
        }
    }
}