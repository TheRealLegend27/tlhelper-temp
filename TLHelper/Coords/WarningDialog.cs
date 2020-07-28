using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TLHelper.Settings;
using TLHelper.SysCom;

namespace TLHelper.Coords
{
    public static class WarningDialog
    {
        private static Point AcceptLoc;

        static WarningDialog()
        {
            AcceptLoc = new Point(850, 375);
        }

        public static void Accept() => HardwareRobot.DoLeftClick(AcceptLoc.X, AcceptLoc.Y);
    }
}
