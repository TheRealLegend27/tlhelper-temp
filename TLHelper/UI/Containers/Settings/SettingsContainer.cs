using System;
using System.Windows.Forms;
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
                Font = Theme.Fonts.H6,
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
                Font = Theme.Fonts.H6,
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

        }
    }
}
