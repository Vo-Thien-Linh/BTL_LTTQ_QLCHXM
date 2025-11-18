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
        private byte[] anhXeBytes = null; // Lưu ảnh dạng byte[]

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

        private void btnChonFileAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn ảnh xe";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Hiển thị ảnh trong PictureBox
                    picAnhXe.Image = Image.FromFile(ofd.FileName);
                    picAnhXe.SizeMode = PictureBoxSizeMode.Zoom;
                    
                    // Chuyển ảnh thành byte[] để lưu vào database
                    using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        anhXeBytes = new byte[fs.Length];
                        fs.Read(anhXeBytes, 0, (int)fs.Length);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnThemXe_Click(object sender, EventArgs e)
        {
            try
            {
                // Build DTO
                XeMayDTO dto = new XeMayDTO
                {
                    ID_Xe = txtMaXe.Text.Trim(),
                    BienSo = txtBienSo.Text.Trim(),
                    MaNCC = cbbNhaCungCap.SelectedValue?.ToString(),
                    NgayMua = dtpNgayMua.Value,
                    GiaMua = string.IsNullOrWhiteSpace(txtGiaMua.Text) ? (decimal?)null : decimal.Parse(txtGiaMua.Text),
                    GiaNhap = string.IsNullOrWhiteSpace(txtGiaNhap.Text) ? (decimal?)null : decimal.Parse(txtGiaNhap.Text),
                    SoLuong = (int?)nudSoLuong.Value,
                    SoLuongBanRa = 0, // Luôn = 0 khi thêm mới, chỉ tăng khi có hóa đơn bán
                    NgayDangKy = dtpNgayDangKy.Value,
                    HetHanDangKy = dtpNgayHetHanDangKy.Value,
                    HetHanBaoHiem = dtpNgayHetHanBaoHiem.Value,
                    KmDaChay = Convert.ToInt32(nudKHDaChay.Value),
                    ThongTinXang = txtThongTinXang.Text.Trim(),
                    AnhXe = anhXeBytes, // Sử dụng byte[] thay vì file path
                    TrangThai = cbbTrangThai.SelectedItem?.ToString(),
                    MucDichSuDung = cbbMucDichSuDung.SelectedItem?.ToString(),
                    
                    // Thông tin cho việc build ID_Loai
                    MaHang = cbbHangXe.SelectedValue?.ToString(),
                    MaDong = cbbDongXe.SelectedValue?.ToString(),
                    MaMau = cbbMauSac.SelectedValue?.ToString(),
                    NamSX = cbbNamSanXuat.SelectedItem != null ? (int?)Convert.ToInt32(cbbNamSanXuat.SelectedItem) : null
                };

                // Tìm hoặc tạo ID_Loai từ MaHang, MaDong, MaMau, NamSX
                if (!string.IsNullOrEmpty(dto.MaHang) && !string.IsNullOrEmpty(dto.MaDong) && 
                    !string.IsNullOrEmpty(dto.MaMau) && dto.NamSX.HasValue)
                {
                    dto.ID_Loai = LoaiXeBLL.GetOrCreateIDLoai(dto.MaHang, dto.MaDong, dto.MaMau, dto.NamSX.Value);
                }

                // BLL xử lý - sẽ throw Exception nếu có lỗi với thông báo chi tiết
                bool success = xeMayBLL.InsertXeMay(dto);

                if (success)
                {
                    MessageBox.Show("Thêm xe thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm xe thất bại!\nVui lòng kiểm tra lại thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi chi tiết từ BLL
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            // Đổ mục đích sử dụng
            cbbMucDichSuDung.Items.Clear();
            cbbMucDichSuDung.Items.Add("Cho thuê");
            cbbMucDichSuDung.Items.Add("Bán");
            cbbMucDichSuDung.SelectedIndex = 0; // Mặc định "Cho thuê"
        }

        private void txtGiaMua_TextChanged(object sender, EventArgs e)
        {
            // Tự động tính GiaNhap = 85% GiaMua
            if (decimal.TryParse(txtGiaMua.Text, out decimal giaMua))
            {
                txtGiaNhap.Text = (giaMua * 0.85m).ToString("0.##");
            }
            else
            {
                txtGiaNhap.Text = "";
            }
        }
    }
}
