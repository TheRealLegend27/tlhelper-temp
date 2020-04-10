using System;
using System.Drawing;

namespace TLHelper
{
    class AvailableFunction
    {
        static readonly Color Profile1 = Color.FromArgb(83, 85, 66);
        static readonly Color PotionColor = Color.FromArgb(1, 1, 1);

        public static bool Trigger(int skillSlot, Color pxl) => true;

        public static bool ByColor(int skillSlot, Color pxl)
        {
            bool isMouse = skillSlot == 1 || skillSlot == 0;
            if (isMouse) return false;
            else
            {
                return pxl.Equals(Profile1);
            }
        }

        public static bool Potion(int skillSlot, Color pxl)
        {
            if (!ScreenTools.GetPixelColor(1060, 1000).Item1.Equals(PotionColor)) return false;
            return pxl.R < 100;
        }

        public static bool FullEssence(int skillSlot, Color pxl) => false;
    }
}
