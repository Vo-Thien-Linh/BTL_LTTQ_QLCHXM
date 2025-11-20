using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;

namespace UI.UserControlUI
{
    public partial class ViewQuanLyBaoTri : UserControl
    {
        private readonly BaoTriBLL baoTriBLL = new BaoTriBLL();
        private readonly List<ChiTietBaoTriDTO> danhSachChiTiet = new List<ChiTietBaoTriDTO>();
        private int idBaoTriSelected = 0;

        public ViewQuanLyBaoTri()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Kiểm tra quyền truy cập
        /// </summary>
        private bool CheckPermission(string action)
        {
            try
            {
                if (!SessionManager.IsLoggedIn)
                {
                    MessageBox.Show("Bạn chưa đăng nhập!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (SessionManager.IsSessionExpired())
                {
                    MessageBox.Show("Phiên làm việc đã hết hạn!\nVui lòng đăng nhập lại.",
                        "Hết phiên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SessionManager.Logout();
                    return false;
                }

                SessionManager.UpdateActivity();

                if (!SessionManager.HasPermission("BaoTri", action))
                {
                    MessageBox.Show(
                        $"Bạn không có quyền {action} bảo trì!\n" +
                        $"Chức vụ: {SessionManager.CurrentUser?.ChucVu}",
                        "Không đủ quyền",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kiểm tra quyền: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ViewQuanLyBaoTri_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachBaoTri();
                LoadComboBoxXe();
                LoadComboBoxNhanVien();
                LoadComboBoxPhuTung();
                SetupDataGridView();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load danh sách bảo trì
        private void LoadDanhSachBaoTri()
        {
            try
            {
                DataTable dt = baoTriBLL.LayDanhSachBaoTri();
                dgvDanhSachBaoTri.DataSource = dt;

                // Định dạng DataGridView
                dgvDanhSachBaoTri.Columns["ID_BaoTri"].HeaderText = "Mã BT";
                dgvDanhSachBaoTri.Columns["ID_Xe"].HeaderText = "Mã Xe";
                dgvDanhSachBaoTri.Columns["BienSo"].HeaderText = "Biển Số";
                dgvDanhSachBaoTri.Columns["TenXe"].HeaderText = "Tên Xe";
                dgvDanhSachBaoTri.Columns["TenNhanVien"].HeaderText = "Nhân Viên";
                dgvDanhSachBaoTri.Columns["GhiChuBaoTri"].HeaderText = "Ghi Chú";
                dgvDanhSachBaoTri.Columns["TongChiPhi"].HeaderText = "Tổng Chi Phí";
                dgvDanhSachBaoTri.Columns["TongChiPhi"].DefaultCellStyle.Format = "N0";

                dgvDanhSachBaoTri.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi load danh sách bảo trì: " + ex.Message);
            }
        }

        // Setup DataGridView chi tiết
        private void SetupDataGridView()
        {
            dgvChiTietBaoTri.Columns.Clear();
            dgvChiTietBaoTri.Columns.Add("MaPhuTung", "Mã PT");
            dgvChiTietBaoTri.Columns.Add("TenPhuTung", "Tên Phụ Tùng");
            dgvChiTietBaoTri.Columns.Add("DonViTinh", "ĐVT");
            dgvChiTietBaoTri.Columns.Add("SoLuong", "Số Lượng");
            dgvChiTietBaoTri.Columns.Add("GiaSuDung", "Đơn Giá");
            dgvChiTietBaoTri.Columns.Add("ThanhTien", "Thành Tiền");
            dgvChiTietBaoTri.Columns.Add("GhiChu", "Ghi Chú");

            // Thêm nút xóa
            DataGridViewButtonColumn btnXoa = new DataGridViewButtonColumn();
            btnXoa.Name = "btnXoa";
            btnXoa.HeaderText = "Xóa";
            btnXoa.Text = "Xóa";
            btnXoa.UseColumnTextForButtonValue = true;
            dgvChiTietBaoTri.Columns.Add(btnXoa);

            // Định dạng
            dgvChiTietBaoTri.Columns["GiaSuDung"].DefaultCellStyle.Format = "N0";
            dgvChiTietBaoTri.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            dgvChiTietBaoTri.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Load ComboBox Xe
        private void LoadComboBoxXe()
        {
            try
            {
                DataTable dt = baoTriBLL.LayDanhSachXe();
                cboXe.DataSource = dt;
                cboXe.DisplayMember = "BienSo";
                cboXe.ValueMember = "ID_Xe";
                cboXe.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi load danh sách xe: " + ex.Message);
            }
        }

        // Load ComboBox Nhân viên
        private void LoadComboBoxNhanVien()
        {
            try
            {
                DataTable dt = baoTriBLL.LayDanhSachNhanVienKyThuat();
                cboNhanVien.DataSource = dt;
                cboNhanVien.DisplayMember = "HoTenNV";
                cboNhanVien.ValueMember = "MaTaiKhoan";
                cboNhanVien.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi load danh sách nhân viên: " + ex.Message);
            }
        }

        // Load ComboBox Phụ tùng
        private void LoadComboBoxPhuTung()
        {
            try
            {
                DataTable dt = baoTriBLL.LayDanhSachPhuTung();
                cboPhuTung.DataSource = dt;
                cboPhuTung.DisplayMember = "TenPhuTung";
                cboPhuTung.ValueMember = "MaPhuTung";
                cboPhuTung.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi load danh sách phụ tùng: " + ex.Message);
            }
        }

        // Thêm phụ tùng vào danh sách
        private void btnThemPhuTung_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboPhuTung.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn phụ tùng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (nudSoLuong.Value <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy thông tin phụ tùng
                DataRowView row = (DataRowView)cboPhuTung.SelectedItem;
                string maPhuTung = row["MaPhuTung"].ToString();
                string tenPhuTung = row["TenPhuTung"].ToString();
                decimal giaBan = Convert.ToDecimal(row["GiaBan"]);
                string donViTinh = row["DonViTinh"].ToString();
                int soLuongTon = Convert.ToInt32(row["SoLuongTon"]);

                // Kiểm tra tồn kho
                if (nudSoLuong.Value > soLuongTon)
                {
                    MessageBox.Show($"Số lượng tồn kho chỉ còn {soLuongTon} {donViTinh}!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra trùng
                bool daTonTai = false;
                foreach (DataGridViewRow dgvRow in dgvChiTietBaoTri.Rows)
                {
                    if (dgvRow.Cells["MaPhuTung"].Value.ToString() == maPhuTung)
                    {
                        dgvRow.Cells["SoLuong"].Value =
                            Convert.ToInt32(dgvRow.Cells["SoLuong"].Value) + (int)nudSoLuong.Value;
                        dgvRow.Cells["ThanhTien"].Value =
                            Convert.ToInt32(dgvRow.Cells["SoLuong"].Value) * giaBan;
                        daTonTai = true;
                        break;
                    }
                }

                if (!daTonTai)
                {
                    // Thêm vào DataGridView
                    dgvChiTietBaoTri.Rows.Add(
                        maPhuTung,
                        tenPhuTung,
                        donViTinh,
                        (int)nudSoLuong.Value,
                        giaBan,
                        (int)nudSoLuong.Value * giaBan,
                        txtGhiChuPhuTung.Text
                    );
                }

                // Cập nhật tổng tiền
                CapNhatTongTien();

                // Reset
                cboPhuTung.SelectedIndex = -1;
                nudSoLuong.Value = 1;
                txtGhiChuPhuTung.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm phụ tùng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa phụ tùng khỏi danh sách
        private void dgvChiTietBaoTri_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgvChiTietBaoTri.Columns["btnXoa"].Index && e.RowIndex >= 0)
                {
                    if (MessageBox.Show("Bạn có chắc muốn xóa phụ tùng này?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvChiTietBaoTri.Rows.RemoveAt(e.RowIndex);
                        CapNhatTongTien();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa phụ tùng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cập nhật tổng tiền
        private void CapNhatTongTien()
        {
            decimal tongTien = 0;
            foreach (DataGridViewRow row in dgvChiTietBaoTri.Rows)
            {
                if (row.Cells["ThanhTien"].Value != null)
                {
                    tongTien += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                }
            }
            lblTongTien.Text = tongTien.ToString("N0") + " VNĐ";
        }

        // Thêm bảo trì
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!CheckPermission("ADD"))
                return;

            try
            {
                // Validate
                if (cboXe.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn xe cần bảo trì!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dgvChiTietBaoTri.Rows.Count == 0)
                {
                    MessageBox.Show("Vui lòng thêm ít nhất một phụ tùng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng bảo trì
                BaoTriDTO baoTri = new BaoTriDTO
                {
                    ID_Xe = cboXe.SelectedValue.ToString(),
                    MaTaiKhoan = cboNhanVien.SelectedValue?.ToString(),
                    GhiChuBaoTri = txtGhiChu.Text
                };

                // Tạo danh sách chi tiết
                List<ChiTietBaoTriDTO> chiTietList = new List<ChiTietBaoTriDTO>();
                foreach (DataGridViewRow row in dgvChiTietBaoTri.Rows)
                {
                    ChiTietBaoTriDTO chiTiet = new ChiTietBaoTriDTO
                    {
                        MaPhuTung = row.Cells["MaPhuTung"].Value.ToString(),
                        SoLuong = Convert.ToInt32(row.Cells["SoLuong"].Value),
                        GiaSuDung = Convert.ToDecimal(row.Cells["GiaSuDung"].Value),
                        GhiChu = row.Cells["GhiChu"].Value?.ToString()
                    };
                    chiTietList.Add(chiTiet);
                }

                Cursor = Cursors.WaitCursor;

                // Thêm vào database
                if (baoTriBLL.ThemBaoTri(baoTri, chiTietList))
                {
                    MessageBox.Show("Thêm bảo trì thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachBaoTri();
                    LoadComboBoxPhuTung(); // Cập nhật tồn kho
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm bảo trì: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // Xóa bảo trì
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!CheckPermission("DELETE"))
                return;

            if (idBaoTriSelected == 0)
            {
                MessageBox.Show("Vui lòng chọn bảo trì cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kiểm tra ràng buộc nghiệp vụ
                string errorMessage;
                if (!baoTriBLL.CanDeleteBaoTri(idBaoTriSelected, out errorMessage))
                {
                    MessageBox.Show(
                        $"Không thể xóa bảo trì!\n\n{errorMessage}",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (MessageBox.Show(
                    "⚠ XÁC NHẬN XÓA BẢO TRÌ\n\n" +
                    "Bạn có chắc muốn xóa bảo trì này?\n" +
                    "Phụ tùng sẽ được hoàn trả về kho.\n\n" +
                    "Thao tác này KHÔNG THỂ HOÀN TÁC!",
                    "Xác nhận", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;

                    if (baoTriBLL.XoaBaoTri(idBaoTriSelected))
                    {
                        MessageBox.Show("Xóa bảo trì thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSachBaoTri();
                        LoadComboBoxPhuTung();
                        ResetForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa bảo trì: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // Chọn bảo trì để xem chi tiết
        private void dgvDanhSachBaoTri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvDanhSachBaoTri.Rows[e.RowIndex];
                    idBaoTriSelected = Convert.ToInt32(row.Cells["ID_BaoTri"].Value);

                    // Load thông tin bảo trì
                    BaoTriDTO baoTri = baoTriBLL.LayBaoTriTheoID(idBaoTriSelected);
                    if (baoTri != null)
                    {
                        cboXe.SelectedValue = baoTri.ID_Xe;
                        if (!string.IsNullOrEmpty(baoTri.MaTaiKhoan))
                            cboNhanVien.SelectedValue = baoTri.MaTaiKhoan;

                        txtGhiChu.Text = baoTri.GhiChuBaoTri;

                        // Load chi tiết
                        LoadChiTietBaoTri(idBaoTriSelected);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load thông tin: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load chi tiết bảo trì
        private void LoadChiTietBaoTri(int idBaoTri)
        {
            try
            {
                dgvChiTietBaoTri.Rows.Clear();
                List<ChiTietBaoTriDTO> chiTietList = baoTriBLL.LayChiTietBaoTri(idBaoTri);

                foreach (var chiTiet in chiTietList)
                {
                    dgvChiTietBaoTri.Rows.Add(
                        chiTiet.MaPhuTung,
                        chiTiet.TenPhuTung,
                        chiTiet.DonViTinh,
                        chiTiet.SoLuong,
                        chiTiet.GiaSuDung,
                        chiTiet.ThanhTien,
                        chiTiet.GhiChu
                    );
                }

                CapNhatTongTien();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi load chi tiết: " + ex.Message);
            }
        }

        // Tìm kiếm
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTimKiem.Text))
                {
                    LoadDanhSachBaoTri();
                }
                else
                {
                    DataTable dt = baoTriBLL.TimKiemBaoTri(txtTimKiem.Text);
                    dgvDanhSachBaoTri.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Reset form
        private void ResetForm()
        {
            idBaoTriSelected = 0;
            cboXe.SelectedIndex = -1;
            cboNhanVien.SelectedIndex = -1;
            cboPhuTung.SelectedIndex = -1;
            txtGhiChu.Text = "";
            txtGhiChuPhuTung.Text = "";
            nudSoLuong.Value = 1;
            dgvChiTietBaoTri.Rows.Clear();
            lblTongTien.Text = "0 VNĐ";
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            LoadDanhSachBaoTri();
        }
    }
}