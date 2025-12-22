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
            this.cboTheme = new System.Windows.Forms.ComboBox();
            this.lblTheme = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnKhoiPhuc = new System.Windows.Forms.Button();
            this.grpNgonNgu.SuspendLayout();
            this.grpGiaoDien.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNgonNgu
            // 
            this.grpNgonNgu.Controls.Add(this.rdoEnglish);
            this.grpNgonNgu.Controls.Add(this.rdoTiengViet);
            this.grpNgonNgu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpNgonNgu.Location = new System.Drawing.Point(27, 25);
            this.grpNgonNgu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpNgonNgu.Name = "grpNgonNgu";
            this.grpNgonNgu.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpNgonNgu.Size = new System.Drawing.Size(467, 123);
            this.grpNgonNgu.TabIndex = 0;
            this.grpNgonNgu.TabStop = false;
            this.grpNgonNgu.Text = "Ngôn ngữ / Language";
            // 
            // rdoEnglish
            // 
            this.rdoEnglish.AutoSize = true;
            this.rdoEnglish.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rdoEnglish.Location = new System.Drawing.Point(27, 74);
            this.rdoEnglish.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoEnglish.Name = "rdoEnglish";
            this.rdoEnglish.Size = new System.Drawing.Size(77, 24);
            this.rdoEnglish.TabIndex = 1;
            this.rdoEnglish.Text = "English";
            this.rdoEnglish.UseVisualStyleBackColor = true;
            // 
            // rdoTiengViet
            // 
            this.rdoTiengViet.AutoSize = true;
            this.rdoTiengViet.Checked = true;
            this.rdoTiengViet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rdoTiengViet.Location = new System.Drawing.Point(27, 43);
            this.rdoTiengViet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoTiengViet.Name = "rdoTiengViet";
            this.rdoTiengViet.Size = new System.Drawing.Size(97, 24);
            this.rdoTiengViet.TabIndex = 0;
            this.rdoTiengViet.TabStop = true;
            this.rdoTiengViet.Text = "Tiếng Việt";
            this.rdoTiengViet.UseVisualStyleBackColor = true;
            // 
            // grpGiaoDien
            // 
            this.grpGiaoDien.Controls.Add(this.cboTheme);
            this.grpGiaoDien.Controls.Add(this.lblTheme);
            this.grpGiaoDien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpGiaoDien.Location = new System.Drawing.Point(27, 172);
            this.grpGiaoDien.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpGiaoDien.Name = "grpGiaoDien";
            this.grpGiaoDien.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpGiaoDien.Size = new System.Drawing.Size(467, 140);
            this.grpGiaoDien.TabIndex = 1;
            this.grpGiaoDien.TabStop = false;
            this.grpGiaoDien.Text = "Giao diện";
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
            this.cboTheme.Location = new System.Drawing.Point(27, 68);
            this.cboTheme.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboTheme.Name = "cboTheme";
            this.cboTheme.Size = new System.Drawing.Size(265, 28);
            this.cboTheme.TabIndex = 1;
            // 
            // lblTheme
            // 
            this.lblTheme.AutoSize = true;
            this.lblTheme.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTheme.Location = new System.Drawing.Point(27, 43);
            this.lblTheme.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(92, 20);
            this.lblTheme.TabIndex = 0;
            this.lblTheme.Text = "Chọn theme:";
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(27, 365);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(133, 49);
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
            this.btnHuy.Location = new System.Drawing.Point(180, 365);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(133, 49);
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
            this.btnKhoiPhuc.Location = new System.Drawing.Point(333, 365);
            this.btnKhoiPhuc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnKhoiPhuc.Name = "btnKhoiPhuc";
            this.btnKhoiPhuc.Size = new System.Drawing.Size(160, 49);
            this.btnKhoiPhuc.TabIndex = 5;
            this.btnKhoiPhuc.Text = "Khôi phục mặc định";
            this.btnKhoiPhuc.UseVisualStyleBackColor = false;
            this.btnKhoiPhuc.Click += new System.EventHandler(this.btnKhoiPhuc_Click);
            // 
            // ViewCaiDat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnKhoiPhuc);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.grpGiaoDien);
            this.Controls.Add(this.grpNgonNgu);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ViewCaiDat";
            this.Size = new System.Drawing.Size(1067, 800);
            this.Load += new System.EventHandler(this.ViewCaiDat_Load);
            this.grpNgonNgu.ResumeLayout(false);
            this.grpNgonNgu.PerformLayout();
            this.grpGiaoDien.ResumeLayout(false);
            this.grpGiaoDien.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpNgonNgu;
        private System.Windows.Forms.RadioButton rdoEnglish;
        private System.Windows.Forms.RadioButton rdoTiengViet;
        private System.Windows.Forms.GroupBox grpGiaoDien;
        private System.Windows.Forms.ComboBox cboTheme;
        private System.Windows.Forms.Label lblTheme;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnKhoiPhuc;
    }
}
