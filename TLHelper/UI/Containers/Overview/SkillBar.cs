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

        public SkillBar(string name, Image icon, Key key, int slot, bool active, Skill skill)
        {
            FlowDirection = FlowDirection.LeftToRight;
            Size = UI.Layout.MainControl.SkillContainer.SkillBar.Rect.Size;

            this.skill = skill;

            NameLabel = new Label()
            {
                Text = name,
                Size = UI.Layout.MainControl.SkillContainer.SkillBar.Name.Rect.Size,
                Font = Theme.Fonts.H6,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.SkillContainer.SkillBar.Name.Top
            };

            IconBox = new PictureBox()
            {
                Image = icon,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = UI.Layout.MainControl.SkillContainer.SkillBar.Icon.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.SkillContainer.SkillBar.Icon.Top
            };

            KeySelection = new KeySelectionButton(key, ChangeKey)
            {
                Size = UI.Layout.MainControl.SkillContainer.SkillBar.KeySelect.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.SkillContainer.SkillBar.KeySelect.Top
            };


            SlotSelection = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = UI.Layout.MainControl.SkillContainer.SkillBar.SlotSelect.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.SkillContainer.SkillBar.SlotSelect.Top
            };
            SlotSelection.Items.AddRange(GlobalData.SkillSlots.GetValues());
            SlotSelection.SelectedIndex = slot;
            SlotSelection.SelectedIndexChanged += (object sender, EventArgs e) => ChangeSlot((sender as ComboBox).SelectedIndex);

            ActiveBox = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = UI.Layout.MainControl.SkillContainer.SkillBar.ActiveSelect.Rect.Size,
                Anchor = AnchorStyles.None,
                Top = UI.Layout.MainControl.SkillContainer.SkillBar.ActiveSelect.Top
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
