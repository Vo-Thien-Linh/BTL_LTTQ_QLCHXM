using BLL;
using DTO;
using System;
using System.Data;
using System.Windows.Forms;

namespace UI.FormUI
{
    public partial class FormThemDonThue : Form
    {
        private KhachHangBLL khachHangBLL;
        private XeMayBLL xeMayBLL;
        private GiaoDichThueBLL giaoDichThueBLL;
        private string maTaiKhoan;
        private bool isLoadingData = false;  //  Cờ tránh trigger event lặp
        private string selectedIDXe = "";    //  Lưu ID xe đã chọn

        public FormThemDonThue(string maTK)
        {
            InitializeComponent();
            this.maTaiKhoan = maTK;

            khachHangBLL = new KhachHangBLL();
            xeMayBLL = new XeMayBLL();
            giaoDichThueBLL = new GiaoDichThueBLL();

            SetDefaultValues();
            LoadKhachHang();
            SetupEvents();  //  Đổi thứ tự: setup events SAU khi load data
            LoadXeTheoThoiGian();
        }

        private void SetDefaultValues()
        {
            //  Đặt ngày mặc định: hôm nay → ngày mai
            dtpNgayBatDau.Value = DateTime.Now.Date;
            dtpNgayKetThuc.Value = DateTime.Now.Date.AddDays(1);
            txtTienCoc.Text = "0";
            nudSoNgay.Value = 1;
        }

        private void SetupEvents()
        {
            cboKhachHang.SelectedIndexChanged += CboKhachHang_SelectedIndexChanged;
            cboXe.SelectedIndexChanged += CboXe_SelectedIndexChanged;
            dtpNgayBatDau.ValueChanged += DateChanged;
            dtpNgayKetThuc.ValueChanged += DateChanged;
            btnLuu.Click += BtnLuu_Click;
            btnHuy.Click += BtnHuy_Click;
            txtTienCoc.KeyPress += TxtNumber_KeyPress;
            
            //  Tự động tính tiền khi thay đổi số ngày
            nudSoNgay.ValueChanged += (s, e) => TinhTienTuDong();
        }

        private void LoadKhachHang()
        {
            try
            {
                isLoadingData = true;
                DataTable dt = khachHangBLL.GetAllKhachHang();

                cboKhachHang.DataSource = dt;
                cboKhachHang.DisplayMember = "HoTenKH";
                cboKhachHang.ValueMember = "MaKH";
                cboKhachHang.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách khách hàng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isLoadingData = false;
            }
        }

        ///  Load xe KHÔNG bị mất dữ liệu khi chọn ngày
        private void LoadXeTheoThoiGian()
        {
            try
            {
                isLoadingData = true;
                
                DateTime ngayBatDau = dtpNgayBatDau.Value.Date;
                DateTime ngayKetThuc = dtpNgayKetThuc.Value.Date;

                // Validate ngày
                if (ngayBatDau >= ngayKetThuc)
                {
                    // KHÔNG xóa cboXe.DataSource, chỉ clear selection
                    cboXe.SelectedIndex = -1;
                    txtBienSo.Text = "";
                    txtGiaThueNgay.Text = "";
                    txtTongTien.Text = "0 VNĐ";
                    return;
                }

                if (ngayBatDau < DateTime.Now.Date)
                {
                    MessageBox.Show(
                        "Ngày bắt đầu không được nhỏ hơn ngày hiện tại!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    dtpNgayBatDau.Value = DateTime.Now.Date;
                    return;
                }

                // ✅ Lưu ID xe đã chọn (nếu có)
                string previousSelectedIDXe = "";
                if (cboXe.SelectedValue != null)
                {
                    previousSelectedIDXe = cboXe.SelectedValue.ToString();
                }

                // Lấy xe khả dụng
                DataTable dt = xeMayBLL.GetXeCoTheThueTheoThoiGian(ngayBatDau, ngayKetThuc);
                
                cboXe.DataSource = dt;
                cboXe.DisplayMember = "TenXe";
                cboXe.ValueMember = "ID_Xe";

                
                if (!string.IsNullOrEmpty(previousSelectedIDXe))
                {
                    bool found = false;
                    for (int i = 0; i < cboXe.Items.Count; i++)
                    {
                        DataRowView row = (DataRowView)cboXe.Items[i];
                        if (row["ID_Xe"].ToString() == previousSelectedIDXe)
                        {
                            cboXe.SelectedIndex = i;
                            found = true;
                            break;
                        }
                    }

                    // Nếu không tìm thấy, thông báo xe không còn khả dụng
                    if (!found)
                    {
                        MessageBox.Show(
                            "Xe bạn đã chọn không còn khả dụng trong thời gian mới!\n" +
                            "Vui lòng chọn xe khác.",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        cboXe.SelectedIndex = -1;
                    }
                }
                else
                {
                    cboXe.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboXe.SelectedIndex = -1;
            }
            finally
            {
                isLoadingData = false;
            }
        }

        private void CboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoadingData || cboKhachHang.SelectedIndex == -1) return;

            DataRowView row = cboKhachHang.SelectedItem as DataRowView;
            if (row != null)
            {
                txtSdtKH.Text = row["Sdt"]?.ToString() ?? "";
                txtEmailKH.Text = row["Email"]?.ToString() ?? "";
            }
        }

        private void CboXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoadingData || cboXe.SelectedIndex == -1)
            {
                txtBienSo.Text = "";
                txtGiaThueNgay.Text = "";
                return;
            }

            DataRowView row = cboXe.SelectedItem as DataRowView;
            if (row != null)
            {
                //  Lưu ID xe đã chọn
                selectedIDXe = row["ID_Xe"].ToString();

                txtBienSo.Text = row["BienSo"]?.ToString() ?? "";
                decimal giaThue = Convert.ToDecimal(row["GiaThueNgay"]);
                txtGiaThueNgay.Text = giaThue.ToString("N0") + " VNĐ";

                //  Tự động tính tiền khi chọn xe
                TinhTienTuDong();
            }
        }

        private void DateChanged(object sender, EventArgs e)
        {
            if (isLoadingData) return;

            //  Tính số ngày thuê
            int soNgay = (dtpNgayKetThuc.Value.Date - dtpNgayBatDau.Value.Date).Days;
            
            if (soNgay > 0)
            {
                nudSoNgay.Value = soNgay;
            }
            else
            {
                nudSoNgay.Value = 1;
            }

            //  Load lại danh sách xe (GIỮ NGUYÊN LỰA CHỌN)
            LoadXeTheoThoiGian();
            
            //  Tính lại tiền
            TinhTienTuDong();
        }

        ///  TỰ ĐỘNG TÍNH TIỀN (không cần nhấn button)
        private void TinhTienTuDong()
        {
            if (isLoadingData || cboXe.SelectedIndex == -1)
            {
                txtTongTien.Text = "0 VNĐ";
                return;
            }

            try
            {
                DataRowView row = cboXe.SelectedItem as DataRowView;
                if (row == null) 
                {
                    txtTongTien.Text = "0 VNĐ";
                    return;
                }

                decimal giaThueNgay = Convert.ToDecimal(row["GiaThueNgay"]);
                DateTime ngayBatDau = dtpNgayBatDau.Value.Date;
                DateTime ngayKetThuc = dtpNgayKetThuc.Value.Date;

                decimal tongTien = giaoDichThueBLL.TinhTongGiaThue(
                    ngayBatDau,
                    ngayKetThuc,
                    giaThueNgay
                );

                txtTongTien.Text = tongTien.ToString("N0") + " VNĐ";
                txtTongTien.BackColor = System.Drawing.Color.FromArgb(200, 230, 201);  // Màu xanh nhạt
                txtTongTien.ForeColor = System.Drawing.Color.FromArgb(56, 142, 60);     // Màu xanh đậm
            }
            catch (Exception ex)
            {
                txtTongTien.Text = "0 VNĐ";
                txtTongTien.BackColor = System.Drawing.Color.White;
                txtTongTien.ForeColor = System.Drawing.Color.Black;
                System.Diagnostics.Debug.WriteLine($"Lỗi tính tiền: {ex.Message}");
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                // Lấy thông tin từ form
                string maKH = cboKhachHang.SelectedValue.ToString();
                string idXe = cboXe.SelectedValue.ToString();
                DateTime ngayBatDau = dtpNgayBatDau.Value.Date;
                DateTime ngayKetThuc = dtpNgayKetThuc.Value.Date;

                DataRowView rowXe = cboXe.SelectedItem as DataRowView;
                decimal giaThueNgay = Convert.ToDecimal(rowXe["GiaThueNgay"]);

                // Tính tổng tiền
                decimal tongTien = giaoDichThueBLL.TinhTongGiaThue(
                    ngayBatDau, ngayKetThuc, giaThueNgay);

                decimal tienCoc = 0;
                if (!string.IsNullOrWhiteSpace(txtTienCoc.Text))
                {
                    decimal.TryParse(txtTienCoc.Text, out tienCoc);
                }

                //  Kiểm tra lại lần cuối trước khi lưu
                string errorXe = "";
                if (giaoDichThueBLL.IsXeDangThue(idXe, ngayBatDau, ngayKetThuc, out errorXe))
                {
                    MessageBox.Show(
                        errorXe + "\n\nXe vừa bị đặt bởi người khác!\n" +
                        "Vui lòng chọn xe khác hoặc thời gian khác.", 
                        "Cảnh báo",
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning
                    );
                    LoadXeTheoThoiGian();  // Reload lại danh sách xe
                    return;
                }

                // Tạo DTO
                GiaoDichThue gd = new GiaoDichThue
                {
                    MaKH = maKH,
                    ID_Xe = idXe,
                    NgayBatDau = ngayBatDau,
                    NgayKetThuc = ngayKetThuc,
                    GiaThueNgay = giaThueNgay,
                    TongGia = tongTien,
                    TrangThai = "Chờ xác nhận",
                    TrangThaiThanhToan = "Chưa thanh toán",
                    SoTienCoc = tienCoc,
                    GiayToGiuLai = cboGiayToGiuLai.SelectedItem?.ToString() ?? "",
                    MaTaiKhoan = maTaiKhoan,
                    TrangThaiDuyet = "Chờ duyệt"
                };

                // Lưu vào database
                string errorMessage = "";
                bool success = giaoDichThueBLL.InsertGiaoDichThue(gd, out errorMessage);

                if (success)
                {
                    MessageBox.Show(
                        $"✓ Tạo đơn thuê thành công!\n\n" +
                        $"Khách hàng: {((DataRowView)cboKhachHang.SelectedItem)["HoTenKH"]}\n" +
                        $"Xe: {rowXe["BienSo"]}\n" +
                        $"Thời gian: {ngayBatDau:dd/MM/yyyy} - {ngayKetThuc:dd/MM/yyyy}\n" +
                        $"Tổng tiền: {tongTien:N0} VNĐ\n\n" +
                        $"Trạng thái: Chờ duyệt",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "Không thể tạo đơn thuê!\n\n" + errorMessage,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi lưu đơn thuê: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private bool ValidateInput()
        {
            if (cboKhachHang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return false;
            }

            if (cboXe.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn xe!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboXe.Focus();
                return false;
            }

            if (dtpNgayBatDau.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Ngày bắt đầu không được nhỏ hơn ngày hiện tại!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayBatDau.Focus();
                return false;
            }

            if (dtpNgayKetThuc.Value <= dtpNgayBatDau.Value)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayKetThuc.Focus();
                return false;
            }

            //  Kiểm tra tổng tiền
            if (string.IsNullOrWhiteSpace(txtTongTien.Text) || txtTongTien.Text == "0 VNĐ")
            {
                MessageBox.Show("Tổng tiền chưa được tính!\nVui lòng kiểm tra lại thông tin.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void TxtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn hủy tạo đơn thuê?",
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
    }
}