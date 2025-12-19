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
        private ErrorProvider errorProvider1 = new ErrorProvider();

        public FormThemXe()
        {
            InitializeComponent();
            txtMaXe.Validating += txtMaXe_Validating;
            txtGiaMua.Validating += txtGiaMua_Validating;
            txtGiaNhap.Validating += txtGiaNhap_Validating;
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

        private void txtMaXe_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaXe.Text))
            {
                errorProvider1.SetError(txtMaXe, "Mã xe không được để trống!");
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
                errorProvider1.SetError(txtGiaMua, "Giá mua phải là số dương!");
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
                errorProvider1.SetError(txtGiaNhap, "Giá nhập phải là số dương!");
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
                // Kiểm tra nhập hợp lệ trước khi thêm
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
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!this.ValidateChildren())
                {
                    MessageBox.Show("Vui lòng nhập đúng và hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Build DTO (giống code trước)
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
                // Log chi tiết lỗi để debug
                string detailError = $"Lỗi: {ex.Message}\n\nChi tiết:\n{ex.ToString()}";
                System.Diagnostics.Debug.WriteLine(detailError);
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

            txtMaXe.Text = xeMayBLL.GenerateNewMaXe();

            cbbNamSanXuat.DataSource = Enumerable.Range(2000, DateTime.Now.Year - 1999).ToList();

            cbbTrangThai.Items.Clear();
            cbbTrangThai.Items.Add("Sẵn sàng");
            cbbTrangThai.Items.Add("Đang thuê");
            cbbTrangThai.Items.Add("Đã bán");
            cbbTrangThai.Items.Add("Đang bảo trì");
            cbbTrangThai.SelectedIndex = 0;

            cbbMucDichSuDung.Items.Clear();
            cbbMucDichSuDung.Items.Add("Cho thuê");
            cbbMucDichSuDung.Items.Add("Bán");
            cbbMucDichSuDung.SelectedIndex = 0;
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
