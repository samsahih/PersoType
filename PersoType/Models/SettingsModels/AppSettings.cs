namespace PersoType.Models.SettingsModels
{
    public class AppSettings
    {
        public class ConfigSettingsModel
        {
            public string PersoTypeAPIsURL { get; set; }
        }

        public ConfigSettingsModel ConfigSettings { get; set; }
    }
}
