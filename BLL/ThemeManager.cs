using System;
using System.Collections.Generic;
using System.Drawing;  // ✅ QUAN TRỌNG
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;  // ✅ QUAN TRỌNG

namespace BLL
{
    public class ThemeManager
    {
        private static ThemeManager _instance;
        private string _currentTheme = "Light";

        // Singleton instance
        public static ThemeManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ThemeManager();
                return _instance;
            }
        }

        // Theme hiện tại
        public string CurrentTheme
        {
            get { return _currentTheme; }
        }

        // Event khi theme thay đổi
        public event EventHandler ThemeChanged;

        // Private constructor để đảm bảo singleton
        private ThemeManager()
        {
            // Có thể load theme từ settings khi khởi tạo
        }

        /// <summary>
        /// Đặt theme mới
        /// </summary>
        public void SetTheme(string themeKey)
        {
            if (string.IsNullOrEmpty(themeKey))
                themeKey = "Light";

            // Validate theme
            if (themeKey != "Light" && themeKey != "Dark" && themeKey != "Auto")
            {
                System.Diagnostics.Debug.WriteLine($"Theme không hợp lệ: {themeKey}, sử dụng Light");
                themeKey = "Light";
            }

            if (_currentTheme != themeKey)
            {
                _currentTheme = themeKey;

                System.Diagnostics.Debug.WriteLine($"Theme đã đổi sang: {_currentTheme}");

                // Trigger event để các form tự động cập nhật
                ThemeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Áp dụng theme cho một control cụ thể
        /// </summary>
        public void ApplyTheme(Control control)
        {
            if (control == null) return;

            switch (_currentTheme)
            {
                case "Light":
                    ApplyLightTheme(control);
                    break;
                case "Dark":
                    ApplyDarkTheme(control);
                    break;
                case "Auto":
                    ApplyAutoTheme(control);
                    break;
            }
        }

        /// <summary>
        /// Áp dụng Light theme
        /// </summary>
        private void ApplyLightTheme(Control control)
        {
            control.BackColor = Color.White;
            control.ForeColor = Color.Black;
            UpdateChildControls(control, Color.White, Color.Black);
        }

        /// <summary>
        /// Áp dụng Dark theme
        /// </summary>
        private void ApplyDarkTheme(Control control)
        {
            control.BackColor = Color.FromArgb(45, 45, 48);
            control.ForeColor = Color.White;
            UpdateChildControls(control, Color.FromArgb(45, 45, 48), Color.White);
        }

        /// <summary>
        /// Áp dụng Auto theme (theo Windows)
        /// </summary>
        private void ApplyAutoTheme(Control control)
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    var value = key?.GetValue("AppsUseLightTheme");

                    if (value != null && (int)value == 0)
                    {
                        // Windows đang dùng Dark mode
                        ApplyDarkTheme(control);
                    }
                    else
                    {
                        // Windows đang dùng Light mode
                        ApplyLightTheme(control);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi đọc theme Windows: {ex.Message}");
                // Fallback về Light theme nếu có lỗi
                ApplyLightTheme(control);
            }
        }

        /// <summary>
        /// Cập nhật màu cho tất cả control con
        /// </summary>
        private void UpdateChildControls(Control parent, Color backColor, Color foreColor)
        {
            foreach (Control ctrl in parent.Controls)
            {
                // GroupBox
                if (ctrl is GroupBox groupBox)
                {
                    groupBox.ForeColor = foreColor;
                    UpdateChildControls(groupBox, backColor, foreColor);
                }
                // Panel
                else if (ctrl is Panel panel)
                {
                    // Không đổi màu cho panel có màu đặc biệt
                    if (panel.BackColor == Color.White ||
                        panel.BackColor == Color.FromArgb(45, 45, 48) ||
                        panel.BackColor == Color.FromArgb(245, 247, 250))
                    {
                        panel.BackColor = backColor;
                    }
                    panel.ForeColor = foreColor;
                    UpdateChildControls(panel, backColor, foreColor);
                }
                // FlowLayoutPanel
                else if (ctrl is FlowLayoutPanel flowPanel)
                {
                    if (flowPanel.BackColor == Color.White ||
                        flowPanel.BackColor == Color.FromArgb(45, 45, 48) ||
                        flowPanel.BackColor == Color.FromArgb(245, 247, 250))
                    {
                        flowPanel.BackColor = backColor;
                    }
                    UpdateChildControls(flowPanel, backColor, foreColor);
                }
                // Label
                else if (ctrl is Label label)
                {
                    label.ForeColor = foreColor;
                }
                // CheckBox
                else if (ctrl is CheckBox checkBox)
                {
                    checkBox.ForeColor = foreColor;
                }
                // RadioButton
                else if (ctrl is RadioButton radioButton)
                {
                    radioButton.ForeColor = foreColor;
                }
                // ComboBox
                else if (ctrl is ComboBox comboBox)
                {
                    comboBox.BackColor = backColor;
                    comboBox.ForeColor = foreColor;
                }
                // TextBox
                else if (ctrl is TextBox textBox)
                {
                    textBox.BackColor = backColor;
                    textBox.ForeColor = foreColor;
                }
                // DataGridView
                else if (ctrl is DataGridView dgv)
                {
                    dgv.BackgroundColor = backColor;
                    dgv.DefaultCellStyle.BackColor = backColor;
                    dgv.DefaultCellStyle.ForeColor = foreColor;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = backColor;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = foreColor;
                }
                // NumericUpDown
                else if (ctrl is NumericUpDown numericUpDown)
                {
                    numericUpDown.BackColor = backColor;
                    numericUpDown.ForeColor = foreColor;
                }
                // DateTimePicker
                else if (ctrl is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.BackColor = backColor;
                    dateTimePicker.ForeColor = foreColor;
                }

                // Recursive cho control có children
                if (ctrl.HasChildren)
                {
                    UpdateChildControls(ctrl, backColor, foreColor);
                }
            }
        }

        /// <summary>
        /// Lấy màu nền theo theme hiện tại
        /// </summary>
        public Color GetBackgroundColor()
        {
            switch (_currentTheme)
            {
                case "Dark":
                    return Color.FromArgb(45, 45, 48);
                case "Auto":
                    return IsWindowsInDarkMode() ? Color.FromArgb(45, 45, 48) : Color.White;
                default:
                    return Color.White;
            }
        }

        /// <summary>
        /// Lấy màu chữ theo theme hiện tại
        /// </summary>
        public Color GetForegroundColor()
        {
            switch (_currentTheme)
            {
                case "Dark":
                    return Color.White;
                case "Auto":
                    return IsWindowsInDarkMode() ? Color.White : Color.Black;
                default:
                    return Color.Black;
            }
        }

        /// <summary>
        /// Kiểm tra Windows có đang dùng Dark mode không
        /// </summary>
        private bool IsWindowsInDarkMode()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    var value = key?.GetValue("AppsUseLightTheme");
                    return value != null && (int)value == 0;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy màu accent theo theme
        /// </summary>
        public Color GetAccentColor()
        {
            switch (_currentTheme)
            {
                case "Dark":
                    return Color.FromArgb(0, 122, 204);
                default:
                    return Color.FromArgb(0, 120, 215);
            }
        }
    }
}