using System;
using System.IO;
using System.Windows.Forms;
using TLHelper.HotKeys;
using TLHelper.SysCom;

namespace TLHelper.Scripts
{
    public static class ScriptCompiler
    {
        public static readonly string ScriptRoot =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\TLHelper\scripts\";

        public static void InterpretScript(string name)
        {
            if (!File.Exists(name)) return;
            string content = File.ReadAllText(name);
            content = content.Replace("\t", "");
            content = content.Replace("\r", "");
            while (content.IndexOf("  ") >= 0) content = content.Replace("  ", " ");

            string[] head = content.Split(new string[] { "::head", "::-head" }, 3, StringSplitOptions.None)[1].Split('\n');
            string[] script = content.Split(new string[] { "::script", "::-script" }, 3, StringSplitOptions.None)[1].Split('\n');

            var scriptName = "No Name";
            var scriptDescription = "A TL-Script";
            var scriptCtrl = false;
            var scriptShift = false;
            var scriptAlt = false;
            var scriptKey = char.MinValue;

            // READ HEAD
            foreach (string h in head)
            {
                var line = h;
                if (line.Length <= 2) continue;
                while (line.StartsWith(" ")) line = h.Substring(1);

                if (line.StartsWith("//") || line.StartsWith("#")) continue;

                line = line.Replace(": ", ":");
                line = line.Replace(" :", ":");

                string key = line.Split(new char[] { ':' }, 2)[0];
                string val = line.Split(new char[] { ':' }, 2)[1];

                switch (key)
                {
                    case "name":
                        scriptName = val;
                        break;
                    case "description":
                        scriptDescription = val;
                        break;
                    case "ctrl":
                        scriptCtrl = int.Parse(val) == 1;
                        break;
                    case "shift":
                        scriptShift = int.Parse(val) == 1;
                        break;
                    case "alt":
                        scriptAlt = int.Parse(val) == 1;
                        break;
                    case "key":
                        scriptKey = val.ToCharArray()[0];
                        break;
                }

            }
            string scriptLine = "";
            // READ SCRIPT
            foreach (string s in script)
            {
                var line = s;

                if (line.Length <= 2) continue;
                if (line.StartsWith("//") || line.StartsWith("#")) continue;
                line = line.Replace(" ", "");

                scriptLine += line + ";";
            }
            var id = Path.GetFileNameWithoutExtension(name);
            ScriptManager.AddScript(id, scriptName, new HotKey(new Key((Keys)(int)scriptKey), scriptCtrl, scriptShift, scriptAlt), true, ScriptOrigins.EXT, () => { RunScript(scriptLine); });

            Console.WriteLine("Added Script: " + name);
        }

        private static void RunScript(string script)
        {
            string[] calls = script.Split(';');
            foreach (string call in calls)
            {
                string command = call.Split(',')[0];
                string[] args = call.Replace(command + ",", "").Split(',');

                switch (command)
                {
                    case "send":
                        SendKeys.SendWait(args[0]);
                        break;
                    case "sleep":
                        InternalScripts.Sleep(int.Parse(args[0]));
                        break;
                    case "move":
                        HardwareRobot.MovePhysicalCursor(int.Parse(args[0]), int.Parse(args[1]));
                        break;
                    case "click":
                        for (int i = int.Parse(args[1]); i > 0; i--)
                            if (args[0] == "left")
                                HardwareRobot.DoLeftClick(Cursor.Position.X, Cursor.Position.Y, HardwareRobot.ActionTypes.PHYSICAL);
                            else
                                HardwareRobot.DoRightClick(Cursor.Position.X, Cursor.Position.Y, HardwareRobot.ActionTypes.PHYSICAL);
                        break;
                    case "unreg_mouse_hooks":
                        HardwareListener.UnregisterMouseHooks();
                        break;
                    case "reg_mouse_hooks":
                        HardwareListener.RegisterMouseHooks();
                        break;
                }

            }
        }
    }
}
