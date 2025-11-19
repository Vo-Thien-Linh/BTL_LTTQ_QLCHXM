using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;
using UI.FormHandleUI; 

namespace UI.FormUI
{
    public partial class FormXemHopDongThue : Form
    {
        private int maGDThue;
        private string maNV;
        private GiaoDichThueBLL giaoDichThueBLL;
        private DataRow dataGiaoDich;

        public FormXemHopDongThue(int maGD, string maNhanVien)
        {
            InitializeComponent();
            this.maGDThue = maGD;
            this.maNV = maNhanVien;
            giaoDichThueBLL = new GiaoDichThueBLL();

            LoadData();
            ConfigureButtons();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = giaoDichThueBLL.GetDonChoThue();
                DataView dv = dt.DefaultView;
                dv.RowFilter = $"MaGDThue = {maGDThue}";

                if (dv.Count == 0)
                {
                    MessageBox.Show("Khong tim thay thong tin hop dong!", "Loi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                dataGiaoDich = dv[0].Row;
                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi tai du lieu: " + ex.Message, "Loi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void DisplayData()
        {
            // Thông tin giao dịch
            lblMaGD.Text = $"Ma GD: #{dataGiaoDich["MaGDThue"]}";

            // Thông tin xe
            txtTenXe.Text = dataGiaoDich["TenXe"].ToString();
            txtBienSo.Text = dataGiaoDich["BienSo"].ToString();

            // Thông tin khách hàng
            txtKhachHang.Text = dataGiaoDich["HoTenKH"].ToString();
            txtSDT.Text = dataGiaoDich["SdtKhachHang"].ToString();
            txtEmail.Text = dataGiaoDich["EmailKhachHang"]?.ToString();

            // Thông tin thuê
            dtpNgayBatDau.Value = Convert.ToDateTime(dataGiaoDich["NgayBatDau"]);
            dtpNgayKetThuc.Value = Convert.ToDateTime(dataGiaoDich["NgayKetThuc"]);
            txtSoNgayThue.Text = dataGiaoDich["SoNgayThue"].ToString();
            txtGiaThueNgay.Text = $"{Convert.ToDecimal(dataGiaoDich["GiaThueNgay"]):N0} VND";
            txtTongGia.Text = $"{Convert.ToDecimal(dataGiaoDich["TongGia"]):N0} VND";

            // Tiền cọc
            decimal soTienCoc = dataGiaoDich["SoTienCoc"] != DBNull.Value
                ? Convert.ToDecimal(dataGiaoDich["SoTienCoc"]) : 0;
            txtSoTienCoc.Text = $"{soTienCoc:N0} VND";

            // Giấy tờ giữ lại
            txtGiayToGiuLai.Text = dataGiaoDich["GiayToGiuLai"]?.ToString();

            // Trạng thái
            string trangThai = dataGiaoDich["TrangThai"].ToString();
            txtTrangThai.Text = trangThai;
            HighlightTrangThai(trangThai);

            string ttThanhToan = dataGiaoDich["TrangThaiThanhToan"].ToString();
            txtTrangThaiThanhToan.Text = ttThanhToan;
            HighlightThanhToan(ttThanhToan);

            txtHinhThucThanhToan.Text = dataGiaoDich["HinhThucThanhToan"]?.ToString();

            // Tính toán quá hạn
            int soNgayQuaHan = dataGiaoDich["SoNgayQuaHan"] != DBNull.Value
                ? Convert.ToInt32(dataGiaoDich["SoNgayQuaHan"]) : 0;

            if (soNgayQuaHan > 0)
            {
                decimal giaThueNgay = Convert.ToDecimal(dataGiaoDich["GiaThueNgay"]);
                decimal tienPhat = giaoDichThueBLL.TinhPhiPhat(
                    Convert.ToDateTime(dataGiaoDich["NgayKetThuc"]), giaThueNgay);

                txtSoNgayQuaHan.Text = soNgayQuaHan.ToString();
                txtTienPhat.Text = $"{tienPhat:N0} VND";

                // Highlight cảnh báo
                txtSoNgayQuaHan.BackColor = Color.FromArgb(255, 205, 210);
                txtTienPhat.BackColor = Color.FromArgb(255, 205, 210);

                lblWarning.Visible = true;
                lblWarning.Text = $"CANH BAO: Don thue qua han {soNgayQuaHan} ngay!";
            }
            else
            {
                txtSoNgayQuaHan.Text = "0";
                txtTienPhat.Text = "0 VND";
                lblWarning.Visible = false;
            }
        }

        private void HighlightTrangThai(string trangThai)
        {
            switch (trangThai)
            {
                case "Đang thuê":
                    txtTrangThai.BackColor = Color.FromArgb(200, 230, 201);
                    txtTrangThai.ForeColor = Color.FromArgb(56, 142, 60);
                    break;
                case "Chờ xác nhận":
                    txtTrangThai.BackColor = Color.FromArgb(255, 243, 205);
                    txtTrangThai.ForeColor = Color.FromArgb(255, 152, 0);
                    break;
                case "Đã thuê":
                    txtTrangThai.BackColor = Color.FromArgb(224, 224, 224);
                    txtTrangThai.ForeColor = Color.FromArgb(97, 97, 97);
                    break;
            }
        }

        private void HighlightThanhToan(string ttThanhToan)
        {
            if (ttThanhToan == "Đã thanh toán")
            {
                txtTrangThaiThanhToan.BackColor = Color.FromArgb(200, 230, 201);
                txtTrangThaiThanhToan.ForeColor = Color.FromArgb(56, 142, 60);
            }
            else
            {
                txtTrangThaiThanhToan.BackColor = Color.FromArgb(255, 243, 205);
                txtTrangThaiThanhToan.ForeColor = Color.FromArgb(255, 152, 0);
            }
        }

        private void ConfigureButtons()
        {
            string trangThai = dataGiaoDich["TrangThai"].ToString();
            string ttThanhToan = dataGiaoDich["TrangThaiThanhToan"].ToString();

            // Logic enable/disable buttons

            // Button Xác nhận Thanh toán
            btnXacNhanThanhToan.Enabled = (ttThanhToan == "Chưa thanh toán");

            // Button Giao xe
            btnGiaoXe.Enabled = (ttThanhToan == "Đã thanh toán" && trangThai != "Đang thuê");

            // Button Trả xe
            btnTraXe.Enabled = (trangThai == "Đang thuê");

            // Màu sắc buttons
            SetButtonStyle(btnXacNhanThanhToan, btnXacNhanThanhToan.Enabled);
            SetButtonStyle(btnGiaoXe, btnGiaoXe.Enabled);
            SetButtonStyle(btnTraXe, btnTraXe.Enabled);
        }

        private void SetButtonStyle(Button btn, bool enabled)
        {
        if (enabled)
        {
            btn.Cursor = Cursors.Hand;
            btn.ForeColor = Color.White;
        }
        else
        {
            btn.BackColor = Color.FromArgb(189, 189, 189);
            btn.Cursor = Cursors.No;
            btn.ForeColor = Color.FromArgb(117, 117, 117);
        }
    }

        private void btnXacNhanThanhToan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboHinhThucThanhToan.Text))
            {
                MessageBox.Show("Vui long chon hinh thuc thanh toan!", "Thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboHinhThucThanhToan.Focus();
                return;
            }

            var result = MessageBox.Show(
                $"Xac nhan khach hang da thanh toan?\nHinh thuc: {cboHinhThucThanhToan.Text}",
                "Xac nhan thanh toan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string errorMessage;
                    bool success = giaoDichThueBLL.XacNhanThanhToan(
                        maGDThue,
                        maNV,
                        cboHinhThucThanhToan.Text,
                        out errorMessage);

                    if (success)
                    {
                        MessageBox.Show("Xac nhan thanh toan thanh cong!", "Thanh cong",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadData();
                        ConfigureButtons();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Loi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Loi: " + ex.Message, "Loi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGiaoXe_Click(object sender, EventArgs e)
        {
            // ✅ Ensure FormGiaoXe is defined in UI.FormHandleUI or the correct namespace
            using (FormGiaoXe formGiaoXe = new FormGiaoXe())
            {
                if (formGiaoXe.ShowDialog() != DialogResult.OK)
                    return;

                var result = MessageBox.Show(
                    $"Xac nhan giao xe cho khach hang?\n\nKm bat dau: {formGiaoXe.KmBatDau}\n" +
                    "Sau khi giao, trang thai xe se chuyen thanh 'Dang thue'.",
                    "Xac nhan giao xe",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string errorMessage;
                        bool success = giaoDichThueBLL.XacNhanGiaoXe(
                            maGDThue,
                            maNV,
                            formGiaoXe.KmBatDau,
                            formGiaoXe.GhiChu,
                            out errorMessage);

                        if (success)
                        {
                            MessageBox.Show("Giao xe thanh cong!", "Thanh cong",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadData();
                            ConfigureButtons();
                        }
                        else
                        {
                            MessageBox.Show(errorMessage, "Loi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Loi: " + ex.Message, "Loi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnTraXe_Click(object sender, EventArgs e)
{
    using (FormTraXe formTraXe = new FormTraXe(dataGiaoDich))
    {
        if (formTraXe.ShowDialog() == DialogResult.OK)
        {
            try
            {
                string errorMessage;
                // You need to provide values for kmKetThuc, isTraSom, soNgayTraSom
                // Example placeholders:
                int kmKetThuc = 0; // TODO: Get actual value from formTraXe or user input
                bool isTraSom = false; // TODO: Determine if early return
                int soNgayTraSom = 0; // TODO: Calculate if early return

                bool success = giaoDichThueBLL.XacNhanTraXe(
                    maGDThue,
                    maNV,
                    formTraXe.TinhTrangXe,
                    formTraXe.ChiPhiPhatSinh,
                    kmKetThuc,
                    isTraSom,
                    soNgayTraSom,
                    formTraXe.GhiChu,
                    out errorMessage);

                if (success)
                {
                    MessageBox.Show(
                        $"Tra xe thanh cong!\n\n" +
                        $"Tien hoan coc: {formTraXe.TienHoanCoc:N0} VND",
                        "Thanh cong",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(errorMessage, "Loi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message, "Loi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}