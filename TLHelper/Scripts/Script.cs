using System.Windows.Forms;
using System.Xml;
using TLHelper.HotKeys;
using TLHelper.SysCom;

namespace TLHelper.Scripts
{
    public delegate void RunScript();
    public class Script
    {
        public HotKey HotKey;
        public RunScript Run;
        public string Name;
        public bool Enabled;
        public ScriptOrigins ScriptOrigin;

        public Script(HotKey hotKey, RunScript run, string name, bool enabled, ScriptOrigins scriptOrigin)
        {
            HotKey = hotKey;
            Run = run;
            Name = name;
            Enabled = enabled;
            ScriptOrigin = scriptOrigin;
        }

        public void RegisterHotkey(string id)
        {
            HotkeyManager.RegisterKey(HotKey, id);
        }

        public void ChangeHotKey(HotKey hk)
        {
            HotKey = hk;
            MessageBox.Show("To apply the change, you need to restart TLHelper...", "Hotkey changed",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ChangeEnabled(bool enabled)
        {
            Enabled = enabled;
        }

        public void SetXmlAttribs(XmlElement e)
        {
            e.SetAttribute("enabled", Enabled.ToString());
            e.SetAttribute("key", ((int)HotKey.CurrentKey.CurrentKey).ToString());
            e.SetAttribute("ctrl", HotKey.IsCtrl.ToString());
            e.SetAttribute("shift", HotKey.IsShift.ToString());
            e.SetAttribute("alt", HotKey.IsAlt.ToString());
            e.SetAttribute("scriptOrigin", ScriptOrigin == ScriptOrigins.EXT ? "EXT" : "INT");
        }

    }

    public enum ScriptOrigins
    {
        INT,
        EXT
    }

}
