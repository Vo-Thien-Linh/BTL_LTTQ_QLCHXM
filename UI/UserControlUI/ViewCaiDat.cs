using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace UI.UserControlUI
{
    public partial class ViewCaiDat : UserControl
    {
        private readonly SettingsBLL settingsBLL;
        private readonly LanguageManagerBLL languageManager;
        private SettingsDTO originalSettings;
        private bool isLoading = false;

        public ViewCaiDat()
        {
            InitializeComponent();
            settingsBLL = new SettingsBLL();
            languageManager = LanguageManagerBLL.Instance;

            languageManager.InitResourceManagerFromUI(typeof(ViewCaiDat).Assembly);

            // Đăng ký event khi ngôn ngữ thay đổi
            languageManager.LanguageChanged += OnLanguageChanged;
        }

        private void ViewCaiDat_Load(object sender, EventArgs e)
        {
            // Load cài đặt hiện tại
            LoadSettings();
            ApplyLanguage();
            languageManager.InitResourceManagerFromUI(typeof(ViewCaiDat).Assembly);

        }
        private void ApplyLanguage()
        {
            // GroupBox Ngôn ngữ
            grpNgonNgu.Text = languageManager.GetString("Language");
            rdoTiengViet.Text = languageManager.GetString("Vietnamese");
            rdoEnglish.Text = languageManager.GetString("English");

            // GroupBox Giao diện
            grpGiaoDien.Text = languageManager.GetString("Interface");
            lblTheme.Text = languageManager.GetString("Theme");
            

            // ComboBox Theme
            UpdateThemeComboBox();

            // GroupBox Thông báo
            

            // Buttons
            btnLuu.Text = languageManager.GetString("ButtonSave");
            btnHuy.Text = languageManager.GetString("ButtonCancel");
            btnKhoiPhuc.Text = languageManager.GetString("ButtonRestore");
        }

        

        private void UpdateThemeComboBox()
        {
            isLoading = true;

            // Lưu key theme hiện tại
            string currentThemeKey = GetCurrentThemeKey();

            cboTheme.Items.Clear();
            cboTheme.Items.Add(languageManager.GetString("ThemeLight"));
            cboTheme.Items.Add(languageManager.GetString("ThemeDark"));
            cboTheme.Items.Add(languageManager.GetString("ThemeAuto"));

            SetThemeByKey(currentThemeKey);

            isLoading = false;
        }

        private string GetCurrentThemeKey()
        {
            int index = cboTheme.SelectedIndex;
            switch (index)
            {
                case 0: return "Light";
                case 1: return "Dark";
                case 2: return "Auto";
                default: return "Light";
            }
        }

        private void SetThemeByKey(string themeKey)
        {
            switch (themeKey)
            {
                case "Light":
                    cboTheme.SelectedIndex = 0;
                    break;
                case "Dark":
                    cboTheme.SelectedIndex = 1;
                    break;
                case "Auto":
                    cboTheme.SelectedIndex = 2;
                    break;
                default:
                    cboTheme.SelectedIndex = 0;
                    break;
            }
        }

        /// <summary>
        /// Event khi ngôn ngữ thay đổi
        /// </summary>
        private void OnLanguageChanged(object sender, EventArgs e)
        {
            ApplyLanguage();
        }

        private void LoadSettings()
        {
            try
            {
                isLoading = true;

                SettingsDTO settings = settingsBLL.GetSettings();
                DisplaySettings(settings);
                originalSettings = settings.Clone();

                isLoading = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{languageManager.GetString("LoadError")} {ex.Message}",
                    languageManager.GetString("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DisplaySettings(SettingsDTO settings)
        {
            // Ngôn ngữ
            rdoTiengViet.Checked = (settings.Language == "vi");
            rdoEnglish.Checked = (settings.Language == "en");

            // Giao diện
            string themeKey = ConvertThemeValueToKey(settings.Theme);
            SetThemeByKey(themeKey);

            

            

            // Áp dụng theme
            ApplyTheme(themeKey);
        }

        private string ConvertThemeValueToKey(string themeValue)
        {
            // Chuyển đổi từ "Sáng"/"Light" -> "Light"
            if (themeValue == "Sáng" || themeValue == "Light")
                return "Light";
            if (themeValue == "Tối" || themeValue == "Dark")
                return "Dark";
            if (themeValue == "Tự động" || themeValue == "Auto")
                return "Auto";
            return "Light";
        }

        private string ConvertThemeKeyToValue(string themeKey)
        {
            // Chuyển từ key -> value theo ngôn ngữ hiện tại
            switch (themeKey)
            {
                case "Light":
                    return languageManager.GetString("ThemeLight");
                case "Dark":
                    return languageManager.GetString("ThemeDark");
                case "Auto":
                    return languageManager.GetString("ThemeAuto");
                default:
                    return languageManager.GetString("ThemeLight");
            }
        }

        private string GetThemeKeyFromComboBox()
        {
            string txt = cboTheme.SelectedItem?.ToString();
            if (txt == languageManager.GetString("ThemeLight")) return "Light";
            if (txt == languageManager.GetString("ThemeDark")) return "Dark";
            if (txt == languageManager.GetString("ThemeAuto")) return "Auto";
            return "Light";
        }


        private SettingsDTO GetSettingsFromUI()
        {
            string themeKey = GetThemeKeyFromComboBox();
            return new SettingsDTO
            {
                Language = rdoTiengViet.Checked ? "vi" : "en",
                Theme = themeKey, // luôn là "Light", "Dark", hoặc "Auto"

            };
        }

        private void SaveSettings()
        {
            try
            {
                SettingsDTO settings = GetSettingsFromUI();

                if (!settingsBLL.HasAnyNotificationEnabled(settings))
                {
                    var result = MessageBox.Show(
                        languageManager.GetString("NotificationWarning"),
                        languageManager.GetString("Warning"),
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                        return;
                }

                // Kiểm tra ngôn ngữ có thay đổi không
                bool languageChanged = (settings.Language != originalSettings.Language);

                if (settingsBLL.SaveSettings(settings))
                {
                    MessageBox.Show(languageManager.GetString("SaveSuccess"),
                        languageManager.GetString("SaveSuccessTitle"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Nếu ngôn ngữ thay đổi, cập nhật ngay
                    if (languageChanged)
                    {
                        languageManager.SetLanguage(settings.Language);
                    }

                    originalSettings = settings.Clone();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{languageManager.GetString("SaveError")} {ex.Message}",
                    languageManager.GetString("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ApplyTheme(string theme)
        {
            switch (theme)
            {
                case "Light":
                    this.BackColor = Color.White;
                    UpdateControlColors(Color.Black);
                    break;
                case "Dark":
                    this.BackColor = Color.FromArgb(45, 45, 48);
                    UpdateControlColors(Color.White);
                    break;
                case "Auto":
                    ApplyAutoTheme();
                    break;
            }
        }

        private void UpdateControlColors(Color foreColor)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is GroupBox groupBox)
                {
                    groupBox.ForeColor = foreColor;
                    foreach (Control innerCtrl in groupBox.Controls)
                    {
                        if (innerCtrl is Label || innerCtrl is CheckBox || innerCtrl is RadioButton)
                        {
                            innerCtrl.ForeColor = foreColor;
                        }
                    }
                }
            }
        }

        private void ApplyAutoTheme()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    var value = key?.GetValue("AppsUseLightTheme");
                    if (value != null && (int)value == 0)
                    {
                        this.BackColor = Color.FromArgb(45, 45, 48);
                        UpdateControlColors(Color.White);
                    }
                    else
                    {
                        this.BackColor = Color.White;
                        UpdateControlColors(Color.Black);
                    }
                }
            }
            catch
            {
                this.BackColor = Color.White;
                UpdateControlColors(Color.Black);
            }
        }


        // Event Handlers
        

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SettingsDTO settings = GetSettingsFromUI();
            string oldLanguage = originalSettings.Language;
            bool languageChanged = (settings.Language != oldLanguage);

            try
            {
                if (!settingsBLL.HasAnyNotificationEnabled(settings))
                {
                    var result = MessageBox.Show(
                        languageManager.GetString("NotificationWarning"),
                        languageManager.GetString("Warning"),
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                        return;
                }

                if (settingsBLL.SaveSettings(settings))
                {
                    MessageBox.Show(languageManager.GetString("SaveSuccess"),
                        languageManager.GetString("SaveSuccessTitle"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Nếu ngôn ngữ thay đổi, cập nhật giao diện ngay
                    if (languageChanged)
                    {
                        languageManager.SetLanguage(settings.Language);
                        DisplaySettings(settings); // Cập nhật lại UI sau khi đổi ngôn ngữ
                    }
                    ThemeManager.Instance.SetTheme(settings.Theme);
                    originalSettings = settings.Clone();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{languageManager.GetString("SaveError")} {ex.Message}",
                    languageManager.GetString("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (originalSettings != null)
            {
                SettingsDTO currentSettings = GetSettingsFromUI();

                if (settingsBLL.IsSettingsChanged(currentSettings, originalSettings))
                {
                    DisplaySettings(originalSettings);
                    MessageBox.Show(languageManager.GetString("CancelConfirm"),
                        languageManager.GetString("SaveSuccessTitle"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                languageManager.GetString("RestoreConfirm"),
                languageManager.GetString("Confirm"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SettingsDTO defaultSettings = settingsBLL.RestoreDefaultSettings();
                    DisplaySettings(defaultSettings);

                    MessageBox.Show(languageManager.GetString("RestoreSuccess"),
                        languageManager.GetString("SaveSuccessTitle"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{languageManager.GetString("Error")} {ex.Message}",
                        languageManager.GetString("Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        
    }

    // Class lưu trữ cài đặt
    public class AppSettings
    {
        public string Language { get; set; }
        public string Theme { get; set; }
        public bool EnableAnimations { get; set; }
        public int FontSize { get; set; }
        public bool NotifyRegistrationExpiry { get; set; }
        public bool NotifyInsuranceExpiry { get; set; }
        public bool NotifyRentalExpiry { get; set; }
        public int NotificationDaysBefore { get; set; }
    }
}
