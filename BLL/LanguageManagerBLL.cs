using DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL
{
    public class LanguageManagerBLL
    {

        private static LanguageManagerBLL instance;
        private ResourceManager resourceManager;
        private CultureInfo currentCulture;
        private readonly SettingsBLL settingsBLL;

        public event EventHandler LanguageChanged;

        private LanguageManagerBLL()
        {
            settingsBLL = new SettingsBLL();
            // Khởi tạo ResourceManager
            resourceManager = new ResourceManager(
                "UI.Resources.Lang.AppStrings",
                typeof(LanguageManagerBLL).Assembly);

            // Load ngôn ngữ từ settings
            LoadLanguageFromSettings();
        }

        public void InitResourceManagerFromUI(System.Reflection.Assembly asm)
        {
            resourceManager = new ResourceManager(
                "UI.Resources.Lang.AppStrings", // thay bằng đúng namespace resource của bạn
                asm);
        }
        public static LanguageManagerBLL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LanguageManagerBLL();
                }
                return instance;
            }
        }

        /// <summary>
        /// Load ngôn ngữ từ settings đã lưu
        /// </summary>
        private void LoadLanguageFromSettings()
        {
            try
            {
                SettingsDTO settings = settingsBLL.GetSettings();
                string languageCode = settings.Language;

                if (languageCode == "en")
                {
                    SetLanguage("en");
                }
                else
                {
                    SetLanguage("vi");
                }
            }
            catch
            {
                // Mặc định tiếng Việt
                SetLanguage("vi");
            }
        }

        /// <summary>
        /// Đặt ngôn ngữ cho ứng dụng
        /// </summary>
        public void SetLanguage(string languageCode)
        {
            try
            {
                CultureInfo newCulture;

                if (languageCode == "en")
                {
                    newCulture = new CultureInfo("en-US");
                }
                else
                {
                    newCulture = new CultureInfo("vi-VN");
                }

                currentCulture = newCulture;
                Thread.CurrentThread.CurrentUICulture = newCulture;
                Thread.CurrentThread.CurrentCulture = newCulture;

                // Trigger event để các form cập nhật
                OnLanguageChanged();
            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể thay đổi ngôn ngữ: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy chuỗi theo ngôn ngữ hiện tại
        /// </summary>
        public string GetString(string key)
        {
            try
            {
                string value = resourceManager.GetString(key, currentCulture);
                return value ?? key; // Trả về key nếu không tìm thấy
            }
            catch
            {
                return key;
            }
        }

        /// <summary>
        /// Lấy chuỗi theo ngôn ngữ cụ thể
        /// </summary>
        public string GetString(string key, string languageCode)
        {
            try
            {
                CultureInfo culture = languageCode == "en"
                    ? new CultureInfo("en-US")
                    : new CultureInfo("vi-VN");

                string value = resourceManager.GetString(key, culture);
                return value ?? key;
            }
            catch
            {
                return key;
            }
        }

        /// <summary>
        /// Lấy ngôn ngữ hiện tại
        /// </summary>
        public string GetCurrentLanguage()
        {
            return currentCulture.TwoLetterISOLanguageName == "en" ? "en" : "vi";
        }

        /// <summary>
        /// Kiểm tra có phải tiếng Việt không
        /// </summary>
        public bool IsVietnamese()
        {
            return GetCurrentLanguage() == "vi";
        }

        protected virtual void OnLanguageChanged()
        {
            LanguageChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
