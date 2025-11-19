using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SettingsDTO
    {
        // Ngôn ngữ
        public string Language { get; set; }

        // Giao diện
        public string Theme { get; set; }
        public bool EnableAnimations { get; set; }
        public int FontSize { get; set; }

        // Thông báo
        public bool NotifyRegistrationExpiry { get; set; }
        public bool NotifyInsuranceExpiry { get; set; }
        public bool NotifyRentalExpiry { get; set; }
        public int NotificationDaysBefore { get; set; }

        // Constructor mặc định
        public SettingsDTO()
        {
            Language = "vi";
            Theme = "Light"; 
            EnableAnimations = true;
            FontSize = 10;
            NotifyRegistrationExpiry = true;
            NotifyInsuranceExpiry = true;
            NotifyRentalExpiry = true;
            NotificationDaysBefore = 7;
        }

        // Constructor có tham số
        public SettingsDTO(string language, string theme, bool enableAnimations,
            int fontSize, bool notifyReg, bool notifyIns, bool notifyRental, int daysBefore)
        {
            Language = language;
            Theme = theme;
            EnableAnimations = enableAnimations;
            FontSize = fontSize;
            NotifyRegistrationExpiry = notifyReg;
            NotifyInsuranceExpiry = notifyIns;
            NotifyRentalExpiry = notifyRental;
            NotificationDaysBefore = daysBefore;
        }

        // Clone object
        public SettingsDTO Clone()
        {
            return new SettingsDTO
            {
                Language = this.Language,
                Theme = this.Theme,
                EnableAnimations = this.EnableAnimations,
                FontSize = this.FontSize,
                NotifyRegistrationExpiry = this.NotifyRegistrationExpiry,
                NotifyInsuranceExpiry = this.NotifyInsuranceExpiry,
                NotifyRentalExpiry = this.NotifyRentalExpiry,
                NotificationDaysBefore = this.NotificationDaysBefore
            };
        }

        // Kiểm tra 2 settings có giống nhau không
        public bool IsEqual(SettingsDTO other)
        {
            if (other == null) return false;

            return this.Language == other.Language &&
                   this.Theme == other.Theme &&
                   this.EnableAnimations == other.EnableAnimations &&
                   this.FontSize == other.FontSize &&
                   this.NotifyRegistrationExpiry == other.NotifyRegistrationExpiry &&
                   this.NotifyInsuranceExpiry == other.NotifyInsuranceExpiry &&
                   this.NotifyRentalExpiry == other.NotifyRentalExpiry &&
                   this.NotificationDaysBefore == other.NotificationDaysBefore;
        }
    }
}
