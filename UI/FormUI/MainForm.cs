using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

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

        private Color logoutNormalColor = Color.FromArgb(192, 57, 43);  // Đỏ
        private Color logoutHoverColor = Color.FromArgb(231, 76, 60);   // Đỏ sáng
        private Color logoutClickColor = Color.FromArgb(169, 50, 38);   // Đỏ đậm

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
        }

        private void InitializeSlideTimer()
        {
            slideTimer = new Timer();
            slideTimer.Interval = 1; // Tốc độ animation
            slideTimer.Tick += SlideTimer_Tick;
        }

        private void SlideTimer_Tick(object sender, EventArgs e)
        {
            int step = 20;

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
                // Ẩn text trước khi thu gọn
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
                        ShowButtonText(false); // Đảm bảo chỉ hiển thị icon
                    }
                }
            }
        }

        private void ShowButtonText(bool show)
        {
            if (show)
            {
                // Hiển thị text đầy đủ
                btnThongKe.Text = "🏠 Dashboard";
                btnQuanLyNhanVien.Text = "👥 Quản Lý Nhân Viên";
                btnQuanLyKhachHang.Text = "👤 Quản Lý Khách Hàng";
                btnQuanLySanPham.Text = "📦 Quản Lý Sản Phẩm";
                btnQuanLyBanHang.Text = "🛒 Quản Lý Bán Hàng";
                btnQuanLyChoThue.Text = "🏢 Quản Lý Cho Thuê";
                btnQuanLyXuLy.Text = "⚙️ Quản Lý Xử Lý";
                btnDangXuat.Text = "🚪 Đăng Xuất";

                // Căn trái
                btnThongKe.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyNhanVien.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyKhachHang.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLySanPham.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyBanHang.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyChoThue.TextAlign = ContentAlignment.MiddleLeft;
                btnQuanLyXuLy.TextAlign = ContentAlignment.MiddleLeft;
                btnDangXuat.TextAlign = ContentAlignment.MiddleLeft;
            }
            else
            {
                // Chỉ hiển thị icon
                btnThongKe.Text = "🏠";
                btnQuanLyNhanVien.Text = "👥";
                btnQuanLyKhachHang.Text = "👤";
                btnQuanLySanPham.Text = "📦";
                btnQuanLyBanHang.Text = "🛒";
                btnQuanLyChoThue.Text = "🏢";
                btnQuanLyXuLy.Text = "⚙️";

                // Căn giữa và padding 0
                btnThongKe.TextAlign = ContentAlignment.MiddleCenter;
                btnQuanLyNhanVien.TextAlign = ContentAlignment.MiddleCenter;
                btnQuanLyKhachHang.TextAlign = ContentAlignment.MiddleCenter;
                btnQuanLySanPham.TextAlign = ContentAlignment.MiddleCenter;
                btnQuanLyBanHang.TextAlign = ContentAlignment.MiddleCenter;
                btnQuanLyChoThue.TextAlign = ContentAlignment.MiddleCenter;
                btnQuanLyXuLy.TextAlign = ContentAlignment.MiddleCenter;

                btnThongKe.Padding = new Padding(0);
                btnQuanLyNhanVien.Padding = new Padding(0);
                btnQuanLyKhachHang.Padding = new Padding(0);
                btnQuanLySanPham.Padding = new Padding(0);
                btnQuanLyBanHang.Padding = new Padding(0);
                btnQuanLyChoThue.Padding = new Padding(0);
                btnQuanLyXuLy.Padding = new Padding(0);
            }
        }
        private void CustomizeForm()
        {
            // Cấu hình Form
            this.BackColor = contentBgColor;

            // Thiết lập panel menu
            pnlMenuBar.Width = expandedWidth;

            // Thêm sự kiện cho panel
            AddMouseEventToControlAndChildren(pnlMenuBar);

            // Tùy chỉnh các button đã có
            CustomizeButton(btnThongKe, "🏠 Dashboard");
            CustomizeButton(btnQuanLyNhanVien, "👥 Quản Lý Nhân Viên");
            CustomizeButton(btnQuanLyKhachHang, "👤 Quản Lý Khách Hàng");
            CustomizeButton(btnQuanLySanPham, "📦 Quản Lý Sản Phẩm");
            CustomizeButton(btnQuanLyBanHang, "🛒 Quản Lý Bán Hàng");
            CustomizeButton(btnQuanLyChoThue, "🏢 Quản Lý Cho Thuê");
            CustomizeButton(btnQuanLyXuLy, "⚙️ Quản Lý Xử Lý");

            // Tùy chỉnh riêng cho nút Đăng Xuất với màu đỏ
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
            // Mở rộng panel
            isExpanded = true;
            targetWidth = expandedWidth;
            slideTimer.Start();
        }

        private void PnlMenuBar_MouseLeave(object sender, EventArgs e)
        {
            // Kiểm tra xem chuột có thực sự rời khỏi panel không
            if (!pnlMenuBar.ClientRectangle.Contains(pnlMenuBar.PointToClient(Control.MousePosition)))
            {
                // Thu gọn panel
                isExpanded = false;
                targetWidth = collapsedWidth;
                ShowButtonText(false);
                btnDangXuat.Visible = false;
                slideTimer.Start();
            }
        }

        private void CustomizeButton(Button btn, string text)
        {
            // Lưu màu và font gốc
            originalColors[btn] = btn.BackColor;
            originalFonts[btn] = btn.Font;

            // Thiết lập style cho button
            btn.Text = text;
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = buttonNormalColor;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.BorderSize = 0;

            // Hiệu ứng hover
            btn.MouseEnter += Button_MouseEnter;
            btn.MouseLeave += Button_MouseLeave;
            btn.MouseDown += Button_MouseDown;
            btn.MouseUp += Button_MouseUp;
        }

        private void CustomizeLogoutButton(Button btn, string text)
        {
            // Lưu màu và font gốc
            originalColors[btn] = btn.BackColor;
            originalFonts[btn] = btn.Font;

            // Thiết lập style cho button Đăng Xuất với màu đỏ
            btn.Text = text;
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = logoutNormalColor;  // Màu đỏ
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.BorderSize = 0;

            // Hiệu ứng hover riêng cho nút Đăng Xuất
            btn.MouseEnter += LogoutButton_MouseEnter;
            btn.MouseLeave += LogoutButton_MouseLeave;
            btn.MouseDown += LogoutButton_MouseDown;
            btn.MouseUp += LogoutButton_MouseUp;
        }

        // Hiệu ứng cho các button thông thường
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

        // Hiệu ứng riêng cho nút Đăng Xuất (màu đỏ)
        private void LogoutButton_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = logoutHoverColor;  // Đỏ sáng
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn.Width += 5;
        }

        private void LogoutButton_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = logoutNormalColor;  // Đỏ thường
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            btn.Width -= 5;
        }

        private void LogoutButton_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = logoutClickColor;  // Đỏ đậm
        }

        private void LogoutButton_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = logoutHoverColor;  // Đỏ sáng
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowWelcomeMessage();
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

        }

        private void btnQuanLyKhachHang_Click(object sender, EventArgs e)
        {

        }

        private void btnQuanLySanPham_Click(object sender, EventArgs e)
        {

        }

        private void btnQuanLyBanHang_Click(object sender, EventArgs e)
        {

        }

        private void btnQuanLyChoThue_Click(object sender, EventArgs e)
        {

        }

        private void btnQuanLyXuLy_Click(object sender, EventArgs e)
        {

        }

        private void btnDashBoard_Click(object sender, EventArgs e)
        {

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
    }
}