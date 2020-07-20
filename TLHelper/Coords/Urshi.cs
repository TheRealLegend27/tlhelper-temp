using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TLHelper.Settings;
using TLHelper.SysCom;

namespace TLHelper.Coords
{
    static class Urshi
    {

        private static Dictionary<string, string> statusNames = new Dictionary<string, string>();

        private static Point GemLoc;
        private static Point UpgradeLoc;

        static Urshi()
        {

            statusNames.Add("inactive", "Inactive");
            statusNames.Add("no-empower", "No Empower");
            statusNames.Add("empower", "Empower");

            GemLoc = new Point(97, 641);
            UpgradeLoc = new Point(266, 549);
        }

        public static int GetUpgradeCount()
        {
            string cur = SettingsManager.GetSetting("auto-gemups");
            if (cur == "no-empower") return 4;
            if (cur == "empower") return 5;
            return 0;
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

        public static void Upgrade(int count)
        {
            HardwareRobot.DoLeftClick(GemLoc.X, GemLoc.Y, HardwareRobot.ActionTypes.SIMULATE);
            Thread.Sleep(50);
            for (int i=0; i<count; i++)
            {
                ConfirmUpgrade();
                if (count - i == 3) SendKeys.SendWait("t");
                Thread.Sleep(1600);
            }
        }

        private static void ConfirmUpgrade() => HardwareRobot.DoLeftClick(UpgradeLoc.X, UpgradeLoc.Y, HardwareRobot.ActionTypes.SIMULATE);

    }
}
