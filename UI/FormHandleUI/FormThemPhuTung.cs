using BLL;
using DTO; // Nếu bạn dùng DTO cho datamodel
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

        public FormThemPhuTung()
        {
            InitializeComponent();
            // Gắn sự kiện cho các control
            txtGiaMua.Validating += txtGiaMua_Validating;
            txtGiaBan.Validating += txtGiaBan_Validating;
            txtSoLuongTonKho.Validating += txtSoLuongTonKho_Validating;
            txtTenPhuTung.Validating += txtTenPhuTung_Validating;
            cbbHangXeTuongThich.SelectedIndexChanged += cbbHangXeTuongThich_SelectedIndexChanged;
        }

        // Load data binding và giá trị mặc định
        private void FormThemPhuTung_Load(object sender, EventArgs e)
        {
             // Binding hãng xe
            cbbHangXeTuongThich.DataSource = hangXeBLL.GetAllHangXe();
            cbbHangXeTuongThich.DisplayMember = "TenHang";
            cbbHangXeTuongThich.ValueMember = "MaHang";

            // Binding dòng xe đầu tiên nếu có hãng
            if (cbbHangXeTuongThich.SelectedValue != null)
                LoadDongXeTuongThich(cbbHangXeTuongThich.SelectedValue.ToString());

            // Đơn vị tính
            cbbDonViTinh.Items.Clear();
            cbbDonViTinh.Items.AddRange(new object[] { "Cái", "Bộ", "Chiếc", "Lít", "Khác..." });
            cbbDonViTinh.SelectedIndex = 0;

            // Sinh mã phụ tùng tự động
            txtMaPhuTung.Text = SinhMaPhuTungTuDong();
            txtMaPhuTung.ReadOnly = true;
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

        // Validation cho các TextBox
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

        // Thay đổi hãng xe để load lại dòng xe
        private void cbbHangXeTuongThich_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbHangXeTuongThich.SelectedValue != null)
                LoadDongXeTuongThich(cbbHangXeTuongThich.SelectedValue.ToString());
        }

        // Xử lý nút Thêm
        private void btnThemPhuTung_Click(object sender, EventArgs e)
        {
            // Ràng buộc kiểm tra dữ liệu
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
            // Kiểm tra bằng hàm ValidateChildren để xác nhận các control hợp lệ
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Vui lòng nhập đúng và hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            bool success = phuTungBLL.InsertPhuTungKho(
                maPT, tenPT, maHang, maDong,
                giaMua, giaBan, donVi, ghiChu, soLuongTon);

            if (success)
            {
                MessageBox.Show("Thêm phụ tùng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm phụ tùng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Các event TextChanged/SelectedIndexChanged cho GUI (tuỳ ý dùng)
        private void txtMaPhuTung_TextChanged(object sender, EventArgs e) { }
        private void txtTenPhuTung_TextChanged(object sender, EventArgs e) { }
        private void cbbDongXeTuongThich_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtGiaMua_TextChanged(object sender, EventArgs e) { }
        private void txtGiaBan_TextChanged(object sender, EventArgs e) { }
        private void cbbDonViTinh_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtGhiChu_TextChanged(object sender, EventArgs e) { }
        private void txtSoLuongTonKho_TextChanged(object sender, EventArgs e) { }
    }
}
