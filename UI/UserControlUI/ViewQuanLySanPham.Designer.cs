namespace UI.UserControlUI
{
    partial class ViewQuanLySanPham
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnQuanLyPhuTung = new System.Windows.Forms.Button();
            this.btnQuanLyXe = new System.Windows.Forms.Button();
          //  this.panel3 = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1928, 91);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1103, 70);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "QUẢN LÝ SẢN PHẨM";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnQuanLyPhuTung);
            this.panel2.Controls.Add(this.btnQuanLyXe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 91);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panel2.Size = new System.Drawing.Size(1928, 60);
            this.panel2.TabIndex = 1;
            // 
            // btnQuanLyPhuTung
            // 
            this.btnQuanLyPhuTung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuanLyPhuTung.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnQuanLyPhuTung.Location = new System.Drawing.Point(190, 10);
            this.btnQuanLyPhuTung.Name = "btnQuanLyPhuTung";
            this.btnQuanLyPhuTung.Size = new System.Drawing.Size(150, 40);
            this.btnQuanLyPhuTung.TabIndex = 1;
            this.btnQuanLyPhuTung.Text = "Quản lý phụ tùng";
            this.btnQuanLyPhuTung.UseVisualStyleBackColor = true;
            this.btnQuanLyPhuTung.Click += new System.EventHandler(this.btnQuanLyPhuTung_Click);
            // 
            // btnQuanLyXe
            // 
            this.btnQuanLyXe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuanLyXe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnQuanLyXe.Location = new System.Drawing.Point(20, 10);
            this.btnQuanLyXe.Name = "btnQuanLyXe";
            this.btnQuanLyXe.Size = new System.Drawing.Size(150, 40);
            this.btnQuanLyXe.TabIndex = 0;
            this.btnQuanLyXe.Text = "Quản Lý xe";
            this.btnQuanLyXe.UseVisualStyleBackColor = true;
            this.btnQuanLyXe.Click += new System.EventHandler(this.btnQuanLyXe_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 151);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(1928, 643);
            this.pnlMain.TabIndex = 3;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            // 
            // ViewQuanLySanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ViewQuanLySanPham";
            this.Size = new System.Drawing.Size(1928, 794);
            this.Load += new System.EventHandler(this.ViewQuanLySanPham_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnQuanLyPhuTung;
        private System.Windows.Forms.Button btnQuanLyXe;
        private System.Windows.Forms.Panel pnlMain;
    }
}
