using System;
using System.IO;
using System.Xml;
using TLHelper.Scripts;
using TLHelper.Settings;
using TLHelper.Skills;

namespace TLHelper.XML
{
    public static class IOManager
    {
        public static readonly string configDir = EnvironmentVariables.CONFIG_DIR;

        public static bool CreateConfigDir()
        {
            if (Directory.Exists(configDir)) return false;
            else Directory.CreateDirectory(configDir);
            return true;
        }

        public static bool CreateScriptsDir()
        {
            if (Directory.Exists(configDir + "/scripts")) return false;
            else Directory.CreateDirectory(configDir + "/scripts");
            return true;
        }

        public static bool CreateAHKScriptsDir()
        {
            if (Directory.Exists(configDir + "/scripts/ahk")) return false;
            else Directory.CreateDirectory(configDir + "/scripts/ahk");
            return true;
        }

        public struct SettingsBundle
        {
            public (XmlNode def, XmlNode special) skills;
            public XmlNode settings;
            public XmlNode scripts;
            public XmlNode actions;

            public SettingsBundle((XmlNode, XmlNode) skills, XmlNode settings, XmlNode scripts, XmlNode actions)
            {
                this.skills = skills;
                this.settings = settings;
                this.scripts = scripts;
                this.actions = actions;
            }
        }

        public static SettingsBundle LoadAllSettings()
        {

            // CREATE SETTINGS BUNDLE
            SettingsBundle bundle = new SettingsBundle();

            // GET SAVE FILES
            var XmlSkills = new XmlDocument();
            string skillsPath = configDir + @"\skills.xml";
            XmlNode SkillsRoot = null;
            if (File.Exists(skillsPath))
            {
                XmlSkills.Load(skillsPath);
                if (XmlSkills.DocumentElement.HasAttribute("version") && int.Parse(XmlSkills.DocumentElement.GetAttribute("version")) >= EnvironmentVariables.MIN_SKILL_SETTINGS_VERSION)
                    SkillsRoot = XmlSkills.DocumentElement;
            }

            var XmlScripts = new XmlDocument();
            string scriptsPath = configDir + @"\scripts.xml";
            XmlNode ScriptsRoot = null;
            if (File.Exists(scriptsPath))
            {
                XmlScripts.Load(scriptsPath);
                if (XmlScripts.DocumentElement.HasAttribute("version") && int.Parse(XmlScripts.DocumentElement.GetAttribute("version")) >= EnvironmentVariables.MIN_SKILL_SETTINGS_VERSION)
                    ScriptsRoot = XmlScripts.DocumentElement;
            }

            var XmlSettings = new XmlDocument();
            string settingsPath = configDir + @"\settings.xml";
            XmlNode SettingsRoot = null;
            if (File.Exists(settingsPath))
            {
                XmlSettings.Load(settingsPath);
                if (XmlSettings.DocumentElement.HasAttribute("version") && int.Parse(XmlSettings.DocumentElement.GetAttribute("version")) >= EnvironmentVariables.MIN_SKILL_SETTINGS_VERSION)
                    SettingsRoot = XmlSettings.DocumentElement;
            }

            var XmlActions = new XmlDocument();
            string actionsPath = configDir + @"\actions.xml";
            XmlNode ActionsRoot = null;
            if (File.Exists(actionsPath))
            {
                XmlActions.Load(actionsPath);
                if (XmlActions.DocumentElement.HasAttribute("version") && int.Parse(XmlActions.DocumentElement.GetAttribute("version")) >= EnvironmentVariables.MIN_SKILL_SETTINGS_VERSION)
                    ActionsRoot = XmlActions.DocumentElement;
            }

            if (SkillsRoot != null)
                bundle.skills = (SkillsRoot.SelectSingleNode("descendant::Skills"), SkillsRoot.SelectSingleNode("descendant::SpecialSkills"));
            if (SettingsRoot != null)
                bundle.settings = SettingsRoot.SelectSingleNode("descendant::Settings");
            if (ScriptsRoot != null)
                bundle.scripts = ScriptsRoot.SelectSingleNode("descendant::Scripts");
            if (ActionsRoot != null)
                bundle.actions = ActionsRoot.SelectSingleNode("descendant::Actions");

            return bundle;
        }

        public static void SaveAllSettings()
        {
            // SAVE SETTINGS
            //  SKILLS
            XmlDocument skillDoc = SkillManager.GetXml();
            skillDoc.Save(configDir + @"\skills.xml");
            //  SCRIPTS
            XmlDocument scriptDoc = ScriptManager.GetXml();
            scriptDoc.Save(configDir + @"\scripts.xml");
            //  SETTINGS
            XmlDocument settingsDoc = SettingsManager.GetXml();
            settingsDoc.Save(configDir + @"\settings.xml");
            //  ACTIONS
            XmlDocument actionsDoc = ActiveMode.GetXml();
            actionsDoc.Save(configDir + @"\actions.xml");
        }

    }
}
