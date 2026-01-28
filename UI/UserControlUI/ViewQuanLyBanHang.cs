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

            langMgr.LanguageChanged += (s, e) => { ApplyLanguage(); LoadXeBan(); };
            ApplyLanguage();
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
                DataTable dt = xeMayBLL.GetLoaiXeSanSangBan();
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
                Text = "MUA NGAY",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Location = new Point(10, 260),
                Size = new Size(240, 38),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnMua.FlatAppearance.BorderSize = 0;
            btnMua.Click += (s, e) => BtnMua_Click(s, e, idXe);
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
    }
}