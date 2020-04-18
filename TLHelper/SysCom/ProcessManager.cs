using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLHelper.SysCom
{
    public static class ProcessManager
    {
        private static readonly string DiabloProcessTitle = "Diablo III";
        private static readonly string Diablo64ProcessTitle = "Diablo III64";
        private static readonly string TurboHUDProcessTitle = "TurboHUD";

        public static bool IsTurboHUDRunning => IsProcessRunning(TurboHUDProcessTitle);
        public static bool IsDiabloRunning => IsProcessRunning(DiabloProcessTitle) || IsProcessRunning(Diablo64ProcessTitle);

        public static bool IsProcessRunning(string procTitle)
        {
            Process[] pname = Process.GetProcessesByName(procTitle);
            return pname.Length > 0;
        }


    }
}
