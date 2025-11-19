using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DAL
{
    public class SettingsDAL
    {
        private readonly string settingsFilePath;
        private readonly string settingsDirectory;

        public SettingsDAL()
        {
            // Đường dẫn lưu settings
            settingsDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "MotorcycleShop");

            settingsFilePath = Path.Combine(settingsDirectory, "settings.xml");
        }

        /// <summary>
        /// Load settings từ file
        /// </summary>
        public SettingsDTO LoadSettings()
        {
            try
            {
                // Kiểm tra file có tồn tại không
                if (!File.Exists(settingsFilePath))
                {
                    // Nếu chưa có file, trả về settings mặc định
                    return new SettingsDTO();
                }

                // Đọc file XML
                XmlSerializer serializer = new XmlSerializer(typeof(SettingsDTO));
                using (FileStream fs = new FileStream(settingsFilePath, FileMode.Open))
                {
                    SettingsDTO settings = (SettingsDTO)serializer.Deserialize(fs);
                    return settings;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi đọc file cài đặt: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lưu settings vào file
        /// </summary>
        public bool SaveSettings(SettingsDTO settings)
        {
            try
            {
                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(settingsDirectory))
                {
                    Directory.CreateDirectory(settingsDirectory);
                }

                // Ghi file XML
                XmlSerializer serializer = new XmlSerializer(typeof(SettingsDTO));
                using (StreamWriter writer = new StreamWriter(settingsFilePath))
                {
                    serializer.Serialize(writer, settings);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu file cài đặt: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Xóa file settings (khôi phục mặc định)
        /// </summary>
        public bool DeleteSettings()
        {
            try
            {
                if (File.Exists(settingsFilePath))
                {
                    File.Delete(settingsFilePath);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa file cài đặt: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Kiểm tra file settings có tồn tại không
        /// </summary>
        public bool SettingsFileExists()
        {
            return File.Exists(settingsFilePath);
        }

        /// <summary>
        /// Backup settings hiện tại
        /// </summary>
        public bool BackupSettings()
        {
            try
            {
                if (File.Exists(settingsFilePath))
                {
                    string backupPath = settingsFilePath + ".backup";
                    File.Copy(settingsFilePath, backupPath, true);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi backup cài đặt: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Restore settings từ backup
        /// </summary>
        public bool RestoreFromBackup()
        {
            try
            {
                string backupPath = settingsFilePath + ".backup";
                if (File.Exists(backupPath))
                {
                    File.Copy(backupPath, settingsFilePath, true);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi khôi phục cài đặt: {ex.Message}", ex);
            }
        }
    }
}
