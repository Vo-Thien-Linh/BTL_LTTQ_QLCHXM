using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;

namespace UI
{
    public partial class frmQuanLyBaoTri : Form
    {
        private BaoTriBLL baoTriBLL = new BaoTriBLL();
        private List<ChiTietBaoTriDTO> danhSachChiTiet = new List<ChiTietBaoTriDTO>();
        private int idBaoTriSelected = 0;

        private LanguageManagerBLL langMgr = LanguageManagerBLL.Instance;


        public frmQuanLyBaoTri()
        {
            InitializeComponent();

            langMgr.LanguageChanged += (s, e) => { ApplyLanguage(); };
            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            lblTitle.Text = langMgr.GetString("MaintenanceTitle");
            grpThongTinBaoTri.Text = langMgr.GetString("InfoTitle");
            lblXe.Text = langMgr.GetString("VehicleLabel");
            lblNhanVien.Text = langMgr.GetString("TechnicianLabel");
            lblGhiChu.Text = langMgr.GetString("NoteLabel");
            grpThemPhuTung.Text = langMgr.GetString("AddPartTitle");
            lblPhuTung.Text = langMgr.GetString("PartLabel");
            lblSoLuong.Text = langMgr.GetString("QuantityLabel");
            btnThemPhuTung.Text = langMgr.GetString("AddBtn");
            grpChiTiet.Text = langMgr.GetString("DetailTitle");
            lblTongTienText.Text = langMgr.GetString("TotalCost");
            btnThem.Text = langMgr.GetString("CreateBtn");
            btnXoa.Text = langMgr.GetString("DeleteBtn");
            btnLamMoi.Text = langMgr.GetString("ResetBtn");
            grpDanhSach.Text = langMgr.GetString("MaintenanceListTitle");

            if (dgvChiTietBaoTri.Columns.Count > 0)
            {
                dgvChiTietBaoTri.Columns["MaPhuTung"].HeaderText = langMgr.GetString("PartCodeColumn");
                dgvChiTietBaoTri.Columns["TenPhuTung"].HeaderText = langMgr.GetString("PartNameColumn");
                dgvChiTietBaoTri.Columns["DonViTinh"].HeaderText = langMgr.GetString("UnitColumn");
                dgvChiTietBaoTri.Columns["SoLuong"].HeaderText = langMgr.GetString("QtyColumn");
                dgvChiTietBaoTri.Columns["GiaSuDung"].HeaderText = langMgr.GetString("UsagePriceColumn");
                dgvChiTietBaoTri.Columns["ThanhTien"].HeaderText = langMgr.GetString("AmountColumn");
                dgvChiTietBaoTri.Columns["GhiChu"].HeaderText = langMgr.GetString("PartNoteColumn");
                dgvChiTietBaoTri.Columns["btnXoa"].HeaderText = langMgr.GetString("RemoveBtnColumn");
            }
        }



        private void frmQuanLyBaoTri_Load(object sender, EventArgs e)
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
                
                if (dt.Rows.Count > 0)
                {
                    cboXe.DataSource = dt;
                    cboXe.DisplayMember = "DisplayText";  // Hiển thị đầy đủ thông tin
                    cboXe.ValueMember = "ID_Xe";
                    cboXe.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Không có xe nào có thể bảo trì!\nVui lòng thêm xe vào hệ thống.", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboXe.DataSource = null;
                }
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
                
                if (dt.Rows.Count > 0)
                {
                    cboNhanVien.DataSource = dt;
                    cboNhanVien.DisplayMember = "HoTenNV";
                    cboNhanVien.ValueMember = "MaTaiKhoan";
                    cboNhanVien.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Không có nhân viên kỹ thuật nào!\nVui lòng thêm nhân viên kỹ thuật vào hệ thống.", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNhanVien.DataSource = null;
                }
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
                
                if (dt.Rows.Count > 0)
                {
                    cboPhuTung.DataSource = dt;
                    cboPhuTung.DisplayMember = "TenPhuTung";
                    cboPhuTung.ValueMember = "MaPhuTung";
                    cboPhuTung.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Không có phụ tùng nào trong kho!\nVui lòng nhập phụ tùng vào kho.", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboPhuTung.DataSource = null;
                }
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

                // Thêm vào database
                if (baoTriBLL.ThemBaoTri(baoTri, chiTietList))
                {
                    MessageBox.Show("Thêm bảo trì thành công!\n" +
                        "Phiếu bảo trì mới đã được tạo riêng biệt.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Đảm bảo reset form trước khi load lại dữ liệu
                    ResetForm();
                    LoadDanhSachBaoTri();
                    LoadComboBoxPhuTung(); // Cập nhật tồn kho
                    LoadComboBoxXe(); // Cập nhật danh sách xe
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm bảo trì: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa bảo trì
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (idBaoTriSelected == 0)
                {
                    MessageBox.Show("Vui lòng chọn bảo trì cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc muốn xóa bảo trì này?\nPhụ tùng sẽ được hoàn trả về kho.",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
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
                        {
                            cboNhanVien.SelectedValue = baoTri.MaTaiKhoan;
                        }
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
            
            // Clear tất cả ComboBox
            if (cboXe.DataSource != null)
                cboXe.SelectedIndex = -1;
            if (cboNhanVien.DataSource != null)
                cboNhanVien.SelectedIndex = -1;
            if (cboPhuTung.DataSource != null)
                cboPhuTung.SelectedIndex = -1;
            
            // Clear text boxes
            txtGhiChu.Clear();
            txtGhiChuPhuTung.Clear();
            
            // Reset numeric
            nudSoLuong.Value = 1;
            
            // Clear DataGridView hoàn toàn
            dgvChiTietBaoTri.Rows.Clear();
            
            // Reset tổng tiền
            lblTongTien.Text = "0 VNĐ";
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            LoadDanhSachBaoTri();
        }

        private void grpChiTiet_Enter(object sender, EventArgs e)
        {

        }

        private void lblTongTienText_Click(object sender, EventArgs e)
        {

        }
    }
}