using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static TLHelper.Coords;

namespace TLHelper
{
    class HardwareRobot
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern void PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern void SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        const int WM_MOUSEMOVE = 0x200;
        const int WM_LBUTTONDOWN = 0x201;
        const int WM_LBUTTONUP = 0x202;
        const int WM_RBUTTONDOWN = 0x204;
        const int WM_RBUTTONUP = 0x205;

        public static void DoMouseClick(int x, int y, bool left = true)
        {
            //Call the imported function with the cursor's current position
            IntPtr diablo3Handle = ScreenTools.d3WindowHandle;
            if (left)
            {
                //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                SendMessage(diablo3Handle, WM_LBUTTONDOWN, 1, MakeLParam(x, y));
                SendMessage(diablo3Handle, WM_LBUTTONUP, 0, MakeLParam(x, y));
            }
            else
            {
                //mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
                PostMessage(diablo3Handle, WM_RBUTTONDOWN, 2, MakeLParam(x, y));
                PostMessage(diablo3Handle, WM_RBUTTONUP, 0, MakeLParam(x, y));
            }
        }

        private static int MakeLParam(int LoWord, int HiWord)
        {
            return (int)((HiWord << 16) | (LoWord & 0xFFFF));
        }

        public static void DoMouseDown(bool left = true)
        {
            //Call the imported function with the cursor's current position
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            if (left)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
            }
            else
            {
                mouse_event(MOUSEEVENTF_RIGHTDOWN, X, Y, 0, 0);
            }
        }

        public static void DoMouseUp(bool left = true)
        {
            //Call the imported function with the cursor's current position
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            if (left)
            {
                mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
            }
            else
            {
                mouse_event(MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
            }
        }

        public static void DoMouseClick(Position p, bool left=true)
        {
            DoMouseClick(p.x, p.y, left);
        }

        public static void DoMouseClick(Coord c, bool left = true)
        {
            DoMouseClick(c.RealX, c.RealY, left);
        }

        public static void MoveCursor(int x, int y)
        {
            var C = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new System.Drawing.Point(x, y);
        }

        const UInt32 WM_KEYDOWN = 0x0100;
        const UInt32 WM_KEYUP = 0x0101;
        public static void PressKey(char keyChar)
        {
            KeyDown(keyChar);
            Thread.Sleep(5);
            KeyUp(keyChar);
        }
        public static void PressKey(int keyCode)
        {
            PressKey((char)keyCode);
        }

        public static void KeyDown(char keyChar)
        {
            IntPtr d3WindowHandle = ScreenTools.d3WindowHandle;
            if (d3WindowHandle == IntPtr.Zero) return;

            PostMessage(d3WindowHandle, WM_KEYDOWN, (int)keyChar, 0);
        }
        public static void KeyDown(int keyCode)
        {
            KeyDown((char)keyCode);
        }

        public static void KeyUp(char keyChar)
        {
            IntPtr d3WindowHandle = ScreenTools.d3WindowHandle;
            if (d3WindowHandle == IntPtr.Zero) return;

            PostMessage(d3WindowHandle, WM_KEYUP, (int)keyChar, 0);
        }
        public static void KeyUp(int keyCode)
        {
            KeyUp((char)keyCode);
        }

        public static class KeyCodes
        {
            public const int ALT = 0x00A4;
            public const int CTRL = 0x00A2;
            public const int SHIFT = 0x00A0;
            public const int CTRL1 = 0x00A3;
            public const int CTRL2 = 0x0011;
            public const int CTRL3 = 0x2000;
        }

    }
}
