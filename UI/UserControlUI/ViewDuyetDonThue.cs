using BLL;
using DTO;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace UI.FormUI
{
    public partial class ViewDuyetDonThue : UserControl
    {
        private GiaoDichThueBLL giaoDichThueBLL;
        private HopDongThueBLL hopDongThueBLL;
        private string maNhanVien;
        private FlowLayoutPanel flpDonChoDuyet;
        private bool hasPermission = false; //  Thêm flag kiểm tra quyền

        public ViewDuyetDonThue()
        {
            InitializeComponent();
            giaoDichThueBLL = new GiaoDichThueBLL();
            hopDongThueBLL = new HopDongThueBLL();
            InitializeFlowLayoutPanel();
            
            btnLamMoi.Click += BtnLamMoi_Click;
        }

        public ViewDuyetDonThue(string maNV) : this()
        {
            this.maNhanVien = maNV;
        }

        public void SetMaNhanVien(string maNV)
        {
            this.maNhanVien = maNV;
            CheckPermission();
            LoadData();
        }

        //  KIỂM TRA QUYỀN KHI HIỂN THỊ
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            
            if (this.Visible)
            {
                //  KIỂM TRA ĐĂNG NHẬP
                if (CurrentUser.MaNV == null)
                {
                    ShowAccessDenied("Bạn chưa đăng nhập!\n\nVui lòng đăng nhập để tiếp tục.");
                    return;
                }
                
                // LẤY MÃ NHÂN VIÊN
                if (string.IsNullOrEmpty(maNhanVien))
                {
                    maNhanVien = CurrentUser.MaNV;
                }
                
                // KIỂM TRA QUYỀN
                CheckPermission();
                
                if (!hasPermission)
                {
                    ShowAccessDenied(
                        " KHÔNG CÓ QUYỀN TRUY CẬP!\n\n" +
                        "Chỉ Quản lý mới được phép duyệt đơn thuê xe.\n\n" +
                        $"Tài khoản của bạn: {CurrentUser.LoaiTaiKhoan}"
                    );
                    return;
                }
                
                // LOAD DATA NẾU CÓ QUYỀN
                LoadData();
            }
        }

        private void ViewDuyetDonThue_Load(object sender, EventArgs e)
        {
            if (CurrentUser.MaNV == null) return;
            
            if (string.IsNullOrEmpty(maNhanVien))
            {
                maNhanVien = CurrentUser.MaNV;
            }
            
            CheckPermission();
            
            if (hasPermission)
            {
                LoadData();
            }
        }

        ///  KIỂM TRA QUYỀN CỦA USER
        private void CheckPermission()
        {
            // Chỉ Quản lý mới được duyệt đơn
            hasPermission = (CurrentUser.LoaiTaiKhoan == "QuanLy");
        }

        ///  HIỂN THỊ THÔNG BÁO KHÔNG CÓ QUYỀN
        private void ShowAccessDenied(string message)
        {
            flpDonChoDuyet.Controls.Clear();
            
            Panel accessDeniedPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(236, 240, 241)
            };
            
            Label lblIcon = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 72F),
                ForeColor = Color.FromArgb(244, 67, 54),
                AutoSize = true,
                Location = new Point(
                    (flpDonChoDuyet.Width - 100) / 2, 
                    100
                )
            };
            
            Label lblMessage = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(66, 66, 66),
                AutoSize = false,
                Width = 600,
                Height = 150,
                TextAlign = ContentAlignment.TopCenter,
                Location = new Point(
                    (flpDonChoDuyet.Width - 600) / 2, 
                    200
                )
            };
            
            Button btnBack = new Button
            {
                Text = " Quay lại",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Width = 150,
                Height = 45,
                Location = new Point(
                    (flpDonChoDuyet.Width - 150) / 2, 
                    370
                ),
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) =>
            {
                // Quay về trang chủ hoặc trang trước
                if (this.Parent is TabControl tabControl)
                {
                    tabControl.SelectedIndex = 0;
                }
            };
            
            accessDeniedPanel.Controls.Add(lblIcon);
            accessDeniedPanel.Controls.Add(lblMessage);
            accessDeniedPanel.Controls.Add(btnBack);
            
            flpDonChoDuyet.Controls.Add(accessDeniedPanel);
            
            // Disable nút làm mới
            if (btnLamMoi != null)
            {
                btnLamMoi.Enabled = false;
            }
            
            // Cập nhật label
            lblTongSoDon.Text = "Tổng: 0 đơn chờ duyệt";
        }

        private void InitializeFlowLayoutPanel()
        {
            if (pnlMain.Controls.Contains(dgvDanhSachDonChoDuyet))
            {
                pnlMain.Controls.Remove(dgvDanhSachDonChoDuyet);
            }

            flpDonChoDuyet = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(236, 240, 241),
                Padding = new Padding(20),
                WrapContents = true
            };

            pnlMain.Controls.Add(flpDonChoDuyet);
            flpDonChoDuyet.BringToFront();
        }

        private void LoadData()
        {
            //  Kiểm tra quyền trước khi load
            if (!hasPermission)
            {
                return;
            }
            
            try
            {
                Cursor = Cursors.WaitCursor;
                
                DataTable dt = giaoDichThueBLL.GetGiaoDichThueByTrangThai("Chờ duyệt");

                DisplayCards(dt);

                lblTongSoDon.Text = $"Tổng: {dt.Rows.Count} đơn chờ duyệt";

                if (pnlChiTiet != null)
                {
                    pnlChiTiet.Visible = false;
                }
                
                // Enable lại nút làm mới
                if (btnLamMoi != null)
                {
                    btnLamMoi.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void DisplayCards(DataTable dt)
        {
            flpDonChoDuyet.Controls.Clear();

            if (dt.Rows.Count == 0)
            {
                Label lblNoData = new Label
                {
                    Text = " Không có đơn nào chờ duyệt!",
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(76, 175, 80),
                    AutoSize = true,
                    Padding = new Padding(50)
                };
                flpDonChoDuyet.Controls.Add(lblNoData);
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                Panel card = CreateCard(row);
                flpDonChoDuyet.Controls.Add(card);
            }
        }

        private Panel CreateCard(DataRow row)
        {
            int maGDThue = Convert.ToInt32(row["MaGDThue"]);
            string idXe = row["ID_Xe"].ToString();
            DateTime ngayBatDau = Convert.ToDateTime(row["NgayBatDau"]);
            DateTime ngayKetThuc = Convert.ToDateTime(row["NgayKetThuc"]);

            string errorMessage;
            bool isXeTrung = giaoDichThueBLL.IsXeDangThue(idXe, ngayBatDau, ngayKetThuc, out errorMessage);
            bool isNgayQuaHan = ngayKetThuc.Date < DateTime.Now.Date;

            Panel card = new Panel
            {
                Width = 360,
                Height = 520,
                Margin = new Padding(10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Tag = maGDThue
            };

            Color borderColor = isXeTrung || isNgayQuaHan
                ? Color.FromArgb(244, 67, 54)
                : Color.FromArgb(255, 152, 0);

            card.Paint += (s, e) =>
            {
                int borderWidth = isXeTrung || isNgayQuaHan ? 3 : 2;
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    borderColor, borderWidth, ButtonBorderStyle.Solid,
                    borderColor, borderWidth, ButtonBorderStyle.Solid,
                    borderColor, borderWidth, ButtonBorderStyle.Solid,
                    borderColor, borderWidth, ButtonBorderStyle.Solid);
            };

            int yPos = 10;

            Panel badgePanel = new Panel
            {
                Location = new Point(10, yPos),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(255, 152, 0)
            };

            Label lblBadge = new Label
            {
                Text = "CHỜ DUYỆT",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            badgePanel.Controls.Add(lblBadge);
            card.Controls.Add(badgePanel);
            yPos += 40;

            // Cảnh báo (nếu có)
            if (isXeTrung || isNgayQuaHan)
            {
                Panel warningPanel = new Panel
                {
                    Location = new Point(0, yPos),
                    Width = 360,
                    Height = 30,
                    BackColor = Color.FromArgb(244, 67, 54)
                };

                string warningText = isNgayQuaHan
                    ? " ĐƠN ĐÃ QUÁ HẠN!"
                    : " XE TRÙNG LỊCH!";

                Label lblWarning = new Label
                {
                    Text = warningText,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.White,
                    AutoSize = false,
                    Width = 360,
                    Height = 30,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                warningPanel.Controls.Add(lblWarning);
                card.Controls.Add(warningPanel);
                yPos += 35;
            }

            // Image (ảnh xe - giảm chiều cao)
            PictureBox picXe = new PictureBox
            {
                Width = 340,
                Height = 140,
                Location = new Point(10, yPos),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(245, 245, 245)
            };

            // Load ảnh nếu có
            if (row.Table.Columns.Contains("AnhXeXeBan"))
            {
                string anhXe = row["AnhXeXeBan"].ToString();
                if (!string.IsNullOrEmpty(anhXe) && File.Exists(anhXe))
                {
                    picXe.Image = Image.FromFile(anhXe);
                }
                else
                {
                    picXe.Image = CreatePlaceholderImage(340, 140, row["TenXe"].ToString());
                }
            }

            card.Controls.Add(picXe);
            yPos += 150;

            // Mã GD
            Label lblMaGD = new Label
            {
                Text = $"Đơn #{maGDThue}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                Location = new Point(15, yPos),
                AutoSize = true
            };
            card.Controls.Add(lblMaGD);
            yPos += 25;

            // Tên xe
            Label lblTenXe = new Label
            {
                Text = row["TenXe"].ToString(),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                AutoSize = false,
                Width = 330,
                Height = 25,
                Location = new Point(15, yPos)
            };
            card.Controls.Add(lblTenXe);
            yPos += 30;

            // Biển số
            Label lblBienSo = new Label
            {
                Text = $" Biển số: {row["BienSo"]}",
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
                Text = $" {row["HoTenKH"]}",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(66, 66, 66),
                AutoSize = false,
                Width = 200,
                Height = 20,
                Location = new Point(15, yPos)
            };
            card.Controls.Add(lblKhachHang);

            Label lblSdt = new Label
            {
                Text = $" {row["SdtKhachHang"]}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                Location = new Point(220, yPos),
                AutoSize = true
            };
            card.Controls.Add(lblSdt);
            yPos += 25;

            // Ngày thuê
            Label lblNgayThue = new Label
            {
                Text = $" {ngayBatDau:dd/MM/yyyy} → {ngayKetThuc:dd/MM/yyyy}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                Location = new Point(15, yPos),
                AutoSize = true
            };
            card.Controls.Add(lblNgayThue);

            int soNgayThue = (ngayKetThuc - ngayBatDau).Days;
            Label lblSoNgay = new Label
            {
                Text = $"({soNgayThue} ngày)",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.Gray,
                Location = new Point(280, yPos),
                AutoSize = true
            };
            card.Controls.Add(lblSoNgay);
            yPos += 30;

            // Giá thuê
            decimal tongGia = Convert.ToDecimal(row["TongGia"]);
            Label lblGia = new Label
            {
                Text = $"{tongGia:N0} VNĐ",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(76, 175, 80),
                Location = new Point(15, yPos),
                AutoSize = true
            };
            card.Controls.Add(lblGia);
            yPos += 40;

            // Button Duyệt - CHỈ ENABLED KHI CÓ QUYỀN + XE HỢP LỆ
            Button btnDuyet = new Button
            {
                Text = hasPermission ? "DUYỆT ĐƠN" : "KHÔNG CÓ QUYỀN",
                Width = 330,
                Height = 40,
                Location = new Point(15, yPos),
                BackColor = hasPermission ? Color.FromArgb(76, 175, 80) : Color.Gray,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = hasPermission ? Cursors.Hand : Cursors.No,
                Enabled = hasPermission && !isXeTrung && !isNgayQuaHan  //  THÊM CHECK QUYỀN
            };
            btnDuyet.FlatAppearance.BorderSize = 0;
            if (hasPermission)
            {
                btnDuyet.Click += (s, e) => DuyetDonFromCard(row);
            }
            card.Controls.Add(btnDuyet);
            yPos += 45;

            //  Button Từ chối - CHỈ ENABLED KHI CÓ QUYỀN
            Button btnTuChoi = new Button
            {
                Text = hasPermission ? " TỪ CHỐI" : "",
                Width = 160,
                Height = 35,
                Location = new Point(15, yPos),
                BackColor = hasPermission ? Color.FromArgb(244, 67, 54) : Color.Gray,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = hasPermission ? Cursors.Hand : Cursors.No,
                Enabled = hasPermission  //  THÊM CHECK QUYỀN
            };
            btnTuChoi.FlatAppearance.BorderSize = 0;
            if (hasPermission)
            {
                btnTuChoi.Click += (s, e) => TuChoiDonFromCard(row);
            }
            card.Controls.Add(btnTuChoi);

            // Button Xem chi tiết - AI CŨNG XEM ĐƯỢC
            Button btnXemChiTiet = new Button
            {
                Text = " CHI TIẾT",
                Width = 160,
                Height = 35,
                Location = new Point(185, yPos),
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = true  //  Ai cũng xem được chi tiết
            };
            btnXemChiTiet.FlatAppearance.BorderSize = 0;
            btnXemChiTiet.Click += (s, e) => XemChiTietFromCard(maGDThue);
            card.Controls.Add(btnXemChiTiet);

            return card;
        }

        private Image CreatePlaceholderImage(int width, int height, string text)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(245, 245, 245));
                Font font = new Font("Segoe UI", 10F, FontStyle.Bold);
                SizeF textSize = g.MeasureString(text, font);
                g.DrawString(text, font, Brushes.Gray,
                    (width - textSize.Width) / 2, (height - textSize.Height) / 2);
            }
            return bmp;
        }

        private void DuyetDonFromCard(DataRow row)
        {
            //  Double-check quyền
            if (!hasPermission)
            {
                MessageBox.Show("Bạn không có quyền duyệt đơn!", "Từ chối",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            int maGDThue = Convert.ToInt32(row["MaGDThue"]);

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn DUYỆT đơn #{maGDThue}?\n\n" +
                $"Khách hàng: {row["HoTenKH"]}\n" +
                $"Xe: {row["TenXe"]} ({row["BienSo"]})\n" +
                $"Thời gian: {Convert.ToDateTime(row["NgayBatDau"]):dd/MM/yyyy} - {Convert.ToDateTime(row["NgayKetThuc"]):dd/MM/yyyy}\n" +
                $"Tổng tiền: {Convert.ToDecimal(row["TongGia"]):N0} VNĐ\n\n" +
                $"Sau khi duyệt, hệ thống sẽ tự động tạo hợp đồng.",
                "Xác nhận duyệt đơn",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                string ghiChu = $"Duyệt bởi {maNhanVien} ({CurrentUser.HoTen}) lúc {DateTime.Now:dd/MM/yyyy HH:mm}";
                string errorMessage;

                bool success = giaoDichThueBLL.ApproveGiaoDichThue(
                    maGDThue,
                    maNhanVien,
                    ghiChu,
                    out errorMessage
                );

                if (success)
                {
                    MessageBox.Show(
                        $" Đã duyệt đơn #{maGDThue} thành công!\n\n" +
                        $"Trạng thái mới: Chờ giao xe\n" +
                        $"Hợp đồng đã được tạo tự động.\n" +
                        $"Người duyệt: {CurrentUser.HoTen} ({maNhanVien})\n" +
                        $"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm}",
                        "Duyệt thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    LoadData();
                }
                else
                {
                    MessageBox.Show(
                        "Không thể duyệt đơn!\n\n" + errorMessage,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void TuChoiDonFromCard(DataRow row)
        {
            // Double-check quyền
            if (!hasPermission)
            {
                MessageBox.Show("Bạn không có quyền từ chối đơn!", "Từ chối",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            int maGDThue = Convert.ToInt32(row["MaGDThue"]);

            using (Form formLyDo = new Form
            {
                Text = "Nhập lý do từ chối",
                Width = 450,
                Height = 250,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            })
            {
                Label lblPrompt = new Label
                {
                    Text = $"Lý do từ chối đơn #{maGDThue}:",
                    Location = new Point(20, 20),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };

                TextBox txtLyDo = new TextBox
                {
                    Location = new Point(20, 50),
                    Width = 390,
                    Height = 80,
                    Multiline = true,
                    Font = new Font("Segoe UI", 10F)
                };

                Button btnOK = new Button
                {
                    Text = "Xác nhận",
                    Location = new Point(150, 150),
                    Width = 120,
                    Height = 35,
                    BackColor = Color.FromArgb(244, 67, 54),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    DialogResult = DialogResult.OK
                };

                Button btnCancel = new Button
                {
                    Text = "Hủy",
                    Location = new Point(280, 150),
                    Width = 120,
                    Height = 35,
                    BackColor = Color.Gray,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    DialogResult = DialogResult.Cancel
                };

                formLyDo.Controls.Add(lblPrompt);
                formLyDo.Controls.Add(txtLyDo);
                formLyDo.Controls.Add(btnOK);
                formLyDo.Controls.Add(btnCancel);
                formLyDo.AcceptButton = btnOK;

                if (formLyDo.ShowDialog() == DialogResult.OK)
                {
                    string lyDo = txtLyDo.Text.Trim();

                    if (string.IsNullOrWhiteSpace(lyDo))
                    {
                        MessageBox.Show("Vui lòng nhập lý do từ chối!", "Cảnh báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string errorMessage;
                    bool success = giaoDichThueBLL.RejectGiaoDichThue(
                        maGDThue,
                        maNhanVien,
                        lyDo,
                        out errorMessage
                    );

                    if (success)
                    {
                        MessageBox.Show(
                            $" Đã từ chối đơn #{maGDThue}!\n\n" +
                            $"Lý do: {lyDo}\n" +
                            $"Người từ chối: {CurrentUser.HoTen} ({maNhanVien})",
                            "Từ chối thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không thể từ chối đơn!\n\n" + errorMessage,
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void XemChiTietFromCard(int maGDThue)
        {
            try
            {
                using (FormXemHopDongThue form = new FormXemHopDongThue(maGDThue, maNhanVien))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form chi tiết: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            if (hasPermission)
            {
                LoadData();
            }
        }

        // Giữ lại các method cũ để tương thích
        private void DgvDanhSachDonChoDuyet_SelectionChanged(object sender, EventArgs e) { }
        private void DgvDanhSachDonChoDuyet_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { }
        private void ShowDetailInfo(DataGridViewRow row) { }
        private void CheckXeAvailability(string idXe, DateTime ngayBatDau, DateTime ngayKetThuc) { }
        private void BtnXemChiTiet_Click(object sender, EventArgs e) { }
        private void XemChiTietDonThue(int maGD) { }
        private void BtnDuyetDon_Click(object sender, EventArgs e) { }
        private void BtnTuChoiDon_Click(object sender, EventArgs e) { }
        private void ClearDetailInfo() { }
    }
}