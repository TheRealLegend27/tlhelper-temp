using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TLHelper.HotKeys;
using TLHelper.Settings;

namespace TLHelper.Scripts
{
    public class AHKScript : Script
    {
        public string ScriptFile;

        private int PID = -1;

        public AHKScript(HotKey hotKey, string name, bool enabled, string scriptFile) : base(hotKey, () => { }, name, enabled, ScriptOrigins.EXT)
        {
            ScriptFile = scriptFile;
            base.Run = Start;
        }

        public void Start()
        {
            if (SettingsManager.GetSetting("ahk-exe").Length == 0)
            {
                MessageBox.Show("You need to install AutoHotkey and set the path to your AutoHotkey.exe in settings", "AutoHotkey.exe not set!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            if (IsRunning())
            {
                Process.GetProcessById(PID).Kill();
            }
            else
            {
                Process p = new Process();
                p.StartInfo.FileName = @"C:\Program Files\AutoHotkey\AutoHotkey.exe"; // TODO: Get AHK-Executable from Settings
                p.StartInfo.Arguments = EnvironmentVariables.SCRIPTS_DIR + @"\ahk\" + ScriptFile;
                p.Start();
                PID = p.Id;
            }
        }

        public bool IsRunning()
        {
            if (PID == -1) return false;
            return Process.GetProcesses().Any(x => x.Id == PID);
        }

        public static void ParseFromFile(string scriptFile)
        {
            if (!File.Exists(scriptFile)) return;
            string content = File.ReadAllText(scriptFile);
            string name = "Unnamed", script = "";
            char key = char.MinValue;
            bool ctrl = false, shift = false, alt = false;

            // CLEAN UP FILE
            while (content.IndexOf("\t") >= 0) content = content.Replace("\t", "");
            while (content.IndexOf("\r") >= 0) content = content.Replace("\r", "");
            while (content.IndexOf("  ") >= 0) content = content.Replace("  ", " ");

            foreach (string line in content.Split('\n'))
            {
                if (line.StartsWith("#") || line.StartsWith("//")) continue;
                if (!line.Contains(":") || line.Split(':').Length != 2)
                {
                    MessageBox.Show("Invalid ahk-tl-File: " + scriptFile);
                    return;
                }
                string id = line.Split(':')[0];
                while (id.StartsWith(" ")) id = id.Substring(1);
                while (id.EndsWith(" ")) id = id.Substring(0, id.Length - 1);

                string val = line.Split(':')[1];
                while (val.StartsWith(" ")) val = val.Substring(1);
                while (val.EndsWith(" ")) val = val.Substring(0, val.Length - 1);

                switch (id)
                {
                    case "name": name = val; break;
                    case "key": key = val.ToCharArray()[0]; break;
                    case "script": script = val; break;

                    case "ctrl": ctrl = val == "1"; break;
                    case "shift": shift = val == "1"; break;
                    case "alt": alt = val == "1"; break;

                    default: break;
                }
            }

            if (script.Length == 0) return;

            HotKey hk = new HotKey(new Key((Keys)(int)key), ctrl, shift, alt);
            var scriptId = Path.GetFileNameWithoutExtension(scriptFile);
            ScriptManager.AddScript(scriptId, new AHKScript(hk, name, true, script));
        }

    }
}
