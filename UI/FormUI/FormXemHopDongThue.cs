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
        private string maTaiKhoan;
        private GiaoDichThueBLL giaoDichThueBLL;
        private HopDongThueBLL hopDongThueBLL;
        private DataRow dataGiaoDich;

        public FormXemHopDongThue(int maGD, string maNhanVien)
        {
            InitializeComponent();
            this.maGDThue = maGD;
            this.maNV = maNhanVien;

            //  LẤY MÃ TÀI KHOẢN TỪ CurrentUser
            this.maTaiKhoan = CurrentUser.MaTaiKhoan;

            //  KIỂM TRA
            if (string.IsNullOrWhiteSpace(this.maTaiKhoan))
            {
                MessageBox.Show(
                    "❌ Lỗi: Không xác định được tài khoản đang đăng nhập!\n" +
                    "Vui lòng đăng nhập lại.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                this.Close();
                return;
            }

            giaoDichThueBLL = new GiaoDichThueBLL();
            hopDongThueBLL = new HopDongThueBLL();

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
                case "Chờ giao xe":
                    txtTrangThai.BackColor = Color.FromArgb(179, 229, 252);
                    txtTrangThai.ForeColor = Color.FromArgb(1, 87, 155);
                    break;
                case "Đã thuê":
                    txtTrangThai.BackColor = Color.FromArgb(224, 224, 224);
                    txtTrangThai.ForeColor = Color.FromArgb(97, 97, 97);
                    break;
                default:
                    txtTrangThai.BackColor = Color.White;
                    txtTrangThai.ForeColor = Color.Black;
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
            string trangThaiDuyet = dataGiaoDich["TrangThaiDuyet"].ToString();

            // Button Xác nhận Thanh toán
            btnXacNhanThanhToan.Enabled = (
                trangThaiDuyet == "Đã duyệt" &&
                ttThanhToan == "Chưa thanh toán" &&
                trangThai != "Đang thuê" &&
                trangThai != "Đã thuê"
            );

            // Button Giao xe
            btnGiaoXe.Enabled = (
                ttThanhToan == "Đã thanh toán" &&
                trangThai == "Chờ giao xe"
            );

            // Button Trả xe
            btnTraXe.Enabled = (trangThai == "Đang thuê");

            // Button In hóa đơn - CHỈ KHI ĐÃ THANH TOÁN
            btnInHoaDon.Enabled = (ttThanhToan == "Đã thanh toán");

            // Màu sắc buttons
            SetButtonStyle(btnXacNhanThanhToan, btnXacNhanThanhToan.Enabled);
            SetButtonStyle(btnGiaoXe, btnGiaoXe.Enabled);
            SetButtonStyle(btnTraXe, btnTraXe.Enabled);
            SetButtonStyle(btnInHoaDon, btnInHoaDon.Enabled);
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

                        this.DialogResult = DialogResult.OK;
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

                            this.DialogResult = DialogResult.OK;
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
            DataTable dtFull = giaoDichThueBLL.GetGiaoDichThueById(maGDThue);

            if (dtFull.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu giao dịch!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (FormTraXe formTraXe = new FormTraXe(dtFull.Rows[0]))
            {
                if (formTraXe.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string errorMessage;

                        bool success = giaoDichThueBLL.XacNhanTraXe(
                            maGDThue,
                            maTaiKhoan,
                            formTraXe.TinhTrangXe,
                            formTraXe.ChiPhiPhatSinh,
                            formTraXe.KmKetThuc,
                            formTraXe.IsTraSom,
                            formTraXe.SoNgayTraSom,
                            formTraXe.GhiChu,
                            out errorMessage);

                        if (success)
                        {
                            MessageBox.Show(
                                $"Trả xe thành công!\n\n" +
                                $"Tiền hoàn cọc: {formTraXe.TienHoanCoc:N0} VNĐ",
                                "Thành công",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(errorMessage, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXuatPDF_Click(object sender, EventArgs e)
        {
            try
            {
                //Kiểm tra trạng thái duyệt
                string trangThaiDuyet = dataGiaoDich["TrangThaiDuyet"].ToString();
                
                if (trangThaiDuyet != "Đã duyệt")
                {
                    MessageBox.Show(
                        "❌ Không thể xuất hợp đồng!\n\n" +
                        $"Đơn thuê chưa được duyệt (Trạng thái: {trangThaiDuyet})\n" +
                        "Vui lòng chờ quản lý duyệt đơn trước.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Lấy dữ liệu hợp đồng từ database
                DataTable dtHopDong = hopDongThueBLL.GetHopDongByMaGDThue(maGDThue);

                // Nếu chưa có hợp đồng, tạo tự động
                if (dtHopDong.Rows.Count == 0)
                {
                    var confirmCreate = MessageBox.Show(
                        "Hợp đồng chưa được tạo!\n\n" +
                        "Bạn có muốn tạo hợp đồng ngay bây giờ không?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirmCreate != DialogResult.Yes)
                    {
                        return;
                    }

                    // Tạo hợp đồng
                    string errorMessage;
                    bool createSuccess = hopDongThueBLL.TaoHopDongThue(
                        maGDThue,
                        maTaiKhoan,
                        out errorMessage);

                    if (!createSuccess)
                    {
                        MessageBox.Show(
                            $"Không thể tạo hợp đồng!\n\n{errorMessage}",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    // Tải lại dữ liệu hợp đồng sau khi tạo
                    dtHopDong = hopDongThueBLL.GetHopDongByMaGDThue(maGDThue);

                    if (dtHopDong.Rows.Count == 0)
                    {
                        MessageBox.Show(
                            "Lỗi: Đã tạo hợp đồng nhưng không thể tải lại dữ liệu!",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show(
                        "Tạo hợp đồng thành công!",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                // Chọn nơi lưu file PDF
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    Title = "Xuất hợp đồng thuê xe",
                    FileName = $"HopDong_ThueXe_{maGDThue}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Xuất PDF
                    PDFHelper.ExportChiTietHopDongThue(dtHopDong.Rows[0], saveDialog.FileName);

                    // Thông báo thành công
                    var result = MessageBox.Show(
                        "✓ Xuất hợp đồng thành công!\n\n" +
                        $"File đã được lưu tại:\n{saveDialog.FileName}\n\n" +
                        "Bạn có muốn mở file vừa xuất không?",
                        "Thành công",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi xuất hợp đồng:\n\n{ex.Message}\n\n" +
                    $"Chi tiết:\n{ex.StackTrace}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                System.Diagnostics.Debug.WriteLine($"[LỖI] btnXuatPDF_Click: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra trạng thái thanh toán
                string trangThaiThanhToan = dataGiaoDich["TrangThaiThanhToan"].ToString();
                
                if (trangThaiThanhToan != "Đã thanh toán")
                {
                    MessageBox.Show(
                        "❌ Không thể in hóa đơn!\n\n" +
                        "Giao dịch chưa được thanh toán.\n" +
                        "Vui lòng xác nhận thanh toán trước khi in hóa đơn.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Chọn nơi lưu file PDF
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    Title = "In hóa đơn thanh toán thuê xe",
                    FileName = $"HoaDon_ThueXe_{maGDThue}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy dữ liệu đầy đủ của giao dịch
                    DataTable dtGiaoDich = giaoDichThueBLL.GetGiaoDichThueById(maGDThue);

                    if (dtGiaoDich.Rows.Count == 0)
                    {
                        MessageBox.Show(
                            "Không tìm thấy dữ liệu giao dịch!",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    // Xuất PDF hóa đơn
                    PDFHelper.ExportHoaDonThueXe(dtGiaoDich.Rows[0], saveDialog.FileName);

                    // Thông báo thành công
                    var result = MessageBox.Show(
                        "✓ In hóa đơn thành công!\n\n" +
                        $"File đã được lưu tại:\n{saveDialog.FileName}\n\n" +
                        "Bạn có muốn mở file vừa xuất không?",
                        "Thành công",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi in hóa đơn:\n\n{ex.Message}\n\n" +
                    $"Chi tiết:\n{ex.StackTrace}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                System.Diagnostics.Debug.WriteLine($"[LỖI] btnInHoaDon_Click: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}