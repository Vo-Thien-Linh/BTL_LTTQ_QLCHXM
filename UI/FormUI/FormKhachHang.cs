using System;
using System.Drawing;
using System.Windows.Forms;
using UI.UserControlUI;

namespace UI.FormUI
{
    public partial class FormKhachHang : Form
    {
        private string maKhachHang;
        private Panel panelSidebar;
        private Panel panelMain;
        private Button btnMuaThue;
        private Button btnLichSu;
        private Button btnTaiKhoan;
        private Button btnDangXuat;
        private Label lblWelcome;
        private Panel activeIndicator;

        public FormKhachHang(string maKH)
        {
            InitializeComponent();
            this.maKhachHang = maKH;
            InitializeCustomComponents();
            LoadUserInfo();
            ShowMuaThueXeView(); // Mặc định hiển thị tab mua/thuê xe
        }

        private void InitializeCustomComponents()
        {
            // Sidebar
            panelSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 250,
                BackColor = Color.FromArgb(33, 150, 243)
            };
            this.Controls.Add(panelSidebar);

            // Logo/Title
            Label lblLogo = new Label
            {
                Text = "KHACH HANG",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };
            panelSidebar.Controls.Add(lblLogo);

            // Welcome message
            lblWelcome = new Label
            {
                Text = "Xin chao!",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Color.White,
                Location = new Point(20, 60),
                AutoSize = true
            };
            panelSidebar.Controls.Add(lblWelcome);

            // Divider
            Panel divider = new Panel
            {
                Location = new Point(20, 100),
                Width = 210,
                Height = 2,
                BackColor = Color.FromArgb(100, 181, 246)
            };
            panelSidebar.Controls.Add(divider);

            int yPos = 130;

            // Button Mua/Thuê Xe
            btnMuaThue = CreateMenuButton("Mua & Thue Xe", yPos);
            btnMuaThue.Click += (s, e) => ShowMuaThueXeView();
            panelSidebar.Controls.Add(btnMuaThue);
            yPos += 60;

            // Button Lịch sử đơn hàng
            btnLichSu = CreateMenuButton("Lich Su Don Hang", yPos);
            btnLichSu.Click += (s, e) => ShowLichSuDonHangView();
            panelSidebar.Controls.Add(btnLichSu);
            yPos += 60;

            // Button Tài khoản
            btnTaiKhoan = CreateMenuButton("Tai Khoan", yPos);
            btnTaiKhoan.Click += (s, e) => ShowTaiKhoanView();
            panelSidebar.Controls.Add(btnTaiKhoan);
            yPos += 60;

            // Button Đăng xuất (ở dưới cùng)
            btnDangXuat = new Button
            {
                Text = "Dang Xuat",
                Location = new Point(20, this.ClientSize.Height - 80),
                Width = 210,
                Height = 45,
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0)
            };
            btnDangXuat.FlatAppearance.BorderSize = 0;
            btnDangXuat.Click += BtnDangXuat_Click;
            panelSidebar.Controls.Add(btnDangXuat);

            // Main Panel
            panelMain = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(236, 240, 241)
            };
            this.Controls.Add(panelMain);

            // Active Indicator
            activeIndicator = new Panel
            {
                Width = 5,
                Height = 45,
                BackColor = Color.White,
                Location = new Point(0, 130)
            };
            panelSidebar.Controls.Add(activeIndicator);
        }

        private Button CreateMenuButton(string text, int yPosition)
        {
            Button btn = new Button
            {
                Text = text,
                Location = new Point(20, yPosition),
                Width = 210,
                Height = 45,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(66, 165, 245);

            return btn;
        }

        private void LoadUserInfo()
        {
            try
            {
                // TODO: Load thông tin khách hàng từ database
                lblWelcome.Text = $"Xin chao, Khach hang!";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin: " + ex.Message);
            }
        }

        private void ShowMuaThueXeView()
        {
            panelMain.Controls.Clear();
            ViewMuaThueXe view = new ViewMuaThueXe
            {
                Dock = DockStyle.Fill
            };
            panelMain.Controls.Add(view);

            SetActiveButton(btnMuaThue, 130);
        }

        private void ShowLichSuDonHangView()
        {
            panelMain.Controls.Clear();
            ViewLichSuDonHang view = new ViewLichSuDonHang(maKhachHang)
            {
                Dock = DockStyle.Fill
            };
            panelMain.Controls.Add(view);

            SetActiveButton(btnLichSu, 190);
        }

        private void ShowTaiKhoanView()
        {
            panelMain.Controls.Clear();
            ViewTaiKhoanKH view = new ViewTaiKhoanKH(maKhachHang)
            {
                Dock = DockStyle.Fill
            };
            panelMain.Controls.Add(view);

            SetActiveButton(btnTaiKhoan, 250);
        }

        private void SetActiveButton(Button activeButton, int indicatorY)
        {
            // Reset all buttons
            btnMuaThue.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            btnLichSu.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            btnTaiKhoan.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            // Set active button
            activeButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            // Move indicator
            activeIndicator.Location = new Point(0, indicatorY);
        }

        private void BtnDangXuat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
                // TODO: Mở lại form đăng nhập
            }
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {

        }
    }
}