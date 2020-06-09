using System.Drawing;
using System.Windows.Forms;

namespace TLHelper.UI
{
    public static class Layout
    {
        private const int defaultMargin = 10;

        public static class MainControl
        {
            public static readonly Rectangle Rect = new Rectangle(250, 0, 1150, 700);

            public static class Headline
            {
                public static readonly Rectangle Rect = new Rectangle(0, 0, 500, 30);
            }

            public static class SideBar
            {
                private const int width = 350;
                public static readonly Rectangle Rect = new Rectangle(
                    x: MainControl.Rect.Width - width,
                    y: 0,
                    width: width,
                    height: MainControl.Rect.Height
                );
                public static readonly Padding Padding = new Padding(defaultMargin);

                public static class CurrentClassSelection
                {
                    public static readonly Rectangle Rect = new Rectangle(
                        x: 0,
                        y: 0,
                        width: SideBar.Rect.Width - (defaultMargin * 2),
                        height: 60
                    );
                }

                public static class CurrentModeLabel
                {
                    public static readonly Rectangle Rect = new Rectangle(
                        x: 0,
                        y: 0,
                        width: SideBar.Rect.Width - (defaultMargin * 2),
                        height: 30
                    );
                }

                public static class AutoPotionBox
                {
                    public static readonly Rectangle Rect = new Rectangle(
                        x: 0,
                        y: 0,
                        width: SideBar.Rect.Width - (defaultMargin * 2),
                        height: 50
                    );

                    public static class Icon
                    {
                        public static readonly Rectangle Rect = new Rectangle(
                            x: 0,
                            y: 0,
                            width: 28,
                            height: 28
                        );
                    }

                    public static class KeySelection
                    {
                        public static readonly Rectangle Rect = new Rectangle(
                            x: 0,
                            y: 0,
                            width: ((AutoPotionBox.Rect.Width - Icon.Rect.Height) / 2) - 10,
                            height: 28
                        );
                    }
                    public static class ActiveSelection
                    {
                        public static readonly Rectangle Rect = new Rectangle(
                            x: 0,
                            y: 0,
                            width: ((AutoPotionBox.Rect.Width - Icon.Rect.Height) / 2) - 10,
                            height: 28
                        );
                    }

                }

                public static class ScriptDescriptionBox
                {
                    public static readonly Rectangle Rect = new Rectangle(
                        x: 0,
                        y: 0,
                        width: SideBar.Rect.Width - (defaultMargin * 2),
                        height: SideBar.Rect.Height - (CurrentClassSelection.Rect.Height - AutoPotionBox.Rect.Height - CurrentModeLabel.Rect.Height) - (Padding.All * 16)
                    );
                }

            }

            public static class SkillContainer
            {
                public static readonly Rectangle Rect = new Rectangle(
                    x: 0,
                    y: Headline.Rect.Height + 10,
                    width: MainControl.Rect.Width - SideBar.Rect.Width,
                    height: MainControl.Rect.Height - Headline.Rect.Height - 10
                );

                public static class SkillBar
                {
                    public static readonly Rectangle Rect = new Rectangle(
                        x: 0,
                        y: 0,
                        width: SkillContainer.Rect.Width,
                        height: 60
                    );

                    public static class Name
                    {
                        public static readonly Rectangle Rect = new Rectangle(
                            x: 0,
                            y: 0,
                            width: 200,
                            height: 25
                        );
                        public static readonly int Top = (SkillBar.Rect.Height - Rect.Height) / 2;
                    }

                    public static class Icon
                    {
                        public static readonly Rectangle Rect = new Rectangle(
                            x: 0,
                            y: 0,
                            width: 50,
                            height: 50
                        );
                        public static readonly int Top = (SkillBar.Rect.Height - Rect.Height) / 2;
                    }

                    public static class KeySelect
                    {
                        public static readonly Rectangle Rect = new Rectangle(
                            x: 0,
                            y: 0,
                            width: 200,
                            height: 28
                        );
                        public static readonly int Top = (SkillBar.Rect.Height - Rect.Height) / 2;
                    }

                    public static class ActiveSelect
                    {
                        public static readonly Rectangle Rect = new Rectangle(
                            x: 0,
                            y: 0,
                            width: 100,
                            height: 28
                        );
                        public static readonly int Top = (SkillBar.Rect.Height - Rect.Height) / 2;
                    }
                    public static class SlotSelect
                    {
                        public static readonly Rectangle Rect = new Rectangle(
                            x: 0,
                            y: 0,
                            width: 200,
                            height: 28
                        );
                        public static readonly int Top = (SkillBar.Rect.Height - Rect.Height) / 2;
                    }

                }

            }

            public static class ScriptList
            {

                public static readonly Rectangle Rect = new Rectangle(0, Headline.Rect.Height, MainControl.Rect.Width - 6, MainControl.Rect.Height - Headline.Rect.Height);

                public static class ScriptBar
                {
                    public static readonly Rectangle Rect = new Rectangle(
                        x: 0,
                        y: 0,
                        width: ScriptList.Rect.Width - (defaultMargin * 2),
                        height: 60
                    );

                    public static class Name
                    {
                        public static readonly Rectangle Rect = new Rectangle(0, 0, ScriptBar.Rect.Width / 3, 28);
                        public static readonly int Top = (ScriptBar.Rect.Height - Rect.Height) / 2;
                    }

                    public static class Key
                    {
                        public static readonly Rectangle Rect = new Rectangle(0, 0, (ScriptBar.Rect.Width - Name.Rect.Width) / 4, 28);
                        public static readonly int Top = (ScriptBar.Rect.Height - Rect.Height) / 2;
                    }

                    public static class Active
                    {
                        public static readonly Rectangle Rect = new Rectangle(0, 0, (ScriptBar.Rect.Width - Name.Rect.Width) / 4, 28);
                        public static readonly int Top = (ScriptBar.Rect.Height - Rect.Height) / 2;
                    }

                    public static class Src
                    {
                        public static readonly Rectangle Rect = new Rectangle(0, 0, (ScriptBar.Rect.Width - Name.Rect.Width) / 4, 28);
                        public static readonly int Top = (ScriptBar.Rect.Height - Rect.Height) / 2;
                    }

                }
            }

            public static class Settings
            {
                public static readonly Rectangle Rect = new Rectangle(
                    x: 0,
                    y: 0,
                    width: MainControl.Rect.Width - (2 * defaultMargin),
                    height: 30
                );
                public static class Name
                {
                    public static readonly Rectangle Rect = new Rectangle(
                        x: 0,
                        y: 0,
                        width: (MainControl.Rect.Width - (6 * defaultMargin)) / 3,
                        height: 30
                    );
                    public static readonly int Top = (Settings.Rect.Height - Rect.Height) / 2;
                }
                public static class Textbox
                {
                    public static readonly Rectangle Rect = new Rectangle(
                        x: 0,
                        y: 0,
                        width: (MainControl.Rect.Width - (6 * defaultMargin)) / 3,
                        height: 25
                    );
                    public static readonly int Top = (Settings.Rect.Height - Rect.Height) / 2;
                }
                public static class BrowserButton
                {
                    public static readonly Rectangle Rect = new Rectangle(
                        x: 0,
                        y: 0,
                        width: 30,
                        height: 25
                    );
                    public static readonly int Top = (Settings.Rect.Height - Rect.Height) / 2;
                }
            }

        }
    }
}
