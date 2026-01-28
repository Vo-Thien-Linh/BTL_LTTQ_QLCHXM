using BLL;
using DTO;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Windows.Forms;

namespace UI.FormHandleUI
{
    public partial class FormBanPhuTung : Form
    {
        public int SoLuongBan => (int)nudSoLuong.Value;

        private readonly PhuTungBLL phuTungBLL = new PhuTungBLL();
        private readonly string _maPhuTung;
        private readonly string _tenPhuTung;
        private readonly int _tonKhoHienTai;
        private readonly decimal _giaBan;
        private readonly KhuyenMaiBLL _kmBLL = new KhuyenMaiBLL();
        private KhuyenMaiDTO _kmChon;
        private decimal _soTienGiamKm;

        // Thông tin cửa hàng
        private const string TenCuaHang = "CỬA HÀNG BÁN CHO THUÊ XE MÁY WINGS";
        private const string DiaChi = "451, Lê Văn Việt, Phường Tăng Nhơn Phú, TP.Hồ Chí Minh";

        // Thông tin động theo nhân viên đang đăng nhập
        private string DienThoai => $"ĐT: {CurrentUser.SoDienThoai ?? "0378.372.031"}";
        private string TenNhanVien => CurrentUser.HoTen ?? "Nhân viên";
        private string ThongTinNhanVien => $"Nhân viên: {TenNhanVien}";

        public FormBanPhuTung(string maPT, string tenPT, int tonKho, decimal giaBan)
        {
            InitializeComponent();

            _maPhuTung = maPT;
            _tenPhuTung = tenPT;
            _tonKhoHienTai = tonKho;
            _giaBan = giaBan;

            lblMaValue.Text = maPT;
            lblTenValue.Text = tenPT;
            lblTonKhoValue.Text = tonKho.ToString();
            lblGiaBanValue.Text = giaBan.ToString("N0");

            nudSoLuong.Maximum = tonKho;
            nudSoLuong.Minimum = 1;
            nudSoLuong.Value = 1;

            CapNhatNhanKm();

            TinhThanhTien();
        }

        private void TinhThanhTien()
        {
            decimal thanhTien = _giaBan * nudSoLuong.Value;
            decimal thanhSauGiam = Math.Max(thanhTien - _soTienGiamKm, 0);

            // Hiển thị thành tiền đã trừ khuyến mãi
            lblThanhTienValue.Text = $"{thanhSauGiam:N0} VNĐ";

            CapNhatNhanKm();
        }

        private void nudSoLuong_ValueChanged(object sender, EventArgs e)
        {
            _soTienGiamKm = 0;
            _kmChon = null;
            CapNhatNhanKm();
            TinhThanhTien();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormBanPhuTung_Load(object sender, EventArgs e)
        {
        }

        private void CapNhatNhanKm()
        {
            if (lblKmValue != null)
                lblKmValue.Text = _kmChon != null ? _kmChon.TenKM : "(Chưa áp)";
            if (lblGiamValue != null)
                lblGiamValue.Text = _soTienGiamKm > 0 ? $"{_soTienGiamKm:N0} đ" : "0 đ";

            if (txtKmValue != null)
                txtKmValue.Text = _kmChon != null ? _kmChon.TenKM : "(Chưa áp)";
            if (txtGiamValue != null)
                txtGiamValue.Text = _soTienGiamKm > 0 ? $"{_soTienGiamKm:N0} đ" : "0 đ";
        }

        /// <summary>
        /// Cho phép chọn khuyến mãi cho phụ tùng lẻ, trả về false nếu người dùng hủy.
        /// </summary>
        private bool ChonKhuyenMai(decimal thanhTien)
        {
            try
            {
                DataTable dt = _kmBLL.GetKhuyenMaiHieuLuc(DateTime.Now, "Phụ tùng");

                if (dt == null || dt.Rows.Count == 0)
                {
                    _kmChon = null;
                    _soTienGiamKm = 0;
                    return true;
                }

                Form picker = new Form
                {
                    Text = "Chọn khuyến mãi phụ tùng",
                    Size = new Size(420, 240),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                Label lblTitle = new Label
                {
                    Text = "Chọn khuyến mãi áp dụng",
                    AutoSize = true,
                    Location = new Point(16, 16)
                };

                ComboBox cboKm = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(16, 44),
                    Width = 360,
                    DataSource = dt,
                    DisplayMember = "TenKM",
                    ValueMember = "MaKM"
                };

                CheckBox chkNone = new CheckBox
                {
                    Text = "Không áp dụng khuyến mãi",
                    AutoSize = true,
                    Location = new Point(16, 78)
                };

                Label lblInfo = new Label
                {
                    AutoSize = true,
                    MaximumSize = new Size(360, 0),
                    Location = new Point(16, 110)
                };

                Button btnOk = new Button
                {
                    Text = "Đồng ý",
                    DialogResult = DialogResult.OK,
                    Location = new Point(200, 170),
                    Width = 80
                };

                Button btnCancel = new Button
                {
                    Text = "Hủy",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(296, 170),
                    Width = 80
                };

                picker.AcceptButton = btnOk;
                picker.CancelButton = btnCancel;

                void UpdateInfo()
                {
                    if (cboKm.SelectedValue == null)
                        return;

                    DataRow[] rows = dt.Select($"MaKM = '{cboKm.SelectedValue}'");
                    if (rows.Length == 0)
                        return;

                    DataRow r = rows[0];
                    string loai = r["LoaiKhuyenMai"]?.ToString() ?? "";
                    string info = $"Loại: {loai}";

                    if (loai == "Giảm %")
                    {
                        string pt = r["PhanTramGiam"] != DBNull.Value ? Convert.ToDecimal(r["PhanTramGiam"]).ToString("0.##") : "0";
                        string max = r["GiaTriGiamToiDa"] != DBNull.Value ? Convert.ToDecimal(r["GiaTriGiamToiDa"]).ToString("N0") : "Không giới hạn";
                        info += $" | -{pt}% (tối đa {max})";
                    }
                    else
                    {
                        string soTien = r["SoTienGiam"] != DBNull.Value ? Convert.ToDecimal(r["SoTienGiam"]).ToString("N0") : "0";
                        info += $" | -{soTien} đ";
                    }

                    lblInfo.Text = info;
                }

                cboKm.SelectedIndexChanged += (s, e) =>
                {
                    chkNone.Checked = false;
                    UpdateInfo();
                };

                chkNone.CheckedChanged += (s, e) =>
                {
                    if (chkNone.Checked)
                    {
                        cboKm.Enabled = false;
                        lblInfo.Text = "Không áp dụng";
                    }
                    else
                    {
                        cboKm.Enabled = true;
                        UpdateInfo();
                    }
                };

                picker.Controls.Add(lblTitle);
                picker.Controls.Add(cboKm);
                picker.Controls.Add(chkNone);
                picker.Controls.Add(lblInfo);
                picker.Controls.Add(btnOk);
                picker.Controls.Add(btnCancel);

                UpdateInfo();

                DialogResult dr = picker.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                    return false;

                if (chkNone.Checked)
                {
                    _kmChon = null;
                    _soTienGiamKm = 0;
                    CapNhatNhanKm();
                    return true;
                }

                DataRow[] selectedRows = dt.Select($"MaKM = '{cboKm.SelectedValue}'");
                if (selectedRows.Length == 0)
                {
                    MessageBox.Show("Không xác định được khuyến mãi đã chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                DataRow row = selectedRows[0];
                _kmChon = new KhuyenMaiDTO
                {
                    MaKM = row["MaKM"].ToString(),
                    TenKM = row["TenKM"].ToString(),
                    LoaiKhuyenMai = row["LoaiKhuyenMai"].ToString(),
                    PhanTramGiam = row["PhanTramGiam"] != DBNull.Value ? Convert.ToDecimal(row["PhanTramGiam"]) : (decimal?)null,
                    SoTienGiam = row["SoTienGiam"] != DBNull.Value ? Convert.ToDecimal(row["SoTienGiam"]) : (decimal?)null,
                    GiaTriGiamToiDa = row["GiaTriGiamToiDa"] != DBNull.Value ? Convert.ToDecimal(row["GiaTriGiamToiDa"]) : (decimal?)null,
                    LoaiApDung = row["LoaiApDung"].ToString()
                };

                string err;
                _soTienGiamKm = _kmBLL.TinhGiaTriGiam(_kmChon.MaKM, thanhTien, out err);
                if (!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show(err, "Khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                CapNhatNhanKm();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn khuyến mãi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int soLuongBan = (int)nudSoLuong.Value;
            int tonKhoMoi = _tonKhoHienTai - soLuongBan;

            decimal thanhTien = _giaBan * soLuongBan;
            if (!ChonKhuyenMai(thanhTien))
                return;
            decimal thanhSauGiam = Math.Max(thanhTien - _soTienGiamKm, 0);
            TinhThanhTien();

            if (phuTungBLL.UpdateKhoPhuTungSoLuong(_maPhuTung, tonKhoMoi))
            {
                MessageBox.Show($"Bán thành công {soLuongBan} sản phẩm!\nTồn kho còn lại: {tonKhoMoi}\nTiền hàng: {thanhTien:N0}\nKhuyến mãi: {_soTienGiamKm:N0}\nKhách trả: {thanhSauGiam:N0}",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult dr = MessageBox.Show("Bạn có muốn in hóa đơn không?", "In hóa đơn",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    InHoaDon();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi khi cập nhật tồn kho!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InHoaDon()
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += PrintPageHandler;

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = pd;
            preview.WindowState = FormWindowState.Maximized;
            preview.ShowDialog();
        }

        private void btnChonKm_Click(object sender, EventArgs e)
        {
            decimal thanhTien = _giaBan * nudSoLuong.Value;
            if (ChonKhuyenMai(thanhTien))
            {
                TinhThanhTien();
            }
        }

        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fontTitle = new Font("Arial", 18, FontStyle.Bold);
            Font fontNormal = new Font("Arial", 11);
            Font fontBold = new Font("Arial", 13, FontStyle.Bold);
            Font fontSmall = new Font("Arial", 10);
            Brush brush = Brushes.Black;

            StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Far };

            float pageWidth = e.MarginBounds.Width;  // Chiều rộng vùng in được
            float leftMargin = e.MarginBounds.Left;   // Lề trái
            float rightMargin = e.MarginBounds.Right;
            float y = e.MarginBounds.Top;             // Bắt đầu từ trên cùng vùng in
            float lineHeight = 30;                    // Khoảng cách giữa các dòng (tăng lên để thoáng)

            // === TIÊU ĐỀ CỬA HÀNG ===
            g.DrawString(TenCuaHang, fontTitle, brush, new RectangleF(leftMargin, y, pageWidth, 40), centerFormat);
            y += 45;

            g.DrawString(DiaChi, fontNormal, brush, new RectangleF(leftMargin, y, pageWidth, 30), centerFormat);
            y += 30;

            g.DrawString(DienThoai, fontNormal, brush, new RectangleF(leftMargin, y, pageWidth, 30), centerFormat);
            y += 50;

            // Đường kẻ ngang
            g.DrawLine(Pens.Black, leftMargin, y, rightMargin, y);
            y += 30;

            // === TIÊU ĐỀ HÓA ĐƠN ===
            g.DrawString("HÓA ĐƠN BÁN HÀNG", fontBold, brush, new RectangleF(leftMargin, y, pageWidth, 40), centerFormat);
            y += 50;

            g.DrawString($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm}", fontNormal, brush, leftMargin, y);
            y += lineHeight;

            g.DrawString(ThongTinNhanVien, fontNormal, brush, leftMargin, y);
            y += lineHeight + 20;

            // === CHI TIẾT SẢN PHẨM ===
            g.DrawString("Mã sản phẩm:", fontNormal, brush, leftMargin, y);
            g.DrawString(_maPhuTung, fontNormal, brush, leftMargin + 150, y);
            y += lineHeight;

            g.DrawString("Tên sản phẩm:", fontNormal, brush, leftMargin, y);
            g.DrawString(_tenPhuTung, fontNormal, brush, leftMargin + 150, y);
            y += lineHeight;

            g.DrawString("Số lượng:", fontNormal, brush, leftMargin, y);
            g.DrawString(SoLuongBan.ToString(), fontNormal, brush, leftMargin + 150, y);
            y += lineHeight;

            g.DrawString("Đơn giá:", fontNormal, brush, leftMargin, y);
            g.DrawString(_giaBan.ToString("N0") + " đ", fontNormal, brush, leftMargin + 150, y);
            y += lineHeight + 20;

            // Đường kẻ ngang dưới chi tiết
            g.DrawLine(Pens.Black, leftMargin, y, rightMargin, y);
            y += 30;

            // === TỔNG TIỀN (căn phải, nổi bật) ===
            decimal tongTien = _giaBan * SoLuongBan;
            decimal tongSauGiam = Math.Max(tongTien - _soTienGiamKm, 0);
            if (_soTienGiamKm > 0)
            {
                g.DrawString("Tổng trước giảm:", fontNormal, brush, leftMargin + 200, y);
                g.DrawString(tongTien.ToString("N0") + " VNĐ", fontNormal, brush, new RectangleF(leftMargin, y, pageWidth - 100, 30), rightFormat);
                y += lineHeight;

                g.DrawString($"Khuyến mãi ({_kmChon?.TenKM ?? ""}):", fontNormal, brush, leftMargin + 200, y);
                g.DrawString("- " + _soTienGiamKm.ToString("N0") + " VNĐ", fontNormal, brush, new RectangleF(leftMargin, y, pageWidth - 100, 30), rightFormat);
                y += lineHeight;
            }

            g.DrawString("KHÁCH TRẢ:", fontBold, brush, leftMargin + 300, y);
            g.DrawString(tongSauGiam.ToString("N0") + " VNĐ", fontBold, brush, new RectangleF(leftMargin, y, pageWidth - 100, 40), rightFormat);
            y += 60;

            // === LỜI CẢM ƠN ===
            g.DrawString("Xin chân thành cảm ơn quý khách!", fontNormal, brush, new RectangleF(leftMargin, y, pageWidth, 30), centerFormat);
            y += 35;
            g.DrawString("Hẹn gặp lại!", fontSmall, brush, new RectangleF(leftMargin, y, pageWidth, 30), centerFormat);

            // Giải phóng
            fontTitle.Dispose();
            fontNormal.Dispose();
            fontBold.Dispose();
            fontSmall.Dispose();
        }

        private void lblKm_Click(object sender, EventArgs e)
        {

        }

        private void txtGiamValue_TextChanged(object sender, EventArgs e)
        {

        }
    }
}