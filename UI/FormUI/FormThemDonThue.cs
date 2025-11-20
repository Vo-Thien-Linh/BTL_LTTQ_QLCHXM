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
        private bool isLoadingData = false;
        private string selectedIDXe = "";

        public FormThemDonThue(string maTK)
        {
            InitializeComponent();
            if (string.IsNullOrWhiteSpace(maTK))
            {
                MessageBox.Show(
                    " Lỗi: Không xác định được tài khoản!\n" +
                    "Vui lòng đăng nhập lại.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                this.Close();
                return;
            }
            this.maTaiKhoan = maTK;

            khachHangBLL = new KhachHangBLL();
            xeMayBLL = new XeMayBLL();
            giaoDichThueBLL = new GiaoDichThueBLL();

            SetDefaultValues();
            LoadKhachHang();
            SetupEvents();
            LoadXeTheoThoiGian();
        }

        private void SetDefaultValues()
        {
            dtpNgayBatDau.Value = DateTime.Now.Date;
            dtpNgayKetThuc.Value = DateTime.Now.Date.AddDays(1);
            
            //  Đặt txtTienCoc thành ReadOnly và màu nền khác để phân biệt
            txtTienCoc.Text = "0";
            txtTienCoc.ReadOnly = true;
            txtTienCoc.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            txtTienCoc.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            
            nudSoNgay.Value = 1;
            
            // : Đặt combobox giấy tờ chưa chọn gì
            if (cboGiayToGiuLai.Items.Count > 0)
            {
                cboGiayToGiuLai.SelectedIndex = -1;
            }
        }

        private void SetupEvents()
        {
            cboKhachHang.SelectedIndexChanged += CboKhachHang_SelectedIndexChanged;
            cboXe.SelectedIndexChanged += CboXe_SelectedIndexChanged;
            dtpNgayBatDau.ValueChanged += DateChanged;
            dtpNgayKetThuc.ValueChanged += DateChanged;
            btnLuu.Click += BtnLuu_Click;
            btnHuy.Click += BtnHuy_Click;
            
            //  Không cần KeyPress cho txtTienCoc vì đã ReadOnly
            // txtTienCoc.KeyPress += TxtNumber_KeyPress;
            
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

        private void LoadXeTheoThoiGian()
        {
            try
            {
                isLoadingData = true;

                DateTime ngayBatDau = dtpNgayBatDau.Value.Date;
                DateTime ngayKetThuc = dtpNgayKetThuc.Value.Date;

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

                if (ngayKetThuc <= ngayBatDau)
                {
                    MessageBox.Show(
                        "Ngày kết thúc phải lớn hơn ngày bắt đầu!\n" +
                        "Đã tự động điều chỉnh ngày kết thúc.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    dtpNgayKetThuc.Value = ngayBatDau.AddDays(1);
                    return;
                }

                if (ngayKetThuc <= DateTime.Now.Date)
                {
                    MessageBox.Show(
                        "Ngày kết thúc phải lớn hơn ngày hiện tại!\n" +
                        "Đã tự động điều chỉnh ngày kết thúc.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    dtpNgayKetThuc.Value = DateTime.Now.Date.AddDays(1);
                    return;
                }

                string previousSelectedIDXe = "";
                if (cboXe.SelectedValue != null)
                {
                    previousSelectedIDXe = cboXe.SelectedValue.ToString();
                }

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
                txtTienCoc.Text = "0";
                return;
            }

            DataRowView row = cboXe.SelectedItem as DataRowView;
            if (row != null)
            {
                selectedIDXe = row["ID_Xe"].ToString();

                txtBienSo.Text = row["BienSo"]?.ToString() ?? "";
                decimal giaThue = Convert.ToDecimal(row["GiaThueNgay"]);
                txtGiaThueNgay.Text = giaThue.ToString("N0") + " VNĐ";

                //  Tự động tính tiền cọc dựa trên giá thuê ngày
                TinhTienCoc(giaThue);

                TinhTienTuDong();
            }
        }

        //  Tính tiền cọc tự động
        private void TinhTienCoc(decimal giaThueNgay)
        {
            decimal tienCoc;
            
            if (giaThueNgay < 500000)
            {
                tienCoc = 500000; // 500,000 VNĐ
            }
            else
            {
                tienCoc = 1000000; // 1,000,000 VNĐ
            }

            txtTienCoc.Text = tienCoc.ToString("N0");
            
            // Highlight để người dùng chú ý
            txtTienCoc.BackColor = System.Drawing.Color.FromArgb(255, 243, 205);
            txtTienCoc.ForeColor = System.Drawing.Color.FromArgb(255, 152, 0);
        }

        private void DateChanged(object sender, EventArgs e)
        {
            if (isLoadingData) return;

            int soNgay = (dtpNgayKetThuc.Value.Date - dtpNgayBatDau.Value.Date).Days;
            
            if (soNgay > 0)
            {
                nudSoNgay.Value = soNgay;
            }
            else
            {
                nudSoNgay.Value = 1;
            }

            LoadXeTheoThoiGian();
            TinhTienTuDong();
        }

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
                txtTongTien.BackColor = System.Drawing.Color.FromArgb(200, 230, 201);
                txtTongTien.ForeColor = System.Drawing.Color.FromArgb(56, 142, 60);
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

            if (string.IsNullOrWhiteSpace(maTaiKhoan))
            {
                MessageBox.Show(
                    " Lỗi hệ thống: Không xác định được tài khoản đang đăng nhập!\n" +
                    "Vui lòng đăng nhập lại.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            try
            {
                string maKH = cboKhachHang.SelectedValue.ToString();
                string idXe = cboXe.SelectedValue.ToString();
                DateTime ngayBatDau = dtpNgayBatDau.Value.Date;
                DateTime ngayKetThuc = dtpNgayKetThuc.Value.Date;

                DataRowView rowXe = cboXe.SelectedItem as DataRowView;
                decimal giaThueNgay = Convert.ToDecimal(rowXe["GiaThueNgay"]);

                decimal tongTien = giaoDichThueBLL.TinhTongGiaThue(
                    ngayBatDau, ngayKetThuc, giaThueNgay);

                // Lấy tiền cọc từ textbox (đã tự động tính)
                decimal tienCoc = 0;
                string tienCocText = txtTienCoc.Text.Replace(",", "").Replace(".", "");
                if (!decimal.TryParse(tienCocText, out tienCoc))
                {
                    MessageBox.Show("Lỗi tính tiền cọc!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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
                    LoadXeTheoThoiGian();
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"MaTaiKhoan: {maTaiKhoan}");
                System.Diagnostics.Debug.WriteLine($"MaKH: {maKH}");
                System.Diagnostics.Debug.WriteLine($"ID_Xe: {idXe}");
                System.Diagnostics.Debug.WriteLine($"TienCoc: {tienCoc:N0}");

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
                    SoTienCoc = tienCoc, //  Sử dụng tiền cọc đã tính tự động
                    GiayToGiuLai = cboGiayToGiuLai.SelectedItem?.ToString() ?? "",
                    MaTaiKhoan = maTaiKhoan,
                    TrangThaiDuyet = "Chờ duyệt",
                    HinhThucThanhToan = null
                };

                string errorMessage = "";
                bool success = giaoDichThueBLL.InsertGiaoDichThue(gd, out errorMessage);

                if (success)
                {
                    MessageBox.Show(
                        $"✓ Tạo đơn thuê thành công!\n\n" +
                        $"Khách hàng: {((DataRowView)cboKhachHang.SelectedItem)["HoTenKH"]}\n" +
                        $"Xe: {rowXe["BienSo"]}\n" +
                        $"Thời gian: {ngayBatDau:dd/MM/yyyy} - {ngayKetThuc:dd/MM/yyyy}\n" +
                        $"Tổng tiền: {tongTien:N0} VNĐ\n" +
                        $"Tiền cọc: {tienCoc:N0} VNĐ\n" +
                        $"Giấy tờ giữ lại: {gd.GiayToGiuLai}\n\n" +
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
                        "❌ Không thể tạo đơn thuê!\n\n" + errorMessage,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ Lỗi khi lưu đơn thuê:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
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

            //  Kiểm tra bắt buộc chọn giấy tờ
            if (cboGiayToGiuLai.SelectedIndex == -1 || 
                string.IsNullOrWhiteSpace(cboGiayToGiuLai.Text))
            {
                MessageBox.Show(
                    "⚠ BẮT BUỘC chọn giấy tờ giữ lại!\n\n" +
                    "Vui lòng chọn loại giấy tờ mà khách hàng sẽ để lại làm tài sản thế chấp.",
                    "Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                cboGiayToGiuLai.Focus();
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

            if (dtpNgayKetThuc.Value.Date <= DateTime.Now.Date)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày hiện tại!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayKetThuc.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTongTien.Text) || txtTongTien.Text == "0 VNĐ")
            {
                MessageBox.Show("Tổng tiền chưa được tính!\nVui lòng kiểm tra lại thông tin.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //  Kiểm tra tiền cọc đã được tính
            if (string.IsNullOrWhiteSpace(txtTienCoc.Text) || txtTienCoc.Text == "0")
            {
                MessageBox.Show("Tiền cọc chưa được tính!\nVui lòng chọn xe trước.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
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