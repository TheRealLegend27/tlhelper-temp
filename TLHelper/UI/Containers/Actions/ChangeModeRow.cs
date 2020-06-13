using System.Windows.Forms;
using TLHelper.UI.Controls;

namespace TLHelper.UI.Containers.Actions
{

    public class ChangeModeRow : FlowLayoutPanel
    {

        private readonly Label nameLabel;
        private readonly KeySelectionButton ksb;

        public ChangeModeRow(string name, HotKeys.Key key, KeySelectionButton.SelectedKeyChange onChange)
        {
            FlowDirection = FlowDirection.LeftToRight;
            Size = UI.Layout.MainControl.SkillContainer.SkillBar.Rect.Size;

            nameLabel = new Label()
            {
                Text = name,
                Font = Theme.Fonts.H4
            };
            Controls.Add(nameLabel);

            ksb = new KeySelectionButton(key, onChange)
            {
                Size = UI.Layout.MainControl.SkillContainer.SkillBar.KeySelect.Rect.Size
            };
            Controls.Add(ksb);
        }

    }
}
