namespace UI.FormUI
{
    partial class ViewDuyetDonThue
    {
        private System.ComponentModel.IContainer components = null;

        // Khai báo controls
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlChiTiet;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblTongSoDon;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataGridView dgvDanhSachDonChoDuyet;
        private System.Windows.Forms.Button btnDuyetDon;
        private System.Windows.Forms.Button btnTuChoiDon;
        private System.Windows.Forms.Button btnXemChiTiet;
        private System.Windows.Forms.TextBox txtMaGiaoDich;
        private System.Windows.Forms.TextBox txtTenKhachHang;
        private System.Windows.Forms.TextBox txtSoDienThoai;
        private System.Windows.Forms.TextBox txtTenXe;
        private System.Windows.Forms.TextBox txtBienSoXe;
        private System.Windows.Forms.TextBox txtNgayBatDau;
        private System.Windows.Forms.TextBox txtNgayKetThuc;
        private System.Windows.Forms.TextBox txtSoNgayThue;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblCanhBao;
        private System.Windows.Forms.Label lblMaGiaoDich;
        private System.Windows.Forms.Label lblTenKhachHang;
        private System.Windows.Forms.Label lblSoDienThoai;
        private System.Windows.Forms.Label lblTenXe;
        private System.Windows.Forms.Label lblBienSoXe;
        private System.Windows.Forms.Label lblThoiGian;
        private System.Windows.Forms.Label lblSoNgayThue;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblGhiChu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblTongSoDon = new System.Windows.Forms.Label();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dgvDanhSachDonChoDuyet = new System.Windows.Forms.DataGridView();
            this.pnlChiTiet = new System.Windows.Forms.Panel();
            this.btnTuChoiDon = new System.Windows.Forms.Button();
            this.btnDuyetDon = new System.Windows.Forms.Button();
            this.btnXemChiTiet = new System.Windows.Forms.Button();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblCanhBao = new System.Windows.Forms.Label();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.txtSoNgayThue = new System.Windows.Forms.TextBox();
            this.lblSoNgayThue = new System.Windows.Forms.Label();
            this.txtNgayKetThuc = new System.Windows.Forms.TextBox();
            this.txtNgayBatDau = new System.Windows.Forms.TextBox();
            this.lblThoiGian = new System.Windows.Forms.Label();
            this.txtBienSoXe = new System.Windows.Forms.TextBox();
            this.lblBienSoXe = new System.Windows.Forms.Label();
            this.txtTenXe = new System.Windows.Forms.TextBox();
            this.lblTenXe = new System.Windows.Forms.Label();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.txtTenKhachHang = new System.Windows.Forms.TextBox();
            this.lblTenKhachHang = new System.Windows.Forms.Label();
            this.txtMaGiaoDich = new System.Windows.Forms.TextBox();
            this.lblMaGiaoDich = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachDonChoDuyet)).BeginInit();
            this.pnlChiTiet.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.pnlTop.Controls.Add(this.btnLamMoi);
            this.pnlTop.Controls.Add(this.lblTongSoDon);
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 80);
            this.pnlTop.TabIndex = 0;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.BackColor = System.Drawing.Color.White;
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnLamMoi.Location = new System.Drawing.Point(1050, 20);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(130, 40);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.BtnLamMoi_Click);
            // 
            // lblTongSoDon
            // 
            this.lblTongSoDon.AutoSize = true;
            this.lblTongSoDon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongSoDon.ForeColor = System.Drawing.Color.White;
            this.lblTongSoDon.Location = new System.Drawing.Point(22, 52);
            this.lblTongSoDon.Name = "lblTongSoDon";
            this.lblTongSoDon.Size = new System.Drawing.Size(141, 19);
            this.lblTongSoDon.TabIndex = 1;
            this.lblTongSoDon.Text = "Tổng: 0 đơn chờ duyệt";
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 15);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(273, 32);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "DUYỆT ĐƠN THUÊ XE";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dgvDanhSachDonChoDuyet);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 80);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMain.Size = new System.Drawing.Size(800, 620);
            this.pnlMain.TabIndex = 1;
            // 
            // dgvDanhSachDonChoDuyet
            // 
            this.dgvDanhSachDonChoDuyet.AllowUserToAddRows = false;
            this.dgvDanhSachDonChoDuyet.AllowUserToDeleteRows = false;
            this.dgvDanhSachDonChoDuyet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSachDonChoDuyet.BackgroundColor = System.Drawing.Color.White;
            this.dgvDanhSachDonChoDuyet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDanhSachDonChoDuyet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachDonChoDuyet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSachDonChoDuyet.Location = new System.Drawing.Point(10, 10);
            this.dgvDanhSachDonChoDuyet.MultiSelect = false;
            this.dgvDanhSachDonChoDuyet.Name = "dgvDanhSachDonChoDuyet";
            this.dgvDanhSachDonChoDuyet.ReadOnly = true;
            this.dgvDanhSachDonChoDuyet.RowHeadersVisible = false;
            this.dgvDanhSachDonChoDuyet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachDonChoDuyet.Size = new System.Drawing.Size(780, 600);
            this.dgvDanhSachDonChoDuyet.TabIndex = 0;
            this.dgvDanhSachDonChoDuyet.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDanhSachDonChoDuyet_CellDoubleClick);
            this.dgvDanhSachDonChoDuyet.SelectionChanged += new System.EventHandler(this.DgvDanhSachDonChoDuyet_SelectionChanged);
            // 
            // pnlChiTiet
            // 
            this.pnlChiTiet.AutoScroll = true;
            this.pnlChiTiet.BackColor = System.Drawing.Color.White;
            this.pnlChiTiet.Controls.Add(this.btnTuChoiDon);
            this.pnlChiTiet.Controls.Add(this.btnDuyetDon);
            this.pnlChiTiet.Controls.Add(this.btnXemChiTiet);
            this.pnlChiTiet.Controls.Add(this.txtGhiChu);
            this.pnlChiTiet.Controls.Add(this.lblGhiChu);
            this.pnlChiTiet.Controls.Add(this.lblCanhBao);
            this.pnlChiTiet.Controls.Add(this.txtTongTien);
            this.pnlChiTiet.Controls.Add(this.lblTongTien);
            this.pnlChiTiet.Controls.Add(this.txtSoNgayThue);
            this.pnlChiTiet.Controls.Add(this.lblSoNgayThue);
            this.pnlChiTiet.Controls.Add(this.txtNgayKetThuc);
            this.pnlChiTiet.Controls.Add(this.txtNgayBatDau);
            this.pnlChiTiet.Controls.Add(this.lblThoiGian);
            this.pnlChiTiet.Controls.Add(this.txtBienSoXe);
            this.pnlChiTiet.Controls.Add(this.lblBienSoXe);
            this.pnlChiTiet.Controls.Add(this.txtTenXe);
            this.pnlChiTiet.Controls.Add(this.lblTenXe);
            this.pnlChiTiet.Controls.Add(this.txtSoDienThoai);
            this.pnlChiTiet.Controls.Add(this.lblSoDienThoai);
            this.pnlChiTiet.Controls.Add(this.txtTenKhachHang);
            this.pnlChiTiet.Controls.Add(this.lblTenKhachHang);
            this.pnlChiTiet.Controls.Add(this.txtMaGiaoDich);
            this.pnlChiTiet.Controls.Add(this.lblMaGiaoDich);
            this.pnlChiTiet.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlChiTiet.Location = new System.Drawing.Point(800, 80);
            this.pnlChiTiet.Name = "pnlChiTiet";
            this.pnlChiTiet.Padding = new System.Windows.Forms.Padding(20);
            this.pnlChiTiet.Size = new System.Drawing.Size(400, 620);
            this.pnlChiTiet.TabIndex = 2;
            this.pnlChiTiet.Visible = false; // Ẩn panel chi tiết bên phải
            // 
            // btnTuChoiDon
            // 
            this.btnTuChoiDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnTuChoiDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTuChoiDon.Enabled = false;
            this.btnTuChoiDon.FlatAppearance.BorderSize = 0;
            this.btnTuChoiDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTuChoiDon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnTuChoiDon.ForeColor = System.Drawing.Color.White;
            this.btnTuChoiDon.Location = new System.Drawing.Point(23, 555);
            this.btnTuChoiDon.Name = "btnTuChoiDon";
            this.btnTuChoiDon.Size = new System.Drawing.Size(340, 45);
            this.btnTuChoiDon.TabIndex = 22;
            this.btnTuChoiDon.Text = "TỪ CHỐI";
            this.btnTuChoiDon.UseVisualStyleBackColor = false;
            this.btnTuChoiDon.Click += new System.EventHandler(this.BtnTuChoiDon_Click);
            // 
            // btnDuyetDon
            // 
            this.btnDuyetDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnDuyetDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDuyetDon.Enabled = false;
            this.btnDuyetDon.FlatAppearance.BorderSize = 0;
            this.btnDuyetDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDuyetDon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDuyetDon.ForeColor = System.Drawing.Color.White;
            this.btnDuyetDon.Location = new System.Drawing.Point(23, 495);
            this.btnDuyetDon.Name = "btnDuyetDon";
            this.btnDuyetDon.Size = new System.Drawing.Size(340, 45);
            this.btnDuyetDon.TabIndex = 21;
            this.btnDuyetDon.Text = "DUYỆT ĐƠN";
            this.btnDuyetDon.UseVisualStyleBackColor = false;
            this.btnDuyetDon.Click += new System.EventHandler(this.BtnDuyetDon_Click);
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnXemChiTiet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXemChiTiet.Enabled = false;
            this.btnXemChiTiet.FlatAppearance.BorderSize = 0;
            this.btnXemChiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemChiTiet.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXemChiTiet.ForeColor = System.Drawing.Color.White;
            this.btnXemChiTiet.Location = new System.Drawing.Point(23, 435);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(340, 45);
            this.btnXemChiTiet.TabIndex = 20;
            this.btnXemChiTiet.Text = "XEM CHI TIẾT";
            this.btnXemChiTiet.UseVisualStyleBackColor = false;
            this.btnXemChiTiet.Click += new System.EventHandler(this.BtnXemChiTiet_Click);
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(23, 395);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(340, 30);
            this.txtGhiChu.TabIndex = 19;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblGhiChu.Location = new System.Drawing.Point(23, 370);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(340, 20);
            this.lblGhiChu.TabIndex = 18;
            this.lblGhiChu.Text = "Ghi chú / Lý do từ chối:";
            // 
            // lblCanhBao
            // 
            this.lblCanhBao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCanhBao.Location = new System.Drawing.Point(23, 340);
            this.lblCanhBao.Name = "lblCanhBao";
            this.lblCanhBao.Size = new System.Drawing.Size(340, 25);
            this.lblCanhBao.TabIndex = 17;
            this.lblCanhBao.Visible = false;
            // 
            // txtTongTien
            // 
            this.txtTongTien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtTongTien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTongTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.txtTongTien.Location = new System.Drawing.Point(23, 305);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.Size = new System.Drawing.Size(340, 29);
            this.txtTongTien.TabIndex = 16;
            // 
            // lblTongTien
            // 
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.Location = new System.Drawing.Point(23, 280);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(340, 20);
            this.lblTongTien.TabIndex = 15;
            this.lblTongTien.Text = "Tổng tiền:";
            // 
            // txtSoNgayThue
            // 
            this.txtSoNgayThue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtSoNgayThue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoNgayThue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoNgayThue.Location = new System.Drawing.Point(23, 245);
            this.txtSoNgayThue.Name = "txtSoNgayThue";
            this.txtSoNgayThue.ReadOnly = true;
            this.txtSoNgayThue.Size = new System.Drawing.Size(165, 25);
            this.txtSoNgayThue.TabIndex = 14;
            // 
            // lblSoNgayThue
            // 
            this.lblSoNgayThue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoNgayThue.Location = new System.Drawing.Point(23, 220);
            this.lblSoNgayThue.Name = "lblSoNgayThue";
            this.lblSoNgayThue.Size = new System.Drawing.Size(340, 20);
            this.lblSoNgayThue.TabIndex = 13;
            this.lblSoNgayThue.Text = "Số ngày thuê:";
            // 
            // txtNgayKetThuc
            // 
            this.txtNgayKetThuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtNgayKetThuc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNgayKetThuc.Location = new System.Drawing.Point(198, 185);
            this.txtNgayKetThuc.Name = "txtNgayKetThuc";
            this.txtNgayKetThuc.ReadOnly = true;
            this.txtNgayKetThuc.Size = new System.Drawing.Size(165, 25);
            this.txtNgayKetThuc.TabIndex = 12;
            // 
            // txtNgayBatDau
            // 
            this.txtNgayBatDau.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtNgayBatDau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNgayBatDau.Location = new System.Drawing.Point(23, 185);
            this.txtNgayBatDau.Name = "txtNgayBatDau";
            this.txtNgayBatDau.ReadOnly = true;
            this.txtNgayBatDau.Size = new System.Drawing.Size(165, 25);
            this.txtNgayBatDau.TabIndex = 11;
            // 
            // lblThoiGian
            // 
            this.lblThoiGian.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblThoiGian.Location = new System.Drawing.Point(23, 160);
            this.lblThoiGian.Name = "lblThoiGian";
            this.lblThoiGian.Size = new System.Drawing.Size(340, 20);
            this.lblThoiGian.TabIndex = 10;
            this.lblThoiGian.Text = "Từ ngày - Đến ngày:";
            // 
            // txtBienSoXe
            // 
            this.txtBienSoXe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtBienSoXe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBienSoXe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBienSoXe.Location = new System.Drawing.Point(198, 125);
            this.txtBienSoXe.Name = "txtBienSoXe";
            this.txtBienSoXe.ReadOnly = true;
            this.txtBienSoXe.Size = new System.Drawing.Size(165, 25);
            this.txtBienSoXe.TabIndex = 9;
            // 
            // lblBienSoXe
            // 
            this.lblBienSoXe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBienSoXe.Location = new System.Drawing.Point(198, 100);
            this.lblBienSoXe.Name = "lblBienSoXe";
            this.lblBienSoXe.Size = new System.Drawing.Size(165, 20);
            this.lblBienSoXe.TabIndex = 8;
            this.lblBienSoXe.Text = "Biển số:";
            // 
            // txtTenXe
            // 
            this.txtTenXe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtTenXe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenXe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenXe.Location = new System.Drawing.Point(23, 125);
            this.txtTenXe.Name = "txtTenXe";
            this.txtTenXe.ReadOnly = true;
            this.txtTenXe.Size = new System.Drawing.Size(165, 25);
            this.txtTenXe.TabIndex = 7;
            // 
            // lblTenXe
            // 
            this.lblTenXe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTenXe.Location = new System.Drawing.Point(23, 100);
            this.lblTenXe.Name = "lblTenXe";
            this.lblTenXe.Size = new System.Drawing.Size(165, 20);
            this.lblTenXe.TabIndex = 6;
            this.lblTenXe.Text = "Xe:";
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtSoDienThoai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoDienThoai.Location = new System.Drawing.Point(198, 65);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.ReadOnly = true;
            this.txtSoDienThoai.Size = new System.Drawing.Size(165, 25);
            this.txtSoDienThoai.TabIndex = 5;
            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoDienThoai.Location = new System.Drawing.Point(198, 40);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new System.Drawing.Size(165, 20);
            this.lblSoDienThoai.TabIndex = 4;
            this.lblSoDienThoai.Text = "SĐT:";
            // 
            // txtTenKhachHang
            // 
            this.txtTenKhachHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtTenKhachHang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenKhachHang.Location = new System.Drawing.Point(23, 65);
            this.txtTenKhachHang.Name = "txtTenKhachHang";
            this.txtTenKhachHang.ReadOnly = true;
            this.txtTenKhachHang.Size = new System.Drawing.Size(165, 25);
            this.txtTenKhachHang.TabIndex = 3;
            // 
            // lblTenKhachHang
            // 
            this.lblTenKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTenKhachHang.Location = new System.Drawing.Point(23, 40);
            this.lblTenKhachHang.Name = "lblTenKhachHang";
            this.lblTenKhachHang.Size = new System.Drawing.Size(165, 20);
            this.lblTenKhachHang.TabIndex = 2;
            this.lblTenKhachHang.Text = "Khách hàng:";
            // 
            // txtMaGiaoDich
            // 
            this.txtMaGiaoDich.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtMaGiaoDich.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaGiaoDich.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaGiaoDich.Location = new System.Drawing.Point(23, 5);
            this.txtMaGiaoDich.Name = "txtMaGiaoDich";
            this.txtMaGiaoDich.ReadOnly = true;
            this.txtMaGiaoDich.Size = new System.Drawing.Size(340, 25);
            this.txtMaGiaoDich.TabIndex = 1;
            // 
            // lblMaGiaoDich
            // 
            this.lblMaGiaoDich.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaGiaoDich.Location = new System.Drawing.Point(23, -20);
            this.lblMaGiaoDich.Name = "lblMaGiaoDich";
            this.lblMaGiaoDich.Size = new System.Drawing.Size(340, 20);
            this.lblMaGiaoDich.TabIndex = 0;
            this.lblMaGiaoDich.Text = "Mã GD:";
            // 
            // ViewDuyetDonThue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlChiTiet);
            this.Controls.Add(this.pnlTop);
            this.Name = "ViewDuyetDonThue";
            this.Size = new System.Drawing.Size(1200, 700);
            this.Load += new System.EventHandler(this.ViewDuyetDonThue_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachDonChoDuyet)).EndInit();
            this.pnlChiTiet.ResumeLayout(false);
            this.pnlChiTiet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}