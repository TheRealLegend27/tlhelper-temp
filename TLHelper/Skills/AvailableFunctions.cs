using System;
using System.Drawing;
using TLHelper.SysCom;

namespace TLHelper.Skills
{
    public static class AvailableFunctions
    {
        public delegate bool AvailableFunction(int skillSlot, Color pxl);

        public enum AvailableType
        {
            Trigger,
            InActive,
            Potion,
            FullEssence,
            SkeletalMage,
            Error
        }

        public static AvailableType ParseType(string s)
        {
            switch (s.ToLower())
            {
                case "trigger": return AvailableType.Trigger;
                case "inactive": return AvailableType.InActive;
                case "potion": return AvailableType.Potion;
                case "fullessence": return AvailableType.FullEssence;
                case "skeletalmage": return AvailableType.SkeletalMage;
            }
            return AvailableType.Error;
        }

        public static String StringifyType(AvailableType type)
        {
            switch (type)
            {
                case AvailableType.Trigger: return "trigger";
                case AvailableType.InActive: return "inactive";
                case AvailableType.Potion: return "potion";
                case AvailableType.FullEssence: return "fullessence";
                case AvailableType.SkeletalMage: return "skeletalmage";
            }
            return "error";
        }

        public static AvailableFunction GetFunction(AvailableType type)
        {
            if (type == AvailableType.Trigger) return Trigger;
            if (type == AvailableType.InActive) return ByColor;
            if (type == AvailableType.Potion) return Potion;
            if (type == AvailableType.FullEssence) return FullEssence;
            if (type == AvailableType.SkeletalMage) return SkeletalMage;
            return Error;
        }

        public static bool Error(int i, Color c) { Console.Error.WriteLine("Unknown AvailableType!"); return false; }

        static readonly Color DefaultProfile = Color.FromArgb(83, 85, 66);
        static readonly Color MouseProfile = Color.FromArgb(28, 32, 27);
        static readonly Color PotionColor = Color.FromArgb(1, 1, 1);
        static readonly int PotionMinRed = 100;
        static readonly Color EssenceBase = Color.FromArgb(49, 160, 160);
        static readonly int EssenceVariance = 60;

#pragma warning disable IDE0060 // Nicht verwendete Parameter entfernen
        public static bool Trigger(int skillSlot, Color pxl) => true;
#pragma warning restore IDE0060 // Nicht verwendete Parameter entfernen

        public static bool ByColor(int skillSlot, Color pxl)
        {
            bool isMouse = skillSlot == 1 || skillSlot == 0;
            if (isMouse)
            {
                return pxl.Equals(MouseProfile);
            }
            else
            {
                return pxl.Equals(DefaultProfile);
            }
        }

#pragma warning disable IDE0060 // Nicht verwendete Parameter entfernen
        public static bool Potion(int skillSlot, Color pxl)
#pragma warning restore IDE0060 // Nicht verwendete Parameter entfernen
        {
            int px = Coords.Coords.PotionSkill.x;
            int py = Coords.Coords.PotionSkill.y;
            if (!ScreenTools.GetPixelColor(px, py).Item1.Equals(PotionColor)) return false;
            return pxl.R < PotionMinRed;
        }

#pragma warning disable IDE0060 // Nicht verwendete Parameter entfernen
        public static bool FullEssence(int skillSlot, Color pxl)
        {
            int px = Coords.Coords.ResourceCheck.x;
            int py = Coords.Coords.ResourceCheck.y;
            Color essCol1 = ScreenTools.GetPixelColor(px, py).Item1;
            Color essCol2 = ScreenTools.GetPixelColor(px + 10, py).Item1;
            Color essCol3 = ScreenTools.GetPixelColor(px - 10, py).Item1;

            if (Math.Abs(essCol1.R - EssenceBase.R) > EssenceVariance) return false;
            if (Math.Abs(essCol1.G - EssenceBase.G) > EssenceVariance) return false;
            if (Math.Abs(essCol1.B - EssenceBase.B) > EssenceVariance) return false;

            if (Math.Abs(essCol2.R - EssenceBase.R) > EssenceVariance) return false;
            if (Math.Abs(essCol2.G - EssenceBase.G) > EssenceVariance) return false;
            if (Math.Abs(essCol2.B - EssenceBase.B) > EssenceVariance) return false;

            if (Math.Abs(essCol3.R - EssenceBase.R) > EssenceVariance) return false;
            if (Math.Abs(essCol3.G - EssenceBase.G) > EssenceVariance) return false;
            if (Math.Abs(essCol3.B - EssenceBase.B) > EssenceVariance) return false;


            return true;
        }
#pragma warning restore IDE0060 // Nicht verwendete Parameter entfernen

        private static int LastMage = 0;
        private static int MinMageDelay = 1500;

        public static bool SkeletalMage(int skillSlot, Color pxl)
        {
            if (Environment.TickCount - LastMage <= MinMageDelay) return false;
            if (FullEssence(skillSlot, pxl))
            {
                LastMage = Environment.TickCount;
                return true;
            }
            return false;
        }

    }
}
