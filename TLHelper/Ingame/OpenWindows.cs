using IronOcr;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLHelper.SysCom;

namespace TLHelper.Ingame
{
    static class OpenWindows
    {

        private static (Point, Color)[] KadalaCheckPoints = new (Point, Color)[2];
        private static (Point, Color)[] SmithCheckPoints = new (Point, Color)[2];
        private static (Point, Color)[] UrshiCheckPoints = new (Point, Color)[2];

        static OpenWindows()
        {
            KadalaCheckPoints[0] = (new Point(293, 73), Color.FromArgb(94, 37, 133));
            KadalaCheckPoints[1] = (new Point(264, 75), Color.FromArgb(237, 200, 83));

            SmithCheckPoints[0] = (new Point(165, 304), Color.FromArgb(229, 182, 42));
            SmithCheckPoints[1] = (new Point(164, 283), Color.FromArgb(72, 71, 89));

            UrshiCheckPoints[0] = (new Point(290, 64), Color.FromArgb(22, 73, 159));
            UrshiCheckPoints[1] = (new Point(269, 327), Color.FromArgb(0, 0, 0));
        }

        #region Kadala
        private static bool KadalaOpenStatus = false;
        public static bool IsKadalaNewOpened()
        {
            bool prevOpen = KadalaOpenStatus;
            return IsKadalaOpen() && !prevOpen;
        }

        private static bool IsKadalaOpen()
        {
            foreach ((Point, Color) cp in KadalaCheckPoints)
            {
                if (!ScreenTools.GetPixelColor(cp.Item1.X, cp.Item1.Y).Item1.Equals(cp.Item2))
                {
                    KadalaOpenStatus = false;
                    return false;
                }
            }
            KadalaOpenStatus = true;
            return true;
        }
        #endregion Kadala

        #region Smith
        private static bool SmithOpenStatus = false;
        public static bool IsSmithNewOpened()
        {
            bool prevOpen = SmithOpenStatus;
            return IsSmithOpen() && !prevOpen;
        }

        public static bool IsSmithOpen()
        {
            foreach ((Point, Color) cp in SmithCheckPoints)
            {
                if (!ScreenTools.GetPixelColor(cp.Item1.X, cp.Item1.Y).Item1.Equals(cp.Item2))
                {
                    SmithOpenStatus = false;
                    return false;
                }
            }
            SmithOpenStatus = true;
            return true;
        }
        #endregion Smith

        #region Urshi
        private static bool UrshiOpenStatus = false;
        public static bool IsUrshiNewOpened()
        {
            bool prevOpen = UrshiOpenStatus;
            return IsUrshiOpen() && !prevOpen;
        }

        public static bool IsUrshiOpen()
        {
            foreach ((Point, Color) cp in UrshiCheckPoints)
            {
                if (!ScreenTools.GetPixelColor(cp.Item1.X, cp.Item1.Y).Item1.Equals(cp.Item2))
                {
                    UrshiOpenStatus = false;
                    return false;
                }
            }
            UrshiOpenStatus = true;
            return true;
        }
        #endregion Urshi

    }
}
