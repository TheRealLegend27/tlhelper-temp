using Gma.UserActivityMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using TLHelper.Hotkeys;

namespace TLHelper
{
    class HardwareListener
    {
        // DLL libraries used to manage hotkeys
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private static IntPtr Handle;
        private static int lastId = -1;

        public static void Init(IntPtr handle)
        {
            Handle = handle;
            RegisterMouseHooks();
            // REGISTER KEYBOARD HOOKS
            HookManager.KeyDown += KeyDownAction;
            HookManager.KeyUp += KeyUpAction;
        }

        public static void RegisterMouseHooks()
        {
            HookManager.MouseDown += MouseDownAction;
            HookManager.MouseUp += MouseUpAction;
            Console.WriteLine("Mouse Hooks registered");
        }
        public static void UnregisterMouseHooks()
        {
            HookManager.MouseDown -= MouseDownAction;
            HookManager.MouseUp -= MouseUpAction;
            Console.WriteLine("Mouse Hooks unregistered");
        }

        private static void Test(object Sender, MouseEventArgs e)
        {
            Console.WriteLine("Test");
        }

        public static int AddAction(int key_codes, Keys key)
        {
            lastId++;
            RegisterHotKey(Handle, lastId, key_codes, (int)key);
            return lastId;
        }

        public static void Action(Message m)
        {
            if (m.Msg == 0x0312)
            {
                if (!ScreenTools.IsDiabloFocused()) return;
                HotkeyRegistry.ProcessAction(m.WParam.ToInt32());
            }
        }

        public static bool IsLButtonDown { get; private set; } = false;
        public static bool IsRButtonDown { get; private set; } = false;

        private static void MouseUpAction(object Sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) IsLButtonDown = false;
            else if (e.Button == MouseButtons.Right) IsRButtonDown = false;
        }
        private static void MouseDownAction(object Sender, MouseEventArgs e)
        {
            if (!ScreenTools.IsDiabloFocused()) return;
            if (e.Button == MouseButtons.Left) IsLButtonDown = true;
            else if (e.Button == MouseButtons.Right) IsRButtonDown = true;
            HotkeyRegistry.ProcessMouse(e.Button, IsCtrlDown, IsShiftDown, IsAltDown);
        }

        public static bool IsCtrlDown { get; private set; } = false;
        public static bool IsShiftDown { get; private set; } = false;
        public static bool IsAltDown { get; private set; } = false;

        private static void KeyUpAction(object Sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.LControlKey: IsCtrlDown = false; break;
                case Keys.LMenu: IsAltDown = false; break;
                case Keys.LShiftKey: IsShiftDown = false; break;
            }
        }
        private static void KeyDownAction(object Sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.LControlKey: IsCtrlDown = true; break;
                case Keys.LMenu: IsAltDown = true; break;
                case Keys.LShiftKey: IsShiftDown = true; break;
            }
        }

        public static void RefreshKeys()
        {
            IsCtrlDown = Control.ModifierKeys.HasFlag(Keys.Control);
            IsAltDown = Control.ModifierKeys.HasFlag(Keys.Alt);
            IsShiftDown = Control.ModifierKeys.HasFlag(Keys.Shift);
        }
    }
}
