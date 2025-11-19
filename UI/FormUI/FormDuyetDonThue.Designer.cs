namespace UI.FormUI
{
    partial class FormDuyetDonThue
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // ==================== KHAI BÁO CONTROLS ====================
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTongSo;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvDonChoDuyet;
        private System.Windows.Forms.Button btnDuyet;
        private System.Windows.Forms.Button btnTuChoi;
        private System.Windows.Forms.Button btnXemChiTiet;
        private System.Windows.Forms.TextBox txtMaGD;
        private System.Windows.Forms.TextBox txtKhachHang;
        private System.Windows.Forms.TextBox txtSdt;
        private System.Windows.Forms.TextBox txtXe;
        private System.Windows.Forms.TextBox txtBienSo;
        private System.Windows.Forms.TextBox txtNgayBatDau;
        private System.Windows.Forms.TextBox txtNgayKetThuc;
        private System.Windows.Forms.TextBox txtSoNgay;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTongSo = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.dgvDonChoDuyet = new System.Windows.Forms.DataGridView();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.btnTuChoi = new System.Windows.Forms.Button();
            this.btnDuyet = new System.Windows.Forms.Button();
            this.btnXemChiTiet = new System.Windows.Forms.Button();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSoNgay = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNgayKetThuc = new System.Windows.Forms.TextBox();
            this.txtNgayBatDau = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBienSo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtXe = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSdt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKhachHang = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaGD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonChoDuyet)).BeginInit();
            this.panelDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.lblTongSo);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 80);
            this.panelTop.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnRefresh.Location = new System.Drawing.Point(1050, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 40);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // lblTongSo
            // 
            this.lblTongSo.AutoSize = true;
            this.lblTongSo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongSo.ForeColor = System.Drawing.Color.White;
            this.lblTongSo.Location = new System.Drawing.Point(22, 52);
            this.lblTongSo.Name = "lblTongSo";
            this.lblTongSo.Size = new System.Drawing.Size(148, 19);
            this.lblTongSo.TabIndex = 1;
            this.lblTongSo.Text = "Tổng: 0 đơn chờ duyệt";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(259, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "DUYỆT ĐƠN THUÊ XE";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.dgvDonChoDuyet);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 80);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(10);
            this.panelMain.Size = new System.Drawing.Size(800, 898);
            this.panelMain.TabIndex = 1;
            // 
            // dgvDonChoDuyet
            // 
            this.dgvDonChoDuyet.AllowUserToAddRows = false;
            this.dgvDonChoDuyet.AllowUserToDeleteRows = false;
            this.dgvDonChoDuyet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDonChoDuyet.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonChoDuyet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDonChoDuyet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonChoDuyet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDonChoDuyet.Location = new System.Drawing.Point(10, 10);
            this.dgvDonChoDuyet.MultiSelect = false;
            this.dgvDonChoDuyet.Name = "dgvDonChoDuyet";
            this.dgvDonChoDuyet.ReadOnly = true;
            this.dgvDonChoDuyet.RowHeadersVisible = false;
            this.dgvDonChoDuyet.RowTemplate.Height = 35;
            this.dgvDonChoDuyet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDonChoDuyet.Size = new System.Drawing.Size(780, 878);
            this.dgvDonChoDuyet.TabIndex = 0;
            this.dgvDonChoDuyet.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDonChoDuyet_CellDoubleClick);
            this.dgvDonChoDuyet.SelectionChanged += new System.EventHandler(this.DgvDonChoDuyet_SelectionChanged);
            // 
            // panelDetail
            // 
            this.panelDetail.AutoScroll = true;
            this.panelDetail.BackColor = System.Drawing.Color.White;
            this.panelDetail.Controls.Add(this.btnTuChoi);
            this.panelDetail.Controls.Add(this.btnDuyet);
            this.panelDetail.Controls.Add(this.btnXemChiTiet);
            this.panelDetail.Controls.Add(this.txtGhiChu);
            this.panelDetail.Controls.Add(this.label9);
            this.panelDetail.Controls.Add(this.lblWarning);
            this.panelDetail.Controls.Add(this.txtTongTien);
            this.panelDetail.Controls.Add(this.label8);
            this.panelDetail.Controls.Add(this.txtSoNgay);
            this.panelDetail.Controls.Add(this.label7);
            this.panelDetail.Controls.Add(this.txtNgayKetThuc);
            this.panelDetail.Controls.Add(this.txtNgayBatDau);
            this.panelDetail.Controls.Add(this.label6);
            this.panelDetail.Controls.Add(this.txtBienSo);
            this.panelDetail.Controls.Add(this.label5);
            this.panelDetail.Controls.Add(this.txtXe);
            this.panelDetail.Controls.Add(this.label4);
            this.panelDetail.Controls.Add(this.txtSdt);
            this.panelDetail.Controls.Add(this.label3);
            this.panelDetail.Controls.Add(this.txtKhachHang);
            this.panelDetail.Controls.Add(this.label2);
            this.panelDetail.Controls.Add(this.txtMaGD);
            this.panelDetail.Controls.Add(this.label1);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelDetail.Location = new System.Drawing.Point(800, 80);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Padding = new System.Windows.Forms.Padding(20);
            this.panelDetail.Size = new System.Drawing.Size(400, 898);
            this.panelDetail.TabIndex = 2;
            // 
            // btnTuChoi
            // 
            this.btnTuChoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnTuChoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTuChoi.Enabled = false;
            this.btnTuChoi.FlatAppearance.BorderSize = 0;
            this.btnTuChoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTuChoi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnTuChoi.ForeColor = System.Drawing.Color.White;
            this.btnTuChoi.Location = new System.Drawing.Point(23, 795);
            this.btnTuChoi.Name = "btnTuChoi";
            this.btnTuChoi.Size = new System.Drawing.Size(340, 45);
            this.btnTuChoi.TabIndex = 22;
            this.btnTuChoi.Text = "TỪ CHỐI";
            this.btnTuChoi.UseVisualStyleBackColor = false;
            this.btnTuChoi.Click += new System.EventHandler(this.BtnTuChoi_Click);
            // 
            // btnDuyet
            // 
            this.btnDuyet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnDuyet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDuyet.Enabled = false;
            this.btnDuyet.FlatAppearance.BorderSize = 0;
            this.btnDuyet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDuyet.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDuyet.ForeColor = System.Drawing.Color.White;
            this.btnDuyet.Location = new System.Drawing.Point(23, 735);
            this.btnDuyet.Name = "btnDuyet";
            this.btnDuyet.Size = new System.Drawing.Size(340, 45);
            this.btnDuyet.TabIndex = 21;
            this.btnDuyet.Text = "DUYỆT ĐƠN";
            this.btnDuyet.UseVisualStyleBackColor = false;
            this.btnDuyet.Click += new System.EventHandler(this.BtnDuyet_Click);
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
            this.btnXemChiTiet.Location = new System.Drawing.Point(23, 675);
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
            this.txtGhiChu.Location = new System.Drawing.Point(23, 590);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(340, 70);
            this.txtGhiChu.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(23, 565);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(340, 20);
            this.label9.TabIndex = 18;
            this.label9.Text = "Ghi chú / Lý do từ chối:";
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblWarning.Location = new System.Drawing.Point(23, 520);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(340, 40);
            this.lblWarning.TabIndex = 17;
            this.lblWarning.Visible = false;
            // 
            // txtTongTien
            // 
            this.txtTongTien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtTongTien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTongTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.txtTongTien.Location = new System.Drawing.Point(23, 485);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.Size = new System.Drawing.Size(340, 29);
            this.txtTongTien.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(23, 460);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(340, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "Tổng tiền:";
            // 
            // txtSoNgay
            // 
            this.txtSoNgay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtSoNgay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoNgay.Location = new System.Drawing.Point(23, 420);
            this.txtSoNgay.Name = "txtSoNgay";
            this.txtSoNgay.ReadOnly = true;
            this.txtSoNgay.Size = new System.Drawing.Size(165, 25);
            this.txtSoNgay.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(23, 395);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(340, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Số ngày thuê:";
            // 
            // txtNgayKetThuc
            // 
            this.txtNgayKetThuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtNgayKetThuc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNgayKetThuc.Location = new System.Drawing.Point(198, 355);
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
            this.txtNgayBatDau.Location = new System.Drawing.Point(23, 355);
            this.txtNgayBatDau.Name = "txtNgayBatDau";
            this.txtNgayBatDau.ReadOnly = true;
            this.txtNgayBatDau.Size = new System.Drawing.Size(165, 25);
            this.txtNgayBatDau.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(23, 330);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(340, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Từ ngày - Đến ngày:";
            // 
            // txtBienSo
            // 
            this.txtBienSo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtBienSo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBienSo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBienSo.Location = new System.Drawing.Point(23, 290);
            this.txtBienSo.Name = "txtBienSo";
            this.txtBienSo.ReadOnly = true;
            this.txtBienSo.Size = new System.Drawing.Size(340, 25);
            this.txtBienSo.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(23, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(340, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Biển số:";
            // 
            // txtXe
            // 
            this.txtXe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtXe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtXe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtXe.Location = new System.Drawing.Point(23, 225);
            this.txtXe.Name = "txtXe";
            this.txtXe.ReadOnly = true;
            this.txtXe.Size = new System.Drawing.Size(340, 25);
            this.txtXe.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(23, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(340, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Xe:";
            // 
            // txtSdt
            // 
            this.txtSdt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtSdt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSdt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSdt.Location = new System.Drawing.Point(23, 160);
            this.txtSdt.Name = "txtSdt";
            this.txtSdt.ReadOnly = true;
            this.txtSdt.Size = new System.Drawing.Size(340, 25);
            this.txtSdt.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(23, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(340, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "SĐT:";
            // 
            // txtKhachHang
            // 
            this.txtKhachHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtKhachHang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtKhachHang.Location = new System.Drawing.Point(23, 95);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.ReadOnly = true;
            this.txtKhachHang.Size = new System.Drawing.Size(340, 25);
            this.txtKhachHang.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(23, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(340, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Khách hàng:";
            // 
            // txtMaGD
            // 
            this.txtMaGD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtMaGD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaGD.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaGD.Location = new System.Drawing.Point(23, 30);
            this.txtMaGD.Name = "txtMaGD";
            this.txtMaGD.ReadOnly = true;
            this.txtMaGD.Size = new System.Drawing.Size(340, 25);
            this.txtMaGD.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(23, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(340, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã GD:";
            // 
            // FormDuyetDonThue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 978);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.panelTop);
            this.Name = "FormDuyetDonThue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Duyệt Đơn Thuê Xe";
            this.Load += new System.EventHandler(this.FormDuyetDonThue_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonChoDuyet)).EndInit();
            this.panelDetail.ResumeLayout(false);
            this.panelDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}