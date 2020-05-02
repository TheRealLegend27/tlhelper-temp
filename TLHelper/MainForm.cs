using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TLHelper.Settings;
using TLHelper.SysCom;
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
        }

        public void Init(string username)
        {
            // CREATE CONTAINERS
            CreateContainers();

            // INITIALIZE COMPONENTS
            InitializeComponent();
            lUserName.Text = username;

            // SET ICONS
            PowerOff.Image = Resources.UIIcons.power_off;
            Minimize.Image = Resources.UIIcons.minimize;
        }


        // CREATE CONTAINERS
        private void CreateContainers()
        {
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
        }

        // MENU BUTTONS
        private void DisplayContainer(Control container)
        {
            ScriptContainer.Visible = false;
            OverviewContainer.Visible = false;
            SettingsContainer.Visible = false;
            ActionsContainer.Visible = false;
            EfficiencyContainer.Visible = false;
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
        private void PowerOff_Click(object sender, EventArgs e) => Close();
        private void Minimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            HardwareListener.Action(m);
            base.WndProc(ref m);
        }

        private void Logout(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to logout of the current account?", "Logout?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SettingsManager.SetSetting("ath", "");
                Close();
            }
        }

    }
}
