namespace UI.UserControlUI
{
    partial class ViewDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.FlowLayoutPanel flpCards;
        private System.Windows.Forms.Panel card1;
        private System.Windows.Forms.Panel card2;
        private System.Windows.Forms.Panel card3;
        private System.Windows.Forms.Panel card4;
        private System.Windows.Forms.Label lblIcon1;
        private System.Windows.Forms.Label lblIcon2;
        private System.Windows.Forms.Label lblIcon3;
        private System.Windows.Forms.Label lblIcon4;
        private System.Windows.Forms.Label lblStat1;
        private System.Windows.Forms.Label lblStat2;
        private System.Windows.Forms.Label lblStat3;
        private System.Windows.Forms.Label lblStat4;
        private System.Windows.Forms.Label lblDesc1;
        private System.Windows.Forms.Label lblDesc2;
        private System.Windows.Forms.Label lblDesc3;
        private System.Windows.Forms.Label lblDesc4;

        private System.Windows.Forms.GroupBox grpXeMoiNhap;
        private System.Windows.Forms.Button btnXemTatCa;
        private System.Windows.Forms.DataGridView dgvXeMoiNhap;

        // --- THÊM MỚI: 3 Nút lọc ---
        private System.Windows.Forms.Button btnFilterAll;
        private System.Windows.Forms.Button btnFilterRent;
        private System.Windows.Forms.Button btnFilterSale;

        // SunnyUI icons
        private Sunny.UI.UISymbolLabel iconLabel1;
        private Sunny.UI.UISymbolLabel iconLabel2;
        private Sunny.UI.UISymbolLabel iconLabel3;
        private Sunny.UI.UISymbolLabel iconLabel4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeading = new System.Windows.Forms.Label();
            this.flpCards = new System.Windows.Forms.FlowLayoutPanel();
            this.card1 = new System.Windows.Forms.Panel();
            this.iconLabel1 = new Sunny.UI.UISymbolLabel();
            this.lblIcon1 = new System.Windows.Forms.Label();
            this.lblStat1 = new System.Windows.Forms.Label();
            this.lblDesc1 = new System.Windows.Forms.Label();
            this.card2 = new System.Windows.Forms.Panel();
            this.iconLabel2 = new Sunny.UI.UISymbolLabel();
            this.lblIcon2 = new System.Windows.Forms.Label();
            this.lblStat2 = new System.Windows.Forms.Label();
            this.lblDesc2 = new System.Windows.Forms.Label();
            this.card3 = new System.Windows.Forms.Panel();
            this.iconLabel3 = new Sunny.UI.UISymbolLabel();
            this.lblIcon3 = new System.Windows.Forms.Label();
            this.lblStat3 = new System.Windows.Forms.Label();
            this.lblDesc3 = new System.Windows.Forms.Label();
            this.card4 = new System.Windows.Forms.Panel();
            this.iconLabel4 = new Sunny.UI.UISymbolLabel();
            this.lblIcon4 = new System.Windows.Forms.Label();
            this.lblStat4 = new System.Windows.Forms.Label();
            this.lblDesc4 = new System.Windows.Forms.Label();
            this.grpXeMoiNhap = new System.Windows.Forms.GroupBox();
            this.btnXemTatCa = new System.Windows.Forms.Button();
            this.dgvXeMoiNhap = new System.Windows.Forms.DataGridView();
            this.btnFilterAll = new System.Windows.Forms.Button();
            this.btnFilterRent = new System.Windows.Forms.Button();
            this.btnFilterSale = new System.Windows.Forms.Button();
            this.flpCards.SuspendLayout();
            this.card1.SuspendLayout();
            this.card2.SuspendLayout();
            this.card3.SuspendLayout();
            this.card4.SuspendLayout();
            this.grpXeMoiNhap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXeMoiNhap)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblHeading.Location = new System.Drawing.Point(20, 18);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(209, 50);
            this.lblHeading.TabIndex = 0;
            this.lblHeading.Text = "Tổng quan";
            // 
            // flpCards
            // 
            this.flpCards.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpCards.BackColor = System.Drawing.Color.Transparent;
            this.flpCards.Controls.Add(this.card1);
            this.flpCards.Controls.Add(this.card2);
            this.flpCards.Controls.Add(this.card3);
            this.flpCards.Controls.Add(this.card4);
            this.flpCards.Location = new System.Drawing.Point(20, 80);
            this.flpCards.Margin = new System.Windows.Forms.Padding(0);
            this.flpCards.Name = "flpCards";
            this.flpCards.Size = new System.Drawing.Size(1483, 160);
            this.flpCards.TabIndex = 1;
            this.flpCards.WrapContents = false;
            // 
            // card1
            // 
            this.card1.BackColor = System.Drawing.Color.White;
            this.card1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.card1.Controls.Add(this.iconLabel1);
            this.card1.Controls.Add(this.lblIcon1);
            this.card1.Controls.Add(this.lblStat1);
            this.card1.Controls.Add(this.lblDesc1);
            this.card1.Location = new System.Drawing.Point(0, 0);
            this.card1.Margin = new System.Windows.Forms.Padding(0, 0, 25, 0);
            this.card1.Name = "card1";
            this.card1.Padding = new System.Windows.Forms.Padding(20);
            this.card1.Size = new System.Drawing.Size(320, 140);
            this.card1.TabIndex = 0;
            this.card1.Paint += new System.Windows.Forms.PaintEventHandler(this.card1_Paint);
            // 
            // iconLabel1
            // 
            this.iconLabel1.BackColor = System.Drawing.Color.Transparent;
            this.iconLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.iconLabel1.Location = new System.Drawing.Point(15, 20);
            this.iconLabel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.iconLabel1.Name = "iconLabel1";
            this.iconLabel1.Size = new System.Drawing.Size(60, 60);
            this.iconLabel1.Symbol = 61475;
            this.iconLabel1.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.iconLabel1.SymbolSize = 40;
            this.iconLabel1.TabIndex = 3;
            // 
            // lblIcon1
            // 
            this.lblIcon1.AutoSize = true;
            this.lblIcon1.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblIcon1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblIcon1.Location = new System.Drawing.Point(10, 10);
            this.lblIcon1.Name = "lblIcon1";
            this.lblIcon1.Size = new System.Drawing.Size(0, 62);
            this.lblIcon1.TabIndex = 0;
            this.lblIcon1.Visible = false;
            // 
            // lblStat1
            // 
            this.lblStat1.AutoSize = true;
            this.lblStat1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblStat1.Location = new System.Drawing.Point(90, 20);
            this.lblStat1.Name = "lblStat1";
            this.lblStat1.Size = new System.Drawing.Size(40, 46);
            this.lblStat1.TabIndex = 1;
            this.lblStat1.Text = "0";
            // 
            // lblDesc1
            // 
            this.lblDesc1.AutoSize = true;
            this.lblDesc1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDesc1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblDesc1.Location = new System.Drawing.Point(92, 70);
            this.lblDesc1.Name = "lblDesc1";
            this.lblDesc1.Size = new System.Drawing.Size(101, 23);
            this.lblDesc1.TabIndex = 2;
            this.lblDesc1.Text = "Xe sẵn sàng";
            // 
            // card2
            // 
            this.card2.BackColor = System.Drawing.Color.White;
            this.card2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.card2.Controls.Add(this.iconLabel2);
            this.card2.Controls.Add(this.lblIcon2);
            this.card2.Controls.Add(this.lblStat2);
            this.card2.Controls.Add(this.lblDesc2);
            this.card2.Location = new System.Drawing.Point(345, 0);
            this.card2.Margin = new System.Windows.Forms.Padding(0, 0, 25, 0);
            this.card2.Name = "card2";
            this.card2.Padding = new System.Windows.Forms.Padding(20);
            this.card2.Size = new System.Drawing.Size(320, 140);
            this.card2.TabIndex = 1;
            this.card2.Paint += new System.Windows.Forms.PaintEventHandler(this.card2_Paint);
            // 
            // iconLabel2
            // 
            this.iconLabel2.BackColor = System.Drawing.Color.Transparent;
            this.iconLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.iconLabel2.Location = new System.Drawing.Point(15, 20);
            this.iconLabel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.iconLabel2.Name = "iconLabel2";
            this.iconLabel2.Size = new System.Drawing.Size(60, 60);
            this.iconLabel2.Symbol = 61731;
            this.iconLabel2.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.iconLabel2.SymbolSize = 40;
            this.iconLabel2.TabIndex = 3;
            // 
            // lblIcon2
            // 
            this.lblIcon2.AutoSize = true;
            this.lblIcon2.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblIcon2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblIcon2.Location = new System.Drawing.Point(10, 10);
            this.lblIcon2.Name = "lblIcon2";
            this.lblIcon2.Size = new System.Drawing.Size(0, 62);
            this.lblIcon2.TabIndex = 0;
            this.lblIcon2.Visible = false;
            // 
            // lblStat2
            // 
            this.lblStat2.AutoSize = true;
            this.lblStat2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblStat2.Location = new System.Drawing.Point(90, 20);
            this.lblStat2.Name = "lblStat2";
            this.lblStat2.Size = new System.Drawing.Size(40, 46);
            this.lblStat2.TabIndex = 1;
            this.lblStat2.Text = "0";
            this.lblStat2.Click += new System.EventHandler(this.lblStat2_Click);
            // 
            // lblDesc2
            // 
            this.lblDesc2.AutoSize = true;
            this.lblDesc2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDesc2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblDesc2.Location = new System.Drawing.Point(92, 70);
            this.lblDesc2.Name = "lblDesc2";
            this.lblDesc2.Size = new System.Drawing.Size(188, 23);
            this.lblDesc2.TabIndex = 2;
            this.lblDesc2.Text = "Giao dịch bán hôm nay";
            this.lblDesc2.Click += new System.EventHandler(this.lblDesc2_Click);
            // 
            // card3
            // 
            this.card3.BackColor = System.Drawing.Color.White;
            this.card3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.card3.Controls.Add(this.iconLabel3);
            this.card3.Controls.Add(this.lblIcon3);
            this.card3.Controls.Add(this.lblStat3);
            this.card3.Controls.Add(this.lblDesc3);
            this.card3.Location = new System.Drawing.Point(690, 0);
            this.card3.Margin = new System.Windows.Forms.Padding(0, 0, 25, 0);
            this.card3.Name = "card3";
            this.card3.Padding = new System.Windows.Forms.Padding(20);
            this.card3.Size = new System.Drawing.Size(320, 140);
            this.card3.TabIndex = 2;
            this.card3.Paint += new System.Windows.Forms.PaintEventHandler(this.card3_Paint);
            // 
            // iconLabel3
            // 
            this.iconLabel3.BackColor = System.Drawing.Color.Transparent;
            this.iconLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.iconLabel3.Location = new System.Drawing.Point(15, 20);
            this.iconLabel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.iconLabel3.Name = "iconLabel3";
            this.iconLabel3.Size = new System.Drawing.Size(60, 60);
            this.iconLabel3.Symbol = 61463;
            this.iconLabel3.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(155)))), ((int)(((byte)(40)))));
            this.iconLabel3.SymbolSize = 40;
            this.iconLabel3.TabIndex = 3;
            // 
            // lblIcon3
            // 
            this.lblIcon3.AutoSize = true;
            this.lblIcon3.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblIcon3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.lblIcon3.Location = new System.Drawing.Point(10, 10);
            this.lblIcon3.Name = "lblIcon3";
            this.lblIcon3.Size = new System.Drawing.Size(0, 62);
            this.lblIcon3.TabIndex = 0;
            this.lblIcon3.Visible = false;
            // 
            // lblStat3
            // 
            this.lblStat3.AutoSize = true;
            this.lblStat3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblStat3.Location = new System.Drawing.Point(69, 24);
            this.lblStat3.Name = "lblStat3";
            this.lblStat3.Size = new System.Drawing.Size(40, 46);
            this.lblStat3.TabIndex = 1;
            this.lblStat3.Text = "0";
            this.lblStat3.Click += new System.EventHandler(this.lblStat3_Click);
            // 
            // lblDesc3
            // 
            this.lblDesc3.AutoSize = true;
            this.lblDesc3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDesc3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblDesc3.Location = new System.Drawing.Point(33, 83);
            this.lblDesc3.Name = "lblDesc3";
            this.lblDesc3.Size = new System.Drawing.Size(251, 23);
            this.lblDesc3.TabIndex = 2;
            this.lblDesc3.Text = "Giao dịch thuê đang hoạt động";
            this.lblDesc3.Click += new System.EventHandler(this.lblDesc3_Click);
            // 
            // card4
            // 
            this.card4.BackColor = System.Drawing.Color.White;
            this.card4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.card4.Controls.Add(this.iconLabel4);
            this.card4.Controls.Add(this.lblIcon4);
            this.card4.Controls.Add(this.lblStat4);
            this.card4.Controls.Add(this.lblDesc4);
            this.card4.Location = new System.Drawing.Point(1035, 0);
            this.card4.Margin = new System.Windows.Forms.Padding(0);
            this.card4.Name = "card4";
            this.card4.Padding = new System.Windows.Forms.Padding(20);
            this.card4.Size = new System.Drawing.Size(403, 140);
            this.card4.TabIndex = 3;
            this.card4.Paint += new System.Windows.Forms.PaintEventHandler(this.card4_Paint);
            // 
            // iconLabel4
            // 
            this.iconLabel4.BackColor = System.Drawing.Color.Transparent;
            this.iconLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.iconLabel4.Location = new System.Drawing.Point(15, 20);
            this.iconLabel4.MinimumSize = new System.Drawing.Size(1, 1);
            this.iconLabel4.Name = "iconLabel4";
            this.iconLabel4.Size = new System.Drawing.Size(60, 60);
            this.iconLabel4.Symbol = 61445;
            this.iconLabel4.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.iconLabel4.SymbolSize = 40;
            this.iconLabel4.TabIndex = 3;
            // 
            // lblIcon4
            // 
            this.lblIcon4.AutoSize = true;
            this.lblIcon4.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblIcon4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.lblIcon4.Location = new System.Drawing.Point(10, 10);
            this.lblIcon4.Name = "lblIcon4";
            this.lblIcon4.Size = new System.Drawing.Size(0, 62);
            this.lblIcon4.TabIndex = 0;
            this.lblIcon4.Visible = false;
            // 
            // lblStat4
            // 
            this.lblStat4.AutoSize = true;
            this.lblStat4.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblStat4.Location = new System.Drawing.Point(90, 20);
            this.lblStat4.Name = "lblStat4";
            this.lblStat4.Size = new System.Drawing.Size(62, 46);
            this.lblStat4.TabIndex = 1;
            this.lblStat4.Text = "0đ";
            // 
            // lblDesc4
            // 
            this.lblDesc4.AutoSize = true;
            this.lblDesc4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDesc4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblDesc4.Location = new System.Drawing.Point(92, 70);
            this.lblDesc4.Name = "lblDesc4";
            this.lblDesc4.Size = new System.Drawing.Size(174, 23);
            this.lblDesc4.TabIndex = 2;
            this.lblDesc4.Text = "Doanh thu tháng này";
            // 
            // grpXeMoiNhap
            // 
            this.grpXeMoiNhap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpXeMoiNhap.BackColor = System.Drawing.Color.Transparent;
            this.grpXeMoiNhap.Controls.Add(this.btnXemTatCa);
            this.grpXeMoiNhap.Controls.Add(this.dgvXeMoiNhap);
            this.grpXeMoiNhap.Controls.Add(this.btnFilterAll);
            this.grpXeMoiNhap.Controls.Add(this.btnFilterRent);
            this.grpXeMoiNhap.Controls.Add(this.btnFilterSale);
            this.grpXeMoiNhap.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpXeMoiNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.grpXeMoiNhap.Location = new System.Drawing.Point(20, 260);
            this.grpXeMoiNhap.Name = "grpXeMoiNhap";
            this.grpXeMoiNhap.Size = new System.Drawing.Size(1483, 560);
            this.grpXeMoiNhap.TabIndex = 2;
            this.grpXeMoiNhap.TabStop = false;
            this.grpXeMoiNhap.Text = "Danh Sách Xe";
            this.grpXeMoiNhap.Enter += new System.EventHandler(this.grpXeMoiNhap_Enter);
            // 
            // btnXemTatCa
            // 
            this.btnXemTatCa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXemTatCa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnXemTatCa.FlatAppearance.BorderSize = 0;
            this.btnXemTatCa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemTatCa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXemTatCa.ForeColor = System.Drawing.Color.White;
            this.btnXemTatCa.Location = new System.Drawing.Point(1316, 28);
            this.btnXemTatCa.Name = "btnXemTatCa";
            this.btnXemTatCa.Size = new System.Drawing.Size(120, 36);
            this.btnXemTatCa.TabIndex = 0;
            this.btnXemTatCa.Text = "Xem tất cả";
            this.btnXemTatCa.UseVisualStyleBackColor = false;
            this.btnXemTatCa.Click += new System.EventHandler(this.btnXemTatCa_Click);
            // 
            // dgvXeMoiNhap
            // 
            this.dgvXeMoiNhap.AllowUserToAddRows = false;
            this.dgvXeMoiNhap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvXeMoiNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvXeMoiNhap.BackgroundColor = System.Drawing.Color.White;
            this.dgvXeMoiNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvXeMoiNhap.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvXeMoiNhap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvXeMoiNhap.ColumnHeadersHeight = 38;
            this.dgvXeMoiNhap.EnableHeadersVisualStyles = false;
            this.dgvXeMoiNhap.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvXeMoiNhap.Location = new System.Drawing.Point(18, 75);
            this.dgvXeMoiNhap.Name = "dgvXeMoiNhap";
            this.dgvXeMoiNhap.ReadOnly = true;
            this.dgvXeMoiNhap.RowHeadersVisible = false;
            this.dgvXeMoiNhap.RowHeadersWidth = 51;
            this.dgvXeMoiNhap.RowTemplate.Height = 45;
            this.dgvXeMoiNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvXeMoiNhap.Size = new System.Drawing.Size(1420, 460);
            this.dgvXeMoiNhap.TabIndex = 1;
            this.dgvXeMoiNhap.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvXeMoiNhap_CellContentClick);
            // 
            // btnFilterAll
            // 
            this.btnFilterAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnFilterAll.FlatAppearance.BorderSize = 0;
            this.btnFilterAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilterAll.ForeColor = System.Drawing.Color.White;
            this.btnFilterAll.Location = new System.Drawing.Point(18, 30);
            this.btnFilterAll.Name = "btnFilterAll";
            this.btnFilterAll.Size = new System.Drawing.Size(100, 32);
            this.btnFilterAll.TabIndex = 2;
            this.btnFilterAll.Text = "Tất cả";
            this.btnFilterAll.UseVisualStyleBackColor = false;
            this.btnFilterAll.Click += new System.EventHandler(this.btnFilterAll_Click);
            // 
            // btnFilterRent
            // 
            this.btnFilterRent.BackColor = System.Drawing.Color.White;
            this.btnFilterRent.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnFilterRent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterRent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilterRent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFilterRent.Location = new System.Drawing.Point(130, 30);
            this.btnFilterRent.Name = "btnFilterRent";
            this.btnFilterRent.Size = new System.Drawing.Size(100, 32);
            this.btnFilterRent.TabIndex = 3;
            this.btnFilterRent.Text = "Xe Thuê";
            this.btnFilterRent.UseVisualStyleBackColor = false;
            this.btnFilterRent.Click += new System.EventHandler(this.btnFilterRent_Click);
            // 
            // btnFilterSale
            // 
            this.btnFilterSale.BackColor = System.Drawing.Color.White;
            this.btnFilterSale.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnFilterSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterSale.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilterSale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFilterSale.Location = new System.Drawing.Point(242, 30);
            this.btnFilterSale.Name = "btnFilterSale";
            this.btnFilterSale.Size = new System.Drawing.Size(100, 32);
            this.btnFilterSale.TabIndex = 4;
            this.btnFilterSale.Text = "Xe Bán";
            this.btnFilterSale.UseVisualStyleBackColor = false;
            this.btnFilterSale.Click += new System.EventHandler(this.btnFilterSale_Click);
            // 
            // ViewDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.flpCards);
            this.Controls.Add(this.grpXeMoiNhap);
            this.Name = "ViewDashboard";
            this.Size = new System.Drawing.Size(1523, 890);
            this.Load += new System.EventHandler(this.ViewDashboard_Load);
            this.flpCards.ResumeLayout(false);
            this.card1.ResumeLayout(false);
            this.card1.PerformLayout();
            this.card2.ResumeLayout(false);
            this.card2.PerformLayout();
            this.card3.ResumeLayout(false);
            this.card3.PerformLayout();
            this.card4.ResumeLayout(false);
            this.card4.PerformLayout();
            this.grpXeMoiNhap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvXeMoiNhap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}