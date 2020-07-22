using System;

namespace TLHelper
{
    public static class EnvironmentVariables
    {
        public static readonly string CURRENT_VERSION = "a1.2.3";
        public static readonly string CURRENT_VERSION_INT = "123";
        public static readonly string API_ROOT = "https://tlhelper.fischer-enterprise.de/api/";
        public static readonly string WEBSITE_LINK = "https://tlhelper.fischer-enterprise.de";
        public static readonly string CONFIG_DIR = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\TLHelper";
        public static readonly string SCRIPTS_DIR = CONFIG_DIR + @"\scripts";

        public static readonly int MIN_SKILL_SETTINGS_VERSION = 123;
        public static readonly int MIN_SETTING_SETTINGS_VERSION = 123;
        public static readonly int MIN_SCRIPT_SETTINGS_VERSION = 123;
        public static readonly int MIN_ACTION_SETTINGS_VERSION = 123;
    }
}
