using BLL;
using DTO;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace UI.FormHandleUI
{
    public partial class FormChinhSuaThongTinXeDashBoard : Form
    {
        private string idXe;
        private XeMayDTO currentXe;
        private XeMayBLL xeMayBLL = new XeMayBLL();
        private HangXeBLL hangXeBLL = new HangXeBLL();
        private DongXeBLL dongXeBLL = new DongXeBLL();
        private MauSacBLL mauSacBLL = new MauSacBLL();
        private NhaCungCapBLL nhaCungCapBLL = new NhaCungCapBLL();
        private LoaiXeBLL loaiXeBLL = new LoaiXeBLL();
        private byte[] anhXeBytes = null;
        public bool IsUpdated { get; private set; }

        public FormChinhSuaThongTinXeDashBoard(string idXe)
        {
            InitializeComponent();
            this.idXe = idXe;
            IsUpdated = false;
        }

        private void FormChinhSuaThongTinXeDashBoard_Load(object sender, EventArgs e)
        {
            LoadHangXe();
            LoadMauSac();
            LoadNhaCungCap();
            LoadLoaiXe();
            LoadNamSanXuat();
            LoadTrangThai();
            LoadMucDichSuDung();
            LoadThongTinXe();
        }

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
        }

        private void LoadThongTinXe()
        {
            try
            {
                XeMayDTO xe = xeMayBLL.GetXeMayById(idXe);
                currentXe = xe;
                if (xe == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin xe!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                txtMaXe.Text = xe.ID_Xe;
                txtMaXe.ReadOnly = true;
                txtBienSo.Text = xe.BienSo;
                cbbHangXe.SelectedValue = xe.MaHang;
                LoadDongXe(xe.MaHang);
                cbbDongXe.SelectedValue = xe.MaDong;
                cbbMauSac.SelectedValue = xe.MaMau;
                cbbNhaCungCap.SelectedValue = xe.MaNCC;
                cbbNamSanXuat.SelectedItem = xe.NamSX?.ToString();
                cbbTrangThai.SelectedItem = xe.TrangThai;
                cbbMucDichSuDung.SelectedItem = xe.MucDichSuDung;
                dtpNgayMua.Value = xe.NgayMua ?? DateTime.Now;
                txtGiaMua.Text = xe.GiaMua.HasValue ? xe.GiaMua.Value.ToString() : "";
                txtGiaNhap.Text = xe.GiaNhap.HasValue ? xe.GiaNhap.Value.ToString() : "";
                nudSoLuong.Value = xe.SoLuong ?? 1;
                dtpNgayDangKy.Value = xe.NgayDangKy ?? DateTime.Now;
                dtpNgayHetHanDangKy.Value = xe.HetHanDangKy ?? DateTime.Now;
                dtpNgayHetHanBaoHiem.Value = xe.HetHanBaoHiem ?? DateTime.Now;
                nudKMDaChay.Value = xe.KmDaChay ?? 0;
                txtThongTinXang.Text = xe.ThongTinXang ?? "";

                if (xe.AnhXe != null && xe.AnhXe.Length > 0)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(xe.AnhXe))
                        {
                            picAnhXe.Image = Image.FromStream(ms);
                            picAnhXe.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        anhXeBytes = xe.AnhXe;
                    }
                    catch { }
                }

                cbbLoaiXe.SelectedValue = xe.ID_Loai;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin xe: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbbHangXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbHangXe.SelectedValue != null)
                LoadDongXe(cbbHangXe.SelectedValue.ToString());
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
                    picAnhXe.Image = Image.FromFile(ofd.FileName);
                    picAnhXe.SizeMode = PictureBoxSizeMode.Zoom;

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

        private void btnSuaXe_Click(object sender, EventArgs e)
        {
            try
            {
                var xeDTO = new XeMayDTO
                {
                    ID_Xe = txtMaXe.Text.Trim(),
                    BienSo = txtBienSo.Text.Trim(),
                    MaNCC = cbbNhaCungCap.SelectedValue?.ToString(),
                    NgayMua = dtpNgayMua.Value,
                    GiaMua = decimal.TryParse(txtGiaMua.Text, out var gia) ? gia : (decimal?)null,
                    GiaNhap = decimal.TryParse(txtGiaNhap.Text, out var giaNhap) ? giaNhap : (decimal?)null,
                    SoLuong = (int?)nudSoLuong.Value,
                    SoLuongBanRa = currentXe?.SoLuongBanRa ?? 0,
                    NgayDangKy = dtpNgayDangKy.Value,
                    HetHanDangKy = dtpNgayHetHanDangKy.Value,
                    HetHanBaoHiem = dtpNgayHetHanBaoHiem.Value,
                    KmDaChay = (int)nudKMDaChay.Value,
                    ThongTinXang = txtThongTinXang.Text.Trim(),
                    AnhXe = anhXeBytes,
                    TrangThai = cbbTrangThai.SelectedItem?.ToString(),
                    MucDichSuDung = cbbMucDichSuDung.SelectedItem?.ToString(),
                    MaHang = cbbHangXe.SelectedValue?.ToString(),
                    MaDong = cbbDongXe.SelectedValue?.ToString(),
                    MaMau = cbbMauSac.SelectedValue?.ToString(),
                    NamSX = cbbNamSanXuat.SelectedItem != null ? (int?)Convert.ToInt32(cbbNamSanXuat.SelectedItem) : null
                };

                if (!string.IsNullOrEmpty(xeDTO.MaHang) && !string.IsNullOrEmpty(xeDTO.MaDong) &&
                    !string.IsNullOrEmpty(xeDTO.MaMau) && xeDTO.NamSX.HasValue)
                {
                    xeDTO.ID_Loai = loaiXeBLL.GetOrCreateIDLoai(xeDTO.MaHang, xeDTO.MaDong, xeDTO.MaMau, xeDTO.NamSX.Value);
                }

                bool success = xeMayBLL.UpdateXeMay(xeDTO);

                if (success)
                {
                    MessageBox.Show("Chỉnh sửa thông tin xe thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    IsUpdated = true;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Chỉnh sửa thất bại!\nVui lòng kiểm tra lại thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtGiaMua_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtGiaMua.Text, out decimal giaMua))
            {
                if (string.IsNullOrWhiteSpace(txtGiaNhap.Text) ||
                    (currentXe != null && currentXe.GiaMua.HasValue && currentXe.GiaNhap.HasValue &&
                     Math.Abs(currentXe.GiaNhap.Value - currentXe.GiaMua.Value * 0.85m) < 0.01m))
                {
                    txtGiaNhap.Text = (giaMua * 0.85m).ToString("0.##");
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

