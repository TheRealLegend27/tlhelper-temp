using System;
using System.Windows.Forms;
using TLHelper.Resources;
using TLHelper.Skills;
using TLHelper.UI.Controls;

namespace TLHelper.UI.Containers
{
    public class SideBarContainer : FlowLayoutPanel
    {
        // DEFAULT CONTROLS
        private readonly ComboBox CurrentClassSelection;
        private readonly TextBox ScriptDescriptions;
        //  AUTO POTION CONTROLS
        private readonly FlowLayoutPanel AutoPotionContainer;
        public PictureBox AutoPotionIcon;
        public KeySelectionButton AutoPotionKey;
        public ComboBox AutoPotionActive;
        // CURRENT MODE LABEL
        private readonly Label CurrentModeLabel;
        public SideBarContainer()
        {
            // SETUP DEFAULT ATTRIBUTES
            FlowDirection = FlowDirection.LeftToRight;
            BackColor = Theme.Background;
            Size = UI.Layout.MainControl.SideBar.Rect.Size;
            Location = UI.Layout.MainControl.SideBar.Rect.Location;
            Padding = UI.Layout.MainControl.SideBar.Padding;

            // SETUP DEFAULT CONTROLS
            CurrentClassSelection = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.System,
                Size = UI.Layout.MainControl.SideBar.CurrentClassSelection.Rect.Size
            };
            CurrentClassSelection.Items.Add("None");
            CurrentClassSelection.Items.AddRange(GlobalData.Classes.GetValues());
            CurrentClassSelection.SelectedIndex = 0;
            CurrentClassSelection.SelectedIndexChanged += (object sender, EventArgs e) =>
            {
                UIActions.CurrentClassChanged((sender as ComboBox).SelectedItem as string);
            };

            ScriptDescriptions = new TextBox()
            {
                Multiline = true,
                ReadOnly = true,
                Size = UI.Layout.MainControl.SideBar.ScriptDescriptionBox.Rect.Size,
                Location = UI.Layout.MainControl.SideBar.ScriptDescriptionBox.Rect.Location,
                Font = Theme.Fonts.H5
            };

            // CURRENT MODE
            CurrentModeLabel = new Label
            {
                Text = "Current Mode: Automatic",
                Size = UI.Layout.MainControl.SideBar.CurrentModeLabel.Rect.Size,
                Font = Theme.Fonts.H6
            };

            //  AUTO POTION
            AutoPotionContainer = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                Size = UI.Layout.MainControl.SideBar.AutoPotionBox.Rect.Size,
            };

            AutoPotionIcon = new PictureBox()
            {
                Image = SkillIcons.potion,
                Size = UI.Layout.MainControl.SideBar.AutoPotionBox.Icon.Rect.Size,
                Location = UI.Layout.MainControl.SideBar.AutoPotionBox.Icon.Rect.Location,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            AutoPotionContainer.Controls.Add(AutoPotionIcon);

            AutoPotionKey = new KeySelectionButton(key: new HotKeys.Key(Keys.None), SkillManager.PotionSkill.SetKey);
            AutoPotionContainer.Controls.Add(AutoPotionKey);

            AutoPotionActive = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = UI.Layout.MainControl.SideBar.AutoPotionBox.ActiveSelection.Rect.Size,
                Location = UI.Layout.MainControl.SideBar.AutoPotionBox.ActiveSelection.Rect.Location
            };
            AutoPotionActive.Items.AddRange(GlobalData.SkillActiveModes.GetValues());
            AutoPotionActive.SelectedIndex = 0;
            AutoPotionActive.SelectedIndexChanged += (object Sender, EventArgs e) =>
            {
                SkillManager.PotionSkill.SetActive((Sender as ComboBox).SelectedIndex == 1);
            };

            SkillManager.PotionSkill.SetControls(AutoPotionActive, AutoPotionKey);

            AutoPotionContainer.Controls.Add(AutoPotionActive);

            Controls.Add(CurrentClassSelection);
            Controls.Add(ScriptDescriptions);
            Controls.Add(CurrentModeLabel);
            Controls.Add(AutoPotionContainer);
        }

        public void ClearScriptDesctiption()
        {
            ScriptDescriptions.Text = "";
        }

        public void AddScriptDescription(string desc)
        {
            ScriptDescriptions.Text += desc + "\r\n";
        }

        public void ChangeCurrentMode(string mode)
        {
            CurrentModeLabel.Text = "Current Mode: " + mode;
        }

    }
}
