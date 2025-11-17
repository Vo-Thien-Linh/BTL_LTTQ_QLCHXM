using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.UserControlUI
{
    public partial class ViewQuanLySanPham : UserControl
    {
        public ViewQuanLySanPham()
        {
            InitializeComponent();
        }

        private void ViewQuanLySanPham_Load(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnQuanLyXe_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();

            ViewQuanLyXe viewQuanLyXe = new ViewQuanLyXe();

            viewQuanLyXe.Dock = DockStyle.Fill;

            pnlMain.Controls.Add(viewQuanLyXe);

            viewQuanLyXe.BringToFront();
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnQuanLyPhuTung_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();

            ViewQuanLyPhuTung viewQuanLyPhuTung = new ViewQuanLyPhuTung();

            viewQuanLyPhuTung.Dock = DockStyle.Fill;

            pnlMain.Controls.Add(viewQuanLyPhuTung);

            viewQuanLyPhuTung.BringToFront();
        }
    }
}
