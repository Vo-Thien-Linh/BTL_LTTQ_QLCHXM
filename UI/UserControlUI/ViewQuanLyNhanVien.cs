using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UI.FormUI;

namespace UI.UserControlUI
{
    public partial class ViewQuanLyNhanVien : UserControl
    {
        private NhanVienBLL nhanVienBLL;
        private FlowLayoutPanel flowPanelEmployees;
        private Panel panelEmployeeDetail;
        private DataTable currentData;
        private string selectedEmployeeId = null;
        private LanguageManagerBLL langMgr = LanguageManagerBLL.Instance;
        private Dictionary<string, string> searchFieldMap;

        

        public ViewQuanLyNhanVien()
        {
            InitializeComponent();
            nhanVienBLL = new NhanVienBLL();
            InitializeCardView();
            LoadData();

            this.Load += ViewQuanLyNhanVien_Load;
            btnTimKiem.Click += BtnSearch_Click;
            btnThemNhanVien.Click += Btn_AddEmployee_Click;
            btnSuaNhanVien.Click += Btn_EditEmployee_Click;
            btnXoaNhanVien.Click += Btn_DeleteEmployee_Click;
            btnLamMoi.Click += Btn_RefreshEmployee_Click;
            txtTuKhoa.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnSearch_Click(s, e); };

            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            ApplyTheme(ThemeManager.Instance.CurrentTheme);

            // Đăng ký cập nhật ngôn ngữ động
            langMgr.LanguageChanged += (s, e) => { ApplyLanguage(); LoadData(); };
            ApplyLanguage();

            InitSearchFieldMap();
            
            // Áp dụng phân quyền cho nút Thêm/Sửa/Xóa
            ApplyPermissions();
        }

        /// <summary>
        /// Áp dụng phân quyền cho các nút thao tác
        /// Chỉ Quản lý mới có quyền thêm/sửa/xóa nhân viên
        /// </summary>
        private void ApplyPermissions()
        {
            bool canManageNhanVien = PermissionManager.CanViewNhanVien(); // Chỉ Quản lý
            btnThemNhanVien.Visible = canManageNhanVien;
            btnSuaNhanVien.Visible = canManageNhanVien;
            btnXoaNhanVien.Visible = canManageNhanVien;
            btnLamMoi.Visible = canManageNhanVien;
            
            ReorganizeButtons();
        }

        /// <summary>
        /// Tự động dồn các button sang trái khi một số button bị ẩn
        /// </summary>
        private void ReorganizeButtons()
        {
            List<System.Windows.Forms.Button> buttons = new List<System.Windows.Forms.Button> { btnThemNhanVien, btnSuaNhanVien, btnXoaNhanVien, btnLamMoi };
            int currentX = 20; // Vị trí X ban đầu
            int spacing = 160; // Khoảng cách giữa các button
            int y = 13; // Vị trí Y cố định

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
            searchFieldMap = new Dictionary<string, string>
    {
        { langMgr.GetString("Mã nhân viên"), "MaNV" },
        { langMgr.GetString("FullName"), "HoTenNV" },
        { langMgr.GetString("Phone"), "Sdt" },
        { langMgr.GetString("Email"), "Email" },
        { langMgr.GetString("vai trò"), "ChucVu" }
    };
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



        private void ApplyLanguage()
        {
            // Gán lại text từ resource cho tất cả controls
            lblTieuDe.Text = langMgr.GetString("EmployeeTitle");
            btnThemNhanVien.Text = langMgr.GetString("AddBtn");
            btnSuaNhanVien.Text = langMgr.GetString("EditBtn");
            btnXoaNhanVien.Text = langMgr.GetString("DeleteBtn");
            btnLamMoi.Text = langMgr.GetString("RefreshBtn");
            btnTimKiem.Text = langMgr.GetString("SearchBtn");

            lblTimKiem.Text = langMgr.GetString("SearchBy");
            lblTuKhoa.Text = langMgr.GetString("Keyword");

            // ComboBox tìm kiếm
            var searchOptions = new[]
            {
                langMgr.GetString("Mã nhân viên"),
                langMgr.GetString("FullName"),
                langMgr.GetString("vai trò"),
                langMgr.GetString("Phone"),
                langMgr.GetString("Email")
            };
            if (cboTimKiem.Items.Count != searchOptions.Length)
            {
                cboTimKiem.Items.Clear();
                cboTimKiem.Items.AddRange(searchOptions);
                cboTimKiem.SelectedIndex = 0;
            }
            else
            {
                for (int i = 0; i < searchOptions.Length; i++)
                    cboTimKiem.Items[i] = searchOptions[i];
            }
            UpdateRecordCount(currentData?.Rows.Count ?? 0);
        }

        private void InitializeCardView()
        {
            // FlowLayoutPanel để chứa các card nhân viên
            flowPanelEmployees = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(20),
                WrapContents = true
            };

            // Panel chi tiết nhân viên (ẩn mặc định)
            panelEmployeeDetail = CreateDetailPanel();
            panelEmployeeDetail.Visible = false;

            panelDataGrid.Controls.Add(flowPanelEmployees);
            panelDataGrid.Controls.Add(panelEmployeeDetail);
            panelEmployeeDetail.BringToFront();
        }

        private Panel CreateDetailPanel()
        {
            Panel detailPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(30)
            };

            Button btnClose = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Size = new Size(40, 40),
                Location = new Point(detailPanel.Width - 60, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => { panelEmployeeDetail.Visible = false; selectedEmployeeId = null; };

            detailPanel.Controls.Add(btnClose);
            return detailPanel;
        }

        private void ViewQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                currentData = nhanVienBLL.GetAllNhanVien();
                DisplayEmployeeCards(currentData);
                UpdateRecordCount(currentData.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayEmployeeCards(DataTable data)
        {
            flowPanelEmployees.Controls.Clear();

            if (data == null || data.Rows.Count == 0)
            {
                Label lblEmpty = new Label
                {
                    Text = "Không có dữ liệu nhân viên",
                    Font = new Font("Segoe UI", 14F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Padding = new Padding(50)
                };
                flowPanelEmployees.Controls.Add(lblEmpty);
                return;
            }

            foreach (DataRow row in data.Rows)
            {
                Panel card = CreateEmployeeCard(row);
                flowPanelEmployees.Controls.Add(card);
            }
        }

        private Panel CreateEmployeeCard(DataRow employee)
        {
            Panel card = new Panel
            {
                Size = new Size(340, 200),
                BackColor = Color.White,
                Margin = new Padding(10),
                Cursor = Cursors.Hand,
                Tag = employee
            };

            // Shadow effect
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = GetRoundedRectangle(card.ClientRectangle, 10))
                {
                    card.Region = new Region(path);
                }
            };

            // Avatar
            Panel avatarPanel = new Panel
            {
                Size = new Size(80, 80),
                Location = new Point(20, 20),
                BackColor = GetRandomColor()
            };
            avatarPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, avatarPanel.Width, avatarPanel.Height);
                    avatarPanel.Region = new Region(path);
                }

                // Kiểm tra nếu có ảnh nhân viên thì hiển thị ảnh, không thì hiển thị chữ cái
                if (employee["AnhNhanVien"] != DBNull.Value)
                {
                    byte[] imageData = (byte[])employee["AnhNhanVien"];
                    if (imageData != null && imageData.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            Image empImage = Image.FromStream(ms);
                            e.Graphics.DrawImage(empImage, 0, 0, avatarPanel.Width, avatarPanel.Height);
                        }
                        return; // Đã vẽ ảnh, không cần vẽ chữ
                    }
                }

                // Vẽ chữ cái đầu nếu không có ảnh
                string initials = GetInitials(employee["HoTenNV"].ToString());
                using (Font font = new Font("Segoe UI", 24F, FontStyle.Bold))
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    SizeF size = e.Graphics.MeasureString(initials, font);
                    e.Graphics.DrawString(initials, font, brush,
                        (avatarPanel.Width - size.Width) / 2,
                        (avatarPanel.Height - size.Height) / 2);
                }
            };

            // Thông tin nhân viên
            Label lblName = new Label
            {
                Text = employee["HoTenNV"].ToString(),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(115, 25),
                Size = new Size(200, 25),
                ForeColor = Color.FromArgb(33, 33, 33)
            };

            Label lblCode = new Label
            {
                Text = "Mã NV: " + employee["MaNV"].ToString(),
                Font = new Font("Segoe UI", 9F),
                Location = new Point(115, 50),
                Size = new Size(200, 20),
                ForeColor = Color.FromArgb(117, 117, 117)
            };

            Label lblPosition = new Label
            {
                Text = employee["ChucVu"].ToString(),
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                Location = new Point(115, 70),
                Size = new Size(200, 20),
                ForeColor = Color.FromArgb(25, 118, 210)
            };

            // Separator line
            Panel separator = new Panel
            {
                Location = new Point(20, 110),
                Size = new Size(300, 1),
                BackColor = Color.FromArgb(224, 224, 224)
            };

            // Thông tin chi tiết
            Label lblPhone = new Label
            {
                Text = "📱 " + (employee["Sdt"] != DBNull.Value ? employee["Sdt"].ToString() : "N/A"),
                Font = new Font("Segoe UI", 9F),
                Location = new Point(20, 125),
                Size = new Size(150, 20),
                ForeColor = Color.FromArgb(97, 97, 97)
            };

            Label lblEmail = new Label
            {
                Text = "✉ " + (employee["Email"] != DBNull.Value ? employee["Email"].ToString() : "N/A"),
                Font = new Font("Segoe UI", 9F),
                Location = new Point(20, 150),
                Size = new Size(300, 20),
                ForeColor = Color.FromArgb(97, 97, 97)
            };

            Label lblStatus = new Label
            {
                Text = employee["TinhTrangLamViec"]?.ToString() ?? "N/A",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Location = new Point(240, 125),
                Size = new Size(80, 25),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = GetStatusColor(employee["TinhTrangLamViec"]?.ToString()),
                ForeColor = Color.White
            };
            lblStatus.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = GetRoundedRectangle(lblStatus.ClientRectangle, 5))
                {
                    lblStatus.Region = new Region(path);
                }
            };

            // Thêm controls vào card
            card.Controls.AddRange(new Control[] { avatarPanel, lblName, lblCode, lblPosition, separator, lblPhone, lblEmail, lblStatus });

            // Hover effect
            card.MouseEnter += (s, e) =>
            {
                card.BackColor = Color.FromArgb(245, 250, 255);
                card.Padding = new Padding(5);
            };
            card.MouseLeave += (s, e) =>
            {
                card.BackColor = Color.White;
                card.Padding = new Padding(0);
            };

            // Click event
            card.Click += (s, e) => ShowEmployeeDetail(employee);
            foreach (Control ctrl in card.Controls)
            {
                ctrl.Click += (s, e) => ShowEmployeeDetail(employee);
            }

            return card;
        }

        private void ShowEmployeeDetail(DataRow employee)
        {
            selectedEmployeeId = employee["MaNV"].ToString();
            panelEmployeeDetail.Controls.Clear();
            panelEmployeeDetail.AutoScroll = true;

            // Nút đóng
            Button btnClose = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Size = new Size(40, 40),
                Location = new Point(panelEmployeeDetail.Width - 60, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => { panelEmployeeDetail.Visible = false; selectedEmployeeId = null; };

            // Header
            Label lblDetailTitle = new Label
            {
                Text = "THÔNG TIN CHI TIẾT NHÂN VIÊN",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(30, 30),
                Size = new Size(500, 30),
                ForeColor = Color.FromArgb(25, 118, 210)
            };

            // Avatar lớn
            Panel avatarLarge = new Panel
            {
                Size = new Size(120, 120),
                Location = new Point(30, 80),
                BackColor = GetRandomColor()
            };
            avatarLarge.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, avatarLarge.Width, avatarLarge.Height);
                    avatarLarge.Region = new Region(path);
                }

                // Kiểm tra nếu có ảnh nhân viên thì hiển thị ảnh, không thì hiển thị chữ cái
                if (employee["AnhNhanVien"] != DBNull.Value)
                {
                    byte[] imageData = (byte[])employee["AnhNhanVien"];
                    if (imageData != null && imageData.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            Image empImage = Image.FromStream(ms);
                            e.Graphics.DrawImage(empImage, 0, 0, avatarLarge.Width, avatarLarge.Height);
                        }
                        return; // Đã vẽ ảnh, không cần vẽ chữ
                    }
                }

                // Nếu không có ảnh, hiển thị chữ cái
                string initials = GetInitials(employee["HoTenNV"].ToString());
                using (Font font = new Font("Segoe UI", 36F, FontStyle.Bold))
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    SizeF size = e.Graphics.MeasureString(initials, font);
                    e.Graphics.DrawString(initials, font, brush,
                        (avatarLarge.Width - size.Width) / 2,
                        (avatarLarge.Height - size.Height) / 2);
                }
            };

            int leftX = 180;
            int rightX = 580;
            int yPos = 90;

            // Cột trái
            CreateDetailLabel("Họ tên:", employee["HoTenNV"].ToString(), leftX, yPos, true);
            yPos += 45;
            CreateDetailLabel("Mã nhân viên:", employee["MaNV"].ToString(), leftX, yPos);
            CreateDetailLabel("Chức vụ:", employee["ChucVu"]?.ToString(), rightX, yPos);
            yPos += 40;
            CreateDetailLabel("Ngày sinh:", employee["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(employee["NgaySinh"]).ToString("dd/MM/yyyy") : "N/A", leftX, yPos);
            CreateDetailLabel("Giới tính:", employee["GioiTinh"]?.ToString(), rightX, yPos);
            yPos += 40;
            CreateDetailLabel("Số điện thoại:", employee["Sdt"]?.ToString(), leftX, yPos);
            CreateDetailLabel("Email:", employee["Email"]?.ToString(), rightX, yPos);
            yPos += 40;
            CreateDetailLabel("CCCD:", employee["CCCD"]?.ToString(), leftX, yPos);
            CreateDetailLabel("Trình độ:", employee["TrinhDoHocVan"]?.ToString(), rightX, yPos);
            yPos += 40;
            CreateDetailLabel("Địa chỉ:", employee["DiaChi"]?.ToString(), leftX, yPos);
            CreateDetailLabel("Lương cơ bản:", employee["LuongCoBan"] != DBNull.Value ? string.Format("{0:N0} VNĐ", employee["LuongCoBan"]) : "N/A", rightX, yPos);
            yPos += 40;
            CreateDetailLabel("Tình trạng:", employee["TinhTrangLamViec"]?.ToString(), leftX, yPos);
            
            if (employee.Table.Columns.Contains("NgayVaoLam") && employee["NgayVaoLam"] != DBNull.Value)
            {
                CreateDetailLabel("Ngày vào làm:", Convert.ToDateTime(employee["NgayVaoLam"]).ToString("dd/MM/yyyy"), rightX, yPos);
            }
            yPos += 40;

            panelEmployeeDetail.Controls.Add(btnClose);
            panelEmployeeDetail.Controls.Add(lblDetailTitle);
            panelEmployeeDetail.Controls.Add(avatarLarge);

            panelEmployeeDetail.AutoScrollMinSize = new Size(0, yPos + 50);

            panelEmployeeDetail.Visible = true;
        }

        private Label CreateDetailLabel(string title, string value, int x, int y, bool isTitle = false)
        {
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", isTitle ? 12F : 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(66, 66, 66),
                AutoSize = true,
                Location = new Point(x, y)
            };
            panelEmployeeDetail.Controls.Add(lblTitle);

            // Tính toán vị trí value dựa trên độ rộng thực tế của title
            int valueX = x + 120; // Khoảng cách cố định 120px
            int valueWidth = 250; // Độ rộng cho value

            Label lblValue = new Label
            {
                Text = value ?? "N/A",
                Font = new Font("Segoe UI", isTitle ? 12F : 10F),
                ForeColor = isTitle ? Color.FromArgb(25, 118, 210) : Color.FromArgb(97, 97, 97),
                AutoSize = false,
                Size = new Size(valueWidth, isTitle ? 30 : 25),
                Location = new Point(valueX, y),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelEmployeeDetail.Controls.Add(lblValue);

            return lblTitle;
        }

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

        private string GetInitials(string name)
        {
            if (string.IsNullOrEmpty(name)) return "?";
            string[] parts = name.Trim().Split(' ');
            if (parts.Length >= 2)
                return (parts[0][0].ToString() + parts[parts.Length - 1][0].ToString()).ToUpper();
            return parts[0].Substring(0, Math.Min(2, parts[0].Length)).ToUpper();
        }

        private Color GetRandomColor()
        {
            Color[] colors = {
                Color.FromArgb(244, 67, 54),
                Color.FromArgb(233, 30, 99),
                Color.FromArgb(156, 39, 176),
                Color.FromArgb(103, 58, 183),
                Color.FromArgb(63, 81, 181),
                Color.FromArgb(33, 150, 243),
                Color.FromArgb(0, 188, 212),
                Color.FromArgb(0, 150, 136),
                Color.FromArgb(76, 175, 80),
                Color.FromArgb(255, 152, 0)
            };
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            return colors[rand.Next(colors.Length)];
        }

        private Color GetStatusColor(string status)
        {
            if (status == null) return Color.Gray;
            if (status.Contains("Còn làm")) return Color.FromArgb(76, 175, 80);
            if (status.Contains("Nghỉ")) return Color.FromArgb(244, 67, 54);
            if (status.Contains("Thử việc")) return Color.FromArgb(255, 152, 0);
            return Color.FromArgb(96, 125, 139);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string displayField = cboTimKiem.SelectedItem?.ToString() ?? langMgr.GetString("EmployeeID");
            string keyword = txtTuKhoa.Text.Trim();

            // Map sang tên trường database
            string searchField;
            if (!searchFieldMap.TryGetValue(displayField, out searchField))
            {
                searchField = "MaNV"; // default nếu không thấy
            }

            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            try
            {
                DataTable dt = nhanVienBLL.SearchNhanVien(searchField, keyword);
                currentData = dt;
                DisplayEmployeeCards(dt);
                UpdateRecordCount(dt.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_RefreshEmployee_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Clear();
            cboTimKiem.SelectedIndex = 0;
            panelEmployeeDetail.Visible = false;
            selectedEmployeeId = null;
            LoadData();
        }

        private void Btn_AddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormQuanLiNV form = new FormQuanLiNV())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                        MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_EditEmployee_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEmployeeId))
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (FormQuanLiNV form = new FormQuanLiNV(selectedEmployeeId))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                        panelEmployeeDetail.Visible = false;
                        selectedEmployeeId = null;
                        MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_DeleteEmployee_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEmployeeId))
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow selectedRow = currentData.AsEnumerable()
                .FirstOrDefault(r => r["MaNV"].ToString() == selectedEmployeeId);

            if (selectedRow == null) return;

            string hoTenNV = selectedRow["HoTenNV"].ToString();

            var confirmResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên '{hoTenNV}' (Mã: {selectedEmployeeId})?\n\nLưu ý: Thao tác này không thể hoàn tác!",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    string errorMessage;
                    bool success = nhanVienBLL.DeleteNhanVien(selectedEmployeeId, out errorMessage);

                    if (success)
                    {
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        panelEmployeeDetail.Visible = false;
                        selectedEmployeeId = null;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateRecordCount(int count)
        {
            lblDemNhanVien.Text = $"Tổng số bản ghi: {count}";
            lblDemNhanVien.ForeColor = count > 0 ? Color.FromArgb(25, 118, 210) : Color.Gray;
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btn_AddEmployee_Click_1(object sender, EventArgs e)
        {

        }

        private void lblRecordCount_Click(object sender, EventArgs e)
        {

        }

        private void panelDataGrid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ViewQuanLyNhanVien_Load_1(object sender, EventArgs e)
        {

        }
    }
}