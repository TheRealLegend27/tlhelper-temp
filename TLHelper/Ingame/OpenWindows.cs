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

        private static string[] KadalaColors = new string[]
        {
            "8D35C4", "7D3BA9", "7B3CA5", "542D6F", "3F2256", "140C1A", "503123",
            "7B4D29", "814A20", "84491B", "8C521E", "975D21", "2E1C09", "A37126",
            "996824", "4E3913", "C9BD41", "AF9C36", "532E0C", "422008", "765721",
            "7C5E24", "B8A53F", "E5D050", "E5D050", "C2AE42", "825A23", "CEAF44",
            "E9D050", "E9D050", "EBCB51", "E2C24D", "B59039", "9C6827", "9E6221",
            "9E6221", "955218", "A56220", "A76421", "A76421", "98561D"
        };

        private static string[] OrekColors = new string[]
        {
            "470F6E", "44155E", "2A0D3B", "300F42", "602483", "71289B", "8C30BD",
            "4A1A66", "1C0B29", "291039", "752BAE", "7F2EBE", "63248C", "6B2893",
            "983FBA", "A24BC0", "843EA2", "521178", "42046B", "4F0780", "5A0F8A",
            "621890", "681897", "BE55E1", "E889F4", "C963E6", "7A25A5", "5A1484",
            "5A1484", "5A1484", "521073", "5B177E", "893AB5", "8836BE", "7529AF",
            "591C80", "601F8B", "632091", "581C82", "210831", "250D35"
        };

        private static string[] ArtisanColors = new string[]
        {
            "12346E", "173B7F", "173B81", "17387B", "0D1326", "080408", "0A0A14",
            "24395A", "4974B6", "5282CE", "2E456C", "0B0C06", "080800", "080800",
            "160D03", "45240C", "7D4917", "BA7123", "C17223", "995419", "F3992E",
            "D3862A", "4A3B20", "100C00", "736137", "B49C6C", "423525", "211408",
            "211408", "101000", "FFF7CE", "DCD4AF", "403D30", "372510", "98571E",
            "864E1B", "372718", "0B0703", "080400", "3C3830", "2C3953"
        };

        private static string[] UrshiColors = new string[]
        {
            "CA7F02", "210D06", "200D07", "190907", "190807", "1C0A07", "180806",
            "261305", "FFA300", "653906", "522C06", "532D06", "542E06", "9C5F04",
            "230F06", "1C0A06", "1D0A07", "824E05", "5B3506", "4F2D06", "502D06",
            "532D06", "975B04", "E89301", "532C06", "532C06", "532C06", "552E06",
            "A06104", "240D07", "1F0B07", "1F0B08", "1E0B08", "200C08", "1F0B08",
            "210C08", "230D08", "220C08", "210D07", "240E07", "210B06", "1D0C06",
            "A26403", "FDA200", "B06D03", "7A4805", "5E3706", "814D05", "D88801",
            "BA7402", "1D0A07", "230C07", "290E07", "290D07", "260C06", "1E0C03",
            "A36402", "ED9700", "8C5601", "7C4C01", "E08E00", "673C04", "B37003",
            "EA9501", "5E3706", "130804", "160904", "180A04", "180A05", "1A0907",
            "1C0A07", "1B0A06", "D98901", "9E6004", "4E2C06", "4C2C06", "4B2D06",
            "CA8102", "AE6E02", "4A2D05", "4B2D06", "4B2D06", "B77203", "B77103",
            "1B0906", "1B0A07", "150906", "120704", "120704", "0F0502", "D98A01",
            "7D4D04", "1C0E04", "472906", "BA7602", "DC8C01", "190D06", "120906",
            "1D0B06", "200C06", "200C06"
        };

        private static string[] ConfirmDialogColors = new string[]
        {
            "290969", "290C63", "301068", "421F78", "693A94", "5F3688", "55307E",
            "4C2A75", "2B1750", "211147", "211147", "201044", "27154D", "312014",
            "230E12", "200815", "602E30", "64253A", "7C3048", "7D2A5B", "702858",
            "792A70", "832989", "331A44", "1D200E", "393021", "262014", "110D08",
            "100C08", "110D09", "1B1613", "100C08", "100C08", "8F6712", "986B13",
            "976713", "2F200B", "231E16", "0E0E07", "0C0C06", "110B07", "0D0806",
            "090704", "060603", "080805", "140C14", "230600", "53160B", "250B20",
            "250D1E", "2C1121", "2D131B", "391F18", "17090C", "0A0500", "160D07",
            "301A59", "1D0F46", "24124E", "371E60", "402369"
        };

        private static string[] WarningDialogColors = new string[]
        {
            "A56F01", "120606", "140606", "180907", "1E0A07", "1E0B07", "1D0A07",
            "1A0A06", "9E6902", "523205", "120605", "110505", "130705", "150705",
            "442506", "AE7501", "140706", "130606", "180806", "1D0906", "6C4005",
            "865503", "140705", "190C04", "A77001", "784E04", "160806", "170806",
            "140706", "130606", "130606", "5F3B05", "7F5303", "120606", "130605",
            "100504", "381F06", "B97D00", "C38500", "1E0E05", "140706", "120606",
            "100605", "0D0606", "322006", "B27901", "110706", "150906", "1A0A06",
            "1A0906", "180806"
        };

        private static (Point, Color)[] KadalaCheckPoints = new (Point, Color)[KadalaColors.Length];
        private static (Point, Color)[] OrekCheckPoints = new (Point, Color)[OrekColors.Length];
        private static (Point, Color)[] SmithCheckPoints = new (Point, Color)[ArtisanColors.Length];
        private static (Point, Color)[] UrshiCheckPoints = new (Point, Color)[UrshiColors.Length];
        private static (Point, Color)[] ConfirmDialogCheckPoints = new (Point, Color)[ConfirmDialogColors.Length];
        private static (Point, Color)[] WarningDialogCheckPoints = new (Point, Color)[WarningDialogColors.Length];

        private static readonly int CPXStart = 240;
        private static readonly int CPY = 70;

        private static readonly int UrshiCPXStart = 200;
        private static readonly int UrshiCPY = 130;

        private static readonly int ConfirmDialogCPXStart = 715;
        private static readonly int ConfirmDialogCPY = 790;

        private static readonly int WarningDialogCPXStart = 925;
        private static readonly int WarningDialogCPY = 190;

        static OpenWindows()
        {
            GenerateCheckPoints(KadalaCheckPoints, KadalaColors, CPXStart, CPY); // KADALA
            GenerateCheckPoints(OrekCheckPoints, OrekColors, CPXStart, CPY); // OREK
            GenerateCheckPoints(SmithCheckPoints, ArtisanColors, CPXStart, CPY); // SMITH

            GenerateCheckPoints(UrshiCheckPoints, UrshiColors, UrshiCPXStart, UrshiCPY); // URSHI
            GenerateCheckPoints(ConfirmDialogCheckPoints, ConfirmDialogColors, ConfirmDialogCPXStart, ConfirmDialogCPY); // CONFIRM DIALOG
            GenerateCheckPoints(WarningDialogCheckPoints, WarningDialogColors, WarningDialogCPXStart, WarningDialogCPY); // WARNING DIALOG
        }

        private static void GenerateCheckPoints((Point, Color)[] cps, string[] colors, int cpx, int cpy)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                Color col = ColorTranslator.FromHtml("#" + colors[i]);
                cps[i] = (new Point(cpx + i, cpy), col);
            }
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

        #region Orek
        private static bool OrekOpenStatus = false;
        public static bool IsOrekNewOpened()
        {
            bool prevOpen = OrekOpenStatus;
            return IsOrekOpen() && !prevOpen;
        }

        public static bool IsOrekOpen()
        {
            foreach ((Point, Color) cp in OrekCheckPoints)
            {
                if (!ScreenTools.GetPixelColor(cp.Item1.X, cp.Item1.Y).Item1.Equals(cp.Item2))
                {
                    OrekOpenStatus = false;
                    return false;
                }
            }
            OrekOpenStatus = true;
            return true;
        }
        #endregion Orek

        #region Confirm Dialog
        private static bool ConfirmDialogOpenStatus = false;
        public static bool IsConfirmDialogNewOpened()
        {
            bool prevOpen = ConfirmDialogOpenStatus;
            return IsConfirmDialogOpen() && !prevOpen;
        }

        public static bool IsConfirmDialogOpen()
        {
            foreach ((Point, Color) cp in ConfirmDialogCheckPoints)
            {
                if (!ScreenTools.GetPixelColor(cp.Item1.X, cp.Item1.Y).Item1.Equals(cp.Item2))
                {
                    ConfirmDialogOpenStatus = false;
                    return false;
                }
            }
            ConfirmDialogOpenStatus = true;
            return true;
        }
        #endregion Confirm Dialog

        #region Warning Dialog
        private static bool WarningDialogOpenStatus = false;
        public static bool IsWarningDialogNewOpened()
        {
            bool prevOpen = WarningDialogOpenStatus;
            return IsWarningDialogOpen() && !prevOpen;
        }

        public static bool IsWarningDialogOpen()
        {
            foreach ((Point, Color) cp in WarningDialogCheckPoints)
            {
                if (!ScreenTools.GetPixelColor(cp.Item1.X, cp.Item1.Y).Item1.Equals(cp.Item2))
                {
                    WarningDialogOpenStatus = false;
                    return false;
                }
            }
            WarningDialogOpenStatus = true;
            return true;
        }
        #endregion Warning Dialog
    }
}
