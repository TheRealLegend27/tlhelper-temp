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
            }
            return "error";
        }

        public static AvailableFunction GetFunction(AvailableType type)
        {
            if (type == AvailableType.Trigger) return Trigger;
            if (type == AvailableType.InActive) return ByColor;
            if (type == AvailableType.Potion) return Potion;
            if (type == AvailableType.FullEssence) return FullEssence;
            return Error;
        }

        public static bool Error (int i, Color c) { Console.Error.WriteLine("Unknown AvailableType!"); return false; }

    static readonly Color DefaultProfile = Color.FromArgb(83, 85, 66);
        static readonly Color PotionColor = Color.FromArgb(1, 1, 1);
        static readonly int PotionMinRed = 100;

#pragma warning disable IDE0060 // Nicht verwendete Parameter entfernen
        public static bool Trigger(int skillSlot, Color pxl) => true;
#pragma warning restore IDE0060 // Nicht verwendete Parameter entfernen

        public static bool ByColor(int skillSlot, Color pxl)
        {
            bool isMouse = skillSlot == 1 || skillSlot == 0;
            if (isMouse) return false;
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
        public static bool FullEssence(int skillSlot, Color pxl) => false;
#pragma warning restore IDE0060 // Nicht verwendete Parameter entfernen
    }
}
