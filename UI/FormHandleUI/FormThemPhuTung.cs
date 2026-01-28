using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.FormHandleUI
{
    public partial class FormThemPhuTung : Form
    {
        private HangXeBLL hangXeBLL = new HangXeBLL();
        private DongXeBLL dongXeBLL = new DongXeBLL();
        private PhuTungBLL phuTungBLL = new PhuTungBLL();
        private ErrorProvider errorProvider1 = new ErrorProvider();

        private LanguageManagerBLL langMgr = LanguageManagerBLL.Instance;

        public FormThemPhuTung()
        {
            InitializeComponent();
            txtGiaMua.Validating += txtGiaMua_Validating;
            txtGiaBan.Validating += txtGiaBan_Validating;
            txtSoLuongTonKho.Validating += txtSoLuongTonKho_Validating;
            txtTenPhuTung.Validating += txtTenPhuTung_Validating;
            txtGiaMua.KeyPress += txtGiaMua_KeyPress;
            txtGiaBan.KeyPress += txtGiaBan_KeyPress;
            txtSoLuongTonKho.KeyPress += txtSoLuongTonKho_KeyPress;
            cbbHangXeTuongThich.SelectedIndexChanged += cbbHangXeTuongThich_SelectedIndexChanged;

            langMgr.LanguageChanged += (s, e) => ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            this.Text = langMgr.GetString("AddPart") ?? "THÊM PHỤ TÙNG";

            UpdateAllLabels(this);

            if (cbbDonViTinh != null)
            {
                int selectedIndex = cbbDonViTinh.SelectedIndex;
                cbbDonViTinh.Items.Clear();
                cbbDonViTinh.Items.Add(langMgr.GetString("Unit_Piece") ?? "Cái");
                cbbDonViTinh.Items.Add(langMgr.GetString("Unit_Set") ?? "Bộ");
                cbbDonViTinh.Items.Add(langMgr.GetString("Unit_Item") ?? "Chiếc");
                cbbDonViTinh.Items.Add(langMgr.GetString("Unit_Liter") ?? "Lít");
                cbbDonViTinh.Items.Add(langMgr.GetString("Unit_Other") ?? "Khác...");

                if (selectedIndex >= 0 && selectedIndex < cbbDonViTinh.Items.Count)
                    cbbDonViTinh.SelectedIndex = selectedIndex;
                else if (cbbDonViTinh.Items.Count > 0)
                    cbbDonViTinh.SelectedIndex = 0;
            }
        }

        private void UpdateAllLabels(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Label lbl)
                {
                    string originalText = lbl.Text.Trim().TrimEnd(':');

                    if (originalText == "THÊM PHỤ TÙNG" || originalText == "ADD PART")
                    {
                        lbl.Text = (langMgr.GetString("AddPart") ?? "THÊM PHỤ TÙNG").ToUpper();
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
                    if (btnText == "Add Part" || btnText == "Thêm phụ tùng")
                        btn.Text = langMgr.GetString("AddPart") ?? "Thêm phụ tùng";
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
                { "Mã phụ tùng", "PartID" },
                { "Part ID", "PartID" },
                { "Tên phụ tùng", "PartName" },
                { "Part Name", "PartName" },
                { "Hãng xe tương thích", "CompatibleBrand" },
                { "Compatible Brand", "CompatibleBrand" },
                { "Dòng xe tương thích", "CompatibleModel" },
                { "Compatible Model", "CompatibleModel" },
                { "Giá mua", "PurchasePrice" },
                { "Purchase Price", "PurchasePrice" },
                { "Giá bán", "SalePrice" },
                { "Sale Price", "SalePrice" },
                { "Đơn vị tính", "Unit" },
                { "Unit", "Unit" },
                { "Ghi chú", "Note" },
                { "Note", "Note" },
                { "Số lượng tồn kho", "StockQuantity" },
                { "Stock Quantity", "StockQuantity" }
            };

            return mapping.ContainsKey(labelText) ? mapping[labelText] : null;
        }

        private void FormThemPhuTung_Load(object sender, EventArgs e)
        {
            cbbHangXeTuongThich.DataSource = hangXeBLL.GetAllHangXe();
            cbbHangXeTuongThich.DisplayMember = "TenHang";
            cbbHangXeTuongThich.ValueMember = "MaHang";

            if (cbbHangXeTuongThich.SelectedValue != null)
                LoadDongXeTuongThich(cbbHangXeTuongThich.SelectedValue.ToString());

            cbbDonViTinh.Items.Clear();

            txtMaPhuTung.Text = SinhMaPhuTungTuDong();
            txtMaPhuTung.ReadOnly = true;

            ApplyLanguage();
        }

        private void LoadDongXeTuongThich(string maHang)
        {
            cbbDongXeTuongThich.DataSource = dongXeBLL.GetDongXeByHang(maHang);
            cbbDongXeTuongThich.DisplayMember = "TenDong";
            cbbDongXeTuongThich.ValueMember = "MaDong";
        }

        private string SinhMaPhuTungTuDong()
        {
            string last = phuTungBLL.GetMaxMaPhuTung();
            if (!string.IsNullOrEmpty(last) && last.StartsWith("PT"))
            {
                int num = int.Parse(last.Substring(2));
                num++;
                return "PT" + num.ToString("D7");
            }
            return "PT0000001";
        }

        private void txtTenPhuTung_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenPhuTung.Text))
            {
                errorProvider1.SetError(txtTenPhuTung, langMgr.GetString("PartNameRequired") ?? "Tên phụ tùng không được để trống!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtTenPhuTung, "");
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

        private void txtGiaBan_Validating(object sender, CancelEventArgs e)
        {
            if (!decimal.TryParse(txtGiaBan.Text, out decimal giaBan) || giaBan < 0)
            {
                errorProvider1.SetError(txtGiaBan, langMgr.GetString("SalePricePositiveRequired") ?? "Giá bán phải là số dương!");
                e.Cancel = true;
            }
            else if (decimal.TryParse(txtGiaMua.Text, out decimal giaMua) && giaBan < giaMua)
            {
                errorProvider1.SetError(txtGiaBan, langMgr.GetString("SalePriceMustBeGreaterOrEqualPurchase") ?? "Giá bán phải lớn hơn hoặc bằng giá mua!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtGiaBan, "");
            }
        }

        private void txtSoLuongTonKho_Validating(object sender, CancelEventArgs e)
        {
            if (!int.TryParse(txtSoLuongTonKho.Text, out int soLuongTon) || soLuongTon < 0)
            {
                errorProvider1.SetError(txtSoLuongTonKho, langMgr.GetString("StockQuantityPositiveRequired") ?? "Số lượng tồn kho phải là số nguyên dương!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtSoLuongTonKho, "");
            }
        }

        private void cbbHangXeTuongThich_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbHangXeTuongThich.SelectedValue != null)
                LoadDongXeTuongThich(cbbHangXeTuongThich.SelectedValue.ToString());
        }

        private void btnThemPhuTung_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenPhuTung.Text)
                || cbbHangXeTuongThich.SelectedValue == null
                || cbbDongXeTuongThich.SelectedValue == null
                || cbbDonViTinh.SelectedItem == null
                || string.IsNullOrWhiteSpace(txtGiaMua.Text)
                || string.IsNullOrWhiteSpace(txtGiaBan.Text)
                || string.IsNullOrWhiteSpace(txtSoLuongTonKho.Text))
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

            string maPT = txtMaPhuTung.Text.Trim();
            string tenPT = txtTenPhuTung.Text.Trim();
            string maHang = cbbHangXeTuongThich.SelectedValue.ToString();
            string maDong = cbbDongXeTuongThich.SelectedValue.ToString();
            string donVi = cbbDonViTinh.SelectedItem.ToString();
            string ghiChu = txtGhiChu.Text.Trim();

            decimal giaMua = decimal.Parse(txtGiaMua.Text);
            decimal giaBan = decimal.Parse(txtGiaBan.Text);
            int soLuongTon = int.Parse(txtSoLuongTonKho.Text);

            if (giaBan < giaMua)
            {
                MessageBox.Show(
                    langMgr.GetString("SalePriceMustBeGreaterOrEqualPurchase") ?? "Giá bán phải lớn hơn hoặc bằng giá mua!",
                    langMgr.GetString("Notification") ?? "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtGiaBan.Focus();
                return;
            }

            bool success = phuTungBLL.InsertPhuTungKho(
                maPT, tenPT, maHang, maDong,
                giaMua, giaBan, donVi, ghiChu, soLuongTon);

            if (success)
            {
                MessageBox.Show(
                    langMgr.GetString("AddPartSuccess") ?? "Thêm phụ tùng thành công.",
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
                    langMgr.GetString("AddPartFailed") ?? "Thêm phụ tùng thất bại!",
                    langMgr.GetString("Error") ?? "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void txtMaPhuTung_TextChanged(object sender, EventArgs e) { }
        private void txtTenPhuTung_TextChanged(object sender, EventArgs e) { }
        private void cbbDongXeTuongThich_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtGiaMua_TextChanged(object sender, EventArgs e) { }
        private void txtGiaBan_TextChanged(object sender, EventArgs e) { }
        private void cbbDonViTinh_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtGhiChu_TextChanged(object sender, EventArgs e) { }
        private void txtSoLuongTonKho_TextChanged(object sender, EventArgs e) { }

        private void txtGiaMua_KeyPress(object sender, KeyPressEventArgs e)
        {
            HandleDecimalKeyPress(sender as TextBox, e);
        }

        private void txtGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            HandleDecimalKeyPress(sender as TextBox, e);
        }

        private void txtSoLuongTonKho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void HandleDecimalKeyPress(TextBox textBox, KeyPressEventArgs e)
        {
            if (textBox == null)
                return;

            bool isControl = char.IsControl(e.KeyChar);
            bool isDigit = char.IsDigit(e.KeyChar);
            bool isDecimalSeparator = e.KeyChar == '.' || e.KeyChar == ',';

            if (!isControl && !isDigit)
            {
                if (!isDecimalSeparator)
                {
                    e.Handled = true;
                }
                else if (textBox.Text.Contains(".") || textBox.Text.Contains(","))
                {
                    e.Handled = true;
                }
            }
        }
    }
}