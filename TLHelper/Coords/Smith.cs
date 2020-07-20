using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TLHelper.Settings;
using TLHelper.SysCom;

namespace TLHelper.Coords
{
    static class Smith
    {

        private static Point SalvageTab;
        private static Point ConfirmButton;

        private static Point SalvageWhitePos;
        private static Color SalvageWhiteCol;

        private static Point SalvageBluePos;
        private static Color SalvageBlueCol;

        private static Point SalvageYellowPos;
        private static Color SalvageYellowCol;

        static Smith()
        {
            SalvageTab = new Point(530, 500);
            ConfirmButton = new Point(843, 373);

            SalvageWhitePos = new Point(261, 294); // 50 49 50
            SalvageWhiteCol = Color.FromArgb(50, 49, 50);

            SalvageBluePos = new Point(325, 294); // 12 25 43
            SalvageBlueCol = Color.FromArgb(12, 25, 43);

            SalvageYellowPos = new Point(395, 294); // 49 40 8
            SalvageYellowCol = Color.FromArgb(49, 40, 8);
        }

        public static bool ShouldSalvage() => SettingsManager.GetSetting("salvage-normals") == "1";

        public static void SalvageNormals()
        {
            SalvageWhites();
            SalvageBlues();
            SalvageYellows();
        }

        private static void SalvageWhites()
        {
            if (ScreenTools.GetPixelColor(SalvageWhitePos.X, SalvageWhitePos.Y).Item1.Equals(SalvageWhiteCol))
            {
                HardwareRobot.DoLeftClick(SalvageWhitePos.X, SalvageWhitePos.Y, HardwareRobot.ActionTypes.SIMULATE);
                Confirm();
            }
        }
        private static void SalvageBlues()
        {
            if (ScreenTools.GetPixelColor(SalvageBluePos.X, SalvageBluePos.Y).Item1.Equals(SalvageBlueCol))
            {
                HardwareRobot.DoLeftClick(SalvageBluePos.X, SalvageBluePos.Y, HardwareRobot.ActionTypes.SIMULATE);
                Confirm();
            }
        }
        private static void SalvageYellows()
        {
            if (ScreenTools.GetPixelColor(SalvageYellowPos.X, SalvageYellowPos.Y).Item1.Equals(SalvageYellowCol))
            {
                HardwareRobot.DoLeftClick(SalvageYellowPos.X, SalvageYellowPos.Y, HardwareRobot.ActionTypes.SIMULATE);
                Confirm();
            }
        }

        private static void Confirm()
        {
            HardwareRobot.DoLeftClick(ConfirmButton.X, ConfirmButton.Y, HardwareRobot.ActionTypes.SIMULATE);
        }


    }
}
