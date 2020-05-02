using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TLHelper.HotKeys;
using TLHelper.Skills;
using TLHelper.SysCom;

namespace TLHelper.Scripts
{
    public static class HotkeyManager
    {
        private static Dictionary<int, string> KeyboardHotkeys = new Dictionary<int, string>();
        private static Dictionary<(MouseButtons, bool, bool, bool), string> MouseHotkeys =
            new Dictionary<(MouseButtons, bool, bool, bool), string>();

        public static void RegisterKey(HotKey key, string id)
        {
            if (key.IsMouse)
                MouseHotkeys.Add((key.CurrentKey.AsMouseButton(), key.IsCtrl, key.IsShift, key.IsAlt), id);
            else
                KeyboardHotkeys.Add(HardwareListener.AddAction(key.AddonKeys, key.CurrentKey.CurrentKey), id);
        }

        public static void ProcessKeyboardAction(int id)
        {
            if (KeyboardHotkeys[id].StartsWith("active-mode")) ActiveMode.KeyPressed(KeyboardHotkeys[id]);
            else ScriptManager.Run(KeyboardHotkeys[id]);
        }
        public static void ProcessMouseAction(MouseButtons button, bool ctrlDown, bool shiftDown, bool altDown)
        {
            var ptId = (button, ctrlDown, shiftDown, altDown);
            if (MouseHotkeys.ContainsKey(ptId))
                ScriptManager.Run(MouseHotkeys[ptId]);
        }

    }
}
