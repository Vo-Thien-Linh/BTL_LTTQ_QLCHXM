using System;
using System.Data;
using System.Drawing;
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

            btnViewDetail.Text = langMgr.GetString("ViewDetailBtn");
            btnApprove.Text = langMgr.GetString("ApproveBtn");
            btnReject.Text = langMgr.GetString("RejectBtn");
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
            string idLoai = row["ID_Loai"].ToString();
            string tenHang = row["TenHang"].ToString();
            string tenDong = row["TenDong"].ToString();
            string tenMau = row["TenMau"].ToString();
            string tenXe = $"{tenHang} {tenDong} - {tenMau}";
            int namSX = row["NamSX"] != DBNull.Value ? Convert.ToInt32(row["NamSX"]) : 0;
            int soLuong = Convert.ToInt32(row["SoLuong"]);
            decimal giaBan = row["GiaBanGanNhat"] != DBNull.Value ? Convert.ToDecimal(row["GiaBanGanNhat"]) : 0;
            int phanKhoi = row["PhanKhoi"] != DBNull.Value ? Convert.ToInt32(row["PhanKhoi"]) : 0;

            // Panel chính của card
            Panel card = new Panel
            {
                Width = 280,
                Height = 220,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Padding = new Padding(15)
            };

            // Tên xe
            Label lblTenXe = new Label
            {
                Text = tenXe,
                Location = new Point(10, 10),
                Width = 250,
                Height = 45,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                TextAlign = ContentAlignment.TopLeft
            };
            card.Controls.Add(lblTenXe);

            // Thông tin phân khối và năm
            Label lblThongTin = new Label
            {
                Text = $"{phanKhoi}cc - Năm {namSX}",
                Location = new Point(10, 60),
                Width = 250,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray
            };
            card.Controls.Add(lblThongTin);

            // Số lượng
            Label lblSoLuong = new Label
            {
                Text = $"Còn: {soLuong} xe",
                Location = new Point(10, 85),
                Width = 250,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = soLuong > 5 ? Color.FromArgb(76, 175, 80) : Color.FromArgb(255, 152, 0)
            };
            card.Controls.Add(lblSoLuong);

            // Đường kẻ
            Panel divider = new Panel
            {
                Location = new Point(10, 115),
                Width = 250,
                Height = 1,
                BackColor = Color.FromArgb(224, 224, 224)
            };
            card.Controls.Add(divider);

            // Giá bán
            Label lblGia = new Label
            {
                Text = giaBan > 0 ? $"{giaBan:N0} VNĐ" : "Liên hệ",
                Location = new Point(10, 125),
                Width = 250,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(244, 67, 54),
                TextAlign = ContentAlignment.MiddleLeft
            };
            card.Controls.Add(lblGia);

            // Nút MUA
            Button btnMua = new Button
            {
                Text = "MUA",
                Location = new Point(10, 165),
                Width = 250,
                Height = 40,
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnMua.FlatAppearance.BorderSize = 0;
            btnMua.Click += (s, e) => BtnMua_Click(s, e, idLoai);
            card.Controls.Add(btnMua);

            // Hover effect
            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(245, 245, 245);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            return card;
        }

        private void BtnMua_Click(object sender, EventArgs e, string idLoai)
        {
            try
            {
                // Mở form bán xe mới (không phải form cũ trong FormHandleUI)
                FormMuaXe formBan = new FormMuaXe(currentMaTaiKhoan);
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

            if (string.IsNullOrEmpty(keyword))
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
            lblRecordCount.Text = $"Tổng số loại xe có thể bán: {count}";
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
    }
}