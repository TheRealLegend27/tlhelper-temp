using System.Windows.Forms;
using System.Xml;
using TLHelper.Scripts;
using TLHelper.UI.Containers.Actions;

namespace TLHelper.Skills
{
    class ActiveMode
    {
        private static Mode CurrentMode;
        private static MainForm MainFormRef;

        private static HotKeys.Key alwaysKey = new HotKeys.Key(Keys.None);
        private static HotKeys.Key autoKey = new HotKeys.Key(Keys.None);
        private static HotKeys.Key neverKey = new HotKeys.Key(Keys.None);

        static ActiveMode()
        {
            CurrentMode = Mode.Automatic;
        }

        public static void SetFormRef(MainForm Ref)
        {
            MainFormRef = Ref;
        }

        public static void LoadSettings(XmlNode n)
        {
            foreach (XmlElement e in n.ChildNodes)
            {
                var key = int.Parse(e.GetAttribute("key"));
                if (e.GetAttribute("id") == "active-mode-never") neverKey = new HotKeys.Key((Keys)key);
                if (e.GetAttribute("id") == "active-mode-auto") autoKey = new HotKeys.Key((Keys)key);
                if (e.GetAttribute("id") == "active-mode-always") alwaysKey = new HotKeys.Key((Keys)key);
            }
        }

        public static void LoadDefaultSettings()
        {
            if (!alwaysKey.IsSet) alwaysKey = new HotKeys.Key(Keys.F1);
            if (!autoKey.IsSet) autoKey = new HotKeys.Key(Keys.F2);
            if (!neverKey.IsSet) neverKey = new HotKeys.Key(Keys.F3);
        }

        public static void Init()
        {
            ChangeModeRow cmrAlways = new ChangeModeRow("Always active", alwaysKey, ChangeAlwaysKey);
            ChangeModeRow cmrAuto = new ChangeModeRow("Automatic", autoKey, ChangeAutoKey);
            ChangeModeRow cmrNever = new ChangeModeRow("Never active", neverKey, ChangeNeverKey);

            MainFormRef.ActionsContainer.AddActionKeyBar(cmrAlways);
            MainFormRef.ActionsContainer.AddActionKeyBar(cmrAuto);
            MainFormRef.ActionsContainer.AddActionKeyBar(cmrNever);

            HotkeyManager.RegisterKey(new HotKeys.HotKey(alwaysKey, false, false, false), "active-mode-always");
            HotkeyManager.RegisterKey(new HotKeys.HotKey(autoKey, false, false, false), "active-mode-auto");
            HotkeyManager.RegisterKey(new HotKeys.HotKey(neverKey, false, false, false), "active-mode-never");
        }

        public static Mode GetCurrentMode() => CurrentMode;

        public static void KeyPressed(string id)
        {
            switch (id)
            {
                case "active-mode-always":
                    CurrentMode = Mode.AlwaysActive;
                    MainFormRef.OverviewContainer.SideBarContainer.ChangeCurrentMode("Always active");
                    break;

                case "active-mode-never":
                    CurrentMode = Mode.NeverActive;
                    MainFormRef.OverviewContainer.SideBarContainer.ChangeCurrentMode("Never active");
                    break;

                case "active-mode-auto":
                    CurrentMode = Mode.Automatic;
                    MainFormRef.OverviewContainer.SideBarContainer.ChangeCurrentMode("Automatic");
                    break;
            }
        }

        public static void ChangeAlwaysKey(HotKeys.Key key)
        {
            alwaysKey = key;
            ShowRestartInfo();
        }
        public static void ChangeAutoKey(HotKeys.Key key)
        {
            autoKey = key;
            ShowRestartInfo();
        }
        public static void ChangeNeverKey(HotKeys.Key key)
        {
            neverKey = key;
            ShowRestartInfo();
        }

        private static void ShowRestartInfo() => MessageBox.Show("You have to restart the Helper to apply the changes.", "Restart Helper!");

        public static XmlDocument GetXml()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("TLHelper");
            root.SetAttribute("version", EnvironmentVariables.CURRENT_VERSION_INT);

            var actions = doc.CreateElement("Actions");

            var activeModeNever = doc.CreateElement("Action"); activeModeNever.SetAttribute("id", "active-mode-never");
            activeModeNever.SetAttribute("key", ((int)neverKey.CurrentKey).ToString());

            var activeModeAuto = doc.CreateElement("Action"); activeModeAuto.SetAttribute("id", "active-mode-auto");
            activeModeAuto.SetAttribute("key", ((int)autoKey.CurrentKey).ToString());

            var activeModeAlways = doc.CreateElement("Action"); activeModeAlways.SetAttribute("id", "active-mode-always");
            activeModeAlways.SetAttribute("key", ((int)alwaysKey.CurrentKey).ToString());

            actions.AppendChild(activeModeNever);
            actions.AppendChild(activeModeAuto);
            actions.AppendChild(activeModeAlways);

            root.AppendChild(actions);
            doc.AppendChild(root);

            return doc;
        }

        public enum Mode
        {
            AlwaysActive,
            Automatic,
            NeverActive
        }

    }
}
