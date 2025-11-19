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
            this.grpDuyet = new System.Windows.Forms.GroupBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtNguoiDuyet = new System.Windows.Forms.TextBox();
            this.lblNguoiDuyet = new System.Windows.Forms.Label();
            this.dtpNgayDuyet = new System.Windows.Forms.DateTimePicker();
            this.lblNgayDuyet = new System.Windows.Forms.Label();
            this.txtTrangThaiDuyet = new System.Windows.Forms.TextBox();
            this.lblTrangThaiDuyet = new System.Windows.Forms.Label();
            this.grpThanhToan = new System.Windows.Forms.GroupBox();
            this.txtHinhThucThanhToan = new System.Windows.Forms.TextBox();
            this.lblHinhThucThanhToan = new System.Windows.Forms.Label();
            this.txtTrangThaiThanhToan = new System.Windows.Forms.TextBox();
            this.lblTrangThaiThanhToan = new System.Windows.Forms.Label();
            this.grpXe = new System.Windows.Forms.GroupBox();
            this.txtGiayToGiuLai = new System.Windows.Forms.TextBox();
            this.lblGiayToGiuLai = new System.Windows.Forms.Label();
            this.txtSoTienCoc = new System.Windows.Forms.TextBox();
            this.lblSoTienCoc = new System.Windows.Forms.Label();
            this.txtTongGia = new System.Windows.Forms.TextBox();
            this.lblTongGia = new System.Windows.Forms.Label();
            this.txtGiaThueNgay = new System.Windows.Forms.TextBox();
            this.lblGiaThueNgay = new System.Windows.Forms.Label();
            this.txtSoNgayThue = new System.Windows.Forms.TextBox();
            this.lblSoNgayThue = new System.Windows.Forms.Label();
            this.dtpNgayKetThuc = new System.Windows.Forms.DateTimePicker();
            this.lblNgayKetThuc = new System.Windows.Forms.Label();
            this.dtpNgayBatDau = new System.Windows.Forms.DateTimePicker();
            this.lblNgayBatDau = new System.Windows.Forms.Label();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.lblGiaBan = new System.Windows.Forms.Label();
            this.dtpNgayBan = new System.Windows.Forms.DateTimePicker();
            this.lblNgayBan = new System.Windows.Forms.Label();
            this.txtBienSo = new System.Windows.Forms.TextBox();
            this.lblBienSo = new System.Windows.Forms.Label();
            this.txtXe = new System.Windows.Forms.TextBox();
            this.lblXe = new System.Windows.Forms.Label();
            this.grpKhachHang = new System.Windows.Forms.GroupBox();
            this.txtDiaChiKhachHang = new System.Windows.Forms.TextBox();
            this.lblDiaChiKhachHang = new System.Windows.Forms.Label();
            this.txtEmailKhachHang = new System.Windows.Forms.TextBox();
            this.lblEmailKhachHang = new System.Windows.Forms.Label();
            this.txtSdtKhachHang = new System.Windows.Forms.TextBox();
            this.lblSdtKhachHang = new System.Windows.Forms.Label();
            this.txtKhachHang = new System.Windows.Forms.TextBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.txtMaGiaoDich = new System.Windows.Forms.TextBox();
            this.lblMaGiaoDich = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnApprove = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.grpDuyet.SuspendLayout();
            this.grpThanhToan.SuspendLayout();
            this.grpXe.SuspendLayout();
            this.grpKhachHang.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 74);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(27, 18);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(314, 41);
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
            this.panelMain.Location = new System.Drawing.Point(0, 74);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.panelMain.Size = new System.Drawing.Size(1200, 726);
            this.panelMain.TabIndex = 1;
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
            this.grpDuyet.Location = new System.Drawing.Point(27, 640);
            this.grpDuyet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDuyet.Name = "grpDuyet";
            this.grpDuyet.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDuyet.Size = new System.Drawing.Size(1120, 197);
            this.grpDuyet.TabIndex = 5;
            this.grpDuyet.TabStop = false;
            this.grpDuyet.Text = "Thông Tin Duyệt";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(173, 119);
            this.txtGhiChu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(919, 61);
            this.txtGhiChu.TabIndex = 7;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGhiChu.Location = new System.Drawing.Point(20, 123);
            this.lblGhiChu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(73, 23);
            this.lblGhiChu.TabIndex = 6;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // txtNguoiDuyet
            // 
            this.txtNguoiDuyet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNguoiDuyet.Location = new System.Drawing.Point(173, 76);
            this.txtNguoiDuyet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNguoiDuyet.Name = "txtNguoiDuyet";
            this.txtNguoiDuyet.ReadOnly = true;
            this.txtNguoiDuyet.Size = new System.Drawing.Size(919, 30);
            this.txtNguoiDuyet.TabIndex = 5;
            // 
            // lblNguoiDuyet
            // 
            this.lblNguoiDuyet.AutoSize = true;
            this.lblNguoiDuyet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNguoiDuyet.Location = new System.Drawing.Point(20, 80);
            this.lblNguoiDuyet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNguoiDuyet.Name = "lblNguoiDuyet";
            this.lblNguoiDuyet.Size = new System.Drawing.Size(109, 23);
            this.lblNguoiDuyet.TabIndex = 4;
            this.lblNguoiDuyet.Text = "Người duyệt:";
            // 
            // dtpNgayDuyet
            // 
            this.dtpNgayDuyet.Enabled = false;
            this.dtpNgayDuyet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayDuyet.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayDuyet.Location = new System.Drawing.Point(720, 33);
            this.dtpNgayDuyet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpNgayDuyet.Name = "dtpNgayDuyet";
            this.dtpNgayDuyet.Size = new System.Drawing.Size(372, 30);
            this.dtpNgayDuyet.TabIndex = 3;
            // 
            // lblNgayDuyet
            // 
            this.lblNgayDuyet.AutoSize = true;
            this.lblNgayDuyet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgayDuyet.Location = new System.Drawing.Point(573, 37);
            this.lblNgayDuyet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgayDuyet.Name = "lblNgayDuyet";
            this.lblNgayDuyet.Size = new System.Drawing.Size(102, 23);
            this.lblNgayDuyet.TabIndex = 2;
            this.lblNgayDuyet.Text = "Ngày duyệt:";
            // 
            // txtTrangThaiDuyet
            // 
            this.txtTrangThaiDuyet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtTrangThaiDuyet.Location = new System.Drawing.Point(173, 33);
            this.txtTrangThaiDuyet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTrangThaiDuyet.Name = "txtTrangThaiDuyet";
            this.txtTrangThaiDuyet.ReadOnly = true;
            this.txtTrangThaiDuyet.Size = new System.Drawing.Size(372, 30);
            this.txtTrangThaiDuyet.TabIndex = 1;
            // 
            // lblTrangThaiDuyet
            // 
            this.lblTrangThaiDuyet.AutoSize = true;
            this.lblTrangThaiDuyet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrangThaiDuyet.Location = new System.Drawing.Point(20, 37);
            this.lblTrangThaiDuyet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrangThaiDuyet.Name = "lblTrangThaiDuyet";
            this.lblTrangThaiDuyet.Size = new System.Drawing.Size(91, 23);
            this.lblTrangThaiDuyet.TabIndex = 0;
            this.lblTrangThaiDuyet.Text = "Trạng thái:";
            // 
            // grpThanhToan
            // 
            this.grpThanhToan.Controls.Add(this.txtHinhThucThanhToan);
            this.grpThanhToan.Controls.Add(this.lblHinhThucThanhToan);
            this.grpThanhToan.Controls.Add(this.txtTrangThaiThanhToan);
            this.grpThanhToan.Controls.Add(this.lblTrangThaiThanhToan);
            this.grpThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpThanhToan.Location = new System.Drawing.Point(27, 517);
            this.grpThanhToan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpThanhToan.Name = "grpThanhToan";
            this.grpThanhToan.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpThanhToan.Size = new System.Drawing.Size(1120, 111);
            this.grpThanhToan.TabIndex = 4;
            this.grpThanhToan.TabStop = false;
            this.grpThanhToan.Text = "Thông Tin Thanh Toán";
            // 
            // txtHinhThucThanhToan
            // 
            this.txtHinhThucThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHinhThucThanhToan.Location = new System.Drawing.Point(720, 33);
            this.txtHinhThucThanhToan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtHinhThucThanhToan.Name = "txtHinhThucThanhToan";
            this.txtHinhThucThanhToan.ReadOnly = true;
            this.txtHinhThucThanhToan.Size = new System.Drawing.Size(372, 30);
            this.txtHinhThucThanhToan.TabIndex = 3;
            // 
            // lblHinhThucThanhToan
            // 
            this.lblHinhThucThanhToan.AutoSize = true;
            this.lblHinhThucThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHinhThucThanhToan.Location = new System.Drawing.Point(573, 37);
            this.lblHinhThucThanhToan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHinhThucThanhToan.Name = "lblHinhThucThanhToan";
            this.lblHinhThucThanhToan.Size = new System.Drawing.Size(89, 23);
            this.lblHinhThucThanhToan.TabIndex = 2;
            this.lblHinhThucThanhToan.Text = "Hình thức:";
            // 
            // txtTrangThaiThanhToan
            // 
            this.txtTrangThaiThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTrangThaiThanhToan.Location = new System.Drawing.Point(173, 33);
            this.txtTrangThaiThanhToan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTrangThaiThanhToan.Name = "txtTrangThaiThanhToan";
            this.txtTrangThaiThanhToan.ReadOnly = true;
            this.txtTrangThaiThanhToan.Size = new System.Drawing.Size(372, 30);
            this.txtTrangThaiThanhToan.TabIndex = 1;
            // 
            // lblTrangThaiThanhToan
            // 
            this.lblTrangThaiThanhToan.AutoSize = true;
            this.lblTrangThaiThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrangThaiThanhToan.Location = new System.Drawing.Point(20, 37);
            this.lblTrangThaiThanhToan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrangThaiThanhToan.Name = "lblTrangThaiThanhToan";
            this.lblTrangThaiThanhToan.Size = new System.Drawing.Size(91, 23);
            this.lblTrangThaiThanhToan.TabIndex = 0;
            this.lblTrangThaiThanhToan.Text = "Trạng thái:";
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
            this.grpXe.Location = new System.Drawing.Point(27, 258);
            this.grpXe.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpXe.Name = "grpXe";
            this.grpXe.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpXe.Size = new System.Drawing.Size(1120, 246);
            this.grpXe.TabIndex = 3;
            this.grpXe.TabStop = false;
            this.grpXe.Text = "Thông Tin Xe & Giao Dịch";
            // 
            // txtGiayToGiuLai
            // 
            this.txtGiayToGiuLai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGiayToGiuLai.Location = new System.Drawing.Point(173, 206);
            this.txtGiayToGiuLai.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtGiayToGiuLai.Name = "txtGiayToGiuLai";
            this.txtGiayToGiuLai.ReadOnly = true;
            this.txtGiayToGiuLai.Size = new System.Drawing.Size(919, 30);
            this.txtGiayToGiuLai.TabIndex = 21;
            // 
            // lblGiayToGiuLai
            // 
            this.lblGiayToGiuLai.AutoSize = true;
            this.lblGiayToGiuLai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGiayToGiuLai.Location = new System.Drawing.Point(20, 209);
            this.lblGiayToGiuLai.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGiayToGiuLai.Name = "lblGiayToGiuLai";
            this.lblGiayToGiuLai.Size = new System.Drawing.Size(119, 23);
            this.lblGiayToGiuLai.TabIndex = 20;
            this.lblGiayToGiuLai.Text = "Giấy tờ giữ lại:";
            // 
            // txtSoTienCoc
            // 
            this.txtSoTienCoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoTienCoc.Location = new System.Drawing.Point(720, 162);
            this.txtSoTienCoc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSoTienCoc.Name = "txtSoTienCoc";
            this.txtSoTienCoc.ReadOnly = true;
            this.txtSoTienCoc.Size = new System.Drawing.Size(372, 30);
            this.txtSoTienCoc.TabIndex = 19;
            this.txtSoTienCoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSoTienCoc
            // 
            this.lblSoTienCoc.AutoSize = true;
            this.lblSoTienCoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSoTienCoc.Location = new System.Drawing.Point(573, 166);
            this.lblSoTienCoc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoTienCoc.Name = "lblSoTienCoc";
            this.lblSoTienCoc.Size = new System.Drawing.Size(107, 23);
            this.lblSoTienCoc.TabIndex = 18;
            this.lblSoTienCoc.Text = "Tiền đặt cọc:";
            // 
            // txtTongGia
            // 
            this.txtTongGia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtTongGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.txtTongGia.Location = new System.Drawing.Point(173, 162);
            this.txtTongGia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTongGia.Name = "txtTongGia";
            this.txtTongGia.ReadOnly = true;
            this.txtTongGia.Size = new System.Drawing.Size(372, 30);
            this.txtTongGia.TabIndex = 17;
            this.txtTongGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTongGia
            // 
            this.lblTongGia.AutoSize = true;
            this.lblTongGia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongGia.Location = new System.Drawing.Point(20, 166);
            this.lblTongGia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTongGia.Name = "lblTongGia";
            this.lblTongGia.Size = new System.Drawing.Size(81, 23);
            this.lblTongGia.TabIndex = 16;
            this.lblTongGia.Text = "Tổng giá:";
            // 
            // txtGiaThueNgay
            // 
            this.txtGiaThueNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGiaThueNgay.Location = new System.Drawing.Point(720, 119);
            this.txtGiaThueNgay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtGiaThueNgay.Name = "txtGiaThueNgay";
            this.txtGiaThueNgay.ReadOnly = true;
            this.txtGiaThueNgay.Size = new System.Drawing.Size(372, 30);
            this.txtGiaThueNgay.TabIndex = 15;
            this.txtGiaThueNgay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblGiaThueNgay
            // 
            this.lblGiaThueNgay.AutoSize = true;
            this.lblGiaThueNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGiaThueNgay.Location = new System.Drawing.Point(573, 123);
            this.lblGiaThueNgay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGiaThueNgay.Name = "lblGiaThueNgay";
            this.lblGiaThueNgay.Size = new System.Drawing.Size(123, 23);
            this.lblGiaThueNgay.TabIndex = 14;
            this.lblGiaThueNgay.Text = "Giá thuê/ngày:";
            // 
            // txtSoNgayThue
            // 
            this.txtSoNgayThue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoNgayThue.Location = new System.Drawing.Point(173, 119);
            this.txtSoNgayThue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSoNgayThue.Name = "txtSoNgayThue";
            this.txtSoNgayThue.ReadOnly = true;
            this.txtSoNgayThue.Size = new System.Drawing.Size(372, 30);
            this.txtSoNgayThue.TabIndex = 13;
            // 
            // lblSoNgayThue
            // 
            this.lblSoNgayThue.AutoSize = true;
            this.lblSoNgayThue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSoNgayThue.Location = new System.Drawing.Point(20, 123);
            this.lblSoNgayThue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoNgayThue.Name = "lblSoNgayThue";
            this.lblSoNgayThue.Size = new System.Drawing.Size(115, 23);
            this.lblSoNgayThue.TabIndex = 12;
            this.lblSoNgayThue.Text = "Số ngày thuê:";
            // 
            // dtpNgayKetThuc
            // 
            this.dtpNgayKetThuc.Enabled = false;
            this.dtpNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayKetThuc.Location = new System.Drawing.Point(720, 76);
            this.dtpNgayKetThuc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            this.dtpNgayKetThuc.Size = new System.Drawing.Size(372, 30);
            this.dtpNgayKetThuc.TabIndex = 11;
            // 
            // lblNgayKetThuc
            // 
            this.lblNgayKetThuc.AutoSize = true;
            this.lblNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgayKetThuc.Location = new System.Drawing.Point(573, 80);
            this.lblNgayKetThuc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgayKetThuc.Name = "lblNgayKetThuc";
            this.lblNgayKetThuc.Size = new System.Drawing.Size(121, 23);
            this.lblNgayKetThuc.TabIndex = 10;
            this.lblNgayKetThuc.Text = "Ngày kết thúc:";
            // 
            // dtpNgayBatDau
            // 
            this.dtpNgayBatDau.Enabled = false;
            this.dtpNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBatDau.Location = new System.Drawing.Point(173, 76);
            this.dtpNgayBatDau.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(372, 30);
            this.dtpNgayBatDau.TabIndex = 9;
            // 
            // lblNgayBatDau
            // 
            this.lblNgayBatDau.AutoSize = true;
            this.lblNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgayBatDau.Location = new System.Drawing.Point(20, 80);
            this.lblNgayBatDau.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgayBatDau.Name = "lblNgayBatDau";
            this.lblNgayBatDau.Size = new System.Drawing.Size(118, 23);
            this.lblNgayBatDau.TabIndex = 8;
            this.lblNgayBatDau.Text = "Ngày bắt đầu:";
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtGiaBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.txtGiaBan.Location = new System.Drawing.Point(720, 76);
            this.txtGiaBan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.ReadOnly = true;
            this.txtGiaBan.Size = new System.Drawing.Size(372, 30);
            this.txtGiaBan.TabIndex = 7;
            this.txtGiaBan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblGiaBan
            // 
            this.lblGiaBan.AutoSize = true;
            this.lblGiaBan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGiaBan.Location = new System.Drawing.Point(573, 80);
            this.lblGiaBan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGiaBan.Name = "lblGiaBan";
            this.lblGiaBan.Size = new System.Drawing.Size(73, 23);
            this.lblGiaBan.TabIndex = 6;
            this.lblGiaBan.Text = "Giá bán:";
            // 
            // dtpNgayBan
            // 
            this.dtpNgayBan.Enabled = false;
            this.dtpNgayBan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayBan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBan.Location = new System.Drawing.Point(173, 76);
            this.dtpNgayBan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpNgayBan.Name = "dtpNgayBan";
            this.dtpNgayBan.Size = new System.Drawing.Size(372, 30);
            this.dtpNgayBan.TabIndex = 5;
            // 
            // lblNgayBan
            // 
            this.lblNgayBan.AutoSize = true;
            this.lblNgayBan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgayBan.Location = new System.Drawing.Point(20, 80);
            this.lblNgayBan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgayBan.Name = "lblNgayBan";
            this.lblNgayBan.Size = new System.Drawing.Size(88, 23);
            this.lblNgayBan.TabIndex = 4;
            this.lblNgayBan.Text = "Ngày bán:";
            // 
            // txtBienSo
            // 
            this.txtBienSo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBienSo.Location = new System.Drawing.Point(720, 33);
            this.txtBienSo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBienSo.Name = "txtBienSo";
            this.txtBienSo.ReadOnly = true;
            this.txtBienSo.Size = new System.Drawing.Size(372, 30);
            this.txtBienSo.TabIndex = 3;
            // 
            // lblBienSo
            // 
            this.lblBienSo.AutoSize = true;
            this.lblBienSo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBienSo.Location = new System.Drawing.Point(573, 37);
            this.lblBienSo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBienSo.Name = "lblBienSo";
            this.lblBienSo.Size = new System.Drawing.Size(69, 23);
            this.lblBienSo.TabIndex = 2;
            this.lblBienSo.Text = "Biển số:";
            // 
            // txtXe
            // 
            this.txtXe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtXe.Location = new System.Drawing.Point(173, 33);
            this.txtXe.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtXe.Name = "txtXe";
            this.txtXe.ReadOnly = true;
            this.txtXe.Size = new System.Drawing.Size(372, 30);
            this.txtXe.TabIndex = 1;
            // 
            // lblXe
            // 
            this.lblXe.AutoSize = true;
            this.lblXe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblXe.Location = new System.Drawing.Point(20, 37);
            this.lblXe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXe.Name = "lblXe";
            this.lblXe.Size = new System.Drawing.Size(33, 23);
            this.lblXe.TabIndex = 0;
            this.lblXe.Text = "Xe:";
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
            this.grpKhachHang.Location = new System.Drawing.Point(27, 74);
            this.grpKhachHang.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpKhachHang.Name = "grpKhachHang";
            this.grpKhachHang.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpKhachHang.Size = new System.Drawing.Size(1120, 172);
            this.grpKhachHang.TabIndex = 2;
            this.grpKhachHang.TabStop = false;
            this.grpKhachHang.Text = "Thông Tin Khách Hàng";
            // 
            // txtDiaChiKhachHang
            // 
            this.txtDiaChiKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiaChiKhachHang.Location = new System.Drawing.Point(173, 119);
            this.txtDiaChiKhachHang.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDiaChiKhachHang.Name = "txtDiaChiKhachHang";
            this.txtDiaChiKhachHang.ReadOnly = true;
            this.txtDiaChiKhachHang.Size = new System.Drawing.Size(919, 30);
            this.txtDiaChiKhachHang.TabIndex = 7;
            // 
            // lblDiaChiKhachHang
            // 
            this.lblDiaChiKhachHang.AutoSize = true;
            this.lblDiaChiKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiaChiKhachHang.Location = new System.Drawing.Point(20, 123);
            this.lblDiaChiKhachHang.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiaChiKhachHang.Name = "lblDiaChiKhachHang";
            this.lblDiaChiKhachHang.Size = new System.Drawing.Size(66, 23);
            this.lblDiaChiKhachHang.TabIndex = 6;
            this.lblDiaChiKhachHang.Text = "Địa chỉ:";
            // 
            // txtEmailKhachHang
            // 
            this.txtEmailKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmailKhachHang.Location = new System.Drawing.Point(173, 76);
            this.txtEmailKhachHang.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEmailKhachHang.Name = "txtEmailKhachHang";
            this.txtEmailKhachHang.ReadOnly = true;
            this.txtEmailKhachHang.Size = new System.Drawing.Size(919, 30);
            this.txtEmailKhachHang.TabIndex = 5;
            // 
            // lblEmailKhachHang
            // 
            this.lblEmailKhachHang.AutoSize = true;
            this.lblEmailKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmailKhachHang.Location = new System.Drawing.Point(20, 80);
            this.lblEmailKhachHang.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmailKhachHang.Name = "lblEmailKhachHang";
            this.lblEmailKhachHang.Size = new System.Drawing.Size(55, 23);
            this.lblEmailKhachHang.TabIndex = 4;
            this.lblEmailKhachHang.Text = "Email:";
            // 
            // txtSdtKhachHang
            // 
            this.txtSdtKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSdtKhachHang.Location = new System.Drawing.Point(720, 33);
            this.txtSdtKhachHang.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSdtKhachHang.Name = "txtSdtKhachHang";
            this.txtSdtKhachHang.ReadOnly = true;
            this.txtSdtKhachHang.Size = new System.Drawing.Size(372, 30);
            this.txtSdtKhachHang.TabIndex = 3;
            // 
            // lblSdtKhachHang
            // 
            this.lblSdtKhachHang.AutoSize = true;
            this.lblSdtKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSdtKhachHang.Location = new System.Drawing.Point(573, 37);
            this.lblSdtKhachHang.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSdtKhachHang.Name = "lblSdtKhachHang";
            this.lblSdtKhachHang.Size = new System.Drawing.Size(44, 23);
            this.lblSdtKhachHang.TabIndex = 2;
            this.lblSdtKhachHang.Text = "SĐT:";
            // 
            // txtKhachHang
            // 
            this.txtKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtKhachHang.Location = new System.Drawing.Point(173, 33);
            this.txtKhachHang.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.ReadOnly = true;
            this.txtKhachHang.Size = new System.Drawing.Size(372, 30);
            this.txtKhachHang.TabIndex = 1;
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblKhachHang.Location = new System.Drawing.Point(20, 37);
            this.lblKhachHang.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(88, 23);
            this.lblKhachHang.TabIndex = 0;
            this.lblKhachHang.Text = "Họ và tên:";
            // 
            // txtMaGiaoDich
            // 
            this.txtMaGiaoDich.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaGiaoDich.Location = new System.Drawing.Point(200, 21);
            this.txtMaGiaoDich.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMaGiaoDich.Name = "txtMaGiaoDich";
            this.txtMaGiaoDich.ReadOnly = true;
            this.txtMaGiaoDich.Size = new System.Drawing.Size(265, 30);
            this.txtMaGiaoDich.TabIndex = 1;
            // 
            // lblMaGiaoDich
            // 
            this.lblMaGiaoDich.AutoSize = true;
            this.lblMaGiaoDich.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMaGiaoDich.Location = new System.Drawing.Point(27, 25);
            this.lblMaGiaoDich.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaGiaoDich.Name = "lblMaGiaoDich";
            this.lblMaGiaoDich.Size = new System.Drawing.Size(122, 23);
            this.lblMaGiaoDich.TabIndex = 0;
            this.lblMaGiaoDich.Text = "Mã Giao Dịch:";
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.White;
            this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Controls.Add(this.btnReject);
            this.panelBottom.Controls.Add(this.btnApprove);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 800);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1200, 86);
            this.panelBottom.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(972, 18);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(200, 49);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "🚪 Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnReject
            // 
            this.btnReject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnReject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReject.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnReject.ForeColor = System.Drawing.Color.White;
            this.btnReject.Location = new System.Drawing.Point(240, 18);
            this.btnReject.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(200, 49);
            this.btnReject.TabIndex = 1;
            this.btnReject.Text = "❌ Từ Chối";
            this.btnReject.UseVisualStyleBackColor = false;
            this.btnReject.Click += new System.EventHandler(this.BtnReject_Click);
            // 
            // btnApprove
            // 
            this.btnApprove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnApprove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApprove.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnApprove.ForeColor = System.Drawing.Color.White;
            this.btnApprove.Location = new System.Drawing.Point(27, 18);
            this.btnApprove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(200, 49);
            this.btnApprove.TabIndex = 0;
            this.btnApprove.Text = "✅ Duyệt";
            this.btnApprove.UseVisualStyleBackColor = false;
            this.btnApprove.Click += new System.EventHandler(this.BtnApprove_Click);
            // 
            // FormDuyetDonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 886);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDuyetDonHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi Tiết Đơn Hàng";
            this.Load += new System.EventHandler(this.FormDuyetDonHang_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.grpDuyet.ResumeLayout(false);
            this.grpDuyet.PerformLayout();
            this.grpThanhToan.ResumeLayout(false);
            this.grpThanhToan.PerformLayout();
            this.grpXe.ResumeLayout(false);
            this.grpXe.PerformLayout();
            this.grpKhachHang.ResumeLayout(false);
            this.grpKhachHang.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}