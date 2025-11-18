namespace UI.UserControlUI
{
partial class ViewQuanLyKhachHang
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cboSearchBy = new System.Windows.Forms.ComboBox();
            this.lblSearchBy = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btn_RefreshCustomer = new System.Windows.Forms.Button();
            this.btn_DeleteCustomer = new System.Windows.Forms.Button();
            this.btn_EditCustomer = new System.Windows.Forms.Button();
            this.btn_AddCustomer = new System.Windows.Forms.Button();
            this.panelDataGrid = new System.Windows.Forms.Panel();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(20, 20);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.panelHeader.Size = new System.Drawing.Size(1160, 70);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1120, 70);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ KHÁCH HÀNG";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.White;
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.txtSearch);
            this.panelSearch.Controls.Add(this.lblSearch);
            this.panelSearch.Controls.Add(this.cboSearchBy);
            this.panelSearch.Controls.Add(this.lblSearchBy);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(20, 90);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelSearch.Size = new System.Drawing.Size(1160, 80);
            this.panelSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(744, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 35);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(438, 24);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 30);
            this.txtSearch.TabIndex = 3;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearch.Location = new System.Drawing.Point(357, 28);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(75, 23);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Từ khóa:";
            // 
            // cboSearchBy
            // 
            this.cboSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearchBy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboSearchBy.FormattingEnabled = true;
            this.cboSearchBy.Items.AddRange(new object[] {
            "Mã khách hàng",
            "Họ và tên",
            "Số điện thoại",
            "Email"});
            this.cboSearchBy.Location = new System.Drawing.Point(149, 24);
            this.cboSearchBy.Name = "cboSearchBy";
            this.cboSearchBy.Size = new System.Drawing.Size(180, 31);
            this.cboSearchBy.TabIndex = 1;
            // 
            // lblSearchBy
            // 
            this.lblSearchBy.AutoSize = true;
            this.lblSearchBy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearchBy.Location = new System.Drawing.Point(20, 28);
            this.lblSearchBy.Name = "lblSearchBy";
            this.lblSearchBy.Size = new System.Drawing.Size(123, 23);
            this.lblSearchBy.TabIndex = 0;
            this.lblSearchBy.Text = "Tìm kiếm theo:";
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.White;
            this.panelActions.Controls.Add(this.btn_RefreshCustomer);
            this.panelActions.Controls.Add(this.btn_DeleteCustomer);
            this.panelActions.Controls.Add(this.btn_EditCustomer);
            this.panelActions.Controls.Add(this.btn_AddCustomer);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(20, 170);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelActions.Size = new System.Drawing.Size(1160, 70);
            this.panelActions.TabIndex = 2;
            // 
            // btn_RefreshCustomer
            // 
            this.btn_RefreshCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btn_RefreshCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_RefreshCustomer.FlatAppearance.BorderSize = 0;
            this.btn_RefreshCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RefreshCustomer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_RefreshCustomer.ForeColor = System.Drawing.Color.White;
            this.btn_RefreshCustomer.Location = new System.Drawing.Point(500, 13);
            this.btn_RefreshCustomer.Name = "btn_RefreshCustomer";
            this.btn_RefreshCustomer.Size = new System.Drawing.Size(140, 45);
            this.btn_RefreshCustomer.TabIndex = 3;
            this.btn_RefreshCustomer.Text = "Làm mới";
            this.btn_RefreshCustomer.UseVisualStyleBackColor = false;
            // 
            // btn_DeleteCustomer
            // 
            this.btn_DeleteCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btn_DeleteCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DeleteCustomer.FlatAppearance.BorderSize = 0;
            this.btn_DeleteCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DeleteCustomer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_DeleteCustomer.ForeColor = System.Drawing.Color.White;
            this.btn_DeleteCustomer.Location = new System.Drawing.Point(340, 13);
            this.btn_DeleteCustomer.Name = "btn_DeleteCustomer";
            this.btn_DeleteCustomer.Size = new System.Drawing.Size(140, 45);
            this.btn_DeleteCustomer.TabIndex = 2;
            this.btn_DeleteCustomer.Text = "Xóa";
            this.btn_DeleteCustomer.UseVisualStyleBackColor = false;
            // 
            // btn_EditCustomer
            // 
            this.btn_EditCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btn_EditCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_EditCustomer.FlatAppearance.BorderSize = 0;
            this.btn_EditCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_EditCustomer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_EditCustomer.ForeColor = System.Drawing.Color.White;
            this.btn_EditCustomer.Location = new System.Drawing.Point(180, 13);
            this.btn_EditCustomer.Name = "btn_EditCustomer";
            this.btn_EditCustomer.Size = new System.Drawing.Size(140, 45);
            this.btn_EditCustomer.TabIndex = 1;
            this.btn_EditCustomer.Text = "Sửa";
            this.btn_EditCustomer.UseVisualStyleBackColor = false;
            // 
            // btn_AddCustomer
            // 
            this.btn_AddCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btn_AddCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AddCustomer.FlatAppearance.BorderSize = 0;
            this.btn_AddCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddCustomer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_AddCustomer.ForeColor = System.Drawing.Color.White;
            this.btn_AddCustomer.Location = new System.Drawing.Point(20, 13);
            this.btn_AddCustomer.Name = "btn_AddCustomer";
            this.btn_AddCustomer.Size = new System.Drawing.Size(140, 45);
            this.btn_AddCustomer.TabIndex = 0;
            this.btn_AddCustomer.Text = "Thêm mới";
            this.btn_AddCustomer.UseVisualStyleBackColor = false;
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
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.BackColor = System.Drawing.Color.White;
            this.lblRecordCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRecordCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblRecordCount.Location = new System.Drawing.Point(20, 680);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.lblRecordCount.Size = new System.Drawing.Size(1160, 40);
            this.lblRecordCount.TabIndex = 4;
            this.lblRecordCount.Text = "Tổng số bản ghi: 0";
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewQuanLyKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelDataGrid);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelHeader);
            this.Name = "ViewQuanLyKhachHang";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(1200, 740);
            this.panelHeader.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

 #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblSearchBy;
    private System.Windows.Forms.ComboBox cboSearchBy;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
      private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panelActions;
      private System.Windows.Forms.Button btn_AddCustomer;
      private System.Windows.Forms.Button btn_EditCustomer;
        private System.Windows.Forms.Button btn_DeleteCustomer;
 private System.Windows.Forms.Button btn_RefreshCustomer;
    private System.Windows.Forms.Panel panelDataGrid;
      private System.Windows.Forms.Label lblRecordCount;
    }
}
