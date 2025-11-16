namespace UI.FormUI
{
    partial class FormDuyetDonHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblTitle;

        // Thông tin giao dịch
        private System.Windows.Forms.Label lblMaGiaoDich;
        private System.Windows.Forms.TextBox txtMaGiaoDich;

        // Thông tin khách hàng
        private System.Windows.Forms.GroupBox grpKhachHang;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.TextBox txtKhachHang;
        private System.Windows.Forms.Label lblSdtKhachHang;
        private System.Windows.Forms.TextBox txtSdtKhachHang;
        private System.Windows.Forms.Label lblEmailKhachHang;
        private System.Windows.Forms.TextBox txtEmailKhachHang;
        private System.Windows.Forms.Label lblDiaChiKhachHang;
        private System.Windows.Forms.TextBox txtDiaChiKhachHang;

        // Thông tin xe
        private System.Windows.Forms.GroupBox grpXe;
        private System.Windows.Forms.Label lblXe;
        private System.Windows.Forms.TextBox txtXe;
        private System.Windows.Forms.Label lblBienSo;
        private System.Windows.Forms.TextBox txtBienSo;

        // Thông tin giao dịch bán
        private System.Windows.Forms.Label lblNgayBan;
        private System.Windows.Forms.DateTimePicker dtpNgayBan;
        private System.Windows.Forms.Label lblGiaBan;
        private System.Windows.Forms.TextBox txtGiaBan;

        // Thông tin giao dịch thuê
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

        // Thông tin thanh toán
        private System.Windows.Forms.GroupBox grpThanhToan;
        private System.Windows.Forms.Label lblTrangThaiThanhToan;
        private System.Windows.Forms.TextBox txtTrangThaiThanhToan;
        private System.Windows.Forms.Label lblHinhThucThanhToan;
        private System.Windows.Forms.TextBox txtHinhThucThanhToan;

        // Thông tin duyệt
        private System.Windows.Forms.GroupBox grpDuyet;
        private System.Windows.Forms.Label lblTrangThaiDuyet;
        private System.Windows.Forms.TextBox txtTrangThaiDuyet;
        private System.Windows.Forms.Label lblNgayDuyet;
        private System.Windows.Forms.DateTimePicker dtpNgayDuyet;
        private System.Windows.Forms.Label lblNguoiDuyet;
        private System.Windows.Forms.TextBox txtNguoiDuyet;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;

        // Buttons
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnReject;
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblMaGiaoDich = new System.Windows.Forms.Label();
            this.txtMaGiaoDich = new System.Windows.Forms.TextBox();
            this.grpKhachHang = new System.Windows.Forms.GroupBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.txtKhachHang = new System.Windows.Forms.TextBox();
            this.lblSdtKhachHang = new System.Windows.Forms.Label();
            this.txtSdtKhachHang = new System.Windows.Forms.TextBox();
            this.lblEmailKhachHang = new System.Windows.Forms.Label();
            this.txtEmailKhachHang = new System.Windows.Forms.TextBox();
            this.lblDiaChiKhachHang = new System.Windows.Forms.Label();
            this.txtDiaChiKhachHang = new System.Windows.Forms.TextBox();
            this.grpXe = new System.Windows.Forms.GroupBox();
            this.lblXe = new System.Windows.Forms.Label();
            this.txtXe = new System.Windows.Forms.TextBox();
            this.lblBienSo = new System.Windows.Forms.Label();
            this.txtBienSo = new System.Windows.Forms.TextBox();
            this.lblNgayBan = new System.Windows.Forms.Label();
            this.dtpNgayBan = new System.Windows.Forms.DateTimePicker();
            this.lblGiaBan = new System.Windows.Forms.Label();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
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
            this.grpThanhToan = new System.Windows.Forms.GroupBox();
            this.lblTrangThaiThanhToan = new System.Windows.Forms.Label();
            this.txtTrangThaiThanhToan = new System.Windows.Forms.TextBox();
            this.lblHinhThucThanhToan = new System.Windows.Forms.Label();
            this.txtHinhThucThanhToan = new System.Windows.Forms.TextBox();
            this.grpDuyet = new System.Windows.Forms.GroupBox();
            this.lblTrangThaiDuyet = new System.Windows.Forms.Label();
            this.txtTrangThaiDuyet = new System.Windows.Forms.TextBox();
            this.lblNgayDuyet = new System.Windows.Forms.Label();
            this.dtpNgayDuyet = new System.Windows.Forms.DateTimePicker();
            this.lblNguoiDuyet = new System.Windows.Forms.Label();
            this.txtNguoiDuyet = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.grpKhachHang.SuspendLayout();
            this.grpXe.SuspendLayout();
            this.grpThanhToan.SuspendLayout();
            this.grpDuyet.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(900, 60);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(270, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CHI TIẾT ĐƠN HÀNG";
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.grpDuyet);
            this.panelMain.Controls.Add(this.grpThanhToan);
            this.panelMain.Controls.Add(this.grpXe);
            this.panelMain.Controls.Add(this.grpKhachHang);
            this.panelMain.Controls.Add(this.txtMaGiaoDich);
            this.panelMain.Controls.Add(this.lblMaGiaoDich);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 60);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(900, 590);
            this.panelMain.TabIndex = 1;
            // 
            // lblMaGiaoDich
            // 
            this.lblMaGiaoDich.AutoSize = true;
            this.lblMaGiaoDich.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMaGiaoDich.Location = new System.Drawing.Point(20, 20);
            this.lblMaGiaoDich.Name = "lblMaGiaoDich";
            this.lblMaGiaoDich.Size = new System.Drawing.Size(115, 19);
            this.lblMaGiaoDich.TabIndex = 0;
            this.lblMaGiaoDich.Text = "Mã Giao Dịch:";
            // 
            // txtMaGiaoDich
            // 
            this.txtMaGiaoDich.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaGiaoDich.Location = new System.Drawing.Point(150, 17);
            this.txtMaGiaoDich.Name = "txtMaGiaoDich";
            this.txtMaGiaoDich.ReadOnly = true;
            this.txtMaGiaoDich.Size = new System.Drawing.Size(200, 25);
            this.txtMaGiaoDich.TabIndex = 1;
            // 
            // grpKhachHang
            // 
            this.grpKhachHang.Controls.Add(this.txtDiaChiKhachHang);
            this.grpKhachHang.Controls.Add(this.lblDiaChiKhachHang);
            this.grpKhachHang.Controls.Add(this.txtEmailKhachHang);
            this.grpKhachHang.Controls.Add(this.lblEmailKhachHang);
            this.grpKhachHang.Controls.Add(this.txtSdtKhachHang);
            this.grpKhachHang.Controls.Add(this.lblSdtKhachHang);
            this.grpKhachHang.Controls.Add(this.txtKhachHang);
            this.grpKhachHang.Controls.Add(this.lblKhachHang);
            this.grpKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpKhachHang.Location = new System.Drawing.Point(20, 60);
            this.grpKhachHang.Name = "grpKhachHang";
            this.grpKhachHang.Size = new System.Drawing.Size(840, 140);
            this.grpKhachHang.TabIndex = 2;
            this.grpKhachHang.TabStop = false;
            this.grpKhachHang.Text = "Thông Tin Khách Hàng";
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblKhachHang.Location = new System.Drawing.Point(15, 30);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(76, 19);
            this.lblKhachHang.TabIndex = 0;
            this.lblKhachHang.Text = "Họ và tên:";
            // 
            // txtKhachHang
            // 
            this.txtKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtKhachHang.Location = new System.Drawing.Point(130, 27);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.ReadOnly = true;
            this.txtKhachHang.Size = new System.Drawing.Size(280, 25);
            this.txtKhachHang.TabIndex = 1;
            // 
            // lblSdtKhachHang
            // 
            this.lblSdtKhachHang.AutoSize = true;
            this.lblSdtKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSdtKhachHang.Location = new System.Drawing.Point(430, 30);
            this.lblSdtKhachHang.Name = "lblSdtKhachHang";
            this.lblSdtKhachHang.Size = new System.Drawing.Size(39, 19);
            this.lblSdtKhachHang.TabIndex = 2;
            this.lblSdtKhachHang.Text = "SĐT:";
            // 
            // txtSdtKhachHang
            // 
            this.txtSdtKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSdtKhachHang.Location = new System.Drawing.Point(540, 27);
            this.txtSdtKhachHang.Name = "txtSdtKhachHang";
            this.txtSdtKhachHang.ReadOnly = true;
            this.txtSdtKhachHang.Size = new System.Drawing.Size(280, 25);
            this.txtSdtKhachHang.TabIndex = 3;
            // 
            // lblEmailKhachHang
            // 
            this.lblEmailKhachHang.AutoSize = true;
            this.lblEmailKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmailKhachHang.Location = new System.Drawing.Point(15, 65);
            this.lblEmailKhachHang.Name = "lblEmailKhachHang";
            this.lblEmailKhachHang.Size = new System.Drawing.Size(47, 19);
            this.lblEmailKhachHang.TabIndex = 4;
            this.lblEmailKhachHang.Text = "Email:";
            // 
            // txtEmailKhachHang
            // 
            this.txtEmailKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmailKhachHang.Location = new System.Drawing.Point(130, 62);
            this.txtEmailKhachHang.Name = "txtEmailKhachHang";
            this.txtEmailKhachHang.ReadOnly = true;
            this.txtEmailKhachHang.Size = new System.Drawing.Size(690, 25);
            this.txtEmailKhachHang.TabIndex = 5;
            // 
            // lblDiaChiKhachHang
            // 
            this.lblDiaChiKhachHang.AutoSize = true;
            this.lblDiaChiKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiaChiKhachHang.Location = new System.Drawing.Point(15, 100);
            this.lblDiaChiKhachHang.Name = "lblDiaChiKhachHang";
            this.lblDiaChiKhachHang.Size = new System.Drawing.Size(56, 19);
            this.lblDiaChiKhachHang.TabIndex = 6;
            this.lblDiaChiKhachHang.Text = "Địa chỉ:";
            // 
            // txtDiaChiKhachHang
            // 
            this.txtDiaChiKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiaChiKhachHang.Location = new System.Drawing.Point(130, 97);
            this.txtDiaChiKhachHang.Name = "txtDiaChiKhachHang";
            this.txtDiaChiKhachHang.ReadOnly = true;
            this.txtDiaChiKhachHang.Size = new System.Drawing.Size(690, 25);
            this.txtDiaChiKhachHang.TabIndex = 7;
            // 
            // grpXe
            // 
            this.grpXe.Controls.Add(this.txtGiayToGiuLai);
            this.grpXe.Controls.Add(this.lblGiayToGiuLai);
            this.grpXe.Controls.Add(this.txtSoTienCoc);
            this.grpXe.Controls.Add(this.lblSoTienCoc);
            this.grpXe.Controls.Add(this.txtTongGia);
            this.grpXe.Controls.Add(this.lblTongGia);
            this.grpXe.Controls.Add(this.txtGiaThueNgay);
            this.grpXe.Controls.Add(this.lblGiaThueNgay);
            this.grpXe.Controls.Add(this.txtSoNgayThue);
            this.grpXe.Controls.Add(this.lblSoNgayThue);
            this.grpXe.Controls.Add(this.dtpNgayKetThuc);
            this.grpXe.Controls.Add(this.lblNgayKetThuc);
            this.grpXe.Controls.Add(this.dtpNgayBatDau);
            this.grpXe.Controls.Add(this.lblNgayBatDau);
            this.grpXe.Controls.Add(this.txtGiaBan);
            this.grpXe.Controls.Add(this.lblGiaBan);
            this.grpXe.Controls.Add(this.dtpNgayBan);
            this.grpXe.Controls.Add(this.lblNgayBan);
            this.grpXe.Controls.Add(this.txtBienSo);
            this.grpXe.Controls.Add(this.lblBienSo);
            this.grpXe.Controls.Add(this.txtXe);
            this.grpXe.Controls.Add(this.lblXe);
            this.grpXe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpXe.Location = new System.Drawing.Point(20, 210);
            this.grpXe.Name = "grpXe";
            this.grpXe.Size = new System.Drawing.Size(840, 200);
            this.grpXe.TabIndex = 3;
            this.grpXe.TabStop = false;
            this.grpXe.Text = "Thông Tin Xe & Giao Dịch";
            // 
            // lblXe
            // 
            this.lblXe.AutoSize = true;
            this.lblXe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblXe.Location = new System.Drawing.Point(15, 30);
            this.lblXe.Name = "lblXe";
            this.lblXe.Size = new System.Drawing.Size(27, 19);
            this.lblXe.TabIndex = 0;
            this.lblXe.Text = "Xe:";
            // 
            // txtXe
            // 
            this.txtXe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtXe.Location = new System.Drawing.Point(130, 27);
            this.txtXe.Name = "txtXe";
            this.txtXe.ReadOnly = true;
            this.txtXe.Size = new System.Drawing.Size(280, 25);
            this.txtXe.TabIndex = 1;
            // 
            // lblBienSo
            // 
            this.lblBienSo.AutoSize = true;
            this.lblBienSo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBienSo.Location = new System.Drawing.Point(430, 30);
            this.lblBienSo.Name = "lblBienSo";
            this.lblBienSo.Size = new System.Drawing.Size(62, 19);
            this.lblBienSo.TabIndex = 2;
            this.lblBienSo.Text = "Biển số:";
            // 
            // txtBienSo
            // 
            this.txtBienSo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBienSo.Location = new System.Drawing.Point(540, 27);
            this.txtBienSo.Name = "txtBienSo";
            this.txtBienSo.ReadOnly = true;
            this.txtBienSo.Size = new System.Drawing.Size(280, 25);
            this.txtBienSo.TabIndex = 3;
            // 
            // lblNgayBan
            // 
            this.lblNgayBan.AutoSize = true;
            this.lblNgayBan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgayBan.Location = new System.Drawing.Point(15, 65);
            this.lblNgayBan.Name = "lblNgayBan";
            this.lblNgayBan.Size = new System.Drawing.Size(74, 19);
            this.lblNgayBan.TabIndex = 4;
            this.lblNgayBan.Text = "Ngày bán:";
            // 
            // dtpNgayBan
            // 
            this.dtpNgayBan.Enabled = false;
            this.dtpNgayBan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayBan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBan.Location = new System.Drawing.Point(130, 62);
            this.dtpNgayBan.Name = "dtpNgayBan";
            this.dtpNgayBan.Size = new System.Drawing.Size(280, 25);
            this.dtpNgayBan.TabIndex = 5;
            // 
            // lblGiaBan
            // 
            this.lblGiaBan.AutoSize = true;
            this.lblGiaBan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGiaBan.Location = new System.Drawing.Point(430, 65);
            this.lblGiaBan.Name = "lblGiaBan";
            this.lblGiaBan.Size = new System.Drawing.Size(63, 19);
            this.lblGiaBan.TabIndex = 6;
            this.lblGiaBan.Text = "Giá bán:";
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtGiaBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.txtGiaBan.Location = new System.Drawing.Point(540, 62);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.ReadOnly = true;
            this.txtGiaBan.Size = new System.Drawing.Size(280, 25);
            this.txtGiaBan.TabIndex = 7;
            this.txtGiaBan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNgayBatDau
            // 
            this.lblNgayBatDau.AutoSize = true;
            this.lblNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgayBatDau.Location = new System.Drawing.Point(15, 65);
            this.lblNgayBatDau.Name = "lblNgayBatDau";
            this.lblNgayBatDau.Size = new System.Drawing.Size(100, 19);
            this.lblNgayBatDau.TabIndex = 8;
            this.lblNgayBatDau.Text = "Ngày bắt đầu:";
            // 
            // dtpNgayBatDau
            // 
            this.dtpNgayBatDau.Enabled = false;
            this.dtpNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBatDau.Location = new System.Drawing.Point(130, 62);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(280, 25);
            this.dtpNgayBatDau.TabIndex = 9;
            // 
            // lblNgayKetThuc
            // 
            this.lblNgayKetThuc.AutoSize = true;
            this.lblNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgayKetThuc.Location = new System.Drawing.Point(430, 65);
            this.lblNgayKetThuc.Name = "lblNgayKetThuc";
            this.lblNgayKetThuc.Size = new System.Drawing.Size(105, 19);
            this.lblNgayKetThuc.TabIndex = 10;
            this.lblNgayKetThuc.Text = "Ngày kết thúc:";
            // 
            // dtpNgayKetThuc
            // 
            this.dtpNgayKetThuc.Enabled = false;
            this.dtpNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayKetThuc.Location = new System.Drawing.Point(540, 62);
            this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            this.dtpNgayKetThuc.Size = new System.Drawing.Size(280, 25);
            this.dtpNgayKetThuc.TabIndex = 11;
            // 
            // lblSoNgayThue
            // 
            this.lblSoNgayThue.AutoSize = true;
            this.lblSoNgayThue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSoNgayThue.Location = new System.Drawing.Point(15, 100);
            this.lblSoNgayThue.Name = "lblSoNgayThue";
            this.lblSoNgayThue.Size = new System.Drawing.Size(95, 19);
            this.lblSoNgayThue.TabIndex = 12;
            this.lblSoNgayThue.Text = "Số ngày thuê:";
            // 
            // txtSoNgayThue
            // 
            this.txtSoNgayThue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoNgayThue.Location = new System.Drawing.Point(130, 97);
            this.txtSoNgayThue.Name = "txtSoNgayThue";
            this.txtSoNgayThue.ReadOnly = true;
            this.txtSoNgayThue.Size = new System.Drawing.Size(280, 25);
            this.txtSoNgayThue.TabIndex = 13;
            // 
            // lblGiaThueNgay
            // 
            this.lblGiaThueNgay.AutoSize = true;
            this.lblGiaThueNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGiaThueNgay.Location = new System.Drawing.Point(430, 100);
            this.lblGiaThueNgay.Name = "lblGiaThueNgay";
            this.lblGiaThueNgay.Size = new System.Drawing.Size(105, 19);
            this.lblGiaThueNgay.TabIndex = 14;
            this.lblGiaThueNgay.Text = "Giá thuê/ngày:";
            // 
            // txtGiaThueNgay
            // 
            this.txtGiaThueNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGiaThueNgay.Location = new System.Drawing.Point(540, 97);
            this.txtGiaThueNgay.Name = "txtGiaThueNgay";
            this.txtGiaThueNgay.ReadOnly = true;
            this.txtGiaThueNgay.Size = new System.Drawing.Size(280, 25);
            this.txtGiaThueNgay.TabIndex = 15;
            this.txtGiaThueNgay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTongGia
            // 
            this.lblTongGia.AutoSize = true;
            this.lblTongGia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongGia.Location = new System.Drawing.Point(15, 135);
            this.lblTongGia.Name = "lblTongGia";
            this.lblTongGia.Size = new System.Drawing.Size(70, 19);
            this.lblTongGia.TabIndex = 16;
            this.lblTongGia.Text = "Tổng giá:";
            // 
            // txtTongGia
            // 
            this.txtTongGia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtTongGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.txtTongGia.Location = new System.Drawing.Point(130, 132);
            this.txtTongGia.Name = "txtTongGia";
            this.txtTongGia.ReadOnly = true;
            this.txtTongGia.Size = new System.Drawing.Size(280, 25);
            this.txtTongGia.TabIndex = 17;
            this.txtTongGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSoTienCoc
            // 
            this.lblSoTienCoc.AutoSize = true;
            this.lblSoTienCoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSoTienCoc.Location = new System.Drawing.Point(430, 135);
            this.lblSoTienCoc.Name = "lblSoTienCoc";
            this.lblSoTienCoc.Size = new System.Drawing.Size(94, 19);
            this.lblSoTienCoc.TabIndex = 18;
            this.lblSoTienCoc.Text = "Tiền đặt cọc:";
            // 
            // txtSoTienCoc
            // 
            this.txtSoTienCoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoTienCoc.Location = new System.Drawing.Point(540, 132);
            this.txtSoTienCoc.Name = "txtSoTienCoc";
            this.txtSoTienCoc.ReadOnly = true;
            this.txtSoTienCoc.Size = new System.Drawing.Size(280, 25);
            this.txtSoTienCoc.TabIndex = 19;
            this.txtSoTienCoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblGiayToGiuLai
            // 
            this.lblGiayToGiuLai.AutoSize = true;
            this.lblGiayToGiuLai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGiayToGiuLai.Location = new System.Drawing.Point(15, 170);
            this.lblGiayToGiuLai.Name = "lblGiayToGiuLai";
            this.lblGiayToGiuLai.Size = new System.Drawing.Size(107, 19);
            this.lblGiayToGiuLai.TabIndex = 20;
            this.lblGiayToGiuLai.Text = "Giấy tờ giữ lại:";
            // 
            // txtGiayToGiuLai
            // 
            this.txtGiayToGiuLai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGiayToGiuLai.Location = new System.Drawing.Point(130, 167);
            this.txtGiayToGiuLai.Name = "txtGiayToGiuLai";
            this.txtGiayToGiuLai.ReadOnly = true;
            this.txtGiayToGiuLai.Size = new System.Drawing.Size(690, 25);
            this.txtGiayToGiuLai.TabIndex = 21;
            // 
            // grpThanhToan
            // 
            this.grpThanhToan.Controls.Add(this.txtHinhThucThanhToan);
            this.grpThanhToan.Controls.Add(this.lblHinhThucThanhToan);
            this.grpThanhToan.Controls.Add(this.txtTrangThaiThanhToan);
            this.grpThanhToan.Controls.Add(this.lblTrangThaiThanhToan);
            this.grpThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpThanhToan.Location = new System.Drawing.Point(20, 420);
            this.grpThanhToan.Name = "grpThanhToan";
            this.grpThanhToan.Size = new System.Drawing.Size(840, 90);
            this.grpThanhToan.TabIndex = 4;
            this.grpThanhToan.TabStop = false;
            this.grpThanhToan.Text = "Thông Tin Thanh Toán";
            // 
            // lblTrangThaiThanhToan
            // 
            this.lblTrangThaiThanhToan.AutoSize = true;
            this.lblTrangThaiThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrangThaiThanhToan.Location = new System.Drawing.Point(15, 30);
            this.lblTrangThaiThanhToan.Name = "lblTrangThaiThanhToan";
            this.lblTrangThaiThanhToan.Size = new System.Drawing.Size(77, 19);
            this.lblTrangThaiThanhToan.TabIndex = 0;
            this.lblTrangThaiThanhToan.Text = "Trạng thái:";
            // 
            // txtTrangThaiThanhToan
            // 
            this.txtTrangThaiThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTrangThaiThanhToan.Location = new System.Drawing.Point(130, 27);
            this.txtTrangThaiThanhToan.Name = "txtTrangThaiThanhToan";
            this.txtTrangThaiThanhToan.ReadOnly = true;
            this.txtTrangThaiThanhToan.Size = new System.Drawing.Size(280, 25);
            this.txtTrangThaiThanhToan.TabIndex = 1;
            // 
            // lblHinhThucThanhToan
            // 
            this.lblHinhThucThanhToan.AutoSize = true;
            this.lblHinhThucThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHinhThucThanhToan.Location = new System.Drawing.Point(430, 30);
            this.lblHinhThucThanhToan.Name = "lblHinhThucThanhToan";
            this.lblHinhThucThanhToan.Size = new System.Drawing.Size(82, 19);
            this.lblHinhThucThanhToan.TabIndex = 2;
            this.lblHinhThucThanhToan.Text = "Hình thức:";
            // 
            // txtHinhThucThanhToan
            // 
            this.txtHinhThucThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHinhThucThanhToan.Location = new System.Drawing.Point(540, 27);
            this.txtHinhThucThanhToan.Name = "txtHinhThucThanhToan";
            this.txtHinhThucThanhToan.ReadOnly = true;
            this.txtHinhThucThanhToan.Size = new System.Drawing.Size(280, 25);
            this.txtHinhThucThanhToan.TabIndex = 3;
            // 
            // grpDuyet
            // 
            this.grpDuyet.Controls.Add(this.txtGhiChu);
            this.grpDuyet.Controls.Add(this.lblGhiChu);
            this.grpDuyet.Controls.Add(this.txtNguoiDuyet);
            this.grpDuyet.Controls.Add(this.lblNguoiDuyet);
            this.grpDuyet.Controls.Add(this.dtpNgayDuyet);
            this.grpDuyet.Controls.Add(this.lblNgayDuyet);
            this.grpDuyet.Controls.Add(this.txtTrangThaiDuyet);
            this.grpDuyet.Controls.Add(this.lblTrangThaiDuyet);
            this.grpDuyet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpDuyet.Location = new System.Drawing.Point(20, 520);
            this.grpDuyet.Name = "grpDuyet";
            this.grpDuyet.Size = new System.Drawing.Size(840, 160);
            this.grpDuyet.TabIndex = 5;
            this.grpDuyet.TabStop = false;
            this.grpDuyet.Text = "Thông Tin Duyệt";
            // 
            // lblTrangThaiDuyet
            // 
            this.lblTrangThaiDuyet.AutoSize = true;
            this.lblTrangThaiDuyet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrangThaiDuyet.Location = new System.Drawing.Point(15, 30);
            this.lblTrangThaiDuyet.Name = "lblTrangThaiDuyet";
            this.lblTrangThaiDuyet.Size = new System.Drawing.Size(77, 19);
            this.lblTrangThaiDuyet.TabIndex = 0;
            this.lblTrangThaiDuyet.Text = "Trạng thái:";
            // 
            // txtTrangThaiDuyet
            // 
            this.txtTrangThaiDuyet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtTrangThaiDuyet.Location = new System.Drawing.Point(130, 27);
            this.txtTrangThaiDuyet.Name = "txtTrangThaiDuyet";
            this.txtTrangThaiDuyet.ReadOnly = true;
            this.txtTrangThaiDuyet.Size = new System.Drawing.Size(280, 25);
            this.txtTrangThaiDuyet.TabIndex = 1;
            // 
            // lblNgayDuyet
            // 
            this.lblNgayDuyet.AutoSize = true;
            this.lblNgayDuyet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgayDuyet.Location = new System.Drawing.Point(430, 30);
            this.lblNgayDuyet.Name = "lblNgayDuyet";
            this.lblNgayDuyet.Size = new System.Drawing.Size(85, 19);
            this.lblNgayDuyet.TabIndex = 2;
            this.lblNgayDuyet.Text = "Ngày duyệt:";
            // 
            // dtpNgayDuyet
            // 
            this.dtpNgayDuyet.Enabled = false;
            this.dtpNgayDuyet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayDuyet.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayDuyet.Location = new System.Drawing.Point(540, 27);
            this.dtpNgayDuyet.Name = "dtpNgayDuyet";
            this.dtpNgayDuyet.Size = new System.Drawing.Size(280, 25);
            this.dtpNgayDuyet.TabIndex = 3;
            // 
            // lblNguoiDuyet
            // 
            this.lblNguoiDuyet.AutoSize = true;
            this.lblNguoiDuyet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNguoiDuyet.Location = new System.Drawing.Point(15, 65);
            this.lblNguoiDuyet.Name = "lblNguoiDuyet";
            this.lblNguoiDuyet.Size = new System.Drawing.Size(95, 19);
            this.lblNguoiDuyet.TabIndex = 4;
            this.lblNguoiDuyet.Text = "Người duyệt:";
            // 
            // txtNguoiDuyet
            // 
            this.txtNguoiDuyet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNguoiDuyet.Location = new System.Drawing.Point(130, 62);
            this.txtNguoiDuyet.Name = "txtNguoiDuyet";
            this.txtNguoiDuyet.ReadOnly = true;
            this.txtNguoiDuyet.Size = new System.Drawing.Size(690, 25);
            this.txtNguoiDuyet.TabIndex = 5;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGhiChu.Location = new System.Drawing.Point(15, 100);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(61, 19);
            this.lblGhiChu.TabIndex = 6;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(130, 97);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(690, 50);
            this.txtGhiChu.TabIndex = 7;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.White;
            this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Controls.Add(this.btnReject);
            this.panelBottom.Controls.Add(this.btnApprove);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 650);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(900, 70);
            this.panelBottom.TabIndex = 2;
            // 
            // btnApprove
            // 
            this.btnApprove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnApprove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApprove.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnApprove.ForeColor = System.Drawing.Color.White;
            this.btnApprove.Location = new System.Drawing.Point(20, 15);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(150, 40);
            this.btnApprove.TabIndex = 0;
            this.btnApprove.Text = "✅ Duyệt";
            this.btnApprove.UseVisualStyleBackColor = false;
            this.btnApprove.Click += new System.EventHandler(this.BtnApprove_Click);
            // 
            // btnReject
            // 
            this.btnReject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnReject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReject.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnReject.ForeColor = System.Drawing.Color.White;
            this.btnReject.Location = new System.Drawing.Point(180, 15);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(150, 40);
            this.btnReject.TabIndex = 1;
            this.btnReject.Text = "❌ Từ Chối";
            this.btnReject.UseVisualStyleBackColor = false;
            this.btnReject.Click += new System.EventHandler(this.BtnReject_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(728, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 40);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "🚪 Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // FormDuyetDonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 720);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDuyetDonHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi Tiết Đơn Hàng";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.grpKhachHang.ResumeLayout(false);
            this.grpKhachHang.PerformLayout();
            this.grpXe.ResumeLayout(false);
            this.grpXe.PerformLayout();
            this.grpThanhToan.ResumeLayout(false);
            this.grpThanhToan.PerformLayout();
            this.grpDuyet.ResumeLayout(false);
            this.grpDuyet.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}