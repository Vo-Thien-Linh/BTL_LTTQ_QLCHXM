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
    public partial class FormSuaXe : Form
    {
        private string idXe;
        private XeMayBLL xeMayBLL = new XeMayBLL();
        private HangXeBLL hangXeBLL = new HangXeBLL();
        private DongXeBLL dongXeBLL = new DongXeBLL();
        private MauSacBLL mauSacBLL = new MauSacBLL();
        private NhaCungCapBLL nhaCungCapBLL = new NhaCungCapBLL();
        private LoaiXeBLL loaiXeBLL = new LoaiXeBLL();
        public FormSuaXe(string idXe)
        {
            InitializeComponent();
            this.idXe = idXe;
        }
        
        private void FormSuaXe_Load(object sender, EventArgs e)
        {
            // Đổ dữ liệu vào Combobox
            LoadHangXe();
            LoadMauSac();
            LoadNhaCungCap();
            LoadLoaiXe();
            LoadNamSanXuat();
            LoadTrangThai();

            // Load xe lên form
            XeMayDTO xe = xeMayBLL.GetXeMayById(idXe);
            if (xe == null)
            {
                MessageBox.Show("Không tìm thấy thông tin xe!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            // Gán dữ liệu lên control
            txtMaXe.Text = xe.ID_Xe;
            txtMaXe.ReadOnly = true;
            txtBienSo.Text = xe.BienSo;
            cbbHangXe.SelectedValue = xe.MaHang;
            LoadDongXe(xe.MaHang); // Cập nhật dòng xe tương ứng
            cbbDongXe.SelectedValue = xe.MaDong;
            cbbMauSac.SelectedValue = xe.MaMau;
            cbbNhaCungCap.SelectedValue = xe.MaNCC;
            cbbNamSanXuat.SelectedItem = xe.NamSX.ToString();
            cbbTrangThai.SelectedItem = xe.TrangThai;
            dtpNgayMua.Value = xe.NgayMua ?? DateTime.Now;
            txtGiaMua.Text = xe.GiaMua.HasValue ? xe.GiaMua.Value.ToString() : "";
            dtpNgayDangKy.Value = xe.NgayDangKy ?? DateTime.Now;
            dtpNgayHetHanDangKy.Value = xe.HetHanDangKy ?? DateTime.Now;
            dtpNgayHetHanBaoHiem.Value = xe.HetHanBaoHiem ?? DateTime.Now;
            nudKHDaChay.Value = xe.KmDaChay ?? 0;
            txtThongTinXang.Text = xe.ThongTinXang ?? "";
            picAnhXe.ImageLocation = xe.AnhXeXeBan ?? "";
            cbbLoaiXe.SelectedValue = xe.ID_Loai;


        }

        // --- Đổ dữ liệu cho các combobox ---
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
            cbbTrangThai.Items.Add("Sẵn sàng");
            cbbTrangThai.Items.Add("Đang thuê");
            cbbTrangThai.Items.Add("Đã bán");
            cbbTrangThai.Items.Add("Đang bảo trì");
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
            ofd.Title = "Chọn ảnh xe";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picAnhXe.ImageLocation = ofd.FileName;
                picAnhXe.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void picAnhXe_Click(object sender, EventArgs e)
        {
            btnChonFileAnh_Click(sender, e);
        }

        private void txtGiaMua_TextChanged(object sender, EventArgs e)
        {

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
            // Validate dữ liệu cần thiết nếu muốn...
            if (string.IsNullOrWhiteSpace(txtBienSo.Text))
            {
                MessageBox.Show("Biển số xe không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Tạo DTO cập nhật
            var xeDTO = new XeMayDTO
            {
                ID_Xe = txtMaXe.Text.Trim(),
                BienSo = txtBienSo.Text.Trim(),
                ID_Loai = cbbLoaiXe.SelectedValue.ToString(),
                MaNCC = cbbNhaCungCap.SelectedValue.ToString(),
                NgayMua = dtpNgayMua.Value,
                GiaMua = decimal.TryParse(txtGiaMua.Text, out var gia) ? gia : (decimal?)null,
                NgayDangKy = dtpNgayDangKy.Value,
                HetHanDangKy = dtpNgayHetHanDangKy.Value,
                HetHanBaoHiem = dtpNgayHetHanBaoHiem.Value,
                KmDaChay = (int)nudKHDaChay.Value,
                ThongTinXang = txtThongTinXang.Text,
                AnhXeXeBan = picAnhXe.ImageLocation,
                TrangThai = cbbTrangThai.SelectedItem?.ToString()
            };

            bool success = xeMayBLL.UpdateXeMay(xeDTO);
            if (success)
            {
                MessageBox.Show("Sửa xe thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Sửa xe thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
