using BLL;
using DTO;
using System;
using System.Data;
using System.Windows.Forms;

namespace UI.FormUI
{
    public partial class FormChonPhuTung : Form
    {
        private PhuTungBLL phuTungBLL;
        public PhuTungDTO PhuTungDaChon { get; private set; }
        public int SoLuong { get; private set; }

        public FormChonPhuTung()
        {
            InitializeComponent();
            phuTungBLL = new PhuTungBLL();
            
            LoadDanhSachPhuTung();
            SetupEvents();
            
            numSoLuong.Minimum = 1;
            numSoLuong.Maximum = 1000;
            numSoLuong.Value = 1;
        }

        private void SetupEvents()
        {
            btnXacNhan.Click += BtnXacNhan_Click;
            btnHuy.Click += BtnHuy_Click;
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            dgvPhuTung.CellDoubleClick += DgvPhuTung_CellDoubleClick;
        }

        private void LoadDanhSachPhuTung()
        {
            try
            {
                // Kiểm tra dgvPhuTung đã khởi tạo chưa
                if (dgvPhuTung == null)
                    return;

                DataTable dt = phuTungBLL.GetAllPhuTungWithTonKho();
                
                // Kiểm tra dữ liệu
                if (dt == null || dt.Rows.Count == 0)
                {
                    dgvPhuTung.DataSource = null;
                    if (lblTongSo != null) lblTongSo.Text = "Tổng số: 0 phụ tùng";
                    MessageBox.Show(
                        "Chưa có phụ tùng nào trong kho!\nVui lòng thêm phụ tùng trước khi sử dụng chức năng này.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
                
                // Lọc chỉ hiển thị phụ tùng còn tồn kho
                DataView dv = dt.DefaultView;
                dv.RowFilter = "SoLuongTon > 0";
                
                if (dv.Count == 0)
                {
                    dgvPhuTung.DataSource = null;
                    if (lblTongSo != null) lblTongSo.Text = "Tổng số: 0 phụ tùng";
                    MessageBox.Show(
                        "Hiện tại không có phụ tùng nào còn tồn kho!\nVui lòng nhập thêm phụ tùng.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
                
                dgvPhuTung.DataSource = dv.ToTable();

                // BỎ QUA PHẦN CẤU HÌNH WIDTH - Tự động resize
                
                if (lblTongSo != null) lblTongSo.Text = $"Tổng số: {dv.Count} phụ tùng";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải danh sách phụ tùng: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();
                
                if (string.IsNullOrEmpty(keyword))
                {
                    LoadDanhSachPhuTung();
                    return;
                }

                DataTable dt = phuTungBLL.SearchPhuTung(keyword, "Tất cả");
                
                // Kiểm tra dữ liệu
                if (dt == null || dt.Rows.Count == 0)
                {
                    dgvPhuTung.DataSource = null;
                    lblTongSo.Text = "Tìm thấy: 0 phụ tùng";
                    return;
                }
                
                // Lọc chỉ hiển thị phụ tùng còn tồn kho
                DataView dv = dt.DefaultView;
                dv.RowFilter = "SoLuongTon > 0";
                
                dgvPhuTung.DataSource = dv.ToTable();
                lblTongSo.Text = $"Tìm thấy: {dv.Count} phụ tùng";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tìm kiếm: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DgvPhuTung_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnXacNhan_Click(sender, e);
            }
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPhuTung.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        "Vui lòng chọn phụ tùng!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow selectedRow = dgvPhuTung.SelectedRows[0];
                string maPhuTung = selectedRow.Cells["MaPhuTung"].Value?.ToString() ?? "";
                int soLuongTon = Convert.ToInt32(selectedRow.Cells["SoLuongTon"].Value);
                int soLuongChon = (int)numSoLuong.Value;

                // Kiểm tra số lượng
                if (soLuongChon > soLuongTon)
                {
                    MessageBox.Show(
                        $"Số lượng tồn kho không đủ!\nTồn kho: {soLuongTon}, Bạn chọn: {soLuongChon}",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Lấy thông tin phụ tùng chi tiết
                PhuTungDaChon = phuTungBLL.GetPhuTungById(maPhuTung);
                SoLuong = soLuongChon;

                if (PhuTungDaChon != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "Không thể lấy thông tin phụ tùng!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi chọn phụ tùng: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
