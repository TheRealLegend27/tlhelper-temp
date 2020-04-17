using System;
using System.Windows.Forms;

namespace TLHelper.Resources
{
    public static class GlobalData
    {
        static GlobalData()
        {
            // CLASSES
            Classes = new GlobalDataList<string, string>();
            Classes.AddData("barb", "Barbarian");
            Classes.AddData("monk", "Monk");
            Classes.AddData("wiz", "Wizard");
            Classes.AddData("nec", "Necro");
            Classes.AddData("crus", "Crusader");
            Classes.AddData("dh", "Demon Hunter");
            Classes.AddData("wd", "Witch Doctor");

            // SKILL ACTIVE MODES
            SkillActiveModes = new GlobalDataList<string, string>();
            SkillActiveModes.AddData("inactive", "Inactive");
            SkillActiveModes.AddData("active", "Active");

            // SKILL ACTIVE MODES
            HelperActiveModes = new GlobalDataList<string, string>();
            HelperActiveModes.AddData("inactive", "Always Inactive");
            HelperActiveModes.AddData("auto", "Automatic");
            HelperActiveModes.AddData("active", "Always Active");

            // SKILL SLOTS
            SkillSlots = new GlobalDataList<int, string>();
            SkillSlots.AddData(0, "LButton");
            SkillSlots.AddData(1, "RButton");
            SkillSlots.AddData(2, "1");
            SkillSlots.AddData(3, "2");
            SkillSlots.AddData(4, "3");
            SkillSlots.AddData(5, "4");

            // ALLOWED KEYS
            AllowedKeys = new GlobalDataList<Keys, string>();
            AllowedKeys.AddData(Keys.None, "No Key");
            // A-Z
            for (int i = (int)Keys.A; i < (int)Keys.Z + 1; i++)
                AllowedKeys.AddData((Keys)i, ((char)i + "").ToUpper());
            // 0-9
            for (int i = (int)Keys.D0; i < (int)Keys.D9 + 1; i++)
                AllowedKeys.AddData((Keys)i, ((char)i + "").ToUpper());
            // Numpad0 - Numpad9
            for (int i = (int)Keys.NumPad0; i < (int)Keys.NumPad9 + 1; i++)
                AllowedKeys.AddData((Keys)i, "Numpad " + (i - 96).ToString());
            // Numpad0 - Numpad9
            for (int i = (int)Keys.F1; i < (int)Keys.F12 + 1; i++)
                AllowedKeys.AddData((Keys)i, "F" + (i - 111).ToString());

            // MOUSE BUTTONS
            AllowedKeys.AddData(Keys.LButton, "LButton");
            AllowedKeys.AddData(Keys.RButton, "RButton");
            AllowedKeys.AddData(Keys.MButton, "MButton");
            AllowedKeys.AddData(Keys.XButton1, "XButton1");
            AllowedKeys.AddData(Keys.XButton2, "XButton2");

            // ARROW KEYS
            AllowedKeys.AddData(Keys.Left, "Arrow Left");
            AllowedKeys.AddData(Keys.Up, "Arrow Up");
            AllowedKeys.AddData(Keys.Right, "Arrow Right");
            AllowedKeys.AddData(Keys.Down, "Arrow Down");

            // SPECIAL KEYS
            AllowedKeys.AddData(Keys.Space, "Space");
            AllowedKeys.AddData(Keys.Tab, "Tab");

            foreach (Keys key in AllowedKeys.GetKeys())
            {
                Console.WriteLine(key + " - " + AllowedKeys.GetValue(key));
            }

        }

        public static GlobalDataList<string, string> Classes;

        public static GlobalDataList<string, string> SkillActiveModes;

        public static GlobalDataList<string, string> HelperActiveModes;

        public static GlobalDataList<int, string> SkillSlots;

        public static GlobalDataList<Keys, string> AllowedKeys;

    }
}
