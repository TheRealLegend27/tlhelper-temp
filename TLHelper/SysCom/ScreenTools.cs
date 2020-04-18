using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace TLHelper.SysCom
{
    public static class ScreenTools
    {
        #region DLLImports
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
        #endregion DLLImports

        public static readonly string DiabloWindowTitle = "Diablo III";

        /// <summary>
        /// Get Title of currently selected Window
        /// </summary>
        /// <returns>Window Title or NULL if no Window selected</returns>
        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
                return Buff.ToString();
            return null;
        }

        public static IntPtr D3Handle { get; private set; } = IntPtr.Zero;

        /// <summary>
        /// Load Handle by Title
        /// </summary>
        /// <returns></returns>
        public static bool LoadWindowHandle()
        {
            D3Handle = FindWindowByCaption(DiabloWindowTitle);
            if (D3Handle == IntPtr.Zero) return false;
            return true;
        }

        /// <summary>
        /// Get PixelColor inside Diablo-Window
        /// </summary>
        /// <param name="x">X-Coordinate</param>
        /// <param name="y">Y-Coordinate</param>
        /// <returns>(color, success)</returns>
        public static (Color, bool) GetPixelColor(int x, int y)
        {
            // CHECK IF HANDLE IS SET
            if (D3Handle == IntPtr.Zero)
                if (!LoadWindowHandle()) // TRY TO GET HANDLE
                    return (Color.Transparent, false);  // RETURN FALSE IF HANDLE NOT FOUND

            // GET DC FOR HANDLE
            IntPtr hdc = GetDC(D3Handle);
            // READ COLOR IN BYTE
            uint pixel = GetPixel(hdc, x, y);
            // RELEASE DC
            ReleaseDC(D3Handle, hdc);
            // CONVERT COLOR TO ARGB
            Color color = Color.FromArgb(
                (int)(pixel & 0x000000FF),
                (int)(pixel & 0x0000FF00) >> 8,
                (int)(pixel & 0x00FF0000) >> 16
            );
            // RETURN COLOR AND TRUE
            return (color, true);
        }

        /// <summary>
        /// Gets the Dimensions of Diablo-Window
        /// </summary>
        /// <returns>Window Dimensions</returns>
        public static (Dim, bool) GetWindowDimensions()
        {
            // CHECK IF HANDLE IS SET
            if (D3Handle == IntPtr.Zero)
                if (!LoadWindowHandle()) // TRY TO GET HANDLE
                    return (new Dim(), false);  // RETURN FALSE IF HANDLE NOT FOUND
            // GET WINDOW RECT
            GetWindowRect(new HandleRef(new object(), D3Handle), out Rect rect);
            // CONVERT RECT TO DIMENSION
            Dim dims = new Dim
            {
                Top = rect.Top,
                Left = rect.Left,
                Right = rect.Right,
                Bottom = rect.Bottom,
                Width = rect.Right - rect.Left,
                Height = rect.Bottom - rect.Top
            };
            return (dims, true);
        }
        /// <summary>
        /// Finds out if Diablo-Window is in Focus
        /// </summary>
        /// <returns>Is Diablo in Focus</returns>
        public static bool IsDiabloFocused()
        {
            string title = GetActiveWindowTitle() ?? "";
            return title.Equals(DiabloWindowTitle);
        }

        /// <summary>
        /// Finds out if Player is in a Rift or GR
        /// </summary>
        /// <returns>Player in Rift or GR</returns>
        public static bool IsInRift()
        {
            return GetPixelColor(1568, 529).Item1.Equals(Color.FromArgb(48, 46, 34)) ||
                GetPixelColor(1568, 602).Item1.Equals(Color.FromArgb(35, 32, 24));
        }

        /// <summary>
        /// Finds out if Player is currently porting or using Townportal
        /// </summary>
        /// <returns>Player is Porting</returns>
        public static bool IsPorting() =>
            GetPixelColor(860, 323).Item1.Equals(Color.FromArgb(30, 26, 23));

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
