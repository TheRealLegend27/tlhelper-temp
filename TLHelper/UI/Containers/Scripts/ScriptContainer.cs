using System.Windows.Forms;
using TLHelper.Scripts;

namespace TLHelper.UI.Containers.Scripts
{
    public class ScriptContainer : Panel
    {
        private Label HeadLine;
        private FlowLayoutPanel ScriptList;

        public ScriptContainer()
        {
            BackColor = Theme.Background;
            Size = UI.Layout.MainControl.Rect.Size;
            Location = UI.Layout.MainControl.Rect.Location;
            AutoScroll = true;

            HeadLine = new Label()
            {
                Text = "Scripts",
                Font = Theme.Fonts.H2,
                Size = UI.Layout.MainControl.Headline.Rect.Size,
                Location = UI.Layout.MainControl.Headline.Rect.Location,
                Name = "Headline"
            };
            Controls.Add(HeadLine);

            ScriptList = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.TopDown,
                BackColor = Theme.Background,
                Size = UI.Layout.MainControl.ScriptList.Rect.Size,
                Location = UI.Layout.MainControl.ScriptList.Rect.Location,
                Dock = DockStyle.None,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            Controls.Add(ScriptList);

        }

        public void ClearScripts()
        {
            ScriptList.Controls.Clear();
        }

        public void AddScript(Script s)
        {
            ScriptList.Controls.Add(new ScriptBar(s, ScriptList.Controls.Count % 2 == 0));
        }

    }
}
