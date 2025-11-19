using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.FormUI;


namespace BTL_LTTQ_QLCHXM
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());

            //Application.Run(new MainForm());
            //Application.Run(new FormTestViewXe());
        }


    }
}
