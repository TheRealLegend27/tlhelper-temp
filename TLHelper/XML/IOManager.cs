using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TLHelper.Scripts;
using TLHelper.Settings;
using TLHelper.Skills;

namespace TLHelper.XML
{
    public static class IOManager
    {
        public static readonly string configDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static bool CreateConfigDir()
        {
            if (Directory.Exists(configDir)) return false;
            else Directory.CreateDirectory(configDir);
            return true;
        }

        public struct SettingsBundle
        {
            public (XmlNode def, XmlNode special) skills;
            public XmlNode settings;
            public XmlNode scripts;

            public SettingsBundle((XmlNode, XmlNode) skills, XmlNode settings, XmlNode scripts)
            {
                this.skills = skills;
                this.settings = settings;
                this.scripts = scripts;
            }
        }

        public static SettingsBundle LoadAllSettings()
        {
            // GET SAVE FILES
            var XmlSkills = new XmlDocument();
            string skillsPath = configDir + @"\TLHelper\skills.xml";
            XmlNode SkillsRoot = null;
            if (File.Exists(skillsPath))
            {
                XmlSkills.Load(skillsPath);
                SkillsRoot = XmlSkills.DocumentElement;
            }

            var XmlScripts = new XmlDocument();
            string scriptsPath = configDir + @"\TLHelper\scripts.xml";
            XmlNode ScriptsRoot = null;
            if (File.Exists(scriptsPath))
            {
                XmlScripts.Load(scriptsPath);
                ScriptsRoot = XmlScripts.DocumentElement;
            }

            var XmlSettings = new XmlDocument();
            string settingsPath = configDir + @"\TLHelper\settings.xml";
            XmlNode SettingsRoot = null;
            if (File.Exists(settingsPath))
            {
                XmlSettings.Load(settingsPath);
                SettingsRoot = XmlSettings.DocumentElement;
            }

            var skills = (SkillsRoot.SelectSingleNode("descendant::Skills"), SkillsRoot.SelectSingleNode("descendant::SpecialSkills"));
            return new SettingsBundle((skills), SettingsRoot.SelectSingleNode("descendant::Settings"), ScriptsRoot.SelectSingleNode("descendant::Scripts"));
        }

        public static void SaveAllSettings()
        {
            // SAVE SETTINGS
            //  SKILLS
            XmlDocument skillDoc = SkillManager.GetXml();
            skillDoc.Save(configDir + @"\TLHelper\skills.xml");
            //  SCRIPTS
            XmlDocument scriptDoc = ScriptManager.GetXml();
            scriptDoc.Save(configDir + @"\TLHelper\scripts.xml");
            //  SETTINGS
            XmlDocument settingsDoc = SettingsManager.GetXml();
            settingsDoc.Save(configDir + @"\TLHelper\settings.xml");
        }

    }
}
