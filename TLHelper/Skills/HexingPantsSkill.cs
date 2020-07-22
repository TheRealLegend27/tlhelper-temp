using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TLHelper.SysCom;

namespace TLHelper.Skills
{
    public static class HexingPantsSkill
    {
        private static Dim d3dim = ScreenTools.GetWindowDimensions().Item1;
        private static double sx, sy;
        private static bool currentState = false;
        private static int lastClick = 0;

        static HexingPantsSkill()
        {
            sx = Math.Abs(d3dim.Right - d3dim.Left) / 1920.0;
            sy = Math.Abs(d3dim.Top - d3dim.Bottom) / 1080.0;
        }

        public static void Move()
        {
            if (Environment.TickCount - lastClick <= 75) return;

            if (currentState)
                HardwareRobot.DoMButtonClick((int)sx * (961 + 50), (int)sy * 507);
            else
                HardwareRobot.DoMButtonClick((int)sx * (961 - 50), (int)sy * 507);

            lastClick = Environment.TickCount;
            currentState = !currentState;
        }


    }
}
