using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BLL;
using DTO;
using UI.FormUI;

namespace UI.UserControlUI
{
    public partial class ViewQuanLyBanHang : UserControl
    {
        private XeMayBLL xeMayBLL;
        private FlowLayoutPanel flowPanelCards;
        private string currentMaTaiKhoan;
        private LanguageManagerBLL langMgr = LanguageManagerBLL.Instance;
        public ViewQuanLyBanHang(string maTaiKhoan)
        {
            InitializeComponent();
            xeMayBLL = new XeMayBLL();
            currentMaTaiKhoan = maTaiKhoan;

            InitializeCardView();
            LoadXeBan();

            // Thiết lập placeholder cho txtSearch
            SetupSearchPlaceholder();

            // Gán sự kiện
            this.Load += ViewQuanLyBanHang_Load;
            btnSearch.Click += BtnSearch_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnXemHopDong.Click += BtnXemHopDong_Click;
            btnLichSuGiaoDich.Click += BtnLichSuGiaoDich_Click;

            langMgr.LanguageChanged += (s, e) => { ApplyLanguage(); LoadXeBan(); };
            ApplyLanguage();
            ApplyPermissions();
        }

        private void ApplyPermissions()
        {
            // Chỉ Admin và Thu ngân được xem lịch sử giao dịch
            bool canViewHistory = CurrentUser.ChucVu == "Quản lý" || CurrentUser.ChucVu == "Thu ngân";
            btnLichSuGiaoDich.Visible = canViewHistory;
        }

        private void ApplyLanguage()
        {
            lblTitle.Text = langMgr.GetString("SaleTitle");
            btnSearch.Text = langMgr.GetString("SearchBtn");
            btnRefresh.Text = langMgr.GetString("RefreshBtn");
            btnXemHopDong.Text = langMgr.GetString("ViewContractBtn");

            //btnViewDetail.Text = langMgr.GetString("ViewDetailBtn");
            //btnApprove.Text = langMgr.GetString("ApproveBtn");
            //btnReject.Text = langMgr.GetString("RejectBtn");
        }


        private void SetupSearchPlaceholder()
        {
            txtSearch.ForeColor = Color.Gray;
            txtSearch.Text = "Tìm kiếm theo tên";

            txtSearch.Enter += (s, e) =>
            {
                if (txtSearch.Text == "Tìm kiếm theo tên" && txtSearch.ForeColor == Color.Gray)
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };

            txtSearch.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Tìm kiếm theo tên";
                    txtSearch.ForeColor = Color.Gray;
                }
            };
        }

        private void InitializeCardView()
        {
            flowPanelCards = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(20)
            };

            panelDataGrid.Controls.Add(flowPanelCards);
        }

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void ViewQuanLyBanHang_Load(object sender, EventArgs e)
        {
            LoadXeBan();
        }

        private void LoadXeBan()
        {
            try
            {
                // Lấy tất cả xe bán bao gồm cả xe hết hàng
                DataTable dt = xeMayBLL.GetTatCaXeBan();
                DisplayXeCards(dt);
                UpdateRecordCount(dt?.Rows.Count ?? 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayXeCards(DataTable dt)
        {
            flowPanelCards.Controls.Clear();

            if (dt == null || dt.Rows.Count == 0)
            {
                Label lblEmpty = new Label
                {
                    Text = "Không có xe nào sẵn sàng bán",
                    Font = new Font("Segoe UI", 14F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Padding = new Padding(20)
                };
                flowPanelCards.Controls.Add(lblEmpty);
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                Panel card = CreateXeCard(row);
                flowPanelCards.Controls.Add(card);
            }
        }

        private Panel CreateXeCard(DataRow row)
        {
            string idXe = row["ID_Xe"].ToString();
            string idLoai = row["ID_Loai"].ToString();
            string bienSo = row["BienSo"] != DBNull.Value ? row["BienSo"].ToString() : "Chưa có";
            string tenHang = row["TenHang"].ToString();
            string tenDong = row["TenDong"].ToString();
            string tenMau = row["TenMau"].ToString();
            string tenXe = $"{tenHang} {tenDong} - {tenMau}";
            int namSX = row["NamSX"] != DBNull.Value ? Convert.ToInt32(row["NamSX"]) : 0;
            int soLuong = Convert.ToInt32(row["SoLuong"]);
            decimal giaBan = row["GiaBanGanNhat"] != DBNull.Value ? Convert.ToDecimal(row["GiaBanGanNhat"]) : 0;
            int phanKhoi = row["PhanKhoi"] != DBNull.Value ? Convert.ToInt32(row["PhanKhoi"]) : 0;

            // Panel chính của card với bo góc
            Panel card = new Panel
            {
                Size = new Size(260, 350),
                Margin = new Padding(10),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };

            // Bo góc và shadow
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                using (System.Drawing.Drawing2D.GraphicsPath path = GetRoundedRectangle(rect, 8))
                {
                    card.Region = new Region(path);
                    using (Pen pen = new Pen(Color.FromArgb(230, 230, 230), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };

            // Panel ảnh xe
            Panel imagePanel = new Panel
            {
                Size = new Size(260, 160),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(255, 240, 245)
            };

            // Vẽ ảnh xe hoặc placeholder
            imagePanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                
                if (row["AnhXe"] != DBNull.Value)
                {
                    try
                    {
                        byte[] imageBytes = (byte[])row["AnhXe"];
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBytes))
                            using (Image img = Image.FromStream(ms))  
                            {
                                e.Graphics.DrawImage(img, 0, 0, imagePanel.Width, imagePanel.Height);
                            }
                        }
                    }
                    catch { }
                }
            };

            card.Controls.Add(imagePanel);

            // Badge "Hết hàng" nếu số lượng = 0
            if (soLuong <= 0)
            {
                Panel badgeHetHang = new Panel
                {
                    Size = new Size(100, 35),
                    Location = new Point(10, 10),
                    BackColor = Color.FromArgb(220, 244, 67, 54) // Đỏ với opacity
                };
                
                badgeHetHang.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Rectangle rect = new Rectangle(0, 0, badgeHetHang.Width - 1, badgeHetHang.Height - 1);
                    using (System.Drawing.Drawing2D.GraphicsPath path = GetRoundedRectangle(rect, 4))
                    {
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(244, 67, 54)))
                        {
                            e.Graphics.FillPath(brush, path);
                        }
                    }
                };
                
                Label lblHetHang = new Label
                {
                    Text = "HẾT HÀNG",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.White,
                    Size = new Size(100, 35),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.Transparent
                };
                
                badgeHetHang.Controls.Add(lblHetHang);
                imagePanel.Controls.Add(badgeHetHang);
                badgeHetHang.BringToFront();
            }
            else if (soLuong <= 3)
            {
                // Badge cảnh báo sắp hết hàng
                Panel badgeSapHet = new Panel
                {
                    Size = new Size(110, 35),
                    Location = new Point(10, 10),
                    BackColor = Color.FromArgb(220, 255, 152, 0) // Cam với opacity
                };
                
                badgeSapHet.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Rectangle rect = new Rectangle(0, 0, badgeSapHet.Width - 1, badgeSapHet.Height - 1);
                    using (System.Drawing.Drawing2D.GraphicsPath path = GetRoundedRectangle(rect, 4))
                    {
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 152, 0)))
                        {
                            e.Graphics.FillPath(brush, path);
                        }
                    }
                };
                
                Label lblSapHet = new Label
                {
                    Text = $"CÒN {soLuong} XE",
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                    ForeColor = Color.White,
                    Size = new Size(110, 35),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.Transparent
                };
                
                badgeSapHet.Controls.Add(lblSapHet);
                imagePanel.Controls.Add(badgeSapHet);
                badgeSapHet.BringToFront();
            }

            // Tên xe (Hãng + Dòng)
            Label lblTenXe = new Label
            {
                Text = $"{tenHang} {tenDong}",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(10, 170),
                Size = new Size(240, 26),
                ForeColor = Color.FromArgb(33, 33, 33)
            };
            card.Controls.Add(lblTenXe);

            // Thông tin chi tiết (bao gồm biển số)
            Label lblThongTin = new Label
            {
                Text = $"Màu: {tenMau} | Năm: {namSX} | {phanKhoi}cc | BSX: {bienSo}",
                Font = new Font("Segoe UI", 8F),
                Location = new Point(10, 198),
                Size = new Size(240, 18),
                ForeColor = Color.FromArgb(120, 120, 120)
            };
            card.Controls.Add(lblThongTin);

            // Số lượng
            //Label lblSoLuong = new Label
            //{
            //    Text = $"Còn: {soLuong} xe",
            //    Location = new Point(10, 85),
            //    Width = 250,
            //    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            //    ForeColor = soLuong > 5 ? Color.FromArgb(76, 175, 80) : Color.FromArgb(255, 152, 0)
            //};
            //card.Controls.Add(lblSoLuong);

            // Giá (lớn và nổi bật)
            Label lblGia = new Label
            {
                Text = giaBan > 0 ? string.Format("{0:N0} VNĐ", giaBan) : "Liên hệ",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(10, 222),
                Size = new Size(240, 28),
                ForeColor = Color.FromArgb(211, 47, 47),
                TextAlign = ContentAlignment.MiddleLeft
            };
            card.Controls.Add(lblGia);

            // Nút MUA NGAY
            Button btnMua = new Button
            {
                Text = soLuong <= 0 ? "HẾT HÀNG" : "MUA NGAY",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Location = new Point(10, 260),
                Size = new Size(240, 38),
                BackColor = soLuong <= 0 ? Color.FromArgb(158, 158, 158) : Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = soLuong <= 0 ? Cursors.No : Cursors.Hand,
                Enabled = soLuong > 0
            };
            btnMua.FlatAppearance.BorderSize = 0;
            
            if (soLuong > 0)
            {
                btnMua.Click += (s, e) => BtnMua_Click(s, e, idXe);
            }
            else
            {
                btnMua.Click += (s, e) => 
                {
                    MessageBox.Show(
                        "Xe này đã hết hàng!\n\nVui lòng chọn xe khác hoặc liên hệ để nhập thêm hàng.",
                        "Xe hết hàng",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                };
            }
            
            card.Controls.Add(btnMua);

            // Hover effect
            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(245, 245, 245);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            return card;
        }

        private void BtnMua_Click(object sender, EventArgs e, string idXe)
        {
            try
            {
                // Kiểm tra xe trước khi mở form
                string errorMessage;
                if (!xeMayBLL.KiemTraXeTruocKhiBan(idXe, 1, out errorMessage))
                {
                    MessageBox.Show(
                        errorMessage,
                        "Không thể bán xe",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Mở form bán xe với thông tin xe đã chọn
                FormMuaXe formBan = new FormMuaXe(currentMaTaiKhoan, idXe);
                if (formBan.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Bán xe thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadXeBan(); // Refresh danh sách
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            // Bỏ qua nếu là placeholder hoặc rỗng
            if (string.IsNullOrEmpty(keyword) || keyword == "Tìm kiếm theo tên")
            {
                LoadXeBan();
                return;
            }

            try
            {
                DataTable dt = xeMayBLL.GetLoaiXeSanSangBan();
                
                // Lọc theo từ khóa
                DataView dv = dt.DefaultView;
                dv.RowFilter = $"TenHang LIKE '%{keyword}%' OR TenDong LIKE '%{keyword}%' OR TenMau LIKE '%{keyword}%'";
                
                DisplayXeCards(dv.ToTable());
                UpdateRecordCount(dv.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadXeBan();
        }

        private void UpdateRecordCount(int count)
        {
            lblRecordCount.Text = $"Tổng số xe có thể bán: {count}";
            lblRecordCount.ForeColor = count > 0 ? Color.FromArgb(25, 118, 210) : Color.Gray;
        }

        private void BtnXemHopDong_Click(object sender, EventArgs e)
        {
            // Mở form danh sách hợp đồng mua
            FormDanhSachHopDongMua formHopDong = new FormDanhSachHopDongMua();
            formHopDong.ShowDialog();
        }

        private void BtnLichSuGiaoDich_Click(object sender, EventArgs e)
        {
            try
            {
                GiaoDichBanBLL giaoDichBLL = new GiaoDichBanBLL();
                DataTable dtGiaoDich = giaoDichBLL.GetLichSuGiaoDichTongHop();
                DataTable dtPhuTung = giaoDichBLL.GetLichSuBanPhuTungLe();

                // Tính tổng doanh thu
                decimal doanhThuNgay = 0;
                decimal doanhThuTuan = 0;
                DateTime today = DateTime.Today;
                DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

                // Tính từ giao dịch bán/thuê
                foreach (DataRow row in dtGiaoDich.Rows)
                {
                    DateTime ngay = Convert.ToDateTime(row["NgayGiaoDich"]);
                    decimal tien = Convert.ToDecimal(row["ThanhToan"]);
                    
                    if (ngay.Date == today)
                        doanhThuNgay += tien;
                    
                    if (ngay.Date >= startOfWeek && ngay.Date <= today)
                        doanhThuTuan += tien;
                }

                // Tính từ phụ tùng lẻ
                foreach (DataRow row in dtPhuTung.Rows)
                {
                    DateTime ngay = Convert.ToDateTime(row["NgayGiaoDich"]);
                    decimal tien = Convert.ToDecimal(row["ThanhTien"]);
                    
                    if (ngay.Date == today)
                        doanhThuNgay += tien;
                    
                    if (ngay.Date >= startOfWeek && ngay.Date <= today)
                        doanhThuTuan += tien;
                }

                // Tạo form hiển thị lịch sử
                Form formLichSu = new Form
                {
                    Text = "Lịch Sử Giao Dịch",
                    Size = new System.Drawing.Size(1400, 850),
                    StartPosition = FormStartPosition.CenterScreen,
                    BackColor = Color.White
                };

                // Panel tổng hợp doanh thu
                Panel panelSummary = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 80,
                    Padding = new Padding(20, 10, 20, 10),
                    BackColor = Color.FromArgb(245, 245, 245)
                };

                Label lblDoanhThuNgay = new Label
                {
                    Text = $"💰 Doanh Thu Hôm Nay: {doanhThuNgay:N0} VNĐ",
                    Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(76, 175, 80),
                    AutoSize = true,
                    Location = new Point(20, 15)
                };

                Label lblDoanhThuTuan = new Label
                {
                    Text = $"📊 Doanh Thu Tuần Này: {doanhThuTuan:N0} VNĐ",
                    Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 150, 243),
                    AutoSize = true,
                    Location = new Point(20, 45)
                };

                panelSummary.Controls.Add(lblDoanhThuNgay);
                panelSummary.Controls.Add(lblDoanhThuTuan);

                // Panel chứa bảng giao dịch bán/thuê
                Panel panelTop = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 360,
                    Padding = new Padding(10)
                };

                Label lblGiaoDich = new Label
                {
                    Text = "GIAO DỊCH BÁN XE VÀ CHO THUÊ",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(25, 118, 210),
                    Dock = DockStyle.Top,
                    Height = 30,
                    TextAlign = ContentAlignment.MiddleLeft
                };

                DataGridView dgvGiaoDich = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    DataSource = dtGiaoDich,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    BackgroundColor = Color.White,
                    BorderStyle = BorderStyle.None,
                    CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                    RowHeadersVisible = false,
                    AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(240, 240, 240) },
                    DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 9F) },
                    ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                    {
                        BackColor = Color.FromArgb(25, 118, 210),
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                    },
                    EnableHeadersVisualStyles = false,
                    RowTemplate = { Height = 30 }
                };

                panelTop.Controls.Add(dgvGiaoDich);
                panelTop.Controls.Add(lblGiaoDich);

                // Panel chứa bảng phụ tùng lẻ
                Panel panelBottom = new Panel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(10)
                };

                Label lblPhuTung = new Label
                {
                    Text = "LỊCH SỬ BÁN PHỤ TÙNG LẺ",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(255, 152, 0),
                    Dock = DockStyle.Top,
                    Height = 30,
                    TextAlign = ContentAlignment.MiddleLeft
                };

                DataGridView dgvPhuTung = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    DataSource = dtPhuTung,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    BackgroundColor = Color.White,
                    BorderStyle = BorderStyle.None,
                    CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                    RowHeadersVisible = false,
                    AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(255, 248, 225) },
                    DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 9F) },
                    ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                    {
                        BackColor = Color.FromArgb(255, 152, 0),
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                    },
                    EnableHeadersVisualStyles = false,
                    RowTemplate = { Height = 30 }
                };

                panelBottom.Controls.Add(dgvPhuTung);
                panelBottom.Controls.Add(lblPhuTung);

                formLichSu.Controls.Add(panelBottom);
                formLichSu.Controls.Add(panelTop);
                formLichSu.Controls.Add(panelSummary);

                // Format cột cho bảng giao dịch
                if (dgvGiaoDich.Columns["LoaiGiaoDich"] != null)
                {
                    dgvGiaoDich.Columns["LoaiGiaoDich"].HeaderText = "Loại";
                    dgvGiaoDich.Columns["LoaiGiaoDich"].Width = 100;
                }
                if (dgvGiaoDich.Columns["NgayGiaoDich"] != null)
                {
                    dgvGiaoDich.Columns["NgayGiaoDich"].HeaderText = "Ngày";
                    dgvGiaoDich.Columns["NgayGiaoDich"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dgvGiaoDich.Columns["ThanhToan"] != null)
                {
                    dgvGiaoDich.Columns["ThanhToan"].HeaderText = "Thanh Toán";
                    dgvGiaoDich.Columns["ThanhToan"].DefaultCellStyle.Format = "#,##0 VNĐ";
                    dgvGiaoDich.Columns["ThanhToan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // Format cột cho bảng phụ tùng
                if (dgvPhuTung.Columns["NgayGiaoDich"] != null)
                {
                    dgvPhuTung.Columns["NgayGiaoDich"].HeaderText = "Ngày";
                    dgvPhuTung.Columns["NgayGiaoDich"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    dgvPhuTung.Columns["NgayGiaoDich"].Width = 150;
                }
                if (dgvPhuTung.Columns["SanPham"] != null)
                {
                    dgvPhuTung.Columns["SanPham"].HeaderText = "Phụ Tùng";
                }
                if (dgvPhuTung.Columns["SoLuong"] != null)
                {
                    dgvPhuTung.Columns["SoLuong"].HeaderText = "SL";
                    dgvPhuTung.Columns["SoLuong"].Width = 50;
                    dgvPhuTung.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvPhuTung.Columns["ThanhTien"] != null)
                {
                    dgvPhuTung.Columns["ThanhTien"].HeaderText = "Thành Tiền";
                    dgvPhuTung.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0 VNĐ";
                    dgvPhuTung.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvPhuTung.Columns["NhanVien"] != null)
                {
                    dgvPhuTung.Columns["NhanVien"].HeaderText = "Nhân Viên";
                    dgvPhuTung.Columns["NhanVien"].Width = 150;
                }

                formLichSu.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử giao dịch: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {
            // Empty
        }

        private void panelDataGrid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLichSuGiaoDich_Click_1(object sender, EventArgs e)
        {

        }
    }
}