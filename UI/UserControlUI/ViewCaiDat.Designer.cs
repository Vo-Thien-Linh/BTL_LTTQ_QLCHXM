namespace UI.UserControlUI
{
    partial class ViewCaiDat
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.grpNgonNgu = new System.Windows.Forms.GroupBox();
            this.rdoEnglish = new System.Windows.Forms.RadioButton();
            this.rdoTiengViet = new System.Windows.Forms.RadioButton();
            this.grpGiaoDien = new System.Windows.Forms.GroupBox();
            this.lblFontSize = new System.Windows.Forms.Label();
            this.trackBarFont = new System.Windows.Forms.TrackBar();
            this.chkHieuUng = new System.Windows.Forms.CheckBox();
            this.cboTheme = new System.Windows.Forms.ComboBox();
            this.lblTheme = new System.Windows.Forms.Label();
            this.grpThongBao = new System.Windows.Forms.GroupBox();
            this.nudSoNgay = new System.Windows.Forms.NumericUpDown();
            this.lblSoNgay = new System.Windows.Forms.Label();
            this.chkThueXe = new System.Windows.Forms.CheckBox();
            this.chkBaoHiem = new System.Windows.Forms.CheckBox();
            this.chkDangKy = new System.Windows.Forms.CheckBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnKhoiPhuc = new System.Windows.Forms.Button();
            this.grpNgonNgu.SuspendLayout();
            this.grpGiaoDien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFont)).BeginInit();
            this.grpThongBao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNgay)).BeginInit();
            this.SuspendLayout();
            // 
            // grpNgonNgu
            // 
            this.grpNgonNgu.Controls.Add(this.rdoEnglish);
            this.grpNgonNgu.Controls.Add(this.rdoTiengViet);
            this.grpNgonNgu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpNgonNgu.Location = new System.Drawing.Point(20, 20);
            this.grpNgonNgu.Name = "grpNgonNgu";
            this.grpNgonNgu.Size = new System.Drawing.Size(350, 100);
            this.grpNgonNgu.TabIndex = 0;
            this.grpNgonNgu.TabStop = false;
            this.grpNgonNgu.Text = "Ngôn ngữ / Language";
            // 
            // rdoEnglish
            // 
            this.rdoEnglish.AutoSize = true;
            this.rdoEnglish.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rdoEnglish.Location = new System.Drawing.Point(20, 60);
            this.rdoEnglish.Name = "rdoEnglish";
            this.rdoEnglish.Size = new System.Drawing.Size(64, 19);
            this.rdoEnglish.TabIndex = 1;
            this.rdoEnglish.Text = "English";
            this.rdoEnglish.UseVisualStyleBackColor = true;
            // 
            // rdoTiengViet
            // 
            this.rdoTiengViet.AutoSize = true;
            this.rdoTiengViet.Checked = true;
            this.rdoTiengViet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rdoTiengViet.Location = new System.Drawing.Point(20, 35);
            this.rdoTiengViet.Name = "rdoTiengViet";
            this.rdoTiengViet.Size = new System.Drawing.Size(79, 19);
            this.rdoTiengViet.TabIndex = 0;
            this.rdoTiengViet.TabStop = true;
            this.rdoTiengViet.Text = "Tiếng Việt";
            this.rdoTiengViet.UseVisualStyleBackColor = true;
            // 
            // grpGiaoDien
            // 
            this.grpGiaoDien.Controls.Add(this.lblFontSize);
            this.grpGiaoDien.Controls.Add(this.trackBarFont);
            this.grpGiaoDien.Controls.Add(this.chkHieuUng);
            this.grpGiaoDien.Controls.Add(this.cboTheme);
            this.grpGiaoDien.Controls.Add(this.lblTheme);
            this.grpGiaoDien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpGiaoDien.Location = new System.Drawing.Point(20, 140);
            this.grpGiaoDien.Name = "grpGiaoDien";
            this.grpGiaoDien.Size = new System.Drawing.Size(350, 200);
            this.grpGiaoDien.TabIndex = 1;
            this.grpGiaoDien.TabStop = false;
            this.grpGiaoDien.Text = "Giao diện";
            // 
            // lblFontSize
            // 
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFontSize.Location = new System.Drawing.Point(20, 125);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(118, 15);
            this.lblFontSize.TabIndex = 4;
            this.lblFontSize.Text = "Kích thước font: 10";
            // 
            // trackBarFont
            // 
            this.trackBarFont.Location = new System.Drawing.Point(20, 145);
            this.trackBarFont.Maximum = 16;
            this.trackBarFont.Minimum = 8;
            this.trackBarFont.Name = "trackBarFont";
            this.trackBarFont.Size = new System.Drawing.Size(310, 45);
            this.trackBarFont.TabIndex = 3;
            this.trackBarFont.Value = 10;
            this.trackBarFont.Scroll += new System.EventHandler(this.trackBarFont_Scroll);
            // 
            // chkHieuUng
            // 
            this.chkHieuUng.AutoSize = true;
            this.chkHieuUng.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkHieuUng.Location = new System.Drawing.Point(20, 95);
            this.chkHieuUng.Name = "chkHieuUng";
            this.chkHieuUng.Size = new System.Drawing.Size(152, 19);
            this.chkHieuUng.TabIndex = 2;
            this.chkHieuUng.Text = "Hiệu ứng chuyển động";
            this.chkHieuUng.UseVisualStyleBackColor = true;
            // 
            // cboTheme
            // 
            this.cboTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTheme.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboTheme.FormattingEnabled = true;
            this.cboTheme.Items.AddRange(new object[] {
            "Sáng",
            "Tối",
            "Tự động"});
            this.cboTheme.Location = new System.Drawing.Point(20, 55);
            this.cboTheme.Name = "cboTheme";
            this.cboTheme.Size = new System.Drawing.Size(200, 23);
            this.cboTheme.TabIndex = 1;
            // 
            // lblTheme
            // 
            this.lblTheme.AutoSize = true;
            this.lblTheme.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTheme.Location = new System.Drawing.Point(20, 35);
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(74, 15);
            this.lblTheme.TabIndex = 0;
            this.lblTheme.Text = "Chọn theme:";
            // 
            // grpThongBao
            // 
            this.grpThongBao.Controls.Add(this.nudSoNgay);
            this.grpThongBao.Controls.Add(this.lblSoNgay);
            this.grpThongBao.Controls.Add(this.chkThueXe);
            this.grpThongBao.Controls.Add(this.chkBaoHiem);
            this.grpThongBao.Controls.Add(this.chkDangKy);
            this.grpThongBao.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpThongBao.Location = new System.Drawing.Point(20, 360);
            this.grpThongBao.Name = "grpThongBao";
            this.grpThongBao.Size = new System.Drawing.Size(350, 180);
            this.grpThongBao.TabIndex = 2;
            this.grpThongBao.TabStop = false;
            this.grpThongBao.Text = "Thông báo";
            // 
            // nudSoNgay
            // 
            this.nudSoNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudSoNgay.Location = new System.Drawing.Point(150, 135);
            this.nudSoNgay.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.nudSoNgay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoNgay.Name = "nudSoNgay";
            this.nudSoNgay.Size = new System.Drawing.Size(70, 23);
            this.nudSoNgay.TabIndex = 4;
            this.nudSoNgay.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // lblSoNgay
            // 
            this.lblSoNgay.AutoSize = true;
            this.lblSoNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoNgay.Location = new System.Drawing.Point(20, 137);
            this.lblSoNgay.Name = "lblSoNgay";
            this.lblSoNgay.Size = new System.Drawing.Size(110, 15);
            this.lblSoNgay.TabIndex = 3;
            this.lblSoNgay.Text = "Số ngày nhắc trước:";
            // 
            // chkThueXe
            // 
            this.chkThueXe.AutoSize = true;
            this.chkThueXe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkThueXe.Location = new System.Drawing.Point(20, 100);
            this.chkThueXe.Name = "chkThueXe";
            this.chkThueXe.Size = new System.Drawing.Size(248, 19);
            this.chkThueXe.TabIndex = 2;
            this.chkThueXe.Text = "Thông báo khi khách hàng sắp hết hạn thuê";
            this.chkThueXe.UseVisualStyleBackColor = true;
            // 
            // chkBaoHiem
            // 
            this.chkBaoHiem.AutoSize = true;
            this.chkBaoHiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkBaoHiem.Location = new System.Drawing.Point(20, 70);
            this.chkBaoHiem.Name = "chkBaoHiem";
            this.chkBaoHiem.Size = new System.Drawing.Size(233, 19);
            this.chkBaoHiem.TabIndex = 1;
            this.chkBaoHiem.Text = "Thông báo khi xe sắp hết hạn bảo hiểm";
            this.chkBaoHiem.UseVisualStyleBackColor = true;
            // 
            // chkDangKy
            // 
            this.chkDangKy.AutoSize = true;
            this.chkDangKy.Checked = true;
            this.chkDangKy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDangKy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkDangKy.Location = new System.Drawing.Point(20, 40);
            this.chkDangKy.Name = "chkDangKy";
            this.chkDangKy.Size = new System.Drawing.Size(228, 19);
            this.chkDangKy.TabIndex = 0;
            this.chkDangKy.Text = "Thông báo khi xe sắp hết hạn đăng ký";
            this.chkDangKy.UseVisualStyleBackColor = true;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(20, 560);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 40);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(135, 560);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 40);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnKhoiPhuc
            // 
            this.btnKhoiPhuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnKhoiPhuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKhoiPhuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKhoiPhuc.ForeColor = System.Drawing.Color.Black;
            this.btnKhoiPhuc.Location = new System.Drawing.Point(250, 560);
            this.btnKhoiPhuc.Name = "btnKhoiPhuc";
            this.btnKhoiPhuc.Size = new System.Drawing.Size(120, 40);
            this.btnKhoiPhuc.TabIndex = 5;
            this.btnKhoiPhuc.Text = "Khôi phục mặc định";
            this.btnKhoiPhuc.UseVisualStyleBackColor = false;
            this.btnKhoiPhuc.Click += new System.EventHandler(this.btnKhoiPhuc_Click);
            // 
            // ViewCaiDat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnKhoiPhuc);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.grpThongBao);
            this.Controls.Add(this.grpGiaoDien);
            this.Controls.Add(this.grpNgonNgu);
            this.Name = "ViewCaiDat";
            this.Size = new System.Drawing.Size(800, 650);
            this.Load += new System.EventHandler(this.ViewCaiDat_Load);
            this.grpNgonNgu.ResumeLayout(false);
            this.grpNgonNgu.PerformLayout();
            this.grpGiaoDien.ResumeLayout(false);
            this.grpGiaoDien.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFont)).EndInit();
            this.grpThongBao.ResumeLayout(false);
            this.grpThongBao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNgay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpNgonNgu;
        private System.Windows.Forms.RadioButton rdoEnglish;
        private System.Windows.Forms.RadioButton rdoTiengViet;
        private System.Windows.Forms.GroupBox grpGiaoDien;
        private System.Windows.Forms.Label lblFontSize;
        private System.Windows.Forms.TrackBar trackBarFont;
        private System.Windows.Forms.CheckBox chkHieuUng;
        private System.Windows.Forms.ComboBox cboTheme;
        private System.Windows.Forms.Label lblTheme;
        private System.Windows.Forms.GroupBox grpThongBao;
        private System.Windows.Forms.NumericUpDown nudSoNgay;
        private System.Windows.Forms.Label lblSoNgay;
        private System.Windows.Forms.CheckBox chkThueXe;
        private System.Windows.Forms.CheckBox chkBaoHiem;
        private System.Windows.Forms.CheckBox chkDangKy;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnKhoiPhuc;
    }
}
