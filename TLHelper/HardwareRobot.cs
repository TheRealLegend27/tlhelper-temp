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
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public static void DoMouseClick(bool left = true)
        {
            //Call the imported function with the cursor's current position
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            if (left)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
            }
            else
            {
                mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
            }
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

        public static void DoMouseClick(int x, int y, bool left=true)
        {
            MoveCursor(x, y);
            DoMouseClick(left);
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
            IntPtr d3WindowHandle = ScreenTools.d3WindowHandle;
            if (d3WindowHandle == IntPtr.Zero) return;

            PostMessage(d3WindowHandle, WM_KEYDOWN, (int)keyChar, 0);
            Thread.Sleep(5);
            PostMessage(d3WindowHandle, WM_KEYUP, (int)keyChar, 0);
        }

    }
}
