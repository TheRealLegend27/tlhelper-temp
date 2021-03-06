﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
                // CREATE SCRIPTS DIR IF NOT PRESENT
                CreateScriptsDir();
                CreateAHKScriptsDir();
                // LOAD ALL SETTINGS
                xml = LoadAllSettings();
            }

            InitSettings(xml.settings);

            if (SettingsManager.GetSetting("license").Length == 0)
            {
                if (!GetLicense()) Environment.Exit(-1);
            }
            API.Variables.License = SettingsManager.GetSetting("license");

            if (TryAuthenticate() && CheckServer())
            {
                if (!Directory.Exists(EnvironmentVariables.CONFIG_DIR))
                    Directory.CreateDirectory(EnvironmentVariables.CONFIG_DIR);
                if (!Directory.Exists(EnvironmentVariables.SCRIPTS_DIR))
                    Directory.CreateDirectory(EnvironmentVariables.SCRIPTS_DIR);

                UpdateScripts();
                DeleteScripts();

                Run(xml);
            }
            else
            {
                if (!GetLicense()) Environment.Exit(-1);
                API.Variables.License = SettingsManager.GetSetting("license");

                if (TryAuthenticate() && CheckServer())
                {
                    if (!Directory.Exists(EnvironmentVariables.CONFIG_DIR))
                        Directory.CreateDirectory(EnvironmentVariables.CONFIG_DIR);
                    if (!Directory.Exists(EnvironmentVariables.SCRIPTS_DIR))
                        Directory.CreateDirectory(EnvironmentVariables.SCRIPTS_DIR);

                    UpdateScripts();
                    DeleteScripts();

                    Run(xml);
                }
                else
                {
                    Environment.Exit(-1);
                }
            }
        }

        private static bool TryAuthenticate()
        {
            bool authSuccess = API.Users.AuthLicense().GetAwaiter().GetResult();
            if (authSuccess)
            {
                if (CheckServer())
                {
                    return true;
                }
                else return false;
            }
            return false;
        }

        private static void UpdateScripts()
        {
            List<API.Scripts.FullScript> outdated = API.Scripts.GetOutdatedScripts().GetAwaiter().GetResult();
            if (outdated.Count > 0)
            {
                string message = string.Format("You have {0} outdated scripts. Do you want to update them?", outdated.Count);
                if (MessageBox.Show(message, "Outdated scripts", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (var script in outdated)
                    {
                        var content = API.Scripts.DownloadScript(script.id).GetAwaiter().GetResult();
                        if (script.script_type == "tls")
                        {
                            File.WriteAllText(EnvironmentVariables.SCRIPTS_DIR + "\\" + script.id + ".tls", content);
                        }
                        else if (script.script_type == "ahk")
                        {
                            string tls = string.Format("name:{0}\ndescription:{1}\nctrl:{2}\nshift:{3}\nalt:{4}\nkey:{5}\nscript:{6}.ahk", script.name, script.description, "1", "1", "1", "S", script.id);
                            File.WriteAllText(EnvironmentVariables.SCRIPTS_DIR + "\\" + script.id + ".ahk-tl", tls);
                            File.WriteAllText(EnvironmentVariables.SCRIPTS_DIR + "\\ahk\\" + script.id + ".ahk", content);
                        }
                    }
                }
            }
        }

        public static void DeleteScripts()
        {
            List<string> toDelete = API.Scripts.GetScriptsToDelete().GetAwaiter().GetResult();
            if (toDelete.Count > 0)
            {
                string message = string.Format("You have {0} scripts that are not in your list. Do you want to delete them?", toDelete.Count);
                if (MessageBox.Show(message, "Unknown scripts", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (string file in toDelete)
                    {
                        if (Path.GetExtension(file) == ".ahk-tl")
                        {
                            string ahkfile = EnvironmentVariables.SCRIPTS_DIR + "\\ahk\\" + Path.GetFileNameWithoutExtension(file) + ".ahk";
                            if (File.Exists(ahkfile))
                                File.Delete(ahkfile);
                        }
                        File.Delete(file);
                    }
                }
            }
        }

        private static bool CheckServer() => API.Users.AuthServer().GetAwaiter().GetResult();

        private static bool GetLicense()
        {
            LoginPopup loginPopup = new LoginPopup();
            if (loginPopup.ShowDialog() == DialogResult.OK)
            {
                var license = loginPopup.license;
                SettingsManager.SetSetting("license", license);
                return true;
            }
            return false;
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

            // START SKILL LOOP
            StartSkillLoop();

            // START AUTH LOOP
            StartAuthLoop();

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

        private static void Init(SettingsBundle xml, MainForm mainForm)
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
            mainForm.Init(username: API.Users.AuthResult.user);

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
                Process.Start("CMD.exe", "/C start " + SettingsManager.GetSetting("thud-exe"));
                return true;
            }
            return false;
        }

        private static void ShutDown(int code = 0)
        {
            SaveAllSettings();
            Running = false;

            Environment.Exit(code);
        }

        private static void StartSkillLoop()
        {
            Running = true;
            Thread mainThread = new Thread(new ThreadStart(RunSkillLoop));
            mainThread.Start();
        }
        private static void RunSkillLoop()
        {
            while (Running)
            {
                SkillManager.ProcessLoop();
                Thread.Sleep(5);
            }
            Thread.CurrentThread.Join();
        }

        private static void StartAuthLoop()
        {
            Thread authThread = new Thread(new ThreadStart(RunAuthLoop));
            authThread.Start();
        }
        private static void RunAuthLoop()
        {
            int counter = 0;
            while (Running)
            {
                while (counter < 60)
                {
                    if (!Running)
                        Thread.CurrentThread.Join();
                    Thread.Sleep(1000);
                    counter++;
                }
                counter = 0;
                // AUTHENTICATE
                bool authed = API.Users.AuthLicense().GetAwaiter().GetResult();
                if (!authed)
                {
                    MessageBox.Show("Could not authenticate your account. Maybe its used by another person or expired.");
                    ShutDown(-1);
                }
            }
            Thread.CurrentThread.Join();
        }

    }
}
