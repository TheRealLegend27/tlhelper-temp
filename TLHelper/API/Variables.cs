using TLHelper.Settings;

namespace TLHelper.API
{
    public static class Variables
    {
        private static string _license = "";
        public static string License
        {
            get { return _license; }
            set
            {
                _license = value;
                SettingsManager.SetSetting("license", _license);
            }
        }
    }
}
