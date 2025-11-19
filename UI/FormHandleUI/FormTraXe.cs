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
                kmBatDau = dataGiaoDich["KmBatDau"] != DBNull.Value
                    ? Convert.ToInt32(dataGiaoDich["KmBatDau"]) : 0;
                ngayBatDau = Convert.ToDateTime(dataGiaoDich["NgayBatDau"]);
                ngayKetThuc = Convert.ToDateTime(dataGiaoDich["NgayKetThuc"]);
                giaThueNgay = Convert.ToDecimal(dataGiaoDich["GiaThueNgay"]);
                soTienCoc = dataGiaoDich["SoTienCoc"] != DBNull.Value
                    ? Convert.ToDecimal(dataGiaoDich["SoTienCoc"]) : 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đọc dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultValues()
        {
            // Ngày trả
            dtpNgayTra.Value = DateTime.Now;

            // Km kết thúc
            nudKmKetThuc.Minimum = kmBatDau;
            nudKmKetThuc.Value = kmBatDau;

            // Chi phí
            nudChiPhiPhatSinh.Value = 0;

            // Trả sớm
            chkTraSom.Checked = false;
            nudSoNgayTraSom.Enabled = false;
            nudSoNgayTraSom.Value = 0;

            // Tình trạng
            cboTinhTrang.SelectedIndex = 0;

            // Hiển thị thông tin ban đầu
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
                // Tính số ngày tối đa có thể trả sớm
                int soNgayDaThue = (DateTime.Now.Date - ngayBatDau.Date).Days;
                int soNgayThueMax = (ngayKetThuc.Date - ngayBatDau.Date).Days;
                int soNgayCoTheTraSom = soNgayThueMax - soNgayDaThue;

                if (soNgayCoTheTraSom > 0)
                {
                    nudSoNgayTraSom.Maximum = soNgayCoTheTraSom;
                }
                else
                {
                    nudSoNgayTraSom.Maximum = 0;
                    MessageBox.Show(
                        "Không thể trả sớm vì đã quá ngày kết thúc!",
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
                //  Tính km đã chạy
                int kmChay = (int)nudKmKetThuc.Value - kmBatDau;
                lblKmChayValue.Text = kmChay >= 0
                    ? $"{kmChay:N0} km"
                    : " Lỗi: km < ban đầu";
                lblKmChayValue.ForeColor = kmChay >= 0
                    ? Color.FromArgb(33, 150, 243)
                    : Color.Red;

                //  Tính tiền phạt (nếu trả muộn)
                int soNgayQuaHan = (DateTime.Now.Date - ngayKetThuc.Date).Days;
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

                //  Tính tiền hoàn (nếu trả sớm)
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

                //  Tính tổng tiền hoàn cọc
                decimal chiPhiPhatSinh = nudChiPhiPhatSinh.Value;
                decimal tienHoanCoc = soTienCoc - chiPhiPhatSinh - tienPhat + tienHoanTraSom;

                lblTienHoanCocValue.Text = $"{tienHoanCoc:N0} VNĐ";

                if (tienHoanCoc > 0)
                {
                    lblTienHoanCocValue.ForeColor = Color.FromArgb(76, 175, 80);
                    lblKetLuan.Text = " Hoàn lại cho khách hàng";
                    lblKetLuan.ForeColor = Color.FromArgb(76, 175, 80);
                }
                else if (tienHoanCoc < 0)
                {
                    lblTienHoanCocValue.ForeColor = Color.FromArgb(244, 67, 54);
                    lblKetLuan.Text = $" Khách hàng phải trả thêm: {Math.Abs(tienHoanCoc):N0} VNĐ";
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
                System.Diagnostics.Debug.WriteLine($"Lỗi tính toán: {ex.Message}");
            }
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            // Hiển thị tóm tắt
            string summary = BuildSummary();

            DialogResult confirm = MessageBox.Show(
                summary,
                "Xác nhận trả xe",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                // Lưu dữ liệu
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
            string summary = "Xác nhận trả xe với thông tin sau?\n\n";
            summary += $" Ngày trả: {dtpNgayTra.Value:dd/MM/yyyy HH:mm}\n";
            summary += $" Km kết thúc: {nudKmKetThuc.Value:N0} km (chạy {(int)nudKmKetThuc.Value - kmBatDau:N0} km)\n";
            summary += $" Tình trạng: {cboTinhTrang.SelectedItem}\n";
            summary += $" Chi phí phát sinh: {nudChiPhiPhatSinh.Value:N0} VNĐ\n\n";

            summary += " TÍNH TOÁN:\n";
            summary += $"  Tiền cọc ban đầu: {soTienCoc:N0} VNĐ\n";
            summary += $"  Chi phí phát sinh: -{nudChiPhiPhatSinh.Value:N0} VNĐ\n";

            if (TienPhat > 0)
            {
                int soNgayQuaHan = (DateTime.Now.Date - ngayKetThuc.Date).Days;
                summary += $" Tiền phạt trả muộn ({soNgayQuaHan} ngày): -{TienPhat:N0} VNĐ\n";
            }

            if (TienHoanTraSom > 0)
            {
                summary += $" Tiền hoàn trả sớm ({SoNgayTraSom} ngày): +{TienHoanTraSom:N0} VNĐ\n";
            }

            summary += $"\n TỔNG TIỀN HOÀN CỌC: {TienHoanCoc:N0} VNĐ\n";

            if (TienHoanCoc < 0)
            {
                summary += $"\n Khách hàng cần trả thêm: {Math.Abs(TienHoanCoc):N0} VNĐ";
            }

            return summary;
        }

        private bool ValidateInput()
        {
            // 1. Kiểm tra km kết thúc
            if (nudKmKetThuc.Value < kmBatDau)
            {
                MessageBox.Show(
                    $" Km kết thúc không hợp lệ!\n\n" +
                    $"Km kết thúc ({nudKmKetThuc.Value:N0}) không thể nhỏ hơn km bắt đầu ({kmBatDau:N0})!",
                    "Lỗi nhập liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                nudKmKetThuc.Focus();
                return false;
            }

            // 2. Cảnh báo km chạy quá nhiều
            int kmChay = (int)nudKmKetThuc.Value - kmBatDau;
            if (kmChay > 10000)
            {
                DialogResult result = MessageBox.Show(
                    $" CẢNH BÁO: Xe đã chạy {kmChay:N0} km!\n\n" +
                    $"Đây là một số km bất thường lớn. Bạn có chắc chắn muốn tiếp tục?",
                    "Cảnh báo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.No)
                {
                    nudKmKetThuc.Focus();
                    return false;
                }
            }

            // 3. Kiểm tra tình trạng xe
            if (cboTinhTrang.SelectedIndex == -1)
            {
                MessageBox.Show(
                    " Vui lòng chọn tình trạng xe!",
                    "Lỗi nhập liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                cboTinhTrang.Focus();
                return false;
            }

            // 4. Cảnh báo chi phí phát sinh lớn
            if (nudChiPhiPhatSinh.Value > 10000000)
            {
                DialogResult result = MessageBox.Show(
                    $" CẢNH BÁO: Chi phí phát sinh rất lớn!\n\n" +
                    $"Chi phí: {nudChiPhiPhatSinh.Value:N0} VNĐ\n\n" +
                    $"Bạn có chắc chắn muốn tiếp tục?",
                    "Cảnh báo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.No)
                {
                    nudChiPhiPhatSinh.Focus();
                    return false;
                }
            }

            // 5. Kiểm tra trả sớm
            if (chkTraSom.Checked && nudSoNgayTraSom.Value <= 0)
            {
                MessageBox.Show(
                    " Vui lòng nhập số ngày trả sớm!",
                    "Lỗi nhập liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                nudSoNgayTraSom.Focus();
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