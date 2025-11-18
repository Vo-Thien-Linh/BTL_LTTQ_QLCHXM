using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;

namespace UI.FormUI
{
    public partial class FormXemHopDongThue : Form
    {
        private int maGDThue;
        private string maNV;
        private GiaoDichThueBLL giaoDichThueBLL;
        private DataRow dataGiaoDich;

        public FormXemHopDongThue(int maGD, string maNhanVien)
        {
            InitializeComponent();
            this.maGDThue = maGD;
            this.maNV = maNhanVien;
            giaoDichThueBLL = new GiaoDichThueBLL();

            LoadData();
            ConfigureButtons();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = giaoDichThueBLL.GetDonChoThue();
                DataView dv = dt.DefaultView;
                dv.RowFilter = $"MaGDThue = {maGDThue}";

                if (dv.Count == 0)
                {
                    MessageBox.Show("Khong tim thay thong tin hop dong!", "Loi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                dataGiaoDich = dv[0].Row;
                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi tai du lieu: " + ex.Message, "Loi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void DisplayData()
        {
            // Thông tin giao dịch
            lblMaGD.Text = $"Ma GD: #{dataGiaoDich["MaGDThue"]}";

            // Thông tin xe
            txtTenXe.Text = dataGiaoDich["TenXe"].ToString();
            txtBienSo.Text = dataGiaoDich["BienSo"].ToString();

            // Thông tin khách hàng
            txtKhachHang.Text = dataGiaoDich["HoTenKH"].ToString();
            txtSDT.Text = dataGiaoDich["SdtKhachHang"].ToString();
            txtEmail.Text = dataGiaoDich["EmailKhachHang"]?.ToString();

            // Thông tin thuê
            dtpNgayBatDau.Value = Convert.ToDateTime(dataGiaoDich["NgayBatDau"]);
            dtpNgayKetThuc.Value = Convert.ToDateTime(dataGiaoDich["NgayKetThuc"]);
            txtSoNgayThue.Text = dataGiaoDich["SoNgayThue"].ToString();
            txtGiaThueNgay.Text = $"{Convert.ToDecimal(dataGiaoDich["GiaThueNgay"]):N0} VND";
            txtTongGia.Text = $"{Convert.ToDecimal(dataGiaoDich["TongGia"]):N0} VND";

            // Tiền cọc
            decimal soTienCoc = dataGiaoDich["SoTienCoc"] != DBNull.Value
                ? Convert.ToDecimal(dataGiaoDich["SoTienCoc"]) : 0;
            txtSoTienCoc.Text = $"{soTienCoc:N0} VND";

            // Giấy tờ giữ lại
            txtGiayToGiuLai.Text = dataGiaoDich["GiayToGiuLai"]?.ToString();

            // Trạng thái
            string trangThai = dataGiaoDich["TrangThai"].ToString();
            txtTrangThai.Text = trangThai;
            HighlightTrangThai(trangThai);

            string ttThanhToan = dataGiaoDich["TrangThaiThanhToan"].ToString();
            txtTrangThaiThanhToan.Text = ttThanhToan;
            HighlightThanhToan(ttThanhToan);

            txtHinhThucThanhToan.Text = dataGiaoDich["HinhThucThanhToan"]?.ToString();

            // Tính toán quá hạn
            int soNgayQuaHan = dataGiaoDich["SoNgayQuaHan"] != DBNull.Value
                ? Convert.ToInt32(dataGiaoDich["SoNgayQuaHan"]) : 0;

            if (soNgayQuaHan > 0)
            {
                decimal giaThueNgay = Convert.ToDecimal(dataGiaoDich["GiaThueNgay"]);
                decimal tienPhat = giaoDichThueBLL.TinhPhiPhat(
                    Convert.ToDateTime(dataGiaoDich["NgayKetThuc"]), giaThueNgay);

                txtSoNgayQuaHan.Text = soNgayQuaHan.ToString();
                txtTienPhat.Text = $"{tienPhat:N0} VND";

                // Highlight cảnh báo
                txtSoNgayQuaHan.BackColor = Color.FromArgb(255, 205, 210);
                txtTienPhat.BackColor = Color.FromArgb(255, 205, 210);

                lblWarning.Visible = true;
                lblWarning.Text = $"CANH BAO: Don thue qua han {soNgayQuaHan} ngay!";
            }
            else
            {
                txtSoNgayQuaHan.Text = "0";
                txtTienPhat.Text = "0 VND";
                lblWarning.Visible = false;
            }
        }

        private void HighlightTrangThai(string trangThai)
        {
            switch (trangThai)
            {
                case "Đang thuê":
                    txtTrangThai.BackColor = Color.FromArgb(200, 230, 201);
                    txtTrangThai.ForeColor = Color.FromArgb(56, 142, 60);
                    break;
                case "Chờ xác nhận":
                    txtTrangThai.BackColor = Color.FromArgb(255, 243, 205);
                    txtTrangThai.ForeColor = Color.FromArgb(255, 152, 0);
                    break;
                case "Đã thuê":
                    txtTrangThai.BackColor = Color.FromArgb(224, 224, 224);
                    txtTrangThai.ForeColor = Color.FromArgb(97, 97, 97);
                    break;
            }
        }

        private void HighlightThanhToan(string ttThanhToan)
        {
            if (ttThanhToan == "Đã thanh toán")
            {
                txtTrangThaiThanhToan.BackColor = Color.FromArgb(200, 230, 201);
                txtTrangThaiThanhToan.ForeColor = Color.FromArgb(56, 142, 60);
            }
            else
            {
                txtTrangThaiThanhToan.BackColor = Color.FromArgb(255, 243, 205);
                txtTrangThaiThanhToan.ForeColor = Color.FromArgb(255, 152, 0);
            }
        }

        private void ConfigureButtons()
        {
            string trangThai = dataGiaoDich["TrangThai"].ToString();
            string ttThanhToan = dataGiaoDich["TrangThaiThanhToan"].ToString();

            // Logic enable/disable buttons

            // Button Xác nhận Thanh toán
            btnXacNhanThanhToan.Enabled = (ttThanhToan == "Chưa thanh toán");

            // Button Giao xe
            btnGiaoXe.Enabled = (ttThanhToan == "Đã thanh toán" && trangThai != "Đang thuê");

            // Button Trả xe
            btnTraXe.Enabled = (trangThai == "Đang thuê");

            // Màu sắc buttons
            SetButtonStyle(btnXacNhanThanhToan, btnXacNhanThanhToan.Enabled);
            SetButtonStyle(btnGiaoXe, btnGiaoXe.Enabled);
            SetButtonStyle(btnTraXe, btnTraXe.Enabled);
        }

        private void SetButtonStyle(Button btn, bool enabled)
        {
            if (enabled)
            {
                btn.Cursor = Cursors.Hand;
                btn.ForeColor = Color.White;
            }
            else
            {
                btn.BackColor = Color.FromArgb(189, 189, 189);
                btn.Cursor = Cursors.No;
                btn.ForeColor = Color.FromArgb(117, 117, 117);
            }
        }

        private void btnXacNhanThanhToan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboHinhThucThanhToan.Text))
            {
                MessageBox.Show("Vui long chon hinh thuc thanh toan!", "Thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboHinhThucThanhToan.Focus();
                return;
            }

            var result = MessageBox.Show(
                $"Xac nhan khach hang da thanh toan?\nHinh thuc: {cboHinhThucThanhToan.Text}",
                "Xac nhan thanh toan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string errorMessage;
                    bool success = giaoDichThueBLL.XacNhanThanhToan(
                        maGDThue,
                        maNV,
                        cboHinhThucThanhToan.Text,
                        out errorMessage);

                    if (success)
                    {
                        MessageBox.Show("Xac nhan thanh toan thanh cong!", "Thanh cong",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadData();
                        ConfigureButtons();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Loi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Loi: " + ex.Message, "Loi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGiaoXe_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Xac nhan giao xe cho khach hang?\n\nSau khi giao, trang thai xe se chuyen thanh 'Dang thue'.",
                "Xac nhan giao xe",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string errorMessage;
                    bool success = giaoDichThueBLL.XacNhanGiaoXe(
                        maGDThue,
                        maNV,
                        out errorMessage);

                    if (success)
                    {
                        MessageBox.Show("Giao xe thanh cong!", "Thanh cong",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadData();
                        ConfigureButtons();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Loi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Loi: " + ex.Message, "Loi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTraXe_Click(object sender, EventArgs e)
        {
            using (FormTraXe formTraXe = new FormTraXe(dataGiaoDich))
            {
                if (formTraXe.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string errorMessage;
                        bool success = giaoDichThueBLL.XacNhanTraXe(
                            maGDThue,
                            maNV,
                            formTraXe.TinhTrangXe,
                            formTraXe.ChiPhiPhatSinh,
                            formTraXe.GhiChu,
                            out errorMessage);

                        if (success)
                        {
                            MessageBox.Show(
                                $"Tra xe thanh cong!\n\n" +
                                $"Tien hoan coc: {formTraXe.TienHoanCoc:N0} VND",
                                "Thanh cong",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(errorMessage, "Loi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Loi: " + ex.Message, "Loi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    // ===== FORM PHỤ: FormTraXe - Nhập thông tin khi trả xe =====
    public class FormTraXe : Form
    {
        private ComboBox cboTinhTrang;
        private NumericUpDown nudChiPhiPhatSinh;
        private TextBox txtGhiChu;
        private Label lblTienPhat;
        private Label lblTienHoanCoc;
        private Button btnXacNhan;
        private Button btnHuy;

        private DataRow dataGiaoDich;
        private decimal soTienCoc;
        private decimal giaThueNgay;
        private DateTime ngayKetThuc;

        public string TinhTrangXe { get; private set; }
        public decimal ChiPhiPhatSinh { get; private set; }
        public string GhiChu { get; private set; }
        public decimal TienHoanCoc { get; private set; }

        public FormTraXe(DataRow data)
        {
            this.dataGiaoDich = data;
            this.soTienCoc = data["SoTienCoc"] != DBNull.Value
                ? Convert.ToDecimal(data["SoTienCoc"]) : 0;
            this.giaThueNgay = Convert.ToDecimal(data["GiaThueNgay"]);
            this.ngayKetThuc = Convert.ToDateTime(data["NgayKetThuc"]);

            InitializeComponents();
            TinhToan();
        }

        private void InitializeComponents()
        {
            this.Text = "Xac nhan tra xe";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int yPos = 20;

            // Title
            Label lblTitle = new Label
            {
                Text = "THONG TIN TRA XE",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                Location = new Point(20, yPos),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);
            yPos += 40;

            // Tình trạng xe
            Label lbl1 = new Label
            {
                Text = "Tinh trang xe:",
                Location = new Point(20, yPos),
                Width = 150,
                Font = new Font("Segoe UI", 10F)
            };
            this.Controls.Add(lbl1);

            cboTinhTrang = new ComboBox
            {
                Location = new Point(180, yPos - 3),
                Width = 280,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cboTinhTrang.Items.AddRange(new object[] { "Tot", "Tray xuoc nhe", "Hu hong" });
            cboTinhTrang.SelectedIndex = 0;
            this.Controls.Add(cboTinhTrang);
            yPos += 40;

            // Chi phí phát sinh
            Label lbl2 = new Label
            {
                Text = "Chi phi phat sinh:",
                Location = new Point(20, yPos),
                Width = 150,
                Font = new Font("Segoe UI", 10F)
            };
            this.Controls.Add(lbl2);

            nudChiPhiPhatSinh = new NumericUpDown
            {
                Location = new Point(180, yPos - 3),
                Width = 280,
                Maximum = 100000000,
                Minimum = 0,
                Value = 0,
                ThousandsSeparator = true,
                Font = new Font("Segoe UI", 10F)
            };
            nudChiPhiPhatSinh.ValueChanged += (s, e) => TinhToan();
            this.Controls.Add(nudChiPhiPhatSinh);
            yPos += 40;

            // Tiền phạt
            Label lbl3 = new Label
            {
                Text = "Tien phat (qua han):",
                Location = new Point(20, yPos),
                Width = 150,
                Font = new Font("Segoe UI", 10F)
            };
            this.Controls.Add(lbl3);

            lblTienPhat = new Label
            {
                Location = new Point(180, yPos),
                Width = 280,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(244, 67, 54),
                Text = "0 VND"
            };
            this.Controls.Add(lblTienPhat);
            yPos += 40;

            // Tiền hoàn cọc
            Label lbl4 = new Label
            {
                Text = "Tien hoan coc:",
                Location = new Point(20, yPos),
                Width = 150,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            this.Controls.Add(lbl4);

            lblTienHoanCoc = new Label
            {
                Location = new Point(180, yPos),
                Width = 280,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(76, 175, 80),
                Text = "0 VND"
            };
            this.Controls.Add(lblTienHoanCoc);
            yPos += 50;

            // Ghi chú
            Label lbl5 = new Label
            {
                Text = "Ghi chu:",
                Location = new Point(20, yPos),
                Width = 150,
                Font = new Font("Segoe UI", 10F)
            };
            this.Controls.Add(lbl5);

            txtGhiChu = new TextBox
            {
                Location = new Point(180, yPos - 3),
                Width = 280,
                Height = 80,
                Multiline = true,
                Font = new Font("Segoe UI", 10F)
            };
            this.Controls.Add(txtGhiChu);
            yPos += 100;

            // Buttons
            btnXacNhan = new Button
            {
                Text = "XAC NHAN",
                Location = new Point(180, yPos),
                Width = 130,
                Height = 40,
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnXacNhan.FlatAppearance.BorderSize = 0;
            btnXacNhan.Click += BtnXacNhan_Click;
            this.Controls.Add(btnXacNhan);

            btnHuy = new Button
            {
                Text = "HUY",
                Location = new Point(330, yPos),
                Width = 130,
                Height = 40,
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnHuy);
        }

        private void TinhToan()
        {
            GiaoDichThueBLL bll = new GiaoDichThueBLL();

            // Tính tiền phạt
            decimal tienPhat = bll.TinhPhiPhat(ngayKetThuc, giaThueNgay);
            lblTienPhat.Text = $"{tienPhat:N0} VND";

            // Tính tiền hoàn cọc
            decimal chiPhiPhatSinh = nudChiPhiPhatSinh.Value;
            TienHoanCoc = bll.TinhTienHoanCoc(soTienCoc, chiPhiPhatSinh, tienPhat);
            lblTienHoanCoc.Text = $"{TienHoanCoc:N0} VND";

            if (TienHoanCoc < 0)
            {
                lblTienHoanCoc.ForeColor = Color.FromArgb(244, 67, 54);
                lblTienHoanCoc.Text += " (Khach phai tra them)";
            }
            else
            {
                lblTienHoanCoc.ForeColor = Color.FromArgb(76, 175, 80);
            }
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            TinhTrangXe = cboTinhTrang.Text;
            ChiPhiPhatSinh = nudChiPhiPhatSinh.Value;
            GhiChu = txtGhiChu.Text.Trim();

            this.DialogResult = DialogResult.OK;
        }
    }
}