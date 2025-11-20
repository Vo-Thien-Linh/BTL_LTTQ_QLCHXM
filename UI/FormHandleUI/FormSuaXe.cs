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
        private string idXe;
        private XeMayDTO currentXe; // Lưu trữ thông tin xe hiện tại
        private XeMayBLL xeMayBLL = new XeMayBLL();
        private HangXeBLL hangXeBLL = new HangXeBLL();
        private DongXeBLL dongXeBLL = new DongXeBLL();
        private MauSacBLL mauSacBLL = new MauSacBLL();
        private NhaCungCapBLL nhaCungCapBLL = new NhaCungCapBLL();
        private LoaiXeBLL loaiXeBLL = new LoaiXeBLL();
        private byte[] anhXeBytes = null; // Lưu ảnh dạng byte[]
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
            LoadMucDichSuDung();

            // Load xe lên form
            XeMayDTO xe = xeMayBLL.GetXeMayById(idXe);
            currentXe = xe; // Lưu lại để dùng sau này
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
            cbbMucDichSuDung.SelectedItem = xe.MucDichSuDung; // Load mục đích sử dụng
            dtpNgayMua.Value = xe.NgayMua ?? DateTime.Now;
            txtGiaMua.Text = xe.GiaMua.HasValue ? xe.GiaMua.Value.ToString() : "";
            txtGiaNhap.Text = xe.GiaNhap.HasValue ? xe.GiaNhap.Value.ToString() : "";
            
            // Xử lý số lượng - cho phép 0
            int soLuong = xe.SoLuong ?? 0;
            nudSoLuong.Minimum = 0; // Cho phép giá trị 0
            nudSoLuong.Value = soLuong;
            
            dtpNgayDangKy.Value = xe.NgayDangKy ?? DateTime.Now;
            dtpNgayHetHanDangKy.Value = xe.HetHanDangKy ?? DateTime.Now;
            dtpNgayHetHanBaoHiem.Value = xe.HetHanBaoHiem ?? DateTime.Now;
            nudKHDaChay.Value = xe.KmDaChay ?? 0;
            txtThongTinXang.Text = xe.ThongTinXang ?? "";
            
            // Hiển thị ảnh từ byte[]
            if (xe.AnhXe != null && xe.AnhXe.Length > 0)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(xe.AnhXe))
                    {
                        picAnhXe.Image = Image.FromStream(ms);
                        picAnhXe.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    anhXeBytes = xe.AnhXe; // Lưu lại byte[] hiện tại
                }
                catch { }
            }
            
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
        
        private void LoadMucDichSuDung()
        {
            cbbMucDichSuDung.Items.Clear();
            cbbMucDichSuDung.Items.Add("Cho thuê");
            cbbMucDichSuDung.Items.Add("Bán");
            cbbMucDichSuDung.SelectedIndex = 0; // Mặc định "Cho thuê"
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
                // Tạo DTO cập nhật
                var xeDTO = new XeMayDTO
                {
                    ID_Xe = txtMaXe.Text.Trim(),
                    BienSo = txtBienSo.Text.Trim(),
                    MaNCC = cbbNhaCungCap.SelectedValue?.ToString(),
                    NgayMua = dtpNgayMua.Value,
                    GiaMua = decimal.TryParse(txtGiaMua.Text, out var gia) ? gia : (decimal?)null,
                    GiaNhap = decimal.TryParse(txtGiaNhap.Text, out var giaNhap) ? giaNhap : (decimal?)null,
                    SoLuong = (int?)nudSoLuong.Value,
                    SoLuongBanRa = currentXe?.SoLuongBanRa ?? 0, // Giữ nguyên từ DB
                    NgayDangKy = dtpNgayDangKy.Value,
                    HetHanDangKy = dtpNgayHetHanDangKy.Value,
                    HetHanBaoHiem = dtpNgayHetHanBaoHiem.Value,
                    KmDaChay = (int)nudKHDaChay.Value,
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
                if (!string.IsNullOrEmpty(xeDTO.MaHang) && !string.IsNullOrEmpty(xeDTO.MaDong) && 
                    !string.IsNullOrEmpty(xeDTO.MaMau) && xeDTO.NamSX.HasValue)
                {
                    xeDTO.ID_Loai = loaiXeBLL.GetOrCreateIDLoai(xeDTO.MaHang, xeDTO.MaDong, xeDTO.MaMau, xeDTO.NamSX.Value);
                }

                // BLL xử lý - sẽ throw Exception nếu có lỗi với thông báo chi tiết
                bool success = xeMayBLL.UpdateXeMay(xeDTO);
                
                if (success)
                {
                    MessageBox.Show("Sửa xe thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sửa xe thất bại!\nVui lòng kiểm tra lại thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi chi tiết từ BLL
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtGiaMua_TextChanged(object sender, EventArgs e)
        {
            // Tự động tính GiaNhap = 85% GiaMua nếu chưa có giá nhập
            if (decimal.TryParse(txtGiaMua.Text, out decimal giaMua))
            {
                // Chỉ tự động tính nếu txtGiaNhap rỗng hoặc bằng 85% giá mửa cũ
                if (string.IsNullOrWhiteSpace(txtGiaNhap.Text) ||
                    (currentXe != null && currentXe.GiaMua.HasValue && currentXe.GiaNhap.HasValue &&
                     Math.Abs(currentXe.GiaNhap.Value - currentXe.GiaMua.Value * 0.85m) < 0.01m))
                {
                    txtGiaNhap.Text = (giaMua * 0.85m).ToString("0.##");
                }
            }
        }
    }
}
