using System;
using System.Windows.Forms;

namespace TLHelper.UI.Containers.Actions
{
    public class ActionsContainer : FlowLayoutPanel
    {
        private readonly Label HeadLine;

        public ActionsContainer()
        {
            BackColor = Theme.Background;
            Size = UI.Layout.MainControl.Rect.Size;
            Location = UI.Layout.MainControl.Rect.Location;

            HeadLine = new Label
            {
                Text = "Actions",
                Font = Theme.Fonts.H2,
                Size = UI.Layout.MainControl.Headline.Rect.Size,
                Location = UI.Layout.MainControl.Headline.Rect.Location
            };
            Controls.Add(HeadLine);
        }

        public void AddActionKeyBar(ChangeModeRow r)
        {
            Controls.Add(r);
        }

    }
}
