using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.FormUI
{
    public partial class FormQuanLiNV : Form
    {
        private NhanVienBLL nhanVienBLL;
        private string maNV;
        private bool isEditMode;
        private byte[] imageData;

        public FormQuanLiNV()
        {
            InitializeComponent();
            nhanVienBLL = new NhanVienBLL();
            isEditMode = false;
            InitializeForm();
            LoadComboBoxData();
            GenerateMaNV();
        }

        public FormQuanLiNV(string maNV)
        {
            InitializeComponent();
            nhanVienBLL = new NhanVienBLL();
            this.maNV = maNV;
            isEditMode = true;
            InitializeForm();
            LoadComboBoxData();
            LoadNhanVienData();
        }


        private void InitializeForm()
        {
            this.Text = isEditMode ? "Sửa Thông Tin Nhân Viên" : "Thêm Nhân Viên Mới";
            lblTitle.Text = isEditMode ? "SỬA THÔNG TIN NHÂN VIÊN" : "THÊM NHÂN VIÊN MỚI";
            txtMaNV.ReadOnly = isEditMode;
        }

        private void LoadComboBoxData()
        {
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.AddRange(new string[] { "Nam", "Nữ", "Khác" });
            cboGioiTinh.SelectedIndex = 0;

            cboChucVu.Items.Clear();
            cboChucVu.Items.AddRange(new string[] {
                "Quản lý",
                "Thu ngân",
                "Kỹ thuật",
                "Bán hàng",
                "Nhân viên"
            });
            cboChucVu.SelectedIndex = 4;

            cboTinhTrang.Items.Clear();
            cboTinhTrang.Items.AddRange(new string[] {
                "Thử việc",
                "Còn làm",
                "Nghỉ làm"
            });
            cboTinhTrang.SelectedIndex = 0;

            cboTrinhDo.Items.Clear();
            cboTrinhDo.Items.AddRange(new string[] {
                "THPT",
                "Trung cấp",
                "Cao đẳng",
                "Đại học",
                "Sau đại học"
            });
            cboTrinhDo.SelectedIndex = 0;
        }

        private void GenerateMaNV()
        {
            try
            {
                txtMaNV.Text = nhanVienBLL.GenerateMaNV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo mã nhân viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNhanVienData()
        {
            try
            {
                var dt = nhanVienBLL.GetNhanVienByMaNV(maNV);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    txtMaNV.Text = row["MaNV"].ToString();
                    txtHoTen.Text = row["HoTenNV"].ToString();
                    dtpNgaySinh.Value = Convert.ToDateTime(row["NgaySinh"]);
                    cboGioiTinh.SelectedItem = row["GioiTinh"].ToString();
                    txtSdt.Text = row["Sdt"] != DBNull.Value ? row["Sdt"].ToString() : "";
                    txtEmail.Text = row["Email"] != DBNull.Value ? row["Email"].ToString() : "";
                    txtDiaChi.Text = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : "";
                    cboChucVu.SelectedItem = row["ChucVu"] != DBNull.Value ? row["ChucVu"].ToString() : "";
                    numLuong.Value = row["LuongCoBan"] != DBNull.Value ? Convert.ToDecimal(row["LuongCoBan"]) : 0;
                    cboTinhTrang.SelectedItem = row["TinhTrangLamViec"].ToString();
                    txtCCCD.Text = row["CCCD"] != DBNull.Value ? row["CCCD"].ToString() : "";
                    cboTrinhDo.SelectedItem = row["TrinhDoHocVan"] != DBNull.Value ? row["TrinhDoHocVan"].ToString() : "";

                    if (row["AnhNhanVien"] != DBNull.Value)
                    {
                        byte[] imgData = (byte[])row["AnhNhanVien"];
                        using (MemoryStream ms = new MemoryStream(imgData))
                        {
                            picNhanVien.Image = Image.FromStream(ms);
                        }
                        imageData = imgData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Chọn ảnh nhân viên";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image img = Image.FromFile(ofd.FileName);
                        Image resizedImg = ResizeImage(img, 200, 200);
                        picNhanVien.Image = resizedImg;

                        using (MemoryStream ms = new MemoryStream())
                        {
                            resizedImg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            imageData = ms.ToArray();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi tải ảnh: " + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, width, height);
            }
            return result;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                NhanVien nv = new NhanVien
                {
                    MaNV = txtMaNV.Text.Trim(),
                    HoTenNV = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = cboGioiTinh.SelectedItem.ToString(),
                    Sdt = txtSdt.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    ChucVu = cboChucVu.SelectedItem?.ToString(),
                    LuongCoBan = numLuong.Value,
                    TinhTrangLamViec = cboTinhTrang.SelectedItem.ToString(),
                    CCCD = txtCCCD.Text.Trim(),
                    TrinhDoHocVan = cboTrinhDo.SelectedItem?.ToString(),
                    AnhNhanVien = imageData
                };

                string errorMessage;
                bool result;

                if (isEditMode)
                {
                    result = nhanVienBLL.UpdateNhanVien(nv, out errorMessage);
                }
                else
                {
                    result = nhanVienBLL.InsertNhanVien(nv, out errorMessage);
                }

                if (result)
                {
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
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (cboGioiTinh.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGioiTinh.Focus();
                return false;
            }

            if (cboTinhTrang.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn tình trạng làm việc!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTinhTrang.Focus();
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
