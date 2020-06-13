using System.Windows.Forms;

namespace TLHelper.UI.Containers
{
    public class OverviewContainer : Panel
    {
        private readonly Label HeadLine;
        public SkillContainer SkillContainer { get; private set; }
        public SideBarContainer SideBarContainer { get; private set; }

        public OverviewContainer()
        {
            BackColor = Theme.Background;
            Size = UI.Layout.MainControl.Rect.Size;
            Location = UI.Layout.MainControl.Rect.Location;

            HeadLine = new Label()
            {
                Text = "No Class selected",
                Size = UI.Layout.MainControl.Headline.Rect.Size,
                Location = UI.Layout.MainControl.Headline.Rect.Location,
                Font = Theme.Fonts.H2
            };

            SkillContainer = new SkillContainer();

            SideBarContainer = new SideBarContainer();

            AddAll(HeadLine, SkillContainer, SideBarContainer);
        }

        private void AddAll(params Control[] controls) => Controls.AddRange(controls);

        public void ChangeTitle(string title) => HeadLine.Text = title;

    }
}
