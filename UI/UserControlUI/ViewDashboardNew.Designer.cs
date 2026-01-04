namespace UI.UserControlUI
{
    partial class ViewDashboardNew
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                // Cleanup timer
                if (refreshTimer != null)
                {
                    refreshTimer.Stop();
                    refreshTimer.Dispose();
                }
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.pnlXeSanSang = new System.Windows.Forms.Panel();
            this.lblXeSanSangTitle = new System.Windows.Forms.Label();
            this.lblXeSanSangValue = new System.Windows.Forms.Label();
            this.lblXeSanSangIcon = new System.Windows.Forms.Label();
            this.pnlDoanhThu = new System.Windows.Forms.Panel();
            this.lblDoanhThuTitle = new System.Windows.Forms.Label();
            this.lblDoanhThuValue = new System.Windows.Forms.Label();
            this.lblDoanhThuIcon = new System.Windows.Forms.Label();
            this.pnlKhachHang = new System.Windows.Forms.Panel();
            this.lblKhachHangTitle = new System.Windows.Forms.Label();
            this.lblKhachHangValue = new System.Windows.Forms.Label();
            this.lblKhachHangIcon = new System.Windows.Forms.Label();
            this.pnlGiaoDich = new System.Windows.Forms.Panel();
            this.lblGiaoDichTitle = new System.Windows.Forms.Label();
            this.lblGiaoDichValue = new System.Windows.Forms.Label();
            this.lblGiaoDichIcon = new System.Windows.Forms.Label();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTop5Xe = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlHoatDongGanDay = new System.Windows.Forms.Panel();
            this.dgvHoatDongGanDay = new System.Windows.Forms.DataGridView();
            this.lblTitleHoatDong = new System.Windows.Forms.Label();
            this.pnlCanhBaoTonKho = new System.Windows.Forms.Panel();
            this.dgvCanhBaoTonKho = new System.Windows.Forms.DataGridView();
            this.lblTitleCanhBao = new System.Windows.Forms.Label();
            this.pnlXeSanSang.SuspendLayout();
            this.pnlDoanhThu.SuspendLayout();
            this.pnlKhachHang.SuspendLayout();
            this.pnlGiaoDich.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTop5Xe)).BeginInit();
            this.pnlHoatDongGanDay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoatDongGanDay)).BeginInit();
            this.pnlCanhBaoTonKho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBaoTonKho)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(470, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "DASHBOARD - TỔNG QUAN";
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDateTime.ForeColor = System.Drawing.Color.Gray;
            this.lblDateTime.Location = new System.Drawing.Point(30, 65);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(158, 23);
            this.lblDateTime.TabIndex = 1;
            this.lblDateTime.Text = "Thứ Ba, 24/12/2025";
            // 
            // pnlXeSanSang
            // 
            this.pnlXeSanSang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.pnlXeSanSang.Controls.Add(this.lblXeSanSangTitle);
            this.pnlXeSanSang.Controls.Add(this.lblXeSanSangValue);
            this.pnlXeSanSang.Controls.Add(this.lblXeSanSangIcon);
            this.pnlXeSanSang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlXeSanSang.Location = new System.Drawing.Point(20, 110);
            this.pnlXeSanSang.Name = "pnlXeSanSang";
            this.pnlXeSanSang.Size = new System.Drawing.Size(280, 130);
            this.pnlXeSanSang.TabIndex = 2;
            // 
            // lblXeSanSangTitle
            // 
            this.lblXeSanSangTitle.AutoSize = true;
            this.lblXeSanSangTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblXeSanSangTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblXeSanSangTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblXeSanSangTitle.Location = new System.Drawing.Point(90, 60);
            this.lblXeSanSangTitle.Name = "lblXeSanSangTitle";
            this.lblXeSanSangTitle.Size = new System.Drawing.Size(120, 20);
            this.lblXeSanSangTitle.TabIndex = 2;
            this.lblXeSanSangTitle.Text = "Xe Sẵn Sàng Bán";
            // 
            // lblXeSanSangValue
            // 
            this.lblXeSanSangValue.AutoSize = true;
            this.lblXeSanSangValue.BackColor = System.Drawing.Color.Transparent;
            this.lblXeSanSangValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblXeSanSangValue.ForeColor = System.Drawing.Color.White;
            this.lblXeSanSangValue.Location = new System.Drawing.Point(90, 20);
            this.lblXeSanSangValue.Name = "lblXeSanSangValue";
            this.lblXeSanSangValue.Size = new System.Drawing.Size(40, 46);
            this.lblXeSanSangValue.TabIndex = 1;
            this.lblXeSanSangValue.Text = "0";
            // 
            // lblXeSanSangIcon
            // 
            this.lblXeSanSangIcon.AutoSize = true;
            this.lblXeSanSangIcon.BackColor = System.Drawing.Color.Transparent;
            this.lblXeSanSangIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 24F);
            this.lblXeSanSangIcon.ForeColor = System.Drawing.Color.White;
            this.lblXeSanSangIcon.Location = new System.Drawing.Point(15, 20);
            this.lblXeSanSangIcon.Name = "lblXeSanSangIcon";
            this.lblXeSanSangIcon.Size = new System.Drawing.Size(78, 53);
            this.lblXeSanSangIcon.TabIndex = 0;
            this.lblXeSanSangIcon.Text = "📦";
            // 
            // pnlDoanhThu
            // 
            this.pnlDoanhThu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.pnlDoanhThu.Controls.Add(this.lblDoanhThuTitle);
            this.pnlDoanhThu.Controls.Add(this.lblDoanhThuValue);
            this.pnlDoanhThu.Controls.Add(this.lblDoanhThuIcon);
            this.pnlDoanhThu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlDoanhThu.Location = new System.Drawing.Point(315, 110);
            this.pnlDoanhThu.Name = "pnlDoanhThu";
            this.pnlDoanhThu.Size = new System.Drawing.Size(280, 130);
            this.pnlDoanhThu.TabIndex = 3;
            // 
            // lblDoanhThuTitle
            // 
            this.lblDoanhThuTitle.AutoSize = true;
            this.lblDoanhThuTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblDoanhThuTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDoanhThuTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblDoanhThuTitle.Location = new System.Drawing.Point(90, 60);
            this.lblDoanhThuTitle.Name = "lblDoanhThuTitle";
            this.lblDoanhThuTitle.Size = new System.Drawing.Size(156, 20);
            this.lblDoanhThuTitle.TabIndex = 2;
            this.lblDoanhThuTitle.Text = "Doanh Thu Tháng Này";
            // 
            // lblDoanhThuValue
            // 
            this.lblDoanhThuValue.AutoSize = true;
            this.lblDoanhThuValue.BackColor = System.Drawing.Color.Transparent;
            this.lblDoanhThuValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblDoanhThuValue.ForeColor = System.Drawing.Color.White;
            this.lblDoanhThuValue.Location = new System.Drawing.Point(90, 20);
            this.lblDoanhThuValue.Name = "lblDoanhThuValue";
            this.lblDoanhThuValue.Size = new System.Drawing.Size(40, 46);
            this.lblDoanhThuValue.TabIndex = 1;
            this.lblDoanhThuValue.Text = "0";
            // 
            // lblDoanhThuIcon
            // 
            this.lblDoanhThuIcon.AutoSize = true;
            this.lblDoanhThuIcon.BackColor = System.Drawing.Color.Transparent;
            this.lblDoanhThuIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 24F);
            this.lblDoanhThuIcon.ForeColor = System.Drawing.Color.White;
            this.lblDoanhThuIcon.Location = new System.Drawing.Point(15, 20);
            this.lblDoanhThuIcon.Name = "lblDoanhThuIcon";
            this.lblDoanhThuIcon.Size = new System.Drawing.Size(78, 53);
            this.lblDoanhThuIcon.TabIndex = 0;
            this.lblDoanhThuIcon.Text = "💰";
            // 
            // pnlKhachHang
            // 
            this.pnlKhachHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.pnlKhachHang.Controls.Add(this.lblKhachHangTitle);
            this.pnlKhachHang.Controls.Add(this.lblKhachHangValue);
            this.pnlKhachHang.Controls.Add(this.lblKhachHangIcon);
            this.pnlKhachHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlKhachHang.Location = new System.Drawing.Point(610, 110);
            this.pnlKhachHang.Name = "pnlKhachHang";
            this.pnlKhachHang.Size = new System.Drawing.Size(280, 130);
            this.pnlKhachHang.TabIndex = 4;
            // 
            // lblKhachHangTitle
            // 
            this.lblKhachHangTitle.AutoSize = true;
            this.lblKhachHangTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblKhachHangTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKhachHangTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblKhachHangTitle.Location = new System.Drawing.Point(90, 60);
            this.lblKhachHangTitle.Name = "lblKhachHangTitle";
            this.lblKhachHangTitle.Size = new System.Drawing.Size(119, 20);
            this.lblKhachHangTitle.TabIndex = 2;
            this.lblKhachHangTitle.Text = "Khách Hàng Mới";
            // 
            // lblKhachHangValue
            // 
            this.lblKhachHangValue.AutoSize = true;
            this.lblKhachHangValue.BackColor = System.Drawing.Color.Transparent;
            this.lblKhachHangValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblKhachHangValue.ForeColor = System.Drawing.Color.White;
            this.lblKhachHangValue.Location = new System.Drawing.Point(90, 20);
            this.lblKhachHangValue.Name = "lblKhachHangValue";
            this.lblKhachHangValue.Size = new System.Drawing.Size(40, 46);
            this.lblKhachHangValue.TabIndex = 1;
            this.lblKhachHangValue.Text = "0";
            // 
            // lblKhachHangIcon
            // 
            this.lblKhachHangIcon.AutoSize = true;
            this.lblKhachHangIcon.BackColor = System.Drawing.Color.Transparent;
            this.lblKhachHangIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 24F);
            this.lblKhachHangIcon.ForeColor = System.Drawing.Color.White;
            this.lblKhachHangIcon.Location = new System.Drawing.Point(15, 20);
            this.lblKhachHangIcon.Name = "lblKhachHangIcon";
            this.lblKhachHangIcon.Size = new System.Drawing.Size(78, 53);
            this.lblKhachHangIcon.TabIndex = 0;
            this.lblKhachHangIcon.Text = "👥";
            // 
            // pnlGiaoDich
            // 
            this.pnlGiaoDich.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.pnlGiaoDich.Controls.Add(this.lblGiaoDichTitle);
            this.pnlGiaoDich.Controls.Add(this.lblGiaoDichValue);
            this.pnlGiaoDich.Controls.Add(this.lblGiaoDichIcon);
            this.pnlGiaoDich.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlGiaoDich.Location = new System.Drawing.Point(905, 110);
            this.pnlGiaoDich.Name = "pnlGiaoDich";
            this.pnlGiaoDich.Size = new System.Drawing.Size(280, 130);
            this.pnlGiaoDich.TabIndex = 5;
            // 
            // lblGiaoDichTitle
            // 
            this.lblGiaoDichTitle.AutoSize = true;
            this.lblGiaoDichTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblGiaoDichTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGiaoDichTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblGiaoDichTitle.Location = new System.Drawing.Point(90, 60);
            this.lblGiaoDichTitle.Name = "lblGiaoDichTitle";
            this.lblGiaoDichTitle.Size = new System.Drawing.Size(141, 20);
            this.lblGiaoDichTitle.TabIndex = 2;
            this.lblGiaoDichTitle.Text = "Giao Dịch Hôm Nay";
            // 
            // lblGiaoDichValue
            // 
            this.lblGiaoDichValue.AutoSize = true;
            this.lblGiaoDichValue.BackColor = System.Drawing.Color.Transparent;
            this.lblGiaoDichValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblGiaoDichValue.ForeColor = System.Drawing.Color.White;
            this.lblGiaoDichValue.Location = new System.Drawing.Point(90, 20);
            this.lblGiaoDichValue.Name = "lblGiaoDichValue";
            this.lblGiaoDichValue.Size = new System.Drawing.Size(40, 46);
            this.lblGiaoDichValue.TabIndex = 1;
            this.lblGiaoDichValue.Text = "0";
            // 
            // lblGiaoDichIcon
            // 
            this.lblGiaoDichIcon.AutoSize = true;
            this.lblGiaoDichIcon.BackColor = System.Drawing.Color.Transparent;
            this.lblGiaoDichIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 24F);
            this.lblGiaoDichIcon.ForeColor = System.Drawing.Color.White;
            this.lblGiaoDichIcon.Location = new System.Drawing.Point(15, 20);
            this.lblGiaoDichIcon.Name = "lblGiaoDichIcon";
            this.lblGiaoDichIcon.Size = new System.Drawing.Size(78, 53);
            this.lblGiaoDichIcon.TabIndex = 0;
            this.lblGiaoDichIcon.Text = "📊";
            // 
            // chartDoanhThu
            // 
            this.chartDoanhThu.BorderlineColor = System.Drawing.Color.LightGray;
            this.chartDoanhThu.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend1);
            this.chartDoanhThu.Location = new System.Drawing.Point(20, 260);
            this.chartDoanhThu.Name = "chartDoanhThu";
            this.chartDoanhThu.Size = new System.Drawing.Size(580, 320);
            this.chartDoanhThu.TabIndex = 6;
            this.chartDoanhThu.Text = "chartDoanhThu";
            // 
            // chartTop5Xe
            // 
            this.chartTop5Xe.BorderlineColor = System.Drawing.Color.LightGray;
            this.chartTop5Xe.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.Name = "ChartArea1";
            this.chartTop5Xe.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartTop5Xe.Legends.Add(legend2);
            this.chartTop5Xe.Location = new System.Drawing.Point(610, 260);
            this.chartTop5Xe.Name = "chartTop5Xe";
            this.chartTop5Xe.Size = new System.Drawing.Size(575, 320);
            this.chartTop5Xe.TabIndex = 7;
            this.chartTop5Xe.Text = "chartTop5Xe";
            // 
            // pnlHoatDongGanDay
            // 
            this.pnlHoatDongGanDay.BackColor = System.Drawing.Color.White;
            this.pnlHoatDongGanDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHoatDongGanDay.Controls.Add(this.dgvHoatDongGanDay);
            this.pnlHoatDongGanDay.Controls.Add(this.lblTitleHoatDong);
            this.pnlHoatDongGanDay.Location = new System.Drawing.Point(20, 600);
            this.pnlHoatDongGanDay.Name = "pnlHoatDongGanDay";
            this.pnlHoatDongGanDay.Size = new System.Drawing.Size(580, 260);
            this.pnlHoatDongGanDay.TabIndex = 8;
            // 
            // dgvHoatDongGanDay
            // 
            this.dgvHoatDongGanDay.AllowUserToAddRows = false;
            this.dgvHoatDongGanDay.AllowUserToDeleteRows = false;
            this.dgvHoatDongGanDay.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoatDongGanDay.BackgroundColor = System.Drawing.Color.White;
            this.dgvHoatDongGanDay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHoatDongGanDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoatDongGanDay.Location = new System.Drawing.Point(10, 45);
            this.dgvHoatDongGanDay.MultiSelect = false;
            this.dgvHoatDongGanDay.Name = "dgvHoatDongGanDay";
            this.dgvHoatDongGanDay.ReadOnly = true;
            this.dgvHoatDongGanDay.RowHeadersVisible = false;
            this.dgvHoatDongGanDay.RowHeadersWidth = 51;
            this.dgvHoatDongGanDay.RowTemplate.Height = 24;
            this.dgvHoatDongGanDay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoatDongGanDay.Size = new System.Drawing.Size(560, 205);
            this.dgvHoatDongGanDay.TabIndex = 1;
            // 
            // lblTitleHoatDong
            // 
            this.lblTitleHoatDong.AutoSize = true;
            this.lblTitleHoatDong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitleHoatDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblTitleHoatDong.Location = new System.Drawing.Point(10, 10);
            this.lblTitleHoatDong.Name = "lblTitleHoatDong";
            this.lblTitleHoatDong.Size = new System.Drawing.Size(201, 28);
            this.lblTitleHoatDong.TabIndex = 0;
            this.lblTitleHoatDong.Text = "Hoạt Động Gần Đây";
            // 
            // pnlCanhBaoTonKho
            // 
            this.pnlCanhBaoTonKho.BackColor = System.Drawing.Color.White;
            this.pnlCanhBaoTonKho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCanhBaoTonKho.Controls.Add(this.dgvCanhBaoTonKho);
            this.pnlCanhBaoTonKho.Controls.Add(this.lblTitleCanhBao);
            this.pnlCanhBaoTonKho.Location = new System.Drawing.Point(610, 600);
            this.pnlCanhBaoTonKho.Name = "pnlCanhBaoTonKho";
            this.pnlCanhBaoTonKho.Size = new System.Drawing.Size(575, 260);
            this.pnlCanhBaoTonKho.TabIndex = 9;
            // 
            // dgvCanhBaoTonKho
            // 
            this.dgvCanhBaoTonKho.AllowUserToAddRows = false;
            this.dgvCanhBaoTonKho.AllowUserToDeleteRows = false;
            this.dgvCanhBaoTonKho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCanhBaoTonKho.BackgroundColor = System.Drawing.Color.White;
            this.dgvCanhBaoTonKho.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCanhBaoTonKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCanhBaoTonKho.Location = new System.Drawing.Point(10, 45);
            this.dgvCanhBaoTonKho.MultiSelect = false;
            this.dgvCanhBaoTonKho.Name = "dgvCanhBaoTonKho";
            this.dgvCanhBaoTonKho.ReadOnly = true;
            this.dgvCanhBaoTonKho.RowHeadersVisible = false;
            this.dgvCanhBaoTonKho.RowHeadersWidth = 51;
            this.dgvCanhBaoTonKho.RowTemplate.Height = 24;
            this.dgvCanhBaoTonKho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCanhBaoTonKho.Size = new System.Drawing.Size(555, 205);
            this.dgvCanhBaoTonKho.TabIndex = 1;
            // 
            // lblTitleCanhBao
            // 
            this.lblTitleCanhBao.AutoSize = true;
            this.lblTitleCanhBao.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitleCanhBao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblTitleCanhBao.Location = new System.Drawing.Point(10, 10);
            this.lblTitleCanhBao.Name = "lblTitleCanhBao";
            this.lblTitleCanhBao.Size = new System.Drawing.Size(239, 28);
            this.lblTitleCanhBao.TabIndex = 0;
            this.lblTitleCanhBao.Text = "Cảnh Báo Tồn Kho Thấp";
            // 
            // ViewDashboardNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.pnlCanhBaoTonKho);
            this.Controls.Add(this.pnlHoatDongGanDay);
            this.Controls.Add(this.chartTop5Xe);
            this.Controls.Add(this.chartDoanhThu);
            this.Controls.Add(this.pnlGiaoDich);
            this.Controls.Add(this.pnlKhachHang);
            this.Controls.Add(this.pnlDoanhThu);
            this.Controls.Add(this.pnlXeSanSang);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.lblTitle);
            this.Name = "ViewDashboardNew";
            this.Size = new System.Drawing.Size(1529, 921);
            this.Load += new System.EventHandler(this.ViewDashboardNew_Load);
            this.pnlXeSanSang.ResumeLayout(false);
            this.pnlXeSanSang.PerformLayout();
            this.pnlDoanhThu.ResumeLayout(false);
            this.pnlDoanhThu.PerformLayout();
            this.pnlKhachHang.ResumeLayout(false);
            this.pnlKhachHang.PerformLayout();
            this.pnlGiaoDich.ResumeLayout(false);
            this.pnlGiaoDich.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTop5Xe)).EndInit();
            this.pnlHoatDongGanDay.ResumeLayout(false);
            this.pnlHoatDongGanDay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoatDongGanDay)).EndInit();
            this.pnlCanhBaoTonKho.ResumeLayout(false);
            this.pnlCanhBaoTonKho.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBaoTonKho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Panel pnlXeSanSang;
        private System.Windows.Forms.Label lblXeSanSangIcon;
        private System.Windows.Forms.Label lblXeSanSangValue;
        private System.Windows.Forms.Label lblXeSanSangTitle;
        private System.Windows.Forms.Panel pnlDoanhThu;
        private System.Windows.Forms.Label lblDoanhThuTitle;
        private System.Windows.Forms.Label lblDoanhThuValue;
        private System.Windows.Forms.Label lblDoanhThuIcon;
        private System.Windows.Forms.Panel pnlKhachHang;
        private System.Windows.Forms.Label lblKhachHangTitle;
        private System.Windows.Forms.Label lblKhachHangValue;
        private System.Windows.Forms.Label lblKhachHangIcon;
        private System.Windows.Forms.Panel pnlGiaoDich;
        private System.Windows.Forms.Label lblGiaoDichTitle;
        private System.Windows.Forms.Label lblGiaoDichValue;
        private System.Windows.Forms.Label lblGiaoDichIcon;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTop5Xe;
        private System.Windows.Forms.Panel pnlHoatDongGanDay;
        private System.Windows.Forms.DataGridView dgvHoatDongGanDay;
        private System.Windows.Forms.Label lblTitleHoatDong;
        private System.Windows.Forms.Panel pnlCanhBaoTonKho;
        private System.Windows.Forms.DataGridView dgvCanhBaoTonKho;
        private System.Windows.Forms.Label lblTitleCanhBao;
    }
}
