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

namespace UI.FormHandleUI
{
    public partial class FormSuaXe : Form
    {
        private ErrorProvider errorProvider1 = new ErrorProvider();
        private string idXe;
        private XeMayDTO currentXe;
        private XeMayBLL xeMayBLL = new XeMayBLL();
        private HangXeBLL hangXeBLL = new HangXeBLL();
        private DongXeBLL dongXeBLL = new DongXeBLL();
        private MauSacBLL mauSacBLL = new MauSacBLL();
        private NhaCungCapBLL nhaCungCapBLL = new NhaCungCapBLL();
        private LoaiXeBLL loaiXeBLL = new LoaiXeBLL();
        private byte[] anhXeBytes = null;

        private LanguageManagerBLL langMgr = LanguageManagerBLL.Instance;

        public FormSuaXe(string idXe)
        {
            InitializeComponent();
            this.idXe = idXe;

            txtMaXe.Validating += txtMaXe_Validating;
            txtGiaMua.Validating += txtGiaMua_Validating;
            txtGiaNhap.Validating += txtGiaNhap_Validating;

            langMgr.LanguageChanged += (s, e) => ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            this.Text = langMgr.GetString("EditVehicle") ?? "SỬA XE";

            UpdateAllLabels(this);

            if (cbbTrangThai != null)
            {
                int selectedIndex = cbbTrangThai.SelectedIndex;
                cbbTrangThai.Items.Clear();
                cbbTrangThai.Items.Add(langMgr.GetString("ReadyStatus") ?? "Sẵn sàng");
                cbbTrangThai.Items.Add(langMgr.GetString("RentedStatus") ?? "Đang thuê");
                cbbTrangThai.Items.Add(langMgr.GetString("SoldStatus") ?? "Đã bán");
                cbbTrangThai.Items.Add(langMgr.GetString("MaintenanceStatus") ?? "Đang bảo trì");
                if (selectedIndex >= 0 && selectedIndex < cbbTrangThai.Items.Count)
                    cbbTrangThai.SelectedIndex = selectedIndex;
            }

            if (cbbMucDichSuDung != null)
            {
                int selectedIndex = cbbMucDichSuDung.SelectedIndex;
                cbbMucDichSuDung.Items.Clear();
                cbbMucDichSuDung.Items.Add(langMgr.GetString("ForRent") ?? "Cho thuê");
                cbbMucDichSuDung.Items.Add(langMgr.GetString("ForSale") ?? "Bán");
                if (selectedIndex >= 0 && selectedIndex < cbbMucDichSuDung.Items.Count)
                    cbbMucDichSuDung.SelectedIndex = selectedIndex;
            }
        }

        private void UpdateAllLabels(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Label lbl)
                {
                    string originalText = lbl.Text.Trim().TrimEnd(':');

                    if (originalText == "SỬA XE" || originalText == "EDIT VEHICLE")
                    {
                        lbl.Text = (langMgr.GetString("EditVehicle") ?? "SỬA XE").ToUpper();
                    }
                    else
                    {
                        string labelKey = GetLanguageKeyForLabel(originalText);
                        if (!string.IsNullOrEmpty(labelKey))
                            lbl.Text = langMgr.GetString(labelKey) ?? lbl.Text;
                    }
                }
                else if (ctrl is Button btn)
                {
                    string btnText = btn.Text.Trim();
                    if (btnText == "Choose Image" || btnText == "Chọn file ảnh")
                        btn.Text = langMgr.GetString("ChooseImage") ?? "Chọn file ảnh";
                    else if (btnText == "Edit Vehicle" || btnText == "Sửa xe")
                        btn.Text = langMgr.GetString("EditVehicle") ?? "Sửa xe";
                }
                else if (ctrl is GroupBox gb)
                {
                    string gbText = gb.Text.Trim();
                    if (gbText == "General Information" || gbText == "Thông tin chung")
                        gb.Text = langMgr.GetString("GeneralInfo") ?? "Thông tin chung";
                    else if (gbText == "Technical Information" || gbText == "Thông tin kỹ thuật")
                        gb.Text = langMgr.GetString("TechnicalInfo") ?? "Thông tin kỹ thuật";
                }

                if (ctrl.HasChildren)
                {
                    UpdateAllLabels(ctrl);
                }
            }
        }

        private string GetLanguageKeyForLabel(string labelText)
        {
            var mapping = new Dictionary<string, string>
            {
                { "Mã xe", "VehicleID" },
                { "Vehicle ID", "VehicleID" },
                { "VehicleID", "VehicleID" },
                { "Biển số", "PlateNumber" },
                { "Plate Number", "PlateNumber" },
                { "PlateNumber", "PlateNumber" },
                { "Hãng xe", "Brand" },
                { "Brand", "Brand" },
                { "Dòng xe", "Model" },
                { "Model", "Model" },
                { "Màu sắc", "Color" },
                { "Color", "Color" },
                { "Năm sản xuất", "YearOfManufacture" },
                { "Year", "YearOfManufacture" },
                { "Nhà cung cấp", "Supplier" },
                { "Supplier", "Supplier" },
                { "Loại xe", "VehicleType" },
                { "Type", "VehicleType" },
                { "Ngày mua", "PurchaseDate" },
                { "Purchase Date", "PurchaseDate" },
                { "Giá mua", "PurchasePrice" },
                { "Price", "PurchasePrice" },
                { "Ngày đăng ký", "RegistrationDate" },
                { "Registration Date", "RegistrationDate" },
                { "Reg. Date", "RegistrationDate" },
                { "KM đã chạy", "Mileage" },
                { "Mileage", "Mileage" },
                { "Thông tin xăng", "FuelInfo" },
                { "Fuel Type", "FuelInfo" },
                { "Trạng thái", "Status" },
                { "Status", "Status" },
                { "Ngày hết hạn đăng ký", "RegistrationExpiry" },
                { "Reg. Expiry", "RegistrationExpiry" },
                { "Reg Expiry", "RegistrationExpiry" },
                { "Ngày hết hạn bảo hiểm", "InsuranceExpiry" },
                { "Insurance Expiry", "InsuranceExpiry" },
                { "Mục đích sử dụng", "Purpose" },
                { "Purpose", "Purpose" },
                { "Giá nhập", "ImportPrice" },
                { "Import Price", "ImportPrice" },
                { "Số lượng", "Quantity" },
                { "Quantity", "Quantity" }
            };

            return mapping.ContainsKey(labelText) ? mapping[labelText] : null;
        }

        private void txtMaXe_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaXe.Text))
            {
                errorProvider1.SetError(txtMaXe, langMgr.GetString("VehicleIDRequired") ?? "Mã xe không được để trống!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtMaXe, "");
            }
        }

        private void txtGiaMua_Validating(object sender, CancelEventArgs e)
        {
            if (!decimal.TryParse(txtGiaMua.Text, out decimal giaMua) || giaMua < 0)
            {
                errorProvider1.SetError(txtGiaMua, langMgr.GetString("PricePositiveRequired") ?? "Giá mua phải là số dương!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtGiaMua, "");
            }
        }

        private void txtGiaNhap_Validating(object sender, CancelEventArgs e)
        {
            if (!decimal.TryParse(txtGiaNhap.Text, out decimal giaNhap) || giaNhap < 0)
            {
                errorProvider1.SetError(txtGiaNhap, langMgr.GetString("ImportPricePositiveRequired") ?? "Giá nhập phải là số dương!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtGiaNhap, "");
            }
        }

        private void FormSuaXe_Load(object sender, EventArgs e)
        {
            LoadHangXe();
            LoadMauSac();
            LoadNhaCungCap();
            LoadLoaiXe();
            LoadNamSanXuat();
            LoadTrangThai();
            LoadMucDichSuDung();

            XeMayDTO xe = xeMayBLL.GetXeMayById(idXe);
            currentXe = xe;
            if (xe == null)
            {
                MessageBox.Show(
                    langMgr.GetString("VehicleNotFound") ?? "Không tìm thấy thông tin xe!",
                    langMgr.GetString("Error") ?? "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                this.Close();
                return;
            }

            txtMaXe.Text = xe.ID_Xe;
            txtMaXe.ReadOnly = true;
            txtBienSo.Text = xe.BienSo;
            cbbHangXe.SelectedValue = xe.MaHang;
            LoadDongXe(xe.MaHang);
            cbbDongXe.SelectedValue = xe.MaDong;
            cbbMauSac.SelectedValue = xe.MaMau;
            cbbNhaCungCap.SelectedValue = xe.MaNCC;
            cbbNamSanXuat.SelectedItem = xe.NamSX.ToString();
            cbbTrangThai.SelectedItem = xe.TrangThai;
            cbbMucDichSuDung.SelectedItem = xe.MucDichSuDung;
            dtpNgayMua.Value = xe.NgayMua ?? DateTime.Now;
            txtGiaMua.Text = xe.GiaMua.HasValue ? xe.GiaMua.Value.ToString() : "";
            txtGiaNhap.Text = xe.GiaNhap.HasValue ? xe.GiaNhap.Value.ToString() : "";

            int soLuong = xe.SoLuong ?? 0;
            nudSoLuong.Minimum = 0;
            nudSoLuong.Value = soLuong;

            dtpNgayDangKy.Value = xe.NgayDangKy ?? DateTime.Now;
            dtpNgayHetHanDangKy.Value = xe.HetHanDangKy ?? DateTime.Now;
            dtpNgayHetHanBaoHiem.Value = xe.HetHanBaoHiem ?? DateTime.Now;
            nudKHDaChay.Value = xe.KmDaChay ?? 0;
            txtThongTinXang.Text = xe.ThongTinXang ?? "";

            if (xe.AnhXe != null && xe.AnhXe.Length > 0)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(xe.AnhXe))
                    {
                        picAnhXe.Image = Image.FromStream(ms);
                        picAnhXe.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    anhXeBytes = xe.AnhXe;
                }
                catch { }
            }

            cbbLoaiXe.SelectedValue = xe.ID_Loai;

            ApplyLanguage();
        }

        private void LoadHangXe()
        {
            cbbHangXe.DataSource = hangXeBLL.GetAllHangXe();
            cbbHangXe.DisplayMember = "TenHang";
            cbbHangXe.ValueMember = "MaHang";
        }

        private void LoadDongXe(string maHang)
        {
            cbbDongXe.DataSource = dongXeBLL.GetDongXeByHang(maHang);
            cbbDongXe.DisplayMember = "TenDong";
            cbbDongXe.ValueMember = "MaDong";
        }

        private void LoadMauSac()
        {
            cbbMauSac.DataSource = mauSacBLL.GetAllMauSac();
            cbbMauSac.DisplayMember = "TenMau";
            cbbMauSac.ValueMember = "MaMau";
        }

        private void LoadNhaCungCap()
        {
            cbbNhaCungCap.DataSource = nhaCungCapBLL.GetAllNhaCungCap();
            cbbNhaCungCap.DisplayMember = "TenNCC";
            cbbNhaCungCap.ValueMember = "MaNCC";
        }

        private void LoadLoaiXe()
        {
            cbbLoaiXe.DataSource = loaiXeBLL.GetAllLoaiXe();
            cbbLoaiXe.DisplayMember = "ID_Loai";
            cbbLoaiXe.ValueMember = "ID_Loai";
        }

        private void LoadNamSanXuat()
        {
            int yearNow = DateTime.Now.Year;
            cbbNamSanXuat.DataSource = Enumerable.Range(2000, yearNow - 1999).ToList();
        }

        private void LoadTrangThai()
        {
            cbbTrangThai.Items.Clear();
        }

        private void LoadMucDichSuDung()
        {
            cbbMucDichSuDung.Items.Clear();
        }

        private void txtMaXe_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbHangXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbHangXe.SelectedValue != null)
                LoadDongXe(cbbHangXe.SelectedValue.ToString());
        }

        private void cbbMauSac_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtpNgayMua_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpNgayDangKy_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnChonFileAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = langMgr.GetString("SelectVehicleImage") ?? "Chọn ảnh xe";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    picAnhXe.Image = Image.FromFile(ofd.FileName);
                    picAnhXe.SizeMode = PictureBoxSizeMode.Zoom;

                    using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        anhXeBytes = new byte[fs.Length];
                        fs.Read(anhXeBytes, 0, (int)fs.Length);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        langMgr.GetString("ImageLoadError") + ": " + ex.Message,
                        langMgr.GetString("Error") ?? "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void picAnhXe_Click(object sender, EventArgs e)
        {
            btnChonFileAnh_Click(sender, e);
        }

        private void cbbLoaiXe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbNamSanXuat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbDongXe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtBienSo_TextChanged(object sender, EventArgs e)
        {

        }

        private void nudKHDaChay_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtThongTinXang_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtpNgayHetHanDangKy_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpNgayHetHanBaoHiem_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSuaXe_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaXe.Text)
                    || cbbNhaCungCap.SelectedValue == null
                    || string.IsNullOrWhiteSpace(txtGiaMua.Text)
                    || string.IsNullOrWhiteSpace(txtGiaNhap.Text)
                    || cbbHangXe.SelectedValue == null
                    || cbbDongXe.SelectedValue == null
                    || cbbMauSac.SelectedValue == null
                    || cbbNamSanXuat.SelectedItem == null
                    || cbbLoaiXe.SelectedValue == null)
                {
                    MessageBox.Show(
                        langMgr.GetString("PleaseEnterFullInfo") ?? "Vui lòng nhập đầy đủ thông tin!",
                        langMgr.GetString("Notification") ?? "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (!this.ValidateChildren())
                {
                    MessageBox.Show(
                        langMgr.GetString("PleaseEnterValidInfo") ?? "Vui lòng nhập đúng và hợp lệ!",
                        langMgr.GetString("Notification") ?? "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                var xeDTO = new XeMayDTO
                {
                    ID_Xe = txtMaXe.Text.Trim(),
                    BienSo = txtBienSo.Text.Trim(),
                    MaNCC = cbbNhaCungCap.SelectedValue?.ToString(),
                    NgayMua = dtpNgayMua.Value,
                    GiaMua = decimal.TryParse(txtGiaMua.Text, out var gia) ? gia : (decimal?)null,
                    GiaNhap = decimal.TryParse(txtGiaNhap.Text, out var giaNhap) ? giaNhap : (decimal?)null,
                    SoLuong = (int?)nudSoLuong.Value,
                    SoLuongBanRa = currentXe?.SoLuongBanRa ?? 0,
                    NgayDangKy = dtpNgayDangKy.Value,
                    HetHanDangKy = dtpNgayHetHanDangKy.Value,
                    HetHanBaoHiem = dtpNgayHetHanBaoHiem.Value,
                    KmDaChay = (int)nudKHDaChay.Value,
                    ThongTinXang = txtThongTinXang.Text.Trim(),
                    AnhXe = anhXeBytes,
                    TrangThai = cbbTrangThai.SelectedItem?.ToString(),
                    MucDichSuDung = cbbMucDichSuDung.SelectedItem?.ToString(),
                    MaHang = cbbHangXe.SelectedValue?.ToString(),
                    MaDong = cbbDongXe.SelectedValue?.ToString(),
                    MaMau = cbbMauSac.SelectedValue?.ToString(),
                    NamSX = cbbNamSanXuat.SelectedItem != null ? (int?)Convert.ToInt32(cbbNamSanXuat.SelectedItem) : null
                };

                if (!string.IsNullOrEmpty(xeDTO.MaHang) && !string.IsNullOrEmpty(xeDTO.MaDong) &&
                    !string.IsNullOrEmpty(xeDTO.MaMau) && xeDTO.NamSX.HasValue)
                {
                    xeDTO.ID_Loai = loaiXeBLL.GetOrCreateIDLoai(xeDTO.MaHang, xeDTO.MaDong, xeDTO.MaMau, xeDTO.NamSX.Value);
                }

                bool success = xeMayBLL.UpdateXeMay(xeDTO);
                if (success)
                {
                    MessageBox.Show(
                        langMgr.GetString("EditVehicleSuccess") ?? "Sửa xe thành công!",
                        langMgr.GetString("Notification") ?? "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        langMgr.GetString("EditVehicleFailed") ?? "Sửa xe thất bại!\nVui lòng kiểm tra lại thông tin.",
                        langMgr.GetString("Error") ?? "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    langMgr.GetString("Error") ?? "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void txtGiaMua_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtGiaMua.Text, out decimal giaMua))
            {
                if (string.IsNullOrWhiteSpace(txtGiaNhap.Text) ||
                    (currentXe != null && currentXe.GiaMua.HasValue && currentXe.GiaNhap.HasValue &&
                     Math.Abs(currentXe.GiaNhap.Value - currentXe.GiaMua.Value * 0.85m) < 0.01m))
                {
                    txtGiaNhap.Text = (giaMua * 0.85m).ToString("0.##");
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}