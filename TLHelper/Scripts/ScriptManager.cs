using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using TLHelper.HotKeys;

namespace TLHelper.Scripts
{
    class ScriptManager
    {

        private static MainForm MainFormRef;
        private static readonly Dictionary<string, Script> LoadedScripts = new Dictionary<string, Script>();

        public static void SetFormRef(MainForm Ref) => MainFormRef = Ref;

        public static void Run(string id) => LoadedScripts[id].Run();

        public static void LoadScripts()
        {
            InternalScripts.RegisterScripts();
            foreach (string file in Directory.GetFiles(ScriptCompiler.ScriptRoot))
            {
                if (Path.GetExtension(file) == ".tls")
                    ScriptCompiler.InterpretScript(file);
                else if (Path.GetExtension(file) == ".ahk-tl")
                    AHKScript.ParseFromFile(file);
            }
        }


        public static void OverrideScripts(XmlNode settings)
        {
            bool scriptsChanged = false;
            foreach (XmlNode script in settings.ChildNodes)
            {
                var id = script.Attributes.GetNamedItem("id").InnerText;
                if (LoadedScripts.ContainsKey(id))
                {
                    LoadedScripts[id].Enabled = bool.Parse(script.Attributes.GetNamedItem("enabled").InnerText);
                    LoadedScripts[id].HotKey.CurrentKey.CurrentKey = (Keys)int.Parse(script.Attributes.GetNamedItem("key").InnerText);
                    LoadedScripts[id].HotKey.IsCtrl = bool.Parse(script.Attributes.GetNamedItem("ctrl").InnerText);
                    LoadedScripts[id].HotKey.IsShift = bool.Parse(script.Attributes.GetNamedItem("shift").InnerText);
                    LoadedScripts[id].HotKey.IsAlt = bool.Parse(script.Attributes.GetNamedItem("alt").InnerText);
                    LoadedScripts[id].ScriptOrigin = script.Attributes.GetNamedItem("scriptOrigin").InnerText == "INT" ? ScriptOrigins.INT : ScriptOrigins.EXT;
                    scriptsChanged = true;
                }
            }

            if (scriptsChanged)
                RefreshScriptDisplay();

            RegisterHotkeys();
        }

        private static void RegisterHotkeys()
        {
            foreach (KeyValuePair<string, Script> scripts in LoadedScripts)
            {
                scripts.Value.RegisterHotkey(scripts.Key);
            }
        }

        public static void RefreshScriptDisplay()
        {
            MainFormRef.OverviewContainer.SideBarContainer.ClearScriptDesctiption();
            MainFormRef.ScriptContainer.ClearScripts();

            foreach (KeyValuePair<string, Script> kvp in LoadedScripts)
            {
                var s = kvp.Value;
                MainFormRef.OverviewContainer.SideBarContainer.AddScriptDescription(s.Name + ": " + s.HotKey.GetString());
                MainFormRef.ScriptContainer.AddScript(s);
            }

        }

        public static void AddScript(string id, string name, HotKey key, bool enabled, ScriptOrigins origin, RunScript run)
        {
            Script s = new Script(key, run, name, enabled, origin);

            MainFormRef.OverviewContainer.SideBarContainer.AddScriptDescription(name + ": " + key.GetString());
            MainFormRef.ScriptContainer.AddScript(s);

            LoadedScripts.Add(id, s);
        }
        public static void AddScript((string id, string name, HotKey key, bool enabled, ScriptOrigins origin, RunScript run) values)
        {
            AddScript(values.id, values.name, values.key, values.enabled, values.origin, values.run);
        }
        public static void AddScript(string id, Script script)
        {
            MainFormRef.OverviewContainer.SideBarContainer.AddScriptDescription(script.Name + ": " + script.HotKey.GetString());
            MainFormRef.ScriptContainer.AddScript(script);

            LoadedScripts.Add(id, script);
        }

        public static XmlDocument GetXml()
        {
            XmlDocument tdoc = new XmlDocument();
            var root = tdoc.CreateElement("TLHelper");
            root.SetAttribute("version", EnvironmentVariables.CURRENT_VERSION_INT);

            var scripts = tdoc.CreateElement("Scripts");

            foreach (KeyValuePair<string, Script> kvp in LoadedScripts)
            {
                var scriptElement = tdoc.CreateElement("Script");
                kvp.Value.SetXmlAttribs(scriptElement);
                scriptElement.SetAttribute("id", kvp.Key);
                scripts.AppendChild(scriptElement);
            }

            root.AppendChild(scripts);

            tdoc.AppendChild(root);

            return tdoc;
        }

    }
}
