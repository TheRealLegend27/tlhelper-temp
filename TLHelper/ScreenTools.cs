using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace TLHelper
{
    class ScreenTools
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef hWnd, out Rect lpRect);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
        public static IntPtr FindWindowByCaption(string caption) { return FindWindowByCaption(IntPtr.Zero, caption); }
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
        // GET PIXEL COLOR OF WINDOW
        public static IntPtr d3WindowHandle = IntPtr.Zero;
        public static (Color, bool) GetPixelColor(int x, int y, string windowTitle = "Diablo III")
        {
            if (d3WindowHandle == IntPtr.Zero)
                d3WindowHandle = FindWindowByCaption(windowTitle);
            if (d3WindowHandle == IntPtr.Zero) return (Color.Transparent, false);
            IntPtr hdc = GetDC(d3WindowHandle);
            uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(d3WindowHandle, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF),
                            (int)(pixel & 0x0000FF00) >> 8,
                            (int)(pixel & 0x00FF0000) >> 16);
            return (color, true);
        }

        // GET DIMENSIONS OF WINDOW
        public static (Dim, bool) GetWindowDimensions(string windowTitle="Diablo III")
        {
            IntPtr hwnd = FindWindowByCaption(windowTitle);
            if (hwnd == IntPtr.Zero) return (new Dim(), false);
            Rect rect;
            GetWindowRect(new HandleRef(new Object(), hwnd), out rect);
            Dim dims = new Dim();
            dims.Top = rect.Top;
            dims.Left = rect.Left;
            dims.Right = rect.Right;
            dims.Bottom = rect.Bottom;
            dims.Width = dims.Right - dims.Left;
            dims.Height = dims.Bottom - dims.Top;
            return (dims, true);
        }

        // DIABLO III IN FOREGROUND
        public static Boolean IsDiabloFocused()
        {
            String title = GetActiveWindowTitle() ?? "";
            return title.Equals("Diablo III");
        }

        public static Boolean IsInRift()
        {
            return GetPixelColor(1568, 529).Item1.Equals(Color.FromArgb(48, 46, 34)) ||
                GetPixelColor(1568, 602).Item1.Equals(Color.FromArgb(35, 32, 24));
        }

        public static Boolean IsPorting()
        {
            return GetPixelColor(860, 323).Item1.Equals(Color.FromArgb(30, 26, 23));
        }

        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        public struct Dim
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public int Width;
            public int Height;
        }
    }
}
