using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using TLHelper.Player;
using TLHelper.Scripts;
using TLHelper.Settings;
using TLHelper.Skills;
using TLHelper.SysCom;
using TLHelper.UI;
using TLHelper.UI.Containers;
using TLHelper.UI.Containers.Actions;
using TLHelper.UI.Containers.Efficiency;
using TLHelper.UI.Containers.Scripts;
using TLHelper.UI.Containers.Settings;

namespace TLHelper
{
    public partial class MainForm : Form
    {
        public static readonly string configDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public OverviewContainer OverviewContainer;
        public ScriptContainer ScriptContainer;
        public SettingsContainer SettingsContainer;
        public ActionsContainer ActionsContainer;
        public EfficiencyContainer EfficiencyContainer;

        public MainForm()
        {
            // INITIALIZE HARDWARE LISTENER
            HardwareListener.Init(Handle);

            if (!Directory.Exists(configDir))
                Directory.CreateDirectory(configDir);

            // GET SAVE FILES
            var XmlSkills = new XmlDocument();
            string skillsPath = configDir + @"\TLHelper\skills.xml";
            XmlNode SkillsRoot = null;
            if (File.Exists(skillsPath))
            {
                XmlSkills.Load(skillsPath);
                SkillsRoot = XmlSkills.DocumentElement;
            }

            var XmlScripts = new XmlDocument();
            string scriptsPath = configDir + @"\TLHelper\scripts.xml";
            XmlNode ScriptsRoot = null;
            if (File.Exists(scriptsPath))
            {
                XmlScripts.Load(scriptsPath);
                ScriptsRoot = XmlScripts.DocumentElement;
            }

            var XmlSettings = new XmlDocument();
            string settingsPath = configDir + @"\TLHelper\settings.xml";
            XmlNode SettingsRoot = null;
            if (File.Exists(settingsPath))
            {
                XmlSettings.Load(settingsPath);
                SettingsRoot = XmlSettings.DocumentElement;
            }

            // INIT SETTINGS MANAGER
            SettingsManager.SetFormRef(this);
            if (SettingsRoot != null)
                SettingsManager.LoadSettings(SettingsRoot.SelectSingleNode("descendant::Settings"));
            SettingsManager.CreateMissingSettings();

            // INITIALIZE COORDINATES
            Coords.Coords.InitCoords();
            (Dim dim, bool success) = ScreenTools.GetWindowDimensions();
            if (!success)
            {
                if (SettingsManager.GetSetting("d3-exe").Length > 3)
                {
                    ProcessStartInfo d3Start = new ProcessStartInfo
                    {
                        CreateNoWindow = false,
                        UseShellExecute = false,
                        FileName = SettingsManager.GetSetting("d3-exe"),
                        WindowStyle = ProcessWindowStyle.Normal,
                        Arguments = "-launch"
                    };
                    Process.Start(d3Start);
                    MessageBox.Show("Klicken Sie 'OK', sobald Diablo vollständig gestartet wurde", "Diablo III wird gestartet...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (SettingsManager.GetSetting("thud-exe").Length > 3)
                    {
                        Process.Start(SettingsManager.GetSetting("thud-exe"));
                    }
                }
                else
                {
                    MessageBox.Show("Diablo 3 konnte nicht gefunden werden!");
                    Environment.Exit(0);
                }
            }
            else
            {
                if (!ScreenTools.IsTHudRunning() && SettingsManager.GetSetting("thud-exe").Length > 3)
                {
                    Process.Start(SettingsManager.GetSetting("thud-exe"));
                }
                Coords.Coords.ConvertCoords(dim.Width, dim.Height);
            }

            // INIT UIACTIONS
            UIActions.SetFormRef(this);

            // INIT SCRIPT MANAGER
            ScriptManager.SetFormRef(this);

            // SETUP SKILLS
            SkillManager.SetFormRef(this);
            if (SkillsRoot != null)
                SkillManager.InitSkills(
                    SkillsRoot.SelectSingleNode("descendant::Skills"),
                    SkillsRoot.SelectSingleNode("descendant::SpecialSkills")
                );
            SkillManager.CreateMissingSkills();

            // CREATE OVERVIEW PANEL
            OverviewContainer = new OverviewContainer();
            Controls.Add(OverviewContainer);

            // CREATE SCRIPT PANEL
            ScriptContainer = new ScriptContainer
            {
                Visible = false
            };
            Controls.Add(ScriptContainer);

            // CREATE SETTINGS PANEL
            SettingsContainer = new SettingsContainer
            {
                Visible = false
            };
            Controls.Add(SettingsContainer);

            // CREATE ACTIONS PANEL
            ActionsContainer = new ActionsContainer
            {
                Visible = false
            };
            Controls.Add(ActionsContainer);

            // CREATE EFFICIENCY PANEL
            EfficiencyContainer = new EfficiencyContainer
            {
                Visible = false
            };
            Controls.Add(EfficiencyContainer);

            // LOAD SCRIPTS
            ScriptManager.LoadScripts();

            // OVERRIDE SCRIPTS
            if (ScriptsRoot != null)
                ScriptManager.OverrideScripts(ScriptsRoot.SelectSingleNode("descendant::Scripts"));

            // INITIALIZE SKILL COORDS
            SkillCoords.Init();

            // INITIALIZE COMPONENTS
            InitializeComponent();

            // SET ICONS
            PowerOff.Image = Resources.UIIcons.power_off;
            Minimize.Image = Resources.UIIcons.minimize;
        }

        // MENU BUTTONS
        private void HideAllContainers()
        {
            ScriptContainer.Visible = false;
            OverviewContainer.Visible = false;
            SettingsContainer.Visible = false;
            ActionsContainer.Visible = false;
            EfficiencyContainer.Visible = false;
        }
        private void DisplayContainer(Control container)
        {
            HideAllContainers();
            container.Visible = true;
        }
        public void OverviewClicked(object sender, EventArgs e) => DisplayContainer(OverviewContainer);
        public void ScriptsClicked(object sender, EventArgs e) => DisplayContainer(ScriptContainer);
        public void SettingsClicked(object sender, EventArgs e) => DisplayContainer(SettingsContainer);
        public void ActionsClicked(object sender, EventArgs e) => DisplayContainer(ActionsContainer);
        public void EfficiencyClicked(object sender, EventArgs e) => DisplayContainer(EfficiencyContainer);

        // CREATE WINDOW SHADOW
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        // FAKE WINDOW MOVEMENT
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void FakeWindowMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        // FORM CONTROL ACTIONS
        private void PowerOff_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // SAVE SETTINGS
            //  SKILLS
            XmlDocument skillDoc = SkillManager.GetXml();
            skillDoc.Save(configDir + @"\TLHelper\skills.xml");
            //  SCRIPTS
            XmlDocument scriptDoc = ScriptManager.GetXml();
            scriptDoc.Save(configDir + @"\TLHelper\scripts.xml");
            //  SETTINGS
            XmlDocument settingsDoc = SettingsManager.GetXml();
            settingsDoc.Save(configDir + @"\TLHelper\settings.xml");

            Environment.Exit(0);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Thread mainThread = new Thread(new ThreadStart(RunSkillLoop));
            mainThread.Start();
        }

        protected void RunSkillLoop()
        {
            while(true)
            {
                SkillManager.ProcessSkills();
                Thread.Sleep(5);
            }
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            HardwareListener.Action(m);
            base.WndProc(ref m);
        }

    }
}
