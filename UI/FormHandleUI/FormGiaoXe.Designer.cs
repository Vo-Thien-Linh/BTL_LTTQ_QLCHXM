namespace UI.FormHandleUI
{
    partial class FormGiaoXe
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

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lblNgayGiao = new System.Windows.Forms.Label();
            this.dtpNgayGiao = new System.Windows.Forms.DateTimePicker();
            this.lblKm = new System.Windows.Forms.Label();
            this.nudKmBatDau = new System.Windows.Forms.NumericUpDown();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.lblWarning = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKmBatDau)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(500, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(480, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = " XÁC NHẬN GIAO XE";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.White;
            this.lblSubtitle.Location = new System.Drawing.Point(10, 50);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(480, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Vui lòng nhập thông tin giao xe cho khách hàng";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Controls.Add(this.lblNgayGiao);
            this.pnlContent.Controls.Add(this.dtpNgayGiao);
            this.pnlContent.Controls.Add(this.lblKm);
            this.pnlContent.Controls.Add(this.nudKmBatDau);
            this.pnlContent.Controls.Add(this.lblGhiChu);
            this.pnlContent.Controls.Add(this.txtGhiChu);
            this.pnlContent.Location = new System.Drawing.Point(20, 100);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(460, 180);
            this.pnlContent.TabIndex = 1;
            // 
            // lblNgayGiao
            // 
            this.lblNgayGiao.AutoSize = true;
            this.lblNgayGiao.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNgayGiao.Location = new System.Drawing.Point(20, 20);
            this.lblNgayGiao.Name = "lblNgayGiao";
            this.lblNgayGiao.Size = new System.Drawing.Size(130, 19);
            this.lblNgayGiao.TabIndex = 0;
            this.lblNgayGiao.Text = " Ngày giao xe:";
            // 
            // dtpNgayGiao
            // 
            this.dtpNgayGiao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayGiao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayGiao.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayGiao.Location = new System.Drawing.Point(200, 17);
            this.dtpNgayGiao.Name = "dtpNgayGiao";
            this.dtpNgayGiao.Size = new System.Drawing.Size(240, 25);
            this.dtpNgayGiao.TabIndex = 1;
            // 
            // lblKm
            // 
            this.lblKm.AutoSize = true;
            this.lblKm.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblKm.Location = new System.Drawing.Point(20, 60);
            this.lblKm.Name = "lblKm";
            this.lblKm.Size = new System.Drawing.Size(115, 19);
            this.lblKm.TabIndex = 2;
            this.lblKm.Text = "Km bắt đầu:";
            // 
            // nudKmBatDau
            // 
            this.nudKmBatDau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudKmBatDau.Location = new System.Drawing.Point(200, 57);
            this.nudKmBatDau.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.nudKmBatDau.Name = "nudKmBatDau";
            this.nudKmBatDau.Size = new System.Drawing.Size(240, 25);
            this.nudKmBatDau.TabIndex = 2;
            this.nudKmBatDau.ThousandsSeparator = true;
            this.nudKmBatDau.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGhiChu.Location = new System.Drawing.Point(20, 100);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(82, 19);
            this.lblGhiChu.TabIndex = 4;
            this.lblGhiChu.Text = " Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(200, 97);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(240, 60);
            this.txtGhiChu.TabIndex = 3;
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblWarning.Location = new System.Drawing.Point(20, 290);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(460, 40);
            this.lblWarning.TabIndex = 2;
            this.lblWarning.Text = "Lưu ý: Sau khi xác nhận giao xe, trạng thái đơn và xe sẽ chuyển sang \"Đang thuê\". Không thể hoàn tác!";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnXacNhan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXacNhan.FlatAppearance.BorderSize = 0;
            this.btnXacNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(20, 340);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(220, 45);
            this.btnXacNhan.TabIndex = 4;
            this.btnXacNhan.Text = "XÁC NHẬN GIAO XE";
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
            this.btnHuy.Location = new System.Drawing.Point(260, 340);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(220, 45);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = " HỦY";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // FormGiaoXe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGiaoXe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xác Nhận Giao Xe";
            this.pnlHeader.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKmBatDau)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblNgayGiao;
        private System.Windows.Forms.DateTimePicker dtpNgayGiao;
        private System.Windows.Forms.Label lblKm;
        private System.Windows.Forms.NumericUpDown nudKmBatDau;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuy;
    }
}