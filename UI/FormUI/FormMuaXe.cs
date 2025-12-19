using BLL;
using DTO;
using System;
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
        private string maTaiKhoan;
        private bool isLoadingData = false;
        private KhachHangDTO khachHangHienTai = null;
        private string idXeDaChon = null;
        private DataRowView xeHienTai = null; // Lưu thông tin xe hiện tại
        
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

            SetDefaultValues();
            SetupEvents();
            LoadXeBan();
        }

        // Constructor mới: nhận mã tài khoản và ID xe cụ thể
        public FormMuaXe(string maTK, string idXe) : this(maTK)
        {
            this.idXeDaChon = idXe;
            
            // Load thông tin xe cụ thể
            if (!string.IsNullOrEmpty(idXe))
            {
                LoadThongTinXeChiDinh(idXe);
            }
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
                // Lấy tất cả xe có thể bán
                DataTable dt = xeMayBLL.GetXeCoTheBan();
                
                // Tìm xe theo ID
                bool found = false;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["ID_Xe"].ToString() == idXe)
                    {
                        // Tạo DataRowView để lưu thông tin xe
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = $"ID_Xe = '{idXe}'";
                        if (dv.Count > 0)
                        {
                            xeHienTai = dv[0];
                            
                            // Hiển thị thông tin xe
                            txtTenXe.Text = row["TenXe"]?.ToString() ?? "";
                            txtBienSo.Text = row["BienSo"]?.ToString() ?? "Chưa có";
                            txtSoLuongTon.Text = row["SoLuong"]?.ToString() ?? "0";
                            
                            decimal giaBan = row["GiaBan"] != DBNull.Value ? Convert.ToDecimal(row["GiaBan"]) : 0;
                            txtGiaBan.Text = giaBan.ToString("N0") + " VNĐ";
                            
                            found = true;
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
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải thông tin xe: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
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

                decimal giaBan = xeHienTai["GiaBan"] != DBNull.Value ? Convert.ToDecimal(xeHienTai["GiaBan"]) : 0;

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

                // Lưu vào database
                string errorMessage = "";
                int maGDBan = giaoDichBanBLL.InsertGiaoDichBan(gd, out errorMessage);

                if (maGDBan > 0)
                {
                    MaGDBanVuaTao = maGDBan;
                    
                    // Tạo hợp đồng mua
                    HopDongMuaDTO hopDong = new HopDongMuaDTO
                    {
                        MaGDBan = maGDBan,
                        MaKH = maKH,
                        MaTaiKhoan = maTaiKhoan,
                        ID_Xe = idXe,
                        NgayLap = DateTime.Now,
                        GiaBan = giaBan,
                        DieuKhoan = "Hợp đồng mua bán xe máy theo quy định của pháp luật.",
                        GhiChu = $"Khách hàng: {khachHangHienTai.HoTenKH}, SĐT: {khachHangHienTai.Sdt}",
                        TrangThaiHopDong = "Đang hiệu lực"
                    };
                    
                    string hopDongError = "";
                    int maHDM = hopDongMuaBLL.InsertHopDongMua(hopDong, out hopDongError);
                    
                    string message = $"✓ Bán xe thành công!\n\n" +
                        $"Khách hàng: {khachHangHienTai.HoTenKH}\n" +
                        $"Xe: {xeHienTai["TenXe"]}\n" +
                        $"Biển số: {xeHienTai["BienSo"]}\n" +
                        $"Giá bán: {giaBan:N0} VNĐ\n" +
                        $"Ngày bán: {ngayBan:dd/MM/yyyy}\n\n";
                    
                    if (maHDM > 0)
                    {
                        message += $"✓ Đã tạo hợp đồng mua #HDM{maHDM}\n\n" +
                            $"Bạn có muốn xem hợp đồng không?";
                        
                        var result = MessageBox.Show(message, "Thành công",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        
                        if (result == DialogResult.Yes)
                        {
                            // Mở form xem hợp đồng
                            FormXemHopDongMua frmXemHD = new FormXemHopDongMua(maGDBan);
                            frmXemHD.ShowDialog();
                        }
                    }
                    else
                    {
                        message += $"⚠ Cảnh báo: Chưa tạo được hợp đồng\n{hopDongError}";
                        MessageBox.Show(message, "Thành công (Có cảnh báo)",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
