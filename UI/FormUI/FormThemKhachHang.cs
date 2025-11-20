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

        public KhachHangDTO KhachHangMoi { get; private set; }

        public FormThemKhachHang()
        {
            InitializeComponent();
            khachHangBLL = new KhachHangBLL();
            isEditMode = false;
            InitializeCustomComponents();
            LoadNewCustomer();
        }

        public FormThemKhachHang(KhachHangDTO kh)
        {
            InitializeComponent();
            khachHangBLL = new KhachHangBLL();
            isEditMode = true;
            khachHangHienTai = kh;
            InitializeCustomComponents();
            LoadCustomerData();
        }

        private void InitializeCustomComponents()
        {
            this.Text = isEditMode ? "Sửa Thông Tin Khách Hàng" : "Thêm Khách Hàng Mới";
            this.Size = new Size(800, 700);
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
            Label lblTitle = new Label
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
            AddLabel(mainPanel, "Mã Khách Hàng:", yPos);
            txtMaKH = AddTextBox(mainPanel, yPos);
            txtMaKH.ReadOnly = true;
            txtMaKH.BackColor = Color.FromArgb(240, 240, 240);
            yPos += 50;

            // Họ tên
            AddLabel(mainPanel, "Họ Tên: *", yPos);
            txtHoTen = AddTextBox(mainPanel, yPos);
            yPos += 50;

            // Ngày sinh & Giới tính (cùng hàng)
            AddLabel(mainPanel, "Ngày Sinh:", yPos, 20);
            dtpNgaySinh = new DateTimePicker
            {
                Location = new Point(150, yPos),
                Width = 200,
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10F)
            };
            mainPanel.Controls.Add(dtpNgaySinh);

            Label lblGioiTinh = new Label
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
            AddLabel(mainPanel, "Số Điện Thoại: *", yPos);
            txtSDT = AddTextBox(mainPanel, yPos);
            yPos += 50;

            // Email
            AddLabel(mainPanel, "Email:", yPos);
            txtEmail = AddTextBox(mainPanel, yPos);
            yPos += 50;

            // Địa chỉ
            AddLabel(mainPanel, "Địa Chỉ:", yPos);
            txtDiaChi = AddTextBox(mainPanel, yPos, true);
            yPos += 70;

            // Số CCCD
            AddLabel(mainPanel, "Số CCCD/CMND:", yPos);
            txtSoCCCD = AddTextBox(mainPanel, yPos);
            yPos += 50;

            // Loại giấy tờ
            AddLabel(mainPanel, "Loại Giấy Tờ:", yPos);
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
            AddLabel(mainPanel, "Ảnh Giấy Tờ:", yPos);
            
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
                Width = 80,
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
                Location = new Point(650, yPos),
                Width = 80,
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
        }

        private void AddLabel(Panel parent, string text, int yPos, int xPos = 20)
        {
            Label lbl = new Label
            {
                Text = text,
                Location = new Point(xPos, yPos + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular)
            };
            parent.Controls.Add(lbl);
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
            // Tự động tạo mã khách hàng mới
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
                ofd.Title = "Chọn Ảnh Giấy Tờ";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        picAnhGiayTo.Image = Image.FromFile(ofd.FileName);
                        imageData = File.ReadAllBytes(ofd.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message, "Lỗi",
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
                    MessageBox.Show("Vui lòng nhập họ tên khách hàng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoTen.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo",
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
                    LoaiGiayTo = cboLoaiGiayTo.Text,
                    AnhGiayTo = imageData
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
                    MessageBox.Show(isEditMode ? "Cập nhật khách hàng thành công!" : "Thêm khách hàng thành công!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
