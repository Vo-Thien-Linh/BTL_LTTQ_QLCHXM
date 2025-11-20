using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace UI.FormHandleUI
{
    public partial class FormTraXe : Form
    {
        // Public properties để export dữ liệu
        public string TinhTrangXe { get; private set; }
        public decimal ChiPhiPhatSinh { get; private set; }
        public int KmKetThuc { get; private set; }
        public bool IsTraSom { get; private set; }
        public int SoNgayTraSom { get; private set; }
        public decimal TienHoanTraSom { get; private set; }
        public decimal TienPhat { get; private set; }
        public decimal TienHoanCoc { get; private set; }
        public string GhiChu { get; private set; }

        // Private fields
        private DataRow dataGiaoDich;
        private int kmBatDau;
        private DateTime ngayBatDau;
        private DateTime ngayKetThuc;
        private decimal giaThueNgay;
        private decimal soTienCoc;

        public FormTraXe(DataRow row)
        {
            InitializeComponent();
            this.dataGiaoDich = row;
            LoadDataFromRow();
            SetDefaultValues();
            SetupEvents();
            TinhToan(); // Tính toán ban đầu
        }

        private void LoadDataFromRow()
        {
            try
            {
                // ✅ ĐỌC DỮ LIỆU TỪ DataRow
                kmBatDau = dataGiaoDich["KmBatDau"] != DBNull.Value
                    ? Convert.ToInt32(dataGiaoDich["KmBatDau"]) : 0;
                ngayBatDau = Convert.ToDateTime(dataGiaoDich["NgayBatDau"]);
                ngayKetThuc = Convert.ToDateTime(dataGiaoDich["NgayKetThuc"]);
                giaThueNgay = Convert.ToDecimal(dataGiaoDich["GiaThueNgay"]);
                soTienCoc = dataGiaoDich["SoTienCoc"] != DBNull.Value
                    ? Convert.ToDecimal(dataGiaoDich["SoTienCoc"]) : 0;

                // ✅ DEBUG: Hiển thị giá trị đọc được
                System.Diagnostics.Debug.WriteLine($"[FormTraXe] KmBatDau: {kmBatDau}");
                System.Diagnostics.Debug.WriteLine($"[FormTraXe] NgayBatDau: {ngayBatDau:dd/MM/yyyy}");
                System.Diagnostics.Debug.WriteLine($"[FormTraXe] NgayKetThuc: {ngayKetThuc:dd/MM/yyyy}");
                System.Diagnostics.Debug.WriteLine($"[FormTraXe] GiaThueNgay: {giaThueNgay:N0}");
                System.Diagnostics.Debug.WriteLine($"[FormTraXe] SoTienCoc: {soTienCoc:N0}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi đọc dữ liệu: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultValues()
        {
            // ✅ SỬA: SET INCREMENT = 1 CHO CÁC NumericUpDown
            nudChiPhiPhatSinh.Increment = 1000;   // Tăng 1,000đ mỗi lần
            nudKmKetThuc.Increment = 1;           // Tăng 1 km mỗi lần
            nudSoNgayTraSom.Increment = 1;        // Tăng 1 ngày mỗi lần

            // Ngày trả
            dtpNgayTra.Value = DateTime.Now;
            dtpNgayTra.MinDate = ngayBatDau; // Không cho chọn ngày trả < ngày thuê

            // Km kết thúc
            nudKmKetThuc.Minimum = kmBatDau;
            nudKmKetThuc.Value = kmBatDau;
            nudKmKetThuc.Maximum = 999999;

            // Chi phí
            nudChiPhiPhatSinh.Value = 0;
            nudChiPhiPhatSinh.Minimum = 0;
            nudChiPhiPhatSinh.Maximum = 999999999;

            // Trả sớm
            chkTraSom.Checked = false;
            nudSoNgayTraSom.Enabled = false;
            nudSoNgayTraSom.Value = 0;
            nudSoNgayTraSom.Minimum = 0;

            // Tình trạng
            if (cboTinhTrang.Items.Count > 0)
            {
                cboTinhTrang.SelectedIndex = 0;
            }

            // ✅ HIỂN THỊ THÔNG TIN BAN ĐẦU (QUAN TRỌNG!)
            lblKmBatDauValue.Text = $"{kmBatDau:N0} km";
            lblNgayBatDauValue.Text = ngayBatDau.ToString("dd/MM/yyyy");
            lblNgayKetThucValue.Text = ngayKetThuc.ToString("dd/MM/yyyy");
            lblSoTienCocValue.Text = $"{soTienCoc:N0} VNĐ";
            lblGiaThueNgayValue.Text = $"{giaThueNgay:N0} VNĐ/ngày";
        }

        private void SetupEvents()
        {
            nudKmKetThuc.ValueChanged += (s, e) => TinhToan();
            nudChiPhiPhatSinh.ValueChanged += (s, e) => TinhToan();
            chkTraSom.CheckedChanged += ChkTraSom_CheckedChanged;
            nudSoNgayTraSom.ValueChanged += (s, e) => TinhToan();
            dtpNgayTra.ValueChanged += (s, e) => OnNgayTraChanged();
        }

        private void OnNgayTraChanged()
        {
            // Nếu ngày trả > ngày kết thúc ban đầu => TỰ ĐỘNG BỎ TICK "TRẢ SỚM"
            if (dtpNgayTra.Value.Date > ngayKetThuc.Date)
            {
                if (chkTraSom.Checked)
                {
                    chkTraSom.Checked = false;
                    MessageBox.Show(
                        "⚠ Ngày trả xe đã QUÁ HẠN so với hợp đồng!\n\n" +
                        "Đã TỰ ĐỘNG BỎ CHỌN 'Trả sớm' và sẽ tính PHÍ PHẠT trả muộn.",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                chkTraSom.Enabled = false;
            }
            else
            {
                chkTraSom.Enabled = true;
            }

            TinhToan();
        }

        private void ChkTraSom_CheckedChanged(object sender, EventArgs e)
        {
            nudSoNgayTraSom.Enabled = chkTraSom.Checked;

            if (!chkTraSom.Checked)
            {
                nudSoNgayTraSom.Value = 0;
            }
            else
            {
                DateTime ngayTraThucTe = dtpNgayTra.Value.Date;

                if (ngayTraThucTe > ngayKetThuc.Date)
                {
                    MessageBox.Show(
                        "⚠ KHÔNG THỂ TRẢ SỚM!\n\n" +
                        $"Ngày trả thực tế: {ngayTraThucTe:dd/MM/yyyy}\n" +
                        $"Ngày kết thúc hợp đồng: {ngayKetThuc:dd/MM/yyyy}\n\n" +
                        "Ngày trả xe đã QUÁ HẠN so với hợp đồng!",
                        "Không thể trả sớm",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    chkTraSom.Checked = false;
                    return;
                }

                int soNgayDaThue = (ngayTraThucTe - ngayBatDau.Date).Days;
                int soNgayThueMax = (ngayKetThuc.Date - ngayBatDau.Date).Days;
                int soNgayCoTheTraSom = soNgayThueMax - soNgayDaThue;

                if (soNgayCoTheTraSom > 0)
                {
                    nudSoNgayTraSom.Maximum = soNgayCoTheTraSom;
                    nudSoNgayTraSom.Value = Math.Min(1, soNgayCoTheTraSom);
                }
                else
                {
                    nudSoNgayTraSom.Maximum = 0;
                    MessageBox.Show(
                        "⚠ Không thể trả sớm!\n\n" +
                        $"Ngày trả: {ngayTraThucTe:dd/MM/yyyy}\n" +
                        $"Ngày kết thúc: {ngayKetThuc:dd/MM/yyyy}",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    chkTraSom.Checked = false;
                }
            }

            TinhToan();
        }

        private void TinhToan()
        {
            try
            {
                // 1. Tính km đã chạy
                int kmChay = (int)nudKmKetThuc.Value - kmBatDau;
                lblKmChayValue.Text = kmChay >= 0 ? $"{kmChay:N0} km" : "⚠ Lỗi";
                lblKmChayValue.ForeColor = kmChay >= 0 
                    ? Color.FromArgb(33, 150, 243) 
                    : Color.Red;

                // 2. Tính tiền phạt (nếu trả muộn)
                DateTime ngayTraThucTe = dtpNgayTra.Value.Date;
                int soNgayQuaHan = (ngayTraThucTe - ngayKetThuc.Date).Days;
                decimal tienPhat = 0;

                if (soNgayQuaHan > 0)
                {
                    tienPhat = soNgayQuaHan * giaThueNgay * 1.5m;
                    lblTienPhatValue.Text = $"{tienPhat:N0} VNĐ";
                    lblTienPhatValue.ForeColor = Color.FromArgb(244, 67, 54);
                    lblSoNgayQuaHanValue.Text = $"({soNgayQuaHan} ngày × {giaThueNgay:N0} × 150%)";
                    lblSoNgayQuaHanValue.Visible = true;
                }
                else
                {
                    lblTienPhatValue.Text = "0 VNĐ";
                    lblTienPhatValue.ForeColor = Color.Gray;
                    lblSoNgayQuaHanValue.Visible = false;
                }

                // 3. Tính tiền hoàn (nếu trả sớm)
                decimal tienHoanTraSom = 0;

                if (chkTraSom.Checked && nudSoNgayTraSom.Value > 0)
                {
                    tienHoanTraSom = (int)nudSoNgayTraSom.Value * giaThueNgay * 0.7m;
                    lblTienHoanTraSomValue.Text = $"{tienHoanTraSom:N0} VNĐ";
                    lblTienHoanTraSomValue.ForeColor = Color.FromArgb(76, 175, 80);
                    lblSoNgayTraSomDetail.Text = $"({nudSoNgayTraSom.Value} ngày × {giaThueNgay:N0} × 70%)";
                    lblSoNgayTraSomDetail.Visible = true;
                }
                else
                {
                    lblTienHoanTraSomValue.Text = "0 VNĐ";
                    lblTienHoanTraSomValue.ForeColor = Color.Gray;
                    lblSoNgayTraSomDetail.Visible = false;
                }

                // 4. Tính tổng tiền hoàn cọc
                decimal chiPhiPhatSinh = nudChiPhiPhatSinh.Value;
                decimal tienHoanCoc = soTienCoc - chiPhiPhatSinh - tienPhat + tienHoanTraSom;

                lblTienHoanCocValue.Text = $"{tienHoanCoc:N0} VNĐ";

                if (tienHoanCoc > 0)
                {
                    lblTienHoanCocValue.ForeColor = Color.FromArgb(76, 175, 80);
                    lblKetLuan.Text = "✓ Hoàn lại cho khách hàng";
                    lblKetLuan.ForeColor = Color.FromArgb(76, 175, 80);
                }
                else if (tienHoanCoc < 0)
                {
                    lblTienHoanCocValue.ForeColor = Color.FromArgb(244, 67, 54);
                    lblKetLuan.Text = $"⚠ Khách hàng phải trả thêm: {Math.Abs(tienHoanCoc):N0} VNĐ";
                    lblKetLuan.ForeColor = Color.FromArgb(244, 67, 54);
                }
                else
                {
                    lblTienHoanCocValue.ForeColor = Color.Gray;
                    lblKetLuan.Text = "= Không hoàn, không thu thêm";
                    lblKetLuan.ForeColor = Color.Gray;
                }

                // Lưu lại để export
                TienPhat = tienPhat;
                TienHoanTraSom = tienHoanTraSom;
                TienHoanCoc = tienHoanCoc;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi tính toán: {ex.Message}");
            }
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            string summary = BuildSummary();

            DialogResult confirm = MessageBox.Show(
                summary,
                "Xác nhận trả xe",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                TinhTrangXe = cboTinhTrang.SelectedItem?.ToString() ?? "Không rõ";
                ChiPhiPhatSinh = nudChiPhiPhatSinh.Value;
                KmKetThuc = (int)nudKmKetThuc.Value;
                IsTraSom = chkTraSom.Checked;
                SoNgayTraSom = (int)nudSoNgayTraSom.Value;
                GhiChu = txtGhiChu.Text.Trim();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private string BuildSummary()
        {
            string summary = "⚠ Xác nhận trả xe với thông tin sau?\n\n";
            summary += $"📅 Ngày trả: {dtpNgayTra.Value:dd/MM/yyyy HH:mm}\n";
            summary += $"🛣 Km kết thúc: {nudKmKetThuc.Value:N0} km (chạy {(int)nudKmKetThuc.Value - kmBatDau:N0} km)\n";
            summary += $"🔧 Tình trạng: {cboTinhTrang.SelectedItem}\n";
            summary += $"💰 Chi phí phát sinh: {nudChiPhiPhatSinh.Value:N0} VNĐ\n\n";

            summary += "💵 TÍNH TOÁN:\n";
            summary += $"  • Tiền cọc ban đầu: {soTienCoc:N0} VNĐ\n";
            summary += $"  • Chi phí phát sinh: -{nudChiPhiPhatSinh.Value:N0} VNĐ\n";

            if (TienPhat > 0)
            {
                int soNgayQuaHan = (dtpNgayTra.Value.Date - ngayKetThuc.Date).Days;
                summary += $"  • ⚠ Tiền phạt trả muộn ({soNgayQuaHan} ngày): -{TienPhat:N0} VNĐ\n";
            }

            if (TienHoanTraSom > 0)
            {
                summary += $"  • ✓ Tiền hoàn trả sớm ({SoNgayTraSom} ngày): +{TienHoanTraSom:N0} VNĐ\n";
            }

            summary += $"\n💳 TỔNG TIỀN HOÀN CỌC: {TienHoanCoc:N0} VNĐ\n";

            if (TienHoanCoc < 0)
            {
                summary += $"\n⚠ Khách hàng cần trả thêm: {Math.Abs(TienHoanCoc):N0} VNĐ";
            }

            return summary;
        }

        private bool ValidateInput()
        {
            if (nudKmKetThuc.Value < kmBatDau)
            {
                MessageBox.Show(
                    $"⚠ Km kết thúc ({nudKmKetThuc.Value:N0}) không thể nhỏ hơn km bắt đầu ({kmBatDau:N0})!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudKmKetThuc.Focus();
                return false;
            }

            if (cboTinhTrang.SelectedIndex == -1)
            {
                MessageBox.Show("⚠ Vui lòng chọn tình trạng xe!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTinhTrang.Focus();
                return false;
            }

            if (chkTraSom.Checked && dtpNgayTra.Value.Date > ngayKetThuc.Date)
            {
                MessageBox.Show(
                    "⚠ Không thể vừa TRẢ MUỘN vừa TÍNH TIỀN TRẢ SỚM!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}