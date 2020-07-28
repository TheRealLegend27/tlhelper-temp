using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TLHelper.Settings;
using TLHelper.SysCom;

namespace TLHelper.Coords
{
    public static class ConfirmDialog
    {
        private static Dictionary<string, string> statusNames = new Dictionary<string, string>();

        private static Point AcceptLoc;
        private static Point EmpowerLoc;

        static ConfirmDialog()
        {

            statusNames.Add("inactive", "Inactive");
            statusNames.Add("accept", "Accept");
            statusNames.Add("empower", "Accept Empowered");

            AcceptLoc = new Point(815, 915);
            EmpowerLoc = new Point(965, 810);
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

        public static bool ShouldAccept()
        {
            return SettingsManager.GetSetting("auto-accept") != "inactive";
        }

        public static void Accept()
        {
            string mode = SettingsManager.GetSetting("auto-accept");
            if (mode == "inactive") return;
            if (mode == "empower")
            {
                HardwareRobot.DoLeftClick(EmpowerLoc.X, EmpowerLoc.Y);
                Thread.Sleep(10);
            }
            HardwareRobot.DoLeftClick(AcceptLoc.X, AcceptLoc.Y);
        }
    }
}
