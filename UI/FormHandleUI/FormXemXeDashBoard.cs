using BLL;
using DTO;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace UI.FormHandleUI
{
    public partial class FormXemXeDashBoard : Form
    {
        private string idXe;
        private XeMayBLL xeMayBLL = new XeMayBLL();

        public FormXemXeDashBoard(string idXe)
        {
            InitializeComponent();
            this.idXe = idXe;
        }

        private void FormXemXeDashBoard_Load(object sender, EventArgs e)
        {
            LoadThongTinXe();
        }

        private void LoadThongTinXe()
        {
            try
            {
                XeMayDTO xe = xeMayBLL.GetXeMayById(idXe);
                if (xe == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin xe!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Hiển thị thông tin
                txtMaXe.Text = xe.ID_Xe;
                txtBienSo.Text = xe.BienSo;
                txtHangXe.Text = xe.TenHang;
                txtDongXe.Text = xe.TenDong;
                txtMauSac.Text = xe.TenMau;
                txtNamSanXuat.Text = xe.NamSX?.ToString() ?? "";
                txtLoaiXe.Text = xe.ID_Loai;
                txtNhaCungCap.Text = xe.MaNCC;
                txtGiaMua.Text = xe.GiaMua.HasValue ? xe.GiaMua.Value.ToString("N0") + " VNĐ" : "";
                txtGiaNhap.Text = xe.GiaNhap.HasValue ? xe.GiaNhap.Value.ToString("N0") + " VNĐ" : "";
                txtSoLuong.Text = xe.SoLuong?.ToString() ?? "1";
                txtNgayMua.Text = xe.NgayMua.HasValue ? xe.NgayMua.Value.ToString("dd/MM/yyyy") : "";
                txtNgayDangKy.Text = xe.NgayDangKy.HasValue ? xe.NgayDangKy.Value.ToString("dd/MM/yyyy") : "";
                txtHetHanDangKy.Text = xe.HetHanDangKy.HasValue ? xe.HetHanDangKy.Value.ToString("dd/MM/yyyy") : "";
                txtHetHanBaoHiem.Text = xe.HetHanBaoHiem.HasValue ? xe.HetHanBaoHiem.Value.ToString("dd/MM/yyyy") : "";
                txtKmDaChay.Text = xe.KmDaChay?.ToString() ?? "0";
                txtThongTinXang.Text = xe.ThongTinXang ?? "";
                txtTrangThai.Text = xe.TrangThai;
                txtMucDichSuDung.Text = xe.MucDichSuDung;

                // Hiển thị ảnh
                if (xe.AnhXe != null && xe.AnhXe.Length > 0)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(xe.AnhXe))
                        {
                            picAnhXe.Image = Image.FromStream(ms);
                            picAnhXe.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin xe: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
