using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.FormUI
{
    public partial class FormQuanLiNV : Form
    {
        private NhanVienBLL nhanVienBLL;
        private string maNV;
        private bool isEditMode;
        private byte[] imageData;
        private Dictionary<TextBox, Label> errorLabels;

        public FormQuanLiNV()
        {
            InitializeComponent();
            nhanVienBLL = new NhanVienBLL();
            isEditMode = false;
            InitializeForm();
            LoadComboBoxData();
            GenerateMaNV();
            InitializeValidation();
        }

        public FormQuanLiNV(string maNV)
        {
            InitializeComponent();
            nhanVienBLL = new NhanVienBLL();
            this.maNV = maNV;
            isEditMode = true;
            InitializeForm();
            LoadComboBoxData();
            LoadNhanVienData();
            InitializeValidation();
        }

        private void InitializeValidation()
        {
            // Khởi tạo dictionary để lưu error labels
            errorLabels = new Dictionary<TextBox, Label>();

            // Tạo error labels cho từng textbox
            CreateErrorLabel(txtHoTen);
            CreateErrorLabelForDatePicker(dtpNgaySinh);
            CreateErrorLabel(txtSdt);
            CreateErrorLabel(txtEmail);
            CreateErrorLabel(txtDiaChi);
            CreateErrorLabel(txtMatKhau);
            CreateErrorLabel(txtCCCD);

            // Hook up validation events - chỉ validate, không disable
            txtHoTen.TextChanged += TxtHoTen_TextChanged;
            dtpNgaySinh.ValueChanged += DtpNgaySinh_ValueChanged;
            txtSdt.TextChanged += TxtSdt_TextChanged;
            txtSdt.KeyPress += TxtSdt_KeyPress; // Chỉ cho phép nhập số
            txtEmail.TextChanged += TxtEmail_TextChanged;
            txtDiaChi.TextChanged += TxtDiaChi_TextChanged;
            txtMatKhau.TextChanged += TxtMatKhau_TextChanged;
            txtCCCD.TextChanged += TxtCCCD_TextChanged;
            txtCCCD.KeyPress += TxtCCCD_KeyPress; // Chỉ cho phép nhập số
        }

        private void TxtSdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và phím điều khiển (Backspace, Delete, ...)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtCCCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và phím điều khiển
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void CreateErrorLabel(TextBox textBox)
        {
            Label errorLabel = new Label
            {
                AutoSize = true,
                ForeColor = Color.Red,
                Font = new Font(textBox.Font.FontFamily, 8.5f, FontStyle.Regular),
                Location = new Point(textBox.Left, textBox.Bottom + 2),
                Visible = false
            };
            textBox.Parent.Controls.Add(errorLabel);
            errorLabel.BringToFront();
            errorLabels[textBox] = errorLabel;
        }

        private void CreateErrorLabelForDatePicker(DateTimePicker dtp)
        {
            Label errorLabel = new Label
            {
                AutoSize = true,
                ForeColor = Color.Red,
                Font = new Font(dtp.Font.FontFamily, 8.5f, FontStyle.Regular),
                Location = new Point(dtp.Left, dtp.Bottom + 2),
                Visible = false
            };
            dtp.Parent.Controls.Add(errorLabel);
            errorLabel.BringToFront();
            
            // Tạo một textbox dummy để lưu vào dictionary (vì dictionary chỉ chấp nhận TextBox)
            // Nhưng ta sẽ truy cập trực tiếp thông qua tag
            dtp.Tag = errorLabel;
        }

        private void ShowErrorForDatePicker(DateTimePicker dtp, string message)
        {
            if (dtp.Tag is Label errorLabel)
            {
                errorLabel.Text = message;
                errorLabel.Visible = true;
            }
        }

        private void HideErrorForDatePicker(DateTimePicker dtp)
        {
            if (dtp.Tag is Label errorLabel)
            {
                errorLabel.Visible = false;
            }
        }

        private void ShowError(TextBox textBox, string message)
        {
            if (errorLabels.ContainsKey(textBox))
            {
                errorLabels[textBox].Text = message;
                errorLabels[textBox].Visible = true;
            }
        }

        private void HideError(TextBox textBox)
        {
            if (errorLabels.ContainsKey(textBox))
            {
                errorLabels[textBox].Visible = false;
            }
        }

        private void TxtHoTen_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                ShowError(txtHoTen, "⚠ Họ tên không được để trống");
            }
            else if (txtHoTen.Text.Trim().Length < 2)
            {
                ShowError(txtHoTen, "⚠ Họ tên phải có ít nhất 2 ký tự");
            }
            else
            {
                HideError(txtHoTen);
            }
        }



        private void DtpNgaySinh_ValueChanged(object sender, EventArgs e)
        {
            // Kiểm tra tuổi: phải từ 18 đến 65 tuổi
            int age = DateTime.Now.Year - dtpNgaySinh.Value.Year;
            if (dtpNgaySinh.Value > DateTime.Now.AddYears(-age)) age--;

            if (age < 18)
            {
                ShowErrorForDatePicker(dtpNgaySinh, "⚠ Nhân viên phải từ 18 tuổi trở lên");
            }
            else if (age > 65)
            {
                ShowErrorForDatePicker(dtpNgaySinh, "⚠ Tuổi nhân viên không được quá 65");
            }
            else
            {
                HideErrorForDatePicker(dtpNgaySinh);
            }
        }





        private void TxtEmail_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                ShowError(txtEmail, "⚠ Email là bắt buộc để tạo tài khoản");
            }
            else if (!IsValidEmail(txtEmail.Text.Trim()))
            {
                ShowError(txtEmail, "⚠ Email không hợp lệ (ví dụ: example@gmail.com)");
            }
            else
            {
                HideError(txtEmail);
            }
        }



        private void TxtDiaChi_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                ShowError(txtDiaChi, "⚠ Địa chỉ là bắt buộc");
            }
            else if (txtDiaChi.Text.Trim().Length < 5)
            {
                ShowError(txtDiaChi, "⚠ Địa chỉ phải có ít nhất 5 ký tự");
            }
            else
            {
                HideError(txtDiaChi);
            }
        }



        private void TxtSdt_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSdt.Text))
            {
                ShowError(txtSdt, "⚠ Số điện thoại là bắt buộc");
            }
            else if (!IsValidPhoneNumber(txtSdt.Text.Trim()))
            {
                ShowError(txtSdt, "⚠ SĐT không hợp lệ (10 số, bắt đầu bằng 0)");
            }
            else
            {
                HideError(txtSdt);
            }
        }



        private void TxtMatKhau_TextChanged(object sender, EventArgs e)
        {
            if (!isEditMode)
            {
                if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
                {
                    ShowError(txtMatKhau, "⚠ Mật khẩu là bắt buộc");
                }
                else if (txtMatKhau.Text.Trim().Length < 6)
                {
                    ShowError(txtMatKhau, "⚠ Mật khẩu phải có ít nhất 6 ký tự");
                }
                else
                {
                    HideError(txtMatKhau);
                }
            }
        }







        private void TxtCCCD_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCCCD.Text) && !IsValidCCCD(txtCCCD.Text.Trim()))
            {
                ShowError(txtCCCD, "⚠ CCCD phải là 12 số");
            }
            else
            {
                HideError(txtCCCD);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email && email.Contains("@") && email.Contains(".");
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phone)
        {
            // Kiểm tra có phải 10 số và bắt đầu bằng 0
            return phone.Length == 10 && phone.All(char.IsDigit) && phone.StartsWith("0");
        }

        private bool IsValidCCCD(string cccd)
        {
            // CCCD phải là 12 số
            return cccd.Length == 12 && cccd.All(char.IsDigit);
        }


        private void InitializeForm()
        {
            this.Text = isEditMode ? "Sửa Thông Tin Nhân Viên" : "Thêm Nhân Viên Mới";
            lblTitle.Text = isEditMode ? "SỬA THÔNG TIN NHÂN VIÊN" : "THÊM NHÂN VIÊN MỚI";
            txtMaNV.ReadOnly = true; // Luôn readonly, không cho nhập tay
        }

        private void LoadComboBoxData()
        {
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.AddRange(new string[] { "Nam", "Nữ", "Khác" });
            cboGioiTinh.SelectedIndex = 0;

            cboChucVu.Items.Clear();
            cboChucVu.Items.AddRange(new string[] {
                "Quản lý",
                "Thu ngân",
                "Kỹ thuật",
                "Bán hàng",
                "Nhân viên"
            });
            cboChucVu.SelectedIndex = 4;

            cboTinhTrang.Items.Clear();
            if (isEditMode)
            {
                // Chế độ sửa: Cho phép tất cả trạng thái
                cboTinhTrang.Items.AddRange(new string[] {
                    "Thử việc",
                    "Còn làm",
                    "Nghỉ làm"
                });
            }
            else
            {
                // Chế độ thêm mới: Không cho phép "Nghỉ làm"
                cboTinhTrang.Items.AddRange(new string[] {
                    "Thử việc",
                    "Còn làm"
                });
            }
            cboTinhTrang.SelectedIndex = 0;

            cboTrinhDo.Items.Clear();
            cboTrinhDo.Items.AddRange(new string[] {
                "THPT",
                "Trung cấp",
                "Cao đẳng",
                "Đại học",
                "Sau đại học"
            });
            cboTrinhDo.SelectedIndex = 0;
        }

        private void GenerateMaNV()
        {
            try
            {
                txtMaNV.Text = nhanVienBLL.GenerateMaNV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo mã nhân viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNhanVienData()
        {
            try
            {
                var dt = nhanVienBLL.GetNhanVienByMaNV(maNV);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    txtMaNV.Text = row["MaNV"].ToString();
                    txtHoTen.Text = row["HoTenNV"].ToString();
                    dtpNgaySinh.Value = Convert.ToDateTime(row["NgaySinh"]);
                    cboGioiTinh.SelectedItem = row["GioiTinh"].ToString();
                    txtSdt.Text = row["Sdt"] != DBNull.Value ? row["Sdt"].ToString() : "";
                    txtEmail.Text = row["Email"] != DBNull.Value ? row["Email"].ToString() : "";
                    txtDiaChi.Text = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : "";
                    cboChucVu.SelectedItem = row["ChucVu"] != DBNull.Value ? row["ChucVu"].ToString() : "";
                    numLuong.Value = row["LuongCoBan"] != DBNull.Value ? Convert.ToDecimal(row["LuongCoBan"]) : 0;
                    cboTinhTrang.SelectedItem = row["TinhTrangLamViec"].ToString();
                    txtCCCD.Text = row["CCCD"] != DBNull.Value ? row["CCCD"].ToString() : "";
                    cboTrinhDo.SelectedItem = row["TrinhDoHocVan"] != DBNull.Value ? row["TrinhDoHocVan"].ToString() : "";

                    if (row["AnhNhanVien"] != DBNull.Value)
                    {
                        byte[] imgData = (byte[])row["AnhNhanVien"];
                        using (MemoryStream ms = new MemoryStream(imgData))
                        {
                            picNhanVien.Image = Image.FromStream(ms);
                        }
                        imageData = imgData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnChooseImage_Click(object sender, EventArgs e)
        {
            
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, width, height);
            }
            return result;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
           
        }

        private bool ValidateInput()
        {
            // 1. Mã nhân viên
            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show("Mã nhân viên là bắt buộc!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Focus();
                return false;
            }

            // 2. Họ tên
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                ShowError(txtHoTen, "⚠ Họ tên không được để trống");
                txtHoTen.Focus();
                return false;
            }
            else if (txtHoTen.Text.Trim().Length < 2)
            {
                ShowError(txtHoTen, "⚠ Họ tên phải có ít nhất 2 ký tự");
                txtHoTen.Focus();
                return false;
            }

            // 3. Ngày sinh (18-65 tuổi)
            int age = DateTime.Now.Year - dtpNgaySinh.Value.Year;
            if (dtpNgaySinh.Value > DateTime.Now.AddYears(-age)) age--;
            if (age < 18 || age > 65)
            {
                MessageBox.Show("Nhân viên phải từ 18 đến 65 tuổi!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }

            // 4. Giới tính
            if (cboGioiTinh.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGioiTinh.Focus();
                return false;
            }

            // 5. Số điện thoại
            if (string.IsNullOrWhiteSpace(txtSdt.Text))
            {
                ShowError(txtSdt, "⚠ Số điện thoại là bắt buộc");
                txtSdt.Focus();
                return false;
            }
            else if (!IsValidPhoneNumber(txtSdt.Text.Trim()))
            {
                ShowError(txtSdt, "⚠ Số điện thoại không hợp lệ (10 số)");
                txtSdt.Focus();
                return false;
            }

            // 6. Email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                ShowError(txtEmail, "⚠ Email là bắt buộc để tạo tài khoản");
                txtEmail.Focus();
                return false;
            }
            else if (!IsValidEmail(txtEmail.Text.Trim()))
            {
                ShowError(txtEmail, "⚠ Email không hợp lệ");
                txtEmail.Focus();
                return false;
            }

            // 7. Địa chỉ
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                ShowError(txtDiaChi, "⚠ Địa chỉ không được để trống");
                txtDiaChi.Focus();
                return false;
            }
            else if (txtDiaChi.Text.Trim().Length < 5)
            {
                ShowError(txtDiaChi, "⚠ Địa chỉ phải có ít nhất 5 ký tự");
                txtDiaChi.Focus();
                return false;
            }

            // 8. Mật khẩu (chỉ khi thêm mới)
            if (!isEditMode)
            {
                if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
                {
                    ShowError(txtMatKhau, "⚠ Mật khẩu là bắt buộc");
                    txtMatKhau.Focus();
                    return false;
                }
                else if (txtMatKhau.Text.Trim().Length < 6)
                {
                    ShowError(txtMatKhau, "⚠ Mật khẩu phải có ít nhất 6 ký tự");
                    txtMatKhau.Focus();
                    return false;
                }
            }

            // 9. Chức vụ
            if (cboChucVu.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn chức vụ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboChucVu.Focus();
                return false;
            }

            // 10. Tình trạng
            if (cboTinhTrang.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn tình trạng làm việc!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTinhTrang.Focus();
                return false;
            }

            // 11. CCCD (nếu có nhập)
            if (!string.IsNullOrWhiteSpace(txtCCCD.Text) && !IsValidCCCD(txtCCCD.Text.Trim()))
            {
                ShowError(txtCCCD, "⚠ CCCD phải là 12 số");
                txtCCCD.Focus();
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            // Kiểm tra ảnh nhân viên
            if (imageData == null || imageData.Length == 0)
            {
                MessageBox.Show("Chưa có ảnh nhân viên!\n\nVui lòng chọn ảnh trước khi lưu.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // ✅ Sinh mã nhân viên mới ngay trước khi lưu (chỉ ở chế độ thêm mới)
                if (!isEditMode)
                {
                    txtMaNV.Text = nhanVienBLL.GenerateMaNV();
                }

                NhanVien nv = new NhanVien
                {
                    MaNV = txtMaNV.Text.Trim(),
                    HoTenNV = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = cboGioiTinh.SelectedItem.ToString(),
                    Sdt = txtSdt.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    ChucVu = cboChucVu.SelectedItem?.ToString(),
                    LuongCoBan = numLuong.Value,
                    TinhTrangLamViec = cboTinhTrang.SelectedItem.ToString(),
                    CCCD = txtCCCD.Text.Trim(),
                    TrinhDoHocVan = cboTrinhDo.SelectedItem?.ToString(),
                    AnhNhanVien = imageData,
                    Password = txtMatKhau.Text.Trim() // Lấy mật khẩu từ TextBox
                };

                string errorMessage;
                bool result;

                if (isEditMode)
                {
                    result = nhanVienBLL.UpdateNhanVien(nv, out errorMessage);

                    if (result)
                    {
                        MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    result = nhanVienBLL.InsertNhanVien(nv, out errorMessage);

                    if (result)
                    {
                        // ✅ KIỂM TRA XEM TÀI KHOẢN CÓ ĐƯỢC TẠO THÀNH CÔNG KHÔNG
                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            // ⚠️ Có lỗi khi tạo tài khoản
                            MessageBox.Show($"✅ Thêm nhân viên thành công!\n\n" +
                                          $"⚠️ CẢNH BÁO:\n{errorMessage}",
                                "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (!string.IsNullOrEmpty(nv.Email) && !string.IsNullOrEmpty(nv.Password))
                        {
                            // ✅ Tạo tài khoản thành công
                            string message = "✅ THÊM NHÂN VIÊN THÀNH CÔNG!\n\n" +
                                           "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                                           "📱 THÔNG TIN TÀI KHOẢN:\n" +
                                           "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
                                           $"👤 Tên đăng nhập:  {nv.Email}\n" +
                                           $"🔑 Mật khẩu:            {nv.Password}\n\n" +
                                           "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                                           "⚠️  LƯU Ý QUAN TRỌNG:\n" +
                                           "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
                                           "🔸 Vui lòng thông báo thông tin này\n" +
                                           "   cho nhân viên!\n" +
                                           "🔸 Nhân viên nên đổi mật khẩu sau lần\n" +
                                           "   đăng nhập đầu tiên!\n" +
                                           "🔸 Không chia sẻ thông tin tài khoản\n" +
                                           "   cho người khác!";

                            MessageBox.Show(message, "🎉 Tạo tài khoản thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Thêm nhân viên thành công!\n\n⚠️ Không tạo được tài khoản do thiếu email hoặc mật khẩu.",
                                "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChooseImage_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Chọn ảnh nhân viên";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image img = Image.FromFile(ofd.FileName);
                        Image resizedImg = ResizeImage(img, 200, 200);
                        picNhanVien.Image = resizedImg;

                        using (MemoryStream ms = new MemoryStream())
                        {
                            resizedImg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            imageData = ms.ToArray();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi tải ảnh: " + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
