using System;
using System.Drawing;
using System.Windows.Forms;
using TLHelper.HotKeys;
using TLHelper.Resources;
using TLHelper.Skills;
using TLHelper.UI.Controls;

namespace TLHelper.UI.Containers
{
    public class SkillBar : FlowLayoutPanel
    {
        private readonly Label NameLabel;
        private readonly PictureBox IconBox;
        private readonly KeySelectionButton KeySelection;
        private readonly ComboBox SlotSelection;
        private readonly ComboBox ActiveBox;
        private readonly Skill skill;

        public SkillBar(string name, Image icon, Key key, int slot, bool active, Skill skill, bool even = false)
        {
            FlowDirection = FlowDirection.LeftToRight;
            Size = UI.Layout.MainControl.SkillContainer.SkillBar.Rect.Size;
            BackColor = even ? Theme.AccentLighter : Theme.Background;

            var comboMargin = (Size.Height - UI.Layout.MainControl.SkillContainer.SkillBar.SlotSelect.Rect.Height) / 2;

            this.skill = skill;

            NameLabel = new Label()
            {
                Text = name,
                Size = UI.Layout.MainControl.SkillContainer.SkillBar.Name.Rect.Size,
                Font = Theme.Fonts.H4,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.SkillContainer.SkillBar.Name.Top,
                ForeColor = Theme.Foreground
            };

            IconBox = new PictureBox()
            {
                Image = icon,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = UI.Layout.MainControl.SkillContainer.SkillBar.Icon.Rect.Size
            };

            KeySelection = new KeySelectionButton(key, ChangeKey)
            {
                Size = UI.Layout.MainControl.SkillContainer.SkillBar.KeySelect.Rect.Size,
                Margin = new Padding(3, comboMargin, 3, comboMargin),
            };


            SlotSelection = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = UI.Layout.MainControl.SkillContainer.SkillBar.SlotSelect.Rect.Size,
                Font = Theme.Fonts.H5,
                BackColor = Theme.Background,
                DrawMode = DrawMode.OwnerDrawFixed,
                Margin = new Padding(3, comboMargin, 3, comboMargin),
            };
            SlotSelection.DrawItem += (object sender, DrawItemEventArgs e) =>
            {
                int index = e.Index >= 0 ? e.Index : 0;
                var brush = Brushes.Black;
                e.DrawBackground();
                e.Graphics.DrawString(SlotSelection.Items[index].ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault);
            };
            SlotSelection.Items.AddRange(GlobalData.SkillSlots.GetValues());
            SlotSelection.SelectedIndex = slot;
            SlotSelection.SelectedIndexChanged += (object sender, EventArgs e) => ChangeSlot((sender as ComboBox).SelectedIndex);

            ActiveBox = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = UI.Layout.MainControl.SkillContainer.SkillBar.ActiveSelect.Rect.Size,
                Font = Theme.Fonts.H5,
                BackColor = Theme.Background,
                DrawMode = DrawMode.OwnerDrawFixed,
                Margin = new Padding(3, comboMargin, 3, comboMargin),
            };
            ActiveBox.DrawItem += (object sender, DrawItemEventArgs e) =>
            {
                int index = e.Index >= 0 ? e.Index : 0;
                var brush = Brushes.Black;
                e.DrawBackground();
                e.Graphics.DrawString(ActiveBox.Items[index].ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault);
            };
            ActiveBox.Items.AddRange(GlobalData.SkillActiveModes.GetValues());
            ActiveBox.SelectedIndex = active ? 1 : 0;
            ActiveBox.SelectedIndexChanged += (object sender, EventArgs e) => ChangeActive((sender as ComboBox).SelectedIndex == 1);

            Controls.AddRange(new Control[] { IconBox, NameLabel, KeySelection, SlotSelection, ActiveBox });
        }

        public void ChangeKey(Key key) => skill.SetKey(key);
        public void ChangeActive(bool active) => skill.SetActive(active);
        public void ChangeSlot(int slot) => skill.SetSlot(slot);

    }
}
