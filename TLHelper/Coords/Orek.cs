using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLHelper.Settings;
using TLHelper.SysCom;

namespace TLHelper.Coords
{
    public static class Orek
    {
        private static Dictionary<string, string> statusNames = new Dictionary<string, string>();

        private static Point RiftLoc;
        private static Point GRiftLoc;
        private static Point AcceptLoc;

        static Orek()
        {

            statusNames.Add("inactive", "Inactive");
            statusNames.Add("gr", "Greater Rift");
            statusNames.Add("r", "Rift");

            RiftLoc = new Point(265, 285);
            GRiftLoc = new Point(265, 475);
            AcceptLoc = new Point(265, 845);
        }

        public static string GetStatusNameByCode(string code)
        {
            foreach (KeyValuePair<string, string> kvp in statusNames)
            {
                if (kvp.Key == code) return kvp.Value;
            }
            return "";
        }

        public static string GetStatusCodeByName(string name)
        {
            foreach (KeyValuePair<string, string> kvp in statusNames)
            {
                if (kvp.Value == name) return kvp.Key;
            }
            return "";
        }

        public static string[] GetStatusNames()
        {
            string[] names = new string[statusNames.Count];
            int i = 0;
            foreach (KeyValuePair<string, string> kvp in statusNames)
            {
                names[i] = kvp.Value;
                i++;
            }
            return names;
        }

        public static bool ShouldOpen()
        {
            return SettingsManager.GetSetting("auto-rift") != "inactive";
        }

        public static void Open()
        {
            string mode = SettingsManager.GetSetting("auto-rift");
            if (mode == "inactive") return;
            if (mode == "r") HardwareRobot.DoLeftClick(RiftLoc.X, RiftLoc.Y);
            else if (mode == "gr") HardwareRobot.DoLeftClick(GRiftLoc.X, GRiftLoc.Y);

            HardwareRobot.DoLeftClick(AcceptLoc.X, AcceptLoc.Y);
        }
    }
}
