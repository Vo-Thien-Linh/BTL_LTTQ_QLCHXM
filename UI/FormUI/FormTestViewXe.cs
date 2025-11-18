using System;
using System.Drawing;
using System.Windows.Forms;
using UI.UserControlUI;

namespace UI.FormUI
{
    public partial class FormTestViewXe : Form
    {
        public FormTestViewXe()
        {
            InitializeComponent();
        }

        private void FormTestViewXe_Load(object sender, EventArgs e)
        {
            // Thêm ViewMuaThueXe vào form
            ViewMuaThueXe viewMuaThueXe = new ViewMuaThueXe
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(viewMuaThueXe);
        }
    }
}