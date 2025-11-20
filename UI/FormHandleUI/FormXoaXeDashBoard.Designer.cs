namespace UI.FormHandleUI
{
    partial class FormXoaXeDashBoard
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMucDichSuDung = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtGiaMua = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTrangThai = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNamSanXuat = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMauSac = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDongXe = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHangXe = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBienSo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaXe = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // panel1
            this.panel1.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 100);
            this.panel1.TabIndex = 0;
            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(280, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "XÓA XE MÁY";
            // groupBox1
            this.groupBox1.Controls.Add(this.txtMucDichSuDung);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtSoLuong);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtGiaMua);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtTrangThai);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtNamSanXuat);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtMauSac);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtDongXe);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtHangXe);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtBienSo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtMaXe);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(20, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 360);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin xe sẽ bị xóa";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // txtMucDichSuDung
            this.txtMucDichSuDung.BackColor = System.Drawing.Color.White;
            this.txtMucDichSuDung.Location = new System.Drawing.Point(190, 320);
            this.txtMucDichSuDung.ReadOnly = true;
            this.txtMucDichSuDung.Size = new System.Drawing.Size(280, 22);
            // label11
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 323);
            this.label11.Text = "Mục đích sử dụng";
            // txtSoLuong
            this.txtSoLuong.BackColor = System.Drawing.Color.White;
            this.txtSoLuong.Location = new System.Drawing.Point(190, 280);
            this.txtSoLuong.ReadOnly = true;
            this.txtSoLuong.Size = new System.Drawing.Size(280, 22);
            // label10
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 283);
            this.label10.Text = "Số lượng";
            // txtGiaMua
            this.txtGiaMua.BackColor = System.Drawing.Color.White;
            this.txtGiaMua.Location = new System.Drawing.Point(190, 240);
            this.txtGiaMua.ReadOnly = true;
            this.txtGiaMua.Size = new System.Drawing.Size(280, 22);
            // label9
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 243);
            this.label9.Text = "Giá mua";
            // txtTrangThai
            this.txtTrangThai.BackColor = System.Drawing.Color.White;
            this.txtTrangThai.Location = new System.Drawing.Point(190, 200);
            this.txtTrangThai.ReadOnly = true;
            this.txtTrangThai.Size = new System.Drawing.Size(280, 22);
            // label8
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 203);
            this.label8.Text = "Trạng thái";
            // txtNamSanXuat
            this.txtNamSanXuat.BackColor = System.Drawing.Color.White;
            this.txtNamSanXuat.Location = new System.Drawing.Point(190, 160);
            this.txtNamSanXuat.ReadOnly = true;
            this.txtNamSanXuat.Size = new System.Drawing.Size(280, 22);
            // label7
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 163);
            this.label7.Text = "Năm sản xuất";
            // txtMauSac
            this.txtMauSac.BackColor = System.Drawing.Color.White;
            this.txtMauSac.Location = new System.Drawing.Point(190, 120);
            this.txtMauSac.ReadOnly = true;
            this.txtMauSac.Size = new System.Drawing.Size(280, 22);
            // label6
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 123);
            this.label6.Text = "Màu sắc";
            // txtDongXe
            this.txtDongXe.BackColor = System.Drawing.Color.White;
            this.txtDongXe.Location = new System.Drawing.Point(190, 80);
            this.txtDongXe.ReadOnly = true;
            this.txtDongXe.Size = new System.Drawing.Size(280, 22);
            // label5
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 83);
            this.label5.Text = "Dòng xe";
            // txtHangXe
            this.txtHangXe.BackColor = System.Drawing.Color.White;
            this.txtHangXe.Location = new System.Drawing.Point(190, 40);
            this.txtHangXe.ReadOnly = true;
            this.txtHangXe.Size = new System.Drawing.Size(280, 22);
            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 43);
            this.label4.Text = "Hãng xe";
            // txtBienSo
            this.txtBienSo.BackColor = System.Drawing.Color.White;
            this.txtBienSo.Location = new System.Drawing.Point(570, 40);
            this.txtBienSo.ReadOnly = true;
            this.txtBienSo.Size = new System.Drawing.Size(150, 22);
            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(510, 43);
            this.label3.Text = "Biển số";
            // txtMaXe
            this.txtMaXe.BackColor = System.Drawing.Color.White;
            this.txtMaXe.Location = new System.Drawing.Point(570, 10);
            this.txtMaXe.ReadOnly = true;
            this.txtMaXe.Size = new System.Drawing.Size(150, 22);
            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(510, 13);
            this.label2.Text = "Mã xe";
            // lblWarning
            this.lblWarning.AutoSize = true;
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblWarning.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.lblWarning.Location = new System.Drawing.Point(180, 500);
            this.lblWarning.Text = "⚠ CẢNH BÁO: Hành động này không thể hoàn tác!";
            // btnXoa
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(250, 540);
            this.btnXoa.Size = new System.Drawing.Size(140, 50);
            this.btnXoa.Text = "XÓA XE";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // btnHuy
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(108,117,125);
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(410, 540);
            this.btnHuy.Size = new System.Drawing.Size(140, 50);
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F,16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 610);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormXoaXeDashBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xóa Xe";
            this.Load += new System.EventHandler(this.FormXoaXeDashBoard_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMaXe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBienSo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHangXe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDongXe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMauSac;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNamSanXuat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTrangThai;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtGiaMua;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMucDichSuDung;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnHuy;
    }
}