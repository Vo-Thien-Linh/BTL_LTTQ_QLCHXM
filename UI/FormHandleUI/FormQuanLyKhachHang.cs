using BLL;
using DTO;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace UI.FormHandleUI
{
    public partial class FormQuanLyKhachHang : Form
    {
        private KhachHangBLL khachHangBLL;
        private string maKH;
        private bool isEditMode;

        public FormQuanLyKhachHang()
        {
            InitializeComponent();
            khachHangBLL = new KhachHangBLL();
            isEditMode = false;
            InitializeForm();
            LoadComboBoxData();
            GenerateMaKH();
            AttachFormatEvents();
        }

        public FormQuanLyKhachHang(string maKH)
        {
            InitializeComponent();
            khachHangBLL = new KhachHangBLL();
            this.maKH = maKH;
            isEditMode = true;
            InitializeForm();
            LoadComboBoxData();
            LoadKhachHangData();
            AttachFormatEvents();
        }

        private void InitializeForm()
        {
            this.Text = isEditMode ? "Sửa Thông Tin Khách Hàng" : "Thêm Khách Hàng Mới";
            lblTitle.Text = isEditMode ? "SỬA THÔNG TIN KHÁCH HÀNG" : "THÊM KHÁCH HÀNG MỚI";
            txtMaKH.ReadOnly = isEditMode;
        }

        private void LoadComboBoxData()
        {
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.AddRange(new string[] { "Nam", "Nữ", "Khác" });
            cboGioiTinh.SelectedIndex = 0;
        }

        /// <summary>
        /// Gắn các sự kiện format tự động cho textbox
        /// </summary>
        private void AttachFormatEvents()
        {
            // Format họ tên: Viết hoa chữ cái đầu mỗi từ
            txtHoTen.Leave += (s, e) => {
                if (!string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    string[] words = txtHoTen.Text.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words[i].Length > 0)
                        {
                            words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                        }
                    }
                    txtHoTen.Text = string.Join(" ", words);
                }
            };

            // Format SDT: Chỉ cho phép nhập số
            txtSdt.KeyPress += (s, e) => {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            };

            // Giới hạn độ dài SDT (10-11 số)
            txtSdt.MaxLength = 11;

            // Format Email: Chuyển về chữ thường
            txtEmail.Leave += (s, e) => {
                if (!string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    txtEmail.Text = txtEmail.Text.Trim().ToLower();
                }
            };

            // Giới hạn độ dài các trường
            txtHoTen.MaxLength = 100;
            txtEmail.MaxLength = 100;
            txtDiaChi.MaxLength = 255;
        }

        private void GenerateMaKH()
        {
            try
            {
                txtMaKH.Text = khachHangBLL.GenerateMaKH();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo mã khách hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadKhachHangData()
        {
            try
            {
                var dt = khachHangBLL.GetKhachHangByMaKH(maKH);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    txtMaKH.Text = row["MaKH"].ToString();
                    txtHoTen.Text = row["HoTenKH"].ToString();

                    if (row["NgaySinh"] != DBNull.Value)
                    {
                        dtpNgaySinh.Value = Convert.ToDateTime(row["NgaySinh"]);
                        chkNgaySinh.Checked = true;
                    }
                    else
                    {
                        chkNgaySinh.Checked = false;
                        dtpNgaySinh.Enabled = false;
                    }

                    cboGioiTinh.SelectedItem = row["GioiTinh"] != DBNull.Value ? row["GioiTinh"].ToString() : "Nam";
                    txtSdt.Text = row["Sdt"] != DBNull.Value ? row["Sdt"].ToString() : "";
                    txtEmail.Text = row["Email"] != DBNull.Value ? row["Email"].ToString() : "";
                    txtDiaChi.Text = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                // Xác nhận trước khi lưu (chỉ khi sửa)
                if (isEditMode)
                {
                    DialogResult confirmResult = MessageBox.Show(
                        "Bạn có chắc chắn muốn cập nhật thông tin khách hàng này?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirmResult == DialogResult.No)
                        return;
                }

                KhachHangDTO kh = new KhachHangDTO
                {
                    MaKH = txtMaKH.Text.Trim(),
                    HoTenKH = txtHoTen.Text.Trim(),
                    NgaySinh = chkNgaySinh.Checked ? (DateTime?)dtpNgaySinh.Value : null,
                    GioiTinh = cboGioiTinh.SelectedItem.ToString(),
                    Sdt = txtSdt.Text.Trim(),
                    Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                    DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim()
                };

                string errorMessage;
                bool result;

                if (isEditMode)
                {
                    result = khachHangBLL.UpdateKhachHang(kh, out errorMessage);
                }
                else
                {
                    result = khachHangBLL.InsertKhachHang(kh, out errorMessage);
                }

                if (result)
                {
                    string message = isEditMode
                        ? "Cập nhật thông tin khách hàng thành công!"
                        : $"Thêm khách hàng mới thành công!\nMã KH: {kh.MaKH}";

                    MessageBox.Show(
                        message,
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

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
                MessageBox.Show(
                    $"Lỗi không mong muốn:\n{ex.Message}\n\nVui lòng liên hệ quản trị viên!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private bool ValidateInput()
        {
            // 1. Kiểm tra mã khách hàng
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return false;
            }

            // 2. Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên khách hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            // Validate họ tên: không chứa số hoặc ký tự đặc biệt
            string hoTen = txtHoTen.Text.Trim();
            if (!Regex.IsMatch(hoTen, @"^[\p{L}\s]+$"))
            {
                MessageBox.Show(
                    "Họ tên không được chứa số hoặc ký tự đặc biệt!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtHoTen.Focus();
                return false;
            }

            if (hoTen.Length < 2)
            {
                MessageBox.Show(
                    "Họ tên phải có ít nhất 2 ký tự!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtHoTen.Focus();
                return false;
            }

            // 3. Kiểm tra giới tính
            if (cboGioiTinh.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGioiTinh.Focus();
                return false;
            }

            // 4. Kiểm tra số điện thoại (BẮT BUỘC)
            if (string.IsNullOrWhiteSpace(txtSdt.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập số điện thoại!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtSdt.Focus();
                return false;
            }

            string sdt = txtSdt.Text.Trim();

            // Validate format SDT Việt Nam (10-11 số, bắt đầu bằng 0)
            if (!Regex.IsMatch(sdt, @"^0[0-9]{9,10}$"))
            {
                MessageBox.Show(
                    "Số điện thoại không hợp lệ!\n" +
                    "Định dạng: 10-11 số, bắt đầu bằng 0\n" +
                    "VD: 0901234567",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtSdt.Focus();
                return false;
            }

            // Kiểm tra trùng SDT
            try
            {
                if (isEditMode)
                {
                    // Khi sửa: kiểm tra SDT trùng với KH khác (loại trừ KH hiện tại)
                    if (khachHangBLL.IsSDTExists(sdt, txtMaKH.Text.Trim()))
                    {
                        MessageBox.Show(
                            "Số điện thoại đã được sử dụng bởi khách hàng khác!",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        txtSdt.Focus();
                        return false;
                    }
                }
                else
                {
                    // Khi thêm mới: kiểm tra SDT có tồn tại chưa
                    if (khachHangBLL.IsSDTExists(sdt))
                    {
                        MessageBox.Show(
                            "Số điện thoại đã tồn tại trong hệ thống!",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        txtSdt.Focus();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi kiểm tra số điện thoại: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            // 5. Kiểm tra Email (tùy chọn, nhưng nếu có thì phải đúng format)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                string email = txtEmail.Text.Trim();

                // Validate format email
                if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    MessageBox.Show(
                        "Email không hợp lệ!\n" +
                        "VD: example@domain.com",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtEmail.Focus();
                    return false;
                }

                // Kiểm tra trùng email
                try
                {
                    if (isEditMode)
                    {
                        if (khachHangBLL.IsEmailExists(email, txtMaKH.Text.Trim()))
                        {
                            MessageBox.Show(
                                "Email đã được sử dụng bởi khách hàng khác!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            txtEmail.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        if (khachHangBLL.IsEmailExists(email))
                        {
                            MessageBox.Show(
                                "Email đã tồn tại trong hệ thống!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            txtEmail.Focus();
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Lỗi kiểm tra email: {ex.Message}",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return false;
                }
            }

            // 6. Kiểm tra ngày sinh (nếu được chọn)
            if (chkNgaySinh.Checked)
            {
                DateTime ngaySinh = dtpNgaySinh.Value.Date;
                DateTime ngayHienTai = DateTime.Now.Date;

                // Kiểm tra ngày sinh không được là tương lai
                if (ngaySinh > ngayHienTai)
                {
                    MessageBox.Show(
                        "Ngày sinh không được là ngày tương lai!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    dtpNgaySinh.Focus();
                    return false;
                }

                // Tính tuổi chính xác
                int tuoi = ngayHienTai.Year - ngaySinh.Year;
                if (ngayHienTai.Month < ngaySinh.Month ||
                    (ngayHienTai.Month == ngaySinh.Month && ngayHienTai.Day < ngaySinh.Day))
                {
                    tuoi--;
                }

                // Kiểm tra tuổi >= 18
                if (tuoi < 18)
                {
                    MessageBox.Show(
                        $"Khách hàng phải đủ 18 tuổi!\n" +
                        $"Tuổi hiện tại: {tuoi}",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    dtpNgaySinh.Focus();
                    return false;
                }

                // Kiểm tra tuổi hợp lý (không quá 120)
                if (tuoi > 120)
                {
                    MessageBox.Show(
                        "Ngày sinh không hợp lệ! (Tuổi > 120)",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    dtpNgaySinh.Focus();
                    return false;
                }
            }

            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ChkNgaySinh_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgaySinh.Enabled = chkNgaySinh.Checked;
            if (!chkNgaySinh.Checked)
            {
                dtpNgaySinh.Value = DateTime.Now;
            }
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            // No implementation needed
        }
    }
}