using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TLHelper.Coords.Coords;

namespace TLHelper.SysCom
{
    public static class HardwareRobot
    {
        #region WM_Keys
        private static class WM
        {
            //Mouse actions
            public const int MOUSEEVENTF_LEFTDOWN = 0x02;
            public const int MOUSEEVENTF_LEFTUP = 0x04;
            public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
            public const int MOUSEEVENTF_RIGHTUP = 0x10;

            // WM
            public const int WM_MOUSEMOVE = 0x200;
            public const int WM_LBUTTONDOWN = 0x201;
            public const int WM_LBUTTONUP = 0x202;
            public const int WM_RBUTTONDOWN = 0x204;
            public const int WM_RBUTTONUP = 0x205;

            // BASIC
            public const uint WM_KEYDOWN = 0x0100;
            public const uint WM_KEYUP = 0x0101;
        }
        #endregion WM_Keys

        #region DLLImports
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern void PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern void SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        #endregion DLLImports

        #region ActionTypes
        public enum ActionTypes
        {
            SIMULATE,
            PHYSICAL
        }
        #endregion ActionTypes

        #region Helper Functions

        private static int MakeLParam(int LoWord, int HiWord)
        {
            return (HiWord << 16) | (LoWord & 0xFFFF);
        }

        #endregion Helper Functions

        #region DoMouseClick

        public static void DoRightClick() => DoRightClick(Cursor.Position.X, Cursor.Position.Y);
        public static void DoRightClick(Position p, ActionTypes actionType = ActionTypes.SIMULATE) =>
            DoRightClick(p.x, p.y, actionType);
        public static void DoRightClick(Coord c, ActionTypes actionType = ActionTypes.SIMULATE) =>
            DoRightClick(c.RealX, c.RealY, actionType);
        public static void DoRightClick(int x, int y, ActionTypes actionType = ActionTypes.SIMULATE) =>
            DoMouseClick(x, y, false, actionType);
        public static void DoRightDown(int x, int y, ActionTypes actionType = ActionTypes.SIMULATE) =>
            DoMouseDown(x, y, false, actionType);
        public static void DoRightUp(int x, int y, ActionTypes actionType = ActionTypes.SIMULATE) =>
            DoMouseUp(x, y, false, actionType);

        public static void DoLeftClick() => DoLeftClick(Cursor.Position.X, Cursor.Position.Y);
        public static void DoLeftClick(Position p, ActionTypes actionType = ActionTypes.SIMULATE) =>
            DoLeftClick(p.x, p.y, actionType);
        public static void DoLeftClick(Coord c, ActionTypes actionType = ActionTypes.SIMULATE) =>
            DoLeftClick(c.RealX, c.RealY, actionType);
        public static void DoLeftClick(int x, int y, ActionTypes actionType = ActionTypes.SIMULATE) =>
            DoMouseClick(x, y, true, actionType);
        public static void DoLeftDown(int x, int y, ActionTypes actionType = ActionTypes.SIMULATE) =>
            DoMouseDown(x, y, true, actionType);
        public static void DoLeftUp(int x, int y, ActionTypes actionType = ActionTypes.SIMULATE) =>
            DoMouseUp(x, y, true, actionType);
        // PRIVATE HANDLERS
        private static void DoMouseClick(int x, int y, bool left, ActionTypes actionType)
        {
            if (actionType == ActionTypes.PHYSICAL)
            {
                uint wm;
                uint _x = (uint)x;
                uint _y = (uint)y;

                if (left)
                    wm = WM.MOUSEEVENTF_LEFTDOWN | WM.MOUSEEVENTF_LEFTUP;
                else
                    wm = WM.MOUSEEVENTF_RIGHTDOWN | WM.MOUSEEVENTF_RIGHTUP;

                mouse_event(wm, _x, _y, 0, 0);
            }
            else
            {
                DoMouseDown(x, y, left, actionType);
                DoMouseUp(x, y, left, actionType);
            }
        }
        private static void DoMouseDown(int x, int y, bool left, ActionTypes actionType)
        {
            if (actionType == ActionTypes.SIMULATE)
            {
                IntPtr d3Handle = ScreenTools.D3Handle;

                uint wm;

                if (left)
                    wm = WM.WM_LBUTTONDOWN;
                else
                    wm = WM.WM_RBUTTONDOWN;

                PostMessage(d3Handle, wm, left ? 1 : 2, MakeLParam(x, y));

            }
            else if (actionType == ActionTypes.PHYSICAL)
            {
                uint wm;

                uint _x = (uint)x;
                uint _y = (uint)y;

                if (left)
                    wm = WM.MOUSEEVENTF_LEFTDOWN;
                else
                    wm = WM.MOUSEEVENTF_RIGHTDOWN;

                mouse_event(wm, _x, _y, 0, 0);
            }
        }
        private static void DoMouseUp(int x, int y, bool left, ActionTypes actionType)
        {
            if (actionType == ActionTypes.SIMULATE)
            {
                IntPtr d3Handle = ScreenTools.D3Handle;

                uint wm;

                if (left)
                    wm = WM.WM_LBUTTONUP;
                else
                    wm = WM.WM_RBUTTONUP;

                PostMessage(d3Handle, wm, 0, MakeLParam(x, y));

            }
            else if (actionType == ActionTypes.PHYSICAL)
            {
                uint wm;

                uint _x = (uint)x;
                uint _y = (uint)y;

                if (left)
                    wm = WM.MOUSEEVENTF_LEFTUP;
                else
                    wm = WM.MOUSEEVENTF_RIGHTUP;

                mouse_event(wm, _x, _y, 0, 0);
            }
        }

        #endregion DoMouseClick

        #region PressKey

        public static void PressKey(int key) => PressKey((char)key);
        public static void PressKey(char key)
        {
            KeyDown(key);
            Thread.Sleep(3);
            KeyUp(key);
        }

        public static void KeyDown(int key) => KeyDown((char)key);
        public static void KeyDown(char key)
        {
            IntPtr d3Handle = ScreenTools.D3Handle;
            if (d3Handle == IntPtr.Zero) return;
            SendMessage(d3Handle, WM.WM_KEYDOWN, (int)key, 0);
        }

        public static void KeyUp(int key) => KeyUp((char)key);
        public static void KeyUp(char key)
        {
            IntPtr d3Handle = ScreenTools.D3Handle;
            if (d3Handle == IntPtr.Zero) return;
            SendMessage(d3Handle, WM.WM_KEYUP, (int)key, 0);
        }

        #endregion PressKey

        #region MoveCursor

        public static void MovePhysicalCursor(Position p) => MovePhysicalCursor(p.x, p.y);
        public static void MovePhysicalCursor(Coord c) => MovePhysicalCursor(c.RealX, c.RealY);
        public static void MovePhysicalCursor(int x, int y) => Cursor.Position = new System.Drawing.Point(x, y);

        #endregion MoveCurser
    }
}
