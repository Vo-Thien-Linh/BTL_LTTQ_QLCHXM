using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SettingsBLL
    {
        private readonly SettingsDAL settingsDAL;

        public SettingsBLL()
        {
            settingsDAL = new SettingsDAL();
        }

        /// <summary>
        /// Lấy settings hiện tại
        /// </summary>
        public SettingsDTO GetSettings()
        {
            try
            {
                return settingsDAL.LoadSettings();
            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể tải cài đặt: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lưu settings với validation
        /// </summary>
        public bool SaveSettings(SettingsDTO settings)
        {
            try
            {
                // Validate dữ liệu
                if (!ValidateSettings(settings))
                {
                    throw new Exception("Dữ liệu cài đặt không hợp lệ");
                }

                // Backup trước khi lưu
                settingsDAL.BackupSettings();

                // Lưu settings
                return settingsDAL.SaveSettings(settings);
            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể lưu cài đặt: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Khôi phục cài đặt mặc định
        /// </summary>
        public SettingsDTO RestoreDefaultSettings()
        {
            try
            {
                // Backup trước khi xóa
                settingsDAL.BackupSettings();

                // Xóa file cài đặt
                settingsDAL.DeleteSettings();

                // Trả về settings mặc định
                return new SettingsDTO();
            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể khôi phục cài đặt: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Validate settings
        /// </summary>
        private bool ValidateSettings(SettingsDTO settings)
        {
            if (settings == null)
                return false;

            // Validate Language
            if (string.IsNullOrEmpty(settings.Language))
                return false;

            if (settings.Language != "vi" && settings.Language != "en")
                return false;

            // Validate Theme
            if (string.IsNullOrEmpty(settings.Theme))
                return false;

            if (settings.Theme != "Light" && settings.Theme != "Dark" && settings.Theme != "Auto")
                return false;

            // Validate FontSize
            if (settings.FontSize < 8 || settings.FontSize > 16)
                return false;

            // Validate NotificationDaysBefore
            if (settings.NotificationDaysBefore < 1 || settings.NotificationDaysBefore > 90)
                return false;

            return true;
        }

        /// <summary>
        /// Kiểm tra có thông báo nào được bật không
        /// </summary>
        public bool HasAnyNotificationEnabled(SettingsDTO settings)
        {
            return settings.NotifyRegistrationExpiry ||
                   settings.NotifyInsuranceExpiry ||
                   settings.NotifyRentalExpiry;
        }

        /// <summary>
        /// Kiểm tra settings đã thay đổi chưa
        /// </summary>
        public bool IsSettingsChanged(SettingsDTO current, SettingsDTO original)
        {
            if (current == null || original == null)
                return true;

            return !current.IsEqual(original);
        }

        /// <summary>
        /// Lấy thông tin về file settings
        /// </summary>
        public string GetSettingsFileInfo()
        {
            if (settingsDAL.SettingsFileExists())
            {
                return "File cài đặt đã tồn tại";
            }
            return "Chưa có file cài đặt (sử dụng mặc định)";
        }

        /// <summary>
        /// Restore từ backup
        /// </summary>
        public bool RestoreFromBackup()
        {
            try
            {
                return settingsDAL.RestoreFromBackup();
            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể khôi phục từ backup: {ex.Message}", ex);
            }
        }
    }
}
