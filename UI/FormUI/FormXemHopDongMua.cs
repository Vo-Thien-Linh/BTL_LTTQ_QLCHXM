using BLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace UI.FormUI
{
    public partial class FormXemHopDongMua : Form
    {
        private HopDongMuaBLL hopDongMuaBLL;
        private int maGDBan;

        public FormXemHopDongMua(int maGiaoDichBan)
        {
            InitializeComponent();
            hopDongMuaBLL = new HopDongMuaBLL();
            maGDBan = maGiaoDichBan;

            LoadHopDong();
        }

        private void LoadHopDong()
        {
            try
            {
                DataTable dt = hopDongMuaBLL.GetHopDongByMaGDBan(maGDBan);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy hợp đồng cho giao dịch này!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                DataRow row = dt.Rows[0];

                // Hiển thị thông tin hợp đồng
                txtMaHDM.Text = row["MaHDM"].ToString();
                txtMaGDBan.Text = row["MaGDBan"].ToString();
                txtNgayLap.Text = Convert.ToDateTime(row["NgayLap"]).ToString("dd/MM/yyyy HH:mm");

                // Thông tin khách hàng
                txtMaKH.Text = row["MaKH"].ToString();
                txtHoTenKH.Text = row["HoTenKH"].ToString();
                txtSdtKH.Text = row["Sdt"]?.ToString() ?? "";
                txtDiaChiKH.Text = row["DiaChi"]?.ToString() ?? "";
                txtCCCD.Text = row["SoCCCD"]?.ToString() ?? "";

                // Thông tin xe
                txtIDXe.Text = row["ID_Xe"].ToString();
                txtTenXe.Text = row["TenXe"].ToString();
                txtBienSo.Text = row["BienSo"].ToString();

                // Thông tin hợp đồng
                decimal giaBan = Convert.ToDecimal(row["GiaBan"]);
                txtGiaBan.Text = giaBan.ToString("N0") + " VNĐ";
                txtDieuKhoan.Text = row["DieuKhoan"]?.ToString() ?? "";
                txtGhiChu.Text = row["GhiChu"]?.ToString() ?? "";
                txtTrangThai.Text = row["TrangThaiHopDong"]?.ToString() ?? "";
                txtNhanVien.Text = row["TenNhanVien"]?.ToString() ?? "";

                // Màu sắc trạng thái
                string trangThai = txtTrangThai.Text;
                if (trangThai == "Đang hiệu lực")
                    txtTrangThai.ForeColor = Color.FromArgb(76, 175, 80);
                else if (trangThai == "Hết hạn")
                    txtTrangThai.ForeColor = Color.FromArgb(255, 152, 0);
                else if (trangThai == "Hủy")
                    txtTrangThai.ForeColor = Color.FromArgb(244, 67, 54);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải hợp đồng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnIn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng in hợp đồng đang được phát triển!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
