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
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cboSearchBy = new System.Windows.Forms.ComboBox();
            this.lblSearchBy = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btn_RefreshEmployee = new System.Windows.Forms.Button();
            this.btn_DeleteEmployee = new System.Windows.Forms.Button();
            this.btn_EditEmployee = new System.Windows.Forms.Button();
            this.btn_AddEmployee = new System.Windows.Forms.Button();
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
            this.lblTitle.Text = "QUẢN LÝ NHÂN VIÊN";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
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
            this.btnSearch.Text = "🔍 Tìm";
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
            "Mã nhân viên",
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
            this.panelActions.Controls.Add(this.btn_RefreshEmployee);
            this.panelActions.Controls.Add(this.btn_DeleteEmployee);
            this.panelActions.Controls.Add(this.btn_EditEmployee);
            this.panelActions.Controls.Add(this.btn_AddEmployee);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(20, 170);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelActions.Size = new System.Drawing.Size(1160, 70);
            this.panelActions.TabIndex = 2;
            // 
            // btn_RefreshEmployee
            // 
            this.btn_RefreshEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btn_RefreshEmployee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_RefreshEmployee.FlatAppearance.BorderSize = 0;
            this.btn_RefreshEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RefreshEmployee.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_RefreshEmployee.ForeColor = System.Drawing.Color.White;
            this.btn_RefreshEmployee.Location = new System.Drawing.Point(500, 13);
            this.btn_RefreshEmployee.Name = "btn_RefreshEmployee";
            this.btn_RefreshEmployee.Size = new System.Drawing.Size(140, 45);
            this.btn_RefreshEmployee.TabIndex = 3;
            this.btn_RefreshEmployee.Text = "🔄 Làm mới";
            this.btn_RefreshEmployee.UseVisualStyleBackColor = false;
            // 
            // btn_DeleteEmployee
            // 
            this.btn_DeleteEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btn_DeleteEmployee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DeleteEmployee.FlatAppearance.BorderSize = 0;
            this.btn_DeleteEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DeleteEmployee.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_DeleteEmployee.ForeColor = System.Drawing.Color.White;
            this.btn_DeleteEmployee.Location = new System.Drawing.Point(340, 13);
            this.btn_DeleteEmployee.Name = "btn_DeleteEmployee";
            this.btn_DeleteEmployee.Size = new System.Drawing.Size(140, 45);
            this.btn_DeleteEmployee.TabIndex = 2;
            this.btn_DeleteEmployee.Text = "🗑️ Xóa";
            this.btn_DeleteEmployee.UseVisualStyleBackColor = false;
            // 
            // btn_EditEmployee
            // 
            this.btn_EditEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btn_EditEmployee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_EditEmployee.FlatAppearance.BorderSize = 0;
            this.btn_EditEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_EditEmployee.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_EditEmployee.ForeColor = System.Drawing.Color.White;
            this.btn_EditEmployee.Location = new System.Drawing.Point(180, 13);
            this.btn_EditEmployee.Name = "btn_EditEmployee";
            this.btn_EditEmployee.Size = new System.Drawing.Size(140, 45);
            this.btn_EditEmployee.TabIndex = 1;
            this.btn_EditEmployee.Text = "✏️ Sửa";
            this.btn_EditEmployee.UseVisualStyleBackColor = false;
            // 
            // btn_AddEmployee
            // 
            this.btn_AddEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btn_AddEmployee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AddEmployee.FlatAppearance.BorderSize = 0;
            this.btn_AddEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddEmployee.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_AddEmployee.ForeColor = System.Drawing.Color.White;
            this.btn_AddEmployee.Location = new System.Drawing.Point(20, 13);
            this.btn_AddEmployee.Name = "btn_AddEmployee";
            this.btn_AddEmployee.Size = new System.Drawing.Size(140, 45);
            this.btn_AddEmployee.TabIndex = 0;
            this.btn_AddEmployee.Text = "+ Thêm mới";
            this.btn_AddEmployee.UseVisualStyleBackColor = false;
            this.btn_AddEmployee.Click += new System.EventHandler(this.btn_AddEmployee_Click_1);
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
            // ViewQuanLyNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelDataGrid);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelHeader);
            this.Name = "ViewQuanLyNhanVien";
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
        private System.Windows.Forms.Button btn_AddEmployee;
        private System.Windows.Forms.Button btn_EditEmployee;
        private System.Windows.Forms.Button btn_DeleteEmployee;
        private System.Windows.Forms.Button btn_RefreshEmployee;
        private System.Windows.Forms.Panel panelDataGrid;
        private System.Windows.Forms.Label lblRecordCount;
    }
}