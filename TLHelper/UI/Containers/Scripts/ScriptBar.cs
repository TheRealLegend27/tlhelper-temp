using System.Drawing;
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

        public ScriptBar(Script s, bool even = false)
        {
            BackColor = even ? Theme.AccentLighter : Theme.Background;
            FlowDirection = FlowDirection.LeftToRight;
            Size = UI.Layout.MainControl.ScriptList.ScriptBar.Rect.Size;
            Margin = new Padding(3, 0, 3, 0);

            var labelMargin = (Size.Height - UI.Layout.MainControl.ScriptList.ScriptBar.Name.Rect.Size.Height) / 2;
            var buttonMargin = (Size.Height - UI.Layout.MainControl.ScriptList.ScriptBar.Key.Rect.Size.Height) / 2;
            var comboMargin = (Size.Height - UI.Layout.MainControl.ScriptList.ScriptBar.Active.Rect.Size.Height) / 2;

            NameLabel = new Label()
            {
                Text = s.Name,
                Size = UI.Layout.MainControl.ScriptList.ScriptBar.Name.Rect.Size,
                Font = Theme.Fonts.H3,
                ForeColor = Theme.Foreground,
                Margin = new Padding(0, labelMargin, 0, labelMargin)
            };
            Controls.Add(NameLabel);

            KeySelect = new HotkeySelectionButton(s.HotKey, s.ChangeHotKey)
            {
                Size = UI.Layout.MainControl.ScriptList.ScriptBar.Key.Rect.Size,
                Margin = new Padding(3, buttonMargin, 3, buttonMargin)
            };
            Controls.Add(KeySelect);

            ActiveSelect = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = UI.Layout.MainControl.ScriptList.ScriptBar.Active.Rect.Size,
                Font = Theme.Fonts.H6,
                BackColor = Theme.Background,
                DrawMode = DrawMode.OwnerDrawFixed,
                Margin = new Padding(3, comboMargin, 3, comboMargin),
            };
            ActiveSelect.DrawItem += (object sender, DrawItemEventArgs e) =>
            {
                int index = e.Index >= 0 ? e.Index : 0;
                var brush = Brushes.Black;
                e.DrawBackground();
                e.Graphics.DrawString(ActiveSelect.Items[index].ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault);
            };
            ActiveSelect.Items.AddRange(GlobalData.SkillActiveModes.GetValues());
            ActiveSelect.SelectedIndex = s.Enabled ? 1 : 0;
            Controls.Add(ActiveSelect);

            Src = new Label()
            {
                Size = UI.Layout.MainControl.ScriptList.ScriptBar.Src.Rect.Size,
                Text = s.ScriptOrigin == ScriptOrigins.EXT ? "EXT" : "INT",
                Font = Theme.Fonts.H5,
                Anchor = AnchorStyles.None,
            };
            Controls.Add(Src);

        }

    }
}
