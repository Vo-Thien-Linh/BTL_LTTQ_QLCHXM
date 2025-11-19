using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ThemeManager
    {
        private static ThemeManager _instance;
        public static ThemeManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ThemeManager();
                return _instance;
            }
        }


        public string CurrentTheme { get; private set; } = "Light"; // hoặc giá trị ban đầu
        public event EventHandler ThemeChanged;

        public void SetTheme(string themeKey)
        {
            if (CurrentTheme != themeKey)
            {
                CurrentTheme = themeKey;
                ThemeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
