namespace UI.FormUI
{
    partial class FormXemHopDongThue
    {
        private System.ComponentModel.IContainer components = null;

        // Top Panel
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMaGD;
        private System.Windows.Forms.Label lblWarning;

        // Info GroupBoxes
        private System.Windows.Forms.GroupBox grpThongTinXe;
        private System.Windows.Forms.GroupBox grpThongTinKhach;
        private System.Windows.Forms.GroupBox grpThongTinThue;
        private System.Windows.Forms.GroupBox grpTinhToan;

        // Thông tin xe
        private System.Windows.Forms.Label lblTenXe;
        private System.Windows.Forms.TextBox txtTenXe;
        private System.Windows.Forms.Label lblBienSo;
        private System.Windows.Forms.TextBox txtBienSo;

        // Thông tin khách
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.TextBox txtKhachHang;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;

        // Thông tin thuê
        private System.Windows.Forms.Label lblNgayBatDau;
        private System.Windows.Forms.DateTimePicker dtpNgayBatDau;
        private System.Windows.Forms.Label lblNgayKetThuc;
        private System.Windows.Forms.DateTimePicker dtpNgayKetThuc;
        private System.Windows.Forms.Label lblSoNgayThue;
        private System.Windows.Forms.TextBox txtSoNgayThue;
        private System.Windows.Forms.Label lblGiaThueNgay;
        private System.Windows.Forms.TextBox txtGiaThueNgay;
        private System.Windows.Forms.Label lblTongGia;
        private System.Windows.Forms.TextBox txtTongGia;
        private System.Windows.Forms.Label lblSoTienCoc;
        private System.Windows.Forms.TextBox txtSoTienCoc;
        private System.Windows.Forms.Label lblGiayToGiuLai;
        private System.Windows.Forms.TextBox txtGiayToGiuLai;

        // Trạng thái
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.TextBox txtTrangThai;
        private System.Windows.Forms.Label lblTrangThaiThanhToan;
        private System.Windows.Forms.TextBox txtTrangThaiThanhToan;
        private System.Windows.Forms.Label lblHinhThucThanhToan;
        private System.Windows.Forms.TextBox txtHinhThucThanhToan;

        // Tính toán
        private System.Windows.Forms.Label lblSoNgayQuaHan;
        private System.Windows.Forms.TextBox txtSoNgayQuaHan;
        private System.Windows.Forms.Label lblTienPhat;
        private System.Windows.Forms.TextBox txtTienPhat;

        // Buttons Panel
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Label lblHinhThucThanhToanNew;
        private System.Windows.Forms.ComboBox cboHinhThucThanhToan;
        private System.Windows.Forms.Button btnXacNhanThanhToan;
        private System.Windows.Forms.Button btnGiaoXe;
        private System.Windows.Forms.Button btnTraXe;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMaGD = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.grpThongTinXe = new System.Windows.Forms.GroupBox();
            this.lblTenXe = new System.Windows.Forms.Label();
            this.txtTenXe = new System.Windows.Forms.TextBox();
            this.lblBienSo = new System.Windows.Forms.Label();
            this.txtBienSo = new System.Windows.Forms.TextBox();
            this.grpThongTinKhach = new System.Windows.Forms.GroupBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.txtKhachHang = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.grpThongTinThue = new System.Windows.Forms.GroupBox();
            this.lblNgayBatDau = new System.Windows.Forms.Label();
            this.dtpNgayBatDau = new System.Windows.Forms.DateTimePicker();
            this.lblNgayKetThuc = new System.Windows.Forms.Label();
            this.dtpNgayKetThuc = new System.Windows.Forms.DateTimePicker();
            this.lblSoNgayThue = new System.Windows.Forms.Label();
            this.txtSoNgayThue = new System.Windows.Forms.TextBox();
            this.lblGiaThueNgay = new System.Windows.Forms.Label();
            this.txtGiaThueNgay = new System.Windows.Forms.TextBox();
            this.lblTongGia = new System.Windows.Forms.Label();
            this.txtTongGia = new System.Windows.Forms.TextBox();
            this.lblSoTienCoc = new System.Windows.Forms.Label();
            this.txtSoTienCoc = new System.Windows.Forms.TextBox();
            this.lblGiayToGiuLai = new System.Windows.Forms.Label();
            this.txtGiayToGiuLai = new System.Windows.Forms.TextBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.txtTrangThai = new System.Windows.Forms.TextBox();
            this.lblTrangThaiThanhToan = new System.Windows.Forms.Label();
            this.txtTrangThaiThanhToan = new System.Windows.Forms.TextBox();
            this.lblHinhThucThanhToan = new System.Windows.Forms.Label();
            this.txtHinhThucThanhToan = new System.Windows.Forms.TextBox();
            this.grpTinhToan = new System.Windows.Forms.GroupBox();
            this.lblSoNgayQuaHan = new System.Windows.Forms.Label();
            this.txtSoNgayQuaHan = new System.Windows.Forms.TextBox();
            this.lblTienPhat = new System.Windows.Forms.Label();
            this.txtTienPhat = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.lblHinhThucThanhToanNew = new System.Windows.Forms.Label();
            this.cboHinhThucThanhToan = new System.Windows.Forms.ComboBox();
            this.btnXacNhanThanhToan = new System.Windows.Forms.Button();
            this.btnGiaoXe = new System.Windows.Forms.Button();
            this.btnTraXe = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.grpThongTinXe.SuspendLayout();
            this.grpThongTinKhach.SuspendLayout();
            this.grpThongTinThue.SuspendLayout();
            this.grpTinhToan.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.lblMaGD);
            this.panelTop.Controls.Add(this.lblWarning);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 80);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THONG TIN HOP DONG THUE";
            // 
            // lblMaGD
            // 
            this.lblMaGD.AutoSize = true;
            this.lblMaGD.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMaGD.ForeColor = System.Drawing.Color.White;
            this.lblMaGD.Location = new System.Drawing.Point(22, 50);
            this.lblMaGD.Name = "lblMaGD";
            this.lblMaGD.Size = new System.Drawing.Size(100, 20);
            this.lblMaGD.TabIndex = 1;
            this.lblMaGD.Text = "Ma GD: #000";
            // 
            // lblWarning
            // 
            this.lblWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWarning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblWarning.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblWarning.ForeColor = System.Drawing.Color.White;
            this.lblWarning.Location = new System.Drawing.Point(650, 25);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblWarning.Size = new System.Drawing.Size(330, 35);
            this.lblWarning.TabIndex = 2;
            this.lblWarning.Text = "CANH BAO: Qua han!";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWarning.Visible = false;
            // 
            // grpThongTinXe
            // 
            this.grpThongTinXe.Controls.Add(this.lblTenXe);
            this.grpThongTinXe.Controls.Add(this.txtTenXe);
            this.grpThongTinXe.Controls.Add(this.lblBienSo);
            this.grpThongTinXe.Controls.Add(this.txtBienSo);
            this.grpThongTinXe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpThongTinXe.Location = new System.Drawing.Point(20, 100);
            this.grpThongTinXe.Name = "grpThongTinXe";
            this.grpThongTinXe.Size = new System.Drawing.Size(470, 110);
            this.grpThongTinXe.TabIndex = 1;
            this.grpThongTinXe.TabStop = false;
            this.grpThongTinXe.Text = "Thong tin xe";
            // 
            // lblTenXe
            // 
            this.lblTenXe.AutoSize = true;
            this.lblTenXe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenXe.Location = new System.Drawing.Point(15, 30);
            this.lblTenXe.Name = "lblTenXe";
            this.lblTenXe.Size = new System.Drawing.Size(50, 15);
            this.lblTenXe.TabIndex = 0;
            this.lblTenXe.Text = "Ten xe:";
            // 
            // txtTenXe
            // 
            this.txtTenXe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenXe.Location = new System.Drawing.Point(120, 27);
            this.txtTenXe.Name = "txtTenXe";
            this.txtTenXe.ReadOnly = true;
            this.txtTenXe.Size = new System.Drawing.Size(330, 23);
            this.txtTenXe.TabIndex = 1;
            // 
            // lblBienSo
            // 
            this.lblBienSo.AutoSize = true;
            this.lblBienSo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBienSo.Location = new System.Drawing.Point(15, 65);
            this.lblBienSo.Name = "lblBienSo";
            this.lblBienSo.Size = new System.Drawing.Size(52, 15);
            this.lblBienSo.TabIndex = 2;
            this.lblBienSo.Text = "Bien so:";
            // 
            // txtBienSo
            // 
            this.txtBienSo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBienSo.Location = new System.Drawing.Point(120, 62);
            this.txtBienSo.Name = "txtBienSo";
            this.txtBienSo.ReadOnly = true;
            this.txtBienSo.Size = new System.Drawing.Size(330, 23);
            this.txtBienSo.TabIndex = 3;
            // 
            // grpThongTinKhach
            // 
            this.grpThongTinKhach.Controls.Add(this.lblKhachHang);
            this.grpThongTinKhach.Controls.Add(this.txtKhachHang);
            this.grpThongTinKhach.Controls.Add(this.lblSDT);
            this.grpThongTinKhach.Controls.Add(this.txtSDT);
            this.grpThongTinKhach.Controls.Add(this.lblEmail);
            this.grpThongTinKhach.Controls.Add(this.txtEmail);
            this.grpThongTinKhach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpThongTinKhach.Location = new System.Drawing.Point(510, 100);
            this.grpThongTinKhach.Name = "grpThongTinKhach";
            this.grpThongTinKhach.Size = new System.Drawing.Size(470, 150);
            this.grpThongTinKhach.TabIndex = 2;
            this.grpThongTinKhach.TabStop = false;
            this.grpThongTinKhach.Text = "Thong tin khach hang";
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKhachHang.Location = new System.Drawing.Point(15, 30);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(81, 15);
            this.lblKhachHang.TabIndex = 0;
            this.lblKhachHang.Text = "Khach hang:";
            // 
            // txtKhachHang
            // 
            this.txtKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtKhachHang.Location = new System.Drawing.Point(120, 27);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.ReadOnly = true;
            this.txtKhachHang.Size = new System.Drawing.Size(330, 23);
            this.txtKhachHang.TabIndex = 1;
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSDT.Location = new System.Drawing.Point(15, 65);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(33, 15);
            this.lblSDT.TabIndex = 2;
            this.lblSDT.Text = "SDT:";
            // 
            // txtSDT
            // 
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSDT.Location = new System.Drawing.Point(120, 62);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.ReadOnly = true;
            this.txtSDT.Size = new System.Drawing.Size(330, 23);
            this.txtSDT.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmail.Location = new System.Drawing.Point(15, 100);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 15);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.Location = new System.Drawing.Point(120, 97);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(330, 23);
            this.txtEmail.TabIndex = 5;
            // 
            // grpThongTinThue
            // 
            this.grpThongTinThue.Controls.Add(this.lblNgayBatDau);
            this.grpThongTinThue.Controls.Add(this.dtpNgayBatDau);
            this.grpThongTinThue.Controls.Add(this.lblNgayKetThuc);
            this.grpThongTinThue.Controls.Add(this.dtpNgayKetThuc);
            this.grpThongTinThue.Controls.Add(this.lblSoNgayThue);
            this.grpThongTinThue.Controls.Add(this.txtSoNgayThue);
            this.grpThongTinThue.Controls.Add(this.lblGiaThueNgay);
            this.grpThongTinThue.Controls.Add(this.txtGiaThueNgay);
            this.grpThongTinThue.Controls.Add(this.lblTongGia);
            this.grpThongTinThue.Controls.Add(this.txtTongGia);
            this.grpThongTinThue.Controls.Add(this.lblSoTienCoc);
            this.grpThongTinThue.Controls.Add(this.txtSoTienCoc);
            this.grpThongTinThue.Controls.Add(this.lblGiayToGiuLai);
            this.grpThongTinThue.Controls.Add(this.txtGiayToGiuLai);
            this.grpThongTinThue.Controls.Add(this.lblTrangThai);
            this.grpThongTinThue.Controls.Add(this.txtTrangThai);
            this.grpThongTinThue.Controls.Add(this.lblTrangThaiThanhToan);
            this.grpThongTinThue.Controls.Add(this.txtTrangThaiThanhToan);
            this.grpThongTinThue.Controls.Add(this.lblHinhThucThanhToan);
            this.grpThongTinThue.Controls.Add(this.txtHinhThucThanhToan);
            this.grpThongTinThue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpThongTinThue.Location = new System.Drawing.Point(20, 220);
            this.grpThongTinThue.Name = "grpThongTinThue";
            this.grpThongTinThue.Size = new System.Drawing.Size(470, 380);
            this.grpThongTinThue.TabIndex = 3;
            this.grpThongTinThue.TabStop = false;
            this.grpThongTinThue.Text = "Thong tin thue";
            // 
            // lblNgayBatDau
            // 
            this.lblNgayBatDau.AutoSize = true;
            this.lblNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayBatDau.Location = new System.Drawing.Point(15, 30);
            this.lblNgayBatDau.Name = "lblNgayBatDau";
            this.lblNgayBatDau.Size = new System.Drawing.Size(84, 15);
            this.lblNgayBatDau.TabIndex = 0;
            this.lblNgayBatDau.Text = "Ngay bat dau:";
            // 
            // dtpNgayBatDau
            // 
            this.dtpNgayBatDau.Enabled = false;
            this.dtpNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBatDau.Location = new System.Drawing.Point(150, 27);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(300, 23);
            this.dtpNgayBatDau.TabIndex = 1;
            // 
            // lblNgayKetThuc
            // 
            this.lblNgayKetThuc.AutoSize = true;
            this.lblNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayKetThuc.Location = new System.Drawing.Point(15, 60);
            this.lblNgayKetThuc.Name = "lblNgayKetThuc";
            this.lblNgayKetThuc.Size = new System.Drawing.Size(88, 15);
            this.lblNgayKetThuc.TabIndex = 2;
            this.lblNgayKetThuc.Text = "Ngay ket thuc:";
            // 
            // dtpNgayKetThuc
            // 
            this.dtpNgayKetThuc.Enabled = false;
            this.dtpNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayKetThuc.Location = new System.Drawing.Point(150, 57);
            this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            this.dtpNgayKetThuc.Size = new System.Drawing.Size(300, 23);
            this.dtpNgayKetThuc.TabIndex = 3;
            // 
            // lblSoNgayThue
            // 
            this.lblSoNgayThue.AutoSize = true;
            this.lblSoNgayThue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoNgayThue.Location = new System.Drawing.Point(15, 90);
            this.lblSoNgayThue.Name = "lblSoNgayThue";
            this.lblSoNgayThue.Size = new System.Drawing.Size(84, 15);
            this.lblSoNgayThue.TabIndex = 4;
            this.lblSoNgayThue.Text = "So ngay thue:";
            // 
            // txtSoNgayThue
            // 
            this.txtSoNgayThue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSoNgayThue.Location = new System.Drawing.Point(150, 87);
            this.txtSoNgayThue.Name = "txtSoNgayThue";
            this.txtSoNgayThue.ReadOnly = true;
            this.txtSoNgayThue.Size = new System.Drawing.Size(300, 23);
            this.txtSoNgayThue.TabIndex = 5;
            // 
            // lblGiaThueNgay
            // 
            this.lblGiaThueNgay.AutoSize = true;
            this.lblGiaThueNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGiaThueNgay.Location = new System.Drawing.Point(15, 120);
            this.lblGiaThueNgay.Name = "lblGiaThueNgay";
            this.lblGiaThueNgay.Size = new System.Drawing.Size(89, 15);
            this.lblGiaThueNgay.TabIndex = 6;
            this.lblGiaThueNgay.Text = "Gia thue/ngay:";
            // 
            // txtGiaThueNgay
            // 
            this.txtGiaThueNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtGiaThueNgay.Location = new System.Drawing.Point(150, 117);
            this.txtGiaThueNgay.Name = "txtGiaThueNgay";
            this.txtGiaThueNgay.ReadOnly = true;
            this.txtGiaThueNgay.Size = new System.Drawing.Size(300, 23);
            this.txtGiaThueNgay.TabIndex = 7;
            // 
            // lblTongGia
            // 
            this.lblTongGia.AutoSize = true;
            this.lblTongGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTongGia.Location = new System.Drawing.Point(15, 150);
            this.lblTongGia.Name = "lblTongGia";
            this.lblTongGia.Size = new System.Drawing.Size(60, 15);
            this.lblTongGia.TabIndex = 8;
            this.lblTongGia.Text = "Tong gia:";
            // 
            // txtTongGia
            // 
            this.txtTongGia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtTongGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.txtTongGia.Location = new System.Drawing.Point(150, 147);
            this.txtTongGia.Name = "txtTongGia";
            this.txtTongGia.ReadOnly = true;
            this.txtTongGia.Size = new System.Drawing.Size(300, 23);
            this.txtTongGia.TabIndex = 9;
            // 
            // lblSoTienCoc
            // 
            this.lblSoTienCoc.AutoSize = true;
            this.lblSoTienCoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoTienCoc.Location = new System.Drawing.Point(15, 180);
            this.lblSoTienCoc.Name = "lblSoTienCoc";
            this.lblSoTienCoc.Size = new System.Drawing.Size(72, 15);
            this.lblSoTienCoc.TabIndex = 10;
            this.lblSoTienCoc.Text = "So tien coc:";
            // 
            // txtSoTienCoc
            // 
            this.txtSoTienCoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSoTienCoc.Location = new System.Drawing.Point(150, 177);
            this.txtSoTienCoc.Name = "txtSoTienCoc";
            this.txtSoTienCoc.ReadOnly = true;
            this.txtSoTienCoc.Size = new System.Drawing.Size(300, 23);
            this.txtSoTienCoc.TabIndex = 11;
            // 
            // lblGiayToGiuLai
            // 
            this.lblGiayToGiuLai.AutoSize = true;
            this.lblGiayToGiuLai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGiayToGiuLai.Location = new System.Drawing.Point(15, 210);
            this.lblGiayToGiuLai.Name = "lblGiayToGiuLai";
            this.lblGiayToGiuLai.Size = new System.Drawing.Size(92, 15);
            this.lblGiayToGiuLai.TabIndex = 12;
            this.lblGiayToGiuLai.Text = "Giay to giu lai:";
            // 
            // txtGiayToGiuLai
            // 
            this.txtGiayToGiuLai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGiayToGiuLai.Location = new System.Drawing.Point(150, 207);
            this.txtGiayToGiuLai.Name = "txtGiayToGiuLai";
            this.txtGiayToGiuLai.ReadOnly = true;
            this.txtGiayToGiuLai.Size = new System.Drawing.Size(300, 23);
            this.txtGiayToGiuLai.TabIndex = 13;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTrangThai.Location = new System.Drawing.Point(15, 250);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(66, 15);
            this.lblTrangThai.TabIndex = 14;
            this.lblTrangThai.Text = "Trang thai:";
            // 
            // txtTrangThai
            // 
            this.txtTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtTrangThai.Location = new System.Drawing.Point(150, 247);
            this.txtTrangThai.Name = "txtTrangThai";
            this.txtTrangThai.ReadOnly = true;
            this.txtTrangThai.Size = new System.Drawing.Size(300, 23);
            this.txtTrangThai.TabIndex = 15;
            // 
            // lblTrangThaiThanhToan
            // 
            this.lblTrangThaiThanhToan.AutoSize = true;
            this.lblTrangThaiThanhToan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTrangThaiThanhToan.Location = new System.Drawing.Point(15, 280);
            this.lblTrangThaiThanhToan.Name = "lblTrangThaiThanhToan";
            this.lblTrangThaiThanhToan.Size = new System.Drawing.Size(129, 15);
            this.lblTrangThaiThanhToan.TabIndex = 16;
            this.lblTrangThaiThanhToan.Text = "Trang thai thanh toan:";
            // 
            // txtTrangThaiThanhToan
            // 
            this.txtTrangThaiThanhToan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtTrangThaiThanhToan.Location = new System.Drawing.Point(150, 277);
            this.txtTrangThaiThanhToan.Name = "txtTrangThaiThanhToan";
            this.txtTrangThaiThanhToan.ReadOnly = true;
            this.txtTrangThaiThanhToan.Size = new System.Drawing.Size(300, 23);
            this.txtTrangThaiThanhToan.TabIndex = 17;
            // 
            // lblHinhThucThanhToan
            // 
            this.lblHinhThucThanhToan.AutoSize = true;
            this.lblHinhThucThanhToan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHinhThucThanhToan.Location = new System.Drawing.Point(15, 310);
            this.lblHinhThucThanhToan.Name = "lblHinhThucThanhToan";
            this.lblHinhThucThanhToan.Size = new System.Drawing.Size(128, 15);
            this.lblHinhThucThanhToan.TabIndex = 18;
            this.lblHinhThucThanhToan.Text = "Hinh thuc thanh toan:";
            // 
            // txtHinhThucThanhToan
            // 
            this.txtHinhThucThanhToan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtHinhThucThanhToan.Location = new System.Drawing.Point(150, 307);
            this.txtHinhThucThanhToan.Name = "txtHinhThucThanhToan";
            this.txtHinhThucThanhToan.ReadOnly = true;
            this.txtHinhThucThanhToan.Size = new System.Drawing.Size(300, 23);
            this.txtHinhThucThanhToan.TabIndex = 19;
            // 
            // grpTinhToan
            // 
            this.grpTinhToan.Controls.Add(this.lblSoNgayQuaHan);
            this.grpTinhToan.Controls.Add(this.txtSoNgayQuaHan);
            this.grpTinhToan.Controls.Add(this.lblTienPhat);
            this.grpTinhToan.Controls.Add(this.txtTienPhat);
            this.grpTinhToan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpTinhToan.Location = new System.Drawing.Point(510, 260);
            this.grpTinhToan.Name = "grpTinhToan";
            this.grpTinhToan.Size = new System.Drawing.Size(470, 120);
            this.grpTinhToan.TabIndex = 4;
            this.grpTinhToan.TabStop = false;
            this.grpTinhToan.Text = "Tinh toan qua han";
            // 
            // lblSoNgayQuaHan
            // 
            this.lblSoNgayQuaHan.AutoSize = true;
            this.lblSoNgayQuaHan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoNgayQuaHan.Location = new System.Drawing.Point(15, 35);
            this.lblSoNgayQuaHan.Name = "lblSoNgayQuaHan";
            this.lblSoNgayQuaHan.Size = new System.Drawing.Size(99, 15);
            this.lblSoNgayQuaHan.TabIndex = 0;
            this.lblSoNgayQuaHan.Text = "So ngay qua han:";
            // 
            // txtSoNgayQuaHan
            // 
            this.txtSoNgayQuaHan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtSoNgayQuaHan.Location = new System.Drawing.Point(140, 32);
            this.txtSoNgayQuaHan.Name = "txtSoNgayQuaHan";
            this.txtSoNgayQuaHan.ReadOnly = true;
            this.txtSoNgayQuaHan.Size = new System.Drawing.Size(310, 23);
            this.txtSoNgayQuaHan.TabIndex = 1;
            // 
            // lblTienPhat
            // 
            this.lblTienPhat.AutoSize = true;
            this.lblTienPhat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTienPhat.Location = new System.Drawing.Point(15, 70);
            this.lblTienPhat.Name = "lblTienPhat";
            this.lblTienPhat.Size = new System.Drawing.Size(115, 15);
            this.lblTienPhat.TabIndex = 2;
            this.lblTienPhat.Text = "Tien phat (150%):";
            // 
            // txtTienPhat
            // 
            this.txtTienPhat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtTienPhat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.txtTienPhat.Location = new System.Drawing.Point(140, 67);
            this.txtTienPhat.Name = "txtTienPhat";
            this.txtTienPhat.ReadOnly = true;
            this.txtTienPhat.Size = new System.Drawing.Size(310, 23);
            this.txtTienPhat.TabIndex = 3;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelButtons.Controls.Add(this.lblHinhThucThanhToanNew);
            this.panelButtons.Controls.Add(this.cboHinhThucThanhToan);
            this.panelButtons.Controls.Add(this.btnXacNhanThanhToan);
            this.panelButtons.Controls.Add(this.btnGiaoXe);
            this.panelButtons.Controls.Add(this.btnTraXe);
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 620);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1000, 100);
            this.panelButtons.TabIndex = 5;
            // 
            // lblHinhThucThanhToanNew
            // 
            this.lblHinhThucThanhToanNew.AutoSize = true;
            this.lblHinhThucThanhToanNew.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHinhThucThanhToanNew.Location = new System.Drawing.Point(20, 23);
            this.lblHinhThucThanhToanNew.Name = "lblHinhThucThanhToanNew";
            this.lblHinhThucThanhToanNew.Size = new System.Drawing.Size(98, 15);
            this.lblHinhThucThanhToanNew.TabIndex = 0;
            this.lblHinhThucThanhToanNew.Text = "Hinh thuc TT:";
            // 
            // cboHinhThucThanhToan
            // 
            this.cboHinhThucThanhToan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHinhThucThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboHinhThucThanhToan.FormattingEnabled = true;
            this.cboHinhThucThanhToan.Items.AddRange(new object[] {
            "Tien mat",
            "Chuyen khoan",
            "The ATM"});
            this.cboHinhThucThanhToan.Location = new System.Drawing.Point(23, 45);
            this.cboHinhThucThanhToan.Name = "cboHinhThucThanhToan";
            this.cboHinhThucThanhToan.Size = new System.Drawing.Size(180, 25);
            this.cboHinhThucThanhToan.TabIndex = 1;
            // 
            // btnXacNhanThanhToan
            // 
            this.btnXacNhanThanhToan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnXacNhanThanhToan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXacNhanThanhToan.FlatAppearance.BorderSize = 0;
            this.btnXacNhanThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhanThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXacNhanThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhanThanhToan.Location = new System.Drawing.Point(220, 30);
            this.btnXacNhanThanhToan.Name = "btnXacNhanThanhToan";
            this.btnXacNhanThanhToan.Size = new System.Drawing.Size(160, 50);
            this.btnXacNhanThanhToan.TabIndex = 2;
            this.btnXacNhanThanhToan.Text = "XAC NHAN\r\nTHANH TOAN";
            this.btnXacNhanThanhToan.UseVisualStyleBackColor = false;
            this.btnXacNhanThanhToan.Click += new System.EventHandler(this.btnXacNhanThanhToan_Click);
            // 
            // btnGiaoXe
            // 
            this.btnGiaoXe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnGiaoXe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGiaoXe.FlatAppearance.BorderSize = 0;
            this.btnGiaoXe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGiaoXe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGiaoXe.ForeColor = System.Drawing.Color.White;
            this.btnGiaoXe.Location = new System.Drawing.Point(395, 30);
            this.btnGiaoXe.Name = "btnGiaoXe";
            this.btnGiaoXe.Size = new System.Drawing.Size(160, 50);
            this.btnGiaoXe.TabIndex = 3;
            this.btnGiaoXe.Text = "GIAO XE";
            this.btnGiaoXe.UseVisualStyleBackColor = false;
            this.btnGiaoXe.Click += new System.EventHandler(this.btnGiaoXe_Click);
            // 
            // btnTraXe
            // 
            this.btnTraXe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnTraXe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTraXe.FlatAppearance.BorderSize = 0;
            this.btnTraXe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTraXe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTraXe.ForeColor = System.Drawing.Color.White;
            this.btnTraXe.Location = new System.Drawing.Point(570, 30);
            this.btnTraXe.Name = "btnTraXe";
            this.btnTraXe.Size = new System.Drawing.Size(160, 50);
            this.btnTraXe.TabIndex = 4;
            this.btnTraXe.Text = "TRA XE";
            this.btnTraXe.UseVisualStyleBackColor = false;
            this.btnTraXe.Click += new System.EventHandler(this.btnTraXe_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(820, 30);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 50);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "DONG";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormXemHopDongThue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 720);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.grpTinhToan);
            this.Controls.Add(this.grpThongTinThue);
            this.Controls.Add(this.grpThongTinKhach);
            this.Controls.Add(this.grpThongTinXe);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormXemHopDongThue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xem Hop Dong Thue";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.grpThongTinXe.ResumeLayout(false);
            this.grpThongTinXe.PerformLayout();
            this.grpThongTinKhach.ResumeLayout(false);
            this.grpThongTinKhach.PerformLayout();
            this.grpThongTinThue.ResumeLayout(false);
            this.grpThongTinThue.PerformLayout();
            this.grpTinhToan.ResumeLayout(false);
            this.grpTinhToan.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}