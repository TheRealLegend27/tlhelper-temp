using System.Drawing;

namespace TLHelper.UI
{
    public static class Theme
    {
        public static readonly Color Background = ColorTranslator.FromHtml("#FFFFFF");
        public static readonly Color Foreground = ColorTranslator.FromHtml("#333333");
        public static readonly Color Gray = ColorTranslator.FromHtml("#999999");
        public static readonly Color Accent = ColorTranslator.FromHtml("#3285a8");
        public static readonly Color AccentLight = ColorTranslator.FromHtml("#6db5d3");
        public static readonly Color AccentLighter = ColorTranslator.FromHtml("#bbddeb");

        public static class Fonts
        {
            static Fonts()
            {
                FontFamily fontFamily = new FontFamily("Calibri");
                H1 = new Font(fontFamily, 30, FontStyle.Bold, GraphicsUnit.Pixel);
                H2 = new Font(fontFamily, 26, FontStyle.Bold, GraphicsUnit.Pixel);
                H3 = new Font(fontFamily, 22, FontStyle.Bold, GraphicsUnit.Pixel);
                H4 = new Font(fontFamily, 18, FontStyle.Bold, GraphicsUnit.Pixel);
                H5 = new Font(fontFamily, 16, FontStyle.Bold, GraphicsUnit.Pixel);
                H6 = new Font(fontFamily, 12, FontStyle.Bold, GraphicsUnit.Pixel);
                P = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
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
