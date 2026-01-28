using BLL;
using DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace UI.FormUI
{
    public partial class FormChonPhuTung : Form
    {
        private PhuTungBLL phuTungBLL;
        public PhuTungDTO PhuTungDaChon { get; private set; }
        public int SoLuong { get; private set; }
        public KhuyenMaiDTO KhuyenMaiDaChon { get; private set; }
        public decimal SoTienGiamKhuyenMai { get; private set; }

        private readonly KhuyenMaiBLL khuyenMaiBLL = new KhuyenMaiBLL();

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
                    decimal thanhTien = PhuTungDaChon.GiaBan * SoLuong;

                    // Cho phép chọn khuyến mãi cho phụ tùng (nếu có)
                    if (!ChonKhuyenMaiChoPhuTung(thanhTien))
                        return;

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

        /// <summary>
        /// Hiển thị hộp thoại nhỏ cho phép chọn khuyến mãi áp dụng cho phụ tùng.
        /// Trả về false nếu người dùng bấm Hủy.
        /// </summary>
        private bool ChonKhuyenMaiChoPhuTung(decimal thanhTien)
        {
            try
            {
                DataTable dt = khuyenMaiBLL.GetKhuyenMaiHieuLuc(DateTime.Now, "Phụ tùng");

                // Không có khuyến mãi khả dụng
                if (dt == null || dt.Rows.Count == 0)
                {
                    KhuyenMaiDaChon = null;
                    SoTienGiamKhuyenMai = 0;
                    return true;
                }

                Form picker = new Form
                {
                    Text = "Chọn khuyến mãi phụ tùng",
                    Size = new Size(420, 240),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                Label lblTitle = new Label
                {
                    Text = "Chọn khuyến mãi áp dụng",
                    AutoSize = true,
                    Location = new Point(16, 16)
                };

                ComboBox cboKm = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(16, 44),
                    Width = 360,
                    DataSource = dt,
                    DisplayMember = "TenKM",
                    ValueMember = "MaKM"
                };

                CheckBox chkNone = new CheckBox
                {
                    Text = "Không áp dụng khuyến mãi",
                    AutoSize = true,
                    Location = new Point(16, 78)
                };

                Label lblInfo = new Label
                {
                    AutoSize = true,
                    MaximumSize = new Size(360, 0),
                    Location = new Point(16, 110)
                };

                Button btnOk = new Button
                {
                    Text = "Đồng ý",
                    DialogResult = DialogResult.OK,
                    Location = new Point(200, 170),
                    Width = 80
                };

                Button btnCancel = new Button
                {
                    Text = "Hủy",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(296, 170),
                    Width = 80
                };

                picker.AcceptButton = btnOk;
                picker.CancelButton = btnCancel;

                void UpdateInfo()
                {
                    if (cboKm.SelectedValue == null)
                        return;

                    DataRow[] rows = dt.Select($"MaKM = '{cboKm.SelectedValue}'");
                    if (rows.Length == 0)
                        return;

                    DataRow r = rows[0];
                    string loai = r["LoaiKhuyenMai"]?.ToString() ?? "";
                    string info = $"Loại: {loai}";

                    if (loai == "Giảm %")
                    {
                        string pt = r["PhanTramGiam"] != DBNull.Value ? Convert.ToDecimal(r["PhanTramGiam"]).ToString("0.##") : "0";
                        string max = r["GiaTriGiamToiDa"] != DBNull.Value ? Convert.ToDecimal(r["GiaTriGiamToiDa"]).ToString("N0") : "Không giới hạn";
                        info += $" | -{pt}% (tối đa {max})";
                    }
                    else
                    {
                        string soTien = r["SoTienGiam"] != DBNull.Value ? Convert.ToDecimal(r["SoTienGiam"]).ToString("N0") : "0";
                        info += $" | -{soTien} đ";
                    }

                    lblInfo.Text = info;
                }

                cboKm.SelectedIndexChanged += (s, e) =>
                {
                    chkNone.Checked = false;
                    UpdateInfo();
                };

                chkNone.CheckedChanged += (s, e) =>
                {
                    if (chkNone.Checked)
                    {
                        cboKm.Enabled = false;
                        lblInfo.Text = "Không áp dụng";
                    }
                    else
                    {
                        cboKm.Enabled = true;
                        UpdateInfo();
                    }
                };

                picker.Controls.Add(lblTitle);
                picker.Controls.Add(cboKm);
                picker.Controls.Add(chkNone);
                picker.Controls.Add(lblInfo);
                picker.Controls.Add(btnOk);
                picker.Controls.Add(btnCancel);

                UpdateInfo();

                DialogResult dr = picker.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    return false;
                }

                if (chkNone.Checked)
                {
                    KhuyenMaiDaChon = null;
                    SoTienGiamKhuyenMai = 0;
                    return true;
                }

                DataRow[] selectedRows = dt.Select($"MaKM = '{cboKm.SelectedValue}'");
                if (selectedRows.Length == 0)
                {
                    MessageBox.Show("Không xác định được khuyến mãi đã chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                DataRow row = selectedRows[0];
                KhuyenMaiDaChon = new KhuyenMaiDTO
                {
                    MaKM = row["MaKM"].ToString(),
                    TenKM = row["TenKM"].ToString(),
                    LoaiKhuyenMai = row["LoaiKhuyenMai"].ToString(),
                    PhanTramGiam = row["PhanTramGiam"] != DBNull.Value ? Convert.ToDecimal(row["PhanTramGiam"]) : (decimal?)null,
                    SoTienGiam = row["SoTienGiam"] != DBNull.Value ? Convert.ToDecimal(row["SoTienGiam"]) : (decimal?)null,
                    GiaTriGiamToiDa = row["GiaTriGiamToiDa"] != DBNull.Value ? Convert.ToDecimal(row["GiaTriGiamToiDa"]) : (decimal?)null,
                    LoaiApDung = row["LoaiApDung"].ToString()
                };

                string err;
                SoTienGiamKhuyenMai = khuyenMaiBLL.TinhGiaTriGiam(KhuyenMaiDaChon.MaKM, thanhTien, out err);

                if (!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show(err, "Khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn khuyến mãi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
