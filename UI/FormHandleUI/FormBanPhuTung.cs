using BLL;
using DTO;
using System;
using System.Drawing;
using System.Drawing.Printing;
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

            TinhThanhTien();
        }

        private void TinhThanhTien()
        {
            decimal thanhTien = _giaBan * nudSoLuong.Value;
            lblThanhTienValue.Text = thanhTien.ToString("N0");
        }

        private void nudSoLuong_ValueChanged(object sender, EventArgs e)
        {
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            int soLuongBan = (int)nudSoLuong.Value;
            int tonKhoMoi = _tonKhoHienTai - soLuongBan;

            if (phuTungBLL.UpdateKhoPhuTungSoLuong(_maPhuTung, tonKhoMoi))
            {
                MessageBox.Show($"Bán thành công {soLuongBan} sản phẩm!\nTồn kho còn lại: {tonKhoMoi}",
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
            g.DrawString("TỔNG TIỀN:", fontBold, brush, leftMargin + 300, y);
            g.DrawString(tongTien.ToString("N0") + " VNĐ", fontBold, brush, new RectangleF(leftMargin, y, pageWidth - 100, 40), rightFormat);
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
    }
}