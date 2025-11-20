using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BLL;
// FORM NÀY CHỈ ĐỂ TEST CHỨ CHƯA SỬ DỤNG
namespace UI.UserControlUI
{
    public partial class ViewMuaThueXe : UserControl
    {
        private ThongTinGiaXeBLL thongTinGiaXeBLL;
        private FlowLayoutPanel flpXe;
        private string currentFilter = ""; // "Bán", "Thuê", or ""

        public ViewMuaThueXe()
        {
            InitializeComponent();
            thongTinGiaXeBLL = new ThongTinGiaXeBLL();

            InitializeFlowLayoutPanel();

            // HARD-CODE: Hiển thị dữ liệu mẫu
            LoadHardCodedData();

            // Gán sự kiện
            btnSearch.Click += BtnSearch_Click;
            btnRefresh.Click += BtnRefresh_Click;
            cboFilter.SelectedIndexChanged += CboFilter_SelectedIndexChanged;
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnSearch_Click(s, e); };
        }

        private void InitializeFlowLayoutPanel()
        {
            flpXe = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(236, 240, 241),
                Padding = new Padding(20),
                WrapContents = true
            };

            panelContent.Controls.Add(flpXe);
        }

        // ===== HARD-CODE DỮ LIỆU MẪU =====
        private void LoadHardCodedData()
        {
            flpXe.Controls.Clear();

            // Tạo 8 sản phẩm xe mẫu
            flpXe.Controls.Add(CreateHardCodedCard(
                "Honda", "Winner X", "Đỏ Đen", 2023, 150, "Bán", 45000000, 0, "XE00000001"));

            flpXe.Controls.Add(CreateHardCodedCard(
                "Yamaha", "Exciter 155", "Xanh GP", 2024, 155, "Bán", 52000000, 0, "XE00000002"));

            flpXe.Controls.Add(CreateHardCodedCard(
                "Honda", "SH Mode", "Trắng", 2023, 125, "Thuê", 0, 200000, "XE00000003"));

            flpXe.Controls.Add(CreateHardCodedCard(
                "Yamaha", "Sirius", "Đen Xám", 2022, 110, "Bán", 18000000, 0, "XE00000004"));

            flpXe.Controls.Add(CreateHardCodedCard(
                "Honda", "Air Blade", "Xanh Dương", 2024, 150, "Thuê", 0, 250000, "XE00000005"));

            flpXe.Controls.Add(CreateHardCodedCard(
                "Suzuki", "Raider 150", "Đỏ", 2021, 150, "Bán", 35000000, 0, "XE00000006"));

            flpXe.Controls.Add(CreateHardCodedCard(
                "Honda", "Vision", "Xám Bạc", 2023, 110, "Thuê", 0, 180000, "XE00000007"));

            flpXe.Controls.Add(CreateHardCodedCard(
                "Yamaha", "Jupiter", "Xanh Đen", 2022, 115, "Bán", 22000000, 0, "XE00000008"));

            UpdateRecordCount(8);
        }

        private Panel CreateHardCodedCard(
            string tenHang,
            string tenDong,
            string mauSac,
            int namSX,
            int phanKhoi,
            string phanLoai,
            decimal giaBan,
            decimal giaThueNgay,
            string idXe)
        {
            // Main card panel
            Panel card = new Panel
            {
                Width = 300,
                Height = 450,
                Margin = new Padding(10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            // Add shadow effect
            card.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid);
            };

            // Image (Placeholder)
            PictureBox picXe = new PictureBox
            {
                Width = 300,
                Height = 200,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(245, 245, 245),
                Dock = DockStyle.Top,
                Image = CreatePlaceholderImage(300, 200, tenHang)
            };
            card.Controls.Add(picXe);

            int yPosition = 210;

            // Tên xe
            string tenXe = $"{tenHang} {tenDong}";
            Label lblTenXe = new Label
            {
                Text = tenXe,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                AutoSize = false,
                Width = 270,
                Height = 30,
                Location = new Point(15, yPosition),
                TextAlign = ContentAlignment.MiddleLeft
            };
            card.Controls.Add(lblTenXe);
            yPosition += 35;

            // Chi tiết
            string chiTiet = $"Mau: {mauSac} | Nam: {namSX} | {phanKhoi}cc";
            Label lblChiTiet = new Label
            {
                Text = chiTiet,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                AutoSize = false,
                Width = 270,
                Height = 40,
                Location = new Point(15, yPosition)
            };
            card.Controls.Add(lblChiTiet);
            yPosition += 45;

            // Phân loại (Bán/Thuê)
            Label lblPhanLoai = new Label
            {
                Text = phanLoai == "Bán" ? "Xe ban" : "Xe cho thue",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = phanLoai == "Bán" ? Color.FromArgb(76, 175, 80) : Color.FromArgb(33, 150, 243),
                AutoSize = true,
                Location = new Point(15, yPosition)
            };
            card.Controls.Add(lblPhanLoai);
            yPosition += 30;

            // Giá
            string giaText = "";
            if (phanLoai == "Bán")
            {
                giaText = $"{giaBan:N0} VND";
            }
            else
            {
                giaText = $"{giaThueNgay:N0} VND/ngay";
            }

            Label lblGia = new Label
            {
                Text = giaText,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(211, 47, 47),
                AutoSize = false,
                Width = 270,
                Height = 30,
                Location = new Point(15, yPosition),
                TextAlign = ContentAlignment.MiddleLeft
            };
            card.Controls.Add(lblGia);
            yPosition += 40;

            // Buttons Panel
            Panel btnPanel = new Panel
            {
                Location = new Point(15, yPosition),
                Width = 270,
                Height = 40
            };

            if (phanLoai == "Bán")
            {
                Button btnMua = new Button
                {
                    Text = "MUA NGAY",
                    Width = 270,
                    Height = 40,
                    BackColor = Color.FromArgb(76, 175, 80),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };
                btnMua.FlatAppearance.BorderSize = 0;
                btnMua.Click += (s, e) => OnMuaClick(tenXe, giaBan, idXe);
                btnPanel.Controls.Add(btnMua);
            }
            else
            {
                Button btnThue = new Button
                {
                    Text = "THUE NGAY",
                    Width = 270,
                    Height = 40,
                    BackColor = Color.FromArgb(33, 150, 243),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };
                btnThue.FlatAppearance.BorderSize = 0;
                btnThue.Click += (s, e) => OnThueClick(tenXe, giaThueNgay, idXe);
                btnPanel.Controls.Add(btnThue);
            }

            card.Controls.Add(btnPanel);

            return card;
        }

        private Image CreatePlaceholderImage(int width, int height, string tenHang)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Background gradient
                Color color1 = Color.FromArgb(245, 245, 245);
                Color color2 = Color.FromArgb(230, 230, 230);

                // Màu sắc theo hãng
                if (tenHang == "Honda")
                {
                    color1 = Color.FromArgb(220, 240, 255);
                    color2 = Color.FromArgb(180, 220, 255);
                }
                else if (tenHang == "Yamaha")
                {
                    color1 = Color.FromArgb(230, 230, 255);
                    color2 = Color.FromArgb(200, 200, 255);
                }
                else if (tenHang == "Suzuki")
                {
                    color1 = Color.FromArgb(255, 240, 240);
                    color2 = Color.FromArgb(255, 220, 220);
                }

                System.Drawing.Drawing2D.LinearGradientBrush brush =
                    new System.Drawing.Drawing2D.LinearGradientBrush(
                        new Rectangle(0, 0, width, height), color1, color2, 45f);
                g.FillRectangle(brush, 0, 0, width, height);

                // Text
                string text = tenHang.ToUpper();
                Font font = new Font("Segoe UI", 24F, FontStyle.Bold);
                SizeF textSize = g.MeasureString(text, font);

                // Shadow
                g.DrawString(text, font, new SolidBrush(Color.FromArgb(50, 0, 0, 0)),
                    (width - textSize.Width) / 2 + 2, (height - textSize.Height) / 2 + 2);

                // Main text
                g.DrawString(text, font, Brushes.DarkGray,
                    (width - textSize.Width) / 2, (height - textSize.Height) / 2);

                // Bike icon (simple representation)
                DrawSimpleBikeIcon(g, width, height);
            }
            return bmp;
        }

        private void DrawSimpleBikeIcon(Graphics g, int width, int height)
        {
            // Vẽ icon xe máy đơn giản
            using (Pen pen = new Pen(Color.Gray, 3))
            {
                int centerX = width / 2;
                int centerY = height / 2 + 40;

                // Bánh xe trước
                g.DrawEllipse(pen, centerX - 60, centerY, 30, 30);
                // Bánh xe sau
                g.DrawEllipse(pen, centerX + 30, centerY, 30, 30);
                // Khung xe
                g.DrawLine(pen, centerX - 45, centerY + 15, centerX + 45, centerY + 15);
                g.DrawLine(pen, centerX - 20, centerY + 15, centerX - 10, centerY - 20);
            }
        }

        private void OnMuaClick(string tenXe, decimal giaBan, string idXe)
        {
            var result = MessageBox.Show(
                $"Ban muon mua xe: {tenXe}?\nGia: {giaBan:N0} VND\nMa xe: {idXe}",
                "Xac nhan mua xe",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Chuc nang dang phat trien. Se mo form dat mua xe.", "Thong bao");
            }
        }

        private void OnThueClick(string tenXe, decimal giaThueNgay, string idXe)
        {
            var result = MessageBox.Show(
                $"Ban muon thue xe: {tenXe}?\nGia: {giaThueNgay:N0} VND/ngay\nMa xe: {idXe}",
                "Xac nhan thue xe",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Chuc nang dang phat trien. Se mo form dat thue xe.", "Thong bao");
            }
        }

        // Giữ nguyên các method load data từ database
        private void LoadData()
        {
            try
            {
                DataTable dt;

                if (string.IsNullOrEmpty(currentFilter) || currentFilter == "Tất cả")
                {
                    dt = thongTinGiaXeBLL.GetXeForCustomer();
                }
                else
                {
                    dt = thongTinGiaXeBLL.GetXeForCustomer(currentFilter);
                }

                DisplayXeCards(dt);
                UpdateRecordCount(dt.Rows.Count);
            }
            catch (Exception ex)
            {
                // Nếu lỗi database, hiển thị hard-code
                MessageBox.Show("Khong ket noi duoc database. Hien thi du lieu mau.", "Thong bao");
                LoadHardCodedData();
            }
        }

        private void DisplayXeCards(DataTable dt)
        {
            flpXe.Controls.Clear();

            if (dt.Rows.Count == 0)
            {
                Label lblNoData = new Label
                {
                    Text = "Khong tim thay xe nao!",
                    Font = new Font("Segoe UI", 14F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true
                };
                flpXe.Controls.Add(lblNoData);
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                Panel cardPanel = CreateXeCard(row);
                flpXe.Controls.Add(cardPanel);
            }
        }

        private Panel CreateXeCard(DataRow row)
        {
            // Main card panel
            Panel card = new Panel
            {
                Width = 300,
                Height = 450,
                Margin = new Padding(10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            // Add shadow effect
            card.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(220, 220, 220), 1, ButtonBorderStyle.Solid);
            };

            // Image
            PictureBox picXe = new PictureBox
            {
                Width = 300,
                Height = 200,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(245, 245, 245),
                Dock = DockStyle.Top
            };

            string anhXe = row["AnhXeXeBan"].ToString();
            if (!string.IsNullOrEmpty(anhXe) && File.Exists(anhXe))
            {
                picXe.Image = Image.FromFile(anhXe);
            }
            else
            {
                // Placeholder image
                picXe.Image = CreatePlaceholderImage(300, 200, row["TenHang"].ToString());
            }

            card.Controls.Add(picXe);

            int yPosition = 210;

            // Tên xe
            string tenXe = $"{row["TenHang"]} {row["TenDong"]}";
            Label lblTenXe = new Label
            {
                Text = tenXe,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                AutoSize = false,
                Width = 270,
                Height = 30,
                Location = new Point(15, yPosition),
                TextAlign = ContentAlignment.MiddleLeft
            };
            card.Controls.Add(lblTenXe);
            yPosition += 35;

            // Chi tiết
            string chiTiet = $"Mau: {row["TenMau"]} | Nam: {row["NamSX"]} | {row["PhanKhoi"]}cc";
            Label lblChiTiet = new Label
            {
                Text = chiTiet,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                AutoSize = false,
                Width = 270,
                Height = 40,
                Location = new Point(15, yPosition)
            };
            card.Controls.Add(lblChiTiet);
            yPosition += 45;

            // Phân loại (Bán/Thuê)
            string phanLoai = row["PhanLoai"].ToString();
            Label lblPhanLoai = new Label
            {
                Text = phanLoai == "Bán" ? "Xe ban" : "Xe cho thue",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = phanLoai == "Bán" ? Color.FromArgb(76, 175, 80) : Color.FromArgb(33, 150, 243),
                AutoSize = true,
                Location = new Point(15, yPosition)
            };
            card.Controls.Add(lblPhanLoai);
            yPosition += 30;

            // Giá
            string giaText = "";
            if (phanLoai == "Bán")
            {
                decimal giaBan = Convert.ToDecimal(row["GiaBan"]);
                giaText = $"{giaBan:N0} VND";
            }
            else
            {
                decimal giaThue = Convert.ToDecimal(row["GiaThueNgay"]);
                giaText = $"{giaThue:N0} VND/ngay";
            }

            Label lblGia = new Label
            {
                Text = giaText,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(211, 47, 47),
                AutoSize = false,
                Width = 270,
                Height = 30,
                Location = new Point(15, yPosition),
                TextAlign = ContentAlignment.MiddleLeft
            };
            card.Controls.Add(lblGia);
            yPosition += 40;

            // Buttons Panel
            Panel btnPanel = new Panel
            {
                Location = new Point(15, yPosition),
                Width = 270,
                Height = 40
            };

            if (phanLoai == "Bán")
            {
                Button btnMua = new Button
                {
                    Text = "MUA NGAY",
                    Width = 270,
                    Height = 40,
                    BackColor = Color.FromArgb(76, 175, 80),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };
                btnMua.FlatAppearance.BorderSize = 0;
                btnMua.Click += (s, e) => OnMuaClick(tenXe, Convert.ToDecimal(row["GiaBan"]), row["ID_Xe"].ToString());
                btnPanel.Controls.Add(btnMua);
            }
            else
            {
                Button btnThue = new Button
                {
                    Text = "THUE NGAY",
                    Width = 270,
                    Height = 40,
                    BackColor = Color.FromArgb(33, 150, 243),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };
                btnThue.FlatAppearance.BorderSize = 0;
                btnThue.Click += (s, e) => OnThueClick(tenXe, Convert.ToDecimal(row["GiaThueNgay"]), row["ID_Xe"].ToString());
                btnPanel.Controls.Add(btnThue);
            }

            card.Controls.Add(btnPanel);

            return card;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            try
            {
                DataTable dt;
                if (string.IsNullOrEmpty(currentFilter) || currentFilter == "Tất cả")
                {
                    dt = thongTinGiaXeBLL.SearchXeForCustomer(keyword);
                }
                else
                {
                    dt = thongTinGiaXeBLL.SearchXeForCustomer(keyword, currentFilter);
                }

                DisplayXeCards(dt);
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
            txtSearch.Clear();
            cboFilter.SelectedIndex = 0;
            currentFilter = "";
            LoadHardCodedData(); // Refresh về dữ liệu mẫu
        }

        private void CboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cboFilter.SelectedItem?.ToString();
            currentFilter = (selected == "Tất cả") ? "" : selected;
            LoadData();
        }

        private void UpdateRecordCount(int count)
        {
            lblCount.Text = $"Tim thay {count} xe";
        }
    }
}