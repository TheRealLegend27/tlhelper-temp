using System.Drawing;

namespace TLHelper.UI
{
    public static class Theme
    {
        public static readonly Color Background = Color.FromArgb(240, 240, 240);
        public static readonly Color Foreground = Color.FromArgb(50, 50, 50);
        public static readonly Color Accent = Color.FromArgb(5, 100, 200);

        public static class Fonts
        {
            static Fonts()
            {
                FontFamily fontFamily = new FontFamily("Calibri");
                H1 = new Font(fontFamily, 30, FontStyle.Bold, GraphicsUnit.Pixel);
                H2 = new Font(fontFamily, 26, FontStyle.Bold, GraphicsUnit.Pixel);
                H3 = new Font(fontFamily, 22, FontStyle.Bold, GraphicsUnit.Pixel);
                H4 = new Font(fontFamily, 18, FontStyle.Bold, GraphicsUnit.Pixel);
                H5 = new Font(fontFamily, 14, FontStyle.Bold, GraphicsUnit.Pixel);
                H6 = new Font(fontFamily, 12, FontStyle.Bold, GraphicsUnit.Pixel);
                H6 = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
            }

            public static readonly Font H1;
            public static readonly Font H2;
            public static readonly Font H3;
            public static readonly Font H4;
            public static readonly Font H5;
            public static readonly Font H6;
            public static readonly Font P;
        }

    }
}
