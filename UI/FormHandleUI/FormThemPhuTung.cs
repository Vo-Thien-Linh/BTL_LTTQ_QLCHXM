using BLL;
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
        public FormThemPhuTung()
        {
            InitializeComponent();
        }

        private void txtMaPhuTung_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenPhuTung_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbHangXeTuongThich_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbHangXeTuongThich.SelectedValue != null)
                LoadDongXeTuongThich(cbbHangXeTuongThich.SelectedValue.ToString());
        }

        private void cbbDongXeTuongThich_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtGiaMua_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtGiaBan_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbDonViTinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbSoLuongTonKho_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtGhiChu_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThemPhuTung_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenPhuTung.Text) ||
                cbbHangXeTuongThich.SelectedValue == null ||
                cbbDongXeTuongThich.SelectedValue == null ||
                cbbDonViTinh.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtGiaMua.Text) ||
                string.IsNullOrWhiteSpace(txtGiaBan.Text) ||
                string.IsNullOrWhiteSpace(txtSoLuongTonKho.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtGiaMua.Text, out decimal giaMua) || giaMua < 0)
            {
                MessageBox.Show("Giá mua phải là số dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!decimal.TryParse(txtGiaBan.Text, out decimal giaBan) || giaBan < 0)
            {
                MessageBox.Show("Giá bán phải là số dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(txtSoLuongTonKho.Text, out int soLuongTon) || soLuongTon < 0)
            {
                MessageBox.Show("Số lượng tồn kho phải là số nguyên dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string maPT = txtMaPhuTung.Text.Trim();
            string tenPT = txtTenPhuTung.Text.Trim();
            string maHang = cbbHangXeTuongThich.SelectedValue.ToString();
            string maDong = cbbDongXeTuongThich.SelectedValue.ToString();
            string donVi = cbbDonViTinh.SelectedItem.ToString();
            string ghiChu = txtGhiChu.Text.Trim();

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

        private void FormThemPhuTung_Load(object sender, EventArgs e)
        {
            // Đổ hãng xe
            cbbHangXeTuongThich.DataSource = hangXeBLL.GetAllHangXe();
            cbbHangXeTuongThich.DisplayMember = "TenHang";
            cbbHangXeTuongThich.ValueMember = "MaHang";

            // Đổ dòng xe theo hãng đầu tiên (hoặc để trống nếu chưa có hãng)
            if (cbbHangXeTuongThich.SelectedValue != null)
                LoadDongXeTuongThich(cbbHangXeTuongThich.SelectedValue.ToString());

            // Đổ đơn vị tính
            cbbDonViTinh.Items.Clear();
            cbbDonViTinh.Items.Add("Cái");
            cbbDonViTinh.Items.Add("Bộ");
            cbbDonViTinh.Items.Add("Chiếc");
            cbbDonViTinh.Items.Add("Lít");
            cbbDonViTinh.Items.Add("Khác...");
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
            // Ví dụ (giả định có hàm GetMaxMaPhuTung ở PhuTungBLL)
            string last = phuTungBLL.GetMaxMaPhuTung();
            if (!string.IsNullOrEmpty(last) && last.StartsWith("PT"))
            {
                int num = int.Parse(last.Substring(2));
                num++;
                return "PT" + num.ToString("D7");
            }
            return "PT0000001";
        }

        private void txtSoLuongTonKho_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
