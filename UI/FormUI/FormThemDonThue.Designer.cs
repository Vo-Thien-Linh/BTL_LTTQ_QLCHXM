namespace UI.FormUI
{
    partial class FormThemDonThue
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpKhachHang;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.TextBox txtSdtKH;
        private System.Windows.Forms.TextBox txtEmailKH;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.Label lblSdtKH;
        private System.Windows.Forms.Label lblEmailKH;
        private System.Windows.Forms.GroupBox grpXe;
        private System.Windows.Forms.ComboBox cboXe;
        private System.Windows.Forms.TextBox txtBienSo;
        private System.Windows.Forms.TextBox txtGiaThueNgay;
        private System.Windows.Forms.Label lblXe;
        private System.Windows.Forms.Label lblBienSo;
        private System.Windows.Forms.Label lblGiaThueNgay;
        private System.Windows.Forms.GroupBox grpThongTinThue;
        private System.Windows.Forms.DateTimePicker dtpNgayBatDau;
        private System.Windows.Forms.DateTimePicker dtpNgayKetThuc;
        private System.Windows.Forms.NumericUpDown nudSoNgay;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.TextBox txtTienCoc;
        private System.Windows.Forms.Label lblNgayBatDau;
        private System.Windows.Forms.Label lblNgayKetThuc;
        private System.Windows.Forms.Label lblSoNgay;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblTienCoc;
        private System.Windows.Forms.GroupBox grpThanhToan;
        private System.Windows.Forms.ComboBox cboGiayToGiuLai;
        private System.Windows.Forms.Label lblGiayToGiuLai;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnTinhTien;

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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpKhachHang = new System.Windows.Forms.GroupBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblSdtKH = new System.Windows.Forms.Label();
            this.txtSdtKH = new System.Windows.Forms.TextBox();
            this.lblEmailKH = new System.Windows.Forms.Label();
            this.txtEmailKH = new System.Windows.Forms.TextBox();
            this.grpXe = new System.Windows.Forms.GroupBox();
            this.lblXe = new System.Windows.Forms.Label();
            this.cboXe = new System.Windows.Forms.ComboBox();
            this.lblBienSo = new System.Windows.Forms.Label();
            this.txtBienSo = new System.Windows.Forms.TextBox();
            this.lblGiaThueNgay = new System.Windows.Forms.Label();
            this.txtGiaThueNgay = new System.Windows.Forms.TextBox();
            this.grpThongTinThue = new System.Windows.Forms.GroupBox();
            this.lblNgayBatDau = new System.Windows.Forms.Label();
            this.dtpNgayBatDau = new System.Windows.Forms.DateTimePicker();
            this.lblNgayKetThuc = new System.Windows.Forms.Label();
            this.dtpNgayKetThuc = new System.Windows.Forms.DateTimePicker();
            this.lblSoNgay = new System.Windows.Forms.Label();
            this.nudSoNgay = new System.Windows.Forms.NumericUpDown();
            this.btnTinhTien = new System.Windows.Forms.Button();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.lblTienCoc = new System.Windows.Forms.Label();
            this.txtTienCoc = new System.Windows.Forms.TextBox();
            this.grpThanhToan = new System.Windows.Forms.GroupBox();
            this.lblGiayToGiuLai = new System.Windows.Forms.Label();
            this.cboGiayToGiuLai = new System.Windows.Forms.ComboBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.grpKhachHang.SuspendLayout();
            this.grpXe.SuspendLayout();
            this.grpThongTinThue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNgay)).BeginInit();
            this.grpThanhToan.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(860, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TẠO ĐƠN THUÊ MỚI";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpKhachHang
            // 
            this.grpKhachHang.Controls.Add(this.lblKhachHang);
            this.grpKhachHang.Controls.Add(this.cboKhachHang);
            this.grpKhachHang.Controls.Add(this.lblSdtKH);
            this.grpKhachHang.Controls.Add(this.txtSdtKH);
            this.grpKhachHang.Controls.Add(this.lblEmailKH);
            this.grpKhachHang.Controls.Add(this.txtEmailKH);
            this.grpKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpKhachHang.Location = new System.Drawing.Point(20, 80);
            this.grpKhachHang.Name = "grpKhachHang";
            this.grpKhachHang.Size = new System.Drawing.Size(420, 150);
            this.grpKhachHang.TabIndex = 1;
            this.grpKhachHang.TabStop = false;
            this.grpKhachHang.Text = "📋 THÔNG TIN KHÁCH HÀNG";
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKhachHang.Location = new System.Drawing.Point(15, 30);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(90, 23);
            this.lblKhachHang.TabIndex = 0;
            this.lblKhachHang.Text = "Khách hàng:";
            this.lblKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.Location = new System.Drawing.Point(120, 30);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(280, 23);
            this.cboKhachHang.TabIndex = 1;
            // 
            // lblSdtKH
            // 
            this.lblSdtKH.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSdtKH.Location = new System.Drawing.Point(15, 70);
            this.lblSdtKH.Name = "lblSdtKH";
            this.lblSdtKH.Size = new System.Drawing.Size(90, 23);
            this.lblSdtKH.TabIndex = 2;
            this.lblSdtKH.Text = "Số điện thoại:";
            this.lblSdtKH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSdtKH
            // 
            this.txtSdtKH.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSdtKH.Location = new System.Drawing.Point(120, 70);
            this.txtSdtKH.Name = "txtSdtKH";
            this.txtSdtKH.ReadOnly = true;
            this.txtSdtKH.Size = new System.Drawing.Size(280, 23);
            this.txtSdtKH.TabIndex = 3;
            // 
            // lblEmailKH
            // 
            this.lblEmailKH.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmailKH.Location = new System.Drawing.Point(15, 110);
            this.lblEmailKH.Name = "lblEmailKH";
            this.lblEmailKH.Size = new System.Drawing.Size(90, 23);
            this.lblEmailKH.TabIndex = 4;
            this.lblEmailKH.Text = "Email:";
            this.lblEmailKH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEmailKH
            // 
            this.txtEmailKH.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmailKH.Location = new System.Drawing.Point(120, 110);
            this.txtEmailKH.Name = "txtEmailKH";
            this.txtEmailKH.ReadOnly = true;
            this.txtEmailKH.Size = new System.Drawing.Size(280, 23);
            this.txtEmailKH.TabIndex = 5;
            // 
            // grpXe
            // 
            this.grpXe.Controls.Add(this.lblXe);
            this.grpXe.Controls.Add(this.cboXe);
            this.grpXe.Controls.Add(this.lblBienSo);
            this.grpXe.Controls.Add(this.txtBienSo);
            this.grpXe.Controls.Add(this.lblGiaThueNgay);
            this.grpXe.Controls.Add(this.txtGiaThueNgay);
            this.grpXe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpXe.Location = new System.Drawing.Point(460, 80);
            this.grpXe.Name = "grpXe";
            this.grpXe.Size = new System.Drawing.Size(420, 150);
            this.grpXe.TabIndex = 2;
            this.grpXe.TabStop = false;
            this.grpXe.Text = "🏍️ THÔNG TIN XE";
            // 
            // lblXe
            // 
            this.lblXe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblXe.Location = new System.Drawing.Point(15, 30);
            this.lblXe.Name = "lblXe";
            this.lblXe.Size = new System.Drawing.Size(90, 23);
            this.lblXe.TabIndex = 0;
            this.lblXe.Text = "Chọn xe:";
            this.lblXe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboXe
            // 
            this.cboXe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboXe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboXe.FormattingEnabled = true;
            this.cboXe.Location = new System.Drawing.Point(120, 30);
            this.cboXe.Name = "cboXe";
            this.cboXe.Size = new System.Drawing.Size(280, 23);
            this.cboXe.TabIndex = 1;
            // 
            // lblBienSo
            // 
            this.lblBienSo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBienSo.Location = new System.Drawing.Point(15, 70);
            this.lblBienSo.Name = "lblBienSo";
            this.lblBienSo.Size = new System.Drawing.Size(90, 23);
            this.lblBienSo.TabIndex = 2;
            this.lblBienSo.Text = "Biển số:";
            this.lblBienSo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBienSo
            // 
            this.txtBienSo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBienSo.Location = new System.Drawing.Point(120, 70);
            this.txtBienSo.Name = "txtBienSo";
            this.txtBienSo.ReadOnly = true;
            this.txtBienSo.Size = new System.Drawing.Size(280, 23);
            this.txtBienSo.TabIndex = 3;
            // 
            // lblGiaThueNgay
            // 
            this.lblGiaThueNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGiaThueNgay.Location = new System.Drawing.Point(15, 110);
            this.lblGiaThueNgay.Name = "lblGiaThueNgay";
            this.lblGiaThueNgay.Size = new System.Drawing.Size(90, 23);
            this.lblGiaThueNgay.TabIndex = 4;
            this.lblGiaThueNgay.Text = "Giá thuê/ngày:";
            this.lblGiaThueNgay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtGiaThueNgay
            // 
            this.txtGiaThueNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtGiaThueNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.txtGiaThueNgay.Location = new System.Drawing.Point(120, 110);
            this.txtGiaThueNgay.Name = "txtGiaThueNgay";
            this.txtGiaThueNgay.ReadOnly = true;
            this.txtGiaThueNgay.Size = new System.Drawing.Size(280, 23);
            this.txtGiaThueNgay.TabIndex = 5;
            // 
            // grpThongTinThue
            // 
            this.grpThongTinThue.Controls.Add(this.lblNgayBatDau);
            this.grpThongTinThue.Controls.Add(this.dtpNgayBatDau);
            this.grpThongTinThue.Controls.Add(this.lblNgayKetThuc);
            this.grpThongTinThue.Controls.Add(this.dtpNgayKetThuc);
            this.grpThongTinThue.Controls.Add(this.lblSoNgay);
            this.grpThongTinThue.Controls.Add(this.nudSoNgay);
            this.grpThongTinThue.Controls.Add(this.btnTinhTien);
            this.grpThongTinThue.Controls.Add(this.lblTongTien);
            this.grpThongTinThue.Controls.Add(this.txtTongTien);
            this.grpThongTinThue.Controls.Add(this.lblTienCoc);
            this.grpThongTinThue.Controls.Add(this.txtTienCoc);
            this.grpThongTinThue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpThongTinThue.Location = new System.Drawing.Point(20, 250);
            this.grpThongTinThue.Name = "grpThongTinThue";
            this.grpThongTinThue.Size = new System.Drawing.Size(860, 180);
            this.grpThongTinThue.TabIndex = 3;
            this.grpThongTinThue.TabStop = false;
            this.grpThongTinThue.Text = "📅 THÔNG TIN THUÊ";
            // 
            // lblNgayBatDau
            // 
            this.lblNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayBatDau.Location = new System.Drawing.Point(15, 30);
            this.lblNgayBatDau.Name = "lblNgayBatDau";
            this.lblNgayBatDau.Size = new System.Drawing.Size(100, 23);
            this.lblNgayBatDau.TabIndex = 0;
            this.lblNgayBatDau.Text = "Ngày bắt đầu:";
            this.lblNgayBatDau.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNgayBatDau
            // 
            this.dtpNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBatDau.Location = new System.Drawing.Point(120, 30);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(280, 23);
            this.dtpNgayBatDau.TabIndex = 1;
            // 
            // lblNgayKetThuc
            // 
            this.lblNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayKetThuc.Location = new System.Drawing.Point(440, 30);
            this.lblNgayKetThuc.Name = "lblNgayKetThuc";
            this.lblNgayKetThuc.Size = new System.Drawing.Size(100, 23);
            this.lblNgayKetThuc.TabIndex = 2;
            this.lblNgayKetThuc.Text = "Ngày kết thúc:";
            this.lblNgayKetThuc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNgayKetThuc
            // 
            this.dtpNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayKetThuc.Location = new System.Drawing.Point(555, 30);
            this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            this.dtpNgayKetThuc.Size = new System.Drawing.Size(280, 23);
            this.dtpNgayKetThuc.TabIndex = 3;
            // 
            // lblSoNgay
            // 
            this.lblSoNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoNgay.Location = new System.Drawing.Point(15, 70);
            this.lblSoNgay.Name = "lblSoNgay";
            this.lblSoNgay.Size = new System.Drawing.Size(100, 23);
            this.lblSoNgay.TabIndex = 4;
            this.lblSoNgay.Text = "Số ngày thuê:";
            this.lblSoNgay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudSoNgay
            // 
            this.nudSoNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.nudSoNgay.Location = new System.Drawing.Point(120, 70);
            this.nudSoNgay.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nudSoNgay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoNgay.Name = "nudSoNgay";
            this.nudSoNgay.ReadOnly = true;
            this.nudSoNgay.Size = new System.Drawing.Size(280, 23);
            this.nudSoNgay.TabIndex = 5;
            this.nudSoNgay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSoNgay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnTinhTien
            // 
            this.btnTinhTien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnTinhTien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTinhTien.FlatAppearance.BorderSize = 0;
            this.btnTinhTien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTinhTien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTinhTien.ForeColor = System.Drawing.Color.White;
            this.btnTinhTien.Location = new System.Drawing.Point(440, 65);
            this.btnTinhTien.Name = "btnTinhTien";
            this.btnTinhTien.Size = new System.Drawing.Size(395, 35);
            this.btnTinhTien.TabIndex = 6;
            this.btnTinhTien.Text = "🧮 TÍNH TIỀN TỰ ĐỘNG";
            this.btnTinhTien.UseVisualStyleBackColor = false;
            // 
            // lblTongTien
            // 
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTongTien.Location = new System.Drawing.Point(15, 110);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(100, 23);
            this.lblTongTien.TabIndex = 7;
            this.lblTongTien.Text = "Tổng tiền:";
            this.lblTongTien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTongTien
            // 
            this.txtTongTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.txtTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.txtTongTien.Location = new System.Drawing.Point(120, 110);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.Size = new System.Drawing.Size(280, 27);
            this.txtTongTien.TabIndex = 8;
            this.txtTongTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTienCoc
            // 
            this.lblTienCoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTienCoc.Location = new System.Drawing.Point(440, 110);
            this.lblTienCoc.Name = "lblTienCoc";
            this.lblTienCoc.Size = new System.Drawing.Size(100, 23);
            this.lblTienCoc.TabIndex = 9;
            this.lblTienCoc.Text = "Tiền cọc:";
            this.lblTienCoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTienCoc
            // 
            this.txtTienCoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTienCoc.Location = new System.Drawing.Point(555, 110);
            this.txtTienCoc.Name = "txtTienCoc";
            this.txtTienCoc.Size = new System.Drawing.Size(280, 23);
            this.txtTienCoc.TabIndex = 10;
            this.txtTienCoc.Text = "0";
            this.txtTienCoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpThanhToan
            // 
            this.grpThanhToan.Controls.Add(this.lblGiayToGiuLai);
            this.grpThanhToan.Controls.Add(this.cboGiayToGiuLai);
            this.grpThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpThanhToan.Location = new System.Drawing.Point(20, 450);
            this.grpThanhToan.Name = "grpThanhToan";
            this.grpThanhToan.Size = new System.Drawing.Size(860, 80);
            this.grpThanhToan.TabIndex = 4;
            this.grpThanhToan.TabStop = false;
            this.grpThanhToan.Text = "💳 THÔNG TIN BỔ SUNG";
            // 
            // lblGiayToGiuLai
            // 
            this.lblGiayToGiuLai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGiayToGiuLai.Location = new System.Drawing.Point(15, 30);
            this.lblGiayToGiuLai.Name = "lblGiayToGiuLai";
            this.lblGiayToGiuLai.Size = new System.Drawing.Size(100, 23);
            this.lblGiayToGiuLai.TabIndex = 0;
            this.lblGiayToGiuLai.Text = "Giấy tờ giữ lại:";
            this.lblGiayToGiuLai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboGiayToGiuLai
            // 
            this.cboGiayToGiuLai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGiayToGiuLai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboGiayToGiuLai.FormattingEnabled = true;
            this.cboGiayToGiuLai.Items.AddRange(new object[] {
            "CMND/CCCD",
            "Bằng lái xe",
            "Hộ chiếu",
            "Giấy phép lái xe quốc tế",
            "Thẻ sinh viên",
            "Thẻ nhân viên"});
            this.cboGiayToGiuLai.Location = new System.Drawing.Point(120, 30);
            this.cboGiayToGiuLai.Name = "cboGiayToGiuLai";
            this.cboGiayToGiuLai.Size = new System.Drawing.Size(715, 25);
            this.cboGiayToGiuLai.TabIndex = 1;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(580, 550);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(140, 45);
            this.btnLuu.TabIndex = 5;
            this.btnLuu.Text = "💾 LƯU";
            this.btnLuu.UseVisualStyleBackColor = false;
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(740, 550);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(140, 45);
            this.btnHuy.TabIndex = 6;
            this.btnHuy.Text = "❌ HỦY";
            this.btnHuy.UseVisualStyleBackColor = false;
            // 
            // FormThemDonThue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(900, 620);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.grpThanhToan);
            this.Controls.Add(this.grpThongTinThue);
            this.Controls.Add(this.grpXe);
            this.Controls.Add(this.grpKhachHang);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormThemDonThue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạo Đơn Thuê Mới";
            this.pnlHeader.ResumeLayout(false);
            this.grpKhachHang.ResumeLayout(false);
            this.grpKhachHang.PerformLayout();
            this.grpXe.ResumeLayout(false);
            this.grpXe.PerformLayout();
            this.grpThongTinThue.ResumeLayout(false);
            this.grpThongTinThue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNgay)).EndInit();
            this.grpThanhToan.ResumeLayout(false);
            this.grpThanhToan.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}