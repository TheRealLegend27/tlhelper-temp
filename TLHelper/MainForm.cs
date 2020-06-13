using System;
using System.Windows.Forms;
using TLHelper.Settings;
using TLHelper.SysCom;
using TLHelper.UI;
using TLHelper.UI.Containers;
using TLHelper.UI.Containers.Actions;
using TLHelper.UI.Containers.Efficiency;
using TLHelper.UI.Containers.Scripts;
using TLHelper.UI.Containers.Settings;
using TLHelper.UI.Controls;

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

            // CREATE UI
            CreateUI();
        }

        // CREATE UI
        private void CreateUI()
        {
            sidebarContainer.BackColor = Theme.Accent;
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
        private void DisplayContainer(Control container, SidebarButton button)
        {
            ScriptContainer.Visible = false;
            OverviewContainer.Visible = false;
            SettingsContainer.Visible = false;
            ActionsContainer.Visible = false;
            EfficiencyContainer.Visible = false;
            container.Visible = true;

            sbbOverview.Active = false;
            sbbScripts.Active = false;
            sbbSettings.Active = false;
            sbbActions.Active = false;
            sbbEfficiency.Active = false;
            button.Active = true;
        }
        public void OverviewClicked(object sender, EventArgs e) => DisplayContainer(OverviewContainer, sbbOverview);
        public void ScriptsClicked(object sender, EventArgs e) => DisplayContainer(ScriptContainer, sbbScripts);
        public void SettingsClicked(object sender, EventArgs e) => DisplayContainer(SettingsContainer, sbbSettings);
        public void ActionsClicked(object sender, EventArgs e) => DisplayContainer(ActionsContainer, sbbActions);
        public void EfficiencyClicked(object sender, EventArgs e) => DisplayContainer(EfficiencyContainer, sbbEfficiency);

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
                SettingsManager.SetSetting("license", "");
                Close();
            }
        }

    }
}
