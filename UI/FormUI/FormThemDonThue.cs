using BLL;
using DTO;
using System;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace UI.FormUI
{
    public partial class FormThemDonThue : Form
    {
        private KhachHangBLL khachHangBLL;
        private XeMayBLL xeMayBLL;
        private GiaoDichThueBLL giaoDichThueBLL;
        private string maTaiKhoan;

        public FormThemDonThue(string maTK)
        {
            InitializeComponent();
            this.maTaiKhoan = maTK;

            khachHangBLL = new KhachHangBLL();
            xeMayBLL = new XeMayBLL();
            giaoDichThueBLL = new GiaoDichThueBLL();

            LoadKhachHang();
            LoadXeCoTheThue();
            SetupEvents();
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayKetThuc.Value = DateTime.Now.AddDays(1);
            txtTienCoc.Text = "0";
        }

        private void SetupEvents()
        {
            cboKhachHang.SelectedIndexChanged += CboKhachHang_SelectedIndexChanged;
            cboXe.SelectedIndexChanged += CboXe_SelectedIndexChanged;
            dtpNgayBatDau.ValueChanged += DateChanged;
            dtpNgayKetThuc.ValueChanged += DateChanged;
            btnTinhTien.Click += BtnTinhTien_Click;
            btnLuu.Click += BtnLuu_Click;
            btnHuy.Click += BtnHuy_Click;
            txtTienCoc.KeyPress += TxtNumber_KeyPress;
        }

        private void LoadKhachHang()
        {
            try
            {
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
        }

        private void LoadXeCoTheThue()
        {
            try
            {
                // Sử dụng BLL thay vì gọi trực tiếp DAL
                DataTable dt = xeMayBLL.GetXeCoTheThue();

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Hiện tại không có xe nào có thể cho thuê!\n" +
                        "Vui lòng kiểm tra lại kho xe.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    cboXe.DataSource = null;
                    return;
                }

                cboXe.DataSource = dt;
                cboXe.DisplayMember = "TenXe";
                cboXe.ValueMember = "ID_Xe";
                cboXe.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách xe: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedIndex == -1) return;

            DataRowView row = cboKhachHang.SelectedItem as DataRowView;
            if (row != null)
            {
                txtSdtKH.Text = row["Sdt"]?.ToString() ?? "";
                txtEmailKH.Text = row["Email"]?.ToString() ?? "";
            }
        }

        private void CboXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboXe.SelectedIndex == -1) return;

            DataRowView row = cboXe.SelectedItem as DataRowView;
            if (row != null)
            {
                txtBienSo.Text = row["BienSo"]?.ToString() ?? "";
                decimal giaThue = Convert.ToDecimal(row["GiaThueNgay"]);
                txtGiaThueNgay.Text = giaThue.ToString("N0") + " VNĐ";

                // Tự động tính tiền khi chọn xe
                BtnTinhTien_Click(sender, e);
            }
        }

        private void DateChanged(object sender, EventArgs e)
        {
            // Tính số ngày thuê
            int soNgay = (dtpNgayKetThuc.Value.Date - dtpNgayBatDau.Value.Date).Days;

            if (soNgay > 0)
            {
                nudSoNgay.Value = soNgay;
            }
            else
            {
                nudSoNgay.Value = 1;
            }
        }

        private void BtnTinhTien_Click(object sender, EventArgs e)
        {
            if (cboXe.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn xe trước!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpNgayKetThuc.Value <= dtpNgayBatDau.Value)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataRowView row = cboXe.SelectedItem as DataRowView;
                decimal giaThueNgay = Convert.ToDecimal(row["GiaThueNgay"]);
                int soNgay = (int)nudSoNgay.Value;

                decimal tongTien = giaoDichThueBLL.TinhTongGiaThue(
                    dtpNgayBatDau.Value,
                    dtpNgayKetThuc.Value,
                    giaThueNgay
                );

                txtTongTien.Text = tongTien.ToString("N0") + " VNĐ";

                MessageBox.Show(
                    $"Tính tiền thành công!\n\n" +
                    $"Số ngày thuê: {soNgay} ngày\n" +
                    $"Giá thuê/ngày: {giaThueNgay:N0} VNĐ\n" +
                    $"Tổng tiền: {tongTien:N0} VNĐ",
                    "Kết quả tính tiền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính tiền: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Kiểm tra xe có đang được thuê không
                string errorXe = "";
                if (giaoDichThueBLL.IsXeDangThue(idXe, ngayBatDau, ngayKetThuc, out errorXe))
                {
                    MessageBox.Show(errorXe, "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    GiayToGiuLai = txtGiayToGiuLai.Text.Trim(),
                    MaTaiKhoan = maTaiKhoan,
                    TrangThaiDuyet = "Chờ duyệt"
                };

                // Lưu vào database
                string errorMessage = "";
                bool success = giaoDichThueBLL.InsertGiaoDichThue(gd, out errorMessage);

                if (success)
                {
                    MessageBox.Show(
                        "Tạo đơn thuê thành công!\n\n" +
                        $"Khách hàng: {cboKhachHang.Text}\n" +
                        $"Xe: {cboXe.Text}\n" +
                        $"Từ ngày: {ngayBatDau:dd/MM/yyyy}\n" +
                        $"Đến ngày: {ngayKetThuc:dd/MM/yyyy}\n" +
                        $"Tổng tiền: {tongTien:N0} VNĐ\n\n" +
                        "Đơn thuê đang chờ duyệt!",
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

            if (string.IsNullOrWhiteSpace(txtTongTien.Text) || txtTongTien.Text == "0 VNĐ")
            {
                MessageBox.Show("Vui lòng nhấn nút 'TÍNH TIỀN TỰ ĐỘNG' trước khi lưu!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnTinhTien.Focus();
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