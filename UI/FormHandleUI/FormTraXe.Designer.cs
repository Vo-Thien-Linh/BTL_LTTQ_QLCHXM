namespace UI.FormHandleUI
{
    partial class FormTraXe
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.grpThongTinBanDau = new System.Windows.Forms.GroupBox();
            this.lblKmBatDau = new System.Windows.Forms.Label();
            this.lblKmBatDauValue = new System.Windows.Forms.Label();
            this.lblNgayBatDau = new System.Windows.Forms.Label();
            this.lblNgayBatDauValue = new System.Windows.Forms.Label();
            this.lblNgayKetThuc = new System.Windows.Forms.Label();
            this.lblNgayKetThucValue = new System.Windows.Forms.Label();
            this.lblSoTienCoc = new System.Windows.Forms.Label();
            this.lblSoTienCocValue = new System.Windows.Forms.Label();
            this.lblGiaThueNgay = new System.Windows.Forms.Label();
            this.lblGiaThueNgayValue = new System.Windows.Forms.Label();
            this.grpNhapLieu = new System.Windows.Forms.GroupBox();
            this.lblNgayTra = new System.Windows.Forms.Label();
            this.dtpNgayTra = new System.Windows.Forms.DateTimePicker();
            this.lblKmKetThuc = new System.Windows.Forms.Label();
            this.nudKmKetThuc = new System.Windows.Forms.NumericUpDown();
            this.lblKmChay = new System.Windows.Forms.Label();
            this.lblKmChayValue = new System.Windows.Forms.Label();
            this.lblTinhTrang = new System.Windows.Forms.Label();
            this.cboTinhTrang = new System.Windows.Forms.ComboBox();
            this.lblChiPhi = new System.Windows.Forms.Label();
            this.nudChiPhiPhatSinh = new System.Windows.Forms.NumericUpDown();
            this.lblVND1 = new System.Windows.Forms.Label();
            this.chkTraSom = new System.Windows.Forms.CheckBox();
            this.nudSoNgayTraSom = new System.Windows.Forms.NumericUpDown();
            this.lblNgay = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.grpTinhToan = new System.Windows.Forms.GroupBox();
            this.lblTienPhat = new System.Windows.Forms.Label();
            this.lblTienPhatValue = new System.Windows.Forms.Label();
            this.lblSoNgayQuaHanValue = new System.Windows.Forms.Label();
            this.lblTienHoanTraSom = new System.Windows.Forms.Label();
            this.lblTienHoanTraSomValue = new System.Windows.Forms.Label();
            this.lblSoNgayTraSomDetail = new System.Windows.Forms.Label();
            this.pnlSeparator = new System.Windows.Forms.Panel();
            this.lblTienHoanCoc = new System.Windows.Forms.Label();
            this.lblTienHoanCocValue = new System.Windows.Forms.Label();
            this.lblKetLuan = new System.Windows.Forms.Label();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.grpThongTinBanDau.SuspendLayout();
            this.grpNhapLieu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKmKetThuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChiPhiPhatSinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNgayTraSom)).BeginInit();
            this.grpTinhToan.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(600, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(580, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "XÁC NHẬN TRẢ XE";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.White;
            this.lblSubtitle.Location = new System.Drawing.Point(10, 50);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(580, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Kiểm tra xe và tính toán chi phí trước khi hoàn trả";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpThongTinBanDau
            // 
            this.grpThongTinBanDau.BackColor = System.Drawing.Color.White;
            this.grpThongTinBanDau.Controls.Add(this.lblKmBatDau);
            this.grpThongTinBanDau.Controls.Add(this.lblKmBatDauValue);
            this.grpThongTinBanDau.Controls.Add(this.lblNgayBatDau);
            this.grpThongTinBanDau.Controls.Add(this.lblNgayBatDauValue);
            this.grpThongTinBanDau.Controls.Add(this.lblNgayKetThuc);
            this.grpThongTinBanDau.Controls.Add(this.lblNgayKetThucValue);
            this.grpThongTinBanDau.Controls.Add(this.lblSoTienCoc);
            this.grpThongTinBanDau.Controls.Add(this.lblSoTienCocValue);
            this.grpThongTinBanDau.Controls.Add(this.lblGiaThueNgay);
            this.grpThongTinBanDau.Controls.Add(this.lblGiaThueNgayValue);
            this.grpThongTinBanDau.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpThongTinBanDau.Location = new System.Drawing.Point(20, 100);
            this.grpThongTinBanDau.Name = "grpThongTinBanDau";
            this.grpThongTinBanDau.Size = new System.Drawing.Size(560, 140);
            this.grpThongTinBanDau.TabIndex = 1;
            this.grpThongTinBanDau.TabStop = false;
            this.grpThongTinBanDau.Text = " Thông tin ban đầu";
            // 
            // lblKmBatDau
            // 
            this.lblKmBatDau.AutoSize = true;
            this.lblKmBatDau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKmBatDau.Location = new System.Drawing.Point(20, 30);
            this.lblKmBatDau.Name = "lblKmBatDau";
            this.lblKmBatDau.Size = new System.Drawing.Size(95, 15);
            this.lblKmBatDau.TabIndex = 0;
            this.lblKmBatDau.Text = " Km bắt đầu:";
            // 
            // lblKmBatDauValue
            // 
            this.lblKmBatDauValue.AutoSize = true;
            this.lblKmBatDauValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKmBatDauValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblKmBatDauValue.Location = new System.Drawing.Point(180, 30);
            this.lblKmBatDauValue.Name = "lblKmBatDauValue";
            this.lblKmBatDauValue.Size = new System.Drawing.Size(40, 15);
            this.lblKmBatDauValue.TabIndex = 1;
            this.lblKmBatDauValue.Text = "0 km";
            // 
            // lblNgayBatDau
            // 
            this.lblNgayBatDau.AutoSize = true;
            this.lblNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayBatDau.Location = new System.Drawing.Point(20, 55);
            this.lblNgayBatDau.Name = "lblNgayBatDau";
            this.lblNgayBatDau.Size = new System.Drawing.Size(105, 15);
            this.lblNgayBatDau.TabIndex = 2;
            this.lblNgayBatDau.Text = " Ngày bắt đầu:";
            // 
            // lblNgayBatDauValue
            // 
            this.lblNgayBatDauValue.AutoSize = true;
            this.lblNgayBatDauValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNgayBatDauValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblNgayBatDauValue.Location = new System.Drawing.Point(180, 55);
            this.lblNgayBatDauValue.Name = "lblNgayBatDauValue";
            this.lblNgayBatDauValue.Size = new System.Drawing.Size(73, 15);
            this.lblNgayBatDauValue.TabIndex = 3;
            this.lblNgayBatDauValue.Text = "01/01/2024";
            // 
            // lblNgayKetThuc
            // 
            this.lblNgayKetThuc.AutoSize = true;
            this.lblNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayKetThuc.Location = new System.Drawing.Point(20, 80);
            this.lblNgayKetThuc.Name = "lblNgayKetThuc";
            this.lblNgayKetThuc.Size = new System.Drawing.Size(110, 15);
            this.lblNgayKetThuc.TabIndex = 4;
            this.lblNgayKetThuc.Text = " Ngày kết thúc:";
            // 
            // lblNgayKetThucValue
            // 
            this.lblNgayKetThucValue.AutoSize = true;
            this.lblNgayKetThucValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNgayKetThucValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblNgayKetThucValue.Location = new System.Drawing.Point(180, 80);
            this.lblNgayKetThucValue.Name = "lblNgayKetThucValue";
            this.lblNgayKetThucValue.Size = new System.Drawing.Size(73, 15);
            this.lblNgayKetThucValue.TabIndex = 5;
            this.lblNgayKetThucValue.Text = "05/01/2024";
            // 
            // lblSoTienCoc
            // 
            this.lblSoTienCoc.AutoSize = true;
            this.lblSoTienCoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoTienCoc.Location = new System.Drawing.Point(300, 30);
            this.lblSoTienCoc.Name = "lblSoTienCoc";
            this.lblSoTienCoc.Size = new System.Drawing.Size(78, 15);
            this.lblSoTienCoc.TabIndex = 6;
            this.lblSoTienCoc.Text = " Tiền cọc:";
            // 
            // lblSoTienCocValue
            // 
            this.lblSoTienCocValue.AutoSize = true;
            this.lblSoTienCocValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoTienCocValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblSoTienCocValue.Location = new System.Drawing.Point(420, 30);
            this.lblSoTienCocValue.Name = "lblSoTienCocValue";
            this.lblSoTienCocValue.Size = new System.Drawing.Size(48, 15);
            this.lblSoTienCocValue.TabIndex = 7;
            this.lblSoTienCocValue.Text = "0 VNĐ";
            // 
            // lblGiaThueNgay
            // 
            this.lblGiaThueNgay.AutoSize = true;
            this.lblGiaThueNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGiaThueNgay.Location = new System.Drawing.Point(300, 55);
            this.lblGiaThueNgay.Name = "lblGiaThueNgay";
            this.lblGiaThueNgay.Size = new System.Drawing.Size(103, 15);
            this.lblGiaThueNgay.TabIndex = 8;
            this.lblGiaThueNgay.Text = " Giá thuê/ngày:";
            // 
            // lblGiaThueNgayValue
            // 
            this.lblGiaThueNgayValue.AutoSize = true;
            this.lblGiaThueNgayValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblGiaThueNgayValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblGiaThueNgayValue.Location = new System.Drawing.Point(420, 55);
            this.lblGiaThueNgayValue.Name = "lblGiaThueNgayValue";
            this.lblGiaThueNgayValue.Size = new System.Drawing.Size(88, 15);
            this.lblGiaThueNgayValue.TabIndex = 9;
            this.lblGiaThueNgayValue.Text = "0 VNĐ/ngày";
            // 
            // grpNhapLieu
            // 
            this.grpNhapLieu.BackColor = System.Drawing.Color.White;
            this.grpNhapLieu.Controls.Add(this.lblNgayTra);
            this.grpNhapLieu.Controls.Add(this.dtpNgayTra);
            this.grpNhapLieu.Controls.Add(this.lblKmKetThuc);
            this.grpNhapLieu.Controls.Add(this.nudKmKetThuc);
            this.grpNhapLieu.Controls.Add(this.lblKmChay);
            this.grpNhapLieu.Controls.Add(this.lblKmChayValue);
            this.grpNhapLieu.Controls.Add(this.lblTinhTrang);
            this.grpNhapLieu.Controls.Add(this.cboTinhTrang);
            this.grpNhapLieu.Controls.Add(this.lblChiPhi);
            this.grpNhapLieu.Controls.Add(this.nudChiPhiPhatSinh);
            this.grpNhapLieu.Controls.Add(this.lblVND1);
            this.grpNhapLieu.Controls.Add(this.chkTraSom);
            this.grpNhapLieu.Controls.Add(this.nudSoNgayTraSom);
            this.grpNhapLieu.Controls.Add(this.lblNgay);
            this.grpNhapLieu.Controls.Add(this.lblGhiChu);
            this.grpNhapLieu.Controls.Add(this.txtGhiChu);
            this.grpNhapLieu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpNhapLieu.Location = new System.Drawing.Point(20, 250);
            this.grpNhapLieu.Name = "grpNhapLieu";
            this.grpNhapLieu.Size = new System.Drawing.Size(560, 260);
            this.grpNhapLieu.TabIndex = 2;
            this.grpNhapLieu.TabStop = false;
            this.grpNhapLieu.Text = " Thông tin trả xe";
            // 
            // lblNgayTra
            // 
            this.lblNgayTra.AutoSize = true;
            this.lblNgayTra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNgayTra.Location = new System.Drawing.Point(20, 30);
            this.lblNgayTra.Name = "lblNgayTra";
            this.lblNgayTra.Size = new System.Drawing.Size(66, 15);
            this.lblNgayTra.TabIndex = 0;
            this.lblNgayTra.Text = " Ngày trả:";
            // 
            // dtpNgayTra
            // 
            this.dtpNgayTra.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayTra.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayTra.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayTra.Location = new System.Drawing.Point(180, 27);
            this.dtpNgayTra.Name = "dtpNgayTra";
            this.dtpNgayTra.Size = new System.Drawing.Size(180, 23);
            this.dtpNgayTra.TabIndex = 1;
            // 
            // lblKmKetThuc
            // 
            this.lblKmKetThuc.AutoSize = true;
            this.lblKmKetThuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKmKetThuc.Location = new System.Drawing.Point(20, 65);
            this.lblKmKetThuc.Name = "lblKmKetThuc";
            this.lblKmKetThuc.Size = new System.Drawing.Size(93, 15);
            this.lblKmKetThuc.TabIndex = 2;
            this.lblKmKetThuc.Text = " Km kết thúc:";
            // 
            // nudKmKetThuc
            // 
            this.nudKmKetThuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudKmKetThuc.Location = new System.Drawing.Point(180, 62);
            this.nudKmKetThuc.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.nudKmKetThuc.Name = "nudKmKetThuc";
            this.nudKmKetThuc.Size = new System.Drawing.Size(180, 23);
            this.nudKmKetThuc.TabIndex = 2;
            this.nudKmKetThuc.ThousandsSeparator = true;
            this.nudKmKetThuc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblKmChay
            // 
            this.lblKmChay.AutoSize = true;
            this.lblKmChay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKmChay.ForeColor = System.Drawing.Color.Gray;
            this.lblKmChay.Location = new System.Drawing.Point(370, 65);
            this.lblKmChay.Name = "lblKmChay";
            this.lblKmChay.Size = new System.Drawing.Size(60, 15);
            this.lblKmChay.TabIndex = 4;
            this.lblKmChay.Text = "Đã chạy:";
            // 
            // lblKmChayValue
            // 
            this.lblKmChayValue.AutoSize = true;
            this.lblKmChayValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKmChayValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblKmChayValue.Location = new System.Drawing.Point(440, 65);
            this.lblKmChayValue.Name = "lblKmChayValue";
            this.lblKmChayValue.Size = new System.Drawing.Size(40, 15);
            this.lblKmChayValue.TabIndex = 5;
            this.lblKmChayValue.Text = "0 km";
            // 
            // lblTinhTrang
            // 
            this.lblTinhTrang.AutoSize = true;
            this.lblTinhTrang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTinhTrang.Location = new System.Drawing.Point(20, 100);
            this.lblTinhTrang.Name = "lblTinhTrang";
            this.lblTinhTrang.Size = new System.Drawing.Size(85, 15);
            this.lblTinhTrang.TabIndex = 6;
            this.lblTinhTrang.Text = " Tình trạng:";
            // 
            // cboTinhTrang
            // 
            this.cboTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTinhTrang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboTinhTrang.FormattingEnabled = true;
            this.cboTinhTrang.Items.AddRange(new object[] {
            "Tốt",
            "Bình thường",
            "Có trầy xước nhẹ",
            "Hư hỏng cần sửa chữa"});
            this.cboTinhTrang.Location = new System.Drawing.Point(180, 97);
            this.cboTinhTrang.Name = "cboTinhTrang";
            this.cboTinhTrang.Size = new System.Drawing.Size(180, 23);
            this.cboTinhTrang.TabIndex = 3;
            // 
            // lblChiPhi
            // 
            this.lblChiPhi.AutoSize = true;
            this.lblChiPhi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblChiPhi.Location = new System.Drawing.Point(20, 135);
            this.lblChiPhi.Name = "lblChiPhi";
            this.lblChiPhi.Size = new System.Drawing.Size(83, 15);
            this.lblChiPhi.TabIndex = 8;
            this.lblChiPhi.Text = " Chi phí PS:";
            // 
            // nudChiPhiPhatSinh
            // 
            this.nudChiPhiPhatSinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudChiPhiPhatSinh.Location = new System.Drawing.Point(180, 132);
            this.nudChiPhiPhatSinh.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            this.nudChiPhiPhatSinh.Name = "nudChiPhiPhatSinh";
            this.nudChiPhiPhatSinh.Size = new System.Drawing.Size(180, 23);
            this.nudChiPhiPhatSinh.TabIndex = 4;
            this.nudChiPhiPhatSinh.ThousandsSeparator = true;
            this.nudChiPhiPhatSinh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblVND1
            // 
            this.lblVND1.AutoSize = true;
            this.lblVND1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblVND1.Location = new System.Drawing.Point(370, 135);
            this.lblVND1.Name = "lblVND1";
            this.lblVND1.Size = new System.Drawing.Size(32, 15);
            this.lblVND1.TabIndex = 10;
            this.lblVND1.Text = "VNĐ";
            // 
            // chkTraSom
            // 
            this.chkTraSom.AutoSize = true;
            this.chkTraSom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkTraSom.Location = new System.Drawing.Point(20, 170);
            this.chkTraSom.Name = "chkTraSom";
            this.chkTraSom.Size = new System.Drawing.Size(82, 19);
            this.chkTraSom.TabIndex = 5;
            this.chkTraSom.Text = " Trả sớm";
            this.chkTraSom.UseVisualStyleBackColor = true;
            // 
            // nudSoNgayTraSom
            // 
            this.nudSoNgayTraSom.Enabled = false;
            this.nudSoNgayTraSom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudSoNgayTraSom.Location = new System.Drawing.Point(180, 167);
            this.nudSoNgayTraSom.Maximum = new decimal(new int[] { 365, 0, 0, 0 });
            this.nudSoNgayTraSom.Name = "nudSoNgayTraSom";
            this.nudSoNgayTraSom.Size = new System.Drawing.Size(100, 23);
            this.nudSoNgayTraSom.TabIndex = 6;
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgay.Location = new System.Drawing.Point(290, 170);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(34, 15);
            this.lblNgay.TabIndex = 13;
            this.lblNgay.Text = "ngày";
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblGhiChu.Location = new System.Drawing.Point(20, 205);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(68, 15);
            this.lblGhiChu.TabIndex = 14;
            this.lblGhiChu.Text = " Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGhiChu.Location = new System.Drawing.Point(180, 202);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(360, 40);
            this.txtGhiChu.TabIndex = 7;
            // 
            // grpTinhToan
            // 
            this.grpTinhToan.BackColor = System.Drawing.Color.White;
            this.grpTinhToan.Controls.Add(this.lblTienPhat);
            this.grpTinhToan.Controls.Add(this.lblTienPhatValue);
            this.grpTinhToan.Controls.Add(this.lblSoNgayQuaHanValue);
            this.grpTinhToan.Controls.Add(this.lblTienHoanTraSom);
            this.grpTinhToan.Controls.Add(this.lblTienHoanTraSomValue);
            this.grpTinhToan.Controls.Add(this.lblSoNgayTraSomDetail);
            this.grpTinhToan.Controls.Add(this.pnlSeparator);
            this.grpTinhToan.Controls.Add(this.lblTienHoanCoc);
            this.grpTinhToan.Controls.Add(this.lblTienHoanCocValue);
            this.grpTinhToan.Controls.Add(this.lblKetLuan);
            this.grpTinhToan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpTinhToan.Location = new System.Drawing.Point(20, 520);
            this.grpTinhToan.Name = "grpTinhToan";
            this.grpTinhToan.Size = new System.Drawing.Size(560, 150);
            this.grpTinhToan.TabIndex = 3;
            this.grpTinhToan.TabStop = false;
            this.grpTinhToan.Text = " Tính toán chi phí";
            // 
            // lblTienPhat
            // 
            this.lblTienPhat.AutoSize = true;
            this.lblTienPhat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTienPhat.Location = new System.Drawing.Point(20, 30);
            this.lblTienPhat.Name = "lblTienPhat";
            this.lblTienPhat.Size = new System.Drawing.Size(140, 15);
            this.lblTienPhat.TabIndex = 0;
            this.lblTienPhat.Text = " Tiền phạt (trả muộn):";
            // 
            // lblTienPhatValue
            // 
            this.lblTienPhatValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTienPhatValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblTienPhatValue.Location = new System.Drawing.Point(370, 30);
            this.lblTienPhatValue.Name = "lblTienPhatValue";
            this.lblTienPhatValue.Size = new System.Drawing.Size(170, 15);
            this.lblTienPhatValue.TabIndex = 1;
            this.lblTienPhatValue.Text = "0 VNĐ";
            this.lblTienPhatValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSoNgayQuaHanValue
            // 
            this.lblSoNgayQuaHanValue.AutoSize = true;
            this.lblSoNgayQuaHanValue.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblSoNgayQuaHanValue.ForeColor = System.Drawing.Color.Gray;
            this.lblSoNgayQuaHanValue.Location = new System.Drawing.Point(180, 30);
            this.lblSoNgayQuaHanValue.Name = "lblSoNgayQuaHanValue";
            this.lblSoNgayQuaHanValue.Size = new System.Drawing.Size(123, 13);
            this.lblSoNgayQuaHanValue.TabIndex = 2;
            this.lblSoNgayQuaHanValue.Text = "(0 ngày × 0 × 150%)";
            this.lblSoNgayQuaHanValue.Visible = false;
            // 
            // lblTienHoanTraSom
            // 
            this.lblTienHoanTraSom.AutoSize = true;
            this.lblTienHoanTraSom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTienHoanTraSom.Location = new System.Drawing.Point(20, 55);
            this.lblTienHoanTraSom.Name = "lblTienHoanTraSom";
            this.lblTienHoanTraSom.Size = new System.Drawing.Size(133, 15);
            this.lblTienHoanTraSom.TabIndex = 3;
            this.lblTienHoanTraSom.Text = " Tiền hoàn (trả sớm):";
            // 
            // lblTienHoanTraSomValue
            // 
            this.lblTienHoanTraSomValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTienHoanTraSomValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblTienHoanTraSomValue.Location = new System.Drawing.Point(370, 55);
            this.lblTienHoanTraSomValue.Name = "lblTienHoanTraSomValue";
            this.lblTienHoanTraSomValue.Size = new System.Drawing.Size(170, 15);
            this.lblTienHoanTraSomValue.TabIndex = 4;
            this.lblTienHoanTraSomValue.Text = "0 VNĐ";
            this.lblTienHoanTraSomValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSoNgayTraSomDetail
            // 
            this.lblSoNgayTraSomDetail.AutoSize = true;
            this.lblSoNgayTraSomDetail.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblSoNgayTraSomDetail.ForeColor = System.Drawing.Color.Gray;
            this.lblSoNgayTraSomDetail.Location = new System.Drawing.Point(180, 55);
            this.lblSoNgayTraSomDetail.Name = "lblSoNgayTraSomDetail";
            this.lblSoNgayTraSomDetail.Size = new System.Drawing.Size(115, 13);
            this.lblSoNgayTraSomDetail.TabIndex = 5;
            this.lblSoNgayTraSomDetail.Text = "(0 ngày × 0 × 70%)";
            this.lblSoNgayTraSomDetail.Visible = false;
            // 
            // pnlSeparator
            // 
            this.pnlSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.pnlSeparator.Location = new System.Drawing.Point(20, 80);
            this.pnlSeparator.Name = "pnlSeparator";
            this.pnlSeparator.Size = new System.Drawing.Size(520, 2);
            this.pnlSeparator.TabIndex = 6;
            // 
            // lblTienHoanCoc
            // 
            this.lblTienHoanCoc.AutoSize = true;
            this.lblTienHoanCoc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTienHoanCoc.Location = new System.Drawing.Point(20, 95);
            this.lblTienHoanCoc.Name = "lblTienHoanCoc";
            this.lblTienHoanCoc.Size = new System.Drawing.Size(189, 19);
            this.lblTienHoanCoc.TabIndex = 7;
            this.lblTienHoanCoc.Text = " TỔNG TIỀN HOÀN CỌC:";
            // 
            // lblTienHoanCocValue
            // 
            this.lblTienHoanCocValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTienHoanCocValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblTienHoanCocValue.Location = new System.Drawing.Point(300, 93);
            this.lblTienHoanCocValue.Name = "lblTienHoanCocValue";
            this.lblTienHoanCocValue.Size = new System.Drawing.Size(240, 21);
            this.lblTienHoanCocValue.TabIndex = 8;
            this.lblTienHoanCocValue.Text = "0 VNĐ";
            this.lblTienHoanCocValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKetLuan
            // 
            this.lblKetLuan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblKetLuan.ForeColor = System.Drawing.Color.Gray;
            this.lblKetLuan.Location = new System.Drawing.Point(20, 120);
            this.lblKetLuan.Name = "lblKetLuan";
            this.lblKetLuan.Size = new System.Drawing.Size(520, 20);
            this.lblKetLuan.TabIndex = 9;
            this.lblKetLuan.Text = "= Không hoàn, không thu thêm";
            this.lblKetLuan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnXacNhan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXacNhan.FlatAppearance.BorderSize = 0;
            this.btnXacNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(20, 690);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(270, 45);
            this.btnXacNhan.TabIndex = 8;
            this.btnXacNhan.Text = "✓ XÁC NHẬN TRẢ XE";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.BtnXacNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(310, 690);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(270, 45);
            this.btnHuy.TabIndex = 9;
            this.btnHuy.Text = "✗ HỦY";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // FormTraXe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(600, 750);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.grpTinhToan);
            this.Controls.Add(this.grpNhapLieu);
            this.Controls.Add(this.grpThongTinBanDau);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTraXe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xác Nhận Trả Xe";
            this.pnlHeader.ResumeLayout(false);
            this.grpThongTinBanDau.ResumeLayout(false);
            this.grpThongTinBanDau.PerformLayout();
            this.grpNhapLieu.ResumeLayout(false);
            this.grpNhapLieu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKmKetThuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChiPhiPhatSinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNgayTraSom)).EndInit();
            this.grpTinhToan.ResumeLayout(false);
            this.grpTinhToan.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.GroupBox grpThongTinBanDau;
        private System.Windows.Forms.Label lblKmBatDau;
        private System.Windows.Forms.Label lblKmBatDauValue;
        private System.Windows.Forms.Label lblNgayBatDau;
        private System.Windows.Forms.Label lblNgayBatDauValue;
        private System.Windows.Forms.Label lblNgayKetThuc;
        private System.Windows.Forms.Label lblNgayKetThucValue;
        private System.Windows.Forms.Label lblSoTienCoc;
        private System.Windows.Forms.Label lblSoTienCocValue;
        private System.Windows.Forms.Label lblGiaThueNgay;
        private System.Windows.Forms.Label lblGiaThueNgayValue;
        private System.Windows.Forms.GroupBox grpNhapLieu;
        private System.Windows.Forms.Label lblNgayTra;
        private System.Windows.Forms.DateTimePicker dtpNgayTra;
        private System.Windows.Forms.Label lblKmKetThuc;
        private System.Windows.Forms.NumericUpDown nudKmKetThuc;
        private System.Windows.Forms.Label lblKmChay;
        private System.Windows.Forms.Label lblKmChayValue;
        private System.Windows.Forms.Label lblTinhTrang;
        private System.Windows.Forms.ComboBox cboTinhTrang;
        private System.Windows.Forms.Label lblChiPhi;
        private System.Windows.Forms.NumericUpDown nudChiPhiPhatSinh;
        private System.Windows.Forms.Label lblVND1;
        private System.Windows.Forms.CheckBox chkTraSom;
        private System.Windows.Forms.NumericUpDown nudSoNgayTraSom;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.GroupBox grpTinhToan;
        private System.Windows.Forms.Label lblTienPhat;
        private System.Windows.Forms.Label lblTienPhatValue;
        private System.Windows.Forms.Label lblSoNgayQuaHanValue;
        private System.Windows.Forms.Label lblTienHoanTraSom;
        private System.Windows.Forms.Label lblTienHoanTraSomValue;
        private System.Windows.Forms.Label lblSoNgayTraSomDetail;
        private System.Windows.Forms.Panel pnlSeparator;
        private System.Windows.Forms.Label lblTienHoanCoc;
        private System.Windows.Forms.Label lblTienHoanCocValue;
        private System.Windows.Forms.Label lblKetLuan;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuy;
    }
}