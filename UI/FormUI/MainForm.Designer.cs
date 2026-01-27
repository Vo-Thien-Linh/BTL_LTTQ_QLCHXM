using System.Windows.Forms;

namespace UI.FormUI
{
    partial class MainForm : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnList = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlMenuBar = new System.Windows.Forms.Panel();
            this.btnCaiDat = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.btnQuanLyXuLy = new System.Windows.Forms.Button();
            this.btnQuanLyChoThue = new System.Windows.Forms.Button();
            this.btnQuanLyBanHang = new System.Windows.Forms.Button();
            this.btnQuanLySanPham = new System.Windows.Forms.Button();
            this.btnQuanLyKhachHang = new System.Windows.Forms.Button();
            this.btnQuanLyNhanVien = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlMenuBar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pnlHeader.Controls.Add(this.btnList);
            this.pnlHeader.Controls.Add(this.pictureBox2);
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1385, 162);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnList
            // 
            this.btnList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnList.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnList.Location = new System.Drawing.Point(26, 83);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(54, 59);
            this.btnList.TabIndex = 2;
            this.btnList.Text = "☰";
            this.btnList.UseVisualStyleBackColor = false;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(257, 47);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(265, 95);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(114, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(137, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pnlMenuBar
            // 
            this.pnlMenuBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.pnlMenuBar.Controls.Add(this.btnCaiDat);
            this.pnlMenuBar.Controls.Add(this.btnThongKe);
            this.pnlMenuBar.Controls.Add(this.btnDangXuat);
            this.pnlMenuBar.Controls.Add(this.btnQuanLyXuLy);
            this.pnlMenuBar.Controls.Add(this.btnQuanLyChoThue);
            this.pnlMenuBar.Controls.Add(this.btnQuanLyBanHang);
            this.pnlMenuBar.Controls.Add(this.btnQuanLySanPham);
            this.pnlMenuBar.Controls.Add(this.btnQuanLyKhachHang);
            this.pnlMenuBar.Controls.Add(this.btnQuanLyNhanVien);
            this.pnlMenuBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenuBar.Location = new System.Drawing.Point(0, 162);
            this.pnlMenuBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlMenuBar.Name = "pnlMenuBar";
            this.pnlMenuBar.Size = new System.Drawing.Size(356, 773);
            this.pnlMenuBar.TabIndex = 1;
            this.pnlMenuBar.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMenuBar_Paint);
            // 
            // btnCaiDat
            // 
            this.btnCaiDat.Location = new System.Drawing.Point(0, 591);
            this.btnCaiDat.Margin = new System.Windows.Forms.Padding(4);
            this.btnCaiDat.Name = "btnCaiDat";
            this.btnCaiDat.Size = new System.Drawing.Size(267, 95);
            this.btnCaiDat.TabIndex = 8;
            this.btnCaiDat.Text = "Cài đặt";
            this.btnCaiDat.UseVisualStyleBackColor = true;
            this.btnCaiDat.Click += new System.EventHandler(this.btnCaiDat_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(0, 516);
            this.btnThongKe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(267, 85);
            this.btnThongKe.TabIndex = 0;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Location = new System.Drawing.Point(26, 702);
            this.btnDangXuat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(181, 59);
            this.btnDangXuat.TabIndex = 7;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangxuat_Click);
            // 
            // btnQuanLyXuLy
            // 
            this.btnQuanLyXuLy.Location = new System.Drawing.Point(0, 428);
            this.btnQuanLyXuLy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuanLyXuLy.Name = "btnQuanLyXuLy";
            this.btnQuanLyXuLy.Size = new System.Drawing.Size(267, 93);
            this.btnQuanLyXuLy.TabIndex = 6;
            this.btnQuanLyXuLy.Text = "Quản lý bảo trì";
            this.btnQuanLyXuLy.UseVisualStyleBackColor = true;
            this.btnQuanLyXuLy.Click += new System.EventHandler(this.btnQuanLyXuLy_Click);
            // 
            // btnQuanLyChoThue
            // 
            this.btnQuanLyChoThue.Location = new System.Drawing.Point(0, 342);
            this.btnQuanLyChoThue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuanLyChoThue.Name = "btnQuanLyChoThue";
            this.btnQuanLyChoThue.Size = new System.Drawing.Size(267, 93);
            this.btnQuanLyChoThue.TabIndex = 5;
            this.btnQuanLyChoThue.Text = "Quản lý cho thuê";
            this.btnQuanLyChoThue.UseVisualStyleBackColor = true;
            this.btnQuanLyChoThue.Click += new System.EventHandler(this.btnQuanLyChoThue_Click);
            // 
            // btnQuanLyBanHang
            // 
            this.btnQuanLyBanHang.Location = new System.Drawing.Point(0, 258);
            this.btnQuanLyBanHang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuanLyBanHang.Name = "btnQuanLyBanHang";
            this.btnQuanLyBanHang.Size = new System.Drawing.Size(267, 93);
            this.btnQuanLyBanHang.TabIndex = 4;
            this.btnQuanLyBanHang.Text = "Quản lý bán hàng";
            this.btnQuanLyBanHang.UseVisualStyleBackColor = true;
            this.btnQuanLyBanHang.Click += new System.EventHandler(this.btnQuanLyBanHang_Click);
            // 
            // btnQuanLySanPham
            // 
            this.btnQuanLySanPham.Location = new System.Drawing.Point(0, 174);
            this.btnQuanLySanPham.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuanLySanPham.Name = "btnQuanLySanPham";
            this.btnQuanLySanPham.Size = new System.Drawing.Size(267, 93);
            this.btnQuanLySanPham.TabIndex = 3;
            this.btnQuanLySanPham.Text = "Quản lý sản phẩm";
            this.btnQuanLySanPham.UseVisualStyleBackColor = true;
            this.btnQuanLySanPham.Click += new System.EventHandler(this.btnQuanLySanPham_Click);
            // 
            // btnQuanLyKhachHang
            // 
            this.btnQuanLyKhachHang.Location = new System.Drawing.Point(0, 88);
            this.btnQuanLyKhachHang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuanLyKhachHang.Name = "btnQuanLyKhachHang";
            this.btnQuanLyKhachHang.Size = new System.Drawing.Size(267, 93);
            this.btnQuanLyKhachHang.TabIndex = 2;
            this.btnQuanLyKhachHang.Text = "Quản lý khách hàng";
            this.btnQuanLyKhachHang.UseVisualStyleBackColor = true;
            this.btnQuanLyKhachHang.Click += new System.EventHandler(this.btnQuanLyKhachHang_Click);
            // 
            // btnQuanLyNhanVien
            // 
            this.btnQuanLyNhanVien.Location = new System.Drawing.Point(0, 0);
            this.btnQuanLyNhanVien.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuanLyNhanVien.Name = "btnQuanLyNhanVien";
            this.btnQuanLyNhanVien.Size = new System.Drawing.Size(267, 93);
            this.btnQuanLyNhanVien.TabIndex = 1;
            this.btnQuanLyNhanVien.Text = "Quản lý nhân viên";
            this.btnQuanLyNhanVien.UseVisualStyleBackColor = true;
            this.btnQuanLyNhanVien.Click += new System.EventHandler(this.btnQuanLyNhanVien_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnlContent);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(356, 162);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1029, 773);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1029, 773);
            this.pnlContent.TabIndex = 0;
            this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1385, 935);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlMenuBar);
            this.Controls.Add(this.pnlHeader);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlMenuBar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlMenuBar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Button btnQuanLyXuLy;
        private System.Windows.Forms.Button btnQuanLyChoThue;
        private System.Windows.Forms.Button btnQuanLyBanHang;
        private System.Windows.Forms.Button btnQuanLySanPham;
        private System.Windows.Forms.Button btnQuanLyKhachHang;
        private System.Windows.Forms.Button btnQuanLyNhanVien;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Panel pnlContent;
        private Button btnCaiDat;
        private Button btnList;
    }
}