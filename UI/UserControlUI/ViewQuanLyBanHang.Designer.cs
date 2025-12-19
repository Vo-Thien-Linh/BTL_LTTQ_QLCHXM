namespace UI.UserControlUI
{
    partial class ViewQuanLyBanHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelDataGrid;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnXemHopDong;
        private System.Windows.Forms.Label lblRecordCount;

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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelDataGrid = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnXemHopDong = new System.Windows.Forms.Button();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.txtSearch);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.panelTop.Size = new System.Drawing.Size(1333, 148);
            this.panelTop.TabIndex = 0;
            this.panelTop.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTop_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(317, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ BÁN HÀNG";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(467, 80);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(399, 30);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.Text = "Tìm kiếm theo tên";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(880, 78);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(133, 37);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "🔍 Tìm";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(1027, 78);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(160, 37);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // panelDataGrid
            // 
            this.panelDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDataGrid.Location = new System.Drawing.Point(0, 148);
            this.panelDataGrid.Margin = new System.Windows.Forms.Padding(4);
            this.panelDataGrid.Name = "panelDataGrid";
            this.panelDataGrid.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.panelDataGrid.Size = new System.Drawing.Size(1333, 591);
            this.panelDataGrid.TabIndex = 1;
            this.panelDataGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDataGrid_Paint);
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.White;
            this.panelBottom.Controls.Add(this.btnXemHopDong);
            this.panelBottom.Controls.Add(this.lblRecordCount);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 739);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(4);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.panelBottom.Size = new System.Drawing.Size(1333, 86);
            this.panelBottom.TabIndex = 2;
            // 
            // btnXemHopDong
            // 
            this.btnXemHopDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.btnXemHopDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemHopDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXemHopDong.ForeColor = System.Drawing.Color.White;
            this.btnXemHopDong.Location = new System.Drawing.Point(39, 31);
            this.btnXemHopDong.Margin = new System.Windows.Forms.Padding(4);
            this.btnXemHopDong.Name = "btnXemHopDong";
            this.btnXemHopDong.Size = new System.Drawing.Size(200, 43);
            this.btnXemHopDong.TabIndex = 4;
            this.btnXemHopDong.Text = "📋 Xem Hợp Đồng";
            this.btnXemHopDong.UseVisualStyleBackColor = false;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRecordCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblRecordCount.Location = new System.Drawing.Point(933, 31);
            this.lblRecordCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(373, 31);
            this.lblRecordCount.TabIndex = 3;
            this.lblRecordCount.Text = "Tổng số giao dịch: 0";
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ViewQuanLyBanHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.Controls.Add(this.panelDataGrid);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ViewQuanLyBanHang";
            this.Size = new System.Drawing.Size(1333, 825);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}