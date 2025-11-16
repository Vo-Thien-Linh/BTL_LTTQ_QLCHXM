using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;

namespace UI.FormUI
{
    public partial class FormDuyetDonHang : Form
    {
        private int maGiaoDich;
        private bool isBan; // true = Bán, false = Thuê
        private string currentMaNV;
        private GiaoDichBanBLL giaoDichBanBLL;
        private GiaoDichThueBLL giaoDichThueBLL;
        private DataTable dtGiaoDich;

        public FormDuyetDonHang(int maGD, bool isBan, string maNV)
        {
            InitializeComponent();
            this.maGiaoDich = maGD;
            this.isBan = isBan;
            this.currentMaNV = maNV;

            giaoDichBanBLL = new GiaoDichBanBLL();
            giaoDichThueBLL = new GiaoDichThueBLL();

            InitializeForm();
            LoadData();
        }

        private void InitializeForm()
        {
            this.Text = isBan ? "Chi Tiết Đơn Bán Hàng" : "Chi Tiết Đơn Thuê Xe";
            lblTitle.Text = isBan ? "CHI TIẾT ĐỠN BÁN HÀNG" : "CHI TIẾT ĐƠN THUÊ XE";

            // Ẩn/hiện các control phù hợp
            if (isBan)
            {
                lblNgayBatDau.Visible = false;
                dtpNgayBatDau.Visible = false;
                lblNgayKetThuc.Visible = false;
                dtpNgayKetThuc.Visible = false;
                lblSoNgayThue.Visible = false;
                txtSoNgayThue.Visible = false;
                lblGiaThueNgay.Visible = false;
                txtGiaThueNgay.Visible = false;
                lblSoTienCoc.Visible = false;
                txtSoTienCoc.Visible = false;
                lblGiayToGiuLai.Visible = false;
                txtGiayToGiuLai.Visible = false;
            }
            else
            {
                lblNgayBan.Visible = false;
                dtpNgayBan.Visible = false;
                lblGiaBan.Visible = false;
                txtGiaBan.Visible = false;
            }
        }

        private void LoadData()
        {
            try
            {
                if (isBan)
                {
                    dtGiaoDich = giaoDichBanBLL.GetGiaoDichBanByMa(maGiaoDich);
                }
                else
                {
                    DataTable dt = giaoDichThueBLL.GetAllGiaoDichThue();
                    dtGiaoDich = dt.Clone();
                    DataRow[] rows = dt.Select($"MaGDThue = {maGiaoDich}");
                    if (rows.Length > 0)
                    {
                        dtGiaoDich.ImportRow(rows[0]);
                    }
                }

                if (dtGiaoDich.Rows.Count > 0)
                {
                    DisplayData(dtGiaoDich.Rows[0]);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin giao dịch!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void DisplayData(DataRow row)
        {
            // Thông tin chung
            txtMaGiaoDich.Text = isBan ? row["MaGDBan"].ToString() : row["MaGDThue"].ToString();
            txtKhachHang.Text = row["HoTenKH"].ToString();
            txtSdtKhachHang.Text = row["SdtKhachHang"].ToString();

            if (row.Table.Columns.Contains("EmailKhachHang"))
                txtEmailKhachHang.Text = row["EmailKhachHang"]?.ToString();
            if (row.Table.Columns.Contains("DiaChiKhachHang"))
                txtDiaChiKhachHang.Text = row["DiaChiKhachHang"]?.ToString();

            txtXe.Text = row["TenXe"].ToString();
            txtBienSo.Text = row["BienSo"].ToString();
            txtTrangThaiDuyet.Text = row["TrangThaiDuyet"].ToString();
            txtTrangThaiThanhToan.Text = row["TrangThaiThanhToan"].ToString();
            txtHinhThucThanhToan.Text = row["HinhThucThanhToan"]?.ToString();

            if (row["NgayDuyet"] != DBNull.Value)
            {
                dtpNgayDuyet.Value = Convert.ToDateTime(row["NgayDuyet"]);
                dtpNgayDuyet.Enabled = false;
            }
            else
            {
                dtpNgayDuyet.Enabled = false;
            }

            if (row["NguoiDuyet"] != DBNull.Value)
            {
                txtNguoiDuyet.Text = row["TenNhanVien"]?.ToString();
            }

            txtGhiChu.Text = row["GhiChuDuyet"]?.ToString();

            // Thông tin riêng
            if (isBan)
            {
                dtpNgayBan.Value = Convert.ToDateTime(row["NgayBan"]);
                txtGiaBan.Text = Convert.ToDecimal(row["GiaBan"]).ToString("N0");
            }
            else
            {
                dtpNgayBatDau.Value = Convert.ToDateTime(row["NgayBatDau"]);
                dtpNgayKetThuc.Value = Convert.ToDateTime(row["NgayKetThuc"]);
                txtSoNgayThue.Text = row["SoNgayThue"].ToString();
                txtGiaThueNgay.Text = Convert.ToDecimal(row["GiaThueNgay"]).ToString("N0");
                txtTongGia.Text = Convert.ToDecimal(row["TongGia"]).ToString("N0");
                txtSoTienCoc.Text = row["SoTienCoc"] != DBNull.Value ?
                    Convert.ToDecimal(row["SoTienCoc"]).ToString("N0") : "0";
                txtGiayToGiuLai.Text = row["GiayToGiuLai"]?.ToString();
            }

            // Highlight trạng thái
            HighlightTrangThai(row["TrangThaiDuyet"].ToString());

            // Enable/Disable buttons
            string trangThai = row["TrangThaiDuyet"].ToString();
            bool isChoDuyet = trangThai == "Chờ duyệt";
            btnApprove.Enabled = isChoDuyet;
            btnReject.Enabled = isChoDuyet;
        }

        private void HighlightTrangThai(string trangThai)
        {
            if (trangThai == "Chờ duyệt")
            {
                txtTrangThaiDuyet.BackColor = Color.FromArgb(255, 243, 205);
                txtTrangThaiDuyet.ForeColor = Color.FromArgb(255, 152, 0);
            }
            else if (trangThai == "Đã duyệt")
            {
                txtTrangThaiDuyet.BackColor = Color.FromArgb(200, 230, 201);
                txtTrangThaiDuyet.ForeColor = Color.FromArgb(56, 142, 60);
            }
            else if (trangThai == "Từ chối")
            {
                txtTrangThaiDuyet.BackColor = Color.FromArgb(255, 205, 210);
                txtTrangThaiDuyet.ForeColor = Color.FromArgb(211, 47, 47);
            }
        }

        private void BtnApprove_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn duyệt đơn hàng này?\n\nSau khi duyệt, trạng thái xe sẽ được cập nhật tự động.",
                "Xác nhận duyệt",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    string errorMessage;
                    bool success;

                    if (isBan)
                    {
                        success = giaoDichBanBLL.ApproveGiaoDichBan(
                            maGiaoDich,
                            currentMaNV,
                            txtGhiChu.Text.Trim(),
                            out errorMessage
                        );
                    }
                    else
                    {
                        success = giaoDichThueBLL.ApproveGiaoDichThue(
                            maGiaoDich,
                            currentMaNV,
                            txtGhiChu.Text.Trim(),
                            out errorMessage
                        );
                    }

                    if (success)
                    {
                        MessageBox.Show("Duyệt đơn hàng thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi duyệt đơn hàng: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnReject_Click(object sender, EventArgs e)
        {
            using (FormNhapLyDo formLyDo = new FormNhapLyDo())
            {
                if (formLyDo.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string errorMessage;
                        bool success;

                        if (isBan)
                        {
                            success = giaoDichBanBLL.RejectGiaoDichBan(
                                maGiaoDich,
                                currentMaNV,
                                formLyDo.LyDo,
                                out errorMessage
                            );
                        }
                        else
                        {
                            success = giaoDichThueBLL.RejectGiaoDichThue(
                                maGiaoDich,
                                currentMaNV,
                                formLyDo.LyDo,
                                out errorMessage
                            );
                        }

                        if (success)
                        {
                            MessageBox.Show("Từ chối đơn hàng thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(errorMessage, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi từ chối đơn hàng: " + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}