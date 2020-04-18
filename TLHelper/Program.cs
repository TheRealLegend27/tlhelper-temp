using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using TLHelper.Scripts;
using TLHelper.Settings;
using TLHelper.Skills;
using TLHelper.SysCom;
using TLHelper.UI;

namespace TLHelper
{
    static class Program
    {
        private static bool Running = false;
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // SETUP APPLICATION STYLES
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // CREATE MAIN FORM
            MainForm mainForm = new MainForm();
            mainForm.FormClosing += (object s, FormClosingEventArgs e) => ShutDown();

            // CHECK IF SETTINGS DIR EXISTS
            XML.IOManager.SettingsBundle xml = new XML.IOManager.SettingsBundle((null, null), null, null);
            if (!XML.IOManager.CreateConfigDir())
            {
                // LOAD ALL SETTINGS
                xml = XML.IOManager.LoadAllSettings();
                Console.WriteLine("[Program]:: Settings Loaded");
            }

            // INITIALIZE
            SetFormRefs(mainForm);
            Init(xml, mainForm);

            // TRY START DIABLO / TURBOHUD
            if (!CheckForStart()) MessageBox.Show("You might have to restart the Helper, after you started Diablo", "Restart Helper");

            // INIT COORDINATES
            Coords.Coords.InitCoords();
            (Dim dim, bool success) = ScreenTools.GetWindowDimensions();
            if (success)
                Coords.Coords.ConvertCoords(dim.Width, dim.Height);

            // INIT KILL COORDS
            SkillCoords.Init();

            StartSkillLoop();

            // LAUNCH APPLICATION
            Application.Run(mainForm);
        }

        private static void SetFormRefs(MainForm Ref)
        {
            HardwareListener.Init(Ref.Handle);
            UIActions.SetFormRef(Ref);
            ScriptManager.SetFormRef(Ref);
            SkillManager.SetFormRef(Ref);
        }

        private static void Init(XML.IOManager.SettingsBundle xml, MainForm mainForm)
        {
            Console.WriteLine(xml.settings.ChildNodes.Count);
            // INIT SETTINGS MANAGER
            if (xml.settings != null)
                SettingsManager.LoadSettings(xml.settings);
            SettingsManager.CreateMissingSettings();

            // SETUP SKILLS
            if (xml.skills.def != null)
                SkillManager.InitSkills(xml.skills.def, xml.skills.special);
            SkillManager.CreateMissingSkills();


            // INIT MAINFORM
            mainForm.Init();

            // LOAD SCRIPTS
            ScriptManager.LoadScripts();
            // OVERRIDE SCRIPTS
            if (xml.scripts != null)
                ScriptManager.OverrideScripts(xml.scripts);

        }

        public static bool CheckForStart()
        {
            // TRY TO START DIABLO
            if (!ProcessManager.IsDiabloRunning)
            {
                // DIABLO IS NOT RUNNING
                if (StartDiablo())
                {
                    // DIABLO EXE WAS IN SETTINGS
                    MessageBox.Show("Klick 'OK', after Diablo has started", "Starting Diablo...", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                else
                {
                    // DIABLO EXE WAS NOT IN SETTINGS
                    MessageBox.Show("Please start Diablo first or set the Path to Diablo in the Settings, to automaticaly start it.", "Diablo is not running!");
                    return false;
                }
            }
            // TRY TO START TURBOHUD
            if (!ProcessManager.IsTurboHUDRunning)
            {
                // TURBOHUD IS NOT RUNNING
                if (!StartTurboHUD())
                {
                    // TURBOHUD EXE WAS NOT IN SETTINGS
                    MessageBox.Show("You can set the Path to your TurbHUD.exe in the Settings, to start it with TLHelper.", "TurboHUD is not running");
                }
            }
            return true;
        }

        public static bool StartDiablo()
        {
            // CHECK IF DIABLO EXECUTEABLE IS SET IN SETTINGS
            if (SettingsManager.Contains("d3-exe"))
            {
                // START DIABLO
                ProcessStartInfo d3Start = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    UseShellExecute = false,
                    FileName = SettingsManager.GetSetting("d3-exe"),
                    WindowStyle = ProcessWindowStyle.Normal,
                    Arguments = "-launch"
                };
                Process.Start(d3Start);
                return true;
            }
            return false;
        }

        public static bool StartTurboHUD()
        {
            if (SettingsManager.Contains("thud-exe"))
            {
                Process.Start(SettingsManager.GetSetting("thud-exe"));
                return true;
            }
            return false;
        }

        private static void ShutDown()
        {
            XML.IOManager.SaveAllSettings();
            Running = false;

            Environment.Exit(0);
        }

        private static void StartSkillLoop()
        {
            Thread mainThread = new Thread(new ThreadStart(RunSkillLoop));
            Running = true;
            mainThread.Start();
        }

        private static void RunSkillLoop()
        {
            while (Running)
            {
                SkillManager.ProcessSkills();
                Thread.Sleep(5);
            }
            Thread.CurrentThread.Join();
        }

    }
}
