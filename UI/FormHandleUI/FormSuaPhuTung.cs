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
    public partial class FormSuaPhuTung : Form
    {
        private string maPhuTung;
        private PhuTungBLL phuTungBLL = new PhuTungBLL();
        private HangXeBLL hangXeBLL = new HangXeBLL();
        private DongXeBLL dongXeBLL = new DongXeBLL();
        public FormSuaPhuTung(string maPhuTung)
        {
            InitializeComponent();
            this.maPhuTung = maPhuTung;
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

        private void txtSoLuongTonKho_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtGhiChu_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSuaPhuTung_Click(object sender, EventArgs e)
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
            string tenPT = txtTenPhuTung.Text.Trim();
            string maHang = cbbHangXeTuongThich.SelectedValue.ToString();
            string maDong = cbbDongXeTuongThich.SelectedValue.ToString();
            string donVi = cbbDonViTinh.SelectedItem.ToString();
            string ghiChu = txtGhiChu.Text.Trim();
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

        private void FormSuaPhuTung_Load(object sender, EventArgs e)
        {
            // Đổ hãng xe
            cbbHangXeTuongThich.DataSource = hangXeBLL.GetAllHangXe();
            cbbHangXeTuongThich.DisplayMember = "TenHang";
            cbbHangXeTuongThich.ValueMember = "MaHang";
            cbbDonViTinh.Items.Clear();
            cbbDonViTinh.Items.AddRange(new string[] { "Cái", "Bộ", "Chiếc", "Lít", "Khác..." });
            cbbDonViTinh.SelectedIndex = 0;

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
            cbbHangXeTuongThich.SelectedValue = pt.MaHangXe;
            LoadDongXeTuongThich(pt.MaHangXe);
            cbbDongXeTuongThich.SelectedValue = pt.MaDongXe;
            txtGiaMua.Text = pt.GiaMua.ToString();
            txtGiaBan.Text = pt.GiaBan.ToString();
            cbbDonViTinh.SelectedItem = pt.DonViTinh;
            txtSoLuongTonKho.Text = pt.SoLuongTon.ToString(); // hoặc lấy từ kho
            txtGhiChu.Text = pt.GhiChu;
        }
        private void LoadDongXeTuongThich(string maHang)
        {
            cbbDongXeTuongThich.DataSource = dongXeBLL.GetDongXeByHang(maHang);
            cbbDongXeTuongThich.DisplayMember = "TenDong";
            cbbDongXeTuongThich.ValueMember = "MaDong";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
