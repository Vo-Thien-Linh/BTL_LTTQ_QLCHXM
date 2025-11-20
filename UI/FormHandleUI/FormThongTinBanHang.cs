using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;
using UI.FormUI;

namespace UI.FormHandleUI
{
    public partial class FormThongTinBanHang : Form
    {
        private string idXe;
        private string maNhanVien;
        private XeMayBLL xeMayBLL;
        private KhachHangBLL khachHangBLL;
        private GiaoDichBanBLL giaoDichBanBLL;
        private KhachHangDTO khachHangHienTai;

        // Controls - Thông tin xe
        private GroupBox groupBox1;
        private TextBox txtMaXe;
        private TextBox txtTenXe;
        private TextBox txtBienSo;
        private TextBox txtGiaBan;

        // Controls - Thông tin khách hàng (programmatic)
        private GroupBox groupBox2;
        private TextBox txtTimKiemSDT;
        private Button btnThemKH;
        private TextBox txtMaKH;
        private TextBox txtHoTen;
        private TextBox txtEmail;
        private TextBox txtDiaChi;
        private TextBox txtSoCCCD;

        // Controls - Thông tin giao dịch
        private GroupBox groupBox3;
        private ComboBox cboHinhThucThanhToan;
        private DateTimePicker dtpNgayBan;
        private TextBox txtGhiChu;
        private Button btnXacNhanBan;
        private Button btnHuy;

        public FormThongTinBanHang(string idXe, string maNV)
        {
            InitializeComponent();
            this.idXe = idXe;
            this.maNhanVien = maNV;

            xeMayBLL = new XeMayBLL();
            khachHangBLL = new KhachHangBLL();
            giaoDichBanBLL = new GiaoDichBanBLL();

            InitializeCustomComponents();
            LoadXeInfo();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Thông Tin Bán Hàng";
            this.Size = new Size(900, 750);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Main panel
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White,
                AutoScroll = true
            };
            this.Controls.Add(mainPanel);

            // Title
            Label lblTitle = new Label
            {
                Text = "THÔNG TIN BÁN HÀNG",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                Location = new Point(20, 10),
                AutoSize = true
            };
            mainPanel.Controls.Add(lblTitle);

            int yPos = 60;

            // ============ GROUP 1: THÔNG TIN XE ============
            groupBox1 = new GroupBox
            {
                Text = "Thông Tin Xe",
                Location = new Point(20, yPos),
                Width = 830,
                Height = 150,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            mainPanel.Controls.Add(groupBox1);

            AddLabelTextBox(groupBox1, "Mã Xe:", ref txtMaXe, 30, true);
            AddLabelTextBox(groupBox1, "Tên Xe:", ref txtTenXe, 65, true);
            AddLabelTextBox(groupBox1, "Biển Số:", ref txtBienSo, 100, true);

            Label lblGiaBan = new Label
            {
                Text = "Giá Bán:",
                Location = new Point(450, 35),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F)
            };
            groupBox1.Controls.Add(lblGiaBan);

            txtGiaBan = new TextBox
            {
                Location = new Point(560, 30),
                Width = 250,
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(244, 67, 54)
            };
            groupBox1.Controls.Add(txtGiaBan);

            yPos += 160;

            // ============ GROUP 2: THÔNG TIN KHÁCH HÀNG (với programmatic controls) ============
            groupBox2 = new GroupBox
            {
                Text = "Thông Tin Khách Hàng",
                Location = new Point(20, yPos),
                Width = 830,
                Height = 250,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            mainPanel.Controls.Add(groupBox2);

            // Thêm controls programmatically
            AddSDTControl();

            yPos += 260;

            // ============ GROUP 3: THÔNG TIN GIAO DỊCH ============
            groupBox3 = new GroupBox
            {
                Text = "Thông Tin Giao Dịch",
                Location = new Point(20, yPos),
                Width = 830,
                Height = 200,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            mainPanel.Controls.Add(groupBox3);

            // Ngày bán
            Label lblNgayBan = new Label
            {
                Text = "Ngày Bán:",
                Location = new Point(30, 35),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F)
            };
            groupBox3.Controls.Add(lblNgayBan);

            dtpNgayBan = new DateTimePicker
            {
                Location = new Point(180, 30),
                Width = 250,
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10F)
            };
            groupBox3.Controls.Add(dtpNgayBan);

            // Hình thức thanh toán
            Label lblHTTT = new Label
            {
                Text = "Hình Thức TT:",
                Location = new Point(30, 75),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F)
            };
            groupBox3.Controls.Add(lblHTTT);

            cboHinhThucThanhToan = new ComboBox
            {
                Location = new Point(180, 70),
                Width = 620,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cboHinhThucThanhToan.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản", "Thẻ tín dụng" });
            cboHinhThucThanhToan.SelectedIndex = 0;
            groupBox3.Controls.Add(cboHinhThucThanhToan);

            // Ghi chú
            Label lblGhiChu = new Label
            {
                Text = "Ghi Chú:",
                Location = new Point(30, 115),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F)
            };
            groupBox3.Controls.Add(lblGhiChu);

            txtGhiChu = new TextBox
            {
                Location = new Point(180, 110),
                Width = 620,
                Height = 60,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 10F)
            };
            groupBox3.Controls.Add(txtGhiChu);

            yPos += 210;

            // Buttons
            btnXacNhanBan = new Button
            {
                Text = "Xác Nhận Bán",
                Location = new Point(580, yPos),
                Width = 130,
                Height = 45,
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnXacNhanBan.FlatAppearance.BorderSize = 0;
            btnXacNhanBan.Click += BtnXacNhanBan_Click;
            mainPanel.Controls.Add(btnXacNhanBan);

            btnHuy = new Button
            {
                Text = "Hủy",
                Location = new Point(720, yPos),
                Width = 130,
                Height = 45,
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            mainPanel.Controls.Add(btnHuy);
        }

        private void AddSDTControl()
        {
            // Tìm kiếm theo SĐT
            Label lblSDT = new Label
            {
                Text = "Số Điện Thoại: *",
                Location = new Point(30, 35),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            groupBox2.Controls.Add(lblSDT);

            txtTimKiemSDT = new TextBox
            {
                Location = new Point(180, 30),
                Width = 250,
                Font = new Font("Segoe UI", 10F),
                Name = "txtTimKiemSDT"
            };
            txtTimKiemSDT.Leave += TxtTimKiemSDT_Leave;
            groupBox2.Controls.Add(txtTimKiemSDT);

            btnThemKH = new Button
            {
                Text = "Thêm KH Mới",
                Location = new Point(440, 28),
                Width = 120,
                Height = 30,
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnThemKH.FlatAppearance.BorderSize = 0;
            btnThemKH.Click += BtnThemKH_Click;
            groupBox2.Controls.Add(btnThemKH);

            // Các textbox thông tin khách hàng
            AddLabelTextBox(groupBox2, "Mã KH:", ref txtMaKH, 70, true);
            AddLabelTextBox(groupBox2, "Họ Tên:", ref txtHoTen, 105, true);
            AddLabelTextBox(groupBox2, "Email:", ref txtEmail, 140, true);
            AddLabelTextBox(groupBox2, "Địa Chỉ:", ref txtDiaChi, 175, true);
            AddLabelTextBox(groupBox2, "Số CCCD:", ref txtSoCCCD, 210, true);
        }

        private void AddLabelTextBox(GroupBox parent, string labelText, ref TextBox textBox, int yPos, bool readOnly = false)
        {
            Label lbl = new Label
            {
                Text = labelText,
                Location = new Point(30, yPos + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F)
            };
            parent.Controls.Add(lbl);

            textBox = new TextBox
            {
                Location = new Point(180, yPos),
                Width = 620,
                Font = new Font("Segoe UI", 10F),
                ReadOnly = readOnly
            };

            if (readOnly)
            {
                textBox.BackColor = Color.FromArgb(240, 240, 240);
            }

            parent.Controls.Add(textBox);
        }

        private void LoadXeInfo()
        {
            try
            {
                DataTable dtGia = xeMayBLL.GetGiaXe(idXe, "Bán");
                if (dtGia != null && dtGia.Rows.Count > 0)
                {
                    DataRow row = dtGia.Rows[0];
                    txtMaXe.Text = idXe;
                    txtTenXe.Text = $"{row["TenHang"]} {row["TenDong"]} - {row["TenMau"]}";
                    txtBienSo.Text = row["BienSo"]?.ToString() ?? "Chưa có";

                    decimal giaBan = row["GiaBan"] != DBNull.Value ? Convert.ToDecimal(row["GiaBan"]) : 0;
                    txtGiaBan.Text = giaBan.ToString("N0") + " VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin xe: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtTimKiemSDT_Leave(object sender, EventArgs e)
        {
            string sdt = txtTimKiemSDT.Text.Trim();

            if (string.IsNullOrEmpty(sdt))
                return;

            khachHangHienTai = khachHangBLL.GetKhachHangBySdt(sdt);

            if (khachHangHienTai != null)
            {
                txtMaKH.Text = khachHangHienTai.MaKH;
                txtHoTen.Text = khachHangHienTai.HoTenKH;
                txtEmail.Text = khachHangHienTai.Email;
                txtDiaChi.Text = khachHangHienTai.DiaChi;
                txtSoCCCD.Text = khachHangHienTai.SoCCCD;
            }
            else
            {
                DialogResult result = MessageBox.Show(
                    "Không tìm thấy khách hàng.\nBạn có muốn thêm khách hàng mới?",
                    "Thông báo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    BtnThemKH_Click(sender, e);
                }
            }
        }

        private void BtnThemKH_Click(object sender, EventArgs e)
        {
            FormThemKhachHang formThem = new FormThemKhachHang();

            if (formThem.ShowDialog() == DialogResult.OK)
            {
                khachHangHienTai = formThem.KhachHangMoi;

                if (khachHangHienTai != null)
                {
                    txtTimKiemSDT.Text = khachHangHienTai.Sdt;
                    txtMaKH.Text = khachHangHienTai.MaKH;
                    txtHoTen.Text = khachHangHienTai.HoTenKH;
                    txtEmail.Text = khachHangHienTai.Email;
                    txtDiaChi.Text = khachHangHienTai.DiaChi;
                    txtSoCCCD.Text = khachHangHienTai.SoCCCD;

                    MessageBox.Show("Đã thêm khách hàng mới!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // Focus lại vào ô SĐT
            txtTimKiemSDT.Focus();
        }

        private void BtnXacNhanBan_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate
                if (khachHangHienTai == null || string.IsNullOrEmpty(txtMaKH.Text))
                {
                    MessageBox.Show("Vui lòng tìm hoặc thêm khách hàng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTimKiemSDT.Focus();
                    return;
                }

                // Lấy giá bán từ textbox
                string giaBanText = txtGiaBan.Text.Replace(" VNĐ", "").Replace(",", "");
                if (!decimal.TryParse(giaBanText, out decimal giaBan))
                {
                    MessageBox.Show("Giá bán không hợp lệ!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo giao dịch bán
                GiaoDichBan gd = new GiaoDichBan
                {
                    MaKH = khachHangHienTai.MaKH,
                    ID_Xe = idXe,
                    NgayBan = dtpNgayBan.Value,
                    GiaBan = giaBan,
                    TrangThaiThanhToan = "Đã thanh toán",
                    HinhThucThanhToan = cboHinhThucThanhToan.Text,
                    MaTaiKhoan = maNhanVien
                };

                string errorMsg;
                int maGDBan = giaoDichBanBLL.InsertGiaoDichBan(gd, out errorMsg);

                if (maGDBan > 0)
                {
                    MessageBox.Show(
                        "Bán xe thành công!\n" +
                        $"Khách hàng: {khachHangHienTai.HoTenKH}\n" +
                        $"Xe: {txtTenXe.Text}\n" +
                        $"Giá bán: {giaBan:N0} VNĐ\n" +
                        $"Mã giao dịch: {maGDBan}",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lỗi: " + errorMsg, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
