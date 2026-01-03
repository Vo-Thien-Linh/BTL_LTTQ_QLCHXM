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

    public partial class FormThemXe : Form
    {
        private HangXeBLL HangXeBLL = new HangXeBLL();
        private DongXeBLL DongXeBLL = new DongXeBLL();
        private MauSacBLL MauSacBLL = new MauSacBLL();
        private NhaCungCapBLL NhaCungCapBLL = new NhaCungCapBLL();
        private LoaiXeBLL LoaiXeBLL = new LoaiXeBLL();
        private XeMayBLL xeMayBLL = new XeMayBLL();
        private byte[] anhXeBytes = null;
        private ErrorProvider errorProvider1 = new ErrorProvider();

        private LanguageManagerBLL langMgr = LanguageManagerBLL.Instance;

        public FormThemXe()
        {
            InitializeComponent();
            txtMaXe.Validating += txtMaXe_Validating;
            txtGiaMua.Validating += txtGiaMua_Validating;
            txtGiaNhap.Validating += txtGiaNhap_Validating;

            langMgr.LanguageChanged += (s, e) => ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            this.Text = langMgr.GetString("AddVehicle") ?? "THÊM XE";

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
                else if (cbbTrangThai.Items.Count > 0)
                    cbbTrangThai.SelectedIndex = 0;
            }

            if (cbbMucDichSuDung != null)
            {
                int selectedIndex = cbbMucDichSuDung.SelectedIndex;
                cbbMucDichSuDung.Items.Clear();
                cbbMucDichSuDung.Items.Add(langMgr.GetString("ForRent") ?? "Cho thuê");
                cbbMucDichSuDung.Items.Add(langMgr.GetString("ForSale") ?? "Bán");
                if (selectedIndex >= 0 && selectedIndex < cbbMucDichSuDung.Items.Count)
                    cbbMucDichSuDung.SelectedIndex = selectedIndex;
                else if (cbbMucDichSuDung.Items.Count > 0)
                    cbbMucDichSuDung.SelectedIndex = 0;
            }
        }

        private void UpdateAllLabels(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Label lbl)
                {
                    string originalText = lbl.Text.Trim().TrimEnd(':');

                    if (originalText == "THÊM XE" || originalText == "ADD VEHICLE")
                    {
                        lbl.Text = (langMgr.GetString("AddVehicle") ?? "THÊM XE").ToUpper();
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
                    else if (btnText == "Add Vehicle" || btnText == "Thêm xe")
                        btn.Text = langMgr.GetString("AddVehicle") ?? "Thêm xe";
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

        private void LoadHangXe()
        {
            cbbHangXe.DataSource = HangXeBLL.GetAllHangXe();
            cbbHangXe.DisplayMember = "TenHang";
            cbbHangXe.ValueMember = "MaHang";
        }

        private void LoadDongXe(string maHang)
        {
            cbbDongXe.DataSource = DongXeBLL.GetDongXeByHang(maHang);
            cbbDongXe.DisplayMember = "TenDong";
            cbbDongXe.ValueMember = "MaDong";
        }

        private void LoadMauSac()
        {
            cbbMauSac.DataSource = MauSacBLL.GetAllMauSac();
            cbbMauSac.DisplayMember = "TenMau";
            cbbMauSac.ValueMember = "MaMau";
        }

        private void LoadNhaCungCap()
        {
            cbbNhaCungCap.DataSource = NhaCungCapBLL.GetAllNhaCungCap();
            cbbNhaCungCap.DisplayMember = "TenNCC";
            cbbNhaCungCap.ValueMember = "MaNCC";
        }

        private void LoadLoaiXe()
        {
            cbbLoaiXe.DataSource = LoaiXeBLL.GetAllLoaiXe();
            cbbLoaiXe.DisplayMember = "ID_Loai";
            cbbLoaiXe.ValueMember = "ID_Loai";
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

        private void txtBienSo_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbDongXe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbNamSanXuat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbLoaiXe_SelectedIndexChanged(object sender, EventArgs e)
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

        private void btnThemXe_Click(object sender, EventArgs e)
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

                XeMayDTO dto = new XeMayDTO
                {
                    ID_Xe = txtMaXe.Text.Trim(),
                    BienSo = string.IsNullOrWhiteSpace(txtBienSo.Text) ? null : txtBienSo.Text.Trim(),
                    MaNCC = cbbNhaCungCap.SelectedValue?.ToString(),
                    NgayMua = dtpNgayMua.Value,
                    GiaMua = string.IsNullOrWhiteSpace(txtGiaMua.Text) ? (decimal?)null : decimal.Parse(txtGiaMua.Text),
                    GiaNhap = string.IsNullOrWhiteSpace(txtGiaNhap.Text) ? (decimal?)null : decimal.Parse(txtGiaNhap.Text),
                    SoLuong = (int?)nudSoLuong.Value,
                    SoLuongBanRa = 0,
                    NgayDangKy = dtpNgayDangKy.Value,
                    HetHanDangKy = dtpNgayHetHanDangKy.Value,
                    HetHanBaoHiem = dtpNgayHetHanBaoHiem.Value,
                    KmDaChay = Convert.ToInt32(nudKHDaChay.Value),
                    ThongTinXang = txtThongTinXang.Text.Trim(),
                    AnhXe = anhXeBytes,
                    TrangThai = cbbTrangThai.SelectedItem?.ToString(),
                    MucDichSuDung = cbbMucDichSuDung.SelectedItem?.ToString(),
                    MaHang = cbbHangXe.SelectedValue?.ToString(),
                    MaDong = cbbDongXe.SelectedValue?.ToString(),
                    MaMau = cbbMauSac.SelectedValue?.ToString(),
                    NamSX = cbbNamSanXuat.SelectedItem != null ? (int?)Convert.ToInt32(cbbNamSanXuat.SelectedItem) : null
                };

                if (!string.IsNullOrEmpty(dto.MaHang) && !string.IsNullOrEmpty(dto.MaDong) &&
                    !string.IsNullOrEmpty(dto.MaMau) && dto.NamSX.HasValue)
                {
                    dto.ID_Loai = LoaiXeBLL.GetOrCreateIDLoai(dto.MaHang, dto.MaDong, dto.MaMau, dto.NamSX.Value);
                }

                bool success = xeMayBLL.InsertXeMay(dto);

                if (success)
                {
                    MessageBox.Show(
                        langMgr.GetString("AddVehicleSuccess") ?? "Thêm xe thành công!",
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
                        langMgr.GetString("AddVehicleFailed") ?? "Thêm xe thất bại!\nVui lòng kiểm tra lại thông tin.",
                        langMgr.GetString("Error") ?? "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                string detailError = $"Lỗi: {ex.Message}\n\nChi tiết:\n{ex.ToString()}";
                System.Diagnostics.Debug.WriteLine(detailError);
                MessageBox.Show(
                    ex.Message,
                    langMgr.GetString("Error") ?? "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void FormThemXe_Load(object sender, EventArgs e)
        {
            LoadHangXe();
            if (cbbHangXe.SelectedValue != null)
                LoadDongXe(cbbHangXe.SelectedValue.ToString());
            LoadMauSac();
            LoadNhaCungCap();
            LoadLoaiXe();

            txtMaXe.Text = xeMayBLL.GenerateNewMaXe();

            cbbNamSanXuat.DataSource = Enumerable.Range(2000, DateTime.Now.Year - 1999).ToList();

            ApplyLanguage();
        }

        private void txtGiaMua_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtGiaMua.Text, out decimal giaMua))
            {
                txtGiaNhap.Text = (giaMua * 0.85m).ToString("0.##");
            }
            else
            {
                txtGiaNhap.Text = "";
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}