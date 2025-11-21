using BLL;
using DTO; // Nếu dùng đối tượng PhuTungDTO hoặc tương tự
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
    public partial class FormSuaPhuTung : Form
    {
        private string maPhuTung;
        private PhuTungBLL phuTungBLL = new PhuTungBLL();
        private HangXeBLL hangXeBLL = new HangXeBLL();
        private DongXeBLL dongXeBLL = new DongXeBLL();
        private ErrorProvider errorProvider1 = new ErrorProvider();

        public FormSuaPhuTung(string maPhuTung)
        {
            InitializeComponent();
            this.maPhuTung = maPhuTung;

            txtTenPhuTung.Validating += txtTenPhuTung_Validating;
            txtGiaMua.Validating += txtGiaMua_Validating;
            txtGiaBan.Validating += txtGiaBan_Validating;
            txtSoLuongTonKho.Validating += txtSoLuongTonKho_Validating;
            cbbHangXeTuongThich.SelectedIndexChanged += cbbHangXeTuongThich_SelectedIndexChanged;
        }

        private void FormSuaPhuTung_Load(object sender, EventArgs e)
        {
            // Đổ hãng xe
            cbbHangXeTuongThich.DataSource = hangXeBLL.GetAllHangXe();
            cbbHangXeTuongThich.DisplayMember = "TenHang";
            cbbHangXeTuongThich.ValueMember = "MaHang";

            // Đơn vị tính
            cbbDonViTinh.Items.Clear();
            cbbDonViTinh.Items.AddRange(new string[] { "Cái", "Bộ", "Chiếc", "Lít", "Khác..." });
            cbbDonViTinh.SelectedIndex = 0;

            // Lấy thông tin phụ tùng từ BLL
            var pt = phuTungBLL.GetPhuTungById(maPhuTung);
            if (pt == null)
            {
                MessageBox.Show("Không tìm thấy phụ tùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txtMaPhuTung.Text = pt.MaPhuTung;
            txtMaPhuTung.ReadOnly = true;
            txtTenPhuTung.Text = pt.TenPhuTung;

            // Đổ dòng xe ngay sau khi chọn hãng (do có thể bị trễ giá trị SelectedValue)
            cbbHangXeTuongThich.SelectedValue = pt.MaHangXe;
            LoadDongXeTuongThich(pt.MaHangXe);
            cbbDongXeTuongThich.SelectedValue = pt.MaDongXe;

            txtGiaMua.Text = pt.GiaMua.ToString();
            txtGiaBan.Text = pt.GiaBan.ToString();
            cbbDonViTinh.SelectedItem = pt.DonViTinh;
            txtSoLuongTonKho.Text = pt.SoLuongTon.ToString();
            txtGhiChu.Text = pt.GhiChu;
        }

        private void LoadDongXeTuongThich(string maHang)
        {
            cbbDongXeTuongThich.DataSource = dongXeBLL.GetDongXeByHang(maHang);
            cbbDongXeTuongThich.DisplayMember = "TenDong";
            cbbDongXeTuongThich.ValueMember = "MaDong";
        }

        // Khi thay đổi hãng xe, load lại dòng xe cho phù hợp
        private void cbbHangXeTuongThich_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbHangXeTuongThich.SelectedValue != null)
                LoadDongXeTuongThich(cbbHangXeTuongThich.SelectedValue.ToString());
        }

        // Validation các trường nhập
        private void txtTenPhuTung_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenPhuTung.Text))
            {
                errorProvider1.SetError(txtTenPhuTung, "Tên phụ tùng không được để trống!");
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
                errorProvider1.SetError(txtGiaMua, "Giá mua phải là số dương!");
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
                errorProvider1.SetError(txtGiaBan, "Giá bán phải là số dương!");
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
                errorProvider1.SetError(txtSoLuongTonKho, "Số lượng tồn kho phải là số nguyên dương!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtSoLuongTonKho, "");
            }
        }

        // Button Sửa phụ tùng
        private void btnSuaPhuTung_Click(object sender, EventArgs e)
        {
            // Kiểm tra nhập đủ thông tin cơ bản
            if (string.IsNullOrWhiteSpace(txtTenPhuTung.Text)
                || cbbHangXeTuongThich.SelectedValue == null
                || cbbDongXeTuongThich.SelectedValue == null
                || cbbDonViTinh.SelectedItem == null
                || string.IsNullOrWhiteSpace(txtGiaMua.Text)
                || string.IsNullOrWhiteSpace(txtGiaBan.Text)
                || string.IsNullOrWhiteSpace(txtSoLuongTonKho.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Kiểm tra các trường có hợp lệ hay không
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Vui lòng nhập đúng và hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenPT = txtTenPhuTung.Text.Trim();
            string maHang = cbbHangXeTuongThich.SelectedValue.ToString();
            string maDong = cbbDongXeTuongThich.SelectedValue.ToString();
            string donVi = cbbDonViTinh.SelectedItem.ToString();
            string ghiChu = txtGhiChu.Text.Trim();
            decimal giaMua = decimal.Parse(txtGiaMua.Text);
            decimal giaBan = decimal.Parse(txtGiaBan.Text);
            int soLuongTon = int.Parse(txtSoLuongTonKho.Text);

            bool success = phuTungBLL.UpdatePhuTungKho(
                maPhuTung,
                tenPT, maHang, maDong, giaMua, giaBan, donVi, ghiChu, soLuongTon);
            if (success)
            {
                MessageBox.Show("Sửa phụ tùng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Sửa phụ tùng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Các event phụ trợ (bạn có thể đặt thêm logic nếu muốn)
        private void txtMaPhuTung_TextChanged(object sender, EventArgs e) { }
        private void txtTenPhuTung_TextChanged(object sender, EventArgs e) { }
        private void cbbDongXeTuongThich_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtGiaMua_TextChanged(object sender, EventArgs e) { }
        private void txtGiaBan_TextChanged(object sender, EventArgs e) { }
        private void cbbDonViTinh_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtSoLuongTonKho_TextChanged(object sender, EventArgs e) { }
        private void txtGhiChu_TextChanged(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}
