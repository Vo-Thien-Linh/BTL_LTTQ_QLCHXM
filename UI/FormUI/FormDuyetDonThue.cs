using BLL;
using DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace UI.FormUI
{
    public partial class FormDuyetDonThue : Form
    {
        private GiaoDichThueBLL giaoDichThueBLL;
        private string maNhanVien;
        private int selectedMaGD = 0;

        public FormDuyetDonThue(string maNV)
        {
            InitializeComponent();
            this.maNhanVien = maNV;
            giaoDichThueBLL = new GiaoDichThueBLL();
        }

        private void FormDuyetDonThue_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = giaoDichThueBLL.GetGiaoDichThueByTrangThai("Chờ duyệt");
                dgvDonChoDuyet.DataSource = dt;

                ConfigureDataGridView();

                lblTongSo.Text = $"Tổng: {dt.Rows.Count} đơn chờ duyệt";

                // Tự động chọn dòng đầu tiên
                if (dt.Rows.Count > 0)
                {
                    dgvDonChoDuyet.Rows[0].Selected = true;
                    ShowDetailInfo(dgvDonChoDuyet.Rows[0]);
                }
                else
                {
                    ClearDetailInfo();
                    MessageBox.Show("Không có đơn nào chờ duyệt!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            // Cấu hình header
            dgvDonChoDuyet.EnableHeadersVisualStyles = false;
            dgvDonChoDuyet.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            dgvDonChoDuyet.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDonChoDuyet.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvDonChoDuyet.ColumnHeadersHeight = 40;

            // Cấu hình cells
            dgvDonChoDuyet.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvDonChoDuyet.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 181, 246);
            dgvDonChoDuyet.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvDonChoDuyet.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            // Cấu hình cột
            if (dgvDonChoDuyet.Columns["MaGDThue"] != null)
            {
                dgvDonChoDuyet.Columns["MaGDThue"].HeaderText = "Mã GD";
                dgvDonChoDuyet.Columns["MaGDThue"].Width = 80;
            }

            if (dgvDonChoDuyet.Columns["HoTenKH"] != null)
            {
                dgvDonChoDuyet.Columns["HoTenKH"].HeaderText = "Khách hàng";
                dgvDonChoDuyet.Columns["HoTenKH"].Width = 150;
            }

            if (dgvDonChoDuyet.Columns["SdtKhachHang"] != null)
            {
                dgvDonChoDuyet.Columns["SdtKhachHang"].HeaderText = "SĐT";
                dgvDonChoDuyet.Columns["SdtKhachHang"].Width = 110;
            }

            if (dgvDonChoDuyet.Columns["TenXe"] != null)
            {
                dgvDonChoDuyet.Columns["TenXe"].HeaderText = "Xe";
                dgvDonChoDuyet.Columns["TenXe"].Width = 180;
            }

            if (dgvDonChoDuyet.Columns["BienSo"] != null)
            {
                dgvDonChoDuyet.Columns["BienSo"].HeaderText = "Biển số";
                dgvDonChoDuyet.Columns["BienSo"].Width = 100;
            }

            if (dgvDonChoDuyet.Columns["NgayBatDau"] != null)
            {
                dgvDonChoDuyet.Columns["NgayBatDau"].HeaderText = "Ngày bắt đầu";
                dgvDonChoDuyet.Columns["NgayBatDau"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvDonChoDuyet.Columns["NgayBatDau"].Width = 110;
            }

            if (dgvDonChoDuyet.Columns["NgayKetThuc"] != null)
            {
                dgvDonChoDuyet.Columns["NgayKetThuc"].HeaderText = "Ngày kết thúc";
                dgvDonChoDuyet.Columns["NgayKetThuc"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvDonChoDuyet.Columns["NgayKetThuc"].Width = 110;
            }

            if (dgvDonChoDuyet.Columns["SoNgayThue"] != null)
            {
                dgvDonChoDuyet.Columns["SoNgayThue"].HeaderText = "Số ngày";
                dgvDonChoDuyet.Columns["SoNgayThue"].Width = 80;
            }

            if (dgvDonChoDuyet.Columns["TongGia"] != null)
            {
                dgvDonChoDuyet.Columns["TongGia"].HeaderText = "Tổng tiền";
                dgvDonChoDuyet.Columns["TongGia"].DefaultCellStyle.Format = "N0";
                dgvDonChoDuyet.Columns["TongGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDonChoDuyet.Columns["TongGia"].Width = 120;
            }

            // Ẩn các cột không cần thiết
            string[] hideColumns = { "MaKH", "ID_Xe", "GiaThueNgay", "TrangThai",
                "TrangThaiThanhToan", "HinhThucThanhToan", "SoTienCoc", "NguoiDuyet",
                "NgayDuyet", "GhiChuDuyet", "TenNhanVien", "TrangThaiDuyet" };

            foreach (string col in hideColumns)
            {
                if (dgvDonChoDuyet.Columns[col] != null)
                    dgvDonChoDuyet.Columns[col].Visible = false;
            }
        }

        private void DgvDonChoDuyet_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDonChoDuyet.SelectedRows.Count > 0)
            {
                ShowDetailInfo(dgvDonChoDuyet.SelectedRows[0]);
            }
        }

        private void DgvDonChoDuyet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int maGD = Convert.ToInt32(dgvDonChoDuyet.Rows[e.RowIndex].Cells["MaGDThue"].Value);
                XemChiTietDonThue(maGD);
            }
        }

        private void ShowDetailInfo(DataGridViewRow row)
        {
            try
            {
                selectedMaGD = Convert.ToInt32(row.Cells["MaGDThue"].Value);

                txtMaGD.Text = selectedMaGD.ToString();
                txtKhachHang.Text = row.Cells["HoTenKH"].Value?.ToString() ?? "";
                txtSdt.Text = row.Cells["SdtKhachHang"].Value?.ToString() ?? "";
                txtXe.Text = row.Cells["TenXe"].Value?.ToString() ?? "";
                txtBienSo.Text = row.Cells["BienSo"].Value?.ToString() ?? "";

                DateTime ngayBatDau = Convert.ToDateTime(row.Cells["NgayBatDau"].Value);
                DateTime ngayKetThuc = Convert.ToDateTime(row.Cells["NgayKetThuc"].Value);

                txtNgayBatDau.Text = ngayBatDau.ToString("dd/MM/yyyy");
                txtNgayKetThuc.Text = ngayKetThuc.ToString("dd/MM/yyyy");
                txtSoNgay.Text = row.Cells["SoNgayThue"].Value?.ToString() ?? "0";

                decimal tongGia = Convert.ToDecimal(row.Cells["TongGia"].Value);
                txtTongTien.Text = tongGia.ToString("N0") + " VNĐ";

                // Kiểm tra xe khả dụng
                string idXe = row.Cells["ID_Xe"].Value?.ToString();
                if (!string.IsNullOrEmpty(idXe))
                {
                    CheckXeAvailability(idXe, ngayBatDau, ngayKetThuc);
                }

                // Enable buttons
                btnDuyet.Enabled = true;
                btnTuChoi.Enabled = true;
                btnXemChiTiet.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị thông tin: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckXeAvailability(string idXe, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            string errorMessage;
            bool isDangThue = giaoDichThueBLL.IsXeDangThue(idXe, ngayBatDau, ngayKetThuc, out errorMessage);

            if (isDangThue)
            {
                lblWarning.Text = "⚠ " + errorMessage;
                lblWarning.ForeColor = Color.FromArgb(244, 67, 54);
                lblWarning.Visible = true;
                btnDuyet.Enabled = false;
            }
            else
            {
                lblWarning.Text = "✓ Xe sẵn sàng cho thuê";
                lblWarning.ForeColor = Color.FromArgb(76, 175, 80);
                lblWarning.Visible = true;
                btnDuyet.Enabled = true;
            }
        }

        private void BtnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (selectedMaGD == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn để xem chi tiết!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            XemChiTietDonThue(selectedMaGD);
        }

        private void XemChiTietDonThue(int maGD)
        {
            try
            {
                using (FormXemHopDongThue form = new FormXemHopDongThue(maGD, maNhanVien))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        // Reload nếu có thay đổi
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form chi tiết: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDuyet_Click(object sender, EventArgs e)
        {
            if (selectedMaGD == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn cần duyệt!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra lại trước khi duyệt
            if (!btnDuyet.Enabled)
            {
                MessageBox.Show("Không thể duyệt đơn này!\n\nXe đã có lịch thuê trùng thời gian.",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn DUYỆT đơn #{selectedMaGD}?\n\n" +
                $"Khách hàng: {txtKhachHang.Text}\n" +
                $"Xe: {txtXe.Text} ({txtBienSo.Text})\n" +
                $"Thời gian: {txtNgayBatDau.Text} - {txtNgayKetThuc.Text}\n" +
                $"Tổng tiền: {txtTongTien.Text}\n\n" +
                $"Sau khi duyệt, trạng thái đơn sẽ chuyển sang 'Chờ giao xe'.",
                "Xác nhận duyệt đơn",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                string ghiChu = txtGhiChu.Text.Trim();
                string errorMessage;

                bool success = giaoDichThueBLL.ApproveGiaoDichThue(
                    selectedMaGD,
                    maNhanVien,
                    ghiChu,
                    out errorMessage
                );

                if (success)
                {
                    MessageBox.Show(
                        $"Đã duyệt đơn #{selectedMaGD} thành công!\n\n" +
                        $"Trạng thái mới: Chờ giao xe\n" +
                        $"Người duyệt: {maNhanVien}\n" +
                        $"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm}",
                        "Duyệt thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    LoadData();
                    ClearDetailInfo();
                }
                else
                {
                    MessageBox.Show(
                        "Không thể duyệt đơn!\n\n" + errorMessage,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void BtnTuChoi_Click(object sender, EventArgs e)
        {
            if (selectedMaGD == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn cần từ chối!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string lyDo = txtGhiChu.Text.Trim();

            if (string.IsNullOrWhiteSpace(lyDo))
            {
                MessageBox.Show(
                    "Vui lòng nhập lý do từ chối vào ô 'Ghi chú'!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtGhiChu.Focus();
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn TỪ CHỐI đơn #{selectedMaGD}?\n\n" +
                $"Khách hàng: {txtKhachHang.Text}\n" +
                $"Xe: {txtXe.Text} ({txtBienSo.Text})\n\n" +
                $"Lý do từ chối:\n{lyDo}\n\n" +
                $"Đơn sẽ bị hủy và KHÔNG THỂ KHÔI PHỤC!",
                "Xác nhận từ chối đơn",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                string errorMessage;

                bool success = giaoDichThueBLL.RejectGiaoDichThue(
                    selectedMaGD,
                    maNhanVien,
                    lyDo,
                    out errorMessage
                );

                if (success)
                {
                    MessageBox.Show(
                        $"✓ Đã từ chối đơn #{selectedMaGD}!\n\n" +
                        $"Lý do: {lyDo}\n" +
                        $"Người từ chối: {maNhanVien}\n" +
                        $"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm}",
                        "Từ chối thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    LoadData();
                    ClearDetailInfo();
                }
                else
                {
                    MessageBox.Show(
                        "Không thể từ chối đơn!\n\n" + errorMessage,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtGhiChu.Text = "";
            LoadData();
        }

        private void ClearDetailInfo()
        {
            selectedMaGD = 0;
            txtMaGD.Text = "";
            txtKhachHang.Text = "";
            txtSdt.Text = "";
            txtXe.Text = "";
            txtBienSo.Text = "";
            txtNgayBatDau.Text = "";
            txtNgayKetThuc.Text = "";
            txtSoNgay.Text = "";
            txtTongTien.Text = "";
            txtGhiChu.Text = "";
            lblWarning.Visible = false;
            btnDuyet.Enabled = false;
            btnTuChoi.Enabled = false;
            btnXemChiTiet.Enabled = false;
        }
    }
}