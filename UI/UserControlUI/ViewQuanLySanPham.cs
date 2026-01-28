using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.UserControlUI
{
    public partial class ViewQuanLySanPham : UserControl
    {
        private LanguageManagerBLL langMgr = LanguageManagerBLL.Instance;
        private Timer animationTimer;
        private int targetHeight = 60;
        private int collapsedHeight = 10;
        private int expandedHeight = 60;
        private bool isExpanded = true; // Bắt đầu ở trạng thái mở rộng
        
        public ViewQuanLySanPham()
        {
            InitializeComponent();
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            ApplyTheme(ThemeManager.Instance.CurrentTheme);
            langMgr.LanguageChanged += (s, e) => ApplyLanguage();
            ApplyLanguage();
            
            // Khởi tạo timer cho animation
            animationTimer = new Timer();
            animationTimer.Interval = 10;
            animationTimer.Tick += AnimationTimer_Tick;
            
            // Đặt chiều cao ban đầu = MỞ RỘNG để người dùng thấy
            panel2.Height = expandedHeight;
            btnQuanLyXe.Visible = true;
            btnQuanLyPhuTung.Visible = true;
            
            // Thêm events
            panel2.MouseEnter += Panel2_MouseEnter;
            panel2.MouseLeave += Panel2_MouseLeave;
            btnQuanLyXe.MouseEnter += (s, e) => Panel2_MouseEnter(s, e);
            btnQuanLyPhuTung.MouseEnter += (s, e) => Panel2_MouseEnter(s, e);
            
            // Áp dụng phân quyền - ẩn nút khuyến mãi với role Kỹ thuật
            ApplyPermissions();
        }
        
        /// <summary>
        /// Áp dụng phân quyền - Kỹ thuật không được xem khuyến mãi
        /// </summary>
        private void ApplyPermissions()
        {
            btnQuanLyKhuyenMai.Visible = PermissionManager.CanViewKhuyenMai();
        }
        
        private void Panel2_MouseEnter(object sender, EventArgs e)
        {
            if (!isExpanded)
            {
                targetHeight = expandedHeight;
                isExpanded = true;
                btnQuanLyXe.Visible = true;
                btnQuanLyPhuTung.Visible = true;
                animationTimer.Start();
            }
        }
        
        private void Panel2_MouseLeave(object sender, EventArgs e)
        {
            // Kiểm tra nếu chuột không còn trong panel2 và các button
            if (!panel2.ClientRectangle.Contains(panel2.PointToClient(Cursor.Position)))
            {
                if (isExpanded)
                {
                    targetHeight = collapsedHeight;
                    isExpanded = false;
                    animationTimer.Start();
                }
            }
        }
        
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            int step = 5;
            
            if (panel2.Height < targetHeight)
            {
                panel2.Height = Math.Min(panel2.Height + step, targetHeight);
            }
            else if (panel2.Height > targetHeight)
            {
                panel2.Height = Math.Max(panel2.Height - step, targetHeight);
                
                // Ẩn button khi thu gọn xong
                if (panel2.Height == collapsedHeight)
                {
                    btnQuanLyXe.Visible = false;
                    btnQuanLyPhuTung.Visible = false;
                }
            }
            
            if (panel2.Height == targetHeight)
            {
                animationTimer.Stop();
            }
        }

        private void ApplyLanguage()
        {
            lblTitle.Text = langMgr.GetString("ProductTitle");
            btnQuanLyXe.Text = langMgr.GetString("ManageVehicleBtn");
            btnQuanLyPhuTung.Text = langMgr.GetString("ManagePhuTungBtn");
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

        private void ViewQuanLySanPham_Load(object sender, EventArgs e)
        {
            
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnQuanLyXe_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();

            ViewQuanLyXe viewQuanLyXe = new ViewQuanLyXe();

            viewQuanLyXe.Dock = DockStyle.Fill;

            pnlMain.Controls.Add(viewQuanLyXe);

            viewQuanLyXe.BringToFront();
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnQuanLyPhuTung_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();

            ViewQuanLyPhuTung viewQuanLyPhuTung = new ViewQuanLyPhuTung();

            viewQuanLyPhuTung.Dock = DockStyle.Fill;

            pnlMain.Controls.Add(viewQuanLyPhuTung);

            viewQuanLyPhuTung.BringToFront();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnQuanLyKhuyenMai_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();

            ViewQuanLyKhuyenMai viewQuanLyKhuyenMai = new ViewQuanLyKhuyenMai();

            viewQuanLyKhuyenMai.Dock = DockStyle.Fill;

            pnlMain.Controls.Add(viewQuanLyKhuyenMai);

            viewQuanLyKhuyenMai.BringToFront();
        }
    }
}
