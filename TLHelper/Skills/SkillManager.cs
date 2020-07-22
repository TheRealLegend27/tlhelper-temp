using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using TLHelper.Coords;
using TLHelper.HotKeys;
using TLHelper.Ingame;
using TLHelper.Settings;
using TLHelper.SysCom;
using static TLHelper.Skills.AvailableFunctions;

namespace TLHelper.Skills
{
    class SkillManager
    {
        private static MainForm MainFormRef;

        private static readonly Dictionary<string, Skill> Skills = new Dictionary<string, Skill>();
        private static readonly List<string> ActiveSkills = new List<string>();

        public static PotionSkill PotionSkill = null;

        public static void SetFormRef(MainForm Ref)
        {
            MainFormRef = Ref;
        }

        public static int ProcessLoop()
        {
            if (!ScreenTools.IsDiabloFocused()) return 1;
            if (ScreenTools.IsPorting()) return 3;

            bool inRift = ScreenTools.IsInRift();

            // Conditions for Skill Activation
            if (
                ActiveMode.GetCurrentMode() == ActiveMode.Mode.AlwaysActive ||
                (ActiveMode.GetCurrentMode() == ActiveMode.Mode.Automatic && inRift)
            )   ProcessSkills();

            // Conditions for Town Actions
            if (ActiveMode.GetCurrentMode() != ActiveMode.Mode.AlwaysActive && !inRift)
                ProcessTown();

            // always process potion
            PotionSkill.Process();
            return 0;
        }

        private static void ProcessSkills()
        {
            foreach (string id in ActiveSkills)
            {
                var skill = Skills[id];

                if (skill.IsActive && skill.CanPress(skill.Slot, SkillCoords.GetPxlColor(skill)))
                {
                    var key = skill.Key;
                    if (key.IsMouse)
                    {
                        if (key.CurrentKey == Keys.LButton)
                            HardwareRobot.DoLeftClickShift();
                        else if (key.CurrentKey == Keys.RButton)
                            HardwareRobot.DoRightClick();
                        else if (key.CurrentKey == Keys.XButton1)
                            HardwareRobot.DoXButton1Click();
                        else if (key.CurrentKey == Keys.XButton2)
                            HardwareRobot.DoXButton2Click();
                    }
                    else
                    {
                        HardwareRobot.PressKey((char)key.CurrentKey);
                    }
                }
            }
            if (SettingsManager.GetSetting("hexing-pants") == "1")
                HexingPantsSkill.Move();
        }

        private static void ProcessTown()
        {
            var kadalaItem = Kadala.GetSelectedItem();
            if (kadalaItem != Kadala.Items.NONE && OpenWindows.IsKadalaNewOpened())
                Kadala.GambleItem(kadalaItem);

            if (Smith.ShouldSalvage() && OpenWindows.IsSmithNewOpened())
                Smith.SalvageNormals();

            int urshiUp = Urshi.GetUpgradeCount();
            if (urshiUp > 0 && OpenWindows.IsUrshiNewOpened())
                Urshi.Upgrade(urshiUp);
        }

        public static void ClearActiveSkills()
        {
            ActiveSkills.Clear();
            MainFormRef.OverviewContainer.SkillContainer.ClearSkills();
        }

        public static void SetActiveSkills(string classId)
        {
            ActiveMode.KeyPressed("active-mode-auto");
            ClearActiveSkills();
            if (classId == null) return;
            foreach (string id in Skills.Keys)
                if (id.Contains(classId))
                {
                    ActiveSkills.Add(id);
                    MainFormRef.OverviewContainer.SkillContainer.AddSkill(Skills[id]);
                }
        }

        public static void InitSkills(XmlNode SkillSettings, XmlNode ExtSkillSettings)
        {
            foreach (XmlNode igClass in SkillSettings.ChildNodes)
            {
                string currentClassId = igClass.Attributes.GetNamedItem("id").InnerText;
                foreach (XmlNode igSkill in igClass.ChildNodes)
                {
                    string currentSkillId = igSkill.Attributes.GetNamedItem("id").InnerText;
                    string name = Resources.Strings.ResourceManager.GetString(currentSkillId);
                    var icon = (Image)Resources.SkillIcons.ResourceManager.GetObject(currentClassId + "_" + currentSkillId);
                    int slot = int.Parse(igSkill.Attributes.GetNamedItem("slot").InnerText);
                    int key = int.Parse(igSkill.Attributes.GetNamedItem("key").InnerText);
                    bool active = bool.Parse(igSkill.Attributes.GetNamedItem("active").InnerText);
                    AvailableType available = ParseType(igSkill.Attributes.GetNamedItem("func").InnerText);

                    Skills.Add(currentClassId + "_" + currentSkillId, new Skill(name, icon, new Key((Keys)key), slot, active, available));
                }
            }

            PotionSkill = new PotionSkill(new Key(Keys.None), false);

            foreach (XmlNode extSkill in ExtSkillSettings)
            {
                if (extSkill.Attributes.GetNamedItem("id").InnerText == "potion")
                {
                    var key = new Key((Keys)int.Parse(extSkill.Attributes.GetNamedItem("key").InnerText));
                    var active = bool.Parse(extSkill.Attributes.GetNamedItem("active").InnerText);

                    PotionSkill.SetKey(key);
                    PotionSkill.SetActive(active);
                }
            }
        }

        public static void CreateMissingSkills()
        {
            foreach (KeyValuePair<string, (string, AvailableType)[]> kvp in DefaultSkills.Skills)
            {
                foreach ((string skill, AvailableType type) in kvp.Value)
                {
                    var name = Resources.Strings.ResourceManager.GetString(skill);
                    var icon = (Image)Resources.SkillIcons.ResourceManager.GetObject(kvp.Key + "_" + skill);
                    if (!Skills.ContainsKey(kvp.Key + "_" + skill))
                        Skills.Add(kvp.Key + "_" + skill, new Skill(name, icon, new Key(Keys.None), 0, false, type));
                }
            }

            // create potion skill
            if (PotionSkill == null)
                PotionSkill = new PotionSkill(new Key(Keys.None), false);
        }

        public static XmlDocument GetXml()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("TLHelper");
            root.SetAttribute("version", EnvironmentVariables.CURRENT_VERSION_INT);
            var skills = doc.CreateElement("Skills");

            var cBarb = doc.CreateElement("Class"); cBarb.SetAttribute("id", "barb");
            var cCrus = doc.CreateElement("Class"); cCrus.SetAttribute("id", "crus");
            var cDH = doc.CreateElement("Class"); cDH.SetAttribute("id", "dh");
            var cMonk = doc.CreateElement("Class"); cMonk.SetAttribute("id", "monk");
            var cNec = doc.CreateElement("Class"); cNec.SetAttribute("id", "nec");
            var cWD = doc.CreateElement("Class"); cWD.SetAttribute("id", "wd");
            var cWiz = doc.CreateElement("Class"); cWiz.SetAttribute("id", "wiz");

            foreach (KeyValuePair<string, Skill> kvp in Skills)
            {
                var skillNode = doc.CreateElement("Skill");
                kvp.Value.SetXmlAttribs(skillNode);
                skillNode.SetAttribute("id", kvp.Key.Split(new char[] { '_' }, 2)[1]);

                if (kvp.Key.StartsWith("barb")) cBarb.AppendChild(skillNode);
                else if (kvp.Key.StartsWith("crus")) cCrus.AppendChild(skillNode);
                else if (kvp.Key.StartsWith("dh")) cDH.AppendChild(skillNode);
                else if (kvp.Key.StartsWith("monk")) cMonk.AppendChild(skillNode);
                else if (kvp.Key.StartsWith("nec")) cNec.AppendChild(skillNode);
                else if (kvp.Key.StartsWith("wd")) cWD.AppendChild(skillNode);
                else if (kvp.Key.StartsWith("wiz")) cWiz.AppendChild(skillNode);
            }

            skills.AppendChild(cBarb);
            skills.AppendChild(cCrus);
            skills.AppendChild(cDH);
            skills.AppendChild(cMonk);
            skills.AppendChild(cNec);
            skills.AppendChild(cWD);
            skills.AppendChild(cWiz);

            root.AppendChild(skills);

            var specialSkills = doc.CreateElement("SpecialSkills");

            var potionSkill = doc.CreateElement("Skill");
            PotionSkill.SetXmlAttribs(potionSkill);
            potionSkill.SetAttribute("id", "potion");

            specialSkills.AppendChild(potionSkill);

            root.AppendChild(specialSkills);

            doc.AppendChild(root);

            return doc;
        }

    }
}
