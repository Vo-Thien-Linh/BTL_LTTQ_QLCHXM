using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BLL;
using DTO;

namespace UI.FormUI
{
    public partial class FormThemKhachHang : Form
    {
        private KhachHangBLL khachHangBLL;
        private KhachHangDTO khachHangHienTai;
        private bool isEditMode;
        private byte[] imageData;
        private LanguageManagerBLL langMgr = LanguageManagerBLL.Instance;

        // Controls
        private TextBox txtMaKH;
        private TextBox txtHoTen;
        private DateTimePicker dtpNgaySinh;
        private ComboBox cboGioiTinh;
        private TextBox txtSDT;
        private TextBox txtEmail;
        private TextBox txtDiaChi;
        private TextBox txtSoCCCD;
        private ComboBox cboLoaiGiayTo;
        private PictureBox picAnhGiayTo;
        private Button btnChonAnh;
        private Button btnXoaAnh;
        private Button btnLuu;
        private Button btnHuy;

        // Labels để cập nhật ngôn ngữ
        private Label lblTitle;
        private Label lblMaKH;
        private Label lblHoTen;
        private Label lblNgaySinh;
        private Label lblGioiTinh;
        private Label lblSDT;
        private Label lblEmail;
        private Label lblDiaChi;
        private Label lblSoCCCD;
        private Label lblLoaiGiayTo;
        private Label lblAnhGiayTo;

        public KhachHangDTO KhachHangMoi { get; private set; }

        public FormThemKhachHang()
        {
            InitializeComponent();
            khachHangBLL = new KhachHangBLL();
            isEditMode = false;
            InitializeCustomComponents();
            LoadNewCustomer();

            // Đăng ký sự kiện thay đổi ngôn ngữ
            langMgr.LanguageChanged += OnLanguageChanged;
            ApplyLanguage();
        }

        public FormThemKhachHang(KhachHangDTO kh)
        {
            InitializeComponent();
            khachHangBLL = new KhachHangBLL();
            isEditMode = true;
            khachHangHienTai = kh;
            InitializeCustomComponents();
            LoadCustomerData();

            // Đăng ký sự kiện thay đổi ngôn ngữ
            langMgr.LanguageChanged += OnLanguageChanged;
            ApplyLanguage();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Hủy đăng ký sự kiện khi đóng form
            langMgr.LanguageChanged -= OnLanguageChanged;
            base.OnFormClosing(e);
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            // Cập nhật tiêu đề form
            this.Text = isEditMode ? langMgr.GetString("EditCustomer") : langMgr.GetString("AddCustomer");

            // Cập nhật tiêu đề chính
            if (lblTitle != null)
                lblTitle.Text = isEditMode ? langMgr.GetString("EditCustomerTitle") : langMgr.GetString("AddCustomerTitle");

            // Cập nhật labels
            if (lblMaKH != null) lblMaKH.Text = langMgr.GetString("CustomerCode") + ":";
            if (lblHoTen != null) lblHoTen.Text = langMgr.GetString("FullName") + ": *";
            if (lblNgaySinh != null) lblNgaySinh.Text = langMgr.GetString("DateOfBirth") + ":";
            if (lblGioiTinh != null) lblGioiTinh.Text = langMgr.GetString("Gender") + ":";
            if (lblSDT != null) lblSDT.Text = langMgr.GetString("PhoneNumber") + ": *";
            if (lblEmail != null) lblEmail.Text = langMgr.GetString("Email") + ":";
            if (lblDiaChi != null) lblDiaChi.Text = langMgr.GetString("Address") + ":";
            if (lblSoCCCD != null) lblSoCCCD.Text = langMgr.GetString("IDNumber") + ":";
            if (lblLoaiGiayTo != null) lblLoaiGiayTo.Text = langMgr.GetString("IDType") + ":";
            if (lblAnhGiayTo != null) lblAnhGiayTo.Text = langMgr.GetString("IDPhoto") + ":";

            // Cập nhật buttons
            if (btnLuu != null) btnLuu.Text = langMgr.GetString("Save");
            if (btnHuy != null) btnHuy.Text = langMgr.GetString("Cancel");
            if (btnChonAnh != null) btnChonAnh.Text = langMgr.GetString("ChooseImage");
            if (btnXoaAnh != null) btnXoaAnh.Text = langMgr.GetString("DeleteImage");

            // Cập nhật ComboBox giới tính
            UpdateGenderComboBox();
            UpdateIDTypeComboBox();
        }

        private void UpdateGenderComboBox()
        {
            if (cboGioiTinh == null) return;

            string selectedValue = cboGioiTinh.SelectedItem?.ToString();
            cboGioiTinh.Items.Clear();

            cboGioiTinh.Items.Add(langMgr.GetString("Male"));
            cboGioiTinh.Items.Add(langMgr.GetString("Female"));
            cboGioiTinh.Items.Add(langMgr.GetString("Other"));

            // Giữ lại lựa chọn hiện tại
            if (!string.IsNullOrEmpty(selectedValue))
            {
                // Map từ giá trị cũ sang giá trị mới
                if (selectedValue.Contains("Nam") || selectedValue.Contains("Male"))
                    cboGioiTinh.SelectedIndex = 0;
                else if (selectedValue.Contains("Nữ") || selectedValue.Contains("Female"))
                    cboGioiTinh.SelectedIndex = 1;
                else
                    cboGioiTinh.SelectedIndex = 2;
            }
            else
            {
                cboGioiTinh.SelectedIndex = 0;
            }
        }

        private void UpdateIDTypeComboBox()
        {
            if (cboLoaiGiayTo == null) return;

            string selectedValue = cboLoaiGiayTo.SelectedItem?.ToString();
            cboLoaiGiayTo.Items.Clear();

            cboLoaiGiayTo.Items.Add(langMgr.GetString("CitizenID"));
            cboLoaiGiayTo.Items.Add(langMgr.GetString("NationalID"));
            cboLoaiGiayTo.Items.Add(langMgr.GetString("Passport"));
            cboLoaiGiayTo.Items.Add(langMgr.GetString("DriverLicense"));

            if (!string.IsNullOrEmpty(selectedValue))
            {
                cboLoaiGiayTo.SelectedIndex = 0; // Default
            }
            else
            {
                cboLoaiGiayTo.SelectedIndex = 0;
            }
        }

        private void InitializeCustomComponents()
        {
            this.Text = isEditMode ? "Sửa Thông Tin Khách Hàng" : "Thêm Khách Hàng Mới";
            this.Size = new Size(800, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Main panel
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White
            };
            this.Controls.Add(mainPanel);

            // Title
            lblTitle = new Label
            {
                Text = isEditMode ? "SỬA THÔNG TIN KHÁCH HÀNG" : "THÊM KHÁCH HÀNG MỚI",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                Location = new Point(20, 10),
                AutoSize = true
            };
            mainPanel.Controls.Add(lblTitle);

            int yPos = 60;

            // Mã khách hàng
            lblMaKH = AddLabel(mainPanel, "Mã Khách Hàng:", yPos);
            txtMaKH = AddTextBox(mainPanel, yPos);
            txtMaKH.ReadOnly = true;
            txtMaKH.BackColor = Color.FromArgb(240, 240, 240);
            yPos += 50;

            // Họ tên
            lblHoTen = AddLabel(mainPanel, "Họ Tên: *", yPos);
            txtHoTen = AddTextBox(mainPanel, yPos);
            yPos += 50;

            // Ngày sinh & Giới tính (cùng hàng)
            lblNgaySinh = AddLabel(mainPanel, "Ngày Sinh:", yPos, 20);
            dtpNgaySinh = new DateTimePicker
            {
                Location = new Point(150, yPos),
                Width = 200,
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10F)
            };
            mainPanel.Controls.Add(dtpNgaySinh);

            lblGioiTinh = new Label
            {
                Text = "Giới Tính:",
                Location = new Point(380, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F)
            };
            mainPanel.Controls.Add(lblGioiTinh);

            cboGioiTinh = new ComboBox
            {
                Location = new Point(480, yPos),
                Width = 250,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            mainPanel.Controls.Add(cboGioiTinh);
            yPos += 50;

            // Số điện thoại
            lblSDT = AddLabel(mainPanel, "Số Điện Thoại: *", yPos);
            txtSDT = AddTextBox(mainPanel, yPos);
            yPos += 50;

            // Email
            lblEmail = AddLabel(mainPanel, "Email:", yPos);
            txtEmail = AddTextBox(mainPanel, yPos);
            yPos += 50;

            // Địa chỉ
            lblDiaChi = AddLabel(mainPanel, "Địa Chỉ:", yPos);
            txtDiaChi = AddTextBox(mainPanel, yPos, true);
            yPos += 70;

            // Số CCCD
            lblSoCCCD = AddLabel(mainPanel, "Số CCCD/CMND:", yPos);
            txtSoCCCD = AddTextBox(mainPanel, yPos);
            yPos += 50;

            // Loại giấy tờ
            lblLoaiGiayTo = AddLabel(mainPanel, "Loại Giấy Tờ:", yPos);
            cboLoaiGiayTo = new ComboBox
            {
                Location = new Point(150, yPos),
                Width = 580,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cboLoaiGiayTo.Items.AddRange(new object[] { "CCCD", "CMND", "Hộ chiếu", "Bằng lái xe" });
            mainPanel.Controls.Add(cboLoaiGiayTo);
            yPos += 50;

            // Ảnh giấy tờ
            lblAnhGiayTo = AddLabel(mainPanel, "Ảnh Giấy Tờ:", yPos);

            Panel imagePanel = new Panel
            {
                Location = new Point(150, yPos),
                Width = 400,
                Height = 150,
                BorderStyle = BorderStyle.FixedSingle
            };
            mainPanel.Controls.Add(imagePanel);

            picAnhGiayTo = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(245, 245, 245)
            };
            imagePanel.Controls.Add(picAnhGiayTo);

            btnChonAnh = new Button
            {
                Text = "Chọn Ảnh",
                Location = new Point(560, yPos),
                Width = 100,
                Height = 35,
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9F)
            };
            btnChonAnh.FlatAppearance.BorderSize = 0;
            btnChonAnh.Click += BtnChonAnh_Click;
            mainPanel.Controls.Add(btnChonAnh);

            btnXoaAnh = new Button
            {
                Text = "Xóa Ảnh",
                Location = new Point(670, yPos),
                Width = 60,
                Height = 35,
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9F)
            };
            btnXoaAnh.FlatAppearance.BorderSize = 0;
            btnXoaAnh.Click += BtnXoaAnh_Click;
            mainPanel.Controls.Add(btnXoaAnh);

            yPos += 170;

            // Buttons
            btnLuu = new Button
            {
                Text = "Lưu",
                Location = new Point(480, yPos),
                Width = 120,
                Height = 40,
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += BtnLuu_Click;
            mainPanel.Controls.Add(btnLuu);

            btnHuy = new Button
            {
                Text = "Hủy",
                Location = new Point(610, yPos),
                Width = 120,
                Height = 40,
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.Click += BtnHuy_Click;
            mainPanel.Controls.Add(btnHuy);

            // Ẩn các control liên quan đến giấy tờ
            HideGiayToControls();
        }

        private void HideGiayToControls()
        {
            // Ẩn label
            if (lblLoaiGiayTo != null) lblLoaiGiayTo.Visible = false;
            if (lblAnhGiayTo != null) lblAnhGiayTo.Visible = false;

            // Ẩn các control input
            if (cboLoaiGiayTo != null) cboLoaiGiayTo.Visible = false;
            if (picAnhGiayTo != null && picAnhGiayTo.Parent != null) picAnhGiayTo.Parent.Visible = false;
            if (btnChonAnh != null) btnChonAnh.Visible = false;
            if (btnXoaAnh != null) btnXoaAnh.Visible = false;
        }

        private Label AddLabel(Panel parent, string text, int yPos, int xPos = 20)
        {
            Label lbl = new Label
            {
                Text = text,
                Location = new Point(xPos, yPos + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular)
            };
            parent.Controls.Add(lbl);
            return lbl;
        }

        private TextBox AddTextBox(Panel parent, int yPos, bool multiline = false)
        {
            TextBox txt = new TextBox
            {
                Location = new Point(150, yPos),
                Width = 580,
                Font = new Font("Segoe UI", 10F),
                Multiline = multiline
            };

            if (multiline)
            {
                txt.Height = 60;
                txt.ScrollBars = ScrollBars.Vertical;
            }

            parent.Controls.Add(txt);
            return txt;
        }

        private void LoadNewCustomer()
        {
            txtMaKH.Text = khachHangBLL.GenerateMaKH();
            dtpNgaySinh.Value = DateTime.Now.AddYears(-20);
            cboGioiTinh.SelectedIndex = 0;
            cboLoaiGiayTo.SelectedIndex = 0;
        }

        private void LoadCustomerData()
        {
            if (khachHangHienTai != null)
            {
                txtMaKH.Text = khachHangHienTai.MaKH;
                txtHoTen.Text = khachHangHienTai.HoTenKH;
                dtpNgaySinh.Value = khachHangHienTai.NgaySinh ?? DateTime.Now.AddYears(-20);
                cboGioiTinh.Text = khachHangHienTai.GioiTinh;
                txtSDT.Text = khachHangHienTai.Sdt;
                txtEmail.Text = khachHangHienTai.Email;
                txtDiaChi.Text = khachHangHienTai.DiaChi;
                txtSoCCCD.Text = khachHangHienTai.SoCCCD;
                cboLoaiGiayTo.Text = khachHangHienTai.LoaiGiayTo;

                if (khachHangHienTai.AnhGiayTo != null && khachHangHienTai.AnhGiayTo.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(khachHangHienTai.AnhGiayTo))
                    {
                        picAnhGiayTo.Image = Image.FromStream(ms);
                        imageData = khachHangHienTai.AnhGiayTo;
                    }
                }
            }
        }

        private void BtnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";
                ofd.Title = langMgr.GetString("SelectIDPhoto");

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        picAnhGiayTo.Image = Image.FromFile(ofd.FileName);
                        imageData = File.ReadAllBytes(ofd.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(langMgr.GetString("ErrorLoadingImage") + ": " + ex.Message,
                            langMgr.GetString("Error"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnXoaAnh_Click(object sender, EventArgs e)
        {
            picAnhGiayTo.Image = null;
            imageData = null;
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate dữ liệu
                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show(langMgr.GetString("PleaseEnterFullName"),
                        langMgr.GetString("Notification"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoTen.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    MessageBox.Show(langMgr.GetString("PleaseEnterPhoneNumber"),
                        langMgr.GetString("Notification"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSDT.Focus();
                    return;
                }

                // Tạo đối tượng khách hàng
                KhachHangDTO kh = new KhachHangDTO
                {
                    MaKH = txtMaKH.Text.Trim(),
                    HoTenKH = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = cboGioiTinh.Text,
                    Sdt = txtSDT.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    SoCCCD = txtSoCCCD.Text.Trim(),
                    LoaiGiayTo = null,
                    AnhGiayTo = null
                };

                string errorMessage;
                bool result;

                if (isEditMode)
                {
                    result = khachHangBLL.UpdateKhachHang(kh, out errorMessage);
                }
                else
                {
                    result = khachHangBLL.InsertKhachHang(kh, out errorMessage);
                }

                if (result)
                {
                    KhachHangMoi = kh;
                    MessageBox.Show(
                        isEditMode ? langMgr.GetString("UpdateCustomerSuccess") : langMgr.GetString("AddCustomerSuccess"),
                        langMgr.GetString("Success"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(errorMessage, langMgr.GetString("Error"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(langMgr.GetString("Error") + ": " + ex.Message,
                    langMgr.GetString("Error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}