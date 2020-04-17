using System.Windows.Forms;
using TLHelper.Resources;
using TLHelper.Scripts;
using TLHelper.UI.Controls;

namespace TLHelper.UI.Containers.Scripts
{
    class ScriptBar : FlowLayoutPanel
    {

        private Label NameLabel;
        private HotkeySelectionButton KeySelect;
        private ComboBox ActiveSelect;
        private Label Src;

        public ScriptBar(Script s)
        {
            BackColor = Theme.Background;
            FlowDirection = FlowDirection.LeftToRight;
            Size = UI.Layout.MainControl.ScriptList.ScriptBar.Rect.Size;

            NameLabel = new Label()
            {
                Text = s.Name,
                Size = UI.Layout.MainControl.ScriptList.ScriptBar.Name.Rect.Size,
                Location = UI.Layout.MainControl.ScriptList.ScriptBar.Name.Rect.Location,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.ScriptList.ScriptBar.Name.Top,
                Font = Theme.Fonts.H6
            };
            Controls.Add(NameLabel);

            KeySelect = new HotkeySelectionButton(s.HotKey, s.ChangeHotKey)
            {
                Size = UI.Layout.MainControl.ScriptList.ScriptBar.Key.Rect.Size,
                Location = UI.Layout.MainControl.ScriptList.ScriptBar.Key.Rect.Location,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.ScriptList.ScriptBar.Key.Top
            };
            Controls.Add(KeySelect);

            ActiveSelect = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = UI.Layout.MainControl.ScriptList.ScriptBar.Active.Rect.Size,
                Location = UI.Layout.MainControl.ScriptList.ScriptBar.Active.Rect.Location,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.ScriptList.ScriptBar.Active.Top,
            };
            ActiveSelect.Items.AddRange(GlobalData.SkillActiveModes.GetValues());
            ActiveSelect.SelectedIndex = s.Enabled ? 1 : 0;
            Controls.Add(ActiveSelect);

            Src = new Label()
            {
                Size = UI.Layout.MainControl.ScriptList.ScriptBar.Src.Rect.Size,
                Location = UI.Layout.MainControl.ScriptList.ScriptBar.Src.Rect.Location,
                Text = s.ScriptOrigin == ScriptOrigins.EXT ? "EXT" : "INT",
                Font = Theme.Fonts.H5,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.ScriptList.ScriptBar.Src.Top,
            };
            Controls.Add(Src);

        }

    }
}
