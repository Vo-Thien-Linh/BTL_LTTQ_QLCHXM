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
    
    public partial class FormThemXe : Form
    {
        private HangXeBLL HangXeBLL = new HangXeBLL();
        private DongXeBLL DongXeBLL = new DongXeBLL();
        private MauSacBLL MauSacBLL = new MauSacBLL();
        private NhaCungCapBLL NhaCungCapBLL = new NhaCungCapBLL();
        private LoaiXeBLL LoaiXeBLL = new LoaiXeBLL();
        private XeMayBLL xeMayBLL = new XeMayBLL();

        public FormThemXe()
        {
            InitializeComponent();
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

        private void txtGiaMua_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnChonFileAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn ảnh xe";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picAnhXe.ImageLocation = ofd.FileName; // gán cho PictureBox
                picAnhXe.SizeMode = PictureBoxSizeMode.Zoom; // auto fit
                                                             // Nếu cần lưu đường dẫn:
                                                             // txtAnhXe.Text = ofd.FileName; hoặc biến string đường dẫn ảnh xe
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

        private void btnThemXe_Click(object sender, EventArgs e)
        {
            // Validate dữ liệu (ví dụ: chưa nhập đủ...)
            if (string.IsNullOrWhiteSpace(txtMaXe.Text) ||
                string.IsNullOrWhiteSpace(txtBienSo.Text) ||
                cbbHangXe.SelectedItem == null ||
                cbbDongXe.SelectedItem == null ||
                cbbMauSac.SelectedItem == null ||
                cbbNhaCungCap.SelectedItem == null ||
                cbbTrangThai.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Build DTO
            XeMayDTO dto = new XeMayDTO
            {
                ID_Xe = txtMaXe.Text.Trim(),
                BienSo = txtBienSo.Text.Trim(),
                // Tính/gán đúng ID_Loai theo logic của bạn, ví dụ:
                ID_Loai = cbbLoaiXe.SelectedValue?.ToString(), // hoặc xử lý build từ hãng, dòng, màu, năm
                MaNCC = cbbNhaCungCap.SelectedValue.ToString(),
                NgayMua = dtpNgayMua.Value,
                GiaMua = decimal.Parse(txtGiaMua.Text), // nếu dùng NumericUpDown thì dùng .Value
                NgayDangKy = dtpNgayDangKy.Value,
                HetHanDangKy = dtpNgayHetHanDangKy.Value,
                HetHanBaoHiem = dtpNgayHetHanBaoHiem.Value,
                KmDaChay = Convert.ToInt32(nudKHDaChay.Value),
                ThongTinXang = txtThongTinXang.Text,
                AnhXeXeBan = picAnhXe.ImageLocation, // nếu lưu link
                TrangThai = cbbTrangThai.SelectedItem.ToString()
            };

            // BLL xử lý
            var xeMayBLL = new XeMayBLL();
            bool success = xeMayBLL.InsertXeMay(dto);

            if (success)
            {
                MessageBox.Show("Thêm xe thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Để form gốc nhận biết đã thêm thành công
                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm xe thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Sinh mã xe
            txtMaXe.Text = xeMayBLL.GenerateNewMaXe();

            // Đổ năm sản xuất (ví dụ: 2000-2025)
            cbbNamSanXuat.DataSource = Enumerable.Range(2000, DateTime.Now.Year - 1999).ToList();

            // Đổ trạng thái
            cbbTrangThai.Items.Clear();
            cbbTrangThai.Items.Add("Sẵn sàng");
            cbbTrangThai.Items.Add("Đang thuê");
            cbbTrangThai.Items.Add("Đã bán");
            cbbTrangThai.Items.Add("Đang bảo trì");
            cbbTrangThai.SelectedIndex = 0;
        }
    }
}
