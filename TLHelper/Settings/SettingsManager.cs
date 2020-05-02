using System.Collections.Generic;
using System.Xml;

namespace TLHelper.Settings
{
    public static class SettingsManager
    {
        private static readonly Dictionary<string, string> Settings = new Dictionary<string, string>();

#pragma warning disable IDE0052 // Ungelesene private Member entfernen
        private static MainForm MainFormRef;
#pragma warning restore IDE0052 // Ungelesene private Member entfernen

        public static void SetFormRef(MainForm Ref) => MainFormRef = Ref;

        public static bool Contains(string key) => Settings.ContainsKey(key) && Settings[key].Length > 0;
        public static string GetSetting(string key) => Settings[key];
        public static void SetSetting(string key, string value) => Settings[key] = value;
        public static void ResetSetting(string key)
        {
            Settings.Remove(key);
            CreateMissingSettings();
        }

        public static void LoadSettings(XmlNode n)
        {
            foreach (XmlElement s in n.ChildNodes)
                Settings[s.GetAttribute("id")] = s.GetAttribute("value");
        }

        public static void CreateMissingSettings()
        {
            (string, string)[] defaultSettings = new (string, string)[]
            {
                ("thud-exe", ""),
                ("d3-exe", ""),
                ("ath", "")
            };

            foreach ((string key, string def) in defaultSettings)
            {
                if (!Settings.ContainsKey(key))
                    Settings.Add(key, def);
            }

        }

        public static string Ath => Settings["ath"];

        public static XmlDocument GetXml()
        {
            XmlDocument tdoc = new XmlDocument();
            var root = tdoc.CreateElement("TLHelper");
            var settings = tdoc.CreateElement("Settings");

            foreach (KeyValuePair<string, string> setting in Settings)
            {
                var settingElement = tdoc.CreateElement("Setting");
                settingElement.SetAttribute("id", setting.Key);
                settingElement.SetAttribute("value", setting.Value);
                settings.AppendChild(settingElement);
            }

            root.AppendChild(settings);
            tdoc.AppendChild(root);
            return tdoc;
        }

    }
}
