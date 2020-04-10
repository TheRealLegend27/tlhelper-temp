using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TLHelper.Stats.Skills
{
    class SkillBar
    {
        public static Dictionary<string, Skill> Skills = new Dictionary<string, Skill>();
        private static List<Skill> currentSkills = new List<Skill>();

        private static string[] classPrefixes = new string[] { "barb", "monk", "wizard", "dh", "crusader", "wd", "necro" };

        public static void RegisterSkill(Skill s, string name)
        {
            Skills.Add(name, s);
        }

        public static void SetCurrentClass(int index)
        {
            currentSkills.Clear();
            string pref = classPrefixes[index];
            Console.WriteLine(pref);

            foreach (KeyValuePair<string, Skill> kvp in Skills)
            {
                if (kvp.Value.id[0] == pref)
                {
                    Console.WriteLine("Activating " + kvp.Value.id[0] + "_" + kvp.Value.id[1]);
                    currentSkills.Add(kvp.Value);
                }
            }
        }

        public static void ProcessSkills()
        {
            if (!ScreenTools.IsDiabloFocused()) return;
            if (!ScreenTools.IsInRift()) return;
            if (ScreenTools.IsPorting()) return;

            foreach (Skill skill in currentSkills)
            {
                if (skill.IsActive && skill.CanPress(skill.SkillSlot, SkillStats.GetPxlColor(skill)))
                {
                    (bool isMouse, Keys key, string button) = skill.GetKey();
                    if (isMouse)
                    {
                        HardwareRobot.DoMouseClick(Cursor.Position.X, Cursor.Position.Y, button == "lmb");
                    }
                    else
                    {
                        HardwareRobot.PressKey((char)key);
                    }
                }
            }
        }
    }
}
