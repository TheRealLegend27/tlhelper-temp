using System;
using System.Collections.Generic;
using System.Xml;

namespace TLHelper.Settings
{
    public static class SettingsManager
    {
        private static readonly string[] validSettings = new string[] { "thud-exe", "d3-exe", "license", "ahk-exe", "salvage-normals", "kadala-gamble", "auto-gemups" };
        private static readonly Dictionary<string, string> Settings = new Dictionary<string, string>();

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
            {
                if (Array.Exists(validSettings, element => element == s.GetAttribute("id")))
                    Settings[s.GetAttribute("id")] = s.GetAttribute("value");
            }
        }

        public static void CreateMissingSettings()
        {
            foreach (string key in validSettings)
            {
                if (!Settings.ContainsKey(key))
                    Settings.Add(key, "");
            }
            FillRequiredSettings();
        }

        public static void FillRequiredSettings()
        {
            if (Settings["salvage-normals"] == "") Settings["salvage-normals"] = "0";
            if (Settings["kadala-gamble"] == "") Settings["kadala-gamble"] = "none";
            if (Settings["auto-gemups"] == "") Settings["auto-gemups"] = "inactive";
        }

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
