using System.Windows.Forms;

namespace TLHelper.UI.Containers.Efficiency
{
    public class EfficiencyContainer : Panel
    {
        private readonly Label HeadLine;

        public EfficiencyContainer()
        {
            BackColor = Theme.Background;
            Size = UI.Layout.MainControl.Rect.Size;
            Location = UI.Layout.MainControl.Rect.Location;

            HeadLine = new Label
            {
                Text = "Efficiency (coming soon)",
                Font = Theme.Fonts.H2,
                Size = UI.Layout.MainControl.Headline.Rect.Size,
                Location = UI.Layout.MainControl.Headline.Rect.Location
            };
            Controls.Add(HeadLine);
        }
    }
}
