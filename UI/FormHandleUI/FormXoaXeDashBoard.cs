using BLL;
using DTO;
using Sunny.UI;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace UI.FormHandleUI
{
    public partial class FormXoaXeDashBoard : Form
    {
        private string idXe;
        private XeMayBLL xeMayBLL = new XeMayBLL();
        public bool IsDeleted { get; private set; }

        public FormXoaXeDashBoard(string idXe)
        {
            InitializeComponent();
            this.idXe = idXe;
            IsDeleted = false;
        }

        private void FormXoaXeDashBoard_Load(object sender, EventArgs e)
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

                // Hiển thị thông tin để xác nhận trước khi xóa
                txtMaXe.Text = xe.ID_Xe;
                txtBienSo.Text = xe.BienSo;
                txtHangXe.Text = xe.TenHang;
                txtDongXe.Text = xe.TenDong;
                txtMauSac.Text = xe.TenMau;
                txtNamSanXuat.Text = xe.NamSX?.ToString() ?? "";
                txtTrangThai.Text = xe.TrangThai;
                txtGiaMua.Text = xe.GiaMua.HasValue ? xe.GiaMua.Value.ToString("N0") + " VNĐ" : "";
                txtSoLuong.Text = xe.SoLuong?.ToString() ?? "1";
                txtMucDichSuDung.Text = xe.MucDichSuDung;

           
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin xe: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xe đã có giao dịch chưa
                if (xeMayBLL.KiemTraXeCoGiaoDich(idXe))
                {
                    // Xe có giao dịch → Soft delete
                    bool result = UIMessageBox.Show(
                        $"Xe {txtMaXe.Text} - {txtBienSo.Text} đã có giao dịch bán/thuê.\n\n" +
                        "Hệ thống sẽ chuyển xe sang trạng thái 'Đã xóa' thay vì xóa hẳn để giữ lại lịch sử giao dịch.\n\n" +
                        "Bạn có muốn tiếp tục?",
                        "Cảnh báo",
                        UIStyle.Orange,
                        UIMessageBoxButtons.OKCancel
                    );

                    if (result)
                    {
                        bool success = xeMayBLL.SoftDeleteXeMay(idXe);
                        if (success)
                        {
                            UIMessageBox.Show(
                                "Đã chuyển xe sang trạng thái 'Đã xóa' thành công!",
                                "Thành công",
                                UIStyle.Green,
                                UIMessageBoxButtons.OK
                            );
                            IsDeleted = true;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            UIMessageBox.Show(
                                "Không thể xóa xe!",
                                "Lỗi",
                                UIStyle.Red,
                                UIMessageBoxButtons.OK
                            );
                        }
                    }
                }
                else
                {
                    // Xe chưa có giao dịch → Có thể xóa hẳn
                    bool result = UIMessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa hoàn toàn xe {txtMaXe.Text} - {txtBienSo.Text}?\n\n" +
                        "Thao tác này không thể hoàn tác!",
                        "Xác nhận xóa",
                        UIStyle.Red,
                        UIMessageBoxButtons.OKCancel
                    );

                    if (result)
                    {
                        bool success = xeMayBLL.DeleteXeMay(idXe);
                        if (success)
                        {
                            UIMessageBox.Show(
                                "Xóa xe thành công!",
                                "Thành công",
                                UIStyle.Green,
                                UIMessageBoxButtons.OK
                            );
                            IsDeleted = true;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            UIMessageBox.Show(
                                "Xóa xe thất bại!",
                                "Lỗi",
                                UIStyle.Red,
                                UIMessageBoxButtons.OK
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.Show(
                    "Lỗi: " + ex.Message,
                    "Lỗi",
                    UIStyle.Red,
                    UIMessageBoxButtons.OK
                );
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
