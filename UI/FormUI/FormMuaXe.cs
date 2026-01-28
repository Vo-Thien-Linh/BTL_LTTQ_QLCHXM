using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace UI.FormUI
{
    public partial class FormMuaXe : Form
    {
        private KhachHangBLL khachHangBLL;
        private XeMayBLL xeMayBLL;
        private GiaoDichBanBLL giaoDichBanBLL;
        private HopDongMuaBLL hopDongMuaBLL;
        private PhuTungBLL phuTungBLL;
        private KhuyenMaiBLL khuyenMaiBLL;
        private string maTaiKhoan;
        private bool isLoadingData = false;
        private KhachHangDTO khachHangHienTai = null;
        private string idXeDaChon = null;
        private DataRowView xeHienTai = null; // Lưu thông tin xe hiện tại
        private DataTable dtPhuTungDaChon; // DataTable lưu phụ tùng đã chọn
        private KhuyenMaiDTO khuyenMaiHienTai = null; // Lưu khuyến mãi đang chọn
        
        public int MaGDBanVuaTao { get; private set; } = 0;

        public FormMuaXe(string maTK)
        {
            InitializeComponent();
            this.maTaiKhoan = maTK;
            this.idXeDaChon = null;

            // Kiểm tra đăng nhập
            if (string.IsNullOrWhiteSpace(maTaiKhoan))
            {
                MessageBox.Show(
                    "Vui lòng đăng nhập để thực hiện chức năng bán xe!",
                    "Yêu cầu đăng nhập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            khachHangBLL = new KhachHangBLL();
            xeMayBLL = new XeMayBLL();
            giaoDichBanBLL = new GiaoDichBanBLL();
            hopDongMuaBLL = new HopDongMuaBLL();
            phuTungBLL = new PhuTungBLL();
            khuyenMaiBLL = new KhuyenMaiBLL();

            // Khởi tạo DataTable cho phụ tùng
            InitializePhuTungDataTable();

            // Ẩn các control liên quan đến giấy tờ
            HideGiayToControls();

            SetDefaultValues();
            SetupEvents();
            LoadXeBan();
        }

        // Constructor mới: nhận mã tài khoản và ID xe cụ thể
        public FormMuaXe(string maTK, string idXe) : this(maTK)
        {
            // Kiểm tra nếu form đã bị dispose từ constructor base
            if (this.IsDisposed || this.DialogResult == DialogResult.Cancel)
                return;
            
            this.idXeDaChon = idXe;
            
            // Load thông tin xe cụ thể
            if (!string.IsNullOrEmpty(idXe))
            {
                LoadThongTinXeChiDinh(idXe);
            }
        }

        private void HideGiayToControls()
        {
            // Ẩn các control liên quan đến loại giấy tờ và ảnh giấy tờ
            if (lblLoaiGiayTo != null) lblLoaiGiayTo.Visible = false;
            if (cboLoaiGiayTo != null) cboLoaiGiayTo.Visible = false;
            if (lblAnhGiayTo != null) lblAnhGiayTo.Visible = false;
            if (picAnhGiayTo != null) picAnhGiayTo.Visible = false;
            if (btnXemAnhGiayTo != null) btnXemAnhGiayTo.Visible = false;
        }

        private void SetDefaultValues()
        {
            dtpNgayBan.Value = DateTime.Now.Date;
            cboHinhThucThanhToan.SelectedIndex = 0;
            cboTrangThaiThanhToan.SelectedIndex = 0;
            
            // Khởi tạo ComboBox loại giấy tờ
            cboLoaiGiayTo.Items.Clear();
            cboLoaiGiayTo.Items.Add("CCCD");
            cboLoaiGiayTo.Items.Add("CMND");
            cboLoaiGiayTo.Items.Add("Hộ chiếu");
            cboLoaiGiayTo.Items.Add("Giấy phép lái xe");
            cboLoaiGiayTo.SelectedIndex = 0;
        }

        private void SetupEvents()
        {
            txtSdtKH.KeyDown += TxtSdtKH_KeyDown;
            btnTimKH.Click += BtnTimKH_Click;
            btnLuu.Click += BtnLuu_Click;
            btnHuy.Click += BtnHuy_Click;
            btnThemKH.Click += BtnThemKH_Click;
            btnXemAnhGiayTo.Click += BtnXemAnhGiayTo_Click;
            btnThemPhuTung.Click += BtnThemPhuTung_Click;
            btnXoaPhuTung.Click += BtnXoaPhuTung_Click;
            cboKhuyenMai.SelectedIndexChanged += CboKhuyenMai_SelectedIndexChanged;
            
            LoadKhuyenMai();
        }

        private void TxtSdtKH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnTimKH_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void BtnTimKH_Click(object sender, EventArgs e)
        {
            string sdt = txtSdtKH.Text.Trim();

            if (string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSdtKH.Focus();
                return;
            }

            try
            {
                // Tìm khách hàng theo SĐT
                khachHangHienTai = khachHangBLL.GetKhachHangBySdt(sdt);

                if (khachHangHienTai != null)
                {
                    // Hiển thị thông tin khách hàng
                    txtMaKH.Text = khachHangHienTai.MaKH;
                    txtHoTenKH.Text = khachHangHienTai.HoTenKH;
                    txtEmailKH.Text = khachHangHienTai.Email ?? "";
                    txtDiaChiKH.Text = khachHangHienTai.DiaChi ?? "";
                    txtCCCD.Text = khachHangHienTai.SoCCCD ?? "";
                    
                    // Hiển thị loại giấy tờ
                    if (!string.IsNullOrEmpty(khachHangHienTai.LoaiGiayTo))
                    {
                        int index = cboLoaiGiayTo.Items.IndexOf(khachHangHienTai.LoaiGiayTo);
                        if (index >= 0)
                            cboLoaiGiayTo.SelectedIndex = index;
                    }
                    cboLoaiGiayTo.Enabled = true;
                    
                    // Hiển thị ảnh giấy tờ
                    HienThiAnhGiayTo(khachHangHienTai.AnhGiayTo);

                    MessageBox.Show($"Đã tìm thấy khách hàng: {khachHangHienTai.HoTenKH}", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Không tìm thấy - hỏi có muốn thêm mới không
                    DialogResult result = MessageBox.Show(
                        "Không tìm thấy khách hàng với số điện thoại này.\nBạn có muốn thêm khách hàng mới?",
                        "Không tìm thấy",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        BtnThemKH_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm khách hàng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadXeBan()
        {
            // Không cần load danh sách xe nữa vì dùng TextBox
            // Form này chỉ được mở khi đã chọn xe cụ thể
        }

        private void LoadThongTinXeChiDinh(string idXe)
        {
            try
            {
                // Kiểm tra form đã bị dispose chưa
                if (this.IsDisposed)
                    return;
                
                // Lấy tất cả xe có thể bán
                DataTable dt = xeMayBLL.GetXeCoTheBan();
                
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Không có xe nào sẵn sàng để bán!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
                
                // Tìm xe theo ID
                bool found = false;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["ID_Xe"].ToString() == idXe)
                    {
                        // Kiểm tra số lượng tồn kho
                        int soLuong = row["SoLuong"] != DBNull.Value ? Convert.ToInt32(row["SoLuong"]) : 0;
                        if (soLuong <= 0)
                        {
                            MessageBox.Show(
                                "Xe này đã hết hàng!\n\nVui lòng chọn xe khác hoặc nhập thêm hàng.",
                                "Xe hết hàng",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            this.DialogResult = DialogResult.Cancel;
                            this.Close();
                            return;
                        }
                        
                        // Tạo DataRowView để lưu thông tin xe
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = $"ID_Xe = '{idXe}'";
                        if (dv.Count > 0)
                        {
                            xeHienTai = dv[0];
                            
                            // Hiển thị thông tin xe - kiểm tra controls tồn tại
                            if (txtTenXe != null)
                                txtTenXe.Text = row["TenXe"]?.ToString() ?? "";
                            
                            // Hiển thị thông tin bổ sung
                            if (txtMauXe != null)
                            {
                                txtMauXe.Text = row["TenMau"]?.ToString() ?? "Không xác định";
                                txtMauXe.Visible = true;
                                if (lblMauXe != null) lblMauXe.Visible = true;
                            }
                            
                            if (txtLoaiXe != null)
                            {
                                txtLoaiXe.Text = string.Empty;
                                txtLoaiXe.Visible = false;
                                if (lblLoaiXe != null) lblLoaiXe.Visible = false;
                            }
                            
                            if (txtPhanKhoi != null)
                            {
                                txtPhanKhoi.Text = row["PhanKhoi"]?.ToString() + " cc" ?? "Không xác định";
                                txtPhanKhoi.Visible = true;
                                if (lblPhanKhoi != null) lblPhanKhoi.Visible = true;
                            }
                            
                            if (txtThongTinXang != null)
                            {
                                txtThongTinXang.Text = row["ThongTinXang"]?.ToString() ?? "Xăng thường";
                                txtThongTinXang.Visible = true;
                                if (lblThongTinXang != null) lblThongTinXang.Visible = true;
                            }
                            
                            if (txtNamSX != null)
                            {
                                txtNamSX.Text = row["NamSX"]?.ToString() ?? "Không xác định";
                                txtNamSX.Visible = true;
                                if (lblNamSX != null) lblNamSX.Visible = true;
                            }
                            
                            if (txtSoLuongTon != null)
                                txtSoLuongTon.Text = row["SoLuong"]?.ToString() ?? "0";
                            
                            if (txtGiaBan != null)
                            {
                                decimal giaBan = row["GiaBan"] != DBNull.Value ? Convert.ToDecimal(row["GiaBan"]) : 0;
                                txtGiaBan.Text = giaBan.ToString("N0") + " VNĐ";
                            }
                            
                            found = true;
                            
                            // Tính tổng tiền ngay khi load xe
                            TinhTongTien();
                        }
                        break;
                    }
                }
                
                if (!found)
                {
                    MessageBox.Show(
                        "Không tìm thấy thông tin xe hoặc xe không còn sẵn sàng để bán!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Lỗi khi tải thông tin xe: {ex.Message}\n\nStack Trace: {ex.StackTrace}";
                if (ex.InnerException != null)
                {
                    errorMsg += $"\n\nInner Exception: {ex.InnerException.Message}";
                }
                
                MessageBox.Show(
                    errorMsg,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra đăng nhập
            if (string.IsNullOrWhiteSpace(maTaiKhoan))
            {
                MessageBox.Show(
                    "Vui lòng đăng nhập để thực hiện chức năng bán xe!",
                    "Yêu cầu đăng nhập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput())
            {
                return;
            }

            try
            {
                // Lấy thông tin từ form
                string maKH = khachHangHienTai.MaKH;
                string idXe = xeHienTai["ID_Xe"].ToString();
                DateTime ngayBan = dtpNgayBan.Value.Date;

                // Kiểm tra xe trước khi bán
                string errorMessage;
                if (!xeMayBLL.KiemTraXeTruocKhiBan(idXe, 1, out errorMessage))
                {
                    MessageBox.Show(
                        errorMessage,
                        "Không thể bán xe",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                decimal giaBan = xeHienTai["GiaBan"] != DBNull.Value ? Convert.ToDecimal(xeHienTai["GiaBan"]) : 0;

                // Chuẩn bị danh sách phụ tùng (nếu có)
                List<ChiTietPhuTungBanDTO> dsPhuTung = new List<ChiTietPhuTungBanDTO>();
                decimal tongTienPhuTung = 0;
                
                if (dtPhuTungDaChon != null && dtPhuTungDaChon.Rows.Count > 0)
                {
                    foreach (DataRow row in dtPhuTungDaChon.Rows)
                    {
                        var pt = new ChiTietPhuTungBanDTO
                        {
                            MaPhuTung = row["MaPhuTung"].ToString(),
                            SoLuong = Convert.ToInt32(row["SoLuong"]),
                            DonGia = Convert.ToDecimal(row["DonGia"]),
                            ThanhTien = Convert.ToDecimal(row["ThanhTien"]),
                            MaKM = row["MaKM"]?.ToString(),
                            SoTienGiam = row["SoTienGiam"] != DBNull.Value ? Convert.ToDecimal(row["SoTienGiam"]) : 0
                        };
                        dsPhuTung.Add(pt);
                        tongTienPhuTung += pt.ThanhTien;
                    }
                }

                // Tạo DTO
                GiaoDichBan gd = new GiaoDichBan
                {
                    MaKH = maKH,
                    ID_Xe = idXe,
                    NgayBan = ngayBan,
                    GiaBan = giaBan,
                    TrangThaiThanhToan = cboTrangThaiThanhToan.SelectedItem?.ToString() ?? "Đã thanh toán",
                    HinhThucThanhToan = cboHinhThucThanhToan.SelectedItem?.ToString() ?? "Tiền mặt",
                    MaTaiKhoan = maTaiKhoan
                };

                // Tính tổng tiền và khuyến mãi
                decimal tongTien = giaBan + tongTienPhuTung;
                string maKM = khuyenMaiHienTai != null ? khuyenMaiHienTai.MaKM : null;
                decimal soTienGiam = 0;
                if (khuyenMaiHienTai != null)
                {
                    soTienGiam = khuyenMaiBLL.TinhGiaTriGiam(khuyenMaiHienTai, tongTien);
                }

                // Lưu vào database (GỌI METHOD MỚI)
                errorMessage = ""; // Reset error message
                int maGDBan = giaoDichBanBLL.InsertGiaoDichBanKemPhuTung(
                    gd, maKM, soTienGiam, dsPhuTung, out errorMessage
                );

                if (maGDBan > 0)
                {
                    MaGDBanVuaTao = maGDBan;
                    
                    // Tạo ghi chú với thông tin khách hàng
                    string ghiChu = $"Khách hàng: {khachHangHienTai.HoTenKH}\n" +
                                   $" Số điện thoại: {khachHangHienTai.Sdt}";
                    
                    if (!string.IsNullOrWhiteSpace(khachHangHienTai.Email))
                        ghiChu += $"\n Email: {khachHangHienTai.Email}";
                    
                    if (dsPhuTung.Count > 0)
                        ghiChu += $"\n\n Giao dịch bao gồm {dsPhuTung.Count} phụ tùng đi kèm.";
                    
                    // Tạo hợp đồng mua 
                    HopDongMuaDTO hopDong = new HopDongMuaDTO
                    {
                        MaGDBan = maGDBan,
                        MaKH = maKH,
                        MaTaiKhoan = maTaiKhoan,
                        ID_Xe = idXe,
                        NgayLap = DateTime.Now,
                        GiaBan = giaBan + tongTienPhuTung,
                        DieuKhoan = "Hợp đồng mua bán xe máy theo quy định của pháp luật.",
                        GhiChu = ghiChu,
                        TrangThaiHopDong = "Đang hiệu lực"
                    };
                    
                    string hopDongError = "";
                    int maHDM = hopDongMuaBLL.InsertHopDongMua(hopDong, out hopDongError);
                    
                    if (maHDM > 0)
                    {
                        // Thông báo ngắn gọn và hỏi có muốn xuất hóa đơn không
                        var result = MessageBox.Show(
                            "Bán xe thành công!\n\nBạn có muốn xuất hóa đơn mua xe không?",
                            "Thành công",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);
                        
                        if (result == DialogResult.Yes)
                        {
                            XuatHoaDonMuaXe(maGDBan);
                        }
                    }
                    else
                    {
                        // Không tạo được hợp đồng
                        MessageBox.Show(
                            $"Bán xe thành công nhưng chưa tạo được hợp đồng!\n\n{hopDongError}",
                            "Cảnh báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "Không thể bán xe!\n\n" + errorMessage,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi lưu giao dịch bán: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private bool ValidateInput()
        {
            if (khachHangHienTai == null || string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng tìm hoặc thêm khách hàng!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSdtKH.Focus();
                return false;
            }

            if (xeHienTai == null || string.IsNullOrEmpty(txtTenXe.Text))
            {
                MessageBox.Show("Không có thông tin xe!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra số lượng tồn
            if (string.IsNullOrWhiteSpace(txtSoLuongTon.Text) || txtSoLuongTon.Text == "0")
            {
                MessageBox.Show("Xe đã hết hàng!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn hủy bán xe?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void BtnThemKH_Click(object sender, EventArgs e)
        {
            try
            {
                FormThemKhachHang formThem = new FormThemKhachHang();
                
                // Truyền SĐT đã nhập vào form thêm (nếu có)
                if (!string.IsNullOrEmpty(txtSdtKH.Text))
                {
                    // Có thể set giá trị SĐT cho form thêm nếu form hỗ trợ
                }

                if (formThem.ShowDialog() == DialogResult.OK)
                {
                    khachHangHienTai = formThem.KhachHangMoi;
                    
                    if (khachHangHienTai != null)
                    {
                        // Hiển thị thông tin khách hàng mới
                        txtSdtKH.Text = khachHangHienTai.Sdt;
                        txtMaKH.Text = khachHangHienTai.MaKH;
                        txtHoTenKH.Text = khachHangHienTai.HoTenKH;
                        txtEmailKH.Text = khachHangHienTai.Email ?? "";
                        txtDiaChiKH.Text = khachHangHienTai.DiaChi ?? "";
                        txtCCCD.Text = khachHangHienTai.SoCCCD ?? "";
                        
                        // Hiển thị loại giấy tờ
                        if (!string.IsNullOrEmpty(khachHangHienTai.LoaiGiayTo))
                        {
                            int index = cboLoaiGiayTo.Items.IndexOf(khachHangHienTai.LoaiGiayTo);
                            if (index >= 0)
                                cboLoaiGiayTo.SelectedIndex = index;
                        }
                        cboLoaiGiayTo.Enabled = true;
                        
                        // Hiển thị ảnh giấy tờ
                        HienThiAnhGiayTo(khachHangHienTai.AnhGiayTo);

                        MessageBox.Show("Đã thêm khách hàng mới thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiAnhGiayTo(byte[] anhGiayTo)
        {
            if (anhGiayTo != null && anhGiayTo.Length > 0)
            {
                try
                {
                    using (var ms = new System.IO.MemoryStream(anhGiayTo))
                    {
                        picAnhGiayTo.Image = System.Drawing.Image.FromStream(ms);
                        picAnhGiayTo.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    btnXemAnhGiayTo.Enabled = true;
                }
                catch
                {
                    picAnhGiayTo.Image = null;
                    btnXemAnhGiayTo.Enabled = false;
                }
            }
            else
            {
                picAnhGiayTo.Image = null;
                btnXemAnhGiayTo.Enabled = false;
            }
        }

        private void BtnXemAnhGiayTo_Click(object sender, EventArgs e)
        {
            if (khachHangHienTai?.AnhGiayTo != null && khachHangHienTai.AnhGiayTo.Length > 0)
            {
                try
                {
                    // Tạo form mới để hiển thị ảnh phóng to
                    Form formXemAnh = new Form
                    {
                        Text = $"Giấy tờ - {khachHangHienTai.HoTenKH}",
                        Size = new System.Drawing.Size(800, 600),
                        StartPosition = FormStartPosition.CenterParent,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        MaximizeBox = true,
                        MinimizeBox = false
                    };

                    PictureBox picBox = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.Zoom
                    };

                    using (var ms = new System.IO.MemoryStream(khachHangHienTai.AnhGiayTo))
                    {
                        picBox.Image = System.Drawing.Image.FromStream(ms);
                    }

                    formXemAnh.Controls.Add(picBox);
                    formXemAnh.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể hiển thị ảnh: " + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Khách hàng chưa có ảnh giấy tờ!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void InitializePhuTungDataTable()
        {
            if (dgvPhuTung == null)
                return;

            dtPhuTungDaChon = new DataTable();
            dtPhuTungDaChon.Columns.Add("MaPhuTung", typeof(string));
            dtPhuTungDaChon.Columns.Add("TenPhuTung", typeof(string));
            dtPhuTungDaChon.Columns.Add("SoLuong", typeof(int));
            dtPhuTungDaChon.Columns.Add("DonGia", typeof(decimal));
            dtPhuTungDaChon.Columns.Add("ThanhTien", typeof(decimal));
            dtPhuTungDaChon.Columns.Add("DonViTinh", typeof(string));
            dtPhuTungDaChon.Columns.Add("MaKM", typeof(string));
            dtPhuTungDaChon.Columns.Add("TenKM", typeof(string));
            dtPhuTungDaChon.Columns.Add("SoTienGiam", typeof(decimal));
            dtPhuTungDaChon.Columns.Add("ThanhTienSauGiam", typeof(decimal));

            dgvPhuTung.DataSource = dtPhuTungDaChon;

            // Chỉ ẩn cột, không set width/properties để tránh lỗi
            if (dgvPhuTung.Columns.Count > 0)
            {
                if (dgvPhuTung.Columns.Contains("MaPhuTung"))
                    dgvPhuTung.Columns["MaPhuTung"].Visible = false;
                    
                if (dgvPhuTung.Columns.Contains("DonViTinh"))
                    dgvPhuTung.Columns["DonViTinh"].Visible = false;

                if (dgvPhuTung.Columns.Contains("MaKM"))
                    dgvPhuTung.Columns["MaKM"].Visible = false;
            }

            TinhTongTien();
        }

        private void BtnThemPhuTung_Click(object sender, EventArgs e)
        {
            try
            {
                FormChonPhuTung formChonPT = new FormChonPhuTung();
                
                if (formChonPT.ShowDialog() == DialogResult.OK)
                {
                    var phuTungChon = formChonPT.PhuTungDaChon;
                    int soLuong = formChonPT.SoLuong;
                    var kmPt = formChonPT.KhuyenMaiDaChon;
                    decimal soTienGiamPt = formChonPT.SoTienGiamKhuyenMai;

                    if (phuTungChon != null && soLuong > 0)
                    {
                        decimal thanhTien = soLuong * phuTungChon.GiaBan;
                        decimal thanhTienSauGiam = Math.Max(thanhTien - soTienGiamPt, 0);
                        string maKM = kmPt?.MaKM ?? "";
                        string tenKM = kmPt?.TenKM ?? "";

                        DataRow[] existingRows = dtPhuTungDaChon.Select($"MaPhuTung = '{phuTungChon.MaPhuTung}'");
                        
                        if (existingRows.Length > 0)
                        {
                            int soLuongCu = Convert.ToInt32(existingRows[0]["SoLuong"]);
                            int soLuongMoi = soLuongCu + soLuong;
                            existingRows[0]["SoLuong"] = soLuongMoi;

                            decimal thanhTienMoi = soLuongMoi * phuTungChon.GiaBan;

                            // Nếu dòng đã có KM thì giữ nguyên mã và tính lại giảm; nếu chưa có, lấy từ lựa chọn mới
                            string maKmHienTai = existingRows[0]["MaKM"]?.ToString() ?? "";
                            string tenKmHienTai = existingRows[0]["TenKM"]?.ToString() ?? "";
                            if (string.IsNullOrEmpty(maKmHienTai) && !string.IsNullOrEmpty(maKM))
                            {
                                maKmHienTai = maKM;
                                tenKmHienTai = tenKM;
                            }

                            decimal giamPt = 0;
                            if (!string.IsNullOrEmpty(maKmHienTai))
                            {
                                string errKm;
                                giamPt = khuyenMaiBLL.TinhGiaTriGiam(maKmHienTai, thanhTienMoi, out errKm);
                                if (!string.IsNullOrEmpty(errKm))
                                    giamPt = 0;
                            }

                            existingRows[0]["MaKM"] = maKmHienTai;
                            existingRows[0]["TenKM"] = tenKmHienTai;
                            existingRows[0]["ThanhTien"] = thanhTienMoi;
                            existingRows[0]["SoTienGiam"] = giamPt;
                            existingRows[0]["ThanhTienSauGiam"] = Math.Max(thanhTienMoi - giamPt, 0);
                        }
                        else
                        {
                            DataRow newRow = dtPhuTungDaChon.NewRow();
                            newRow["MaPhuTung"] = phuTungChon.MaPhuTung;
                            newRow["TenPhuTung"] = phuTungChon.TenPhuTung;
                            newRow["SoLuong"] = soLuong;
                            newRow["DonGia"] = phuTungChon.GiaBan;
                            newRow["ThanhTien"] = thanhTien;
                            newRow["DonViTinh"] = phuTungChon.DonViTinh ?? "";
                            newRow["MaKM"] = maKM;
                            newRow["TenKM"] = tenKM;
                            newRow["SoTienGiam"] = soTienGiamPt;
                            newRow["ThanhTienSauGiam"] = thanhTienSauGiam;

                            dtPhuTungDaChon.Rows.Add(newRow);
                        }

                        TinhTongTien();

                        MessageBox.Show(
                            $"Đã thêm {soLuong} {phuTungChon.DonViTinh} {phuTungChon.TenPhuTung}",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi thêm phụ tùng: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnXoaPhuTung_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPhuTung.CurrentRow != null && dgvPhuTung.CurrentRow.Index >= 0)
                {
                    string tenPhuTung = dgvPhuTung.CurrentRow.Cells["TenPhuTung"].Value?.ToString() ?? "";
                    
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa phụ tùng '{tenPhuTung}' khỏi danh sách?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        dtPhuTungDaChon.Rows.RemoveAt(dgvPhuTung.CurrentRow.Index);
                        TinhTongTien();

                        MessageBox.Show(
                            "Đã xóa phụ tùng khỏi danh sách",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Vui lòng chọn phụ tùng cần xóa!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi xóa phụ tùng: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void TinhTongTien()
        {
            try
            {
                if (dtPhuTungDaChon == null)
                    return;

                decimal tongTienXe = 0;
                if (xeHienTai != null && xeHienTai.Row != null)
                {
                    if (xeHienTai["GiaBan"] != DBNull.Value)
                    {
                        tongTienXe = Convert.ToDecimal(xeHienTai["GiaBan"]);
                    }
                    else if (xeHienTai.Row.Table.Columns.Contains("GiaBanGanNhat") && xeHienTai["GiaBanGanNhat"] != DBNull.Value)
                    {
                        tongTienXe = Convert.ToDecimal(xeHienTai["GiaBanGanNhat"]);
                    }
                }
                
                if (txtTongTienXe != null)
                    txtTongTienXe.Text = tongTienXe.ToString("N0") + " VNĐ";

                decimal tongTienPhuTung = 0;
                decimal tongGiamPhuTung = 0;
                if (dtPhuTungDaChon != null)
                {
                    foreach (DataRow row in dtPhuTungDaChon.Rows)
                    {
                        if (row.RowState != DataRowState.Deleted)
                        {
                            tongTienPhuTung += Convert.ToDecimal(row["ThanhTien"]);
                            tongGiamPhuTung += row["SoTienGiam"] != DBNull.Value ? Convert.ToDecimal(row["SoTienGiam"]) : 0;
                        }
                    }
                }
                
                if (txtTongTienPhuTung != null)
                    txtTongTienPhuTung.Text = tongTienPhuTung.ToString("N0") + " VNĐ";

                // Tính tổng trước khi giảm
                decimal tongTruocGiam = tongTienXe + tongTienPhuTung;
                
                // Tính số tiền giảm từ khuyến mãi
                decimal soTienGiamXe = 0;
                if (khuyenMaiHienTai != null)
                {
                    soTienGiamXe = khuyenMaiBLL.TinhGiaTriGiam(khuyenMaiHienTai, tongTruocGiam);
                }

                decimal tongGiam = soTienGiamXe + tongGiamPhuTung;
                
                if (txtSoTienGiam != null)
                    txtSoTienGiam.Text = tongGiam.ToString("N0") + " VNĐ";

                // Tổng thanh toán sau khi giảm
                decimal tongThanhToan = tongTruocGiam - tongGiam;
                if (txtTongThanhToan != null)
                    txtTongThanhToan.Text = tongThanhToan.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                // Bỏ qua lỗi tính tổng tiền
            }
        }

        /// <summary>
        /// Xuất hóa đơn mua xe (bao gồm xe và phụ tùng nếu có)
        /// </summary>
        private void XuatHoaDonMuaXe(int maGDBan)
        {
            try
            {
                // Lấy thông tin giao dịch bán
                DataTable dtGiaoDich = giaoDichBanBLL.GetGiaoDichBanByMa(maGDBan);
                
                if (dtGiaoDich == null || dtGiaoDich.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin giao dịch!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow giaoDich = dtGiaoDich.Rows[0];

                // Lấy chi tiết phụ tùng (nếu có)
                DataTable dtPhuTung = giaoDichBanBLL.GetChiTietPhuTungBan(maGDBan);

                // Chọn nơi lưu file
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveDialog.FileName = $"HoaDon_MuaXe_{maGDBan}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                saveDialog.Title = "Lưu hóa đơn mua xe";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Xuất PDF
                    PDFHelper.ExportHoaDonMuaXe(giaoDich, dtPhuTung, saveDialog.FileName);

                    // Thông báo thành công và hỏi có muốn mở file không
                    var result = MessageBox.Show(
                        "Xuất hóa đơn thành công!\n\nBạn có muốn mở file vừa xuất không?",
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
                MessageBox.Show($"Lỗi khi xuất hóa đơn: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grpThongTinBan_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Load danh sách khuyến mãi hợp lệ cho xe bán
        /// </summary>
        private void LoadKhuyenMai()
        {
            try
            {
                // Lấy khuyến mãi còn hiệu lực, áp dụng cho "Xe bán" hoặc "Tất cả"
                DataTable dt = khuyenMaiBLL.GetKhuyenMaiHieuLuc(DateTime.Now, "Xe bán");
                
                cboKhuyenMai.Items.Clear();
                cboKhuyenMai.DisplayMember = "TenKM";
                cboKhuyenMai.ValueMember = "MaKM";
                
                // Thêm option "Không áp dụng"
                DataRow emptyRow = dt.NewRow();
                emptyRow["MaKM"] = "";
                emptyRow["TenKM"] = "-- Không áp dụng khuyến mãi --";
                dt.Rows.InsertAt(emptyRow, 0);
                
                cboKhuyenMai.DataSource = dt;
                cboKhuyenMai.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải khuyến mãi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý khi chọn khuyến mãi
        /// </summary>
        private void CboKhuyenMai_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboKhuyenMai.SelectedValue == null || string.IsNullOrEmpty(cboKhuyenMai.SelectedValue.ToString()))
                {
                    khuyenMaiHienTai = null;
                    TinhTongTien();
                    return;
                }

                string maKM = cboKhuyenMai.SelectedValue.ToString();
                
                // Lấy thông tin khuyến mãi từ DataTable
                DataTable dt = (DataTable)cboKhuyenMai.DataSource;
                DataRow[] rows = dt.Select($"MaKM = '{maKM}'");
                
                if (rows.Length > 0)
                {
                    DataRow row = rows[0];
                    khuyenMaiHienTai = new KhuyenMaiDTO
                    {
                        MaKM = row["MaKM"].ToString(),
                        TenKM = row["TenKM"].ToString(),
                        LoaiKhuyenMai = row["LoaiKhuyenMai"] != DBNull.Value ? row["LoaiKhuyenMai"].ToString() : "Giảm %",
                        PhanTramGiam = row["PhanTramGiam"] != DBNull.Value ? Convert.ToDecimal(row["PhanTramGiam"]) : (decimal?)null,
                        SoTienGiam = row["SoTienGiam"] != DBNull.Value ? Convert.ToDecimal(row["SoTienGiam"]) : (decimal?)null,
                        GiaTriGiamToiDa = row["GiaTriGiamToiDa"] != DBNull.Value ? Convert.ToDecimal(row["GiaTriGiamToiDa"]) : (decimal?)null
                    };
                    
                    TinhTongTien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn khuyến mãi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
