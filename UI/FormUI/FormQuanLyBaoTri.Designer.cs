namespace UI
{
    partial class frmQuanLyBaoTri
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnLamMoi;

        private System.Windows.Forms.GroupBox grpThongTinBaoTri;
        private System.Windows.Forms.Label lblXe;
        private System.Windows.Forms.ComboBox cboXe;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;

        private System.Windows.Forms.GroupBox grpThemPhuTung;
        private System.Windows.Forms.Label lblPhuTung;
        private System.Windows.Forms.ComboBox cboPhuTung;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Label lblGhiChuPhuTung;
        private System.Windows.Forms.TextBox txtGhiChuPhuTung;
        private System.Windows.Forms.Button btnThemPhuTung;

        private System.Windows.Forms.GroupBox grpChiTiet;
        private System.Windows.Forms.DataGridView dgvChiTietBaoTri;
        private System.Windows.Forms.Label lblTongTienText;
        private System.Windows.Forms.Label lblTongTien;

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnXoa;

        private System.Windows.Forms.GroupBox grpDanhSach;
        private System.Windows.Forms.DataGridView dgvDanhSachBaoTri;

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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnLamMoi = new System.Windows.Forms.Button();

            this.grpThongTinBaoTri = new System.Windows.Forms.GroupBox();
            this.lblXe = new System.Windows.Forms.Label();
            this.cboXe = new System.Windows.Forms.ComboBox();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();

            this.grpThemPhuTung = new System.Windows.Forms.GroupBox();
            this.lblPhuTung = new System.Windows.Forms.Label();
            this.cboPhuTung = new System.Windows.Forms.ComboBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblGhiChuPhuTung = new System.Windows.Forms.Label();
            this.txtGhiChuPhuTung = new System.Windows.Forms.TextBox();
            this.btnThemPhuTung = new System.Windows.Forms.Button();

            this.grpChiTiet = new System.Windows.Forms.GroupBox();
            this.dgvChiTietBaoTri = new System.Windows.Forms.DataGridView();
            this.lblTongTienText = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();

            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();

            this.grpDanhSach = new System.Windows.Forms.GroupBox();
            this.dgvDanhSachBaoTri = new System.Windows.Forms.DataGridView();

            this.pnlTop.SuspendLayout();
            this.grpThongTinBaoTri.SuspendLayout();
            this.grpThemPhuTung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            this.grpChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietBaoTri)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.grpDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachBaoTri)).BeginInit();
            this.SuspendLayout();

            // pnlTop
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Controls.Add(this.txtTimKiem);
            this.pnlTop.Controls.Add(this.btnLamMoi);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1400, 70);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 32);
            this.lblTitle.Text = "QUẢN LÝ BẢO TRÌ XE";

            // txtTimKiem
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTimKiem.Location = new System.Drawing.Point(900, 20);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(300, 27);
           
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);

            // btnLamMoi
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(1220, 15);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(150, 40);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // grpThongTinBaoTri
            this.grpThongTinBaoTri.Controls.Add(this.lblXe);
            this.grpThongTinBaoTri.Controls.Add(this.cboXe);
            this.grpThongTinBaoTri.Controls.Add(this.lblNhanVien);
            this.grpThongTinBaoTri.Controls.Add(this.cboNhanVien);
            this.grpThongTinBaoTri.Controls.Add(this.lblGhiChu);
            this.grpThongTinBaoTri.Controls.Add(this.txtGhiChu);
            this.grpThongTinBaoTri.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpThongTinBaoTri.Location = new System.Drawing.Point(20, 85);
            this.grpThongTinBaoTri.Name = "grpThongTinBaoTri";
            this.grpThongTinBaoTri.Size = new System.Drawing.Size(680, 180);
            this.grpThongTinBaoTri.Text = "Thông tin bảo trì";

            // lblXe
            this.lblXe.AutoSize = true;
            this.lblXe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblXe.Location = new System.Drawing.Point(20, 35);
            this.lblXe.Name = "lblXe";
            this.lblXe.Text = "Xe:";

            // cboXe
            this.cboXe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboXe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboXe.FormattingEnabled = true;
            this.cboXe.Location = new System.Drawing.Point(150, 32);
            this.cboXe.Name = "cboXe";
            this.cboXe.Size = new System.Drawing.Size(500, 25);

            // lblNhanVien
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNhanVien.Location = new System.Drawing.Point(20, 75);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Text = "Nhân viên kỹ thuật:";

            // cboNhanVien
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNhanVien.FormattingEnabled = true;
            this.cboNhanVien.Location = new System.Drawing.Point(150, 72);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(500, 25);

            // lblGhiChu
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGhiChu.Location = new System.Drawing.Point(20, 115);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Text = "Ghi chú:";

            // txtGhiChu
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(150, 112);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(500, 50);

            // grpThemPhuTung
            this.grpThemPhuTung.Controls.Add(this.lblPhuTung);
            this.grpThemPhuTung.Controls.Add(this.cboPhuTung);
            this.grpThemPhuTung.Controls.Add(this.lblSoLuong);
            this.grpThemPhuTung.Controls.Add(this.nudSoLuong);
            this.grpThemPhuTung.Controls.Add(this.lblGhiChuPhuTung);
            this.grpThemPhuTung.Controls.Add(this.txtGhiChuPhuTung);
            this.grpThemPhuTung.Controls.Add(this.btnThemPhuTung);
            this.grpThemPhuTung.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpThemPhuTung.Location = new System.Drawing.Point(720, 85);
            this.grpThemPhuTung.Name = "grpThemPhuTung";
            this.grpThemPhuTung.Size = new System.Drawing.Size(660, 180);
            this.grpThemPhuTung.Text = "Thêm phụ tùng";

            // lblPhuTung
            this.lblPhuTung.AutoSize = true;
            this.lblPhuTung.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPhuTung.Location = new System.Drawing.Point(20, 35);
            this.lblPhuTung.Name = "lblPhuTung";
            this.lblPhuTung.Text = "Phụ tùng:";

            // cboPhuTung
            this.cboPhuTung.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhuTung.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboPhuTung.FormattingEnabled = true;
            this.cboPhuTung.Location = new System.Drawing.Point(120, 32);
            this.cboPhuTung.Name = "cboPhuTung";
            this.cboPhuTung.Size = new System.Drawing.Size(350, 25);

            // lblSoLuong
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSoLuong.Location = new System.Drawing.Point(490, 35);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Text = "SL:";

            // nudSoLuong
            this.nudSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudSoLuong.Location = new System.Drawing.Point(530, 32);
            this.nudSoLuong.Minimum = 1;
            this.nudSoLuong.Maximum = 1000;
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(100, 25);
            this.nudSoLuong.Value = 1;

            // lblGhiChuPhuTung
            this.lblGhiChuPhuTung.AutoSize = true;
            this.lblGhiChuPhuTung.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGhiChuPhuTung.Location = new System.Drawing.Point(20, 75);
            this.lblGhiChuPhuTung.Name = "lblGhiChuPhuTung";
            this.lblGhiChuPhuTung.Text = "Ghi chú:";

            // txtGhiChuPhuTung
            this.txtGhiChuPhuTung.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChuPhuTung.Location = new System.Drawing.Point(120, 72);
            this.txtGhiChuPhuTung.Multiline = true;
            this.txtGhiChuPhuTung.Name = "txtGhiChuPhuTung";
            this.txtGhiChuPhuTung.Size = new System.Drawing.Size(510, 50);

            // btnThemPhuTung
            this.btnThemPhuTung.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThemPhuTung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemPhuTung.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemPhuTung.ForeColor = System.Drawing.Color.White;
            this.btnThemPhuTung.Location = new System.Drawing.Point(480, 130);
            this.btnThemPhuTung.Name = "btnThemPhuTung";
            this.btnThemPhuTung.Size = new System.Drawing.Size(150, 35);
            this.btnThemPhuTung.Text = "Thêm";
            this.btnThemPhuTung.Click += new System.EventHandler(this.btnThemPhuTung_Click);

            // grpChiTiet
            this.grpChiTiet.Controls.Add(this.dgvChiTietBaoTri);
            this.grpChiTiet.Controls.Add(this.lblTongTienText);
            this.grpChiTiet.Controls.Add(this.lblTongTien);
            this.grpChiTiet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpChiTiet.Location = new System.Drawing.Point(20, 280);
            this.grpChiTiet.Name = "grpChiTiet";
            this.grpChiTiet.Size = new System.Drawing.Size(1360, 250);
            this.grpChiTiet.Text = "Chi tiết phụ tùng";

            // dgvChiTietBaoTri
            this.dgvChiTietBaoTri.AllowUserToAddRows = false;
            this.dgvChiTietBaoTri.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTietBaoTri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietBaoTri.Location = new System.Drawing.Point(20, 30);
            this.dgvChiTietBaoTri.Name = "dgvChiTietBaoTri";
            this.dgvChiTietBaoTri.Size = new System.Drawing.Size(1320, 170);
            this.dgvChiTietBaoTri.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTietBaoTri_CellContentClick);

            // lblTongTienText
            this.lblTongTienText.AutoSize = true;
            this.lblTongTienText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongTienText.Location = new System.Drawing.Point(1000, 213);
            this.lblTongTienText.Name = "lblTongTienText";
            this.lblTongTienText.Text = "TỔNG CHI PHÍ:";

            // lblTongTien
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.Red;
            this.lblTongTien.Location = new System.Drawing.Point(1150, 213);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Text = "0 VNĐ";

            // pnlButtons
            this.pnlButtons.Controls.Add(this.btnThem);
            this.pnlButtons.Controls.Add(this.btnXoa);
            this.pnlButtons.Location = new System.Drawing.Point(20, 545);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1360, 60);

            // btnThem
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(900, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(200, 45);
            this.btnThem.Text = "Tạo phiếu bảo trì";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            // btnXoa
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(1140, 10);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(200, 45);
            this.btnXoa.Text = "Xóa bảo trì";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            // grpDanhSach
            this.grpDanhSach.Controls.Add(this.dgvDanhSachBaoTri);
            this.grpDanhSach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpDanhSach.Location = new System.Drawing.Point(20, 620);
            this.grpDanhSach.Name = "grpDanhSach";
            this.grpDanhSach.Size = new System.Drawing.Size(1360, 250);
            this.grpDanhSach.Text = "Danh sách bảo trì";

            // dgvDanhSachBaoTri
            this.dgvDanhSachBaoTri.AllowUserToAddRows = false;
            this.dgvDanhSachBaoTri.AllowUserToDeleteRows = false;
            this.dgvDanhSachBaoTri.BackgroundColor = System.Drawing.Color.White;
            this.dgvDanhSachBaoTri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachBaoTri.Location = new System.Drawing.Point(20, 30);
            this.dgvDanhSachBaoTri.Name = "dgvDanhSachBaoTri";
            this.dgvDanhSachBaoTri.ReadOnly = true;
            this.dgvDanhSachBaoTri.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachBaoTri.Size = new System.Drawing.Size(1320, 200);
            this.dgvDanhSachBaoTri.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachBaoTri_CellClick);

            // frmQuanLyBaoTri
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1400, 890);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.grpThongTinBaoTri);
            this.Controls.Add(this.grpThemPhuTung);
            this.Controls.Add(this.grpChiTiet);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.grpDanhSach);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmQuanLyBaoTri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Bảo Trì Xe";
            this.Load += new System.EventHandler(this.frmQuanLyBaoTri_Load);

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpThongTinBaoTri.ResumeLayout(false);
            this.grpThongTinBaoTri.PerformLayout();
            this.grpThemPhuTung.ResumeLayout(false);
            this.grpThemPhuTung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            this.grpChiTiet.ResumeLayout(false);
            this.grpChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietBaoTri)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.grpDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachBaoTri)).EndInit();
            this.ResumeLayout(false);
        }
    }
}