using System;
using System.Windows.Forms;
using TLHelper.Coords;
using TLHelper.Settings;

namespace TLHelper.UI.Containers.Settings
{
    public class SettingsContainer : FlowLayoutPanel
    {
        private readonly Label HeadLine;

        // SETTINGS
        //  THUD
        private readonly FlowLayoutPanel PThud;
        private readonly Label SLThud;
        private readonly TextBox TBThud;
        private readonly Button BThud;

        //  D3
        private readonly FlowLayoutPanel PD3;
        private readonly Label SLD3;
        private readonly TextBox TBD3;
        private readonly Button BD3;

        // AHK
        private readonly FlowLayoutPanel PAHK;
        private readonly Label SLAHK;
        private readonly TextBox TBAHK;
        private readonly Button BAHK;

        // Hexing Pants
        private readonly FlowLayoutPanel PHexing;
        private readonly Label SLHexing;
        private readonly ComboBox CBHexing;

        // Salvage Normals
        private readonly FlowLayoutPanel PSalvageNormals;
        private readonly Label SLSalvageNormals;
        private readonly ComboBox CBSalvageNromals;

        // Kadala Auto Gamble
        private readonly FlowLayoutPanel PKadala;
        private readonly Label SLKadala;
        private readonly ComboBox CBKadala;

        // Auto Gemups
        private readonly FlowLayoutPanel PGemups;
        private readonly Label SLGemups;
        private readonly ComboBox CBGemups;

        public SettingsContainer()
        {
            BackColor = Theme.Background;
            FlowDirection = FlowDirection.TopDown;
            Size = UI.Layout.MainControl.Rect.Size;
            Location = UI.Layout.MainControl.Rect.Location;

            HeadLine = new Label
            {
                Text = "Settings",
                Font = Theme.Fonts.H2,
                Size = UI.Layout.MainControl.Headline.Rect.Size,
                Location = UI.Layout.MainControl.Headline.Rect.Location
            };
            Controls.Add(HeadLine);

            #region THUD EXE
            PThud = new FlowLayoutPanel
            {
                Size = UI.Layout.MainControl.Settings.Rect.Size,
                BackColor = Theme.Background,
                FlowDirection = FlowDirection.LeftToRight
            };
            Controls.Add(PThud);

            SLThud = new Label
            {
                Text = "TurboHUD-Directory:",
                Font = Theme.Fonts.H4,
                Size = UI.Layout.MainControl.Settings.Name.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.Name.Top
            };
            PThud.Controls.Add(SLThud);

            TBThud = new TextBox
            {
                ReadOnly = true,
                Font = Theme.Fonts.P,
                Size = UI.Layout.MainControl.Settings.Textbox.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.Textbox.Top,
                Text = SettingsManager.GetSetting("thud-exe")
            };
            PThud.Controls.Add(TBThud);

            BThud = new Button
            {
                Text = "...",
                Font = Theme.Fonts.H5,
                Size = UI.Layout.MainControl.Settings.BrowserButton.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.BrowserButton.Top,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            BThud.Click += (object sender, EventArgs e) =>
            {
                OpenFileDialog fileDialog = new OpenFileDialog
                {
                    Filter = "TurboHUD|TurboHUD.exe"
                };
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var thud = fileDialog.FileName;
                    TBThud.Text = thud;
                    SettingsManager.SetSetting("thud-exe", thud);
                }
            };
            PThud.Controls.Add(BThud);
            #endregion THUD EXE

            #region D3 EXE

            PD3 = new FlowLayoutPanel
            {
                Size = UI.Layout.MainControl.Settings.Rect.Size,
                BackColor = Theme.Background,
                FlowDirection = FlowDirection.LeftToRight
            };
            Controls.Add(PD3);

            SLD3 = new Label
            {
                Text = "Diablo III EXE:",
                Font = Theme.Fonts.H4,
                Size = UI.Layout.MainControl.Settings.Name.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.Name.Top
            };
            PD3.Controls.Add(SLD3);

            TBD3 = new TextBox
            {
                ReadOnly = true,
                Font = Theme.Fonts.P,
                Size = UI.Layout.MainControl.Settings.Textbox.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.Textbox.Top,
                Text = SettingsManager.GetSetting("d3-exe")
            };
            PD3.Controls.Add(TBD3);

            BD3 = new Button
            {
                Text = "...",
                Font = Theme.Fonts.H5,
                Size = UI.Layout.MainControl.Settings.BrowserButton.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.BrowserButton.Top,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            BD3.Click += (object sender, EventArgs e) =>
            {
                OpenFileDialog fileDialog = new OpenFileDialog
                {
                    Filter = "Diablo III (x64)|Diablo III64.exe"
                };
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var thud = fileDialog.FileName;
                    TBD3.Text = thud;
                    SettingsManager.SetSetting("d3-exe", thud);
                }
            };
            PD3.Controls.Add(BD3);

            #endregion D3 EXE

            #region AHK EXE
            PAHK = new FlowLayoutPanel
            {
                Size = UI.Layout.MainControl.Settings.Rect.Size,
                BackColor = Theme.Background,
                FlowDirection = FlowDirection.LeftToRight
            };
            Controls.Add(PAHK);

            SLAHK = new Label
            {
                Text = "AutoHotkey.exe location",
                Font = Theme.Fonts.H4,
                Size = UI.Layout.MainControl.Settings.Name.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.Name.Top
            };
            PAHK.Controls.Add(SLAHK);

            TBAHK = new TextBox
            {
                ReadOnly = true,
                Font = Theme.Fonts.P,
                Size = UI.Layout.MainControl.Settings.Textbox.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.Textbox.Top,
                Text = SettingsManager.GetSetting("ahk-exe")
            };
            PAHK.Controls.Add(TBAHK);

            BAHK = new Button
            {
                Text = "...",
                Font = Theme.Fonts.H5,
                Size = UI.Layout.MainControl.Settings.BrowserButton.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.BrowserButton.Top,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            BAHK.Click += (object sender, EventArgs e) =>
            {
                OpenFileDialog fileDialog = new OpenFileDialog
                {
                    Filter = "AutoHotkey.exe|AutoHotkey.exe"
                };
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var ahk = fileDialog.FileName;
                    TBAHK.Text = ahk;
                    SettingsManager.SetSetting("ahk-exe", ahk);
                }
            };
            PAHK.Controls.Add(BAHK);
            #endregion AHK EXE

            #region Spacer #1
            Controls.Add(new Panel()
            {
                Size = UI.Layout.MainControl.Settings.Rect.Size,
                BackColor = Theme.Background,
            });
            #endregion Spacer #1

            #region Hexing Pants
            PHexing = new FlowLayoutPanel()
            {
                Size = UI.Layout.MainControl.Settings.Rect.Size,
                BackColor = Theme.Background,
                FlowDirection = FlowDirection.LeftToRight
            };
            Controls.Add(PHexing);

            SLHexing = new Label
            {
                Text = "Hexing Pants",
                Font = Theme.Fonts.H4,
                Size = UI.Layout.MainControl.Settings.Name.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.Name.Top
            };
            PHexing.Controls.Add(SLHexing);

            CBHexing = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = Theme.Fonts.P,
                Size = UI.Layout.MainControl.Settings.Textbox.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.Textbox.Top
            };
            CBHexing.Items.AddRange(new string[] { "Inactive", "Active" });
            CBHexing.SelectedIndex = int.Parse(SettingsManager.GetSetting("hexing-pants"));
            CBHexing.SelectedIndexChanged += CBHexing_SelectedIndexChanged; ;
            PHexing.Controls.Add(CBHexing);
            #endregion Hexing Pants

            #region Spacer #2
            Controls.Add(new Panel()
            {
                Size = UI.Layout.MainControl.Settings.Rect.Size,
                BackColor = Theme.Background,
            });
            #endregion Spacer #2

            #region Salvage Normals
            PSalvageNormals = new FlowLayoutPanel()
            {
                Size = UI.Layout.MainControl.Settings.Rect.Size,
                BackColor = Theme.Background,
                FlowDirection = FlowDirection.LeftToRight
            };
            Controls.Add(PSalvageNormals);

            SLSalvageNormals = new Label
            {
                Text = "Automatic salvage normal items",
                Font = Theme.Fonts.H4,
                Size = UI.Layout.MainControl.Settings.Name.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.Name.Top
            };
            PSalvageNormals.Controls.Add(SLSalvageNormals);

            CBSalvageNromals = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = Theme.Fonts.P,
                Size = UI.Layout.MainControl.Settings.Textbox.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.Textbox.Top
            };
            CBSalvageNromals.Items.AddRange(new string[] {"Inactive", "Active"});
            CBSalvageNromals.SelectedIndex = int.Parse(SettingsManager.GetSetting("salvage-normals"));
            CBSalvageNromals.SelectedIndexChanged += CBSalvageNromals_SelectedIndexChanged;
            PSalvageNormals.Controls.Add(CBSalvageNromals);
            #endregion Salvage Normals

            #region Kadala Auto Gamble
            PKadala = new FlowLayoutPanel()
            {
                Size = UI.Layout.MainControl.Settings.Rect.Size,
                BackColor = Theme.Background,
                FlowDirection = FlowDirection.LeftToRight
            };
            Controls.Add(PKadala);

            SLKadala = new Label
            {
                Text = "Automatic Kadala gamble",
                Font = Theme.Fonts.H4,
                Size = UI.Layout.MainControl.Settings.Name.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.Name.Top
            };
            PKadala.Controls.Add(SLKadala);

            CBKadala = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = Theme.Fonts.P,
                Size = UI.Layout.MainControl.Settings.Textbox.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.Textbox.Top
            };
            CBKadala.Items.AddRange(Kadala.GetItemNames());
            CBKadala.SelectedItem = Kadala.GetItemNameByCode(SettingsManager.GetSetting("kadala-gamble"));
            CBKadala.SelectedIndexChanged += CBKadala_SelectedIndexChanged;
            PKadala.Controls.Add(CBKadala);
            #endregion Kadala Auto Gamble

            #region Auto Gemups
            PGemups = new FlowLayoutPanel()
            {
                Size = UI.Layout.MainControl.Settings.Rect.Size,
                BackColor = Theme.Background,
                FlowDirection = FlowDirection.LeftToRight
            };
            Controls.Add(PGemups);

            SLGemups = new Label
            {
                Text = "Automatic Gemups",
                Font = Theme.Fonts.H4,
                Size = UI.Layout.MainControl.Settings.Name.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.Name.Top
            };
            PGemups.Controls.Add(SLGemups);

            CBGemups = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = Theme.Fonts.P,
                Size = UI.Layout.MainControl.Settings.Textbox.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.Settings.Textbox.Top
            };
            CBGemups.Items.AddRange(Urshi.GetStatusNames());
            CBGemups.SelectedItem = Urshi.GetStatusNameByCode(SettingsManager.GetSetting("auto-gemups"));
            CBGemups.SelectedIndexChanged += CBGemups_SelectedIndexChanged;
            PGemups.Controls.Add(CBGemups);
            #endregion Auto Gemups

        }

        private void CBHexing_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbb = (ComboBox)sender;
            SettingsManager.SetSetting("hexing-pants", cbb.SelectedIndex.ToString());
        }

        private void CBGemups_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbb = (ComboBox)sender;
            var item = Urshi.GetStatusCodeByName(cbb.SelectedItem.ToString());
            SettingsManager.SetSetting("auto-gemups", item);
        }

        private void CBKadala_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbb = (ComboBox)sender;
            var item = Kadala.GetItemCodeByName(cbb.SelectedItem.ToString());
            SettingsManager.SetSetting("kadala-gamble", item);
        }

        private void CBSalvageNromals_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbb = (ComboBox)sender;
            SettingsManager.SetSetting("salvage-normals", cbb.SelectedIndex.ToString());
        }
    }
}
