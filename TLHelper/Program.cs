using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using TLHelper.Scripts;
using TLHelper.Settings;
using TLHelper.Skills;
using TLHelper.SysCom;
using TLHelper.UI;
using TLHelper.UI.Popups;
using static TLHelper.XML.IOManager;

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

            // CHECK IF SETTINGS DIR EXISTS
            SettingsBundle xml = new SettingsBundle((null, null), null, null, null);
            if (!CreateConfigDir())
            {
                // LOAD ALL SETTINGS
                xml = LoadAllSettings();
                Console.WriteLine("[Program]:: Settings Loaded");
            }

            InitSettings(xml.settings);

            if (SettingsManager.Ath.Length == 0)
            {
                if (!TryLogin()) Environment.Exit(-1);
            }
            API.Users.Token = SettingsManager.Ath;

            bool authSuccess = API.Users.Authenticate().GetAwaiter().GetResult();
            if (authSuccess)
            {
                if (CheckServer()) Run(xml);
                else Environment.Exit(-1);
            }
            else
            {
                if (!TryLogin()) Environment.Exit(-1);
                else
                {
                    authSuccess = API.Users.Authenticate().GetAwaiter().GetResult();
                    if (!authSuccess) Environment.Exit(-1);
                    else Run(xml);
                }
            }
        }

        private static bool CheckServer() => API.Users.AuthServer().GetAwaiter().GetResult();

        private static bool TryLogin()
        {
            LoginPopup loginPopup = new LoginPopup();
            if (loginPopup.ShowDialog() == DialogResult.OK)
            {
                var username = loginPopup.username;
                var password = loginPopup.password;

                bool loginSuccess = API.Users.Login(username, password).GetAwaiter().GetResult();
                if (!loginSuccess)
                    return false;
            }
            else
            {
                return false;
            }
            return true;
        }

        private static void Run(SettingsBundle xml)
        {
            // CREATE MAIN FORM
            MainForm mainForm = new MainForm();
            mainForm.FormClosing += (object s, FormClosingEventArgs e) => ShutDown();

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
            ActiveMode.SetFormRef(Ref);
        }

        private static void InitSettings(XmlNode e)
        {
            // INIT SETTINGS MANAGER
            if (e != null)
                SettingsManager.LoadSettings(e);
            SettingsManager.CreateMissingSettings();
        }

        private static void Init(XML.IOManager.SettingsBundle xml, MainForm mainForm)
        {
            // SETUP SKILLS
            if (xml.skills.def != null)
                SkillManager.InitSkills(xml.skills.def, xml.skills.special);
            SkillManager.CreateMissingSkills();

            // SETUP ACTIONS
            if (xml.actions != null)
                ActiveMode.LoadSettings(xml.actions);
            ActiveMode.LoadDefaultSettings();


            // INIT MAINFORM
            mainForm.Init(username: API.Users.CurrentUser.username);

            // LOAD SCRIPTS
            ScriptManager.LoadScripts();
            // OVERRIDE SCRIPTS
            if (xml.scripts != null)
                ScriptManager.OverrideScripts(xml.scripts);

            // INIT ACTIVE MODE
            ActiveMode.Init();
        }

        public static bool CheckForStart()
        {
            // TRY TO START DIABLO
            if (!ProcessManager.IsDiabloRunning)
            {
                // DIABLO IS NOT RUNNING
                if (!StartDiablo())
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
