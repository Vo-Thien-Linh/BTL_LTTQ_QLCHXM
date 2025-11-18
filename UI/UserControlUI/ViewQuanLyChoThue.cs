using BLL;
using DTO;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using UI.FormUI;

namespace UI.UserControlUI
{
    public partial class ViewQuanLyChoThue : UserControl
    {
        private GiaoDichThueBLL giaoDichThueBLL;
        private FlowLayoutPanel flpDonThue;
        private string currentFilter = "";// "Đang thuê", "Chờ xác nhận", etc.
        private string maNhanVien; 

        public ViewQuanLyChoThue(string maNV)
        {
            InitializeComponent();
            this.maNhanVien = maNV; // Lưu lại
            giaoDichThueBLL = new GiaoDichThueBLL();

            InitializeFlowLayoutPanel();
            LoadData();

            // Gán sự kiện
            btnSearch.Click += BtnSearch_Click;
            btnRefresh.Click += BtnRefresh_Click;
            cboFilter.SelectedIndexChanged += CboFilter_SelectedIndexChanged;
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnSearch_Click(s, e); };
        }
        // Constructor mặc định (để Designer không lỗi)
        public ViewQuanLyChoThue() : this(DTO.CurrentUser.MaNV ?? "")
        {
        }
        private void InitializeFlowLayoutPanel()
        {
            flpDonThue = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(236, 240, 241),
                Padding = new Padding(20),
                WrapContents = true
            };

            panelContent.Controls.Add(flpDonThue);
        }

        private void LoadData()
        {
            try
            {
                DataTable dt;

                if (string.IsNullOrEmpty(currentFilter) || currentFilter == "Tất cả")
                {
                    dt = giaoDichThueBLL.GetDonChoThue();
                }
                else
                {
                    dt = giaoDichThueBLL.FilterDonChoThueByStatus(currentFilter);
                }

                DisplayCards(dt);
                UpdateRecordCount(dt.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi tai du lieu: " + ex.Message, "Loi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayCards(DataTable dt)
        {
            flpDonThue.Controls.Clear();

            if (dt.Rows.Count == 0)
            {
                Label lblNoData = new Label
                {
                    Text = "Khong tim thay don thue nao!",
                    Font = new Font("Segoe UI", 14F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Padding = new Padding(20)
                };
                flpDonThue.Controls.Add(lblNoData);
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                Panel card = CreateCard(row);
                flpDonThue.Controls.Add(card);
            }
        }

        private Panel CreateCard(DataRow row)
        {
            int soNgayQuaHan = row["SoNgayQuaHan"] != DBNull.Value
                ? Convert.ToInt32(row["SoNgayQuaHan"]) : 0;
            bool isQuaHan = soNgayQuaHan > 0;

            // Main card panel
            Panel card = new Panel
            {
                Width = 350,
                Height = 480,
                Margin = new Padding(10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            // Border với màu cảnh báo nếu quá hạn
            Color borderColor = isQuaHan
                ? Color.FromArgb(244, 67, 54) // Đỏ nếu quá hạn
                : Color.FromArgb(220, 220, 220); // Xám bình thường

            card.Paint += (s, e) =>
            {
                int borderWidth = isQuaHan ? 3 : 1;
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    borderColor, borderWidth, ButtonBorderStyle.Solid,
                    borderColor, borderWidth, ButtonBorderStyle.Solid,
                    borderColor, borderWidth, ButtonBorderStyle.Solid,
                    borderColor, borderWidth, ButtonBorderStyle.Solid);
            };

            // Cảnh báo quá hạn (nếu có)
            int yPos = 10;
            if (isQuaHan)
            {
                Panel warningPanel = new Panel
                {
                    Location = new Point(0, 0),
                    Width = 350,
                    Height = 35,
                    BackColor = Color.FromArgb(244, 67, 54)
                };

                Label lblWarning = new Label
                {
                    Text = $"QUA HAN {soNgayQuaHan} NGAY!",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.White,
                    AutoSize = false,
                    Width = 350,
                    Height = 35,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                warningPanel.Controls.Add(lblWarning);
                card.Controls.Add(warningPanel);
                yPos = 40;
            }

            // Image
            PictureBox picXe = new PictureBox
            {
                Width = 330,
                Height = 180,
                Location = new Point(10, yPos),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(245, 245, 245)
            };

            string anhXe = row["AnhXeXeBan"].ToString();
            if (!string.IsNullOrEmpty(anhXe) && File.Exists(anhXe))
            {
                picXe.Image = Image.FromFile(anhXe);
            }
            else
            {
                picXe.Image = CreatePlaceholderImage(330, 180, row["TenXe"].ToString());
            }

            card.Controls.Add(picXe);
            yPos += 190;

            // Mã GD + Trạng thái
            Label lblMaGD = new Label
            {
                Text = $"Don #{row["MaGDThue"]}",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                Location = new Point(15, yPos),
                AutoSize = true
            };
            card.Controls.Add(lblMaGD);

            string trangThai = row["TrangThai"].ToString();
            Color trangThaiColor = GetStatusColor(trangThai);
            Label lblTrangThai = new Label
            {
                Text = trangThai,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = trangThaiColor,
                Location = new Point(250, yPos),
                AutoSize = true
            };
            card.Controls.Add(lblTrangThai);
            yPos += 25;

            // Tên xe
            Label lblTenXe = new Label
            {
                Text = row["TenXe"].ToString(),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                AutoSize = false,
                Width = 320,
                Height = 25,
                Location = new Point(15, yPos)
            };
            card.Controls.Add(lblTenXe);
            yPos += 30;

            // Biển số
            Label lblBienSo = new Label
            {
                Text = $"Bien so: {row["BienSo"]}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                Location = new Point(15, yPos),
                AutoSize = true
            };
            card.Controls.Add(lblBienSo);
            yPos += 25;

            // Khách hàng
            Label lblKhachHang = new Label
            {
                Text = $"Khach: {row["HoTenKH"]} - {row["SdtKhachHang"]}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(66, 66, 66),
                AutoSize = false,
                Width = 320,
                Height = 20,
                Location = new Point(15, yPos)
            };
            card.Controls.Add(lblKhachHang);
            yPos += 25;

            // Ngày thuê
            Label lblNgayThue = new Label
            {
                Text = $"{Convert.ToDateTime(row["NgayBatDau"]):dd/MM/yyyy} - {Convert.ToDateTime(row["NgayKetThuc"]):dd/MM/yyyy}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                Location = new Point(15, yPos),
                AutoSize = true
            };
            card.Controls.Add(lblNgayThue);

            Label lblSoNgay = new Label
            {
                Text = $"({row["SoNgayThue"]} ngay)",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.Gray,
                Location = new Point(240, yPos),
                AutoSize = true
            };
            card.Controls.Add(lblSoNgay);
            yPos += 25;

            // Giá thuê
            decimal tongGia = Convert.ToDecimal(row["TongGia"]);
            Label lblGia = new Label
            {
                Text = $"{tongGia:N0} VND",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(76, 175, 80),
                Location = new Point(15, yPos),
                AutoSize = true
            };
            card.Controls.Add(lblGia);
            yPos += 30;

            // Thanh toán
            string ttThanhToan = row["TrangThaiThanhToan"].ToString();
            Label lblThanhToan = new Label
            {
                Text = ttThanhToan == "Đã thanh toán" ? "Da thanh toan" : "Chua thanh toan",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = ttThanhToan == "Đã thanh toán"
                    ? Color.FromArgb(76, 175, 80)
                    : Color.FromArgb(255, 152, 0),
                BackColor = ttThanhToan == "Đã thanh toán"
                    ? Color.FromArgb(200, 230, 201)
                    : Color.FromArgb(255, 243, 205),
                Padding = new Padding(8, 4, 8, 4),
                Location = new Point(15, yPos),
                AutoSize = true
            };
            card.Controls.Add(lblThanhToan);
            yPos += 35;

            // Button xem hợp đồng
            Button btnXemHopDong = new Button
            {
                Text = "XEM HOP DONG",
                Width = 320,
                Height = 40,
                Location = new Point(15, yPos),
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnXemHopDong.FlatAppearance.BorderSize = 0;
            btnXemHopDong.Click += (s, e) => OpenHopDongForm(row);
            card.Controls.Add(btnXemHopDong);

            return card;
        }

        private Image CreatePlaceholderImage(int width, int height, string text)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(245, 245, 245));

                Font font = new Font("Segoe UI", 12F, FontStyle.Bold);
                SizeF textSize = g.MeasureString(text, font);

                g.DrawString(text, font, Brushes.Gray,
                    (width - textSize.Width) / 2, (height - textSize.Height) / 2);
            }
            return bmp;
        }

        private Color GetStatusColor(string trangThai)
        {
            switch (trangThai)
            {
                case "Đang thuê":
                    return Color.FromArgb(76, 175, 80); // Xanh lá
                case "Chờ xác nhận":
                    return Color.FromArgb(255, 152, 0); // Cam
                case "Đã thuê":
                    return Color.FromArgb(158, 158, 158); // Xám
                default:
                    return Color.Gray;
            }
        }

        private void OpenHopDongForm(DataRow row)
        {
            int maGDThue = Convert.ToInt32(row["MaGDThue"]);

            using (FormXemHopDongThue form = new FormXemHopDongThue(maGDThue, maNhanVien))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword) ||
                keyword == "Tim kiem theo ma GD, ten khach, bien so...")
            {
                LoadData();
                return;
            }

            try
            {
                DataTable dt = giaoDichThueBLL.SearchDonChoThue(keyword);

                if (!string.IsNullOrEmpty(currentFilter) && currentFilter != "Tất cả")
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = $"TrangThai = '{currentFilter}'";
                    dt = dv.ToTable();
                }

                DisplayCards(dt);
                UpdateRecordCount(dt.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi tim kiem: " + ex.Message, "Loi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "Tim kiem theo ma GD, ten khach, bien so...";
            txtSearch.ForeColor = Color.Gray;
            cboFilter.SelectedIndex = 0;
            currentFilter = "";
            LoadData();
        }

        private void CboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cboFilter.SelectedItem?.ToString();
            currentFilter = (selected == "Tất cả") ? "" : selected;
            LoadData();
        }

        private void UpdateRecordCount(int count)
        {
            lblCount.Text = $"Tim thay {count} don thue";

            // Hiển thị số đơn quá hạn
            try
            {
                DataTable dtQuaHan = giaoDichThueBLL.GetDonQuaHan();
                if (dtQuaHan.Rows.Count > 0)
                {
                    lblCount.Text += $" ({dtQuaHan.Rows.Count} qua han)";
                    lblCount.ForeColor = Color.FromArgb(244, 67, 54);
                }
                else
                {
                    lblCount.ForeColor = Color.FromArgb(33, 150, 243);
                }
            }
            catch { }
        }

        private void btnThemDon_Click(object sender, EventArgs e)
        {
            using (FormThemDonThue form = new FormThemDonThue(maNhanVien))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Reload lại dữ liệu sau khi thêm đơn thành công
                    LoadData();
                    MessageBox.Show("Đã tải lại dữ liệu!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}