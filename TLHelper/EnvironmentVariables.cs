using System;

namespace TLHelper
{
    public static class EnvironmentVariables
    {
        public static readonly string CURRENT_VERSION = "a1.2.1";
        public static readonly string API_ROOT = "https://tlhelper.fischer-enterprise.de/api/";
        public static readonly string WEBSITE_LINK = "https://tlhelper.fischer-enterprise.de";
        public static readonly string CONFIG_DIR = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\TLHelper";
        public static readonly string SCRIPTS_DIR = CONFIG_DIR + @"\scripts";
    }
}
