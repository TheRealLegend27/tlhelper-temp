using Gma.UserActivityMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TLHelper.Scripts;

namespace TLHelper.SysCom
{
    public static class HardwareListener
    {
        #region DLLImports
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        #endregion DLLImports

        private static IntPtr MainHandle;
        private static int LastId = -1;

        public static void Init(IntPtr handle)
        {
            MainHandle = handle;
            RegisterMouseHooks();
        }

        public static void RegisterMouseHooks()
        {
            HookManager.MouseDown += MouseDownAction;
            HookManager.MouseUp += MouseUpAction;
        }
        public static void UnregisterMouseHooks()
        {
            HookManager.MouseDown -= MouseDownAction;
            HookManager.MouseUp -= MouseUpAction;
        }

        public static int AddAction(int key_codes, Keys key)
        {
            LastId++;
            RegisterHotKey(MainHandle, LastId, key_codes, (int)key);
            return LastId;
        }

        public static void Action(Message m)
        {
            if (m.Msg == 0x0312)
            {
                if (!ScreenTools.IsDiabloFocused()) return;
                HotkeyManager.ProcessKeyboardAction(m.WParam.ToInt32());
            }
        }

        public static bool IsLButtonDown { get; private set; } = false;
        public static bool IsRButtonDown { get; private set; } = false;
        public static bool IsMButtonDown { get; private set; } = false;
        public static bool IsXButton1Down { get; private set; } = false;
        public static bool IsXButton2Down { get; private set; } = false;

        private static void MouseUpAction(object Sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) IsLButtonDown = false;
            else if (e.Button == MouseButtons.Right) IsRButtonDown = false;
            else if (e.Button == MouseButtons.Middle) IsMButtonDown = false;
            else if (e.Button == MouseButtons.XButton1) IsXButton1Down = false;
            else if (e.Button == MouseButtons.XButton2) IsXButton2Down = false;
        }
        private static void MouseDownAction(object Sender, MouseEventArgs e)
        {
            if (!ScreenTools.IsDiabloFocused()) return;
            if (e.Button == MouseButtons.Left) IsLButtonDown = true;
            else if (e.Button == MouseButtons.Right) IsRButtonDown = true;
            else if (e.Button == MouseButtons.Middle) IsMButtonDown = true;
            else if (e.Button == MouseButtons.XButton1) IsXButton1Down = true;
            else if (e.Button == MouseButtons.XButton2) IsXButton2Down = true;

            HotkeyManager.ProcessMouseAction(e.Button, IsCtrlDown, IsShiftDown, IsAltDown);
        }

        public static bool IsCtrlDown { get { return Control.ModifierKeys.HasFlag(Keys.Control); } }
        public static bool IsShiftDown { get { return Control.ModifierKeys.HasFlag(Keys.Shift); } }
        public static bool IsAltDown { get { return Control.ModifierKeys.HasFlag(Keys.Alt); } }
    }
}
