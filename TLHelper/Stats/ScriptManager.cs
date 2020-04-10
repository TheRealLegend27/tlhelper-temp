using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using TLHelper.Hotkeys;
using static TLHelper.Coords;
using static TLHelper.Hotkeys.HotkeyRegistry;

namespace TLHelper.Stats
{
    class ScriptManager
    {

        private static readonly int ScriptSleep = 40;
        private static readonly string ScriptRoot = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/TLHelper/scripts/";


        public static void AddScript(Hotkey hk, ActionHandle runScript)
        {
            RegisterKey(hk, runScript);
        }

        public static void Sleep(int addms = 0)
        {
            Thread.Sleep(ScriptSleep + addms);
        }

        public static void Init()
        {
            slt.Start();
            srt.Start();
        }

        public static (string, string, string) InterpretScript(string name)
        {
            if (!File.Exists(ScriptRoot + name)) return (null, null, null);
            string content = File.ReadAllText(ScriptRoot + name);
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
            var scriptKey = Char.MinValue;

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
                        scriptCtrl = Int32.Parse(val) == 1;
                        break;
                    case "shift":
                        scriptShift = Int32.Parse(val) == 1;
                        break;
                    case "alt":
                        scriptAlt = Int32.Parse(val) == 1;
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

            AddScript(new Hotkey(scriptCtrl, scriptShift, scriptAlt, scriptKey), () => { RunScript(scriptLine); });

            string hk = "";
            if (scriptCtrl) hk += "Ctrl + ";
            if (scriptShift) hk += "Shift + ";
            if (scriptAlt) hk += "Alt + ";
            hk += scriptKey;
            return (hk, scriptName, scriptDescription);
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
                        SendKeys.Send(args[0]);
                        break;
                    case "sleep":
                        Sleep(int.Parse(args[0]));
                        break;
                    case "move":
                        HardwareRobot.MoveCursor(int.Parse(args[0]), int.Parse(args[1]));
                        break;
                    case "click":
                        for (int i = int.Parse(args[1]); i>0; i--)
                            HardwareRobot.DoMouseClick(Cursor.Position.X, Cursor.Position.Y, args[0] == "left");
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

        // ACTION HANDLES

        public static void ClearInv1Space()
        {
            // DISABLE MOUSE HOOKS TO PREVENT LAG
            HardwareListener.UnregisterMouseHooks();

            HardwareRobot.DoMouseClick(coords["smith_salvage"].RealX, coords["smith_salvage"].RealY);

            InventoryIterator invIt = Inventory.Get1SlotIterator();
            while (invIt.HasNext())
            {
                Position p = invIt.getNext();
                HardwareRobot.MoveCursor(p.x, p.y);
                HardwareRobot.DoMouseDown();
                HardwareRobot.DoMouseUp();
                SendKeys.Send("{ENTER}");
                SendKeys.Send("{ENTER}");
            }

            // RE-ENABLE MOUSE HOOKS
            HardwareListener.RegisterMouseHooks();

            // RESCAN MODIFIER KEYS
            HardwareListener.RefreshKeys();
        }

        public static void ClearInv2Space()
        {
            // DISABLE MOUSE HOOKS TO PREVENT LAG
            HardwareListener.UnregisterMouseHooks();

            HardwareRobot.DoMouseClick(coords["smith_salvage"].RealX, coords["smith_salvage"].RealY);

            InventoryIterator invIt = Inventory.Get2SlotIterator();
            while (invIt.HasNext())
            {
                Position p = invIt.getNext();
                HardwareRobot.MoveCursor(p.x, p.y);
                HardwareRobot.DoMouseDown();
                HardwareRobot.DoMouseUp();
                SendKeys.Send("{ENTER}");
                SendKeys.Send("{ENTER}");
            }

            // RE-ENABLE MOUSE HOOKS
            HardwareListener.RegisterMouseHooks();

            // RESCAN MODIFIER KEYS
            HardwareListener.RefreshKeys();
        }

        public static void DoCube1Space()
        {
            // DISABLE MOUSE HOOKS TO PREVENT LAG
            HardwareListener.UnregisterMouseHooks();

            Coord cubeFill = coords["cube_fill"];
            Coord cubeTransute = coords["cube_transmute"];
            Position cubeLeft = SwitchPagesLeft;
            Position cubeRight = SwitchPagesRight;

            InventoryIterator invIt = Inventory.Get1SlotIterator();
            while (invIt.HasNext())
            {
                Position p = invIt.getNext();
                HardwareRobot.DoMouseClick(p, false);
                Sleep(0);
                HardwareRobot.DoMouseClick(cubeFill);
                Sleep(0);
                HardwareRobot.DoMouseClick(cubeTransute);
                Sleep(80);
                HardwareRobot.DoMouseClick(cubeRight);
                Sleep(0);
                HardwareRobot.DoMouseClick(cubeLeft);
                Sleep(0);
            }

            // RE-ENABLE MOUSE HOOKS
            HardwareListener.RegisterMouseHooks();

            // RESCAN MODIFIER KEYS
            HardwareListener.RefreshKeys();

        }

        public static void DoCube2Space()
        {
            // DISABLE MOUSE HOOKS TO PREVENT LAG
            HardwareListener.UnregisterMouseHooks();

            Coord cubeFill = coords["cube_fill"];
            Coord cubeTransute = coords["cube_transmute"];
            Position cubeLeft = SwitchPagesLeft;
            Position cubeRight = SwitchPagesRight;

            InventoryIterator invIt = Inventory.Get2SlotIterator();
            while (invIt.HasNext())
            {
                Position p = invIt.getNext();
                HardwareRobot.DoMouseClick(p, false);
                Sleep(0);
                HardwareRobot.DoMouseClick(cubeFill);
                Sleep(0);
                HardwareRobot.DoMouseClick(cubeTransute);
                Sleep(80);
                HardwareRobot.DoMouseClick(cubeRight);
                Sleep(0);
                HardwareRobot.DoMouseClick(cubeLeft);
                Sleep(0);
            }

            // RE-ENABLE MOUSE HOOKS
            HardwareListener.RegisterMouseHooks();

            // RESCAN MODIFIER KEYS
            HardwareListener.RefreshKeys();

        }

        public static void DropItems()
        {
            // DISABLE MOUSE HOOKS TO PREVENT LAG
            HardwareListener.UnregisterMouseHooks();

            Coord Drop = coords["drop_item"];
            InventoryIterator invIt = Inventory.Get1SlotIterator();

            SendKeys.SendWait("i");
            while (invIt.HasNext())
            {
                Position p = invIt.getNext();
                HardwareRobot.MoveCursor(p.x, p.y);
                Sleep(-25);
                HardwareRobot.DoMouseDown();
                HardwareRobot.MoveCursor(Drop.RealX, Drop.RealY);
                Sleep(-25);
                HardwareRobot.DoMouseUp();
                Sleep(-25);
            }
            SendKeys.SendWait("i");

            // RE-ENABLE MOUSE HOOKS
            HardwareListener.RegisterMouseHooks();

            // RESCAN MODIFIER KEYS
            HardwareListener.RefreshKeys();
        }

        public static void MoveInventory()
        {
            // DISABLE MOUSE HOOKS TO PREVENT LAG
            HardwareListener.UnregisterMouseHooks();

            InventoryIterator invIt = Inventory.Get1SlotIterator();
            while (invIt.HasNext())
            {
                Position p = invIt.getNext();
                HardwareRobot.DoMouseClick(p.x, p.y, false);
            }

            // RE-ENABLE MOUSE HOOKS
            HardwareListener.RegisterMouseHooks();

            // RESCAN MODIFIER KEYS
            HardwareListener.RefreshKeys();
        }

        private static EventWaitHandle sltWait = new ManualResetEvent(initialState: false);
        private static EventWaitHandle srtWait = new ManualResetEvent(initialState: false);

        private static Thread slt = new Thread(new ParameterizedThreadStart(SLT));
        private static Thread srt = new Thread(new ParameterizedThreadStart(SRT));

        private static bool sltRunning = false;
        private static bool srtRunning = false;
        public static void SpamLeft()
        {
            if (!sltRunning)
            {
                sltWait.Set();
                sltRunning = true;
            }
        }
        private static void SLT(object obj)
        {
            while (true)
            {
                if (!HardwareListener.IsAltDown)
                {
                    sltWait.Reset();
                    sltRunning = false;
                }
                sltWait.WaitOne();
                HardwareRobot.DoMouseClick(Cursor.Position.X, Cursor.Position.Y);
                Sleep(-30);
            }
        }
        public static void SpamRight()
        {
            if (!srtRunning)
            {
                srtWait.Set();
                srtRunning = true;
            }
        }
        private static void SRT(object obj)
        {
            while (true)
            {
                if (!HardwareListener.IsAltDown)
                {
                    srtWait.Reset();
                    srtRunning = false;
                }
                srtWait.WaitOne();
                HardwareRobot.DoMouseClick(Cursor.Position.X, Cursor.Position.Y, false);
                Sleep(-30);
            }
        }

    }
}
