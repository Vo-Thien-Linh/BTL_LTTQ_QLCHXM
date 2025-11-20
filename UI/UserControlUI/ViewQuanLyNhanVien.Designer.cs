namespace UI.UserControlUI
{
    partial class ViewQuanLyNhanVien
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTuKhoa = new System.Windows.Forms.TextBox();
            this.lblTuKhoa = new System.Windows.Forms.Label();
            this.cboTimKiem = new System.Windows.Forms.ComboBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoaNhanVien = new System.Windows.Forms.Button();
            this.btnSuaNhanVien = new System.Windows.Forms.Button();
            this.btnThemNhanVien = new System.Windows.Forms.Button();
            this.panelDataGrid = new System.Windows.Forms.Panel();
            this.lblDemNhanVien = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.panelHeader.Controls.Add(this.lblTieuDe);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(20, 20);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.panelHeader.Size = new System.Drawing.Size(1160, 70);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(1120, 70);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "QUẢN LÝ NHÂN VIÊN";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTieuDe.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.White;
            this.panelSearch.Controls.Add(this.btnTimKiem);
            this.panelSearch.Controls.Add(this.txtTuKhoa);
            this.panelSearch.Controls.Add(this.lblTuKhoa);
            this.panelSearch.Controls.Add(this.cboTimKiem);
            this.panelSearch.Controls.Add(this.lblTimKiem);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(20, 90);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelSearch.Size = new System.Drawing.Size(1160, 80);
            this.panelSearch.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(744, 20);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 35);
            this.btnTimKiem.TabIndex = 4;
            this.btnTimKiem.Text = "🔍 Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // txtTuKhoa
            // 
            this.txtTuKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTuKhoa.Location = new System.Drawing.Point(438, 24);
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.Size = new System.Drawing.Size(300, 30);
            this.txtTuKhoa.TabIndex = 3;
            // 
            // lblTuKhoa
            // 
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTuKhoa.Location = new System.Drawing.Point(357, 28);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(75, 23);
            this.lblTuKhoa.TabIndex = 2;
            this.lblTuKhoa.Text = "Từ khóa:";
            // 
            // cboTimKiem
            // 
            this.cboTimKiem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTimKiem.FormattingEnabled = true;
            this.cboTimKiem.Items.AddRange(new object[] {
            "Mã nhân viên",
            "Họ và tên",
            "Số điện thoại",
            "Email"});
            this.cboTimKiem.Location = new System.Drawing.Point(149, 24);
            this.cboTimKiem.Name = "cboTimKiem";
            this.cboTimKiem.Size = new System.Drawing.Size(180, 31);
            this.cboTimKiem.TabIndex = 1;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTimKiem.Location = new System.Drawing.Point(20, 28);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(123, 23);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tìm kiếm theo:";
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.White;
            this.panelActions.Controls.Add(this.btnLamMoi);
            this.panelActions.Controls.Add(this.btnXoaNhanVien);
            this.panelActions.Controls.Add(this.btnSuaNhanVien);
            this.panelActions.Controls.Add(this.btnThemNhanVien);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(20, 170);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelActions.Size = new System.Drawing.Size(1160, 70);
            this.panelActions.TabIndex = 2;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(500, 13);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(140, 45);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "🔄 Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnXoaNhanVien
            // 
            this.btnXoaNhanVien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnXoaNhanVien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaNhanVien.FlatAppearance.BorderSize = 0;
            this.btnXoaNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaNhanVien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoaNhanVien.ForeColor = System.Drawing.Color.White;
            this.btnXoaNhanVien.Location = new System.Drawing.Point(340, 13);
            this.btnXoaNhanVien.Name = "btnXoaNhanVien";
            this.btnXoaNhanVien.Size = new System.Drawing.Size(140, 45);
            this.btnXoaNhanVien.TabIndex = 2;
            this.btnXoaNhanVien.Text = "🗑️ Xóa";
            this.btnXoaNhanVien.UseVisualStyleBackColor = false;
            // 
            // btnSuaNhanVien
            // 
            this.btnSuaNhanVien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnSuaNhanVien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSuaNhanVien.FlatAppearance.BorderSize = 0;
            this.btnSuaNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuaNhanVien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSuaNhanVien.ForeColor = System.Drawing.Color.White;
            this.btnSuaNhanVien.Location = new System.Drawing.Point(180, 13);
            this.btnSuaNhanVien.Name = "btnSuaNhanVien";
            this.btnSuaNhanVien.Size = new System.Drawing.Size(140, 45);
            this.btnSuaNhanVien.TabIndex = 1;
            this.btnSuaNhanVien.Text = "✏️ Sửa";
            this.btnSuaNhanVien.UseVisualStyleBackColor = false;
            // 
            // btnThemNhanVien
            // 
            this.btnThemNhanVien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnThemNhanVien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemNhanVien.FlatAppearance.BorderSize = 0;
            this.btnThemNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemNhanVien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemNhanVien.ForeColor = System.Drawing.Color.White;
            this.btnThemNhanVien.Location = new System.Drawing.Point(20, 13);
            this.btnThemNhanVien.Name = "btnThemNhanVien";
            this.btnThemNhanVien.Size = new System.Drawing.Size(140, 45);
            this.btnThemNhanVien.TabIndex = 0;
            this.btnThemNhanVien.Text = "+ Thêm mới";
            this.btnThemNhanVien.UseVisualStyleBackColor = false;
            this.btnThemNhanVien.Click += new System.EventHandler(this.btn_AddEmployee_Click_1);
            // 
            // panelDataGrid
            // 
            this.panelDataGrid.BackColor = System.Drawing.Color.White;
            this.panelDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDataGrid.Location = new System.Drawing.Point(20, 240);
            this.panelDataGrid.Name = "panelDataGrid";
            this.panelDataGrid.Padding = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.panelDataGrid.Size = new System.Drawing.Size(1160, 440);
            this.panelDataGrid.TabIndex = 3;
            this.panelDataGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDataGrid_Paint);
            // 
            // lblDemNhanVien
            // 
            this.lblDemNhanVien.BackColor = System.Drawing.Color.White;
            this.lblDemNhanVien.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDemNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblDemNhanVien.Location = new System.Drawing.Point(20, 680);
            this.lblDemNhanVien.Name = "lblDemNhanVien";
            this.lblDemNhanVien.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.lblDemNhanVien.Size = new System.Drawing.Size(1160, 40);
            this.lblDemNhanVien.TabIndex = 4;
            this.lblDemNhanVien.Text = "Tổng số nhân viên: 0";
            this.lblDemNhanVien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDemNhanVien.Click += new System.EventHandler(this.lblRecordCount_Click);
            // 
            // ViewQuanLyNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelDataGrid);
            this.Controls.Add(this.lblDemNhanVien);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelHeader);
            this.Name = "ViewQuanLyNhanVien";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(1200, 740);
            this.Load += new System.EventHandler(this.ViewQuanLyNhanVien_Load_1);
            this.panelHeader.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.ComboBox cboTimKiem;
        private System.Windows.Forms.Label lblTuKhoa;
        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnThemNhanVien;
        private System.Windows.Forms.Button btnSuaNhanVien;
        private System.Windows.Forms.Button btnXoaNhanVien;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Panel panelDataGrid;
        private System.Windows.Forms.Label lblDemNhanVien;
    }
}