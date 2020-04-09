using System;
using System.Drawing;

namespace TLHelper
{
    class AvailableFunction
    {
        //static readonly Color Profile1 = Color.FromArgb(25, 29, 24);
        //static readonly Color Profile2 = Color.FromArgb(27, 31, 26);
        //static readonly Color Profile3 = Color.FromArgb(25, 25, 17);
        //static readonly Color Profile4 = Color.FromArgb(27, 27, 18);
        //static readonly Color Profile5 = Color.FromArgb(25, 29, 24);

        static readonly Color Profile1 = Color.FromArgb(83, 85, 66);
        static readonly Color Profile2 = Color.FromArgb(83, 85, 66);
        static readonly Color Profile3 = Color.FromArgb(83, 85, 66);
        static readonly Color Profile4 = Color.FromArgb(83, 85, 66);
        static readonly Color Profile5 = Color.FromArgb(83, 85, 66);

        static readonly Color Mouse1 = Color.FromArgb(28, 32, 27);
        static readonly Color Mouse2 = Color.FromArgb(29, 33, 28);
        static readonly Color Mouse3 = Color.FromArgb(28, 28, 19);
        static readonly Color Mouse4 = Color.FromArgb(29, 29, 20);
        static readonly Color Mouse5 = Color.FromArgb(195, 179, 63);

        static readonly Color PotionColor = Color.FromArgb(1, 1, 1);

        public static bool Trigger(bool isMouse, Color pxl) => true;

        public static bool ColorProfile1(bool isMouse, Color pxl)
        {
            if (isMouse) return pxl.Equals(Mouse1);
            else return pxl.Equals(Profile1);
        }
        public static bool ColorProfile2(bool isMouse, Color pxl)
        {
            if (isMouse) return pxl.Equals(Mouse2);
            else return pxl.Equals(Profile2);
        }
        public static bool ColorProfile3(bool isMouse, Color pxl)
        {
            if (isMouse) return pxl.Equals(Mouse3);
            else return pxl.Equals(Profile3);
        }
        public static bool ColorProfile4(bool isMouse, Color pxl)
        {
            if (isMouse) return pxl.Equals(Mouse4);
            else return pxl.Equals(Profile4);
        }
        public static bool ColorProfile5(bool isMouse, Color pxl)
        {
            if (isMouse) return pxl.Equals(Mouse5);
            else return pxl.Equals(Profile5);
        }

        public static bool Potion(bool isMouse, Color pxl)
        {
            if (!ScreenTools.GetPixelColor(1060, 1000).Item1.Equals(PotionColor)) return false;
            return pxl.R < 100;
        }

        public static bool FullEssence(bool isMouse, Color pxl) => false;
    }
}
